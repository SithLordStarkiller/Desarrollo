using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PubliPayments.Entidades
{
    /// <summary>
    /// Procesa lo relacionado a la Tabla de Catalogo General
    /// </summary>
    public class EntCatalogoGeneral
    {
        /// <summary>
        /// Inserta o actualiza un campo en el catalogo general
        /// </summary>
        /// <param name="llave">Llave que se utiliza para diferenciar el registro</param>
        /// <param name="valor">Valor que tendra el registro</param>
        /// <param name="descripcion">Descripcion de lo que se refiere el registro</param>
        /// <returns>id del registro, en caso de que sea un registro nuevo traera el nuevo id</returns>
        public string InsUpdCatalogoGeneral(string llave,string valor,string descripcion=null)
        {

            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@Llave", SqlDbType.VarChar,500) { Value = llave };
            parametros[1] = new SqlParameter("@Valor", SqlDbType.VarChar,500) { Value = valor };
            parametros[2] = new SqlParameter("@Descripcion", SqlDbType.VarChar, 2000) { Value = descripcion };
            return instancia.EjecutarEscalar("SqlDefault", "InsUpdCatalogoGeneral", parametros);
        }
        /// <summary>
        /// Obtiene la informacion del registro que se tiene en el catalogo general
        /// </summary>
        /// <param name="id">id que se utiliza para diferenciar el registro</param>
        /// <param name="llave">Llave que se utiliza para diferenciar el registro</param>
        /// <returns>DataSet que contendra todas las columnas que tiene el registro en la tabla CatalogGeneral</returns>
        public DataSet ObtenerDatosCatalogoGeneral(int id=0,string llave="")
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            parametros[1] = new SqlParameter("@Llave", SqlDbType.VarChar,500) { Value = llave };
            
            return instancia.EjecutarDataSet("SqlDefault", "ObtenerDatosCatalogoGeneral", parametros);
        }
        
    }
}
