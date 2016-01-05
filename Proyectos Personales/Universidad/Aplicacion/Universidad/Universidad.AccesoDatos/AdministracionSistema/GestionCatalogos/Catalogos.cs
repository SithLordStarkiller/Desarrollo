using System;

namespace Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos
{
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Entidades.Catalogos;
    using Entidades;

    public class Catalogos
    {
        public List<CatalogosSistema> ObtenCatalogosTSql()
        {
            var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.StoredProcedure,
                "Usp_ObtenCatalogosSistema", null);

            var resultado = new List<CatalogosSistema>();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new CatalogosSistema
                             {
                                 NombreTabla = (string)row["TableName"]
                             }).ToList();
            }

            return resultado;
        }

        public List<AUL_CAT_TIPO_AULA> ObtenListaAUL_CAT_TIPO_AULATSql()
        {
            var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.StoredProcedure,
               "Usp_ObtenListaAUL_CAT_TIPO_AULA", null);

            var resultado = new List<AUL_CAT_TIPO_AULA>();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new AUL_CAT_TIPO_AULA
                             {
                                 IDTIPOAULA = (short)row["IDTIPOAULA"],
                                 TIPOAULA = (string)row["TIPOAULA"],
                                 DESCRIPCION = (string)row["DESCRIPCION"]
                             }).ToList();
            }

            return resultado;
        }

        public bool ActualizaRegistroAUL_CAT_TIPO_AULATSql(AUL_CAT_TIPO_AULA registro)
        {
            try
            {
                var para = new[]
                {
                    new SqlParameter("@IdTipoAula", registro.IDTIPOAULA),
                    new SqlParameter("@TipoAula", registro.TIPOAULA),
                    new SqlParameter("@Descripcion", registro.DESCRIPCION)
                };

                var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.StoredProcedure,
                    "Usp_ActualizaRegistroAUL_CAT_TIPO_AULA", para);

                return obj != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AUL_CAT_TIPO_AULA NuevoRegistroAUL_CAT_TIPO_AULATSql(AUL_CAT_TIPO_AULA registro)
        {
            try
            {
                var para = new[]
                {
                    new SqlParameter("@IdTipoAula", registro.IDTIPOAULA),
                    new SqlParameter("@TipoAula", registro.TIPOAULA),
                    new SqlParameter("@Descripcion", registro.DESCRIPCION)
                };

                var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.StoredProcedure,
                    "Usp_InsertaAUL_CAT_TIPO_AULA", para);

                return registro;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool EliminaRegistroAUL_CAT_TIPO_AULATSql(int idTipoAula)
        {
            try
            {
                var para = new[]
                {
                    new SqlParameter("@IdTipoAula", idTipoAula)
                };

                var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.StoredProcedure,
                    "Usp_EliminaRegistroAUL_CAT_TIPO_AULA", para);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
