using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.ControlUsuarios.LoginA
{
    public class LoginA
    {
        #region Propiedades de la clase

        public LoginA()
        {
        }

        #endregion

        #region Metodos de Insercion

        #endregion

        #region Metodos de Extraccion

        public US_USUARIOS LoginAdministradorLinq(string nombre, string contrasena)
        {
            US_USUARIOS usuario;

            using (var aux = new Repositorio<US_USUARIOS>())
            {
                usuario = aux.Extraer(r => r.USUARIO == nombre && r.CONTRASENA == contrasena);
            }

            return usuario;
        }

        public US_USUARIOS LoginAdministradoresTSql(string nombre, string contrasena)
        {
            const string executesqlstr = "SELECT TOP 1 * FROM US_USUARIOS WHERE USUARIO = @Usuario AND CONTRASENA = @Contrasena";
            var resultado = new US_USUARIOS();

            var para = new[] { 
                new SqlParameter("@Usuario",nombre),
                new SqlParameter("@Contrasena",contrasena)                    
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
                                 ID_HISTORIAL = Convert.IsDBNull(row["ID_HISTORIAL"]) ? null : (int?)row["ID_HISTORIAL"],
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
