using System.Web;
using System.Web.Mvc;

namespace Universidad.WebAdministrativa
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ctx = HttpContext.Current;

            if (ctx.Session["Sesion"] == null)
            {
                filterContext.Result = new RedirectResult("~/Index/Index");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}