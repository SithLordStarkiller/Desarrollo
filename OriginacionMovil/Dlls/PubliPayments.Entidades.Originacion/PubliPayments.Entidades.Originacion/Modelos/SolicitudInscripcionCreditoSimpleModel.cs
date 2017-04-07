using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PubliPayments.Utiles;
using System.IO;

namespace PubliPayments.Entidades.Originacion.Modelos
{
    public class SolicitudInscripcionCreditoSimpleModel 
    {

        public string nss { get; set; }
        public string curp { get; set; }
        public string rfc { get; set; }
        public string apellidoPaterno { get; set; }               
        public string apellidoMaterno { get; set; }
	    public string nombre { get; set; }
	    public string calle { get; set; }
	    public string numCasaExterior{ get; set; }
	    public string numCasaInterior{ get; set; }
	    public string colonia{ get; set; }
	    public string entidad{ get; set; }
	    public string municipio{ get; set; }
	    public string cp { get; set; }
	    public string identificacionTipo{ get; set; }
	    public string identificacionNum{ get; set; }
	    public string identificacionFechaDia{ get; set; }
	    public string identificacionFechaMes{ get; set; }
	    public string identificacionFechaAno{ get; set; }
	    public string telefonoLada{ get; set; }
	    public string telefonoNumero{ get; set; }
	    public string celular{ get; set; }
	    public string genero{ get; set; }
	    public string email{ get; set; }
	    public string estadoCivil{ get; set; }
	    public string regimen{ get; set; }
	    public string tipoDevivienda{ get; set; }
	    public string dependientes{ get; set; }
	    public string escolaridad{ get; set; } //Opcional
	    public string empresaNombre{ get; set; }
	    public string empresaNRP{ get; set; }
	    public string empresaLada{ get; set; }
	    public string empresaTelefono{ get; set; }
	    public string empresaExt{ get; set; }
	    public string horarioLaboralEntrada{ get; set; }
	    public string horarioLaboralSalida{ get; set; }
	    public string referencia1ApellidoPaterno{ get; set; }
	    public string referencia1ApellidoMaterno{ get; set; }
	    public string referencia1Nombre { get; set; }
	    public string referencia1Lada{ get; set; }
	    public string referencia1Telefono{ get; set; }
	    public string referencia1Celular{ get; set; }
	    public string referencia2ApellidoPaterno{ get; set; }
	    public string referencia2ApellidoMaterno{ get; set; }
	    public string referencia2Nombre{ get; set; }
	    public string referencia2Lada{ get; set; }
	    public string referencia2Telefono{ get; set; }
	    public string referencia2Celular{ get; set; }
	    public string pensionAlimenticia{ get; set; }
	    public string creditoPlazo{ get; set; }
	    public string montoManoDeObra{ get; set; }
        public string montoCreditoSolicitado { get; set; }
        public string beneficiarioNombre { get; set; }
        public string beneficiarioApellidoPaterno { get; set; }
        public string beneficiarioApellidoMaterno { get; set; }
        public string clabe { get; set; }
        //"submit": "submit"

        public static SolicitudInscripcionCreditoSimpleModel LlenarModelo(string json)
        {
            
            
            //JObject jsonResponse = JObject.Parse(result);
            //var solicitudInscCred = jsonResponse.ToObject<SolicitudInscripcionCreditoSimpleModel>();

            var rr =JsonConvert.DeserializeObject<SolicitudInscripcionCreditoSimpleModel>(json);
            return rr;            
        }

        
    }
}
