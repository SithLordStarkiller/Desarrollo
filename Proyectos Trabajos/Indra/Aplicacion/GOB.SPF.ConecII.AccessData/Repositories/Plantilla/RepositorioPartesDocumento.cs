using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Attributes;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositorioPartesDocumento : Repositorio<PartesDocumento>
    {
    
        #region Propiedades
        public PartesDocumento PartesDocumentoActual { get; set;}
        #endregion
        
        #region IRepositorioPartesDocumento implementation
                
                public virtual List<PartesDocumento> GetPagedElementsStored(PartesDocumentoDtoBuscar busqueda)
                {
                        PartesDocumento entity = new PartesDocumento();
                        TiposDocumento tiposDocumento = new TiposDocumento();
                        
                        IPageBase paged = busqueda;
                        List<SqlParameter> parametros = new List<SqlParameter>();
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdParteDocumento), busqueda.IdParteDocumentoBuscar.Equals(null) ? 0 : busqueda.IdParteDocumentoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdTipoDocumento), busqueda.IdTipoDocumentoBuscar.Equals(null) ? 0 : busqueda.IdTipoDocumentoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.RutaLogo), string.IsNullOrEmpty(busqueda.RutaLogoBuscar) ? string.Empty : busqueda.RutaLogoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Folio), string.IsNullOrEmpty(busqueda.FolioBuscar) ? string.Empty : busqueda.FolioBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Asunto), string.IsNullOrEmpty(busqueda.AsuntoBuscar) ? string.Empty : busqueda.AsuntoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.LugarFecha), string.IsNullOrEmpty(busqueda.LugarFechaBuscar) ? string.Empty : busqueda.LugarFechaBuscar));
                        if(busqueda.PaginadoBuscar.Equals(null))
                            parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Paginado), DBNull.Value));
                        else
                            parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Paginado), busqueda.PaginadoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Ccp), string.IsNullOrEmpty(busqueda.CcpBuscar) ? string.Empty : busqueda.CcpBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Puesto), string.IsNullOrEmpty(busqueda.PuestoBuscar) ? string.Empty : busqueda.PuestoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Nombre), string.IsNullOrEmpty(busqueda.NombreBuscar) ? string.Empty : busqueda.NombreBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Direccion), string.IsNullOrEmpty(busqueda.DireccionBuscar) ? string.Empty : busqueda.DireccionBuscar));
                        if(busqueda.ActivoBuscar.Equals(null))
                            parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Activo), DBNull.Value));
                        else
                            parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Activo), busqueda.ActivoBuscar));
                        parametros.Add(new SqlParameter("@p_max", paged.PageCurrent * paged.PageSize));
                        parametros.Add(new SqlParameter("@p_min", (paged.PageCurrent - 1) * paged.PageSize + 1));
                        
                        
                        List<string> relatedEntity = new List<string>();
                        relatedEntity.Add(entity.GetMemberName(x => x.TiposDocumento));
                        List<string> relatedEntityFields = tiposDocumento.GetRelatedField(x => new { x.IdTipoDocumento, x.Nombre});
                        
                        int totalItems = 0;
                        List<PartesDocumento> entityList = CurrentUoW.ExecuteReaderStoredMany<PartesDocumento>("PartesDocumentoPaged", parametros, ref totalItems, "@p_totalItems", relatedEntity, relatedEntityFields);
                        
                        paged.PageTotalItems = totalItems;
                        return entityList;
                        
                }
                
        #endregion
        
    }
}