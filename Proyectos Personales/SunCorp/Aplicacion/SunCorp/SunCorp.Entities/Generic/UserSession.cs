namespace SunCorp.Entities.Generic
{
    using System.Runtime.Serialization;


    [DataContract]
    public class UserSession
    {
        [DataMember]
        public string UrlServer;

        [DataMember]
        public object SessionSecurity;

        [DataMember]
        public string User;

        [DataMember]
        public string Password;

        [DataMember]
        public bool RemmemberSession;

        [DataMember]
        public bool RememberPassword;
    }
}
