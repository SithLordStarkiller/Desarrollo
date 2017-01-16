
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using PubliPayments.Entidades;
using System.Globalization;
using System.Data;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class Formularios
    {
        #region ProcesarXML

        int _indexElement = 1;

        readonly Dictionary<string, string> _operadorLogicoDic = new Dictionary<string, string>
        {
            {"EQUAL", "=="},
            {"MOREOREQUALTHAN", ">="},
            {"MORETHAN", ">"},
            {"LESSOREQUALTHAN", "<="},
            {"LESSTHAN", "<"},
            {"DISTINCT", "!="}
        };

        public string ProcesarFormularioXML(XmlDocument xmlFormDocument, int captura, string descripcion, string ruta)
        {
            var formularioModel = new FormularioModel();
            var root = xmlFormDocument.DocumentElement;

            try
            {
                if (root != null)
                {
                    var formularioPadre = root.FirstChild;
                    if (formularioPadre.Name != "Template")
                        return "Error";
                    // se comienza a procesar y cargar datos al formulario
                    formularioModel.Nombre = GetXmlAttrValue(formularioPadre, "Name");
                    formularioModel.Version = GetXmlAttrValue(formularioPadre, "Version");
                    formularioModel.Captura = captura;
                    formularioModel.Descripcion = descripcion;
                    formularioModel.Ruta = ruta;

                    var formulariosHijosList = formularioPadre.ChildNodes;
                    foreach (XmlNode nodo in formulariosHijosList)
                    {
                        if (nodo.Name != "Subform" || GetXmlAttrValue(nodo, "Caption") == "Dictamenes")
                            continue;
                        formularioModel.ListaSubFormularios.Add(new SubFormularioModel
                        {
                            SubFormulario = GetXmlAttrValue(nodo, "Caption"),
                            Clase = GetXmlAttrValue(nodo, "Name")

                        });

                        formularioModel.ListaSubFormularios.Last().ListaFuncionesJs.Add(new FuncionXCampoModel { Condicionales = (ObtenerCondicionales(nodo)), IdFormulario = formularioModel.IdFormulario, Campo = "Tab#" + GetXmlAttrValue(nodo, "Name") });
                        if (nodo.HasChildNodes)
                        {
                            var campos = nodo.ChildNodes;
                            foreach (XmlNode nodocampo in campos)
                            {
                                if (nodocampo.Name == "Field")
                                {
                                    var camposList = ProcesarCampoFormulario(nodocampo, null);
                                    if (camposList != null)
                                    {
                                        foreach (var element in camposList)
                                        {
                                            formularioModel.ListaSubFormularios.Last().ListaCampoXSubFormularios.Add(element);
                                        }
                                        formularioModel.ListaSubFormularios.Last().ListaFuncionesJs.Add(new FuncionXCampoModel { Condicionales = (ObtenerCondicionales(nodocampo)), IdFormulario = formularioModel.IdFormulario, Campo = camposList.First().Nombre });
                                        formularioModel.ListaSubFormularios.Last().ListaFuncionPrecargada.Add(new FuncionXCampoModel { FuncionFinal = (ObtenerFunValorCalculado(nodocampo, camposList.First().Nombre)), IdFormulario = formularioModel.IdFormulario, Campo = camposList.First().Nombre });
                                    }
                                }
                            }
                        }
                    }
                    

                    if (captura == 3)
                    {
                        foreach (var subformulario in formularioModel.ListaSubFormularios)
                        {
                            foreach (var camposXSubformulario in subformulario.ListaCampoXSubFormularios)
                            {
                                InsertaCampoXSubFormulario(camposXSubformulario, captura, formularioModel.Ruta);
                            }
                        }
                    }
                    else
                    {
                        InsertaFormulario(formularioModel);

                        foreach (var subformulario in formularioModel.ListaSubFormularios)
                        {
                            InsertaSubFormulario(subformulario);
                            foreach (var camposXSubformulario in subformulario.ListaCampoXSubFormularios)
                            {
                                InsertaCampoXSubFormulario(camposXSubformulario, captura, formularioModel.Ruta);

                                foreach (var catXcAmpo in camposXSubformulario.ListaCatalogoXCampo)
                                {
                                    InsertaCatalogoXCampo(catXcAmpo);
                                }
                            }
                            foreach (var funcionesJs in subformulario.ListaFuncionesJs)
                            {
                                GenerarFuncion(funcionesJs);
                                InsertaFuncionesJs(funcionesJs);
                                RelacionarFunciones(funcionesJs);
                            }
                            foreach (var funcionesP in subformulario.ListaFuncionPrecargada)
                            {
                                InsertaFuncionesJs(funcionesP);
                                RelacionarFuncPrecargadas(funcionesP);
                            }
                        }
                        CompementoFunciones(formularioModel.IdFormulario);
                        GenerarJavascript(formularioModel);
                    }
                }
                return "El formulario se ha procesado exitosamente";
            }
            catch (Exception ex)
            {

                return "Error de carga-" + ex.Message;
            }
        }

        private List<CampoXSubFormularioModel> ProcesarCampoFormulario(XmlNode node, string nameParent)
        {
            var campoXFormularioList = new List<CampoXSubFormularioModel>();
            List<CampoXSubFormularioModel> temp;
            var nameElement = "";
            string keyForSave;
            try
            {  // se obtiene el nombre de la llave del elemento
                var keyTextNode = node.SelectSingleNode("Value/Text");
                keyForSave = keyTextNode != null ? GetXmlAttrValue(keyTextNode, "KeyForSave") : null;
            }
            catch (Exception) { keyForSave = null; }
            try
            {
                nameElement = nameParent ?? GetXmlAttrValue(node, "Name"); //nombre del elemento
                var dictionaryNode = node.SelectSingleNode("Value/Dictionary");//valor que puede estar predefinido
                string predefinedVal = null;
                if (dictionaryNode != null)
                {
                    predefinedVal = dictionaryNode.InnerText != "" ? ("[Tabla]" + dictionaryNode.InnerText) : ""; //valor predefinido que se obtendra desde tabla
                }
                var selectSingleNode = node.SelectSingleNode("Caption/Value/Text");
                if (selectSingleNode != null)
                {
                    var text = node["Caption"] != null ? selectSingleNode.InnerXml : "";// nombre del elemento que se despliega en pantalla
                    var typeElementNode = node["UI"];  // tipo de elemento al que pertenece 
                    if (typeElementNode != null)
                    {
                        string elementType = typeElementNode.InnerXml;
                        elementType = Regex.Replace(elementType, "[^a-zA-Z]+", "", RegexOptions.Compiled).Trim();//se limpia el texto

                        elementType = nameParent != null ? "Heading" : elementType;
                        Dictionary<string, string> atributos = GetAttributes(node);
                        switch (elementType)
                        {
                            case "Heading":
                            case "Label":
                                var textClass = nameParent != null ? ("CWSubTitulo " + nameElement) : "CWSubTituloNegrita";
                                campoXFormularioList.Add(new CampoXSubFormularioModel { IdTipoCampo = 1, Orden = _indexElement++, Nombre = nameElement, Texto = text, ValorPrecargado = null, ClasesLinea = "CWCentrado " + atributos["importancia"], ClasesTexto = textClass, ClasesValor = null });
                                break;
                            case "TextEdit":
                                campoXFormularioList.Add(new CampoXSubFormularioModel { IdTipoCampo = 2, Orden = _indexElement++, Nombre = keyForSave ?? nameElement, Texto = text, ValorPrecargado = predefinedVal, ClasesLinea = "CWCentrado " + atributos["importancia"], ClasesTexto = "CWAncho300", ClasesValor = "CWAncho300 " + atributos["atributos"], Validacion = atributos["regex"] });
                                break;
                            case "RadioButton":
                                campoXFormularioList.Add(ProcesarCampoFormulario(node, nameElement).First());
                                temp = ProcesarCampoFormularioItems(node, nameElement, atributos, 3);
                                campoXFormularioList.AddRange(temp);
                                break;
                            case "CheckButton":
                                campoXFormularioList.Add(ProcesarCampoFormulario(node, nameElement).First());
                                temp = ProcesarCampoFormularioItems(node, nameElement, atributos, 4);
                                campoXFormularioList.AddRange(temp);
                                break;
                            case "GpsEdit":
                                campoXFormularioList.Add(new CampoXSubFormularioModel { IdTipoCampo = 6, Orden = _indexElement++, Nombre = keyForSave ?? nameElement, Texto = "", ValorPrecargado = null, ClasesLinea = "CWCentrado " + atributos["importancia"], ClasesTexto = null, ClasesValor = atributos["atributos"], Validacion = null });
                                break;
                            case "DateTimeEdit":
                                string[] elementos = { "", "", "" };
                                string[] valorPrecargado = { "FechaActual", "FinMesActual", "FechaActual" };
                                string[] rango = { "0", "0" };
                                complementosFecha(atributos, elementos, valorPrecargado, rango);
                                campoXFormularioList.Add(new CampoXSubFormularioModel { IdTipoCampo = 7, Orden = _indexElement++, Nombre = keyForSave ?? nameElement, Texto = text, ValorPrecargado = (String.Format("{0},{1},{2}", valorPrecargado[0], valorPrecargado[1], valorPrecargado[2])), ClasesLinea = "CWCentrado " + atributos["importancia"], ClasesTexto = "CWAncho300", ClasesValor = atributos["atributos"], Validacion = (string.Format("{0},{1}", rango[0], rango[1])) });
                                break;
                            case "ChoiceList":
                                var listaCatalogoXCampo = ListSelectElement(node);
                                campoXFormularioList.Add(new CampoXSubFormularioModel { IdTipoCampo = 9, Orden = _indexElement++, Nombre = keyForSave ?? nameElement, Texto = text, ValorPrecargado = null, ClasesLinea = "CWCentrado " + atributos["importancia"], ClasesTexto = "CWAncho300", ClasesValor = "CWAncho300 " + atributos["atributos"], ListaCatalogoXCampo = listaCatalogoXCampo });
                                break;
                            case "ImageEdit":
                                campoXFormularioList.Add(new CampoXSubFormularioModel { IdTipoCampo = 10, Orden = _indexElement++, Nombre = keyForSave ?? nameElement, Texto = text, ValorPrecargado = ".png, .jpg", ClasesLinea = "CWDerecha " + atributos["importancia"], ClasesTexto = null, ClasesValor = atributos["atributos"] });
                                break;
                            case "UpdateEdit":
                            case "Table":
                                var orden = _indexElement++;
                                campoXFormularioList.Add(new CampoXSubFormularioModel { IdTipoCampo = 11, Orden = orden, Nombre = keyForSave ?? nameElement, Texto = text, ValorPrecargado = "FuncBt" + (keyForSave ?? nameElement) + orden, ClasesLinea = "CWCentrado " + atributos["importancia"], ClasesTexto = null, ClasesValor = "CWAncho300 CWHeight30 " + atributos["atributos"] });
                                break;
                            default:
                                return null;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "FormularioXml", "ParseCampoFormulario- nameElement " + nameElement + " - " + ex.Message);
            }

            return campoXFormularioList;
        }

        /// <summary>
        /// Se encarga de procesar las condiciones para mostrar u ocultar un elemento
        /// </summary>
        /// <param name="node"> Nodo del cual se procesara la informacion</param>
        /// <returns>Elemento a validar y el valor que debera de tener</returns>
        private List<CondicionalesJsModel> ObtenerCondicionales(XmlNode node)
        {

            var result = new List<CondicionalesJsModel>();
            try
            {
                var conditionalNodesAnd = node["ConditionalFormatting"] != null ? node.SelectNodes("ConditionalFormatting/Where/And/FieldValue") : null;
                var conditionalNodesOr = node["ConditionalFormatting"] != null ? node.SelectNodes("ConditionalFormatting/Where/Or/FieldValue") : null;
                var conditionalNodesThen = node["ConditionalFormatting"] != null ? node.SelectNodes("ConditionalFormatting/Then") : null;

                if (conditionalNodesAnd != null && conditionalNodesAnd.Count > 0)
                {
                    result.AddRange(from XmlNode cNode in conditionalNodesAnd select new CondicionalesJsModel { SignoComparacion = _operadorLogicoDic[cNode.FirstChild.Name.ToUpper()], OperadorLogico = "&&", Referencia = GetXmlAttrValue(cNode, "FieldName"), ValorXEvaluar = cNode.InnerText });
                }
                if (conditionalNodesOr != null && conditionalNodesOr.Count > 0)
                {
                    result.AddRange(from XmlNode cNode in conditionalNodesOr select new CondicionalesJsModel { SignoComparacion = _operadorLogicoDic[cNode.FirstChild.Name.ToUpper()], OperadorLogico = "||", Referencia = GetXmlAttrValue(cNode, "FieldName"), ValorXEvaluar = cNode.InnerText });
                }

                if (result.Count > 0 && conditionalNodesThen != null && conditionalNodesThen.Count > 0)
                {
                    var xmlNode = conditionalNodesThen.Item(0);
                    if (xmlNode != null)
                        foreach (var x in result)
                        {
                            x.Accion = Regex.Replace(xmlNode.InnerXml, "[^a-zA-Z]+", "", RegexOptions.Compiled);
                        }
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "FormularioXml", "ObtenerCondicionales-node.InnerText- " + node.InnerText + " -" + ex.Message);
                throw;
            }

        }
        /// <summary>
        /// Obiene la funcion que va a calcular e inicializar un elemento del formulario
        /// </summary>
        /// <param name="node">Nodo a procesar</param>
        /// <param name="campo">Campo al cual se va a asignar el resultado de la funcion</param>
        /// <returns>Funcion javascript</returns>
        private StringBuilder ObtenerFunValorCalculado(XmlNode node, string campo)
        {
            var result = new StringBuilder();
            try
            {
                var calculoValor = node["Value"] != null ? node.SelectSingleNode("Value/Text") : null;
                if (calculoValor != null)
                {
                    if (calculoValor.InnerText.StartsWith("![CDATA[="))
                    {
                        result.Append(string.Format("$('#{0}').val(Calc('{1}'));", campo, calculoValor.InnerText.Replace("![CDATA[=", "").Replace("]]", "") + ""));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "FormularioXml", "ObtenerFuncPrecargadas-node.InnerText " + node.InnerText + " -" + ex.Message);
                throw;
            }

            return result;
        }
        /// <summary>
        /// Procesa las propiedades iniciales del elemento visibilidad,valores de entrada permitidos etc
        /// </summary>
        /// <param name="node">nodo a procesar</param>
        /// <returns>array  0- edicion,1- visibildad, 2- Regex validacion de entrada</returns>
        private Dictionary<string, string> GetAttributes(XmlNode node)
        {
            var attributes = new string[10, 2];

            //var attributesArr = new[] { "", "", "" };
            var attributesDic = new Dictionary<string, string>() { { "atributos", "" }, { "importancia", "" }, { "regex", "" }, { "date", "" } };

            try
            {
                var attributesRegex = node.SelectNodes("Validate/Regex/Value/Text"); //se obtiene alguna validacion Regex que se deba cumplir para el elemento --regex
                var attributesNodes = node.SelectNodes("Settings/add"); // se obtienen propiedades iniciales del elemento (visibilidad,lectura) -- atributos
                var attributesReq = node.SelectNodes("Validate/Required"); // se verifica si el elemento debe de ser requerido --importancia

                if (attributesRegex != null && attributesRegex.Count > 0)
                {
                    var textLong = attributesRegex[0].InnerText;
                    int longitud;

                    do
                    {
                        longitud = textLong.IndexOf("{", StringComparison.Ordinal);
                        textLong = textLong.Substring(longitud + 1);
                        longitud = textLong.IndexOf("{", StringComparison.Ordinal);

                    } while (longitud != -1);

                    if (attributesRegex[0].InnerText.StartsWith("^[A-"))
                    {
                        //  attributesArr[2] = "^[abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789áéíóúÁÉÍÓÚ&-_ ]{" + textLong.Substring(0, textLong.IndexOf("}", StringComparison.Ordinal)) + "}$";
                        attributesDic["regex"] += "^[abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789áéíóúÁÉÍÓÚ&-_ ]{" + textLong.Substring(0, textLong.IndexOf("}", StringComparison.Ordinal)) + "}$";
                    }
                    else if (attributesRegex[0].InnerText.StartsWith("^[0-9]"))
                    {
                        //attributesArr[2] = "^[0123456789]{" + textLong.Substring(0, textLong.IndexOf("}", StringComparison.Ordinal)) + "}$";
                        attributesDic["regex"] += "^[0123456789]{" + textLong.Substring(0, textLong.IndexOf("}", StringComparison.Ordinal)) + "}$";
                    }
                    else
                    {
                        attributesDic["regex"] += attributesRegex[0].InnerText;
                    }
                }
                if (attributesReq != null && attributesReq.Count > 0)
                {
                    //attributesArr[1] += " CWRec ";
                    attributesDic["importancia"] += " CWRec ";
                }

                if (attributesNodes != null && attributesNodes.Count > 0)
                {
                    int count = 0;
                    foreach (XmlNode attributeNode in attributesNodes)
                    {
                        var value = GetXmlAttrValue(attributeNode, "value");
                        bool applyAttr = value != "";
                        attributes[count, 0] = applyAttr ? GetXmlAttrValue(attributeNode, "key") : "";
                        attributes[count, 1] = applyAttr ? value : "";
                        count++;
                    }
                }
                else
                {
                    //return attributesArr;
                    return attributesDic;
                }

                for (int att = 0; att < 10; att++)
                {
                    if (attributes[att, 0] != null)
                    {
                        switch (attributes[att, 0])
                        {
                            case "readonly":
                                //attributesArr[0] = (attributes[att, 1] == "true") ? "CWDisabled" : "";
                                attributesDic["atributos"] += (attributes[att, 1] == "true") ? "CWDisabled" : "";
                                break;
                            case "visible":
                                //attributesArr[1] += (attributes[att, 1] == "false") ? "CWHidden" : "";
                                attributesDic["importancia"] += (attributes[att, 1] == "false") ? "CWHidden" : "";
                                break;
                            case "min":
                                attributesDic["date"] += "|min" + attributes[att, 1];
                                break;
                            case "max":
                                attributesDic["date"] += "|max" + attributes[att, 1];

                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "FormularioXml", "GetAttributes-node.InnerText " + node.InnerText + " -" + ex.Message);
                throw;
            }
            //return attributesArr;
            return attributesDic;
        }

        private string GetXmlAttrValue(XmlNode node, string attribute)
        {
            string response = "";
            try
            {
                if (node != null)
                {
                    response = node.Attributes != null ? node.Attributes[attribute].Value : "";
                }
            }
            catch (Exception ex)
            {
                if (attribute != "KeyForSave")
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "FormularioXml", "GetXmlAttrValue- " + attribute + " -" + ex.Message);
                }
                throw;
            }

            return response;
        }

        private List<CampoXSubFormularioModel> ProcesarCampoFormularioItems(XmlNode node, string nombrePadre, Dictionary<string, string> atributos, int idTipoCampo)
        {
            var entCampoFormularioList = new List<CampoXSubFormularioModel>();
            try
            {
                var text = node["Items"] != null ? node.SelectNodes("Items/Text") : null;
                var keyText = node["Items"] != null ? node.SelectNodes("Value/Text") : null;
                var nombre = nombrePadre;
                var clasesLinea = "CWCentrado" + (idTipoCampo == 3 ? "" : " " + nombrePadre);
                if (text == null) return entCampoFormularioList;
                for (int i = 0; i < text.Count; i++)
                {
                    if (keyText != null)
                    {
                        var xmlNode = text.Item(i);
                        if (xmlNode != null)
                            entCampoFormularioList.Add(new CampoXSubFormularioModel { IdTipoCampo = idTipoCampo, Orden = _indexElement++, Nombre = idTipoCampo == 3 ? nombre : GetXmlAttrValue(keyText.Item(i), "KeyForSave"), Texto = xmlNode.InnerText, ValorPrecargado = "false", ClasesLinea = clasesLinea + " " + atributos["importancia"], ClasesTexto = "CWDerecha CWAncho300", ClasesValor = "CWMargenIzquierdo20 CWIzquierda CWAncho200 " + atributos["atributos"] });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "FormularioXml", "ParseCampoFormulario-text " + node.InnerText + " -" + ex.Message);
                throw;
            }
            return entCampoFormularioList;
        }

        private List<CatalogoXCampoModel> ListSelectElement(XmlNode node)
        {
            var catalogoXCampoList = new List<CatalogoXCampoModel>();

            try
            {
                var listElements = node["Items"] != null ? node.SelectNodes("Items/Text") : null;

                if (listElements == null) return catalogoXCampoList;
                for (int i = 0; i < listElements.Count; i++)
                {
                    var xmlNode = listElements.Item(i);
                    if (xmlNode != null)
                    {
                        var val = xmlNode.InnerText.Split(',')[0];
                        var text = xmlNode.InnerText.Split(',')[1];
                        catalogoXCampoList.Add(new CatalogoXCampoModel { Texto = text, Valor = val });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "FormularioXml", "ListSelectElement- " + node.InnerText + " -" + ex.Message);
                throw;
            }
            return catalogoXCampoList;
        }


        private void complementosFecha(Dictionary<string, string> atributos, string[] elementos, string[] valorPrecargado, string[] rango)
        {
            if (atributos.ContainsKey("date"))
                elementos = atributos["date"].Split('|');
            foreach (var x in elementos)
            {
                const string val = "FechaActual{0}{1}";
                if (x.StartsWith("min"))
                {
                    rango[0] = Regex.Replace(x, "[^0-9]+", "", RegexOptions.Compiled).Trim();
                    switch (x.Substring(x.Length - 1))
                    {
                        case "d":
                            valorPrecargado[0] = String.Format(val, x.Substring(3, 1), "dias");
                            break;
                        case "m":
                            valorPrecargado[0] = String.Format(val, x.Substring(3, 1), "mes");
                            break;
                    }
                }
                else if (x.StartsWith("max"))
                {
                    rango[1] = Regex.Replace(x, "[^0-9]+", "", RegexOptions.Compiled).Trim();
                    switch (x.Substring(x.Length - 1))
                    {
                        case "d":
                            valorPrecargado[1] = String.Format(val, x.Substring(3, 1), "dias");
                            break;
                        case "m":
                            valorPrecargado[1] = String.Format(val, x.Substring(3, 1), "mes");
                            break;
                    }
                }
            }
        }
        #endregion

        #region Formulario
        public FormularioModel InsertaFormulario(FormularioModel modelo)
        {
            var ent = new EntFormulario();
            ent.InsertaFormulario(modelo);

            foreach (var x in modelo.ListaSubFormularios)
            {
                x.IdFormulario = modelo.IdFormulario;
            }
            return modelo;
        }
        #endregion

        #region SubFormulario

        public SubFormularioModel InsertaSubFormulario(SubFormularioModel modelo)
        {
            var ent = new EntSubFormulario();
            
                ent.InsertaSubFormulario(modelo);
            if (modelo.IdSubFormulario > 0)
            {
                foreach (var cxs in modelo.ListaCampoXSubFormularios)
                {
                    cxs.IdSubformulario = modelo.IdSubFormulario;
                }
                foreach (var fp in modelo.ListaFuncionPrecargada)
                {
                    fp.IdSubFormulario = modelo.IdSubFormulario;
                    fp.IdFormulario = modelo.IdFormulario;
                }
                foreach (var f in modelo.ListaFuncionesJs)
                {
                    f.IdSubFormulario = modelo.IdSubFormulario;
                    f.IdFormulario = modelo.IdFormulario;
                }
            }

            return modelo;
        }

        #endregion

        #region CampoXSubFormulario

        public CampoXSubFormularioModel InsertaCampoXSubFormulario(CampoXSubFormularioModel modelo, int captura, string ruta)
        {
            var ent = new EntCampoXSubFormulario();
            if (captura == 3)
            {
                ent.InsertaCamposXml(ruta, modelo);
            }
            else
            {
                ent.InsertaCampoXSubFormulario(modelo);

                if (modelo.IdCampoFormulario > 0)
                {
                    foreach (var cxc in modelo.ListaCatalogoXCampo)
                    {
                        cxc.IdCampoFormulario = modelo.IdCampoFormulario;
                    }
                }
            }


            return modelo;
        }
        #endregion

        #region CatalogoXcampo

        public CatalogoXCampoModel InsertaCatalogoXCampo(CatalogoXCampoModel modelo)
        {
            var ent = new EntCatalogoXCampo();
            return ent.InsertaCatalogoXCampo(modelo);

        }
        #endregion

        #region FuncionesJs

        public FuncionXCampoModel InsertaFuncionesJs(FuncionXCampoModel modelo)
        {
            var ent = new EntFuncionesJs();
            return ent.InsertaFuncionesJs(modelo);
        }

        public Dictionary<string, string> ObtenerFuncionesCampo(int usuarioLog, string campoPadre, int idFormulario)
        {
            var ent = new EntFuncionesJs();
            var funciones = new Dictionary<string, string>();
            var ds = ent.ObtenerFuncionesCampo(usuarioLog, campoPadre, idFormulario);

            string anterior = "";
            string func = "";
            if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var actual = row["NombreCampo"].ToString();
                    if (actual != anterior && anterior != "")
                    {
                        funciones.Add(anterior, func);
                        func = row["Nombre"] + "();";
                    }
                    else
                    {
                        func += row["Nombre"] + "();";
                    }
                    anterior = actual;
                }
                if (anterior != "")
                {
                    funciones.Add(anterior, func);
                }
            }
            return funciones;
        }

        public FuncionXCampoModel GenerarFuncion(FuncionXCampoModel modelo)
        {
            if (modelo.Condicionales.Count == 0)
                return modelo;
            var validadores = new StringBuilder();
            var x = modelo.Condicionales.FirstOrDefault();
            if (x != null)
                modelo.Mostrar = x.Accion.ToUpper() == "SHOW";

            foreach (var cm in modelo.Condicionales)
            {
                validadores.Append(String.Format("{0}(ValC('{1}', '{2}','{3}'))", cm.OperadorLogico, cm.Referencia, cm.ValorXEvaluar, cm.SignoComparacion));
            }
            modelo.FuncionFinal.Append("if(");
            modelo.FuncionFinal.Append(validadores.ToString().Substring(2));
            if (modelo.Campo.StartsWith("Tab#"))
            {
                modelo.Campo = modelo.Campo.Replace("Tab#", "");

                modelo.FuncionFinal.Append("){pcFormularios.GetTabByName('" + modelo.Campo + "').SetEnabled(" + (modelo.Mostrar ? "true" : "false") + ");}");
                modelo.FuncionFinal.Append("else{pcFormularios.GetTabByName('" + modelo.Campo + "').SetEnabled(" + (modelo.Mostrar ? "false" : "true") + ");}");
            }
            else
            {
                modelo.FuncionFinal.Append("){Mostrar('" + modelo.Campo + "', " + (modelo.Mostrar ? "true" : "false") + ");}");
                modelo.FuncionFinal.Append("else{Mostrar('" + modelo.Campo + "'," + (modelo.Mostrar ? "false" : "true") + ");}");
            }
            return modelo;
        }

        public void GenerarFuncionPrincipal(FuncionJavascriptModel modelo)
        {
            modelo.FuncionPrincipal = new StringBuilder();
            modelo.FuncionPrincipal.Append(" function {Nombre}(){ {validacion}{funcionSi}{funcionNo}}");
            modelo.FuncionPrincipal.Replace("{Nombre}", modelo.Nombre);
            modelo.FuncionPrincipal.Replace("{validacion}", modelo.Validacion);
            modelo.FuncionPrincipal.Replace("{funcionSi}", modelo.FuncionNo.Trim() != "" ? "{" + modelo.FuncionSi + "}" : modelo.FuncionSi);
            modelo.FuncionPrincipal.Replace("{funcionNo}", modelo.FuncionNo.Trim() != "" ? "else{" + modelo.FuncionNo + "}" : modelo.FuncionNo);
        }

        public FuncionXCampoModel RelacionarFunciones(FuncionXCampoModel model)
        {
            if (model.Condicionales.Count == 0)
                return model;
            var entCampoSubFormulario = new EntCampoXSubFormulario();
            var keysText = "";
            foreach (CondicionalesJsModel x in model.Condicionales)
            {
                keysText += x.Referencia + ",";
            }
            var listaCampos = entCampoSubFormulario.ObtenerCampoSubFormulario(model.IdSubFormulario, keysText.Substring(0, keysText.Length - 1));
            var listIdCampos = new StringBuilder();
            if (listaCampos != null && listaCampos.Count > 0)
            {
                foreach (var campos in listaCampos)
                {
                    listIdCampos.Append(campos.IdCampoFormulario + ",");
                }
                new EntFuncionesJs().InsFuncionesXCampos(model.IdFuncionJs, listIdCampos.Remove(listIdCampos.Length - 1, 1).ToString());
            }
            return model;
        }

        public FuncionXCampoModel RelacionarFuncPrecargadas(FuncionXCampoModel model)
        {
            if (model.FuncionFinal.Length == 0)
                return model;
            var entCampoSubFormulario = new EntCampoXSubFormulario();
            var funcion = model.FuncionFinal.ToString();
            var camposFunciones = funcion.Substring(funcion.IndexOf("Calc", System.StringComparison.Ordinal) + 6);
            var listaCampos = entCampoSubFormulario.ObtenerCampoSubFormulario(model.IdSubFormulario, camposFunciones.Substring(0, camposFunciones.Length - 4).Replace('+', ','));
            if (listaCampos != null && listaCampos.Count > 0)
            {

                foreach (var campos in listaCampos)
                {
                    new EntFuncionesJs().InsFuncionesXCampos(model.IdFuncionJs, campos.IdCampoFormulario.ToString(CultureInfo.InvariantCulture));
                }
            }
            return model;
        }

        public void CompementoFunciones(int idformulario)
        {
            new EntFuncionJavascript().CompementoFunciones(idformulario);
        }

        #endregion

        #region Javascipt

        public void GenerarJavascript(FormularioModel modelo)
        {
            GeneraJavascriptBuilder(modelo);
            try
            {
                if (modelo.FuncionesJavascripts.Count > 0)
                {
                    var sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Scripts\\ScriptFormulario" + modelo.Ruta + ".js");
                    using (sw)
                    {
                        foreach (var funcionJavascript in modelo.FuncionesJavascripts)
                        {
                            sw.WriteLine(funcionJavascript.FuncionPrincipal.ToString());
                        }
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Formularios", "GenerarJavascript: " + ex.Message);

            }

        }


        public void GeneraJavascriptBuilder(FormularioModel modelo)
        {
            FuncionJavascriptModel funcionJavascript = null;
            DataSet ds = new EntFuncionesJs().ObtenerFuncionesJs(modelo.IdFormulario);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow res in ds.Tables[0].Rows)
                {
                    try
                    {
                        foreach (var columna in ds.Tables[0].Columns)
                        {
                            var campo = ((DataColumn)columna).ColumnName;
                            switch (campo)
                            {
                                case "Nombre":
                                    if (funcionJavascript == null)
                                    {
                                        funcionJavascript = new FuncionJavascriptModel(res["Nombre"].ToString());
                                    }
                                    if (funcionJavascript.Nombre != res[campo].ToString())
                                    {
                                        GenerarFuncionPrincipal(funcionJavascript);
                                        modelo.FuncionesJavascripts.Add(funcionJavascript);

                                        funcionJavascript = new FuncionJavascriptModel(res["Nombre"].ToString());
                                    }
                                    break;
                                case "Validacion":
                                    if (funcionJavascript.Validacion == null)
                                    {
                                        funcionJavascript.Validacion = res[campo].ToString();
                                    }
                                    break;
                                case "FuncionSI":
                                    funcionJavascript.FuncionSi += res[campo].ToString();
                                    break;
                                case "FuncionNo":
                                    funcionJavascript.FuncionNo += res[campo].ToString();
                                    break;
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Formularios", "GeneraJavascriptBuilder: " + ex.Message);
                    }

                }
                if (funcionJavascript != null)
                {
                    GenerarFuncionPrincipal(funcionJavascript);
                    modelo.FuncionesJavascripts.Add(funcionJavascript);

                }
            }
        }
        #endregion

        #region otras

        public List<FormularioModel> ObtenerListaFormularios(string ruta)
        {
            var entFormulario = new EntFormulario();
            return entFormulario.ObtenerListaFormularios(ruta);
        }
        /// <summary>
        ///  Obtiene los datos del formulario que esta relacionado a la Orden dada
        /// </summary>
        /// <param name="idOrden">orden a a buscar</param>
        /// <param name="idusuario">id usuario a la que este asignada, para el caso que el formulario sea distinto entre dominios</param>
        /// <param name="captura">tipo de captura a buscar 1-mobil, 2-CW, 0 - sin filtro</param>
        /// <returns>Lista de los formularios relacionados</returns>
        public List<FormularioModel> ObtenerFormulariosXOrden(int idOrden, int idusuario, int captura)
        {
            var entFormulario = new EntFormulario();
            return entFormulario.ObtenerFormularioXOrden(idOrden, idusuario, captura);
        }
        #endregion
    }
}
