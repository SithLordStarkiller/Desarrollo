using System;

namespace wsServices01800.AppCode.Requests
{
    [Serializable]
    public class InputFieldsBuroCredito
    {
        public string APaterno { set; get; }
        public string AMaterno { set; get; }
        public string Nombres { set; get; }
        public string Rfc { set; get; }
        public string Calle { set; get; }
        public string NumeroExt { set; get; }
        public string NumeroInt { set; get; }
        public string Colonia { set; get; }
        public string Delegacion { set; get; }
        public string Estado { set; get; }
        public string Cp { set; get; }
    }
}