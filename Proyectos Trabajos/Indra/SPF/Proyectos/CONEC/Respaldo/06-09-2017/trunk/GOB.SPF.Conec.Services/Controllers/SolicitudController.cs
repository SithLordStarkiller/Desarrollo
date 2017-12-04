namespace GOB.SPF.Conec.Services.Controllers
{
    using ConecII.Entities.DTO;
    using ConecII.Entities.Request;
    using GOB.SPF.Conec.Services.Models;
    using GOB.SPF.Conec.Services.Validators;
    using ConecII.Business;
    using GOB.SPF.ConecII.Entities;
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using business = ConecII.Business;

    [RoutePrefix("api/Solicitud")]
    public class SolicitudController : ApiController
    {
        [HttpPost]
        [Route("Cliente/ObtenerTodos")]
        public async Task<IHttpActionResult> ClienteObtenerTodos(RequestCliente entity)
        {
            ResultPage<Cliente> result = new ResultPage<Cliente>();
            try
            {
                business.ClienteBusiness business = new business.ClienteBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
                result.Success = true;
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Cliente/ObtenerTodosPorRazonSocial")]
        public async Task<IHttpActionResult> ObtenerTodosPorRazonSocial(RequestCliente entity)
        {
            ResultPage<Cliente> result = new ResultPage<Cliente>();
            try
            {
                business.ClienteBusiness business = new business.ClienteBusiness();
                result.List.AddRange(business.ObtenerPorRazonSocial(entity.Item.RazonSocial));
                result.Page.CurrentPage = 0;
                result.Page.Pages = 0;
                result.Success = true;
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Cliente/ObtenerTodosPorNombreCorto")]
        public async Task<IHttpActionResult> ObtenerTodosPorNombreCorto(RequestCliente entity)
        {
            ResultPage<Cliente> result = new ResultPage<Cliente>();
            try
            {
                business.ClienteBusiness business = new business.ClienteBusiness();
                result.List.AddRange(business.ObtenerPorNombreCorto(entity.Item.NombreCorto));
                result.Page.CurrentPage = 0;
                result.Page.Pages = 0;
                result.Success = true;
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Cliente/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> ClienteObtenerPorCriterio(RequestCliente entity)
        {
            ResultPage<Cliente> result = new ResultPage<Cliente>();
            try
            {
                business.ClienteBusiness business = new business.ClienteBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
                result.Success = true;
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);
        }
    }
}
