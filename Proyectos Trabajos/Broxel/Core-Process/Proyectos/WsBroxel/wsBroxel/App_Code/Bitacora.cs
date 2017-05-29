//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace wsBroxel.App_Code
//{
//    [Serializable]
//    public class Bitacora
//    {
//        public int IdTipo { get; set; }
//        public int IdUsuario { get; set; }
//        public int idComercio { get; set; }
//        //public String Comercio { get; set; }
//        public String Tipo { get; set; }
//        public String UsuarioCreacion {get;set;}
//        public DateTime FechaHoraRegistro {get;set;}
//        public String Mensaje { get; set; }
//    }

//    [Serializable]
//    public class BitacoraRequest : Request {
//        public int IdTipo { get; set; }
//        public int IdUsuario { get; set; }
//        public String Operacion {get;set;}
//        public int idComercio { get; set; }
//        public String Mensaje { get; set; }
//    }

//    [Serializable]
//    public class BitacoraResponse : Response {
//        public List<Bitacora> ListaBitacora { get; set; }
//        public BitacoraResponse()
//        {
//            ListaBitacora = new List<Bitacora>();
//        }
//    }
//}