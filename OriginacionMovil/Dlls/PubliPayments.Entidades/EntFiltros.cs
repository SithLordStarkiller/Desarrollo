using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubliPayments.Entidades
{
   public class EntFiltros
    {
       /// <summary>
       /// Inserta el filtro que se esta aplicando para este usuario y elemento que se esta utilizando
       /// </summary>
       /// <param name="idUsuario">usuario del sistema</param>
       /// <param name="filtro">Nombre del filtro que se esta aplicando</param>
       /// <param name="valor">Nuevo valor que se tiene del filtro</param>
       public void InsertaFiltro( int idUsuario, string filtro,string valor)
       {
           var instancia = ConnectionDB.Instancia;
           var parametros = new SqlParameter[3];
           parametros[0] = new SqlParameter("@idUsuario", SqlDbType.Int) { Value = idUsuario };
           parametros[1] = new SqlParameter("@Filtro", SqlDbType.NVarChar,50) { Value = filtro };
           parametros[2] = new SqlParameter("@Valor", SqlDbType.VarChar, 100) { Value = valor };

           instancia.EjecutarDataSet("SqlDefault", "InsertaFiltroAplicacion", parametros);
       }

       /// <summary>
       /// Obtiene el valor de un filtro que se tiene relacionado para el usuario
       /// </summary>
       /// <param name="idUsuario">usuario del sistema</param>
       /// <param name="filtro">Nombre del filtro que se esta consultando</param>
       /// <returns>Valor que se tiene guardado para ese filtro</returns>
       public string ObtenerFiltros( int idUsuario, string filtro)
       {
           var instancia = ConnectionDB.Instancia;
           var parametros = new SqlParameter[2];
           parametros[0] = new SqlParameter("@idUsuario", SqlDbType.Int) { Value = idUsuario };
           parametros[1] = new SqlParameter("@Filtro", SqlDbType.NVarChar,50) { Value = filtro };

           var ds= instancia.EjecutarDataSet("SqlDefault", "ObtenerFiltrosAplicacion", parametros);
           if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
           {
               return Convert.ToString(ds.Tables[0].Rows[0]["Valor"]);
           }
           return null;
       }

       
    }
}
