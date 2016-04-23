namespace Universidad.AccesoDatos.ControlUsuarios.LoginA
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Reflection;
    using Entidades;
    using Helpers;

    public class LoginA
    {
        #region Metodos de Insercion

        #endregion

        #region Metodos de Extraccion

        public US_USUARIOS LoginAdministradorLinq(string nombre, string contrasena)
        {
            var Log = new LogManager();

            try
            {
                US_USUARIOS usuario;

                var textoEncriptado = new Encriptacion().EncriptarTexto(contrasena);

                using (var aux = new Repositorio<US_USUARIOS>())
                {
                    usuario = aux.Extraer(r => r.USUARIO == nombre && r.CONTRASENA == textoEncriptado);
                }
                var log = "Login de " + usuario.ID_USUARIO + " ";

                Log.RegistraInformacion(GetType().Name, MethodBase.GetCurrentMethod().Name, "Servidor Interno", log);

                return usuario;
            }
            catch (Exception e)
            {
                Log.RegistraExcepcion(e, GetType().Name, MethodBase.GetCurrentMethod().Name, "Servidor Interno");
                return null;
            }
        }

        public US_USUARIOS LoginAdministradoresTSql(string nombre, string contrasena)
        {
            const string executesqlstr = "SELECT TOP 1 * FROM US_USUARIOS WHERE USUARIO = @Usuario AND CONTRASENA = @Contrasena";
            var resultado = new US_USUARIOS();

            var textoEncriptado = new Encriptacion().EncriptarTexto(contrasena);

            var para = new[] { 
                new SqlParameter("@Usuario",nombre),
                new SqlParameter("@Contrasena",textoEncriptado)                    
            };
            var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);


            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new US_USUARIOS
                             {
                                 CONTRASENA = (string)row["CONTRASENA"],
                                 USUARIO = (string)row["Usuario"],
                                 ID_ESTATUS_USUARIOS = Convert.IsDBNull(row["ID_ESTATUS_USUARIOS"]) ? null : (int?)row["ID_ESTATUS_USUARIOS"],
                                 ID_USUARIO = (int)row["ID_USUARIO"],
                                 ID_NIVEL_USUARIO = Convert.IsDBNull(row["ID_NIVEL_USUARIO"]) ? null : (int?)row["ID_NIVEL_USUARIO"],
                                 ID_TIPO_USUARIO = Convert.IsDBNull(row["ID_TIPO_USUARIO"]) ? null : (int?)row["ID_TIPO_USUARIO"]

                             }).ToList().FirstOrDefault();
            }
            return resultado;
        }


        public PER_PERSONAS ObtenPersonaLinq(US_USUARIOS usuario)
        {
            using (var aux = new Repositorio<PER_PERSONAS>())
            {
                return aux.Extraer(per => per.ID_USUARIO == usuario.ID_USUARIO);
            }
        }

        #endregion
    }
}
