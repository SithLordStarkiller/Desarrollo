namespace SunCorp.DataAccessSqlServer.EntitiesDataAccesss
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Entities;
    using Entities.Generic;
    using System.Collections.Generic;

    public class EntitiesAccess
    {
        #region UsUsuarios
        public UsUsuarios GetUsUsuarioTSql(UserSession session)
        {
            var executesqlstr = "SELECT * FROM UsUsuarios WHERE Usuario = '" + session.User + "' AND Contrasena = '" + session.Password + "'";
            // 
            var para = new[]
            {
                new SqlParameter("@user", session.User),
                new SqlParameter("@password", session.Password)
            };

            var obj = ControllerSqlServer.ExecuteDataTable(ParametersSql.StrConDbLsWebApp, CommandType.Text, executesqlstr, para);

            var resultado = new UsUsuarios();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new UsUsuarios
                             {
                                 IdTipoUsuario = (int)row["IdTipoUsuario"],
                                 Contrasena = (string)row["Contrasena"],
                                 Usuario = (string)row["Usuario"],
                                 IdUsuario = (int)row["IdUsuario"],
                             }).ToList().FirstOrDefault();
            }

            return resultado;
        }

        public UsUsuarios GetUsUsuario(UserSession user)
        {
            using (var aux = new Repositorio<UsUsuarios>())
            {
                return aux.Extraer(r => r.Usuario == user.User && r.Contrasena == user.Password);
            }
        }

        #endregion

        #region UsTipoUsuario

        public UsTipoUsuario GetTypeUser(UsUsuarios user)
        {
            using (var aux = new Repositorio<UsTipoUsuario>())
            {
                return aux.Extraer(r => r.IdTipoUsuario == user.IdTipoUsuario);
            }
        }

        #endregion

        #region UsZona

        public List<UsZona> GetListUsZona()
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return aux.TablaCompleta().Where(r => r.Borrado == false).ToList();
            }
        }

        public List<UsZona> GetListUsZonaPageList(int page, int numRows,ref int totalRows, bool includeDelete)
        {
            var parameters = new[]
            {
                new SqlParameter("@i_Page_Index", page),
                new SqlParameter("@i_Page_Count", numRows),
                new SqlParameter("@o_total_rows", totalRows) {Direction = ParameterDirection.Output},
                new SqlParameter("@includeDelete", includeDelete)
            };
            var obj = new DataTable();

            using (var con = new SqlConnection(ParametersSql.StrConDbLsWebApp))
            {
                using (var cmd = new SqlCommand("Usp_GetListUsZonaPageList", con))
                {

                    //foreach (var p in parameters)
                    //{
                    //    if (p != null)
                    //    {
                    //        cmd.Parameters.Add(p);
                    //    }
                    //}

                    foreach (var p in parameters.Where(p => p != null))
                    {
                        cmd.Parameters.Add(p);
                    }

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(obj);
                        totalRows = Convert.ToInt32(cmd.Parameters["@o_total_rows"].Value);
                    }
                }
            }


            //ControllerSqlServer.ExecuteDataTable(ParametersSql.StrConDbLsWebApp, CommandType.StoredProcedure,
            //"Usp_GetListUsZonaPageList", parameters);

            var resultado = (from DataRow row in obj.Rows
                select new UsZona
                {
                    IdZona = (int)row["IdZona"]
                }).ToList();

            return resultado;
        }

        public UsZona NewRegUsZona(UsZona zona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return aux.Agregar(zona);
            }
        }

        public bool UpdateRegUsZona(UsZona zona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return aux.Actualizar(zona);
            }
        }

        public bool DeleteRegUsZona(UsZona zona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return aux.Eliminar(zona);
            }
        }
        #endregion

        #region UsUsuarioPorZona

        public List<UsUsuarioPorZona> GetUsUsuarioPorZona(UsUsuarios usUsuario)
        {
            using (var aux = new Repositorio<UsUsuarioPorZona>())
            {
                return aux.Filtro(r => r.IdUsuarios == usUsuario.IdUsuario);
            }
        }

        public List<UsZona> GetListUsZonaUserLinq(List<int> listUserZona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                var listZonas = aux.TablaCompleta();

                return listZonas.Where(r => listUserZona.Any(x => x == r.IdZona)).ToList();
            }
        }

        #endregion

        #region SisTipoUsuarioPorMenu

        public List<SisTipoUsuarioPorMenu> GetListMenusForTypeUser(UsUsuarios user)
        {
            using (var aux = new Repositorio<SisTipoUsuarioPorMenu>())
            {
                return aux.Filtro(r => r.IdTipoUsuario == user.IdTipoUsuario);
            }
        }

        #endregion

        #region ProCatMarca

        public List<ProCatMarca> GetListProCatMarca()
        {
            using (var aux = new Repositorio<ProCatMarca>())
            {
                return aux.TablaCompleta().Where(r=> r.Borrado == false).ToList();
            }
        }

        public ProCatMarca NewRegProCatMarca(ProCatMarca reg)
        {
            using (var aux = new Repositorio<ProCatMarca>())
            {
                return aux.Agregar(reg);
            }
        }

        public bool UpdateRegProCatMarca(ProCatMarca reg)
        {
            using (var aux = new Repositorio<ProCatMarca>())
            {
                return aux.Actualizar(reg);
            }
        }

        public bool DeleteRegProCatMarca(ProCatMarca reg)
        {
            using (var aux = new Repositorio<ProCatMarca>())
            {
                return aux.Eliminar(reg);
            }
        }

        #endregion

        #region ProCatModelo

        public List<ProCatModelo> GetListProCatModelo()
        {
            using (var aux = new Repositorio<ProCatModelo>())
            {
                return aux.TablaCompleta().Where(r => r.Borrado == false).ToList();
            }
        }

        public ProCatModelo NewRegProCatModelo(ProCatModelo reg)
        {
            using (var aux = new Repositorio<ProCatModelo>())
            {
                return aux.Agregar(reg);
            }
        }

        public bool UpdateRegProCatModelo(ProCatModelo reg)
        {
            using (var aux = new Repositorio<ProCatModelo>())
            {
                return aux.Actualizar(reg);
            }
        }

        public bool DeleteRegProCatModelo(ProCatModelo reg)
        {
            using (var aux = new Repositorio<ProCatModelo>())
            {
                return aux.Eliminar(reg);
            }
        }

        #endregion

        #region ProDiviciones

        public List<ProDiviciones> GetListProCatDiviciones()
        {
            using (var aux = new Repositorio<ProDiviciones>())
            {
                return aux.TablaCompleta().Where(r => r.Borrado == false).ToList();
            }
        }

        public ProDiviciones NewRegProDiviciones(ProDiviciones reg)
        {
            using (var aux = new Repositorio<ProDiviciones>())
            {
                return aux.Agregar(reg);
            }
        }

        public bool UpdateRegProDiviciones(ProDiviciones reg)
        {
            using (var aux = new Repositorio<ProDiviciones>())
            {
                return aux.Actualizar(reg);
            }
        }

        public bool DeleteRegProDiviciones(ProDiviciones reg)
        {
            using (var aux = new Repositorio<ProDiviciones>())
            {
                return aux.Eliminar(reg);
            }
        }

        #endregion
    }
}
