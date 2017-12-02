using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using proSeguridad;

namespace proDatos
{
    public class clsDatConexion
    {
        public static string getConnectionString_Entity()
        {
            try
            {
                string newStringCon =
                "provider=" + clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["provider"])
                + clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["SICER_Entiti"]);// +"';";
                //newStringCon += "User ID=" + objSesion.Usuario.UsuLogin + ";Password=" + clsSegRijndaelSimple.Decrypt(objSesion.Usuario.UsuContrasenia) + "';";
                return newStringCon;
            }
            catch (Exception)
            {

            }
            return string.Empty;
        }

        public static string getConnectionString_Entity(clsEntSesion objSesion)
        {
            try
            {
                #region Configuración de conexión

                string newStringCon =
                "provider=" + clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["provider"])
                + ";metadata=res://*; provider connection string=';Data Source="
                + clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["db_Entiti"]) + ";Initial Catalog=" + clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["catalog_EntitiAdmin"])
                + ";Persist Security Info=True;MultipleActiveResultSets=True;";
                newStringCon += "User ID=" + objSesion.Usuario.UsuLogin + ";Password=" + clsSegRijndaelSimple.Decrypt(objSesion.Usuario.UsuContrasenia) + "';";
                #endregion

                #region Response
                return newStringCon;
                #endregion
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }
    }
}