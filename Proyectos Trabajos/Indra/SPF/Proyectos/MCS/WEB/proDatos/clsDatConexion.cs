using System;
using System.Data.Common;
using SICOGUA.Seguridad;

namespace SICOGUA.Datos
{
    public class clsDatConexion
    {
        private string strProvider;//Proveedor de mi BD
        public DbProviderFactory dbProvider;

        public clsDatConexion()
        {
            strProvider = clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["provider"]);
            dbProvider = DbProviderFactories.GetFactory(strProvider);
        }

        public DbConnection getConexion()
        {
            string strConnectionString = clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["base"]);
 
            

            strProvider = clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["provider"]);

            dbProvider = DbProviderFactories.GetFactory(strProvider);
            DbConnection nuevaConexion = dbProvider.CreateConnection();
            nuevaConexion.ConnectionString = strConnectionString;

            try
            {
                nuevaConexion.Open();
                return nuevaConexion;
            }
            catch (DbException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DbConnection getConexionREA()
        {
            string strConnectionString = clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["bd"]);



            strProvider = clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["provider"]);

            dbProvider = DbProviderFactories.GetFactory(strProvider);
            DbConnection nuevaConexion = dbProvider.CreateConnection();
            nuevaConexion.ConnectionString = strConnectionString;

            try
            {
                nuevaConexion.Open();
                return nuevaConexion;
            }
            catch (DbException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DbConnection getConexion(clsEntSesion objSesion)
        {
            string strConnectionString = clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["base"]);
            //string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["base"];

            
           strConnectionString += "UID=" + objSesion.usuario.UsuLogin + ";PWD=" + clsSegRijndaelSimple.Decrypt(objSesion.usuario.UsuContrasenia);
            strProvider = clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["provider"]);

            dbProvider = DbProviderFactories.GetFactory(strProvider);
            DbConnection nuevaConexion = dbProvider.CreateConnection();
            nuevaConexion.ConnectionString = strConnectionString;

            try
            {
                nuevaConexion.Open();
                return nuevaConexion;
            }
            catch (DbException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public DbConnection getConexionREA(clsEntSesion objSesion)
        {
            string strConnectionString = clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["bd"]);
            //string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["base"];

            //String KK = clsSegRijndaelSimple.Encrypt("Data Source=10.237.78.77\\BdDesarrollo;Initial Catalog=sicogua30092011;Pooling=False;");

            strConnectionString += "UID=" + objSesion.usuario.UsuLogin + ";PWD=" + clsSegRijndaelSimple.Decrypt(objSesion.usuario.UsuContrasenia);
            strProvider = clsSegRijndaelSimple.Decrypt(System.Configuration.ConfigurationManager.AppSettings["provider"]);

            dbProvider = DbProviderFactories.GetFactory(strProvider);
            DbConnection nuevaConexion = dbProvider.CreateConnection();
            nuevaConexion.ConnectionString = strConnectionString;

            try
            {
                nuevaConexion.Open();
                return nuevaConexion;
            }
            catch (DbException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public static bool cerrarTransaccion(DbConnection objConexion)
        {
            try 
            {
                objConexion.Dispose();
                objConexion.Close();                
                return true;
            }
            catch (DbException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
