using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Resources;
using System.Globalization;
using System.Collections;
using wsBroxel.App_Code.GenericBL;
using wsBroxel.GeneralAccountService;
using System.Net;

namespace wsBroxel.Dispatcher
{
    public class Dispatcher
    {
        public string GetPetrusReferenceByIdCharge(int idCharge)
        {
            string returnValue = string.Empty;
            BroxelEntities entity = new BroxelEntities();
            var petrusReference = entity.Movimiento.Where(c => c.idMovimiento == idCharge).ToList();
            if (petrusReference.Count > 0) returnValue = petrusReference[0].NoAutorizacion;

            return returnValue;
        }
        public long GetBranchByIdCommerce(long idCommerce)
        {
            broxelco_rdgEntities entity = new broxelco_rdgEntities();
            var y = entity.Comercio.FirstOrDefault(x => x.idComercio == idCommerce);
            if (y == null)
                throw new Exception("No se encontro el comercio " + idCommerce.ToString(CultureInfo.InvariantCulture));
            if (y.BranchIdentifier == null)
                throw new Exception("No se encontro la equivalencia para el comercio " + idCommerce.ToString(CultureInfo.InvariantCulture));
            return (long)y.BranchIdentifier;
        }
        public string GetMessageFromResponseCode(string code, Type resource)
        {
            string message = string.Empty;
            try
            {
                var rm = new ResourceManager(resource);
                var resourceSet = rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
                var resourceDictionary = resourceSet.Cast<DictionaryEntry>()
                                        .ToDictionary(r => r.Key.ToString(),
                                                      r => r.Value.ToString());

                message = resourceDictionary["_" + code];
            }
            catch (Exception ex)
            {
                //Force to result error generic
                message = ErrorCodes._XX;
            }
            return message;
        }

        private T ExecuteJson<T>(object entityRequest, string baseUrl, string apiOperation, int idMovimiento)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var jsonEntity = JsonConvert.SerializeObject(entityRequest);

            //Read result
            var dateTimeInicio = DateTime.Now;
            var result = client.PostAsync(apiOperation, new StringContent(jsonEntity, Encoding.UTF8, "application/json")).Result;
            var dateTimeFin = DateTime.Now;
            string jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<T>(jsonResult);

            try
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    new GenericBL().PersisteMensaje(dateTimeInicio, dateTimeFin, "SIN CUENTA", 21, 6,
                        jsonEntity, jsonResult, idMovimiento);
                });
            }
            catch (Exception e)
            {
                Trace.WriteLine("Ocurrio un error al guardar el mensaje de persistencia: " + e);
            }



            return (T)response;
        }

        public PetrusCancellationResponse PetrusExecuteCancellation(PetrusCancellationRequest request, int idMovimiento)
        {
            PetrusCancellationResponse response = null;

            string baseUrl = "https://" + ConfigurationManager.AppSettings["PetrusHost"] + ":" + ConfigurationManager.AppSettings["PetrusPortRest"] + "/api/"; ;
            string chargeOperation = ConfigurationManager.AppSettings["PETRUS_CANCELLATION"];

            response = ExecuteJson<PetrusCancellationResponse>(request, baseUrl, chargeOperation,idMovimiento);
            return response;
        }
        public PetrusChargeResponse PetrusExecuteCharge(PetrusChargeRequest request, int idMovimiento)
        {
            PetrusChargeResponse response = null;

            string baseUrl = "https://" + ConfigurationManager.AppSettings["PetrusHost"] + ":" + ConfigurationManager.AppSettings["PetrusPortRest"] + "/api/";
            string chargeOperation = ConfigurationManager.AppSettings["PETRUS_CHARGE"];

            response = ExecuteJson<PetrusChargeResponse>(request, baseUrl, chargeOperation,idMovimiento);
            response.Ammount = GetBalanceFromCard(request.Card.Number);
            return response;
        }

        public decimal GetBalanceFromCard(string cardnumber)
        {
            GeneralAccountServiceClient proxy = new GeneralAccountServiceClient();
            var response = proxy.GetBalance("103", new GetBalanceDTO() { CardNumber = cardnumber });
            return response.AccountBalance;
        }
    }
}