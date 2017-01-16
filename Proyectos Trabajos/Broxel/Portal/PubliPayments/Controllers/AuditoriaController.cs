using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Office.Utils;
using DevExpress.XtraReports.Native;
using PubliPayments.Entidades;
using PubliPayments.Models;
using PubliPayments.Negocios;

namespace PubliPayments.Controllers
{
    public class AuditoriaController : Controller
    {
        //
        // GET: /Auditoria/

        public ActionResult Index()
        {
            ViewData["Rol"] = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);
            ViewData["Delegacion"] = new EntRankingIndicadores().ObtenerDelegacionUsuario(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario))[0];
            return View();
        }

        public ActionResult ComboFiltro(string idCombo, int tipoCombo, string delegacion, string despacho, string supervisor, string conexionBd = "SqlDefault")
        {
            var lista = new List<OpcionesFiltroDashboard>();
            var elemento = new OpcionesFiltroDashboard();
            
            switch (idCombo)
            {
                case "supervisorCombo":
                    if (tipoCombo != 3 && tipoCombo != 1)
                    {
                        throw new HttpException(500, "");
                    }

                    elemento.Value = "9999";
                    elemento.Description = "Supervisor";

                    if (tipoCombo == 1)
                    {
                        lista.Add(elemento);
                        return PartialView(lista);
                    }
                    break;
                case "gestorCombo":
                    if (tipoCombo != 4 && tipoCombo != 1)
                    {
                        throw new HttpException(500, "");
                    }

                    elemento.Value = "9999";
                    elemento.Description = "Gestor";

                    if (tipoCombo == 1)
                    {
                        lista.Add(elemento);
                        return PartialView(lista);
                    }
                    break;
                case "despachoCombo":
                    if (tipoCombo != 2)
                    {
                        throw new HttpException(500, "");
                    }

                    elemento.Value = "9999";
                    elemento.Description = "Despacho";
                    break;
                case "delegacionCombo":
                    if (tipoCombo != 5)
                    {
                        throw new HttpException(500, "");
                    }

                    elemento.Value = "9999";
                    elemento.Description = "Delegación";
                    break;
                case "dictamenCombo":
                    if (tipoCombo != 12)
                    {
                        throw new HttpException(500, "");
                    }

                    elemento.Value = "9999";
                    elemento.Description = "Dictamen";
                    break;
                case "autorizacionCombo":
                    if (tipoCombo != 13)
                    {
                        throw new HttpException(500, "");
                    }

                    elemento.Value = "9999";
                    elemento.Description = "Autorización";
                    break;
                case "historicoCombo":
                    if (tipoCombo != 14)
                    {
                        throw new HttpException(500, "");
                    }
                    lista.Add(new OpcionesFiltroDashboard { Value = "SqlDefault", Description = "Mes Actual" });
                    var model=new UsuariosServicios().ObtenerUsuariosServicios(3);

                    var listaTemp = model.Select(m => new OpcionesFiltroDashboard
                    {
                        Value = m.Nombre, Description = m.Descripcion
                    }).ToList();

                    lista.AddRange(listaTemp);
                    return PartialView(lista);
                
                case "ocrCombo":
                    if (tipoCombo != 15)
                    {
                        throw new HttpException(500, "");
                    }

                    elemento.Value = "9999";
                    elemento.Description = "Lectura OCR";
                    break;
                default:
                    throw new HttpException(500, "");

            }

            lista.Add(elemento);

            var ds = new Auditoria().ObtenerCombosAutorizaImagenes(conexionBd, idCombo, tipoCombo, delegacion, Convert.ToInt32(despacho), supervisor);
            
            var listaAux = new List<OpcionesFiltroDashboard>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var elementoAux = new OpcionesFiltroDashboard
                {
                    Value = row[0].ToString(),
                    Description = row[1].ToString()
                };
                listaAux.Add(elementoAux);
            }
            lista.AddRange(listaAux);
            
            return PartialView(lista);
        }

        public ActionResult ImagenesTemplate()
        {
            return PartialView();
        }

        public int AutorizarGestion(string autorizado, string credito, int idAuditoriaGestion, string conexionBd = "SqlDefault")
        {
            var ds = new Auditoria().InsertaAuditoriaImagenes(conexionBd, idAuditoriaGestion, Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)), credito, autorizado);

            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }

        public void AutorizarImagenes(string idAuditoriaImagenes, string imagen, string resultado, string conexionBd = "SqlDefault")
        {
            new Auditoria().InsertaAuditoriaCamposImagen(idAuditoriaImagenes, imagen, resultado, conexionBd);
        }

        public ActionResult TablaImagenes(string delegacion = "", string despacho = "", string supervisor = "", string gestor = "", string dictamen = "", string status = "", string autoriza = "", string valorOcr = "", string conexionBd = "SqlDefault")
        {
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol) == "5")
            {
                delegacion =
                    new EntRankingIndicadores().ObtenerDelegacionUsuario(
                        SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario))[0];
            }

           var ds= new Auditoria().ObtenerTablaAuditoriaImagenes(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario),delegacion, despacho, supervisor, gestor, dictamen, status,autoriza, CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta,valorOcr, conexionBd);

            var lista = new List<AuditoriaGestion>();
            var tabla = ds.Tables[0];
            
            for (var i = 0; i < tabla.Rows.Count;)
            {
                var audGes = new AuditoriaGestion();            
                var imagenNum = 1;

                audGes.Autorizacion = tabla.Rows[i]["tipoAutorizado"].ToString();
                audGes.IdOrden = tabla.Rows[i]["idOrden"].ToString();
                audGes.Credito = tabla.Rows[i]["num_Cred"].ToString();
                audGes.Dictamen = tabla.Rows[i]["Valor"].ToString();
                audGes.Despacho = tabla.Rows[i]["NombreDominio"].ToString();
                audGes.Supervisor = tabla.Rows[i]["Supervisor"].ToString();
                audGes.Gestor = tabla.Rows[i]["Gestor"].ToString();
                audGes.Delegacion = tabla.Rows[i]["Delegacion"].ToString();
                audGes.Resultado = tabla.Rows[i]["resultadoGeneral"].ToString();
                audGes.IdAuditoriaImagenes = tabla.Rows[i]["idAuditoriaImagenes"].ToString();

                var creditoActual = audGes.Credito;
                i++;


                while (i < tabla.Rows.Count && creditoActual == tabla.Rows[i][1].ToString())
                {
                    switch (imagenNum)
                    {
                        case 1:
                            audGes.ImagenTitulo1 = tabla.Rows[i]["Nombre"].ToString();
                            audGes.ImagenUrl1 = tabla.Rows[i]["Valor"].ToString();
                            audGes.ImagenResultado1 = tabla.Rows[i]["resultadoImagen"].ToString();
                            break;
                        case 2:
                            audGes.ImagenTitulo2 = tabla.Rows[i]["Nombre"].ToString();
                            audGes.ImagenUrl2 = tabla.Rows[i]["Valor"].ToString();
                            audGes.ImagenResultado2 = tabla.Rows[i]["resultadoImagen"].ToString();
                            break;
                        case 3:
                            audGes.ImagenTitulo3 = tabla.Rows[i]["Nombre"].ToString();
                            audGes.ImagenUrl3 = tabla.Rows[i]["Valor"].ToString();
                            audGes.ImagenResultado3 = tabla.Rows[i]["resultadoImagen"].ToString();
                            break;
                        case 4:
                            audGes.ImagenTitulo4 = tabla.Rows[i]["Nombre"].ToString();
                            audGes.ImagenUrl4 = tabla.Rows[i]["Valor"].ToString();
                            audGes.ImagenResultado4 = tabla.Rows[i]["resultadoImagen"].ToString();
                            break;
                        case 5:
                            audGes.ImagenTitulo5 = tabla.Rows[i]["Nombre"].ToString();
                            audGes.ImagenUrl5 = tabla.Rows[i]["Valor"].ToString();
                            audGes.ImagenResultado5 = tabla.Rows[i]["resultadoImagen"].ToString();
                            break;
                        case 6:
                            audGes.ImagenTitulo6 = tabla.Rows[i]["Nombre"].ToString();
                            audGes.ImagenUrl6 = tabla.Rows[i]["Valor"].ToString();
                            audGes.ImagenResultado6 = tabla.Rows[i]["resultadoImagen"].ToString();
                            break;
                        case 7:
                            audGes.ImagenTitulo7 = tabla.Rows[i]["Nombre"].ToString();
                            audGes.ImagenUrl7 = tabla.Rows[i]["Valor"].ToString();
                            audGes.ImagenResultado7 = tabla.Rows[i]["resultadoImagen"].ToString();
                            break;
                        case 8:
                            audGes.ImagenTitulo8 = tabla.Rows[i]["Nombre"].ToString();
                            audGes.ImagenUrl8 = tabla.Rows[i]["Valor"].ToString();
                            audGes.ImagenResultado8 = tabla.Rows[i]["resultadoImagen"].ToString();
                            break;
                        case 9:
                            audGes.ImagenTitulo9 = tabla.Rows[i]["Nombre"].ToString();
                            audGes.ImagenUrl9 = tabla.Rows[i]["Valor"].ToString();
                            audGes.ImagenResultado9 = tabla.Rows[i]["resultadoImagen"].ToString();
                            break;
                        case 10:
                            audGes.ImagenTitulo10 = tabla.Rows[i]["Nombre"].ToString();
                            audGes.ImagenUrl10 = tabla.Rows[i]["Valor"].ToString();
                            audGes.ImagenResultado10 = tabla.Rows[i]["resultadoImagen"].ToString();
                            break;
                    }

                    imagenNum++;
                    i++;
                }

                lista.Add(audGes);
            }


            ViewData["delegacion"] = delegacion;
            ViewData["despacho"] = despacho;
            ViewData["supervisor"] = supervisor;
            ViewData["gestor"] = gestor;
            ViewData["dictamen"] = dictamen;
            ViewData["status"] = status;
            ViewData["autoriza"] = autoriza;
            ViewData["conexionBd"] = conexionBd;
            ViewData["valorOcr"] = valorOcr;
            return PartialView(lista);
        }


        public void CancelacionAutomaticaXRechazo(string idAuditoriaImagenes, string credito)
        {
            new Auditoria().CancelacionAutomaticaXRechazo(credito, Convert.ToInt32(idAuditoriaImagenes), Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)), Config.AplicacionActual().Productivo,Config.AplicacionActual().Nombre);
        }
    }
}
