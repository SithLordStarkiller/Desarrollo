using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proEntidad;
using proSeguridad;
using System.Data.Common;

namespace proDatos
{
    public class clsDatUsuario
    {
        public static int consultarPermisoAdmin(ref clsEntSesion objSesion)
        {

            sicerEntities context_Entities;
            int errorDB = 0;
            string strConnection = clsDatConexion.getConnectionString_Entity(objSesion);
            using (context_Entities = new sicerEntities(strConnection))
            {
                try
                {
                    var result = context_Entities.spuConsultarUsuario(objSesion.Usuario.UsuLogin);
                    if (result != null)
                    {

                        objSesion.Usuario.Perfil = new clsEntPerfil();
                        foreach (spuConsultarUsuario_Result cust in result)
                        {
                            objSesion.Usuario.IdUsuario = Convert.ToInt16(cust.idUsuario);
                            objSesion.Usuario.IdEmpleado = cust.idempleado;
                            objSesion.Usuario.UsuNombre = cust.nombre.Trim().ToUpper();
                            objSesion.Usuario.Perfil.IdRol = cust.idRol;
                            objSesion.Usuario.Perfil.PerDescripcion = cust.rol.Trim().ToUpper();
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorDB = ((System.Data.SqlClient.SqlException)(ex.InnerException)).Number;
                }

                return errorDB;
            }
        }

        public static int consultarPermisoUsuarioExamen(clsEntSesion objSesion,ref int idRegistro)
        {

            sicerEntities context_Entities;
            int errorDB = 0;
            string strConnection = clsDatConexion.getConnectionString_Entity();
            string strContrasena = clsSegRijndaelSimple.Decrypt(objSesion.Usuario.UsuContrasenia);
            using (context_Entities = new sicerEntities(strConnection))
            {
                try
                {
                    System.Data.Objects.ObjectParameter registro = new System.Data.Objects.ObjectParameter("idRegistro", typeof(int));
                    System.Data.Objects.ObjectParameter resultado = new System.Data.Objects.ObjectParameter("resultado", typeof(byte));


                    context_Entities.spuValidarDatosEvaluacion(
                                            objSesion.Usuario.UsuLogin, 
                                            clsSegRijndaelSimple.Decrypt(objSesion.Usuario.UsuContrasenia),
                                            registro,
                                            resultado);

                    idRegistro = (int)registro.Value;
                    errorDB = Convert.ToInt32(resultado.Value);
                    
                }
                catch (Exception ex)
                {
                    errorDB = ((System.Data.SqlClient.SqlException)(ex.InnerException)).Number;
                }

                return errorDB;
            }
        }

        /*
        public static void consultaDatosUsuarioExamen(int idRegistro,ref clsEntSesion objSession)
        {
            sicerEntities context_Entities;
            int errorDB = 0;
            string strConnection = clsDatConexion.getConnectionString_Entity();
            using (context_Entities = new sicerEntities(strConnection))
            {
                try
                {
                    System.Data.Objects.ObjectParameter registro = new System.Data.Objects.ObjectParameter("idRegistro", typeof(int));
                    System.Data.Objects.ObjectParameter resultado = new System.Data.Objects.ObjectParameter("resultado", typeof(byte));

                    context_Entities.spuValidarDatosEvaluacion(
                                            objSesion.Usuario.UsuLogin,
                                            clsSegRijndaelSimple.Decrypt(objSesion.Usuario.UsuContrasenia),
                                            registro,
                                            resultado);

                    idRegistro = (int)registro.Value;
                    errorDB = Convert.ToInt32(resultado.Value);

                }
                catch (Exception ex)
                {
                    errorDB = ((System.Data.SqlClient.SqlException)(ex.InnerException)).Number;
                }
        
            }

        }*/
    }
}
