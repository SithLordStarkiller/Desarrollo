using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using IdSecure;
using MySql.Data.MySqlClient;
using wsBroxel.App_Code.Online;
using wsBroxel.App_Code.RequestResponses;
using wsBroxel.App_Code.SolicitudBL.Model;
using wsBroxel.App_Code.TokenBL;
using wsBroxel.App_Code.VCBL.Models;
using wsBroxel.App_Code;


namespace wsBroxel.App_Code.SolicitudBL
{
    public class MySqlDataAccess
    {
        private MySqlConnection _conn;

		#region ComisionesRedDePagos
		/// <summary>
		/// Inserta en la tabla PscDatosAd encryptado
		/// </summary>
		/// <param name="id">id de PagoServicioCtrl</param>
		/// <param name="js">cadena encryptada </param>
		/// <returns>nombre</returns>
		public bool instertPscDatosAd(string js, int id)
		{
			var val = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[] 
                {
                    new MySqlParameter("@id", MySqlDbType.Int32) { Value = id },
                    new MySqlParameter("@js", MySqlDbType.VarChar) { Value = js }
                };

                

                var sql = "insert into PscDatosAd(IdPsc,strAd) values(@id,@js)";

				var cmd = new MySqlCommand
				{
					CommandText = sql,
					Connection = _conn,
					CommandType = CommandType.Text
				};

                cmd.Parameters.AddRange(parameters);

				var insert = cmd.ExecuteNonQuery();
				if (insert > 0)
					val = true;
			}
			catch (MySqlException)
			{
				val = false;
			}
			finally
			{
				_conn.Close();
				_conn = null;
			}

			return val;
		}
		/// <summary>
		/// Obtiene la clave de cliente filtrando por el número de cuenta.
		/// </summary>
		/// <param name="id">numero PscDatosAd id.</param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public string ObtenerTarjetaPscDatosAd(int idPsc)
		{
			string res = "";
			try
			{
				_conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
				_conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idPsc", MySqlDbType.Int32) { Value = idPsc }
                };

                var cmd = new MySqlCommand
				{
					CommandText = " select IdPsc,strAd from  PscDatosAd  where IdPsc = @idPsc;",
					Connection = _conn,
					CommandType = CommandType.Text
				};

                cmd.Parameters.Add(parameters);

				var dr = cmd.ExecuteReader();
				var dataTable = new DataTable();
				dataTable.Load(dr);
				if (dataTable.Rows.Count > 0)
				{
					DataRow row = dataTable.Rows[0];
					// TipoObtencionCanal = 2 : se aplica query, si es 1 solo el nombre  
					res = row["strAd"].ToString();
					if (string.IsNullOrEmpty(res))
						throw new Exception("No se encontro IdPsc");
				}
			}
			catch (Exception)
			{
				res = null;
			}
			finally
			{
				_conn.Close();
				_conn = null;
			}
			return res;
		}
		/// <summary>
		/// obtiene los nombres del comercio deacuerdo al id del comercio.
		/// </summary>
		/// <param name="idComercio">id del comercio a obtener</param>
		/// <returns>nombre</returns>
		public string ObtenerNombreComercio(int idComercio)
        {
            string res = "";
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idComercio", MySqlDbType.Int32) { Value = idComercio }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = " select  idComercio, Comercio from Comercio where idComercio = @idComercio;",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    // TipoObtencionCanal = 2 : se aplica query, si es 1 solo el nombre  
                    res = row["Comercio"].ToString();

                    if (string.IsNullOrEmpty(res))
                        throw new Exception("No se encontro Comercio");
                }
            }
            catch (Exception)
            {
                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        /// <summary>
        /// Obtiene la clave de cliente filtrando por el número de cuenta.
        /// </summary>
        /// <param name="numCuenta">numero de cuenta.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string ObtenerClaveCliente(string numCuenta)
        {
            string res = "";
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = " select clave_cliente from  maquila  where num_cuenta = @numCuenta;",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    // TipoObtencionCanal = 2 : se aplica query, si es 1 solo el nombre  
                    res = row["clave_cliente"].ToString();

                    if (string.IsNullOrEmpty(res))
                        throw new Exception("No se encontro Clave_Cliente");
                }
            }
            catch (Exception)
            {
                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }
        /// <summary>
        ///  SE obtiene el canal atravez del query que se obtiene de la base de datos.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="folio"></param>
        /// <returns></returns>
        private string ObtenerCanalPorQuery(string query, long folio)
        {
            string res = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                var cmd = new MySqlCommand
                {
                    CommandText = query.Replace("##Folio##", folio.ToString(CultureInfo.InvariantCulture)),
                    Connection = _conn,
                    CommandType = CommandType.Text
                };
                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    res = row["Canal"].ToString();
                    if (string.IsNullOrEmpty(res))
                        throw new Exception("Sin Canal");
                }
            }
            catch (Exception)
            {
                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        /// <summary>
        ///  Obtener el Canal del pago.
        /// </summary>
        /// <param name="idUsuario">idUsuario del web service.</param>
        /// <param name="folio">folio de la dispersión.</param>
        /// <returns></returns>
        public string ObtenerCanal(string idUsuario, long folio)
        {
            string res = "";
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idUsuario", MySqlDbType.VarChar) { Value = idUsuario }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select  idUser, Canal, TipoObtencionCanal from usuarios_dispersiones_ws where  idUser = @idUsuario",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    // TipoObtencionCanal = 2 : se aplica query, si es 1 solo el nombre  
                    res = (int)row["TipoObtencionCanal"] == 2 ? ObtenerCanalPorQuery(row["Canal"].ToString(), folio) : row["Canal"].ToString();

                    if (string.IsNullOrEmpty(res))
                        throw new Exception("No se encontró canal");
                }
            }
            catch (Exception)
            {
                res = null;
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
                _conn = null;
            }
            return res;
        }

        /// <summary>
        /// Cargo de las comisiones que se aplican cuando se hace uso de la plataforma de red de pagos.
        /// </summary>
        /// <returns>Lista de comisiones de todos los canales disponibles para broxel.</returns>
        public List<ComisionRedPagos> ObtenerComisionesRedPagos()
        {
            List<ComisionRedPagos> comisiones = new List<ComisionRedPagos>();
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                var sql = "select  Canal, descripcion,comision from  ComisionesCargoRedPagos where estatus = 1 order by canal, comision desc";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };
                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var comision = new ComisionRedPagos
                        {
                            Canal = row["Canal"].ToString(),
                            Leyenda = row["descripcion"].ToString(),
                            Monto = (Decimal)row["comision"]
                        };

                        comisiones.Add(comision);
                    }

                }
            }
            catch (Exception ex)
            {
                comisiones = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return comisiones;
        }

        /// <summary>
        /// Lista de comisiones que se aplican por el uso de red de pagos especificando el canal.
        /// </summary>
        /// <param name="idUsuario">idUsuario del web service <example>wsRedDePagos</example></param>
        /// <param name="canal">tienda donde se realiza el pago  <example>OXXO</example></param>
        /// <param name="claveCliente">Clave de cliente asociado a la cuenta</param>
        /// <param name="producto">Producto asociado a la cuenta</param>
        /// <returns>lista de comisiones del número de cuenta.</returns>
        public List<DetalleComisionAsignacion> ObtenerComisionCanal(string idUsuario, string canal, string claveCliente, string producto)
        {
            var comisiones = new List<DetalleComisionAsignacion>();
            try
            {
                var parameters = new MySqlParameter[]
               {
                    new MySqlParameter("@idUsuario", MySqlDbType.VarChar) { Value = idUsuario },
                    new MySqlParameter("@canal", MySqlDbType.VarChar) { Value = canal },
                    new MySqlParameter("@claveCliente", MySqlDbType.VarChar) { Value = claveCliente },
                    new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto }
               };

                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                var sql = @"SELECT crp.Id, u.consecutivo, u.idUser, crp.Canal, crp.Comision, crp.descripcion,  crp.IdComercio, crp.AplicaComo 
                            FROM ComisionesCargoRedPagos crp 
                            INNER JOIN usuarios_dispersiones_ws u  ON u.consecutivo = crp.ConsecutivoUsuario
                            INNER JOIN DetalleComisionesRedDePagos dcr on crp.id = dcr.idComisionCargo
                            WHERE crp.Estatus = 1 AND u.idUser = @idUsuario and crp.Canal = @canal " +
                           "AND @claveCliente like concat('%',dcr.claveCliente, '%') and dcr.producto = @producto";

                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    comisiones = new List<DetalleComisionAsignacion>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var comision = new DetalleComisionAsignacion
                        {
                            Id = (int)row["Id"],
                            idComercio = (int)(row["IdComercio"]),
                            NombreComercio = row["Canal"].ToString(),
                            Valor = (decimal)row["Comision"],
                            TipoAplica = row["AplicaComo"].ToString()
                        };
                        comisiones.Add(comision);
                    }
                }
            }
            catch (Exception ex)
            {
                comisiones = new List<DetalleComisionAsignacion>();
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return comisiones;
        }
        public List<CargosDisposiciones> ObtenerCargosDisposiciones(string numCuenta, DateTime fechaInicio, DateTime fechaFin)
        {
            var res = new List<CargosDisposiciones>();

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
               {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta },
                    new MySqlParameter("@fechaInicio", MySqlDbType.Date) { Value = fechaInicio.ToString("yyyy-MM-dd") },
                    new MySqlParameter("@fechaFin", MySqlDbType.Date) { Value = fechaFin.ToString("yyyy-MM-dd") }
               };

                var query = @"select numcuenta, idDisposicion, numautorizacion, monto from CargosDisposicionesEfectivo where NumCuenta = @numCuenta and date(fechaHoraCreacion) between @fechaInicio and @fechaFin;";

                var cmd = new MySqlCommand
                {
                    CommandText = query,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);

                if (dataTable.Rows.Count > 0)
                {
                    res = (from DataRow row in dataTable.Rows
                        select new CargosDisposiciones
                        {
                            NumCuenta = row["numcuenta"].ToString(), IdDisposicion = Convert.ToInt32(row["idDisposicion"].ToString()), NumAutorizacion = row["numautorizacion"].ToString(), Monto = Convert.ToDecimal(row["monto"].ToString())
                        }).ToList();
                }

            }

            catch (Exception ex)
            {
                res = new List<CargosDisposiciones>();
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }
        public List<TransferenciasDeTerceros> ObtenerTransferenciasDeTerceros(string numCuenta, DateTime fechaInicio, DateTime fechaFin)
        {
            var trasferenciasDeTerceros = new List<TransferenciasDeTerceros>();

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
              {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta },
                    new MySqlParameter("@fechaInicio", MySqlDbType.Date) { Value = fechaInicio.ToString("yyyy-MM-dd") },
                    new MySqlParameter("@fechaFin", MySqlDbType.Date) { Value = fechaFin.ToString("yyyy-MM-dd") }
              };

                //                var query = @"select a.fechaHoraCreacion as FechaCreacion,b.codigoAutorizacion as CodigoAutorizacion,m.nombre_titular as NombreTitular
                //                              from TransferenciasSolicitud a join TransferenciasDetalle b on a.folio = b.folioSolicitud
                //                              join maquila m on b.cuentaOrigen = m.num_cuenta
                //                              where cuentadestino = '" + numCuenta + "' and date(fechaHoraCreacion) between '" + fechaInicio.ToString("yyyy-MM-dd") + "' and '" + fechaFin.ToString("yyyy-MM-dd") + "';";

                var query = @"select a.fechaHoraCreacion as FechaCreacion,b.codigoAutorizacion as CodigoAutorizacion, m1.nombre_titular Remitente, m2.nombre_titular Destinatario, b.MontoComision Comision, a.ConceptoTransferencia
                             from TransferenciasSolicitud a join TransferenciasDetalle b on a.folio = b.folioSolicitud, maquila m1, maquila m2
                             where cuentadestino = @numCuenta and date(fechaHoraCreacion) between @fechaInicio and @fechaFin and m1.num_cuenta = b.CuentaOrigen and m2.num_cuenta = b.CuentaDestino;";

                var cmd = new MySqlCommand
                {
                    CommandText = query,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);

                if (dataTable.Rows.Count > 0)
                {
                    trasferenciasDeTerceros = new List<TransferenciasDeTerceros>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var transferencia = new TransferenciasDeTerceros
                        {
                            FechaCreacion = (DateTime)row["FechaCreacion"],
                            CodigoAutorizacion = row["CodigoAutorizacion"].ToString(),
                            Remitente = row["Remitente"].ToString().ToUpper(),
                            Destinatario = row["Destinatario"].ToString().ToUpper(),
                            Comision = (decimal)row["Comision"],
                            ConceptoTransferencia = row["ConceptoTransferencia"].ToString()
                        };
                        trasferenciasDeTerceros.Add(transferencia);
                    }
                }

            }

            catch (Exception ex)
            {
                trasferenciasDeTerceros = new List<TransferenciasDeTerceros>();
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return trasferenciasDeTerceros;
        }

        public List<TransferenciasATerceros> ObtenerTransferenciasATerceros(string numCuenta, DateTime fechaInicio, DateTime fechaFin)
        {
            var trasferenciasATerceros = new List<TransferenciasATerceros>();

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta },
                    new MySqlParameter("@fechaInicio", MySqlDbType.Date) { Value = fechaInicio.ToString("yyyy-MM-dd") },
                    new MySqlParameter("@fechaFin", MySqlDbType.Date) { Value = fechaFin.ToString("yyyy-MM-dd") }
                };

                //                var query = @"select a.fechaHoraCreacion as FechaCreacion,b.codigoAutorizacion as CodigoAutorizacion,m.nombre_titular as NombreTitular
                //                              from TransferenciasSolicitud a join TransferenciasDetalle b on a.folio = b.folioSolicitud
                //                              join maquila m on b.cuentaDestino = m.num_cuenta
                //                              where cuentaorigen = '" + numCuenta + "' and date(fechaHoraCreacion) between '" + fechaInicio.ToString("yyyy-MM-dd") + "' and '" + fechaFin.ToString("yyyy-MM-dd") + "';";

                var query = @"select a.fechaHoraCreacion as FechaCreacion,b.codigoAutorizacion as CodigoAutorizacion, m1.nombre_titular Remitente, m2.nombre_titular Destinatario,b.MontoComision Comision, a.ConceptoTransferencia
                              from TransferenciasSolicitud a join TransferenciasDetalle b on a.folio = b.folioSolicitud, maquila m1, maquila m2
                              where cuentaorigen = @numCuenta and date(fechaHoraCreacion) between @fechaInicio and @fechaFin and m1.num_cuenta = b.CuentaOrigen and m2.num_cuenta = b.CuentaDestino;";

                var cmd = new MySqlCommand
                {
                    CommandText = query,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);

                if (dataTable.Rows.Count > 0)
                {
                    trasferenciasATerceros = new List<TransferenciasATerceros>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var transferencia = new TransferenciasATerceros
                        {
                            FechaCreacion = (DateTime)row["FechaCreacion"],
                            CodigoAutorizacion = row["CodigoAutorizacion"].ToString(),
                            Remitente = row["Remitente"].ToString().ToUpper(),
                            Destinatario = row["Destinatario"].ToString().ToUpper(),
                            Comision =(decimal)row["Comision"],
                            ConceptoTransferencia = row["ConceptoTransferencia"].ToString()

                        };
                        trasferenciasATerceros.Add(transferencia);
                    }
                }

            }

            catch (Exception ex)
            {
                trasferenciasATerceros = new List<TransferenciasATerceros>();
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return trasferenciasATerceros;
        }

        public List<TransferenciasDisposicion> ObtenerTransferenciasDisposicion(string numCuenta, DateTime fechaInicio, DateTime fechaFin)
        {
            var transferenciasDisposiciones = new List<TransferenciasDisposicion>();

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta },
                    new MySqlParameter("@fechaInicio", MySqlDbType.Date) { Value = fechaInicio.ToString("yyyy-MM-dd") },
                    new MySqlParameter("@fechaFin", MySqlDbType.Date) { Value = fechaFin.ToString("yyyy-MM-dd") }
                };

                var query = @" select a.NumAutorizacion, b.fechaHoraCreacion, d.Nombre as Destinatario from CargosDisposicionesEfectivo a
                               join DisposicionesEfectivo b on a.IdDisposicion = b.Id
                               join UsuariosOnlineCLABE c on b.clabeDestino = c.id
                               join bancos_stp d on c.IdBanco = d.Id
                               where a.numCuenta = @numCuenta and date(a.fechaHoraCreacion) between @fechaInicio and @fechaFin and a.IdMovimiento > 0 and a.Autorizado = 1;";

                 var cmd = new MySqlCommand
                {
                    CommandText = query,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);

                if (dataTable.Rows.Count > 0)
                {
                    transferenciasDisposiciones = new List<TransferenciasDisposicion>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var transferencia = new TransferenciasDisposicion
                        {
                           
                            NumAutorizacion = row["NumAutorizacion"].ToString(),
                            fechaHoraCreacion = (DateTime)row["fechaHoraCreacion"],
                            Destinatario = row["Destinatario"].ToString()


                        };
                        transferenciasDisposiciones.Add(transferencia);
                    }
                }

            }

            catch (Exception ex)
            {
                transferenciasDisposiciones = new List<TransferenciasDisposicion>();
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            

            return transferenciasDisposiciones;
        }

        public List<TransferenciasAbonosACuenta> ObtenerTrasnferenciasAbonos(string numCuenta, DateTime fechaInicio, DateTime fechaFin)
        {
            var transferenciasAbonos = new List<TransferenciasAbonosACuenta>();

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta },
                    new MySqlParameter("@fechaInicio", MySqlDbType.Date) { Value = fechaInicio.ToString("yyyy-MM-dd") },
                    new MySqlParameter("@fechaFin", MySqlDbType.Date) { Value = fechaFin.ToString("yyyy-MM-dd") }
                };

                var query = @"SELECT b.codigoAutorizacionPOS as codigoAutorizacion , a.fechaCreacion, d.Nombre as Remitente
                              FROM broxelco_rdg.dispersiones_ws_solicitudes a
                              join dispersionesInternas b on a.idSolicitud = b.idSolicitud
                              join RecepcionTransferencias c on a.idTransacFrom = c.idStp
                              join bancos_stp d on c.InstitucionOrdenante = d.clave
                              where a.cuenta = @numCuenta and date(a.fechaCreacion) between @fechaInicio and @fechaFin;";

                var cmd = new MySqlCommand
                {
                    CommandText = query,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);

                if (dataTable.Rows.Count > 0)
                {
                    transferenciasAbonos = new List<TransferenciasAbonosACuenta>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var transferencia = new TransferenciasAbonosACuenta
                        {

                            codigoAutorizacion = row["codigoAutorizacion"].ToString(),
                            fechaCreacion = (DateTime)row["fechaCreacion"],
                            Remitente = row["Remitente"].ToString()


                        };
                        transferenciasAbonos.Add(transferencia);
                    }
                }

            }

            catch (Exception ex)
            {
                transferenciasAbonos = new List<TransferenciasAbonosACuenta>();
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return transferenciasAbonos;
        }

        /// <summary>
        /// Obtiene las comisiones adicionales como  el incremento de linea de crédito.
        /// </summary>
        /// <param name="producto">producto que usa el corporativo</param>
        /// <param name="claveClienteCorporativo">id del cliente corporativo.</param>
        /// <returns>lista de comisiones adicionales para el uso de las tarjetas.</returns>
        public List<DetalleComisionAsignacion> ObtenerComisionTarjeta(string producto, string claveClienteCorporativo)
        {
            var comisiones = new List<DetalleComisionAsignacion>();
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto },
                    new MySqlParameter("@claveCliente", MySqlDbType.VarChar) { Value = claveClienteCorporativo },
                };

                var sql = @"select  
			                dt.ClaveCliente, dt.Producto, dt.AplicaComo,dt.Monto , ct.IdComercio ,ct.Descripcion
		                    from  
			                DetalleComisionTarjeta dt
                            inner join ComisionesTarjeta ct
                            on dt.IdComisionesTarjeta = ct.Id
                            where dt.Estatus = 1 and ct.Estatus = 1 and dt.Producto = @producto and dt.ClaveCliente = @claveCliente";

                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    comisiones = new List<DetalleComisionAsignacion>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var comision = new DetalleComisionAsignacion
                        {
                            idComercio = (int)row["IdComercio"],
                            Valor = (decimal)row["Monto"],
                            TipoAplica = row["AplicaComo"].ToString()
                        };
                        comisiones.Add(comision);
                    }
                }
            }
            catch (Exception ex)
            {
                comisiones = new List<DetalleComisionAsignacion>();
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return comisiones;
        }

        #endregion
        #region Dispersiones

        public long InsertDispersionWs(string idUser, string lastDigits, double amount, string token, string ipFrom, int idTransacFrom, string cuenta = null, string referencia = null)
        {
            long data;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idUser", MySqlDbType.VarChar) { Value = idUser },
                    new MySqlParameter("@lastDigits", MySqlDbType.VarChar) { Value = lastDigits },
                    new MySqlParameter("@amount", MySqlDbType.Decimal) { Value = amount },
                    new MySqlParameter("@token", MySqlDbType.VarChar) { Value = token },
                    new MySqlParameter("@ipFrom", MySqlDbType.VarChar) { Value = ipFrom },
                    new MySqlParameter("@cuenta", MySqlDbType.VarChar) { Value = cuenta },
                    new MySqlParameter("@idTransacFrom", MySqlDbType.VarChar) { Value = idTransacFrom },
                    new MySqlParameter("@referencia", MySqlDbType.VarChar) { Value = referencia }
                };

                var sql = "insert into dispersiones_ws_solicitudes (idUser,lastDigits,amount,token,fechaCreacion, ipFrom, idTransacFrom, cuenta, referencia) " +
                          "values (@idUser, @lastDigits,@amount, @token,now(), @ipFrom,@idTransacFrom,@cuenta,@referencia);";
                
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                var id = cmd.LastInsertedId;
                data = id;
            }
            catch (Exception)
            {
                data = 0;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return data;
        }
        public List<OperDb> GetOperacionesSolicitud(int id)
        {
            List<OperDb> operaciones = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@id", MySqlDbType.Int32) { Value = id }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select id, consecutivo, implementadora, estatus " +
                                  "from dispersiones_ws_operaciones " +
                                  "where id = @id " +
                                  "and estatus = 1 " +
                                  "order by consecutivo",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var operacionesTable = new DataTable();
                operacionesTable.Load(dr);
                if (operacionesTable.Rows.Count > 0)
                {
                    operaciones = (from DataRow row in operacionesTable.Rows
                                   select new OperDb
                                   {
                                       Id = Convert.ToInt32(row["id"].ToString()),
                                       Consecutivo = Convert.ToInt32(row["consecutivo"].ToString()),
                                       Implementadora = row["implementadora"].ToString(),
                                       Estatus = Convert.ToInt32(row["estatus"].ToString())
                                   }).ToList();
                }
            }
            catch (Exception)
            {
                operaciones = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return operaciones;
        }
        public void InsertDispersionErr(long folio, string desc)
        {
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio },
                    new MySqlParameter("@desc", MySqlDbType.MediumText) { Value = desc }
                };

                var sql = "insert into dispersiones_ws_errores(folio,descripcion) values (@folio,@desc)";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
        }
        public InfoCuentaSolicitud ValidaCuenta(string numCuenta)
        {
            InfoCuentaSolicitud res = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select d.CLABE, d.claveCliente, d.producto " +
                                  "from DetalleClientesBroxel d join maquila m on d.ClaveCliente = m.clave_cliente and d.Producto = m.producto " +
                                  "where num_cuenta = @numCuenta;",
                    Connection = _conn,
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    res = new InfoCuentaSolicitud
                    {
                        Clabe = row["CLABE"].ToString(),
                        ClaveCliente = row["claveCliente"].ToString(),
                        Producto = row["producto"].ToString()
                    };
                    
                    if (string.IsNullOrEmpty(res.Clabe))
                        throw new Exception("CLABE Vacia");
                }
            }
            catch (Exception)
            {
                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }
        /// <summary>
        /// Obtiene los datos de la cuenta virtual para una cuenta WE Expenses
        /// </summary>
        /// <param name="claveCliente">Clave de cliente corporativo</param>
        /// <returns>Informacion de la cuenta</returns>
        public InfoCuentaSolicitud ObtenCuentaVirtualWe(string claveCliente)
        {
            InfoCuentaSolicitud res = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@claveCliente", MySqlDbType.VarChar) { Value = claveCliente },
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select d.CLABE, m.num_cuenta " +
                                  "from DetalleClientesBroxel d join maquila m on d.ClaveCliente = m.clave_cliente and d.Producto = m.producto  " +
                                  "where claveCliente = @claveCliente and d.producto = 'D152';",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    res = new InfoCuentaSolicitud
                    {
                        Clabe = row["CLABE"].ToString(),
                        Cuenta = row["num_cuenta"].ToString(),
                    };

                    if (string.IsNullOrEmpty(res.Clabe))
                        throw new Exception("CLABE Vacia");
                }
            }
            catch (Exception)
            {
                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        /// <summary>
        /// Valida que la cuenta exista en broxel y sea suceptible de ser dispersada
        /// </summary>
        /// <param name="numCuenta">Numero de cuenta</param>
        /// <param name="idUser">Identificador de usuario</param>
        /// <returns>Id de la tabla registro tc, nulo en caso de no encontrar registro</returns>
        public string ValidaCuentaNoClabe(string numCuenta)
        {
            string res = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select r.id as Id " +
                                  "from registro_tc r join registri_broxel rb on r.id = rb.id_de_registro " +
                                  "join maquila m on r.numero_de_cuenta = m.num_cuenta " +
                        //"join usuarios_dispersiones_ws u on m.clave_cliente = u.claveCliente and m.producto = u.producto " +
                                  "where rb.tipo='00' " +
                                  "and m.num_cuenta = @numCuenta ",
                    //"and u.idUser = '" + idUser + "'",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    res = row["Id"].ToString();
                }
            }
            catch (Exception)
            {
                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        public bool ValidaOperEnLinea(string numCuenta)
        {
            var res = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select d.AplicaSPEI " +
                                  "from DetalleClientesBroxel d join maquila m on d.ClaveCliente = m.clave_cliente and d.Producto = m.producto " +
                                  "where num_cuenta = @numCuenta;",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    res = row["AplicaSPEI"].ToString() == "True";
                }
            }
            catch (Exception)
            {
                res = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }
        public ProductoValInfo ValidaDispersaPagoEnLinea(string numCuenta)
        {
            ProductoValInfo res = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select dispersa, PagoEnLinea, m.producto  " +
                                  "from productos_broxel p join maquila m on p.codigo = m.producto  " +
                                  "where num_cuenta = @numCuenta;",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();               

                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    res = new ProductoValInfo
                    {
                        DisperDispersion = row["dispersa"].ToString() == "1",
                        Pago = row["PagoEnLinea"].ToString() == "True",
                        Producto = row["producto"].ToString()
                    };
                }
            }
            catch (Exception)
            {
                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }
        public bool SetCuentaWsStatus(long folio, string cuenta)
        {
            var val = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio },
                    new MySqlParameter("@cuenta", MySqlDbType.VarChar) { Value = cuenta }
                };

                var sql = "update dispersiones_ws_solicitudes set cuenta = @cuenta where folio = @folio;";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                    val = true;
            }
            catch (MySqlException)
            {
                val = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return val;
        }
        public bool SetReferenciaWs(long folio, string referencia)
        {
            var val = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio },
                    new MySqlParameter("@referencia", MySqlDbType.VarChar) { Value = referencia }
                };

                var sql = "update dispersiones_ws_solicitudes set referencia = @referencia where folio = @folio;";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                    val = true;
            }
            catch (MySqlException)
            {
                val = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return val;
        }

        public bool SetDispersionWsStatus(long folio, int status)
        {
            var val = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio },
                    new MySqlParameter("@status", MySqlDbType.Int16) { Value = status }
                };

                var sql = "update dispersiones_ws_solicitudes set status = @status, fechaUltAct = now() where folio = @folio;";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                    val = true;
            }
            catch (Exception)
            {
                val = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return val;
        }
        /// <summary>
        /// Este metodo tiene la funcion de activar la bandera de la de Desbloqueo cuenta,
        /// la cual sirve para indicar si la tarjeta estaba Bloqueada y se necesito desbloquer
        /// para poder llevar acabo el proceso para porterios volver a Bloquearla.
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        public bool SetBanderaDesbloqueoCuenta(long folio, int status)
        {
            var val = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio },
                    new MySqlParameter("@status", MySqlDbType.Int16) { Value = status }
                };

                var sql = "update dispersiones_ws_solicitudes set bloquearCuenta = @status where folio = @folio;";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                    val = true;
            }
            catch (Exception)
            {
                val = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return val;
        }
        /// <summary>
        /// Este metodo tiene la funcion de retornar si se tiene que volver a bloquear la cuenta o no,
        /// dependiendo de si esta al momento de dispersar se encontraba operando o no.
        /// </summary>
        /// <param name="folio">Folio de la solicitud</param>
        /// <returns></returns>
        public bool reversoBloqueoCuenta(long folio)
        {
            var res = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                var cmd = new MySqlCommand
                {
                    CommandText = " select bloquearCuenta from dispersiones_ws_solicitudes where folio = " + folio + ";",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var resReader = int.Parse(reader["bloquearCuenta"].ToString());
                        if (resReader == 1)
                        {
                            return true;

                        }

                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }
            catch (Exception)
            {
                res = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }
        /// <summary>
        /// Actualiza el id de la tabla DetalleClientesBroxel Correspondiente al movimiento de ajuste de linea de credito en la tabla dispersiones_ws_solicitudes
        /// </summary>
        /// <param name="folio">Folio de la tabla dispersiones_ws_solicitudes</param>
        /// <param name="updLc">Valor del campo idUpdLC</param>
        /// <returns></returns>
        public bool SetDispersionWsUpdLc(long folio, int updLc)
        {
            var val = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio },
                    new MySqlParameter("@updLc", MySqlDbType.Int32) { Value = updLc }
                };

                var sql = "update dispersiones_ws_solicitudes set idUpdLC = @updLc, fechaUltAct = now() where folio = @folio;";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                    val = true;
            }
            catch (Exception)
            {
                val = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return val;
        }
        /// <summary>
        /// Inserta solicitudes de dispersion en base a los datos en la tabla dispersiones_ws_solicitudes
        /// </summary>
        /// <param name="folio">Folio tabla dispersiones_ws_solicitudes</param>
        /// <returns></returns>
        public bool SpInsDispersionesFromWs(long folio)
        {
            bool result;

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio }
                };

                var cmd = new MySqlCommand
                {
                    Connection = _conn,
                    CommandText = "call spInsDispersionesFromWS(@folio);",
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return result;
        }
        /// <summary>
        /// Inserta solicitudes de pago en base a los datos en la tabla dispersiones_ws_solicitudes
        /// </summary>
        /// <param name="folio">Folio tabla dispersiones_ws_solicitudes</param>
        /// <returns></returns>
        public bool SpInsPagosFromWs(long folio)
        {
            bool result;

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio }
                };

                var cmd = new MySqlCommand
                {
                    Connection = _conn,
                    CommandText = "call spInsPagosFromWS(@folio);",
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return result;
        }
        /// <summary>
        /// Inserta solicitudes de devolucion en base a los datos en la tabla dispersiones_ws_solicitudes
        /// </summary>
        /// <param name="folio">Folio tabla dispersiones_ws_solicitudes</param>
        /// <returns></returns>
        public bool spInsDevolucionesFromWS(long folio)
        {
            bool result;

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio }
                };

                var cmd = new MySqlCommand
                {
                    Connection = _conn,
                    CommandText = "call spInsDevolucionesFromWS(@folio);",
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return result;
        }
        /// <summary>
        /// Obtiene informacion para invocar al servicio de dispersion / pago
        /// </summary>
        /// <param name="folio">Folio tabla dispersiones_ws_solicitudes</param>
        /// <returns></returns>
        public WsInvokeData GetWsInvokeData(long folio)
        {
            WsInvokeData data;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio }
                };

                var sql = "select a.idSolicitud, b.email " +
                          "from dispersiones_ws_solicitudes a join usuarios_dispersiones_ws b on a.idUser = b.idUser " +
                          "where folio = @folio";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tableLogin = new DataTable();
                tableLogin.Load(dr);
                if (tableLogin.Rows.Count > 0)
                {
                    data = new WsInvokeData();
                    var row = tableLogin.Rows[0];
                    data.IdSolicitud = row["idSolicitud"].ToString();
                    data.Email = row["email"].ToString();
                }
                else
                {
                    data = null;
                }
            }
            catch (Exception)
            {
                data = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return data;
        }
        /// <summary>
        /// Obtiene el folio y estatus de la tabla dispersiones_ws_solicitudes
        /// </summary>
        /// <param name="idTransac">id de transacción del servicio cliente</param>
        /// <param name="numCuenta">numero de cuenta</param>
        /// <param name="idUser">id de usuario</param>
        /// <returns></returns>
        public DispWsValModel GetFolio(int idTransac, string numCuenta, string idUser)
        {
            DispWsValModel res = null;
            if (idTransac == 0)
                return res;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idTransac", MySqlDbType.Int32) { Value = idTransac },
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta },
                    new MySqlParameter("@idUser", MySqlDbType.VarChar) { Value = idUser }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select folio, status " +
                                  "from dispersiones_ws_solicitudes " +
                                  "where idTransacFrom = @idTransac and cuenta = @numCuenta and idUser = @idUser and status <> 100;",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    res = new DispWsValModel
                    {
                        Folio = Convert.ToInt64(row["folio"].ToString()),
                        Status = Convert.ToInt32(row["status"].ToString())
                    };
                }
            }
            catch (MySqlException)
            {
                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }
        /// <summary>
        /// Obtiene el id de operacion en la tabla LogDetalleClientesBroxel
        /// </summary>
        /// <param name="monto">Monto de la transacción</param>
        /// <param name="clabe">Clabe del cliente</param>
        /// <returns></returns>
        public int GetIdLc(decimal monto, string clabe)
        {
            var res = 1;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@monto", MySqlDbType.Decimal) { Value = monto.ToString("F2") },
                    new MySqlParameter("@clabe", MySqlDbType.VarChar) { Value = clabe }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select l.id, d.CLABE  " +
                                  "from LogDetalleClientesBroxel l join DetalleClientesBroxel d on l.IdDetalleClientesBroxel = d.Id " +
                                  "where l.Monto = @monto and l.Motivo = 'RecepcionTransferenciawsSPEI' " +
                                  "and l.campoAfectado = 'Abonos' and d.CLABE = @clabe " +
                                  "and date(l.fechaHoraCreacion) = date(now()) " +
                                  "group by d.CLABE;",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    var row = dataTable.Rows[0];
                    res = Convert.ToInt32(row["id"].ToString());
                }
            }
            catch (Exception)
            {
                res = 1;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }
        /// <summary>
        /// Inserta en dispersiones_ws_detalles
        /// </summary>
        /// <param name="folio">folio de dispersiones_ws_solicitudes</param>
        /// <param name="cuenta">cuenta</param>
        /// <returns>id de la inserción</returns>
        public string InsertDetalleDispersionesWs(long folio, string cuenta, decimal saldo = 0)
        {
            string data;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio },
                    new MySqlParameter("@cuenta", MySqlDbType.VarChar) { Value = cuenta },
                    new MySqlParameter("@saldo", MySqlDbType.Decimal) { Value = saldo }
                };

                var sql = "insert into dispersiones_ws_detalles (folio, cuenta, saldo) values (folio,@cuenta, @saldo);";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                var id = cmd.LastInsertedId;
                data = id.ToString(CultureInfo.InvariantCulture);
            }
            catch (MySqlException)
            {
                data = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return data;
        }
        /// <summary>
        /// Inserta en la tabla dispersiones internas
        /// </summary>
        /// <param name="folio">Folio de solicitud de asignacion de línea</param>
        /// <param name="cuenta">Numero de cuenta</param>
        /// <param name="producto">Clave de producto</param>
        /// <param name="claveCliente">Clave de cliente</param>
        /// <param name="montoPos">Monto POS</param>
        /// <param name="sinAtm">Boleano que indica si se calcula o no el monto atm a partir de la configuracion en base de datos</param>
        /// <returns></returns>
        public bool InsertDispersionInterna(string folio, string cuenta, string producto, string claveCliente, decimal montoPos, bool sinAtm = false)
        {
            bool res;
            Decimal perc = 0;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                if (!sinAtm)
                {
                    var parameters1 = new MySqlParameter[]
                    {
                        new MySqlParameter("@claveCliente", MySqlDbType.VarChar) { Value = claveCliente },
                        new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto }
                    };

                    var cmd = new MySqlCommand
                    {
                        CommandText = "select ifnull(atm,0)/100.00 perc " +
                                      "from DetalleClientesBroxel " +
                                      "where claveCliente = @claveCliente " +
                                      "and producto = @producto",
                        CommandType = CommandType.Text,
                        CommandTimeout = 1200,
                        Connection = _conn
                    };

                    cmd.Parameters.AddRange(parameters1);
                    
                    var dr = cmd.ExecuteReader();
                    var dataTable = new DataTable();
                    dataTable.Load(dr);
                    if (dataTable.Rows.Count > 0)
                    {
                        perc = Convert.ToDecimal(dataTable.Rows[0]["perc"].ToString());
                    }
                    
                }

                var parameters2 = new MySqlParameter[]
                    {
                        new MySqlParameter("@folio", MySqlDbType.VarChar) { Value = folio },
                        new MySqlParameter("@cuenta", MySqlDbType.VarChar) { Value = cuenta },
                        new MySqlParameter("@montoPos", MySqlDbType.Double) { Value = montoPos.ToString(CultureInfo.InvariantCulture) },
                        new MySqlParameter("@incrementoATM", MySqlDbType.Double) { Value = (montoPos * perc).ToString(CultureInfo.InvariantCulture) },
                        new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto },
                        new MySqlParameter("@claveCliente", MySqlDbType.VarChar) { Value = claveCliente }
                    };

                var cmd2 = new MySqlCommand
                {
                    CommandText = "insert into dispersionesInternas (idSolicitud, cuenta, incrementoPOS, incrementoATM, producto, claveCliente) " +
                                  "values (@folio, @cuenta, @montoPos, @incrementoATM, @producto, @claveCliente)",
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200,
                    Connection = _conn
                };

                cmd2.Parameters.AddRange(parameters2);

                var c = cmd2.ExecuteNonQuery();

                if (c == 0)
                    throw new Exception("No se pudo insertar en dispersiones internas");

                res = true;
            }
            catch (Exception ex)
            {
                var msg = "Error al insertar en dispersionesInternas: " + ex.Message;
                Trace.WriteLine(msg);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Error en proceso de originaciones pendientes", msg, "67896789", "Originaciones Pendientes");
                res = false;
            }
            finally
            {
                _conn.Close();
            }
            return res;
        }
        /// <summary>
        /// Inserta en la tabla dispersionesSolicitudes
        /// </summary>
        /// <param name="claveCliente">clave de cliente</param>
        /// <param name="producto">producto</param>
        /// <param name="estado">Estado inicial del registro</param>
        /// <returns></returns>
        public string InsertSolicitudDispersion(string claveCliente, string producto, string estado)
        {
            var res = "";
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                var cmd = new MySqlCommand
                {
                    CommandText = "select COALESCE(concat(date_format(now(),'%y%m'),'D',right(concat('000000',cast((max(cast(substring(folio,6) as unsigned))+1) as char(6))),6)), concat(date_format(now(), '%y%m'),'D', '000001')) as folioD " +
                                  "from dispersionesSolicitudes " +
                                  "where folio like concat(date_format(now(),'%y%m'),'D', '%')",
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200,
                    Connection = _conn
                };
                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    res = dataTable.Rows[0]["folioD"].ToString();
                }

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@claveCliente", MySqlDbType.Int32) { Value = claveCliente },
                    new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto },
                    new MySqlParameter("@estado", MySqlDbType.VarChar) { Value = estado },
                    new MySqlParameter("@res", MySqlDbType.VarChar) { Value = res }
                };

                cmd = new MySqlCommand
                {
                    CommandText = "insert into dispersionesSolicitudes (folio, cliente,claveCliente,rfc,solicitante, areaSolicitante,email, montoPrincipal, producto, usuarioCreacion, fechaCreacion, usuarioEjecucion, fechaEjecucion, estado, tipo, usuarioAprobacion, fechaAprobacion, total_cuentas, valor_estimado) " +
                                  "select @res, " +
                                  "nombreCorto, " +
                                  "@claveCliente, " +
                                  "rfc, " +
                                  "'webService', " +
                                  "'WEBSERVICE', " +
                                  "case when CorreoContacto = '' then 'asignaciondelinea@broxel.com' else ifnull(CorreoContacto, 'asignaciondelinea@broxel.com') end as Correo, " +
                                  "0, " +
                                  "@producto, " +
                                  "'webService', " +
                                  "now(), " +
                                  "'webService', " +
                                  "now(), " +
                                  "@estado, " +
                                  "'INCREMENTO', " +
                                  "'webService', " +
                                  "now(), " +
                                  "0, " +
                                  "0 " +
                                  "from clientesBroxel where claveCliente = @claveCliente ",
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200,
                    Connection = _conn
                };

                cmd.Parameters.AddRange(parameters);

                var c = cmd.ExecuteNonQuery();

                if (c == 0)
                    throw new Exception("No se pudo insertar el folio de solicitud de dispersion");

            }
            catch (Exception ex)
            {
                var msg = "Error al insertar solicitud de dispersion: " + ex;
                Trace.WriteLine(msg);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Error en proceso de originaciones pendientes", msg, "67896789", "Dispersiones WebService");
                res = "";
            }
            finally
            {
                _conn.Close();
            }
            return res;
        }
        /// <summary>
        /// Finaliza folio de dispersión
        /// </summary>
        /// <param name="folio">Folio de dispersion</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool FinalizaDispersionSolicitud(string folio)
        {
            var res = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.Int32) { Value = folio }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "update dispersionesSolicitudes a join (" +
                                  "select count(id) cuentas, sum(incrementoPOS) total, idSolicitud " +
                                  "from dispersionesInternas " +
                                  "where idSolicitud = @folio " +
                                  "group by idSolicitud" +
                                  ") b on a.folio = b.idSolicitud " +
                                  "set total_cuentas = b.cuentas, valor_estimado = b.total, montoPrincipal = b.total " +
                                  "where a.folio = @folio",
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200,
                    Connection = _conn
                };

                cmd.Parameters.AddRange(parameters);

                var c = cmd.ExecuteNonQuery();

                if (c == 0)
                    throw new Exception("No se pudo actualizar la solicitud de dispersion");

                res = true;
            }
            catch (Exception ex)
            {
                var msg = "Error al actualizar solicitud de dispersion: " + ex.Message;
                Trace.WriteLine(msg);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Error al actualizar dispersionesSolicitudes", msg, "67896789", "Dispersiones WS");
                res = false;
            }
            finally
            {
                _conn.Close();
            }
            return res;
        }
        /// <summary>
        /// Metodo que valida si un comercio existe en Black-List
        /// </summary>
        /// <param name="numeroCuenta">numero de cuenta del comercio</param>
        /// <returns></returns>
        public bool ValidaComercioBlackList(string numeroCuenta)
        {
            var resultado = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numeroCuenta", MySqlDbType.VarChar) { Value = numeroCuenta }
                };

                string query = " SELECT m.id FROM maquila m INNER JOIN Comercio c " +
                                           "ON m.num_cuenta = c.numCuentaBroxel INNER JOIN comerciosBlacklist b " +
                                           "ON b.Comercio = c.Comercio WHERE m.num_cuenta = @numeroCuenta;";

                var cmd = new MySqlCommand
                {
                    CommandText = query,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tabla = new DataTable();
                tabla.Load(dr);

                if (tabla.Rows.Count > 0)
                    resultado = true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("{0:dd/MM/yyyy HH:mm:ss.fff} Error en ValidaComercioBlackList :  {1}", DateTime.Now, ex.Message));
                resultado = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return resultado;
        }
        #endregion
        #region Token
        public string GetTokenUsuarioApp(string idApp)
        {
            string resultado;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idApp", MySqlDbType.VarChar) { Value = idApp }
                };

                string sql = "select pwd " +
                             "from TokenAppUsers " +
                             "where idApp = @idApp and status = 1 ";

                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tableResumen = new DataTable();
                tableResumen.Load(dr);
                if (tableResumen.Rows.Count > 0)
                {
                    var row = tableResumen.Rows[0];
                    resultado = row["pwd"].ToString();
                    if (resultado.Equals(""))
                        resultado = null;
                }
                else
                {
                    resultado = null;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                resultado = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return resultado;
        }
        public int GetTokenAppTolerance(string idApp)
        {
            var resultado = 0;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idApp", MySqlDbType.VarChar) { Value = idApp }
                };

                string sql = "select tolerance " +
                             "from TokenAppUsers " +
                             "where idApp = @idApp and status = 1 ";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tableResumen = new DataTable();
                tableResumen.Load(dr);
                if (tableResumen.Rows.Count > 0)
                {
                    var row = tableResumen.Rows[0];
                    resultado = Convert.ToInt32(row["tolerance"].ToString());
                }
                else
                {
                    resultado = 5;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado = 5;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return resultado;
        }

        public string InsertToken(string idUser, string token, string idApp, string idDevice)
        {
            string data;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idUser", MySqlDbType.VarChar) { Value = idUser },
                    new MySqlParameter("@token", MySqlDbType.VarChar) { Value = token },
                    new MySqlParameter("@idApp", MySqlDbType.VarChar) { Value = idApp },
                    new MySqlParameter("@idDevice", MySqlDbType.VarChar) { Value = idDevice }
                };

                var sql = "insert into TokenBroxel (usuario,token,idApp, idDevice) values (@idUser,@token, @idApp, @idDevice )";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                var id = cmd.LastInsertedId;
                data = id.ToString(CultureInfo.InvariantCulture);
            }
            catch (MySqlException)
            {
                data = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return data;
        }

        public TokenUsuario GetTokenUsuario(string usuario)
        {
            TokenUsuario resultado = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@usuario", MySqlDbType.VarChar) { Value = usuario }
                };

                string sql = "select token, offset " +
                             "from TokenBroxel " +
                             "where usuario = @usuario";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tableResumen = new DataTable();
                tableResumen.Load(dr);
                if (tableResumen.Rows.Count > 0)
                {
                    var row = tableResumen.Rows[0];
                    resultado = new TokenUsuario
                    {
                        TokenSeed = row["token"].ToString(),
                        Offset = Convert.ToInt64(row["offset"].ToString())
                    };
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                resultado = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return resultado;
        }
        public TokenUsuario GetTokenUsuario(string usuario, string idApp)
        {
            TokenUsuario resultado = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@usuario", MySqlDbType.VarChar) { Value = usuario },
                    new MySqlParameter("@idApp", MySqlDbType.VarChar) { Value = idApp }
                };

                string sql = "select token, offset " +
                             "from TokenBroxel " +
                             "where usuario = @usuario and idApp = @idApp;";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tableResumen = new DataTable();
                tableResumen.Load(dr);
                if (tableResumen.Rows.Count > 0)
                {
                    var row = tableResumen.Rows[0];
                    resultado = new TokenUsuario
                    {
                        TokenSeed = row["token"].ToString(),
                        Offset = Convert.ToInt64(row["offset"].ToString())
                    };
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                resultado = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return resultado;
        }

        public bool UpdOffsetToken(string user, string tokenSeed, long offset)
        {
            var result = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@user", MySqlDbType.VarChar) { Value = user },
                    new MySqlParameter("@tokenSeed", MySqlDbType.VarChar) { Value = tokenSeed },
                    new MySqlParameter("@offset", MySqlDbType.VarChar) { Value = offset }
                };

                var sql = "update TokenBroxel set offset = @offset where usuario = @user and token = @tokenSeed";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                    result = true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return result;
        }

        /// <summary>
        /// Actualiza el device asociado al token
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <param name="idApp">Id de aplicacion</param>
        /// <param name="device">Dispostivo</param>
        /// <returns></returns>
        public bool UpdDeviceToken(string user, string idApp, string device)
        {
            var result = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@user", MySqlDbType.VarChar) { Value = user },
                    new MySqlParameter("@idApp", MySqlDbType.VarChar) { Value = idApp },
                    new MySqlParameter("@device", MySqlDbType.VarChar) { Value = device }
                };

                var sql = "update TokenBroxel set idDevice = @device where usuario = @user and idApp = @idApp";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return result;
        }

        /// <summary>
        /// Actualiza el otp asociado al Token
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <param name="idApp">Id de la Aplicación</param>
        /// <param name="otp">OTP</param>
        /// <returns></returns>
        public bool UpdOtpToken(string otp, string user, string idApp)
        {
            var result = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@otp", MySqlDbType.VarChar) { Value = otp },
                    new MySqlParameter("@user", MySqlDbType.VarChar) { Value = user },
                    new MySqlParameter("@idApp", MySqlDbType.VarChar) { Value = idApp }
                };

                var sql = "update TokenBroxel set otpTemp = @otp, fechaUltOtp = now() where usuario = @user and idApp = @idApp";

                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return result;
        }

        public bool ValidaOtpTokenBroxel(string usuario, string idApp, string otp)
        {
            var resultado=false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@usuario", MySqlDbType.VarChar) { Value = usuario },
                    new MySqlParameter("@idApp", MySqlDbType.VarChar) { Value = idApp },
                    new MySqlParameter("@otp", MySqlDbType.VarChar) { Value = otp }
                };

                var sql = "select a.otpTemp " +
                             "from TokenBroxel a join TokenAppUsers b on a.idApp = b.idApp " +
                             "where a.usuario = @usuario and a.idAPp = @idApp " +
                             "and a.otpTemp = @otp " +
                             "and timediff(now(),fechaUltOtp) < str_to_date(cast(b.tolerance as char), '%i');";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tableResumen = new DataTable();
                tableResumen.Load(dr);
                if (tableResumen.Rows.Count > 0)
                {
                    resultado = true;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                resultado = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return resultado;
        }


        #endregion
        #region Renominacion

        /// <summary>
        /// Inserta Solicitud de Renominacion
        /// </summary>
        /// <param name="claveCliente">Clave de cliente</param>
        /// <param name="producto">Producto</param>
        /// <param name="emailDefault">Indica si la solicitud de renominación se hace a nombre de asignacion de lineas o el correo del cliente.</param>
        /// <returns>Folio de renominacion</returns>
        /// <exception cref="Exception"></exception>
        public string InsertSolicitudRenominacion(string claveCliente, string producto, bool emailDefault)
        {
            var res = "";
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                var cmd = new MySqlCommand
                {
                    CommandText = "select COALESCE(concat(date_format(now(),'%y%m'),'R',right(concat('000000',cast((max(cast(substring(folio,6) as unsigned))+1) as char(6))),6)), concat(date_format(now(), '%y%m'),'R', '000001')) as folioR " +
                                  "from RenominacionSolicitudes " +
                                  "where folio like concat(date_format(now(),'%y%m'),'R', '%')",
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200,
                    Connection = _conn
                };
                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    res = dataTable.Rows[0]["folioR"].ToString();
                }

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@claveCliente", MySqlDbType.VarChar) { Value = claveCliente },
                    new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto },
                    new MySqlParameter("@folio", MySqlDbType.VarChar) { Value = res }
                };

                cmd = new MySqlCommand
                {
                    CommandText = "insert into RenominacionSolicitudes (folio, claveCliente, producto, cliente, rfc, solicitante, Areasolicitante, email, usuariocreacion, fechaCreacion, usuarioEjecucion, FechaEjecucion, estado, tipo) " +
                                  "select @res ,@claveCliente, @producto, NombreCorto, rfc, 'webService', 'WEBSERVICE', " +
                                  (emailDefault?"'asignaciondelinea@broxel.com' as Correo, ": "case when CorreoContacto = '' then 'asignaciondelinea@broxel.com' else ifnull(CorreoContacto, 'asignaciondelinea@broxel.com') end as Correo, ") +
                                  "'webService', now(), 'webService', now(),'WebService','RENOMINACION' " +
                                  "from clientesBroxel where claveCliente = @claveCliente ",
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200,
                    Connection = _conn
                };

                cmd.Parameters.AddRange(parameters);

                var c = cmd.ExecuteNonQuery();

                if (c == 0)
                    throw new Exception("No se pudo insertar el folio de solicitud de renominacion");

            }
            catch (Exception ex)
            {
                var msg = "Error al insertar solicitud de renominacion: " + ex.Message;
                Trace.WriteLine("InsertSolicitudRenominacion: " + msg);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Error en InsertSolicitudRenominacion", msg, "67896789", "RenominacionesService");
                res = "";
            }
            finally
            {
                _conn.Close();
            }
            return res;
        }

        /// <summary>
        /// Inserta en renominaciones Internas
        /// </summary>
        /// <param name="folio">Folio de renominación</param>
        /// <param name="cuenta">Cuenta a renominar</param>
        /// <param name="tarjeta">Numero de tarjeta a renominar</param>
        /// <param name="producto">Producto</param>
        /// <param name="claveCliente">Clave de cliente</param>
        /// <param name="calle">Calle</param>
        /// <param name="numeroCalle">Numero de la calle, interior y exterior</param>
        /// <param name="colonia">Colonia</param>
        /// <param name="codigoPostal">Codigo Postal</param>
        /// <param name="nombreMunicipio">Municipio o Delegación</param>
        /// <param name="tipoDireccion">Tipo de dirección</param>
        /// <param name="codigoEstado">Codigo de Estado Credencial</param>
        /// <param name="codigoMunicipio">Codigo de Municipio Credencial</param>
        /// <param name="numeroDocumento">Numero de documento de identificacion</param>
        /// <param name="tipoDocumento">Tipo de documento de identificacion</param>
        /// <param name="telefono">Telefono</param>
        /// <param name="fechaNacimiento">Fecha de nacimiento</param>
        /// <param name="estadoCivil">Estado civil</param>
        /// <param name="hijos">Numero de hijos</param>
        /// <param name="sexo">Sexo</param>
        /// <param name="ocupacion">ocupación</param>
        /// <param name="denominacionTarjeta">Nombre que se embozara en la tarjeta</param>
        /// <param name="nombreTitular">Nombre del titular de la cuenta</param>
        /// <returns>Exito en caso de poder realizar la inserción sin problema</returns>
        /// <exception cref="Exception"></exception>
        public bool InsertRenominacionInterna(string folio, string cuenta, string tarjeta, string producto, string claveCliente, string calle, string numeroCalle, string colonia,
            string codigoPostal, string nombreMunicipio, string tipoDireccion, string codigoEstado, string codigoMunicipio, string numeroDocumento, string tipoDocumento,
            string telefono, string fechaNacimiento, string estadoCivil, string hijos, string sexo, string ocupacion, string denominacionTarjeta, string nombreTitular)
        {
            var res = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.VarChar) { Value = folio },
                    new MySqlParameter("@cuenta", MySqlDbType.VarChar) { Value = cuenta },
                    new MySqlParameter("@tarjeta", MySqlDbType.VarChar) { Value = tarjeta },
                    new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto },
                    new MySqlParameter("@claveCliente", MySqlDbType.VarChar) { Value = claveCliente },
                    new MySqlParameter("@calle", MySqlDbType.VarChar) { Value = calle },
                    new MySqlParameter("@numeroCalle", MySqlDbType.VarChar) { Value = numeroCalle },
                    new MySqlParameter("@colonia", MySqlDbType.VarChar) { Value = colonia },
                    new MySqlParameter("@codigoPostal", MySqlDbType.VarChar) { Value = codigoPostal },
                    new MySqlParameter("@nombreMunicipio", MySqlDbType.VarChar) { Value = nombreMunicipio },
                    new MySqlParameter("@tipoDireccion", MySqlDbType.VarChar) { Value = tipoDireccion },
                    new MySqlParameter("@codigoEstado", MySqlDbType.VarChar) { Value = codigoEstado },
                    new MySqlParameter("@codigoMunicipio", MySqlDbType.VarChar) { Value = codigoMunicipio },
                    new MySqlParameter("@numeroDocumento", MySqlDbType.VarChar) { Value = numeroDocumento },
                    new MySqlParameter("@tipoDocumento", MySqlDbType.VarChar) { Value = tipoDocumento },
                    new MySqlParameter("@telefono", MySqlDbType.VarChar) { Value = telefono },
                    new MySqlParameter("@fechaNacimiento", MySqlDbType.DateTime) { Value = fechaNacimiento },
                    new MySqlParameter("@estadoCivil", MySqlDbType.VarChar) { Value = estadoCivil },
                    new MySqlParameter("@hijos", MySqlDbType.VarChar) { Value = hijos },
                    new MySqlParameter("@sexo", MySqlDbType.VarChar) { Value = sexo },
                    new MySqlParameter("@ocupacion", MySqlDbType.VarChar) { Value = ocupacion },
                    new MySqlParameter("@denominacionTarjeta", MySqlDbType.VarChar) { Value = denominacionTarjeta },
                    new MySqlParameter("@nombreTitular", MySqlDbType.VarChar) { Value = nombreTitular }

                };

                var cmd = new MySqlCommand
                {
                    CommandText = "insert into RenominacionesInternas (idSolicitud, folioSolicitud, cuenta, tarjeta,  producto, " +
                                  "claveCliente, NombreCalle, NumeroCalle,Colonia, CodigoPostal, NombreMunicipio, " +
                                  "TipoDireccion, CodigoEstado, CodigoMunicipio, Pais, NumeroDeDocumento, TipoDocumento, " +
                                  "GrupoLiquidacion, Telefono, FechaNacimiento, EstadoCivil, Hijos, Sexo, Ocupacion, " +
                                  "DenominacionTarjeta, NombreCompletoTitular) " +
                                  "select id, @folio, @cuenta, @tarjeta, @producto, @claveCliente, @calle, @numeroCalle, " +
                                  "@colonia, @codigoPostal, @nombreMunicipio, @tipoDireccion, @codigoEstado, @codigoMunicipio, " +
                                  "'484', @numeroDocumento, @tipoDocumento, '9', @telefono, @fechaNacimiento, " +
                                  "@estadoCivil, @hijos, @sexo, @ocupacion, @denominacionTarjeta, @nombreTitular " +
                                  "from RenominacionSolicitudes " +
                                  "where folio = @folio",
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200,
                    Connection = _conn
                };

                cmd.Parameters.AddRange(parameters);

                var c = cmd.ExecuteNonQuery();

                if (c == 0)
                    throw new Exception("No se pudo insertar en renominacion interna");
                res = true;

            }
            catch (Exception ex)
            {
                var msg = "Error al insertar renominacion interna: " + ex.Message;
                Trace.WriteLine("InsertaRenominacionInterna: " + msg);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Error en proceso de inserción en renominaciones Internas", msg, "67896789", "RenominacionesService");
                res = false;
            }
            finally
            {
                _conn.Close();
            }
            return res;
        }

        /// <summary>
        /// Método para invocar al proceso previo a la renominación.
        /// </summary>
        /// <param name="idSolicitud">Id de la solicitud de renominación</param>
        /// <param name="msg">Mensaje de Error, en caso de exitir uno</param>
        /// <returns>booleano, true si la invocación fue exitosa, false de no serlo.</returns>
        public bool SpPreRenominacion(long idSolicitud, ref string msg)
        {
            var result = false;
            _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
            try
            {

                _conn.Open();
                var cmd = new MySqlCommand
                {
                    CommandText = "call spPreRenominacion(" + idSolicitud.ToString(CultureInfo.InvariantCulture) + ");",
                    Connection = _conn,
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception e)
            {
                msg = e.Message;
                _conn.Close();
                _conn = null;
                result = false;
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
                _conn = null;
            }
            return result;
        }

        /// <summary>
        /// Método para invocar al proceso posterior a la renominación.
        /// </summary>
        /// <param name="idSolicitud">Id de la solicitud de renominación</param>
        /// <param name="msg">Mensaje de error en caso de existirlo</param>
        /// <returns>booleano, true si la invocación fue exitosa, false de no serlo.</returns>
        public bool SpPostRenominacion(long idSolicitud, ref string msg)
        {
            var result = false;
            _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
            try
            {
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idSolicitud", MySqlDbType.Int32) { Value = idSolicitud }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "call SpPostRenominacion(@idSolicitud);",
                    Connection = _conn,
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception e)
            {
                msg = e.Message;
                _conn.Close();
                _conn = null;
                result = false;
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
                _conn = null;
            }
            return result;
        }
        #endregion
        #region Generales

        /// <summary>
        /// Valida un comercio debe crear MerchantAccount y usuario
        /// </summary>
        /// <returns>la cuenta si existe cuenta y usuario</returns>
        public string ValidaCreaciónMerchantAccountUsuario(int idComercio)
        {
            var res = "";
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idComercio", MySqlDbType.VarChar) { Value = idComercio }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select ac.cuenta " +
                                  "from Comercio c join maquila m on c.NumcuentaBroxel = m.num_cuenta " +
                                  "join accessos_clientes ac on c.numCuentaBroxel = ac.cuenta " +
                                  "where c.idComercio = @idComercio",
                    CommandType = CommandType.Text,
                    Connection = _conn,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var merchantTable = new DataTable();
                merchantTable.Load(dr);
                if (merchantTable.Rows.Count > 0)
                {
                    res = merchantTable.Rows[0]["cuenta"].ToString();
                }

            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al validar comercio con merchant account y usuario: " + e);
                res = "";
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }
        /// <summary>
        /// Valida si una cuenta esta en cuarentena de niveles de cuenta
        /// </summary>
        /// <param name="numCuenta">numero de cuenta</param>
        /// <returns>True si la cuenta se encuentra en cuarentena, False si no</returns>
        public bool ValidaCuentaEnCuarentena(string numCuenta)
        {
            var res = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select id " +
                                  "from CuarentenaCuentas " +
                                  "where numCuenta = @numCuenta and fechaSalida is null",
                    CommandType = CommandType.Text,
                    Connection = _conn,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var cuarentenaTable = new DataTable();
                cuarentenaTable.Load(dr);
                if (cuarentenaTable.Rows.Count > 0)
                {
                    res = true;
                }

            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al validar cuenta en cuarentena: " + e);
                res = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        /// <summary>
        /// Respalda y elimina un registro de accesso_clientes por usuario
        /// </summary>
        /// <param name="idUsuarioOnlineBroxel">id de usuario en UsuariosOnlineBroxel</param>
        public void RespaldaAccesosClientes(int idUsuarioOnlineBroxel)
        {
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idUsuarioOnlineBroxel", MySqlDbType.Int32) { Value = idUsuarioOnlineBroxel }
                };

                var sql = "insert into accessos_clientesbk " +
                          "select * from accessos_clientes where idUsuarioOnlineBroxel = @idUsuarioOnlineBroxel; " +
                          "delete from accessos_clientes where idUsuarioOnlineBroxel = @idUsuarioOnlineBroxel;";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al Respaldar accesos_clientes:" + e);
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
        }
        /// <summary>
        /// Inserta en la tabla CuarentenaCuentas
        /// </summary>
        /// <param name="cuarentena">Datos de la cuarentena</param>
        /// <returns>Id del registro insertado</returns>
        public long InsertaEnCuarentena(CuarentenaNivelesData cuarentena)
        {
            long id = 0;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@NumCuenta", MySqlDbType.VarChar) { Value = cuarentena.NumCuenta },
                    new MySqlParameter("@NivelIngreso", MySqlDbType.Int32) { Value = cuarentena.NivelIngreso },
                    new MySqlParameter("@UsuarioIngreso", MySqlDbType.VarChar) { Value = cuarentena.UsuarioIngreso }
                };

                var sql = "insert into CuarentenaCuentas (numCuenta, nivelIngreso, fechaIngreso, usuarioIngreso) " +
                          "values (@NumCuenta, @NivelIngreso, now(), @UsuarioIngreso);";

                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                id = cmd.LastInsertedId;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al insertan en CuarentenaCuentas:" + e );
                id = 0;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return id;
        }
        /// <summary>
        /// Saca una cuenta de la cuarentena y la envia al siguiente nivel 
        /// </summary>
        /// <param name="numCuenta">Numero de cuenta</param>
        /// <param name="usuario">Usuario que solicita</param>
        /// <returns></returns>
        public int SacaDeCuarentena(string numCuenta, string usuario)
        {
            var id = 0;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@usuario", MySqlDbType.VarChar) { Value = (string.IsNullOrEmpty(usuario)?"NO IDENTIFICADO":usuario) },
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var sql = "update CuarentenaCuentas " +
                          "set fechaSalida = now(), usuarioSalida = @usuario " +
                          "where numCuenta = @numCuenta; " +
                          "update maquila m " +
                          "set m.IdNivelDeCuenta = ifnull((select min(id) from CatNivelDeCuenta where id > m.IdNivelDeCuenta and activo = 1),m.IdNivelDeCuenta) " +
                          "where m.num_cuenta = @numCuenta;";

                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                id = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al sacar de CuarentenaCuentas:" + e);
                id = 0;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return id;
        }

        /// <summary>
        /// Valida si una cuenta debe entrar en cuarentena
        /// </summary>
        /// <param name="numCuenta">Numero de cuenta</param>
        /// <param name="limiteCompra">Limite actual de compra</param>
        /// <param name="usuario">Usuario que opera la solicitud</param>
        /// <param name="idNivel">Id de limite necesario para el envio de correo y mail</param>
        /// <returns>Objeto de cuarentena</returns>
        public CuarentenaNivelesData ValidaCuarentena(string numCuenta, decimal limiteCompra, string usuario, ref int idNivel)
        {
            CuarentenaNivelesData res = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                string clientes = ConfigurationManager.AppSettings["ClientesCuarentena"];
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@clientes", MySqlDbType.VarChar) { Value = clientes },
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select c.monto, m.IdNivelDeCuenta " +
                                  "from maquila m join CatNivelDeCuenta c on m.IdNivelDeCuenta = c.id " +
                                  "where (@clientes) and c.activo = 1 and m.num_cuenta = @numCuenta",
                    Connection = _conn,
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var nivelesTable = new DataTable();
                nivelesTable.Load(dr);
                if (nivelesTable.Rows.Count > 0)
                {
                    idNivel = Convert.ToInt32(nivelesTable.Rows[0]["IdNivelDeCuenta"].ToString());
                    var limiteNivel = Convert.ToDecimal(nivelesTable.Rows[0]["monto"].ToString());

                    if (limiteCompra > limiteNivel)
                    {
                        res = new CuarentenaNivelesData
                        {
                            NivelIngreso = idNivel,
                            NumCuenta = numCuenta,
                            UsuarioIngreso = usuario
                        };

                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al obtener niveles de cuenta: " + e);
                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        /// <summary>
        /// Valida nivel de cuenta para transferencia
        /// </summary>
        /// <param name="numCuenta">Numero de cuenta</param>
        /// <returns>Objeto de cuarentena</returns>
        public bool ValidaNivelCuarentenaDisposicion(string numCuenta)
        {
            bool res = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                string clientes = ConfigurationManager.AppSettings["ClientesCuarentena"];
                _conn.Open();

               var parameters = new MySqlParameter[]
               {
                    new MySqlParameter("@clientes", MySqlDbType.VarChar) { Value = clientes },
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
               };

                var cmd = new MySqlCommand
                {
                    CommandText = "select id " +
                                  "from maquila m " +
                                  "where (@clientes) and m.IdNivelDeCuenta = 1 and m.num_cuenta = @numCuenta",
                    Connection = _conn,
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var nivelesTable = new DataTable();
                nivelesTable.Load(dr);
                if (nivelesTable.Rows.Count > 0)
                {
                    res = true;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al obtener niveles de cuenta: " + e);
                res = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        /// <summary>
        /// Valida nivel de cuenta para transferencia
        /// </summary>
        /// <param name="numCuenta">Numero de cuenta</param>
        /// <returns>Objeto de cuarentena</returns>
        public bool EsB2C(string numCuenta)
        {
            bool res = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                string clientes = ConfigurationManager.AppSettings["ClientesCuarentena"];
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@clientes", MySqlDbType.VarChar) { Value = clientes },
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select id " +
                                  "from maquila m " +
                                  "where (@clientes) and m.num_cuenta = @numCuenta",
                    Connection = _conn,
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var nivelesTable = new DataTable();
                nivelesTable.Load(dr);
                if (nivelesTable.Rows.Count > 0)
                {
                    res = true;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al obtener si la cuenta es B2C: " + e);
                res = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        /// <summary>
        /// Obtiene los avisos de nivel de cuenta.
        /// </summary>
        /// <param name="idNivel">Identificador de nivel.</param>
        /// <param name="numCuenta">Número de cuenta del cliente.</param>
        /// <returns></returns>
        public List<NivelDeCuentaAvisos> ObtieneNivelDeCuentaAvisos(int idNivel, string numCuenta)
        {
            var res = new List<NivelDeCuentaAvisos>();
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idNivel", MySqlDbType.Int32) { Value = idNivel },
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select a.id IdAviso, a.IdCatNivel,a.Porcentaje,c.monto,a.SMSBody,a.DeMail,a.Asunto,a.DePwd,a.DeAlias,a.MailBody " +
                    "from CatNivelDeCuentaAvisos a join CatNivelDeCuenta c on a.IdCatNivel = c.id " +
                    "where IdCatNivel = @idNivel and not exists(select * from LogMensagesAvisosNivelDeCuenta l where l.IdCatNivelAviso = a.Id and l.NumCuenta = @numCuenta) " +
                    "order by Porcentaje asc; ",
                    Connection = _conn,
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var nivelesTable = new DataTable();
                nivelesTable.Load(dr);
                if (nivelesTable.Rows.Count > 0)
                {
                    for (var i = 0; i < nivelesTable.Rows.Count; i++)
                    {
                        var aviso = new NivelDeCuentaAvisos
                        {
                            IdAviso = Convert.ToInt32(nivelesTable.Rows[i]["IdAviso"].ToString()),
                            IdCatNivel = Convert.ToInt32(nivelesTable.Rows[i]["IdCatNivel"].ToString()),
                            Porcentaje = Convert.ToDecimal(nivelesTable.Rows[i]["Porcentaje"].ToString()),
                            Limite = Convert.ToDecimal(nivelesTable.Rows[i]["monto"].ToString()),
                            SMSBody = nivelesTable.Rows[i]["SMSBody"].ToString(),
                            DeMail = nivelesTable.Rows[i]["DeMail"].ToString(),
                            Asunto = nivelesTable.Rows[i]["Asunto"].ToString(),
                            DePwd = nivelesTable.Rows[i]["DePwd"].ToString(),
                            DeAlias = nivelesTable.Rows[i]["DeAlias"].ToString(),
                            MailBody = nivelesTable.Rows[i]["MailBody"].ToString()
                        };
                        res.Add(aviso);
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al obtener avisos de niveles de cuenta: " + e);
                res = new List<NivelDeCuentaAvisos>();
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        /// <summary>
        /// Guarda el registro del envio de avisos de niveles de cuenta.
        /// </summary>
        /// <param name="celular">celular a cual se le envio el mensaje.</param>
        /// <param name="numCuenta">numero de cuenta del cliente.</param>
        /// <param name="msg">mensaje enviado al cliente.</param>
        /// <param name="monto">monto total.</param>
        /// <param name="idAviso">identificador del aviso.</param>
        /// <returns></returns>
        public long InsertLogMensajeAvisoNivel(string celular, string numCuenta, string msg, decimal monto, int idAviso )
        {
            long id = 0;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@celular", MySqlDbType.VarChar) { Value = celular },
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta },
                    new MySqlParameter("@msg", MySqlDbType.VarChar) { Value = msg },
                    new MySqlParameter("@monto", MySqlDbType.VarChar) { Value = monto },
                    new MySqlParameter("@idAviso", MySqlDbType.Int32) { Value = idAviso }
                };

                var sql = "insert into LogMensagesAvisosNivelDeCuenta (Date, Celular, NumCuenta, Mensaje, Monto,IdCatNivelAviso ) " +
                          "values (now(), @celular, @numCuenta, @msg, @monto, @idAviso);";

                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                id = cmd.LastInsertedId;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al insertan en LogMensagesAvisosNivelDeCuenta:" + e);
                id = 0;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return id;
        }

        /// <summary>
        /// Obtiene los datos de las cuentas favoritas asociadas al usuario Broxel Online
        /// </summary>
        /// <param name="idUsuario">id de Usuario Broxel Online</param>
        /// <returns></returns>
        public List<Favorito> GetFavoritos(int idUsuario)
        {
            List<Favorito> favoritos = new List<Favorito>();
            try
            {
                var idSecComp = new IdSecureComp();
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idUsuario", MySqlDbType.Int32) { Value = idUsuario }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select a.id, a.cuentaFavorita cuenta, b.`nro-tarjeta` Tarjeta, a.Alias, b.producto, c.descripcion " +
                                  "from OnLineFavoritos a join maquila b on a.cuentaFavorita = b.num_cuenta " +
                                  "join productos_broxel c on b.producto = c.codigo " +
                                  "where a.idUser = @idUsuario and status = 1",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var favoritosTable = new DataTable();
                favoritosTable.Load(dr);
                if (favoritosTable.Rows.Count > 0)
                {
                    favoritos = (from DataRow row in favoritosTable.Rows
                                 select new Favorito
                                 {
                                     IdFavorito = Convert.ToInt32(row["id"].ToString()),
                                     NumCuenta = idSecComp.GetIdSecure(Convert.ToInt32(row["id"].ToString())).ToString(CultureInfo.InvariantCulture),
                                     Tarjeta = row["Tarjeta"].ToString(),
                                     Alias = string.IsNullOrEmpty(row["Alias"].ToString()) ? "" : row["Alias"].ToString(),
                                     Producto = row["producto"].ToString(),
                                     ProductoDesc = row["descripcion"].ToString()
                                 }).ToList();

                } 
            }
            catch (Exception)
            {
                favoritos = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return favoritos;

        }
        /// <summary>
        /// Obtiene el tipo de producto para una cuenta
        /// </summary>
        /// <param name="cuenta">Numero de cuenta</param>
        /// <returns>Tipo de producto, expenses o credito</returns>
        public string GetTipo(string cuenta)
        {
            string res = "";
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@cuenta", MySqlDbType.VarChar) { Value = cuenta }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select b.tipo from maquila a join productos_broxel b on a.producto = b.codigo where a.num_cuenta = @cuenta;",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);
                
                var dr = cmd.ExecuteReader();
                var rdTable = new DataTable();
                rdTable.Load(dr);
                if (rdTable.Rows.Count > 0)
                {
                    res = rdTable.Rows[0]["tipo"].ToString();
                }
            }
            catch (Exception)
            {
                res = "";
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return res;

        }

        /// <summary>
        /// Obtiene la clave de cliente de la maquila
        /// </summary>
        /// <param name="idMaquila">id de la tabla maquila</param>
        /// <returns>Tipo de producto, expenses o credito</returns>
        public string GetClaveCliente(int idMaquila)
        {
            string res = "";
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idMaquila", MySqlDbType.Int32) { Value = idMaquila }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select clave_Cliente from maquila where id = @idMaquila;",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var rdTable = new DataTable();
                rdTable.Load(dr);
                if (rdTable.Rows.Count > 0)
                {
                    res = rdTable.Rows[0]["clave_cliente"].ToString();
                }
            }
            catch (Exception)
            {
                res = "";
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return res;

        }
        /// <summary>
        /// Inserta en log de cambios de contraseña
        /// </summary>
        /// <param name="idUser">idUsuario</param>
        /// <param name="original">vieja contraseña</param>
        /// <param name="nueva">nueva contraseña</param>
        /// <returns>id de historial</returns>
        public long InsertCambiaContrasenaLog(int idUser, string original, string nueva)
        {
            long id = 0;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idUser", MySqlDbType.Int32) { Value = idUser },
                    new MySqlParameter("@original", MySqlDbType.VarChar) { Value = original },
                    new MySqlParameter("@nueva", MySqlDbType.Int16) { Value = nueva }
                };

                var sql = "insert into CambiaContrasenaLog (idUser, original, new) " +
                          "values (@idUser,@original, @nueva);";

                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                id = cmd.LastInsertedId;

            }
            catch (Exception)
            {
                id = 0;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return id;
        }


        #endregion
        #region Transferencias
        /// <summary>
        /// Valida si la cuenta pertenece a UberCard
        /// </summary>
        /// <param name="numCuenta">Numero de cuenta de la tarjeta</param>
        /// <returns>true si pertenece, false si no pertenece</returns>
        public bool ValidateUberCard(string numCuenta)
        {
            var resultado = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                string sql = "select m.id " +
                             "from maquila m join ParametrosServicio p on m.clave_cliente = p.valor " +
                             "where p.descripcion = 'Cliente UBER' and m.num_cuenta = @numCuenta;";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tableResumen = new DataTable();
                tableResumen.Load(dr);
                if (tableResumen.Rows.Count > 0)
                    resultado = true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                resultado = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return resultado;

        }

        public DateTime GetFechaLimite(string numCuenta)
        {
            DateTime resultado;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                string sql = "select date_format(min(a.fecha_vto),'%Y/%m/%d') fechaLimite from fechas_corte_x_grupos a join registro_de_cuenta b on a.grupo = b.grupo_de_liquidacion where b.numero_de_cuenta = @numCuenta and a.fecha > date(now())";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tableResumen = new DataTable();
                tableResumen.Load(dr);
                if (tableResumen.Rows.Count > 0)
                {
                    var row = tableResumen.Rows[0];
                    resultado = DateTime.ParseExact(row["fechaLimite"].ToString(), "yyyy/MM/dd", null);
                }
                else
                {
                    resultado = default(DateTime);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                resultado = default(DateTime);
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return resultado;

        }
        #endregion
        #region FiltroPagoTransferencias
        /// <summary>
        /// Obtiene pagos o transferencias
        /// </summary>
        /// <returns>Listado de pagos o tranferencias</returns>
        public List<MovimientoOnline> GetPagosTransferencias(string numCuenta, DateTime fechaIni, DateTime fechaFin, int tipo)
        {
            List<MovimientoOnline> pagoTransferencias = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta },
                    new MySqlParameter("@fechaIni", MySqlDbType.Date) { Value = fechaIni.ToString("yyyy-MM-dd") },
                    new MySqlParameter("@fechaFin", MySqlDbType.Date) { Value = fechaFin.ToString("yyyy-MM-dd") },
                    new MySqlParameter("@tipo", MySqlDbType.Int32) { Value = tipo }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "call spObtenPagosTransferencias(@numCuenta, @fechaIni, @fechaFin, tipo);",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);
                
                var dr = cmd.ExecuteReader();
                var ptTable = new DataTable();
                ptTable.Load(dr);
                if (ptTable.Rows.Count > 0)
                {
                    IFormatProvider cultureMx = new System.Globalization.CultureInfo("es-MX", true);
                    pagoTransferencias = (from DataRow row in ptTable.Rows
                                          select new MovimientoOnline
                                          {
                                              Cargo = Convert.ToDecimal(row["ImpTotal"].ToString()),
                                              Descripcion = row["DenMov"].ToString(),
                                              Fecha = DateTime.ParseExact(row["Origen"].ToString(), "yyyy-MM-dd", null),
                                              ImpOriginal = row["ImpOriginal"].ToString(),
                                              MonedaOriginal = row["CodMont"].ToString(),
                                              NumAutorizacion = row["NroAut"].ToString(),
                                              NumTarjeta = row["NroTar"].ToString(),
                                              Referencia = tipo == 3 ? row["DenMovAdl"].ToString() : ""
                                          }).ToList();
                }
            }
            catch (Exception)
            {
                pagoTransferencias = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return pagoTransferencias;

        }
        #endregion
        #region Preautorizador
        /// <summary>
        /// Obtiene pagos o transferencias
        /// </summary>
        /// <returns>Listado de pagos o tranferencias</returns>
        public List<GruposPreAut> GetGruposPreAut()
        {
            List<GruposPreAut> grupos = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                var cmd = new MySqlCommand
                {
                    CommandText = "select idGrupo, nombre as descripcion from cat_grupos_preautorizador where visibleMobile = 1",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };
                var dr = cmd.ExecuteReader();
                var ptTable = new DataTable();
                ptTable.Load(dr);
                if (ptTable.Rows.Count > 0)
                {
                    grupos = (from DataRow row in ptTable.Rows
                              select new GruposPreAut
                              {
                                  IdGrupo = Convert.ToInt32(row["idGrupo"].ToString()),
                                  Descripcion = row["descripcion"].ToString(),
                              }).ToList();
                }
            }
            catch (Exception)
            {
                grupos = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return grupos;

        }

        #endregion
        #region TarjetaTemporal
        /// <summary>
        /// Inserta en log de consulta de tarjetas
        /// </summary>
        /// <param name="idApp">idApp que solicita la consulta</param>
        /// <param name="usuario">usuario que solicita la consulta</param>
        /// <param name="token">token asociado al usuario</param>
        /// <returns></returns>
        public long InsertVcConsulta(int idApp, string usuario, string token)
        {
            long id = 0;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idApp", MySqlDbType.Int32) { Value = idApp },
                    new MySqlParameter("@usuario", MySqlDbType.VarChar) { Value = usuario },
                    new MySqlParameter("@token", MySqlDbType.VarChar) { Value = token }
                };

                var sql = "insert into LogConsultasVC (idApp, usuario, token) values ( @idApp, @usuario,@token);";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                id = cmd.LastInsertedId;

            }
            catch (MySqlException)
            {
                id = 0;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return id;
        }

        /// <summary>
        /// Actualiza la cuenta consultada 
        /// </summary>
        /// <param name="id">id de registro de consulta</param>
        /// <param name="cuenta">cuenta consultada</param>
        /// <returns></returns>
        public bool SetCuentaVcConsulta(long id, string cuenta)
        {
            var val = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@id", MySqlDbType.Int32) { Value = id },
                    new MySqlParameter("@cuenta", MySqlDbType.VarChar) { Value = cuenta }
                };

                var sql = "update LogConsultasVC set cuentaResult = @cuenta, fechaRespuesta = now() where id = @id;";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                    val = true;
            }
            catch (MySqlException)
            {
                val = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return val;
        }

        /// <summary>
        /// Obtiene id de maquila libre del pull
        /// </summary>
        /// <param name="idApp"></param>
        /// <param name="producto"></param>
        /// <returns></returns>
        public MaquilaVcInfo GetIdMaquilaFromPull(int idApp, string producto)
        {


            MaquilaVcInfo res = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idApp", MySqlDbType.Int32) { Value = idApp },
                    new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select m.id, m.num_cuenta " +
                                  "from maquila m join clienteBroxelLayout cbl on m.clave_cliente = cbl.pull and m.producto = cbl.producto " +
                                  "left join accessos_clientes ac on m.id = ac.idMaquila " +
                                  "where cbl.idApp = @idApp and cbl.producto = @producto and length(num_cuenta)=9 and ac.id is null order by maquila desc; ",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var rdTable = new DataTable();
                rdTable.Load(dr);
                if (rdTable.Rows.Count > 0)
                {
                    res = new MaquilaVcInfo
                    {
                        IdMaquila = Convert.ToInt32(rdTable.Rows[0]["id"].ToString()),
                        NumCuenta = rdTable.Rows[0]["num_cuenta"].ToString()
                    };
                }


            }
            catch (Exception e)
            {

                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return res;

        }

        /// <summary>
        /// Obtiene id de maquila libre del pull para AON
        /// </summary>
        /// <param name="idApp"></param>
        /// <param name="producto"></param>
        /// <returns></returns>
        public MaquilaVcInfo GetIdMaquilaAonFromPull(int idApp, string producto)
        {
            MaquilaVcInfo res = null;

            Trace.WriteLine("Entra a verificar que haya tarjetas en el pull");

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idApp", MySqlDbType.Int32) { Value = idApp },
                    new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select c.id, c.num_cuenta from (select m.id, m.num_cuenta, m.clave_cliente from maquila m " +
                   "join clienteBroxelLayout cbl on m.clave_cliente = cbl.pull and m.producto = cbl.producto " +
                   "left join accessos_clientes ac on m.id = ac.idMaquila where cbl.idApp = @idApp and cbl.producto =@producto and length(num_cuenta)=9 and ac.id is null ) as c" +
                   " left join CreaClienteSinTarjetaLog cst on c.num_cuenta = cst.cuenta where cst.id is null order by c.id desc",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var rdTable = new DataTable();
                rdTable.Load(dr);
                if (rdTable.Rows.Count > 0)
                {
                    res = new MaquilaVcInfo
                    {
                        IdMaquila = Convert.ToInt32(rdTable.Rows[0]["id"].ToString()),
                        NumCuenta = rdTable.Rows[0]["num_cuenta"].ToString()
                    };
                }

                Trace.WriteLine("Obteniendo una tarjeta del pull: " + res);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al obtener el pull de tarjetas: " + e.Message);
                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return res;

        }

        /// <summary>
        /// Creac cliente broxel
        /// </summary>
        /// <param name="idClienteLog"></param>
        /// <param name="producto"></param>
        /// <param name="idMaquila"></param>
        /// <param name="esFisica"></param>
        /// <returns></returns>
        public bool CreaClienteBroxel(int idClienteLog, string producto, int idMaquila, bool esFisica)
        {
            bool result;

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idClienteLog", MySqlDbType.Int32) { Value = idClienteLog },
                    new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto },
                    new MySqlParameter("@idMaquila", MySqlDbType.Int32) { Value = idMaquila },
                    new MySqlParameter("@esFisica", MySqlDbType.Int16) { Value = (esFisica ? 1 : 0) }
                };

                var cmd = new MySqlCommand();
                cmd.Connection = _conn;
                cmd.CommandText = "call spCreaClienteBroxel(@idClienteLog, @producto, @idMaquila, @esFisica);";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error Catch CreaClienteBroxel: " + ex.Message);
                result = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return result;
        }

        /// <summary>
        /// Creac cliente broxel
        /// </summary>
        /// <param name="producto"></param>
        /// <param name="idMaquila"></param>
        /// <param name="pNombre"></param>
        /// <param name="sNombre"></param>
        /// <param name="aPaterno"></param>
        /// <param name="aMaterno"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="rfc"></param>
        /// <param name="colonia"></param>
        /// <param name="numeroExt"></param>
        /// <param name="calle"></param>
        /// <param name="numeroInt"></param>
        /// <param name="telefono"></param>
        /// <param name="usuario"></param>
        /// <param name="noEmpleado"></param>
        /// <param name="delegacionMunicipio"></param>
        /// <param name="estado"></param>
        /// <param name="codigoPostal"></param>
        /// 
        /// <returns></returns>
        public bool CreaClienteBroxelB2C(string producto, int idMaquila, string pNombre, string sNombre,
            string aPaterno,
            string aMaterno,
            DateTime fechaNacimiento,
            string rfc,
            string colonia,
            string calle,
            string numeroExt,
            string numeroInt,
            string telefono,
            string usuario,
            string noEmpleado,
            string delegacionMunicipio,
            string estado, string codigoPostal)
        {
            bool result;

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto },
                    new MySqlParameter("@idMaquila", MySqlDbType.Int32) { Value = idMaquila },
                    new MySqlParameter("@pNombre", MySqlDbType.VarChar) { Value = pNombre },
                    new MySqlParameter("@sNombre", MySqlDbType.VarChar) { Value = sNombre },
                    new MySqlParameter("@aPaterno", MySqlDbType.VarChar) { Value = aPaterno },
                    new MySqlParameter("@aMaterno", MySqlDbType.VarChar) { Value = aMaterno },
                    new MySqlParameter("@fechaNacimiento", MySqlDbType.Date) { Value = fechaNacimiento.ToString("yyyy-MM-dd") },
                    new MySqlParameter("@rfc", MySqlDbType.VarChar) { Value = rfc },
                    new MySqlParameter("@colonia", MySqlDbType.VarChar) { Value = colonia },
                    new MySqlParameter("@calle", MySqlDbType.VarChar) { Value = calle },
                    new MySqlParameter("@numeroExt", MySqlDbType.VarChar) { Value = numeroExt },
                    new MySqlParameter("@numeroInt", MySqlDbType.VarChar) { Value = numeroInt },
                    new MySqlParameter("@telefono", MySqlDbType.VarChar) { Value = telefono },
                    new MySqlParameter("@usuario", MySqlDbType.VarChar) { Value = usuario },
                    new MySqlParameter("@noEmpleado", MySqlDbType.VarChar) { Value = noEmpleado },
                    new MySqlParameter("@delegacionMunicipio", MySqlDbType.VarChar) { Value = delegacionMunicipio },
                    new MySqlParameter("@estado", MySqlDbType.VarChar) { Value = estado },
                    new MySqlParameter("@codigoPostal", MySqlDbType.VarChar) { Value = codigoPostal }
                };

                var cmd = new MySqlCommand();
                cmd.Connection = _conn;
                cmd.CommandText = "call spCreaClienteBroxelTarFisica(" +
                                  "@producto,"+
                                  " @idMaquila," +
                                  "@pNombre," +
                                  "@sNombre," +
                                  "@aPaterno," +
                                  "@aMaterno," +
                                  "@fechaNacimiento," +
                                  "@rfc," +
                                  "@colonia," +
                                  "@calle," +
                                  "@numeroExt," +
                                  "@numeroInt," +
                                  "@telefono," +
                                  "@usuario," +
                                  "@noEmpleado," +
                                  "@delegacionMunicipio," +
                                  "@estado," +
                                  "@codigoPostal)";
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1200;

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();                
                result = true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error Catch CreaClienteBroxel: " + ex.Message);
                result = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return result;
        }

        /// <summary>
        /// Crea cliente para comercio
        /// </summary>
        /// <param name="idComercio">Identificador del comercio en tabla Comercio</param>
        /// <param name="idLayout">Identificador del layout de cliente a emplear</param>
        /// <param name="producto">producto de la cuenta</param>
        /// <param name="idMaquila">Identificador de la maquila</param>
        /// <returns></returns>
        public bool CreaClienteComercioBroxel(int idComercio,int idLayout, string producto, int idMaquila)
        {
            bool result;

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idComercio", MySqlDbType.Int32) { Value = idComercio },
                    new MySqlParameter("@idLayout", MySqlDbType.Int32) { Value = idLayout },
                    new MySqlParameter("@producto", MySqlDbType.VarChar) { Value = producto },
                    new MySqlParameter("@idMaquila", MySqlDbType.Int32) { Value = idMaquila }
                };

                var cmd = new MySqlCommand
                {
                    Connection = _conn,
                    CommandText = "call spCreaClienteComercio(@idComercio,@idLayout,@producto,@idMaquila);",
                    CommandType = CommandType.Text,
                    CommandTimeout = 1200
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al crear cliente broxel para comercio " + idComercio + ": " + e);
                result = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCreaLog"></param>
        /// <param name="idMaquila"></param>
        /// <param name="producto"></param>
        /// <returns></returns>
        public bool ConectaMaquila(int idCreaLog, int idMaquila, string producto)
        {
            var val = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idCreaLog", MySqlDbType.Int32) { Value = idCreaLog },
                    new MySqlParameter("@idMaquila", MySqlDbType.Int32) { Value = idMaquila },
                    new MySqlParameter("@producto", MySqlDbType.Int16) { Value = producto }
                };

                var sql = "update maquila m join " +
                          "(select @producto as producto, claveCliente, @idMaquila as idMaquila from CreaClienteSinTarjetaLog where id = @idCreaLog) b " +
                          "on m.producto = b.producto and m.id = b.idMaquila " +
                          "set m.clave_cliente = b.claveCliente;";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                    val = true;
            }
            catch (MySqlException)
            {
                val = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return val;
        }

        /// <summary>
        /// Obtiene usuario en base a idUsuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public VcTokenData GetUsuario(int idUsuario)
        {
            VcTokenData res = null;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idUsuario", MySqlDbType.Int32) { Value = idUsuario }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = "select usuario, celular from UsuariosOnlineBroxel where id = @idUsuario; ",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var rdTable = new DataTable();
                rdTable.Load(dr);
                if (rdTable.Rows.Count > 0)
                {
                    res = new VcTokenData
                    {
                        Usuario = rdTable.Rows[0]["usuario"].ToString(),
                        Celular = rdTable.Rows[0]["celular"].ToString().StartsWith("044")
                        ? rdTable.Rows[0]["celular"].ToString().Substring(3, rdTable.Rows[0]["celular"].ToString().Length - 3).Replace(" ", "")
                        : rdTable.Rows[0]["celular"].ToString().Replace(" ", "")
                    };
                }
            }
            catch (Exception)
            {
                res = null;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return res;
        }

        #endregion
        #region Mejoravit

        /// <summary>
        /// Actualiza Ultimo Re
        /// </summary>
        /// <param name="idComercio"></param>
        public void UpdateCambioClabe(int idComercio)
        {
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@idComercio", MySqlDbType.Int32) { Value = idComercio }
                };

                var sql = "update CambioClabe set estatus = 1 where idComercio = @idComercio";

                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al actualizar CambioClabe: " + e);
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
        }

        /// <summary>
        /// Limpia CreaClientesSinTarjetaLog para no tener problemas de no poder visualizar las cuentas
        /// </summary>
        /// <param name="user">usuario</param>
        public void ClearCreaClientesSinTarjetaLog(string user)
        {
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@user", MySqlDbType.VarChar) { Value = user }
                };

                var sql = "insert into CreaClienteSinTarjetaLogBk " +
                          "select * from CreaClienteSinTarjetaLog where usuario = @user;" +
                          "delete from CreaClienteSinTarjetaLog where usuario = @user;";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al limpiar creaclientesintarjetalog: " + e);
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
        }

        /// <summary>
        /// Retorna el monto de porcentaje disponible para transferencia
        /// </summary>
        /// <param name="numCuenta">numero de cuenta</param>
        /// <returns></returns>
        public Decimal MontoPorcentaje(string numCuenta)
        {
            Decimal data = 0;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var sql = "select PorcentajeDisposicion " +
                          "from Programas pg join maquila m on pg.idCliente = m.clave_Cliente and pg.idProducto = m.Producto " +
                          "where idProgramas in (1,5) and m.num_cuenta = @numCuenta";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var rdTable = new DataTable();
                rdTable.Load(dr);
                if (rdTable.Rows.Count > 0)
                {
                    data = Convert.ToDecimal(rdTable.Rows[0]["PorcentajeDisposicion"].ToString());
                }
            }
            catch (Exception)
            {
                data = 0;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return data;
        }

        /// <summary>
        /// Valida si la cuenta tiene disposiciones de efectivo exitosas
        /// </summary>
        /// <param name="numCuenta">Numero de cuenta</param>
        /// <returns></returns>
        public bool ValidaDisposicionesEfectivo(string numCuenta)
        {
            var data = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta }
                };

                var sql = "select id from DisposicionesEfectivo where numCuenta = @numCuenta and statusPago in (1,2)";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var rdTable = new DataTable();
                rdTable.Load(dr);
                if (rdTable.Rows.Count > 0)
                {
                    data = true;
                }
            }
            catch (Exception)
            {
                data = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return data;
        }

        /// <summary>
        /// Valida si el producto pertenece al programa mejoravit y bloquea  las transferencias.
        /// </summary>
        /// <param name="numeroCuenta">numero cuenta origen</param>
        /// <param name="numeroCuentaDestino">numero de cuenta destino</param>
        /// <returns>true si pertenece al programa</returns>
        public bool ValidaProductoMejoravit(string numeroCuenta, string numeroCuentaDestino = "")
        {

            var resultado = false;

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numeroCuenta", MySqlDbType.VarChar) { Value = numeroCuenta },
                    new MySqlParameter("@numeroCuentaDestino", MySqlDbType.VarChar) { Value = numeroCuentaDestino }
                };

                string query = " select m.id from " +
                                           " maquila m join Programas p on m.producto = p.idProducto and m.clave_cliente = p.idCliente " +
                                           " where p.idProgramas = '5' and m.num_cuenta in(@numeroCuenta,@numeroCuentaDestino);";

                var cmd = new MySqlCommand
                {
                    CommandText = query,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tabla = new DataTable();
                tabla.Load(dr);

                if (tabla.Rows.Count > 0)
                    resultado = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("{0:dd/MM/yyyy HH:mm:ss.fff} Error en ValidaProductoMejoravit :  {1}", DateTime.Now, ex.Message));
                resultado = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return resultado;
        }


        /// <summary>
        /// Valida si la dispersion es de Mejoravit
        /// </summary>
        /// <param name="folio">Folio de solicitud</param>
        /// <returns>true si pertenece al programael </returns>
        public bool ValidaDispersionMejoravit(string folio)
        {

            var resultado = false;

            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.VarChar) { Value = folio }
                };

                string query = "select id from ind_Originacion where folioDispersion = @folio;";

                var cmd = new MySqlCommand
                {
                    CommandText = query,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tabla = new DataTable();
                tabla.Load(dr);

                if (tabla.Rows.Count > 0)
                    resultado = true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("{0:dd/MM/yyyy HH:mm:ss.fff} Error en ValidaDispersionMejoravit :  {1}", DateTime.Now, ex.Message));
                resultado = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return resultado;
        }
        /// <summary>
        /// Actualiza el estado de las originaciones de la dispersion
        /// </summary>
        /// <param name="folio">folio de la dispersion</param>
        /// <returns></returns>
        public bool ActualizaIndOriginacionDispersion(string folio)
        {
            var val = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@folio", MySqlDbType.VarChar) { Value = folio }
                };

                var sql = "update ind_Originacion i join dispersionesInternas di on i.folioDispersion = di.idSolicitud and i.numeroCarrier = di.cuenta " +
                          "set i.status = case di.codigoRespuestaPOS when '-1' then 3 when '' then i.status when '983' then i.status else 93 end " +
                          "where di.idSolicitud = @folio";
                var cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var insert = cmd.ExecuteNonQuery();
                if (insert > 0)
                    val = true;
            }
            catch (Exception e)
            {
                Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Error en ActualizaIndOriginacionDispersion", "La actualizacion de estado fallo para el folio "+ folio + " :  " + e, "yMQ3E3ert6", "Broxel : Dispersión");
                val = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }

            return val;
        }

        /// <summary>
        /// Valida si una cuneta tiene asignado los productos: K174,K175,K151
        /// </summary>
        /// <param name="numeroCuenta">cuenta del usuario</param>
        /// <returns>true si tiene asignado algun producto</returns>
        public bool validarMerchant(string numeroCuenta)
        {
            var resultado = false;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                string productos = ConfigurationManager.AppSettings["Merchant"];

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numeroCuenta", MySqlDbType.VarChar) { Value = numeroCuenta },
                    new MySqlParameter("@productos", MySqlDbType.VarChar) { Value = productos }
                };

                string query = "select ac.usuario,m.num_cuenta, m.producto from maquila m" +
                                        " join accessos_clientes ac on m.num_cuenta = ac.cuenta" +
                                        " where m.producto in(@productos)" +
                                        " AND ac.cuenta = @numeroCuenta;";

                var cmd = new MySqlCommand
                {
                    CommandText = query,
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var tabla = new DataTable();
                tabla.Load(dr);

                if (tabla.Rows.Count > 0)
                    resultado = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("{0:dd/MM/yyyy HH:mm:ss.fff} Error en validarMerchant :  {1}", DateTime.Now, ex.Message));
                resultado = false;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return resultado;
        }

        #endregion
        #region BroxelFamily

        /// <summary>
        /// Obtiene el folio de family vinculado al id de usuario online de la cuenta padre en caso de que exista.
        /// </summary>
        /// <param name="idUsuarioOnline"></param>
        /// <returns></returns>
        public string ObtenerFolioExistente(int idUsuarioOnline)
        {
            string res = string.Empty;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();
                var cmd = new MySqlCommand
                {
                    CommandText = " select IdFolio from DireccionEnvioTarjetaFisica where IdUsuarioOnline = " + idUsuarioOnline +
                    " and IdFolio like '%FAM%' order by IdFolio desc limit 1; ",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };
                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    res = row["IdFolio"].ToString();
                }
            }
            catch (Exception)
            {
                res = string.Empty;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        /// <summary>
        /// Obtiene el ultimo folio asignado correspondiente al periodo solicitado.
        /// </summary>
        /// <param name="pref">prefijo del folio a buscar.</param>
        /// <returns></returns>
        public string ObtenerUltimoFolio(string pref)
        {
            string res = string.Empty;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@pref", MySqlDbType.VarChar) { Value = pref }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = " select IdFolio from DireccionEnvioTarjetaFisica where IdFolio like '%@pref%' order by IdFolio desc limit 1; ",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    res = row["IdFolio"].ToString();
                }
            }
            catch (Exception)
            {
                res = string.Empty;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        /// <summary>
        /// Obtine el nombre de cliente de broxel.
        /// </summary>
        /// <param name="numCuenta">Número de cuenta del cliente a buscar.</param>
        /// <param name="idUsuarioOnline">Id de Usuario Online.</param>
        /// <returns></returns>
        public string ObtenerNombreCliente(string numCuenta, int idUsuarioOnline)
        {
            string res = string.Empty;
            try
            {
                _conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLBroxel_RDG"].ToString());
                _conn.Open();

                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@numCuenta", MySqlDbType.VarChar) { Value = numCuenta },
                    new MySqlParameter("@idUsuarioOnline", MySqlDbType.Int32) { Value = idUsuarioOnline }
                };

                var cmd = new MySqlCommand
                {
                    CommandText = " select b.NombreCompleto from accessos_clientes a " +
                    "join UsuariosOnlineBroxel b on a.IdUsuarioOnlineBroxel = b.Id " +
                    "where a.cuenta = @numCuenta and b.Id = @idUsuarioOnline;",
                    Connection = _conn,
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddRange(parameters);

                var dr = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dr);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    res = row["NombreCompleto"].ToString();
                }
            }
            catch (Exception)
            {
                res = string.Empty;
            }
            finally
            {
                _conn.Close();
                _conn = null;
            }
            return res;
        }

        #endregion
    }
}