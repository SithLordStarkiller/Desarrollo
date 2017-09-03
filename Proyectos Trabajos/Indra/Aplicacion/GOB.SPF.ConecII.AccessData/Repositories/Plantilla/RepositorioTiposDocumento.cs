using GOB.SPF.ConecII.Entities.Plantilla;
using GOB.SPF.ConecII.Entities.Attributes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositorioTiposDocumento : Repositorio<TiposDocumento>
    {
    
        #region Propiedades
        public TiposDocumento TiposDocumentoActual { get; set;}
        #endregion
        
        
        
        #region IRepositorioTiposDocumento implementation
        
                
                public virtual List<TiposDocumento> GetPagedElementsStored(TiposDocumentoDtoBuscar busqueda)
                {
                        TiposDocumento entity = new TiposDocumento();
                        
                        IPageBase paged = busqueda;
                        List<SqlParameter> parametros = new List<SqlParameter>();
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdTipoDocumento), busqueda.IdTipoDocumentoBuscar.Equals(null) ? 0 : busqueda.IdTipoDocumentoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Nombre), string.IsNullOrEmpty(busqueda.NombreBuscar) ? string.Empty : busqueda.NombreBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Descripcion), string.IsNullOrEmpty(busqueda.DescripcionBuscar) ? string.Empty : busqueda.DescripcionBuscar));
                        if(busqueda.ActivoBuscar.Equals(null))
                            parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Activo), DBNull.Value));
                        else
                            parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Activo), busqueda.ActivoBuscar));
                        parametros.Add(new SqlParameter("@p_max", paged.PageCurrent * paged.PageSize));
                        parametros.Add(new SqlParameter("@p_min", (paged.PageCurrent - 1) * paged.PageSize + 1));
                        
                        
                        List<string> relatedEntity = new List<string>();
                        List<string> relatedEntityFields = new List<string>();
                        
                        int totalItems = 0;
                        List<TiposDocumento> entityList = CurrentUoW.ExecuteReaderStoredMany<TiposDocumento>("TiposDocumentoPaged", parametros, ref totalItems, "@p_totalItems", relatedEntity, relatedEntityFields);
                        
                        paged.PageTotalItems = totalItems;
                        return entityList;
                        
                }
                
        #endregion
        
    }
}