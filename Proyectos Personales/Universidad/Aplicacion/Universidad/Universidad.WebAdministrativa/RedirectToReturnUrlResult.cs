namespace Universidad.WebAdministrativa
{
    using System;
    using System.Web.Mvc;

    public class RedirectToReturnUrlResult : ActionResult
    {
        private readonly Func<ActionResult> _funcIfNoReturnUrl;

        public RedirectToReturnUrlResult(Func<ActionResult> funcIfNoReturnUrl)
        {
            if (funcIfNoReturnUrl == null) throw new ArgumentNullException("funcIfNoReturnUrl");
            _funcIfNoReturnUrl = funcIfNoReturnUrl;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            string returnUrl;

            if (TryGetReturnUrl(context, out returnUrl))
            {
                new RedirectResult(returnUrl).ExecuteResult(context);
            }
            else
            {
                _funcIfNoReturnUrl().ExecuteResult(context);
            }
        }

        private bool TryGetReturnUrl(ControllerContext context, out string returnUrl)
        {
            try
            {
                var queryString = context.HttpContext.Request.QueryString;
                returnUrl = queryString["ReturnUrl"];
                return !string.IsNullOrEmpty(returnUrl);
            }

            catch (Exception)
            {
                returnUrl = null;
                return false;
            }
        }
    }
}