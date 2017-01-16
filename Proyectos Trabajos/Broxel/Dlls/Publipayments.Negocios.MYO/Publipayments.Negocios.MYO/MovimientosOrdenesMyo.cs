using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using PubliPayments.Entidades;
using PubliPayments.Entidades.MYO;

namespace Publipayments.Negocios.MYO
{
    public static class MovimientosOrdenesMyo
    {
        public static string AutorizaMyo(int idOrden, string idUsuario)
        {
            var datosOrden = EntOrdenesMyo.ObtenerInfoOrdenMyo(idOrden);
            try
            {
                if (datosOrden.Etiqueta == "ACREDITADO" && datosOrden.idVisita == 1)
                {
                    EntOrdenesMyo.AutorizaMyo(idOrden, 3, 2, "", idUsuario);
                    EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario),2,3,idOrden,"");
                }
                else
                {
                    if (datosOrden.Etiqueta == "ACREDITADO" && datosOrden.idVisita == 2)
                    {
                        if (datosOrden.mayorAlLimite != "0")
                        {
                            EntOrdenesMyo.AutorizaMyo(idOrden, 3, 3,"",idUsuario);
                            EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 3, 3, idOrden, "");
                            var ent = new EntLoan();
                            ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 6,datosOrden.Etiqueta);
                        }
                        else
                        {
                            EntOrdenesMyo.AutorizaMyo(idOrden,4,-1,"",idUsuario);
                            EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 2, 4, idOrden, "");
                            var ent = new EntLoan();
                            ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 5, datosOrden.Etiqueta);
                        }
                    }
                    else
                    {
                        if (datosOrden.Etiqueta == "ACREDITADO" && datosOrden.idVisita == 3)
                        {
                            EntOrdenesMyo.AutorizaMyo(idOrden, 4, -1,"",idUsuario);
                            EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 3, 4, idOrden, "");
                            var ent = new EntLoan();
                            ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 5, datosOrden.Etiqueta);
                        }
                    }

                }

                if (datosOrden.Etiqueta == "Inversionista" && datosOrden.idVisita == 1)
                {
                    EntOrdenesMyo.AutorizaMyo(idOrden, 3, 4,"",idUsuario);
                    EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 4, 3, idOrden, "");
                }
                else
                {
                    if (datosOrden.Etiqueta == "Inversionista" && datosOrden.idVisita == 4)
                    {
                        EntOrdenesMyo.AutorizaMyo(idOrden, 3, 5, "",idUsuario);
                        EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 5, 3, idOrden, "");
                    }
                    else
                    {
                        if (datosOrden.Etiqueta == "Inversionista" && datosOrden.idVisita == 5)
                        {
                            EntOrdenesMyo.AutorizaMyo(idOrden, 4, -1, "",idUsuario);
                            EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 5, 4, idOrden, "");
                            var ent = new EntLoan();
                            ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 5, datosOrden.Etiqueta);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ocurrio un error al procesar la instruccion. Por favor intente de nuevo.";
            }
            return "Autorizado";
        }

        public static string RechazaMyo(int idOrden, string idUsuario)
        {
            try
            {
                var datosOrden = EntOrdenesMyo.ObtenerInfoOrdenMyo(idOrden);
                EntOrdenesMyo.RechazaMyo(idOrden,idUsuario);
                EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario),datosOrden.idVisita, 2, idOrden, "");
                var ent = new EntLoan();
                ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), -1, datosOrden.Etiqueta);
            }
            catch (Exception ex)
            {
                return "Ocurrio un error al procesar la instruccion. Por favor intente de nuevo.";
            }
            return "Rechazado";
        }

        public static string ReasignaMyo(int idOrden, string idUsuario)
        {
            try
            {
                var datosOrden = EntOrdenesMyo.ObtenerInfoOrdenMyo(idOrden);
                if (datosOrden.idVisita==1)
                {
                    EntRespuestasMYO.BorrarRespuestasOrden(idOrden, "DocCotejadoINE");
                    EntRespuestasMYO.BorrarRespuestasOrden(idOrden, "DocCotejadoRENAPO");
                    EntOrdenesMyo.ReasignaMyo(idOrden,idUsuario);
                    EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), datosOrden.idVisita, 1, idOrden, "I");
                    var ent = new EntLoan();
                    ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 3, datosOrden.Etiqueta);
                }
                else
                {
                    EntOrdenesMyo.ReasignaMyo(idOrden,idUsuario);
                    EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), datosOrden.idVisita, 1, idOrden, "I");
                    var ent = new EntLoan();
                    ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 3, datosOrden.Etiqueta);
                }
            }
            catch (Exception ex)    
            {
                return "Ocurrio un error al procesar la instruccion. Por favor intente de nuevo.";
            }
            return "Reasignado";
        }


        public static string ProcesarRespuestas(int idOrden, int idusuario)
        {
            var formularioModel = ObtenerFormulariosXOrden(idOrden, idusuario, 2)[0];
            string resultado = "";
            switch (formularioModel.Nombre.ToUpper())
            {
                case "MYOMC":
                    break;
                case "MYOREFERENCIAS":
                    resultado = ValidaReferencias(idOrden, idusuario);
                    break;
            }
            return resultado;
        }

        public static string ValidaReferencias(int idOrden, int idusuario)
        {
            var entLoan = new EntLoan();
            var respuestas = new EntRespuestasMYO().ProcesarRespuestasMyo(idOrden);
            string resultado="";
            if (respuestas.Tables.Count > 0)
            {
                var situacion = respuestas.Tables[0].Rows[0]["Resultado"].ToString();

                switch (situacion.ToUpper())
                {
                    case "VALIDO":
                        resultado = AutorizaMyo(idOrden, idusuario.ToString());
                        break;
                    case "REASIGNAR":
                        var referenciasList=new List<ReferenciasModel>();
                        foreach (DataRow rows in respuestas.Tables[1].Rows)
                        {
                            EntRespuestasMYO.BorrarRespuestasOrden(idOrden, rows["tipoRef"].ToString());
                            entLoan.ActualizarDatosRefImg(rows["resultado"].ToString(), Convert.ToInt32(rows["idRef"].ToString()), rows["tipoRef"].ToString().Substring(0, rows["tipoRef"].ToString().Length-1), 3);
                            referenciasList.Add(new ReferenciasModel { Comentario = rows["resultado"].ToString(), IdDocumento = rows["idRef"].ToString(), Nombre = rows["tipoRef"].ToString() });
                        }

                        CorreosMyo.ReferenciasIncorrectas(referenciasList, idOrden);
                        resultado = ReasignaMyo(idOrden, idusuario.ToString());
                        break;
                    case "RECHAZAR":
                        resultado = RechazaMyo(idOrden, idusuario.ToString());
                        break;
                }    
            }
            return resultado;

        }
        /// <summary>
        ///  Obtiene los datos del formulario que esta relacionado a la Orden dada
        /// </summary>
        /// <param name="idOrden">orden a a buscar</param>
        /// <param name="idusuario">id usuario a la que este asignada, para el caso que el formulario sea distinto entre dominios</param>
        /// <param name="captura">tipo de captura a buscar 1-mobil, 2-CW, 0 - sin filtro</param>
        /// <returns>Lista de los formularios relacionados</returns>
        public static List<FormularioModel> ObtenerFormulariosXOrden(int idOrden, int idusuario, int captura)
        {
            var entFormulario = new EntFormulario();
            return entFormulario.ObtenerFormularioXOrden(idOrden, idusuario, captura);
        }
    }
}
