using System.Collections.Generic;
using System.Linq;
using ExamenCsi.Entities;

namespace ExamenCsi.DataAccess
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class SqlController
    {
        public int InsertarUsuario(UsUsuario usuario)
        {
            var id = -1;

            try
            {
                var constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

                using (var con = new SqlConnection(constr))
                {
                    using (var cmd = new SqlCommand("Usp_UsUsuarioInsertar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@IdTipoUsuario", SqlDbType.Int).Value = usuario.IdTipoUsuario;
                        cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario.Usuario;
                        cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar).Value = usuario.Contrasena;

                        con.Open();
                        var o = cmd.ExecuteScalar();
                        if (o != null)
                        {
                            id = Convert.ToInt32(o.ToString());
                        }
                        con.Close();
                        return id;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<UsUsuario> UsuariosObtenerTodos()
        {
            try
            {
                var constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

                using (var con = new SqlConnection(constr))
                {
                    using (var cmd = new SqlCommand("Usp_UsUsuarioObtenerTodos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();

                        var result = cmd.ExecuteReader();
                        var dt = new DataTable();

                        dt.Load(result);
                        
                        con.Close();

                        var lista = ConvertDataTable<UsUsuario>(dt);

                        return lista;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            return (from DataRow row in dt.Rows select GetItem<T>(row)).ToList();
        }

        private static T GetItem<T>(DataRow dr)
        {
            var temp = typeof(T);
            var obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (var pro in temp.GetProperties().Where(pro => pro.Name == column.ColumnName))
                {
                    pro.SetValue(obj, dr[column.ColumnName], null);
                }
            }
            return obj;
        }
    }
}
