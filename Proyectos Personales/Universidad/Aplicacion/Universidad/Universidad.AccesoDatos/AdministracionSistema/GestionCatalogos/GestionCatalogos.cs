namespace Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos
{
    using System.Collections.Generic;
    using Entidades.Catalogos;
    using System.Data;
    using System.Linq;

    public class GestionCatalogos
    {
        //private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public List<ListasGenerica> ObtenTablasCatalogosTsql()
        {
            const string executesqlstr = "SELECT name AS object_name ,SCHEMA_NAME(schema_id) AS schema_name,type_desc,create_date,modify_date FROM sys.objects WHERE type_desc = 'USER_TABLE' and name like '%CAT%'";
            
            var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.Text, executesqlstr, null);

            List<ListasGenerica> lista;

            if (obj != null)
            {
                lista = (from DataRow row in obj.Rows select new ListasGenerica
                    {
                        Nombre = (string)row["object_name"]
                    }).ToList();

                var counter = 1;

                foreach (var item in lista)
                {
                    item.Id = counter;
                    counter++;
                }
            }
            else
            {
                return null;
            }

            return lista;
        }

        
    }
}
