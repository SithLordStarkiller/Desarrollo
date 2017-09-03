using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Attributes;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositorioContratantes : Repositorio<Contratantes>
    {
    
        #region Propiedades
        public Contratantes ContratantesActual { get; set;}
        #endregion
        
        #region IRepositorioContratantes implementation
        
                
                public virtual List<Contratantes> GetPagedElementsStored(ContratantesDtoBuscar busqueda)
                {
                        Contratantes entity = new Contratantes();
                        TiposDocumento tiposDocumento = new TiposDocumento();
                        
                        IPageBase paged = busqueda;
                        List<SqlParameter> parametros = new List<SqlParameter>();
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdContratante), busqueda.IdContratanteBuscar.Equals(null) ? 0 : busqueda.IdContratanteBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdTipoDocumento), busqueda.IdTipoDocumentoBuscar.Equals(null) ? 0 : busqueda.IdTipoDocumentoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Nombre), string.IsNullOrEmpty(busqueda.NombreBuscar) ? string.Empty : busqueda.NombreBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Cargo), string.IsNullOrEmpty(busqueda.CargoBuscar) ? string.Empty : busqueda.CargoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Domicilio), string.IsNullOrEmpty(busqueda.DomicilioBuscar) ? string.Empty : busqueda.DomicilioBuscar));
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
                        List<Contratantes> entityList = CurrentUoW.ExecuteReaderStoredMany<Contratantes>("ContratantesPaged", parametros, ref totalItems, "@p_totalItems", relatedEntity, relatedEntityFields);
                        
                        paged.PageTotalItems = totalItems;
                        return entityList;
                        
                }
                
        #endregion
        
    }
}