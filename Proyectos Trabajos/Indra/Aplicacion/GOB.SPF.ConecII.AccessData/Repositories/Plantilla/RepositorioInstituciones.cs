using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Attributes;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositorioInstituciones : Repositorio<Instituciones>
    {
    
        #region Propiedades
        public Instituciones InstitucionesActual { get; set;}
        #endregion
        
        #region IRepositorioInstituciones implementation
                
                public virtual List<Instituciones> GetPagedElementsStored(InstitucionesDtoBuscar busqueda)
                {
                        Instituciones entity = new Instituciones();
                        TiposDocumento tiposDocumento = new TiposDocumento();
                        
                        IPageBase paged = busqueda;
                        List<SqlParameter> parametros = new List<SqlParameter>();
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdInstitucion), busqueda.IdInstitucionBuscar.Equals(null) ? 0 : busqueda.IdInstitucionBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdTipoDocumento), busqueda.IdTipoDocumentoBuscar.Equals(null) ? 0 : busqueda.IdTipoDocumentoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.Nombre), string.IsNullOrEmpty(busqueda.NombreBuscar) ? string.Empty : busqueda.NombreBuscar));
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
                        List<Instituciones> entityList = CurrentUoW.ExecuteReaderStoredMany<Instituciones>("InstitucionesPaged", parametros, ref totalItems, "@p_totalItems", relatedEntity, relatedEntityFields);
                        
                        paged.PageTotalItems = totalItems;
                        return entityList;
                        
                }
                
        #endregion
        
    }
}