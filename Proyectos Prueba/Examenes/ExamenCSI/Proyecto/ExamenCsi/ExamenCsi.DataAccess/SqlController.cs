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
    }
}
