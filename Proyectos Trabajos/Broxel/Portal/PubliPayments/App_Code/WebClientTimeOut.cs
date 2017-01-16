using System;
using System.Net;

namespace PubliPayments
{
    public class WebClientTimeOut : WebClient
    {
        public int TimeOut { get; set; }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            if (w != null)
                w.Timeout = TimeOut;
            return w;
        }
    }
}