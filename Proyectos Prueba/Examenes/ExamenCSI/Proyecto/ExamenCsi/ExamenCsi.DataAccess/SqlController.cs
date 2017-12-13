namespace ExamenCsi.DataAccess
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class SqlController
    {
        protected int Insertar(int idTipoUsuario, string usuario, string contrasena)
        {
            var id = -1;

            try
            {
                var constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

                using (var con = new SqlConnection(constr))
                {
                    using (var cmd = new SqlCommand("AddEmployeeReturnID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@IdTipoUsuario", SqlDbType.Int).Value = idTipoUsuario;
                        cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario;
                        cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar).Value = contrasena;

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
