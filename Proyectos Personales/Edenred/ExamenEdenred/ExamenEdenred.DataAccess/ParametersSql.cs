namespace ExamenEdenred.DataAccess
{
    using System.Web;

    public class ParametersSql
    {
        static readonly LsApplicationState Appstate = new LsApplicationState(HttpContext.Current.Application);
        public static string StrConDbLsWebApp = Appstate.SqlConnString;
    }
}
