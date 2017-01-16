using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;


namespace PubliPayments.Entidades
{
    public class EntRespuestas
    {
        public List<EntMarcadorGps> ObtenerRespuestasMarcadores(int usuario, DateTime fecha, int tipo, string tipoFormulario)
        {
            var marcadores = new List<EntMarcadorGps>();

            var ds = ObtenerRespuestasGps(usuario, fecha, tipo, tipoFormulario);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var valor = row["34"].ToString();
                if (valor.StartsWith("Lat:", StringComparison.Ordinal))
                {
                    var marcador = new EntMarcadorGps
                    {
                        Id = Convert.ToInt32(row["idOrden"]),
                        FechaInicial = row["InitialDate"].ToString(),
                        FechaFinal = row["FinalDate"].ToString(),
                        FechaRecepcion = Convert.ToDateTime(row["FechaRecepcion"]).ToString("yyyyMMdd HH:mm:ss"),
                        Referencia = row["num_Cred"].ToString()
                    };
                    var posLong = valor.IndexOf("Lon:", StringComparison.Ordinal);
                    var posSat = valor.IndexOf("Sat:", StringComparison.Ordinal);
                    var latitud = valor.Substring(4, posLong - 4);
                    var longitud = valor.Substring(posLong + 4, posSat - posLong - 4);
                    marcador.Latitud = Convert.ToDouble(latitud, CultureInfo.InvariantCulture);
                    marcador.Longitud = Convert.ToDouble(longitud, CultureInfo.InvariantCulture);

                    marcadores.Add(marcador);
                }

                //var fecha1Dia = fecha.AddDays(1);
                //var posiciones = from res in contex.Respuestas
                //                 group res by new { res.idOrden, res.idCampo, res.Valor }
                //                     into r
                //                     join o in contex.Ordenes on r.Key.idOrden equals o.idOrden
                //                     join u in contex.Usuario on o.idUsuario equals u.idUsuario
                //                     where
                //                         r.Key.idCampo == 34 && o.idUsuario == usuario && o.FechaRecepcion >= fecha &&
                //                         o.FechaRecepcion < fecha1Dia
                //                     orderby o.FechaRecepcion
                //                     select new
                //                     {
                //                         r.Key.Valor,
                //                         r.Key.idOrden,
                //                         o.num_Cred,
                //                         u.Usuario1,
                //                         o.FechaRecepcion
                //                     }

                //                 ;



                //foreach (var r in posiciones)
                //{
                //    var valor = r.Valor;
                //    if (valor.StartsWith("Lat:", StringComparison.Ordinal))
                //    {
                //        var marcador = new EntMarcadorGps { Id = r.idOrden, Nombre = r.Usuario1 + " " + r.num_Cred };
                //        var posLong = valor.IndexOf("Lon:", StringComparison.Ordinal);
                //        var posSat = valor.IndexOf("Sat:", StringComparison.Ordinal);
                //        var latitud = valor.Substring(4, posLong - 4);
                //        var longitud = valor.Substring(posLong + 4, posSat - posLong - 4);
                //        marcador.Latitud = Convert.ToDouble(latitud, CultureInfo.InvariantCulture);
                //        marcador.Longitud = Convert.ToDouble(longitud, CultureInfo.InvariantCulture);

                //        marcadores.Add(marcador);
                //    }
                //}
            }

            return marcadores;
        }

        private DataSet ObtenerRespuestasGps(int usuario, DateTime fecha, int tipo, string tipoFormulario)
        {
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand("ObtenerRespuestasGps", cnn);
            var parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("@idUsuario", SqlDbType.Int) { Value = usuario };
            parametros[1] = new SqlParameter("@fecha", SqlDbType.DateTime) { Value = fecha };
            parametros[2] = new SqlParameter("@tipo", SqlDbType.Int) { Value = tipo };
            parametros[3] = new SqlParameter("@tipoFormulario", SqlDbType.VarChar, 10) { Value = tipoFormulario };
            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public DataSet ObtenerValorCampoRespuesta(int idOrden, string nombreCampo)
        {
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand("ObtenerValorCampoRespuesta", cnn);
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@idOrden", SqlDbType.Int) { Value = idOrden };
            parametros[1] = new SqlParameter("@Nombre", SqlDbType.VarChar, 50) { Value = nombreCampo };

            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        /// <summary>
        /// Inserta una respuesta, inserta el campo si no existe y valida el formulario.
        /// Se tienen que manejar los errores fuera de este método
        /// </summary>
        /// <param name="idOrden">Orden a la que se le quiere insertar la respuesta</param>
        /// <param name="nombreCampo">Nombre del campo</param>
        /// <param name="valor">Valor del campo</param>
        /// <param name="idFormulario">Id del formulario que se va valida e insertar el campo, 
        /// si es -1 obtiene el formulario de una respuesta existente</param>
        public void InsertarRespuesta(int idOrden, string nombreCampo, string valor, int idFormulario = -1)
        {
            var parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("@idOrden", SqlDbType.Int) { Value = idOrden };
            parametros[1] = new SqlParameter("@NombreCampo", SqlDbType.NVarChar, 50) { Value = nombreCampo };
            parametros[2] = new SqlParameter("@Valor", SqlDbType.NVarChar, 350) { Value = valor };
            parametros[3] = new SqlParameter("@idFormulario", SqlDbType.Int) { Value = idFormulario };
            ConnectionDB.Instancia.EjecutarNonQuery("SqlDefault", "InsertarRespuesta",
                parametros);
        }

        /// <summary>
        /// Obtiene las respuestas de la orden 
        /// </summary>
        /// <param name="tipo"> Tipo de operacion a realizar</param>
        /// <param name="idOrden">id orden a buscar</param>
        /// <param name="reporte">indica si es para reporte</param>
        /// <param name="idUsuarioPadre">id usuario supervisor</param>
        /// <param name="restriccion">Bandera que indica si se necesita aplicar restriccion para la busqueda de la respuesta</param>
        /// <param name="conexionBd">Base de datos a conectarse</param>
        /// <returns>Registros que se tiene sobre la respuesta</returns>
        public DataSet ObtenerRespuestas(int tipo, string idOrden, int reporte, int idUsuarioPadre, bool restriccion, string conexionBd)
        {
            var parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("@tipo", SqlDbType.Int) { Value = tipo };
            parametros[1] = new SqlParameter("@idOrden", SqlDbType.NVarChar, 20) { Value = idOrden };
            parametros[2] = new SqlParameter("@reporte", SqlDbType.Int) { Value = reporte };
            parametros[3] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) { Value = idUsuarioPadre };
            parametros[4] = new SqlParameter("@restriccion", SqlDbType.Bit) { Value = restriccion };  
            
            return ConnectionDB.Instancia.EjecutarDataSet(conexionBd, "ObtenerRespuestas", parametros);

        }


        
    }
}
