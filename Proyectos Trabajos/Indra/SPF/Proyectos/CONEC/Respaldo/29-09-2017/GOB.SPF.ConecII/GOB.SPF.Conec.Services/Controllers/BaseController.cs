using GOB.SPF.ConecII.Business;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.DTO;
using GOB.SPF.ConecII.Entities.Request;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GOB.SPF.Conec.Services.Controllers
{
    public class BaseController<TRequest, T> : ApiController
    where TRequest : class, IRequestBase<T>, new()
    where T : TEntity, new()
    {
        public Business<T> mBusiness;

        internal ResultPage<T> ObtenerTodos(TRequest entity)
        {
            ResultPage<T> result = new ResultPage<T>();

            try
            {
                result.List.AddRange(mBusiness.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = mBusiness.Pages;
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

            return result;
        }

        internal ResultPage<T> ObtenerPorCriterio(TRequest entity)
        {
            ResultPage<T> result = new ResultPage<T>();
            try
            {
                result.List.AddRange(mBusiness.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = mBusiness.Pages;
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

            return result;
        }

        internal ResultPage<T> ObtenerPorId(TRequest entity)
        {
            ResultPage<T> result = new ResultPage<T>();
            try
            {
                result.Entity = mBusiness.ObtenerPorId(entity.Item);
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
            return result;
        }

        internal ResultPage<T> Guardar(TRequest entity)
        {
            ResultPage<T> result = new ResultPage<T>();
            try
            {
                result.Success = mBusiness.Guardar(entity.Item);
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                result.Errors.Add(new Error { Code = e.ErrorCode, Message = ResourceExceptionMessage.MensajeErrorAplicacion });
            }
            catch (ConecException ex)
            {
                result.Errors.Add(new Error { Code = 1000, Message = ex.Message });
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                result.Errors.Add(new Error { Code = System.Runtime.InteropServices.Marshal.GetExceptionCode(), Message = ResourceExceptionMessage.MensajeErrorAplicacion });
            }
            return result;
        }

        internal ResultPage<T> CambiarEstatus(TRequest entity)
        {
            ResultPage<T> result = new ResultPage<T>();
            try
            {
                result.Success = mBusiness.CambiarEstatus(entity.Item);
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                result.Errors.Add(new Error { Code = e.ErrorCode, Message = ResourceExceptionMessage.MensajeErrorAplicacion });
            }
            catch (ConecException ex)
            {
                result.Errors.Add(new Error { Code = 1000, Message = ex.Message });
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                result.Errors.Add(new Error { Code = System.Runtime.InteropServices.Marshal.GetExceptionCode(), Message = ResourceExceptionMessage.MensajeErrorAplicacion });
            }
            return result;
        }

        internal ResultPage<DropDto> ObtenerDropDownList(TRequest entity)
        {
            ResultPage<DropDto> result = new ResultPage<DropDto>();
            try
            {
                result.List.AddRange(mBusiness.ObtenerDropDownList());
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

            return result;
        }

        internal ResultPage<DropDto> ObtenerDropDownListCriterio(TRequest entity)
        {
            ResultPage<DropDto> result = new ResultPage<DropDto>();
            try
            {
                result.List.AddRange(mBusiness.ObtenerDropDownListCriterio(entity.Item));
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

            return result;
        }

    }
}