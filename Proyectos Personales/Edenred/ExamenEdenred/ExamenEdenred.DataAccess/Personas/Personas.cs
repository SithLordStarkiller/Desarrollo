namespace ExamenEdenred.DataAccess.Personas
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class Personas
    {
        public bool EliminaPersona(int idPersona)
        {
            try
            {
                var para = new[]
                {
                    new SqlParameter("@IdPersona", idPersona)
                };

                var a = ControllerSqlServer.ExecuteNonQuery(ParametersSql.StrConDbLsWebApp, CommandType.StoredProcedure, "Usp_EliminarPersona", para);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
