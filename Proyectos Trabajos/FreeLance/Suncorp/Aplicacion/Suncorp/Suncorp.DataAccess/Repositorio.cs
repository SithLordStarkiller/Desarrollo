namespace Suncorp.DataAccess
{
    using Helpers;
    using Models;
    using System.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        private readonly SuncorpEntities _contexto;

        private DbSet<TEntity> EntitySet => _contexto.Set<TEntity>();

        public Repositorio()
        {
            try
            {
                _contexto = new SuncorpEntities();
                _contexto.Configuration.ProxyCreationEnabled = false;
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al insertar en entityFramework", "", ex, _contexto.ToString()));
                throw ex;
            }
}

        public async Task<TEntity> Insertar(TEntity agregar)
        {
            TEntity resultado;

            try
            {
                EntitySet.Add(agregar);
                await _contexto.SaveChangesAsync();
                resultado = agregar;
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al insertar en entityFramework", "", ex, agregar.GetType().ToString()));
                throw ex;
            }

            return resultado;
        }

        public async Task<bool> Eliminar(TEntity eliminar)
        {
            bool resultado;

            try
            {

                EntitySet.Attach(eliminar);
                EntitySet.Remove(eliminar);
                resultado = await _contexto.SaveChangesAsync() > 0;

            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                      () =>
                          _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                              Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                              MethodBase.GetCurrentMethod().Name, "Error al eliminar en entityFramework", "", ex, eliminar.GetType().ToString()));
                throw ex;
            }

            return resultado;
        }

        public async Task<bool> Actualizar(TEntity actualizar)
        {
            bool resultado;

            try
            {
                EntitySet.Attach(actualizar);
                _contexto.Entry(actualizar).State = EntityState.Modified;
                resultado = await _contexto.SaveChangesAsync() > 0;

            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                      () =>
                          _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                              Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                              MethodBase.GetCurrentMethod().Name, "Error al actualizar en entityFramework", "", ex, actualizar.GetType().ToString()));
                throw ex;
            }

            return resultado;
        }

        public async Task<TEntity> Consulta(Expression<Func<TEntity, bool>> criterio)
        {
            TEntity resultado;

            try
            {
                resultado = await EntitySet.FirstOrDefaultAsync(criterio);
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                       () =>
                          _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                              Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                              MethodBase.GetCurrentMethod().Name, "Error al consultar en entityFramework", "", ex, criterio.ToString()));
                throw ex;
            }

            return resultado;
        }

        public async Task<List<TEntity>> ConsultaLista(Expression<Func<TEntity, bool>> listaCriterio)
        {
            List<TEntity> resultado;

            try
            {
                resultado = await EntitySet.Where(listaCriterio).ToListAsync();
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                      () =>
                          _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                              Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                              MethodBase.GetCurrentMethod().Name, "Error al consultar lista en entityFramework", "", ex, listaCriterio.ToString()));
                throw ex;
            }

            return resultado;
        }

        public async Task<List<TEntity>> ObtenerTabla()
        {
            List<TEntity> resultado;

            try
            {
                resultado = await EntitySet.ToListAsync();
            }
            catch (Exception ex)
            {
                await Task.Factory.StartNew(
                      () =>
                          _logLogger.EscribeLog(Logger.TipoLog.ErrorCritico,
                              Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                              MethodBase.GetCurrentMethod().Name, "Error al consultar lista en entityFramework", "", ex, ""));
                throw ex;
            }

            return resultado;
        }
        public void Dispose()
        {
            _contexto?.Dispose();
        }
    }
}
