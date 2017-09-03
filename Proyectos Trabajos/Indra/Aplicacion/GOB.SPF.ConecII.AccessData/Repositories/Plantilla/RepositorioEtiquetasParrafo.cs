using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Attributes;
using GOB.SPF.ConecII.Entities.Plantilla;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GOB.SPF.ConecII.AccessData.Repositories.Plantilla
{
    public partial class RepositorioEtiquetasParrafo : Repositorio<EtiquetasParrafo>
    {
    
        #region Propiedades
        public EtiquetasParrafo EtiquetasParrafoActual { get; set;}
        #endregion

        #region IRepositorioEtiquetasParrafo implementation
                
                public virtual List<EtiquetasParrafo> GetPagedElementsStored(EtiquetasParrafoDtoBuscar busqueda)
                {
                        EtiquetasParrafo entity = new EtiquetasParrafo();
                        Parrafos parrafos = new Parrafos();
                        
                        IPageBase paged = busqueda;
                        List<SqlParameter> parametros = new List<SqlParameter>();
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdEtiquetaParrafo), busqueda.IdEtiquetaParrafoBuscar.Equals(null) ? 0 : busqueda.IdEtiquetaParrafoBuscar));
                        parametros.Add(new SqlParameter("@p_" + entity.GetMemberName(x => x.IdParrafo), busqueda.IdParrafoBuscar.Equals(null) ? 0 : busqueda.IdParrafoBuscar));
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
                        relatedEntity.Add(entity.GetMemberName(x => x.Parrafos));
                        List<string> relatedEntityFields = parrafos.GetRelatedField(x => new { x.IdParrafo, x.Nombre});
                        
                        int totalItems = 0;
                        List<EtiquetasParrafo> entityList = CurrentUoW.ExecuteReaderStoredMany<EtiquetasParrafo>("EtiquetasParrafoPaged", parametros, ref totalItems, "@p_totalItems", relatedEntity, relatedEntityFields);
                        
                        paged.PageTotalItems = totalItems;
                        return entityList;
                        
                }


                public virtual List<EtiquetasParrafo> GetPorIdTipoDocumento(int IdTipoDocumento)
                {
                    EtiquetasParrafo entity = new EtiquetasParrafo();
                    Parrafos parrafos = new Parrafos();

                    
                    List<SqlParameter> parametros = new List<SqlParameter>();
                    parametros.Add(new SqlParameter("@p_IdTipoDocumento" , IdTipoDocumento));
                    

                    List<string> relatedEntity = new List<string>();
                    relatedEntity.Add(entity.GetMemberName(x => x.Parrafos));
                    List<string> relatedEntityFields = parrafos.GetRelatedField(x => new { x.IdParrafo, x.Nombre });

                    int totalItems = 0;
                    List<EtiquetasParrafo> entityList = CurrentUoW.ExecuteReaderStoredMany<EtiquetasParrafo>("EtiquetasParrafoGetPorIdTipoDocumento", parametros, ref totalItems, string.Empty, relatedEntity, relatedEntityFields);

                    
                    return entityList;

                }
                
        #endregion
        
    }
}