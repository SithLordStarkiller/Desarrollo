using GOB.SPF.ConecII.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GOB.SPF.Conec.Services.Filters
{
    internal class RequestTrackerFilter : ActionFilterAttribute
    {
        private DateTime initAt;
        private DateTime endsAt;
        private string request;
        private string clientIp;
        private string actionName;
        private string controllerName;
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            try
            {
                actionName = actionContext.ActionDescriptor.ActionName;
                controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                initAt = DateTime.UtcNow;
                request = await actionContext.Request.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

        }

        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            try
            {
                var response = await actionExecutedContext.Response.Content.ReadAsStringAsync();
                endsAt = DateTime.UtcNow;
                var task = Task.Run(() => { LogRequest(1, $"{controllerName}/{actionName}", request, response, initAt, endsAt); });
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

        }

        private void LogRequest(int userId, string url, string request, string response, DateTime initDate, DateTime endDate)
        {
            try
            {
                ConecII.Business.BitacoraBusiness bitacoraBusiness = new ConecII.Business.BitacoraBusiness();
                var bitacora = new Bitacora()
                {
                    IdUsuario = userId,
                    Request = request,
                    Response = response,
                    Path = url,
                    FechaRegistro = initDate,
                    Activo = true,
                    IP = "",
                    MAC = ""
                };
                bitacoraBusiness.InsertarBitacora(bitacora);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}