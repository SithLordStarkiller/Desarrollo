using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
    public class DocumentosOrden
    {
        readonly List<Documento> _lista= new List<Documento>();
        private readonly string _idOrden = "";
        List<Documento> _listaIncompletos = new List<Documento>();
            
        public DocumentosOrden(string idOrden)
        {
            _idOrden = idOrden;
            _lista = ObtenerDocumentosOrden();
        }

        private List<Documento> ObtenerDocumentosOrden()
        {
            var ds = EntDatosProspecto.ObtenerDocumentos(_idOrden);

            return (from DataRow row in ds.Tables[1].Rows
                     where row["TipoCampos"].ToString()=="2"
                        select new Documento
                        {
                            NombreDocumento = row["Titulo"].ToString(),
                            Fase = Convert.ToInt32(row["visitaCorresp"].ToString()), 
                            Cargado = (row["Valor"] != null && row["Valor"].ToString() != "")
                        }
                    ).ToList();
        }

        public Boolean DocumentosCompletos(int fase)
        {
            _listaIncompletos = (from Documento d in _lista
                where d.Cargado == false
                && d.Fase <= fase
                select d).ToList();

            return _listaIncompletos.Count == 0;
        }

        public string GenerarDocumentos(string NombreDocumento)
        {           
            string fullpath = "";
            string url = "";
            
            switch (NombreDocumento)
            {
                case "DocSolCredito":
                    Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocSolCredito");
                    var TaskGenerarDocSolCredFin = new Task(GenerarDocSolCredFin);
                    TaskGenerarDocSolCredFin.Start();
                    break;
                case "DocCarContrato":
                    Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocCarContrato");
                    var taskGenerarDocCarContrato = new Task(GenerarDocCarContrato);
                    taskGenerarDocCarContrato.Start();
                    break;
                case "DocBuroCredito":
                    Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocBuroCredito");
                    var taskGenerarDocBuroCredito = new Task(GenerarDocBuroCredito);
                    taskGenerarDocBuroCredito.Start();
                    break;
                case "DocAcuRecTarjeta":
                    Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocAcuRecTarjeta");
                    var taskGenerarDocAcuRecTarjeta = new Task(GenerarDocAcuRecTarjeta);
                    taskGenerarDocAcuRecTarjeta.Start();
                    break;
                default:
                    break;
            }
  
            return url;
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

        private void GenerarDocAcuRecTarjeta()
        {
            try
            {
                var url = "";
                var fullpath = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocAcuRecTarjeta");
                var model = ReciboTarjetaModel.ObtenerReciboTarjetaModel(int.Parse(_idOrden));
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocAcuRecTarjeta",url,fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - GenerarDocAcuRecTarjeta - " + ex.Message;
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

                var model = ConsultaBuroModel.ObtenerConsultaBuroModel(int.Parse(_idOrden));
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocBuroCredito",url,fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - GenerarDocBuroCredito - " + ex.Message;
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

                var model = CaratulaContratoModel.ObtenerCaratulaContratoModel(int.Parse(_idOrden));
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocCarContrato",url,fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - GenerarDocCarContrato - " + ex.Message;
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

                var model = SolicitudInscripcionCreditoModel.ObtenerSolicitudInscripcionCredito(int.Parse(_idOrden));
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocSolCredito", url, fullpath);
                if (res == "-1")
                {
                    throw new Exception(" Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - GenerarDocSolCredFin - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
            
        }
    }
}
