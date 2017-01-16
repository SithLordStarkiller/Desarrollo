using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Charts.Native;
using DevExpress.Data.WcfLinq.Helpers;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class ActualizarDatosComerciosController : Controller
    {
        //
        // GET: /ActualizarDatosComercios/
        
        public ActionResult Index()
        {
            if (!"2".Contains(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)))
            {
                Response.Redirect("unauthorized.aspx");
            }
            return View();
        }

        public ActionResult TablaComercios()
        {
            var db = new SistemasCobranzaEntities();
            return PartialView("TablaComercios",db.Creditos.ToList());
            //return PartialView();

        }

        // GET: /ActualizarDatosComercios/Details/5

        public ActionResult Details(String Rfc = "", int IdS = 0)
        {
            var db = new SistemasCobranzaEntities();
            Creditos creditos = null;// db.Creditos.FirstOrDefault(x => x.CV_CREDITO == Rfc && x.IdentificadorSucursal == IdS);
            if (creditos == null)
            {
                return HttpNotFound();
            }
            return View(creditos);
        }

        //
        // GET: /ActualizarDatosComercios/Edit/5

        public ActionResult Edit(String Rfc = "", int IdS = 0)
        {
            var db = new SistemasCobranzaEntities();
            Creditos creditos = null;//db.Creditos.FirstOrDefault(x => x.CV_CREDITO == Rfc && x.IdentificadorSucursal == IdS);
            if (creditos == null)
            {
                return HttpNotFound();
            }
            return View(creditos);
        }

        //
        // POST: /ActualizarDatosComercios/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Creditos creditos)
        {
            var db = new SistemasCobranzaEntities();

            //var creditosTemp =
            //    db.Creditos.FirstOrDefault(
            //        x =>
            //            x.CV_CREDITO == creditos.CV_CREDITO && x.IdentificadorSucursal == creditos.IdentificadorSucursal);
            //try
            //{
            //    if (ModelState.IsValid && creditosTemp != null)
            //    {
            //        creditosTemp.RazonSocial = creditos.RazonSocial;
            //        creditosTemp.NombreComercial = creditos.NombreComercial;
            //        creditosTemp.PersonalidadJuridica = creditos.PersonalidadJuridica;
            //        creditosTemp.CV_CREDITO = creditos.CV_CREDITO;
            //        creditosTemp.PuntoDeVenta = creditos.PuntoDeVenta;
            //        creditosTemp.Calle = creditos.Calle;
            //        creditosTemp.NumExt = creditos.NumExt;
            //        creditosTemp.NumInt = creditos.NumInt;
            //        creditosTemp.Colonia = creditos.Colonia;
            //        creditosTemp.CodigoPostal = creditos.CodigoPostal;
            //        creditosTemp.Municipio = creditos.Municipio;
            //        creditosTemp.Ciudad = creditos.Ciudad;
            //        creditosTemp.Estado = creditos.Estado;
            //        creditosTemp.Telefono_Lada = creditos.Telefono_Lada;
            //        creditosTemp.Telefono_AtencionClientes = creditos.Telefono_AtencionClientes;
            //        creditosTemp.Telefono_Extencion = creditos.Telefono_Extencion;
            //        creditosTemp.ContactoComercio_Nombre = creditos.ContactoComercio_Nombre;
            //        creditosTemp.ContactoComercio_ApellidoP = creditos.ContactoComercio_ApellidoP;
            //        creditosTemp.ContactoComercio_ApellidoM = creditos.ContactoComercio_ApellidoM;
            //        creditosTemp.ContactoComercio_Cargo = creditos.ContactoComercio_Cargo;
            //        creditosTemp.TelefonoContacto = creditos.TelefonoContacto;
            //        creditosTemp.NumExtencion = creditos.NumExtencion;
            //        creditosTemp.EmailContacto = creditos.EmailContacto;
            //        creditosTemp.HorariosAtencion_Dias = creditos.HorariosAtencion_Dias;
            //        creditosTemp.HorariosAtencion_Horario = creditos.HorariosAtencion_Horario;
            //        creditosTemp.PaginaWeb = creditos.PaginaWeb;
            //        creditosTemp.RepresentanteLegal_ApellidoP = creditos.RepresentanteLegal_ApellidoP;
            //        creditosTemp.RepresentanteLegal_ApellidoM = creditos.RepresentanteLegal_ApellidoM;
            //        creditosTemp.RepresentanteLegal_Nombre = creditos.RepresentanteLegal_Nombre;
            //        creditosTemp.RepresentanteLegal_Cargo = creditos.RepresentanteLegal_Cargo;
            //        creditosTemp.EmailRepresentanteLegal = creditos.EmailRepresentanteLegal;
            //        creditosTemp.TelefonoRepresentanteLegal = creditos.TelefonoRepresentanteLegal;
            //        creditosTemp.NumExtencionRepresentanteLegal = creditos.NumExtencionRepresentanteLegal;
            //        creditosTemp.ObjetoSociedad = creditos.ObjetoSociedad;
            //        creditosTemp.FechaConstitucion = creditos.FechaConstitucion;
            //        creditosTemp.DomicilioFiscal = creditos.DomicilioFiscal;
            //        creditosTemp.ActividadRegistrada = creditos.ActividadRegistrada;
            //        creditosTemp.NumEmpleadosTotal = creditos.NumEmpleadosTotal;
            //        creditosTemp.NumEmpleadosCadaSucursal = creditos.NumEmpleadosCadaSucursal;
            //        creditosTemp.NumSucursales = creditos.NumSucursales;
            //        creditosTemp.Socios_Duenio = creditos.Socios_Duenio;
            //        creditosTemp.Socios_Accionista1 = creditos.Socios_Accionista1;
            //        creditosTemp.Socios_Accionista2 = creditos.Socios_Accionista2;
            //        creditosTemp.Socios_Accionista3 = creditos.Socios_Accionista3;
            //        creditosTemp.SICPreponderante = creditos.SICPreponderante;
            //        creditosTemp.ComentariosResolucion = creditos.ComentariosResolucion;
            //    }

            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ActualizarDatosComerciosController", "Edit: " + ex.Message);

            //}

            return View(creditos);
        }


    }
}
