using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Utiles;
using System.Configuration;

namespace PubliPayments.Negocios.Originacion
{
    public class GeneraPdf
    {
        public void GeneraReciboTarjeta(ReciboTarjetaModel modelo, ref PdfStamper pdfStamper)
        {
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            #region Fields
            pdfFormFields.SetField("Dia", modelo.Dia);
            pdfFormFields.SetField("Mes", modelo.Mes);
            pdfFormFields.SetField("Ano", modelo.Anno);
            pdfFormFields.SetField("NombreTrabajador", modelo.NombreTrabajador);
            pdfFormFields.SetField("NumeroTarjeta", EncriptaTarjeta.DesencriptarTarjeta(modelo.NumeroTarjeta));
            pdfFormFields.SetField("Credito", modelo.Credito);

            pdfFormFields.SetFieldProperty("Dia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Mes", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Ano", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NombreTrabajador", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NumeroTarjeta", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Credito", "setfflags", PdfFormField.FF_READ_ONLY, null);
            #endregion
        }

        public void GeneraCaratulaContrato(CaratulaContratoModel modelo, ref PdfStamper pdfStamper)
        {         
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            #region Fields
            pdfFormFields.SetField("TasaInteres", modelo.TasaInteres); 
            pdfFormFields.SetField("Domicilio", modelo.Domicilio); 
            pdfFormFields.SetField("MontoCredito", modelo.MontoCredito);
            pdfFormFields.SetField("NumeroTarjeta", EncriptaTarjeta.DesencriptarTarjeta(modelo.NumeroTarjeta)); 
            pdfFormFields.SetField("NSS", modelo.NSS); 
            pdfFormFields.SetField("NombreAcreditado", modelo.NombreAcreditado); 
            pdfFormFields.SetField("CURP", modelo.CURP);
            pdfFormFields.SetField("RFC", modelo.RFC); 
            pdfFormFields.SetField("Identificacion", modelo.Identificacion); 
            pdfFormFields.SetField("PlazoCredito", modelo.PlazoCredito); 
            pdfFormFields.SetField("AmortizacionMensual", modelo.AmortizacionMensual); 
            pdfFormFields.SetField("LugarFecha", modelo.LugarFecha); 
            pdfFormFields.SetField("MontoTotal", modelo.MontoTotal);
            pdfFormFields.SetField("NumeroCredito", modelo.NumeroCredito);

            pdfFormFields.SetFieldProperty("TasaInteres", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Domicilio", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("MontoCredito", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NumeroTarjeta", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NSS", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NombreAcreditado", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CURP", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("RFC", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Identificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("PlazoCredito", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("AmortizacionMensual", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("LugarFecha", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("MontoTotal", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NumeroCredito", "setfflags", PdfFormField.FF_READ_ONLY, null);

            #endregion
        }

        public void GeneraSolicitudInscripcionCredito(SolicitudInscripcionCreditoModel modelo,ref PdfStamper pdfStamper)
        {
            #region Info Radios
            //LISTA RADIOBUTTONS
            //-Genero
            //    1 -> M = 1
            //    2 -> F = 2
            //-Regimen
            //    1 -> SEPARACION DE BIENES = 1
            //    2 -> SOCIEDAD CONYUGAL    = 2
            //    3 -> SOCIEDAD LEGAL       = 3
            //-Escolaridad
            //    1 -> SIN ESTUDIOS = SinEstudios 
            //    2 -> PRIMARIA = Primaria
            //    3 -> SECUNDARIA = Secundaria
            //    4 -> PREPARATORIA = Preparatoria
            //    5 -> TECNICO = Tecnico
            //    6 -> LICENCIATURA = Licenciatura
            //    7 -> POSGRADO = PosGrado
            //-Vivienda
            //    1 -> PROPIA = 0
            //    2 -> FAMILIARES = 1
            //-EstadoCivil
            //    1 -> SOLTERO = 2
            //    2 -> CASADO = 1
            //-Plazo
            //    1 -> 12 MESES = 12
            //    2 -> 18 MESES = 18
            //    3 -> 24 MESES = 24
            //    4 -> 30 MESES = 30
#endregion
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            #region Fields
            pdfFormFields.SetField("Colonia", modelo.Colonia);
            pdfFormFields.SetField("NumeroDependientes", modelo.NumeroDependientes);
            pdfFormFields.SetField("NumeroIdentificacion", modelo.NumeroIdentificacion);
            pdfFormFields.SetField("AnoIdentificacion", modelo.AnoIdentificacion);
            pdfFormFields.SetField("Entidad", modelo.Entidad);
            pdfFormFields.SetField("ReferenciaLada1", modelo.ReferenciaLada1);
            pdfFormFields.SetField("TelefonoLada", modelo.TelefonoLada);
            pdfFormFields.SetField("Mes", modelo.Mes);
            pdfFormFields.SetField("ApellidoMaterno", modelo.ApellidoMaterno);
            pdfFormFields.SetField("NumeroCredito", modelo.NumeroCredito);
            pdfFormFields.SetField("Domicilio", modelo.Domicilio);
            pdfFormFields.SetField("MontoManoObra", modelo.MontoManoObra);
            pdfFormFields.SetField("Text4", modelo.Text4);
            //M=1 F=2
            pdfFormFields.SetField("Genero", modelo.Genero);
            pdfFormFields.SetField("Dia", modelo.Dia);
            pdfFormFields.SetField("ReferenciaCelular2", modelo.ReferenciaCelular2);
            pdfFormFields.SetField("PensionAlimenticia", modelo.PensionAlimenticia);
            pdfFormFields.SetField("ReferenciaTelefono2", modelo.ReferenciaTelefono2);
            pdfFormFields.SetField("ReferenciaApellidoMaterno1", modelo.ReferenciaApellidoMaterno1);
            pdfFormFields.SetField("ReferenciaLada2", modelo.ReferenciaLada2);
            pdfFormFields.SetField("NombrePatron", modelo.NombrePatron);
            pdfFormFields.SetField("NRPPatron", modelo.NRPPatron);
            //SEPARACION DE BIENES = 1 SOCIEDAD CONYUGAL = 2 SOCIEDAD LEGAL = 3
            pdfFormFields.SetField("Regimen", modelo.Regimen);
            pdfFormFields.SetField("ReferenciaApellidoPaterno1", modelo.ReferenciaApellidoPaterno1);
            pdfFormFields.SetField("HorarioHoraDe", modelo.HorarioHoraDe);
            pdfFormFields.SetField("NSS", modelo.NSS);
            pdfFormFields.SetField("EmpresaTelefono", modelo.EmpresaTelefono);
            pdfFormFields.SetField("MesIdentificacion", modelo.MesIdentificacion);
            pdfFormFields.SetField("ReferenciaNombre2", modelo.ReferenciaNombre2);
            pdfFormFields.SetField("Municipio", modelo.Municipio);
            pdfFormFields.SetField("RFC", modelo.RFC);
            pdfFormFields.SetField("DiaIdentificacion", modelo.DiaIdentificacion);
            pdfFormFields.SetField("HorarioMinutosDe", modelo.HorarioMinutosDe);
            pdfFormFields.SetField("Telefono", modelo.Telefono);
            //SIN ESTUDIOS = SinEstudios PRIMARIA = Primaria SECUNDARIA = Secundaria PREPARATORIA = Preparatoria TECNICO = Tecnico LICENCIATURA = Licenciatura POSGRADO = PosGrado
            //pdfFormFields.SetField("Escolaridad", modelo.Escolaridad);
            pdfFormFields.SetField("ReferenciaNombre1", modelo.ReferenciaNombre1);
            //PROPIA = 0 FAMILIARES = 1
            //pdfFormFields.SetField("Vivienda", modelo.Vivienda);
            pdfFormFields.SetField("MontoCredito", modelo.MontoCredito);
            pdfFormFields.SetField("EmpresaExtension", modelo.EmpresaExtension);
            pdfFormFields.SetField("ReferenciaCelular1", modelo.ReferenciaCelular1);
            pdfFormFields.SetField("Nombre", modelo.Nombre);
            pdfFormFields.SetField("ReferenciaTelefono1", modelo.ReferenciaTelefono1);
            pdfFormFields.SetField("Celular", modelo.Celular);
            pdfFormFields.SetField("CURP", modelo.CURP);
            pdfFormFields.SetField("ReferenciaApellidoMaterno2", modelo.ReferenciaApellidoMaterno2);
            pdfFormFields.SetField("EmpresaLada", modelo.EmpresaLada);
            //SOLTERO = 2 CASADO = 1
            pdfFormFields.SetField("EstadoCivil", modelo.EstadoCivil);
            pdfFormFields.SetField("CorreoElectronico", modelo.CorreoElectronico);
            pdfFormFields.SetField("ApellidoPaterno", modelo.ApellidoPaterno);
            pdfFormFields.SetField("CodigoPostal", modelo.CodigoPostal);
            pdfFormFields.SetField("HorarioMinutosA", modelo.HorarioMinutosA);
            pdfFormFields.SetField("TipoIdentificacion", modelo.TipoIdentificacion);
            //12 MESES = 12 18 MESES = 18 24 MESES = 24 30 MESES = 30
            pdfFormFields.SetField("Plazo", modelo.Plazo);
            pdfFormFields.SetField("HorarioHoraA", modelo.HorarioHoraA);
            pdfFormFields.SetField("Ano", modelo.Ano);
            pdfFormFields.SetField("ReferenciaApellidoPaterno2", modelo.ReferenciaApellidoPaterno2);
            pdfFormFields.SetField("NumeroTarjeta", EncriptaTarjeta.DesencriptarTarjeta(modelo.NumeroTarjeta));
            pdfFormFields.SetField("Ciudad", modelo.Ciudad);

            pdfFormFields.SetFieldProperty("Colonia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NumeroDependientes", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NumeroIdentificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("AnoIdentificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Entidad", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaLada1", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("TelefonoLada", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Mes", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ApellidoMaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NumeroCredito", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Domicilio", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("MontoManoObra", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Text4", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Genero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Dia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaCelular2", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("PensionAlimenticia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaTelefono2", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaApellidoMaterno1", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaLada2", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NombrePatron", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NRPPatron", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Regimen", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaApellidoPaterno1", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("HorarioHoraDe", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NSS", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("EmpresaTelefono", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("MesIdentificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaNombre2", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Municipio", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("RFC", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("DiaIdentificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("HorarioMinutosDe", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Telefono", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Escolaridad", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaNombre1", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Vivienda", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("MontoCredito", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("EmpresaExtension", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaCelular1", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaTelefono1", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Celular", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CURP", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaApellidoMaterno2", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("EmpresaLada", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("EstadoCivil", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CorreoElectronico", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ApellidoPaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CodigoPostal", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("HorarioMinutosA", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("TipoIdentificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Plazo", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("HorarioHoraA", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Ano", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("ReferenciaApellidoPaterno2", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("NumeroTarjeta", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Ciudad", "setfflags", PdfFormField.FF_READ_ONLY, null);
            #endregion
        }

        public void GeneraConsultaBuro(ConsultaBuroModel modelo,ref PdfStamper pdfStamper)
        {
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            pdfFormFields.SetField("i", modelo.i);
            pdfFormFields.SetField("LugarFecha", modelo.LugarFecha);
            pdfFormFields.SetField("EntidadFinanciera", modelo.EntidadFinanciera);
            pdfFormFields.SetField("Nombre", modelo.Nombre);
            pdfFormFields.SetFieldProperty("i", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("LugarFecha", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("EntidadFinanciera", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
        }

        public string Upload(ReciboTarjetaModel modelo,string visita,int orden,string campo,string url,string fullpath)
        {
            var pdfTemplate = ConfigurationManager.AppSettings["pdfTemplates"];

            if (modelo == null)
                throw new Exception(" Modelo nulo ");
            

            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "Upload ReciboTarjeta Orden: " + orden.ToString(CultureInfo.InvariantCulture));
            try
            {
                const string nombrePdf = "ReciboTarjeta";
                const string ext = ".pdf";
                var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));

                GeneraReciboTarjeta(modelo, ref pdfStamper);

                pdfStamper.FormFlattening = false;
                pdfStamper.Close();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf",
                    "Upload - OK: " + fullpath + " - url: " + url);

                return url;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "Upload ReciboTarjeta Orden: " + orden.ToString(CultureInfo.InvariantCulture) + " - Error:" + ex.Message );
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Documento ReciboTarjeta", "Error as generar archivo");
                return "-1";
            }
        }

        public string Upload(CaratulaContratoModel modelo, string visita, int orden, string campo, string url, string fullpath)
        {
            if (modelo == null)
                throw new Exception(" Modelo nulo ");
                
            try
            {
                var pdfTemplate = ConfigurationManager.AppSettings["pdfTemplates"];

                const string nombrePdf = "CaratulaContrato";
                const string ext = ".pdf";
                
                var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));

                GeneraCaratulaContrato(modelo, ref pdfStamper);

                pdfStamper.FormFlattening = false;
                pdfStamper.Close();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf", "Upload - OK: " + fullpath + " - url:" + url);
                return url;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf",
                    "Upload CaratulaContrato Orden: " + orden.ToString(CultureInfo.InvariantCulture) + " - Error:" + ex.Message);

                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Documento CaratulaContrato", "Error al generar archivo");
                return "-1";
            }
        }

        public string Upload(SolicitudInscripcionCreditoModel modelo, string visita, int orden, string campo, string url, string fullpath)
        {
            if (modelo == null)
                throw new Exception(" Modelo nulo ");
            
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "Upload SolicitudInscripcionCredito Orden: " + orden.ToString(CultureInfo.InvariantCulture));

                string pdfTemplate = ConfigurationManager.AppSettings["pdfTemplates"];

                const string nombrePdf = "SolicitudInscripcionCredito";
                const string ext = ".pdf";
                
                var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));

                GeneraSolicitudInscripcionCredito(modelo, ref pdfStamper);

                pdfStamper.FormFlattening = false;
                pdfStamper.Close();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf",
                    "Upload - OK: " + fullpath + " - url:" + url);
                return url;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf",
                    "Upload Orden: " + orden.ToString(CultureInfo.InvariantCulture) + " - Error:" + ex.Message);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Documento SolicitudInscripcionCredito", "Error al generar el archivo");
                return "-1";
            }
        }

        public string Upload(ConsultaBuroModel modelo, string visita, int orden, string campo, string url, string fullpath)
        {
            try
            {
                var pdfTemplate = ConfigurationManager.AppSettings["pdfTemplates"];

                if (modelo == null)
                    throw new Exception("Modelo nulo ");

                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "Upload ConsultaBuro Orden: " + orden.ToString(CultureInfo.InvariantCulture));
                const string nombrePdf = "ConsultaBuro";
                const string ext = ".pdf";

                var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));

                GeneraConsultaBuro(modelo,ref pdfStamper);

                pdfStamper.FormFlattening = false;
                pdfStamper.Close();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf",
                    "Upload - OK: " + fullpath + " - url:" + url);
                return url;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf",
                    "Upload ConsultaBuro Orden: " + orden.ToString(CultureInfo.InvariantCulture) + " - Error: " + ex.Message);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Documento ConsultaBuro", "Error al generar el archivo");
                return "-1";
            }
        }

        public void Rutas(ref string fullpath, ref string url, string visita, string orden, string campo = "",string ext = "pdf")
        {
            try
            {
                var fase = "";
                if (fullpath == null) throw new ArgumentNullException("fullpath");
                var directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"];
                var urlmagenes = ConfigurationManager.AppSettings["CWDirectorioDocumentosOriginacionDescarga"];

                if (visita != "")
                    fase = @"\" + (visita == "1" ? "Originacion" : (visita == "2" ? "Formalizacion" : "Preautorizacion"));
                
                var path = directorioImagenes + orden + fase;

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                fullpath = path + @"\";
                if (campo != "")
                    fullpath += campo + "." + ext;
                
                url = urlmagenes + orden + "/" + fase + "/" + campo + "." + ext;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "Rutas - Error: " + ex.Message);
            }
        }

        public Dictionary<string,string> ObtenerPaths(string orden)
        {
            var lista = new Dictionary<string, string>();
            var fullpath = "";
            var url = "";

            try
            {
                for(var i = 1 ; i <= 3 ; i++ )
                {
                    Rutas(ref fullpath, ref url, i.ToString(CultureInfo.InvariantCulture), orden.ToString(CultureInfo.InvariantCulture));
                    
                    var rutasArchivos = Directory.GetFiles(fullpath).ToList();

                    foreach (var rutaArchivo in rutasArchivos)
                    {
                        if ((rutaArchivo.Contains("Foto") || rutaArchivo.Contains("ListaNominal")) && !rutaArchivo.Contains("FotoBiometrico"))
                        {
                            var file = Path.GetFileNameWithoutExtension(rutaArchivo);
                            
                            Rutas(ref fullpath, ref url, i.ToString(CultureInfo.InvariantCulture), orden.ToString(CultureInfo.InvariantCulture), file,"jpg");
                            if (File.Exists(fullpath))
                                lista.Add(file,fullpath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "ObtenerPaths - Error: " + ex.Message);
            }
            return lista;
        }

        public void GeneraPdfCompleto(int orden, string campo = "Unificado")
        {
            var fullpath = "";
            var url = "";
            const string visita = "";

            string[] fotosOrden = {"FotoSolCredito","FotoIdOficial",
                                   "DocListaNominal","FotoActNacimiento",
                                   "FotoCompDomicilio","FotoAvRetencion",
                                   "FotoBuroCredito","FotoCarContrato",
                                   "FotoAcuRecTarjeta","FotoContrato1",
                                   "FotoContrato2","FotoContrato3",
                                   "FotoContrato4","FotoContrato5",
                                   "FotoContrato6","FotoContrato7",
                                   "FotoContrato8","FotoContrato9",
                                   "FotoContrato9","FotoContrato10"};
            try
            {
                var sourceFiles = ObtenerPaths(orden.ToString(CultureInfo.InvariantCulture));

                if(sourceFiles.Count == 0)
                    throw new Exception("No existe ningun archivo para realizar el documento unificado de la orden " + orden.ToString(CultureInfo.InvariantCulture));

                Rutas(ref fullpath, ref url, visita, orden.ToString(CultureInfo.InvariantCulture), campo);

                var document = new Document(PageSize.A4, 3, 3, 15, 3);
                PdfWriter pdfNuevo = PdfWriter.GetInstance(document,new FileStream(fullpath,FileMode.Create));
                document.Open();
                foreach (var foto in fotosOrden)
                {
                    foreach (var file in sourceFiles)
                    {
                        if (foto == file.Key)
                        {
                            var imagen = Image.GetInstance(file.Value);
                            imagen.Alignment = Element.ALIGN_MIDDLE;
                            imagen.ScaleAbsoluteWidth(500);
                            imagen.ScaleAbsoluteHeight(800);
                            document.Add(imagen);
                            break;
                        }
                    }
                }
                document.Close();
                pdfNuevo.Close();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "GeneraPdfCompleto - Error: " + error);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Error Documento Unificado Originacion", "Error al generar el archivo unificado Error - " + error);
            }
        }
    }
}
