using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proSeguridad;
using proEntidad;

namespace proDatos
{
    public class clsDatSesion
    {
        public static int iniciarSesion(clsEntSesion objSesion)
        {
            sicerEntities context_Entities;

            string strConnection = clsDatConexion.getConnectionString_Entity(objSesion);
            using (context_Entities = new sicerEntities(strConnection))
            {

                var result = context_Entities.spuIniciarSesion(objSesion.SessionId, Convert.ToInt32(objSesion.Usuario.IdUsuario), objSesion.Ip, Convert.ToInt32(objSesion.Intentos), Convert.ToInt32(objSesion.Estatus));
                int estatus = 0;
                foreach (spuIniciarSesion_Result cust in result)
                {
                    estatus = 1;
                    Convert.ToInt32(cust.valido);
                }
                return estatus;
            }
            
        }
        public static bool finalizarSesion(clsEntSesion objSesion)
        {
            sicerEntities context_Entities;

            string strConnection = clsDatConexion.getConnectionString_Entity(objSesion);
            using (context_Entities = new sicerEntities(strConnection))
            {
                var result = context_Entities.spuFinalizarSesion(objSesion.SessionId, objSesion.Usuario.IdUsuario.ToString(), Convert.ToInt32(objSesion.Estatus));
                return true;
            }
        }

        public static void iniciarSesionExamen(int idRegistro)
        {
            sicerEntities context_Entities;
            int? reg = idRegistro;
            string strConnection = clsDatConexion.getConnectionString_Entity();
            using (context_Entities = new sicerEntities(strConnection))
            {
                System.Data.Objects.ObjectParameter idSesionEvaluacion = new System.Data.Objects.ObjectParameter("idSesionEvaluacion", typeof(int));
                System.Data.Objects.ObjectParameter inserto = new System.Data.Objects.ObjectParameter("inserto", typeof(bool));

                idSesionEvaluacion.Value = 0;

                context_Entities.spuInsertarSesionEvaluacion(idSesionEvaluacion, reg, null, null, inserto);

            }

        }

    }
}
