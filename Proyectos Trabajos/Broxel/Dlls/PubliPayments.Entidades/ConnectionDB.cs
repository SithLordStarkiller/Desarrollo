using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class ConnectionDB
    {
        private static readonly ConnectionDB Instance = new ConnectionDB();
        private static readonly Dictionary<string, string> Connections = new Dictionary<string, string>();
        private int _timeOut = 60;

        /// <summary>
        /// Establece el tiempo de ejecución en segundos máximo para la ejecución de los commands 
        /// </summary>
        public int TimeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; } 
        }


        private ConnectionDB()
        {
        }

        public static void EstalecerConnectionString(string dataBase, string connectionString)
        {
            if (!Connections.ContainsKey(dataBase))
                Connections.Add(dataBase, connectionString);
        }

        public SqlConnection IniciaConexion(string dataBase)
        {
            var cnn = new SqlConnection(Connections[dataBase]);
            cnn.Open();
            return cnn;
        }

        public static ConnectionDB Instancia
        {
            get { return Instance; }
        }

        public void CierraConexion(SqlConnection cnn)
        {
            cnn.Close();
        }

        /// <summary>
        /// Ejecuta el stored procedure indicado en la base indicada
        /// </summary>
        /// <param name="dataBase">Nombre de la base de datos a la que se quiere conectar</param>
        /// <param name="storedProcedure">Nombre del stored procedure que se quiere ejecutar</param>
        /// <param name="parametros">Arreglo de parámetros</param>
        /// <returns>Regresa un dataset con el resultado del SP</returns>
        public DataSet EjecutarDataSet(string dataBase, string storedProcedure, SqlParameter[] parametros)
        {
            var instancia = Instancia;
            SqlConnection cnn = null;
            DataSet ds;
            try
            {
                cnn = instancia.IniciaConexion(dataBase);
                var sc = new SqlCommand(storedProcedure, cnn)
                {
                    CommandTimeout = _timeOut,
                    CommandType = CommandType.StoredProcedure
                };
                if (parametros != null)
                    sc.Parameters.AddRange(parametros);
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
                instancia.CierraConexion(cnn);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ConnectionDB", "EjecutarDataSet - Error:" + ex.Message);
                throw;
            }
            finally
            {
                if (cnn != null)
                    instancia.CierraConexion(cnn);
            }

            return ds;
        }

        /// <summary>
        /// Ejecuta un Query en la base de datos indicada
        /// </summary>
        /// <param name="dataBase">Nombre de la base de datos a la que se quiere conectar</param>
        /// <param name="sql">Query que se quiere ejecutar</param>
        /// <returns>Regresa un dataset con el resultado del query</returns>
        public DataSet EjecutarDataSet(string dataBase, string sql)
        {
            var instancia = Instancia;
            SqlConnection cnn = null;
            DataSet ds; 
            try
            {
                cnn = instancia.IniciaConexion(dataBase);
                var sc = new SqlCommand(sql, cnn)
                {
                    CommandType = CommandType.Text,
                    CommandTimeout = _timeOut
                };
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
                instancia.CierraConexion(cnn);
            }
            catch (Exception)
            {
                if (cnn != null)
                    CierraConexion(cnn);
                throw;
            }
            
            return ds;
        }

        /// <summary>
        /// Ejecuta una query en la base de datos y regresa el primer campo de la primer columna
        /// </summary>
        /// <param name="dataBase">Nombre de la base de datos a la que se quiere conectar</param>
        /// <param name="storedProcedure">Procedimiento a ejecutar</param>
        /// <param name="parametros">Opcional - Arreglo de parámetros del SP a ejecutar</param>
        /// <returns>Regresa el valor de la primera columna del primer registro si existe</returns>
        public string EjecutarEscalar(string dataBase, string storedProcedure, SqlParameter[] parametros = null)
        {
            var instancia = Instancia;
            SqlConnection cnn = null;
            string resultado;

            try
            {
                cnn = instancia.IniciaConexion(dataBase);
                var sc = new SqlCommand(storedProcedure, cnn);
                if (parametros != null)
                {
                    sc.Parameters.AddRange(parametros);
                    sc.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    sc.CommandType = CommandType.Text;
                }
                sc.CommandTimeout = _timeOut;
                resultado = Convert.ToString(sc.ExecuteScalar());
                instancia.CierraConexion(cnn);
            }
            catch (Exception)
            {
                if (cnn != null)
                    cnn.Close();
                throw;

            }

            return resultado;
        }

        /// <summary>
        /// Ejecuta un escalar sobre una conexión ya existente
        /// </summary>
        /// <param name="cnn">Conexión ya abierta</param>
        /// <param name="query">Query que se quiere ejecutar</param>
        /// <returns>Regresa el valor de la primera columna del primer registro si existe</returns>
        public string EjecutarEscalar(SqlConnection cnn, string query)
        {
            var sc = new SqlCommand(query, cnn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = _timeOut
            };
            var resultado = Convert.ToString(sc.ExecuteScalar());
            return resultado;
        }

        /// <summary>
        /// Ejecuta un comando de tipo NonQuery
        /// </summary>
        /// <param name="dataBase">Nombre de la base de datos a la que se quiere conectar</param>
        /// <param name="query">Comando que se quiere ejecutar</param>
        /// <param name="parametros">Opcional - Arreglo de parámetros</param>
        /// <returns>Regresa un entero con la cantidad de registros afectados</returns>
        public int EjecutarNonQuery(string dataBase, string query, SqlParameter[] parametros = null)
        {
            var instancia = Instancia;
            SqlConnection cnn = null;
            int resultado;
            try
            {
                cnn = instancia.IniciaConexion(dataBase);
                var sc = new SqlCommand(query, cnn);
                if (parametros != null)
                {
                    sc.Parameters.AddRange(parametros);
                    sc.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    sc.CommandType = CommandType.Text;
                }
                sc.CommandTimeout = _timeOut;
                resultado = sc.ExecuteNonQuery();
                CierraConexion(cnn);
            }
            catch (Exception)
            {
                if (cnn != null)
                    CierraConexion(cnn);
                throw;
            }

            return resultado;
        }
    }

}
