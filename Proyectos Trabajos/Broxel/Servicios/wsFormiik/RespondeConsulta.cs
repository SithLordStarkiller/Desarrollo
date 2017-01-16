using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PubliPayments
{
    
    [DataContract(Namespace = "")]
    [Serializable]
    public class Settings
    {
        public string ReadOnly { get; set; }
        public string Requested { get; set; }
        public string Visible { get; set; }
    }

    [DataContract(Namespace = "")]
    [Serializable]
    public class AfectedField
    {
        public AfectedField() { Settings = new Settings(); }
        public string Name { get; set; }
        public Settings Settings { get; set; }
    }

    [DataContract(Namespace = "")]
    [Serializable]
    public class RespondeConsulta
    {
          public List<AfectedField> AfectedFields { get; set; }
    }


}