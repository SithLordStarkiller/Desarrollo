using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
    public class RegenerarDocumento
    {
        private int _idOrden = 0;

        public RegenerarDocumento(int idOrden)
        {
            _idOrden = idOrden;
        }

        public void GenerarDocumentos(DocumentosRegenerar doc)
        {
            switch (doc)
            {
                    case DocumentosRegenerar.DocAcuRecTarjeta:
                    GenerarDocAcuRecTarjeta();
                    break;
                    case DocumentosRegenerar.DocBuroCredito:
                    GenerarDocBuroCredito();
                    break;
                    case DocumentosRegenerar.DocCarContrato:
                    GenerarDocCarContrato();
                    break;
                    case DocumentosRegenerar.DocCartaSessionIrr:
                    GenerarDocCartaSesionIrre();
                    break;
                    case DocumentosRegenerar.DocPreventivo:
                    GenerarDocPreventivo();
                    break;
                    case DocumentosRegenerar.DocSolCredito:
                    GenerarDocSolCredFin();
                    break;
                    case DocumentosRegenerar.Todos:
                    GenerarDocSolCredFin();
                    GenerarDocPreventivo();
                    GenerarDocCartaSesionIrre();
                    GenerarDocCarContrato();
                    GenerarDocBuroCredito();
                    GenerarDocAcuRecTarjeta();
                    new DocumentosOrden().GenerarDocumentos("DocUnificado");
                    break;
            }
        }

        public void Rutas(ref string fullpath, ref string url, string visita, string orden, string campo)
        {
            if (fullpath == null) throw new ArgumentNullException("fullpath");
            string directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"];
            string urlmagenes = ConfigurationManager.AppSettings["CWDirectorioDocumentosOriginacionDescarga"];

            const string ext = "pdf";
            var fase = visita == "1" ? "Originacion" : (visita == "2" ? "Formalizacion" : "Preautorizacion");
            var path = directorioImagenes + orden + @"\" + fase;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            fullpath = path + @"\" + campo + "." + ext;
            url = urlmagenes + orden + "/" + fase + "/" + campo + "." + ext;
        }

        private void GenerarDocPreventivo()
        {
            try
            {
                var url = "";
                var fullpath = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocPreventivo");
                var model = DocumentoPreventivoDerhabienteModel.ObtenerDocPreventivoModel(_idOrden);
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocAcuRecTarjeta", url, fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo regenerar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "RegenerarDocumento - GenerarDocAcuRecTarjeta - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
        }

        private void GenerarDocCartaSesionIrre()
        {
            try
            {
                var url = "";
                var fullpath = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocCartaSessionIrr");
                var model = CartadeSesionIrrevocableModel.ObtenerCartadeSesionIrrevocable(_idOrden);
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocAcuRecTarjeta", url, fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "RegenerarDocumento - GenerarDocCartaSesionIrre - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
        }

        private void GenerarDocAcuRecTarjeta()
        {
            try
            {
                var url = "";
                var fullpath = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocAcuRecTarjeta");
                var model = ReciboTarjetaModel.ObtenerReciboTarjetaModel(_idOrden);
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocAcuRecTarjeta", url, fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "RegenerarDocumento - GenerarDocAcuRecTarjeta - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
        }

        private void GenerarDocBuroCredito()
        {
            try
            {
                var url = "";
                var fullpath = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocBuroCredito");

                var model = ConsultaBuroModel.ObtenerConsultaBuroModel(_idOrden);
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocBuroCredito", url, fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "RegenerarDocumento - GenerarDocBuroCredito - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
        }

        private void GenerarDocCarContrato()
        {
            try
            {
                var url = "";
                var fullpath = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocCarContrato");

                var model = CaratulaContratoModel.ObtenerCaratulaContratoModel(_idOrden);
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocCarContrato", url, fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo regenerar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "RegenerarDocumento - GenerarDocCarContrato - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
        }

        private void GenerarDocSolCredFin()
        {
            try
            {
                var fullpath = "";
                var url = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocSolCredito");

                var model = SolicitudInscripcionCreditoModel.ObtenerSolicitudInscripcionCredito(_idOrden);
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocSolCredito", url, fullpath);
                if (res == "-1")
                {
                    throw new Exception(" Error no se puedo regenerar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "RegenerarDocumento - GenerarDocSolCredFin - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }

        }
    }

    public enum DocumentosRegenerar
    {
        DocAcuRecTarjeta = 1,
        DocBuroCredito = 2,
        DocCarContrato = 3,
        DocCartaSessionIrr = 4,
        DocPreventivo = 5,
        DocSolCredito = 6,
        Todos = 7
    }
}
