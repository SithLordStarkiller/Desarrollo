using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PubliPayments.Entidades
{
   public  class EntUsuariosServicios
    {
       /// <summary>
       /// Obtiene la informacion que se tenga de un servicio que se encuentre  registrado en la tabla UsuariosServicios
       /// </summary>
       /// <param name="tipo"> Tipo del servicio que se quiere consultar</param>
       /// <param name="idAplicacion">Id de la aplicacion a utilizar</param>
       /// <returns>Lista compuesta de los elementos que se tengan con ese servicio para la aplicacion que se este corriendo</returns>
       /// 1._Envio de email
       /// 2._Envio de mensajes SMS
       /// 3._Conexion a BD
       public List<UsuariosServiciosModel> ObtenerUsuariosServicios(int tipo, int idAplicacion=0)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@Tipo", SqlDbType.Int) { Value = tipo };
            parametros[1] = new SqlParameter("@idAplicacion", SqlDbType.Int) { Value = idAplicacion };
            var ds = instancia.EjecutarDataSet("SqlDefault", "ObtenerUsuariosServicios", parametros);
            List<UsuariosServiciosModel> lista=null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lista = (from DataRow rows in ds.Tables[0].Rows select new UsuariosServiciosModel { Usuario = Convert.ToString(rows["Usuario"]), Password = Convert.ToString(rows["Password"]), Orden = Convert.ToInt32(rows["Orden"].ToString()), TipoElemento = Convert.ToString(rows["TipoElemento"]), Nombre = Convert.ToString(rows["Nombre"]), Servidor = Convert.ToString(rows["Servidor"]), Extra = Convert.ToString(rows["Extra"]), Descripcion = Convert.ToString(rows["Descripcion"]) }).ToList();
                }
            return lista;
        }
    }
}
