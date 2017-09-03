using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Attributes;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositorioParrafos : Repositorio<Parrafos>
    {
    
        #region Propiedades
        public Parrafos ParrafosActual { get; set;}
        #endregion
                
        #region IRepositorioParrafos implementation
                
                public virtual List<Parrafos> GetPagedElementsStored(ParrafosDtoBuscar busqueda)
                {
                        Parrafos entity = new Parrafos();
                        TiposDocumento tiposDocumento = new TiposDocumento();
                        
                        IPageBase paged = busqueda;
                        List<SqlParameter> parametros = new List<SqlParameter>();
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdParrafo), busqueda.IdParrafoBuscar.Equals(null) ? 0 : busqueda.IdParrafoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdTipoDocumento), busqueda.IdTipoDocumentoBuscar.Equals(null) ? 0 : busqueda.IdTipoDocumentoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Nombre), string.IsNullOrEmpty(busqueda.NombreBuscar) ? string.Empty : busqueda.NombreBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Texto), string.IsNullOrEmpty(busqueda.TextoBuscar) ? string.Empty : busqueda.TextoBuscar));
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
                        List<Parrafos> entityList = CurrentUoW.ExecuteReaderStoredMany<Parrafos>("ParrafosPaged", parametros, ref totalItems, "@p_totalItems", relatedEntity, relatedEntityFields);
                        
                        paged.PageTotalItems = totalItems;
                        return entityList;
                        
                }
                
        #endregion
        
    }
}