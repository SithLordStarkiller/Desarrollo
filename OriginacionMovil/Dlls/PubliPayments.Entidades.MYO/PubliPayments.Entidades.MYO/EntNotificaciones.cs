using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades.MYO
{
   public  class EntNotificaciones
    {
      public enum CatalogNotificationContexts
       {
           Generico=1,
           Solicitud
       }
       /// <summary>
       /// Insertar nuevo registro para notificacion
       /// </summary>
       /// <param name="idusuario">idUsuario del sistema de FLOCK</param>
       /// <param name="titulo">Titulo de la notificacion</param>
       /// <param name="descripcion">Texto que contendra el cuerpo de la notificacion</param>
        /// <param name="tipo">El tipo de importancia que tiene la notificacion Normal, Urgente</param>
        /// <param name="contexto">La forma en la cual se tratara la notificacion 1:Generico, 2:Solicitud </param>
       /// <returns></returns>
       public DataSet InsertarNotificaciones(int idusuario, string titulo, string descripcion, string tipo, int contexto)
       {
           SqlConnection cnn = null;
           try
           {
               ConnectionDB.EstalecerConnectionString("FlockMYO", ConfigurationManager.ConnectionStrings["FlockMYO"].ConnectionString);
               cnn = ConnectionDB.Instancia.IniciaConexion("FlockMYO");
               var sc = new SqlCommand("InsertarNotificaciones", cnn);
               var parametros = new SqlParameter[5];
               parametros[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 32) { Value = titulo };
               parametros[1] = new SqlParameter("@Description", SqlDbType.NVarChar, 256) { Value = descripcion };
               parametros[2] = new SqlParameter("@NotificationType", SqlDbType.NVarChar, 16) { Value = tipo };
               parametros[3] = new SqlParameter("@ContextId", SqlDbType.Int) { Value = contexto };
               parametros[4] = new SqlParameter("@UserId", SqlDbType.Int) { Value = idusuario };
               sc.Parameters.AddRange(parametros);
               sc.CommandType = CommandType.StoredProcedure;
               var sda = new SqlDataAdapter(sc);
               var ds = new DataSet();
               sda.Fill(ds);
               ConnectionDB.Instancia.CierraConexion(cnn);
               return ds;
           }
           catch (Exception ex)
           {
               if (cnn!=null)
               {
                   ConnectionDB.Instancia.CierraConexion(cnn);
               }
               Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntNotificaciones", "InsertarNotificaciones: " + ex.Message);
           }
           finally
           {
               ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
           }
           return null;
       }
    }
}


