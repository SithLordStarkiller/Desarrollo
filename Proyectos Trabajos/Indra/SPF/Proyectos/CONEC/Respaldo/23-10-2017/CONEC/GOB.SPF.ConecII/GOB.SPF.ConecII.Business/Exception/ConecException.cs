using System;

namespace GOB.SPF.ConecII.Business
{
    public class ConecException: Exception
    {
        public ConecException():base()
        {

        }

        public ConecException(string message) : base(message) { }
    }
}
