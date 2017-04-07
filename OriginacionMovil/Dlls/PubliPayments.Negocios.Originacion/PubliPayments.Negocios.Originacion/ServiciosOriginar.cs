using System.IO;
using System.Net;
using System.Text;
using OriginacionMovil.Models;

namespace PubliPayments.Negocios.Originacion
{
    public class ServiciosOriginar
    {
        public string UrlOriginacion = "";
        public bool Login(RequestLoginModel model)
        {
            var xmlLogin = "<Usuario>" +
                                "<password>@@Usuario@@</password>" +
                                "<username>@@Password@@</username>" +
                           "</Usuario>";

            xmlLogin = xmlLogin.Replace("@@Usuario@@", model.Usuario);
            xmlLogin = xmlLogin.Replace("@@Password@@", model.Password);

            var res = Request(xmlLogin, "ValidateUserSimpleReturnGroup");

            return true;
        }

        public void Precalificacion(RequestPrecalificacionModel model)
        {
            var xmlPrecalificacion = "{ " +
                                        "\"Action\": \"Precalificacion\", " +
                                        "\"ExternalId\": \"iecajv03_20170324_172116\", " +
                                        "\"IdWorkOrder\": \"c9c4fb6d-bb6e-4429-be17-1a2912c41bdb\", " +
                                        "\"IdWorkOrderFormType\": \"1f5fc592-e820-4da2-9a32-a48ff238025f\", " +
                                        "\"InputFields\": { " +
                                            "\"OficinasCESI\": \"" + model.OficinaCesi + "\", " +
                                            "\"Nss\": \"" + model.Nss + "\", " +
                                            "\"ExternalType\": \"OriginaPP\" " +
                                        "}, " +
                                        "\"Username\": \"" + model.Usuario + "\", " +
                                        "\"WorkOrderType\": \"OriginaPP\" " +
                                      "}";

            var res = Request(xmlPrecalificacion, "FlexibleUpdateWorkOrder");
        }

        public void RegistrarCredito(RequestRegistroCreditoModel model)
        {
            var xmlRegistrarCredito = "{ " +
                                       "\"Action\":\"RegistrarCredito\"," +
                                       "\"ExternalId\":\"ievacs04_20170404_090821\", " +
                                       "\"IdWorkOrder\":\"c1ebffda-7bdb-4989-9c7a-b6b57d3ff348\", " +
                                       "\"IdWorkOrderFormType\":\"1f5fc592-e820-4da2-9a32-a48ff238025f\", " +
                                       "\"InputFields\":{  " +
                                          "\"EstadoCESI\":\"" + model.EstadoCesi + "\", " +
                                          "\"OficinasCESI\":\"" + model.OficinaCesi + "\", " +
                                          "\"Nss\":\"" + model.Nss + "\", " +
                                          "\"RfcPrecalificacion\":\"" + model.Rfc + "\", " +
                                          "\"Nombre\":\"" + model.Nombre + "\", " +
                                          "\"CURP\":\"" + model.Curp + "\", " +
                                          "\"Plazo\":\"" + model.Plazo + "\", " +
                                          "\"Nombres\":\"" + model.Nombres + "\", " +
                                          "\"APaterno\":\"" + model.APaterno + "\", " +
                                          "\"AMaterno\":\"" + model.AMaterno + "\", " +
                                          "\"GeneroCB\":\"" + model.GeneroCb + "\", " +
                                          "\"FechadeNacimientoCB\":\"" + model.FechaNacimiento + "\", " +
                                          "\"CorreoElectronico\":\"" + model.CorreoElectronico + "\", " +
                                          "\"Corrobora_CE\":\"" + model.CorreoElectronicoCorrabora + "\", " +
                                          "\"LadaCelular\":\"" + model.LadaCelular + "\", " +
                                          "\"Telefono1Cte\":\"" + model.Telefono1Cte + "\", " +
                                          "\"Lada\":\"" + model.Lada + "\", " +
                                          "\"Telefono2Cte\":\"" + model.Telefono2Cte + "\", " +
                                          "\"CentObre\":\"" + model.CentObre + "\", " +
                                          "\"LadaEmpresa\":\"" + model.LadaEmpresa + "\", " +
                                          "\"TelEmpresa\":\"" + model.TelEmpresa + "\", " +
                                          "\"ExtEmpresa\":\"" + model.ExtEmpresa + "\", " +
                                          "\"Estado\":\"" + model.Estado +"\", " +
                                          "\"Delegacion\":\"" + model.Delegacion + "\", " +
                                          "\"DelegacionId\":\"" + model.DelegacionId + "\", " +
                                          "\"Cp\":\"" + model.Cp + "\", " +
                                          "\"Colonia\":\"" + model.Colonia + "\", " +
                                          "\"Calle\":\"" + model.Calle + "\", " +
                                          "\"NumeroExt\":\"" + model.NumeroExt + "\", " +
                                          "\"NumeroInt\":\"" + model.NumeroInt + "\", " +
                                          "\"ManzanaCB\":\"" + model.ManzanaCb + "\", " +
                                          "\"LoteCB\":\"" + model.LoteCb + "\", " +
                                          "\"EntreCalle1CB\":\"" + model.EntreCalle1Cb + "\", " +
                                          "\"EntreCalle2CB\":\"" + model.EntreCalle2Cb + "\", " +
                                          "\"EdoCivil\":\"" + model.EdoCivil + "\", " +
                                          "\"RegimenCony\":\"" + model.RegimenCony + "\", " +
                                          "\"NombreRef1\":\"" + model.NombreRef1 + "\", " +
                                          "\"ApPaternoRef1\":\"" + model.APaternoRef1 + "\", " +
                                          "\"ApMaternoRef1\":\"" + model.AMaternoRef1 + "\", " +
                                          "\"TipoTelR1\":\"" + model.TipoTelR1 + "\", " +
                                          "\"LadaR1\":\"" + model.LadaR1 + "\", " +
                                          "\"Telefono1Ref1\":\"" + model.Telefono1Ref1 + "\", " +
                                          "\"NombreRef2\":\"" + model.NombreRef2 + "\", " +
                                          "\"ApPaternoRef2\":\"" + model.APaternoRef2 + "\", " +
                                          "\"ApMaternoRef2\":\"" + model.AMaternoRef2 + "\", " +
                                          "\"TipoTelR2\":\"" + model.TipoTelR2 + "\", " +
                                          "\"ValidacionTel_2\":\"" + model.ValidacionTel2 + "\", " +
                                          "\"LadaR2\":\"" + model.LadaR2 + "\", " +
                                          "\"Telefono1Ref2\":\"" + model.Telefono1Ref2 + "\", " +
                                          "\"NombreBeneficiario\":\"" + model.NombreBeneficiario + "\", " +
                                          "\"ApPaternoBen\":\"" + model.APaternoBen + "\", " +
                                          "\"ApMaternoBen\":\"" + model.AMaternoBen + "\", " +
                                          "\"DomNomDH\":\"" + model.DomNomDh + "\", " +
                                          "\"numcue\":\"" + model.Cuenta + "\", " +
                                          "\"ListaDocumentos\":\"" + model.ListaDocumentos + "\", " +
                                          "\"NumIdentificacionIFE\":\"" + model.NumIdentificacionIne + "\", " +
                                          "\"NumIdentificacionPas\":\"" + model.NumIdentigicacionPas + "\", " +
                                          "\"FechaValidezIdentificacion\":\"" + model.FechaVigenciaIdentificacion + "\", " +
                                          "\"LocalidadCB\":\"" + model.LocalidadCb + "\", " +
                                          "\"ExternalType\":\"OriginaPP\" " +
                                       "}, " +
                                       "\"Username\":\"" + model.Usuario + "\", " +
                                       "\"WorkOrderType\":\"OriginaPP\" " +
                                    "}";

            var res = Request(xmlRegistrarCredito, "FlexibleUpdateWorkOrder");
        }

        public void RegistrarCuenta(RequestRegistraCuenta model)
        {
            var xmlRegistroCuenta = "{ " +
                                      "\"Action\": \"registraTarjeta\", " +
                                      "\"ExternalId\": \"9322\", " +
                                      "\"IdWorkOrder\": \"f08b6902-94ef-4f0c-8958-c7eeacfd78e2\", " +
                                      "\"IdWorkOrderFormType\": \"04f62363-a360-46da-aa90-38257c1b4c91\", " +
                                      "\"InputFields\": { " +
                                        "\"Nss\": \"" + model.Nss + "\", " +
                                        "\"CifradoTC\": \"" + model.Tc + "\", " +
                                        "\"ExternalType\": \"Formalizacion\" " +
                                      "}, " +
                                      "\"Username\": \"" + model.Usuario + "\", " +
                                      "\"WorkOrderType\": \"Formalizacion\" " +
                                    "}";

            var res = Request(xmlRegistroCuenta, "FlexibleUpdateWorkOrder");
        }

        public void ValidarCredito(RequestValidaCreditoModel model)
        {
            var xmlValidaCredito = "{ " +
                               "\"Action\": \"ValCred\", " +
                               "\"ExternalId\": \"8913\", " +
                               "\"IdWorkOrder\": \"7b0e0f4d-38a5-444e-ac8b-002ac5d46136\", " +
                               "\"IdWorkOrderFormType\": \"04f62363-a360-46da-aa90-38257c1b4c91\", " +
                               "\"InputFields\": { " +
                                    "\"Nss\": \"" + model.Nss + "\", " +
                                    "\"ExternalType\": \"Formalizacion\" " +
                               "}, " +
                               "\"Username\": \"" + model.Usuario + "\", " +
                               "\"WorkOrderType\": \"Formalizacion\" " +
                               "}";

            var res = Request(xmlValidaCredito, "FlexibleUpdateWorkOrder");
        }

        public string Request(string info, string metodo)
        {
            var request = (HttpWebRequest)WebRequest.Create(UrlOriginacion + "/" + metodo);

            var data = Encoding.ASCII.GetBytes(info);

            request.Method = "POST";
            request.ContentType = "text/plain";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }
    }
}
