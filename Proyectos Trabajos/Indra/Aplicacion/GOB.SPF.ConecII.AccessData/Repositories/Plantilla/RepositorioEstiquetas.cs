using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Attributes;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositorioEstiquetas : Repositorio<Estiquetas>
    {
    
        #region Propiedades
        public Estiquetas EstiquetasActual { get; set;}
        #endregion
        
        #region IRepositorioEstiquetas implementation
                
                public virtual List<Estiquetas> GetPagedElementsStored(EstiquetasDtoBuscar busqueda)
                {
                        Estiquetas entity = new Estiquetas();
                        TiposDocumento tiposDocumento = new TiposDocumento();
                        
                        IPageBase paged = busqueda;
                        List<SqlParameter> parametros = new List<SqlParameter>();
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdEtiqueta), busqueda.IdEtiquetaBuscar.Equals(null) ? 0 : busqueda.IdEtiquetaBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdTipoDocumento), busqueda.IdTipoDocumentoBuscar.Equals(null) ? 0 : busqueda.IdTipoDocumentoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Etiqueta), string.IsNullOrEmpty(busqueda.EtiquetaBuscar) ? string.Empty : busqueda.EtiquetaBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Contenido), string.IsNullOrEmpty(busqueda.ContenidoBuscar) ? string.Empty : busqueda.ContenidoBuscar));
                        if(busqueda.NegritaBuscar.Equals(null))
                            parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Negrita), DBNull.Value));
                        else
                            parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Negrita), busqueda.NegritaBuscar));
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
                        List<Estiquetas> entityList = CurrentUoW.ExecuteReaderStoredMany<Estiquetas>("EstiquetasPaged", parametros, ref totalItems, "@p_totalItems", relatedEntity, relatedEntityFields);
                        
                        paged.PageTotalItems = totalItems;
                        return entityList;
                        
                }
                
        #endregion
        
    }
}