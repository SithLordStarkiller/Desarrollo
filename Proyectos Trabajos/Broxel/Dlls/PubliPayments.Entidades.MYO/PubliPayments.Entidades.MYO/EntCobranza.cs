using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades.MYO
{
    public class EntCobranza
    {
        /// <summary>
        /// Se encarga de llenar la tabla CarteraVencida con datos nuevos
        /// </summary>
        /// <param name="loanDocumentationRequestId">id del registro de la petición</param>
        /// <param name="numCredito"> Crédito de cliente</param>
        /// <param name="codCliente"> Código del cliente</param>
        /// <param name="codAgencia">Código de la agencia</param>
        /// <param name="codEmpresa">Código de la empresa</param>
        /// <param name="nomCliente"> Nombre del cliente</param>
        /// <param name="numPagosAtrasados">Numero de pagos atrasados</param>
        /// <param name="monDeduccion">Cantidad que tiene que pagar</param>
        /// <param name="diaUltPago">Día en que se debió de realizar ultimo pago</param>
        /// <param name="casa">Número de teléfono de casa</param>
        /// <param name="celular">Número de Celular</param>
        /// <param name="oficina">Número de teléfono de oficina</param>
        /// <param name="Email">Email</param>
        /// <param name="frecuencia">Frecuencia en que se deben de realizar los pagos</param>
        public void InsCarteraVencida(string loanDocumentationRequestId
                                        ,long numCredito
                                        , string codCliente
                                        , string codAgencia
                                        , string codEmpresa
                                        , string nomCliente
                                        , int? numPagosAtrasados
                                        , decimal? monDeduccion
                                        , DateTime? diaUltPago
                                        , string casa
                                        , string celular
                                        , string oficina
                                        , string email
                                        , string frecuencia
                                         )
        {

            try
            {
                var instancia = ConnectionDB.Instancia;

                var parametros = new SqlParameter[14];
                parametros[0] = new SqlParameter("@LoanDocumentationRequestId", SqlDbType.NVarChar,50) { Value = loanDocumentationRequestId };
                parametros[1] = new SqlParameter("@SAF_Credit_Number", SqlDbType.BigInt) { Value = numCredito };
                parametros[2] = new SqlParameter("@Cod_Cliente ", SqlDbType.VarChar, 50) { Value = codCliente };
                parametros[3] = new SqlParameter("@Cod_Agencia ", SqlDbType.VarChar, 50) { Value = codAgencia };
                parametros[4] = new SqlParameter("@Cod_Empresa ", SqlDbType.VarChar, 50) { Value = codEmpresa };
                parametros[5] = new SqlParameter("@Nom_Cliente ", SqlDbType.VarChar, 50) { Value = nomCliente };
                parametros[6] = new SqlParameter("@Num_Pagos_Atrasados", SqlDbType.Int) { Value = numPagosAtrasados };
                parametros[7] = new SqlParameter("@Mon_Deduccion  ", SqlDbType.Decimal) { Value = monDeduccion };
                parametros[8] = new SqlParameter("@Dia_Ult_Pago ", SqlDbType.DateTime) { Value = diaUltPago };
                parametros[9] = new SqlParameter("@Casa", SqlDbType.VarChar, 15) { Value = casa };
                parametros[10] = new SqlParameter("@Celular ", SqlDbType.VarChar, 15) { Value = celular };
                parametros[11] = new SqlParameter("@Oficina ", SqlDbType.VarChar, 15) { Value = oficina };
                parametros[12] = new SqlParameter("@email ", SqlDbType.VarChar, 100) { Value = email };
                parametros[13] = new SqlParameter("@Frecuencia", SqlDbType.VarChar, 50) { Value = frecuencia };

                instancia.EjecutarDataSet("SqlDefault", "InsCarteraVencida", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntCobranza", "Error: InsCarteraVencida: " + ex.Message);
            }
            
        }

        /// <summary>
        /// Obtiene el producto que se tiene relacionado a este crédito  (el producto que se contrato)
        /// </summary>
        /// <param name="numCredito">numero de crédito de SAF</param>
        /// <returns>información del producto</returns>
        public DataSet ObtenerProductoXCredito(long numCredito)
        {
            ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["FlockMYO"].ConnectionString);
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sc = new SqlCommand("ObtenerProductoXCredito", cnn) { CommandType = CommandType.StoredProcedure };
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@SAFCreditNumber", SqlDbType.BigInt) { Value = numCredito };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntLoan", "EntCobranza - Error: " + ex.Message);
            }
            finally
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            }
            return ds;
        }

        /// <summary>
        /// Se encarga de desactivar todos aquellos créditos que ya no tenga SAF como deudores
        /// </summary>
        /// <param name="creditos">Créditos que se mantienen deudores por parte de SAF</param>
        public void DesactivarCreditosPagados(string creditos)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@SAF_Credit_Number", SqlDbType.VarChar) { Value = creditos };
                instancia.EjecutarDataSet("SqlDefault", "DesactivarCreditosPagados", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntCobranza", "Error: DesactivarCreditosPagados: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los créditos que se mantienen sin pago
        /// </summary>
        /// <returns>Información de los créditos que se tiene como deudores </returns>
        public DataSet ObtenerCarteraVencida()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
              return  instancia.EjecutarDataSet("SqlDefault", "ObtenerCarteraVencida");
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntCobranza", "Error: ObtenerCarteraVencida: " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Se encarga de manejar la inserción de una nueva notificación dependiendo de las condiciones que esta presente
        /// </summary>
        /// <param name="idCarteraVencida">identificador de tabla carteraVencida</param>
        public void ProcesarAlertasXId(int idCarteraVencida)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idCarteraVencida", SqlDbType.VarChar) { Value = idCarteraVencida };
                instancia.EjecutarDataSet("SqlDefault", "ProcesarAlertasXId", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntCobranza", "Error: ProcesarAlertasXId: " + ex.Message);
            }   
        }

        /// <summary>
        /// Obtiene las alertas que no se han enviado
        /// </summary>
        /// <returns>DataSet con la información necesaria</returns>
        public DataSet ObtenerRegistroAlertas()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                return instancia.EjecutarDataSet("SqlDefault", "ObtenerRegistroAlertas");
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntCobranza", "Error: ObtenerRegistroAlertas: " + ex.Message);
            }
            return null;
        }

        public void ActualizaAlertaEnviada(int idRegistroAlerta, string respuestaEnvio)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
               var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@idRegistroAlerta", SqlDbType.Int) { Value = idRegistroAlerta };
                parametros[1] = new SqlParameter("@RespuestaEnvio", SqlDbType.VarChar,100) { Value = respuestaEnvio };
                instancia.EjecutarDataSet("SqlDefault", "ActualizaAlertaEnviada", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntCobranza", "Error: ActualizaAlertaEnviada: " + ex.Message);
            } 
        }
        
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuenta"></param>
        /// <param name="claveCliente"></param>
        /// <param name="producto"></param>
        /// <param name="nomCliente"></param>
        /// <param name="monto1"></param>
        /// <param name="monto2"></param>
        /// <param name="diaPago"></param>
        /// <param name="casa"></param>
        /// <param name="celular"></param>
        /// <param name="oficina"></param>
        /// <param name="email"></param>
        /// <param name="estado"></param>
        /// <param name="direccion"></param>
        public void InsCarteraPreventiva(string cuenta
                                        , string claveCliente
                                        , string producto
                                        , string nomCliente
                                        , float monto1
                                        , float monto2
                                        , DateTime diaPago
                                        , string casa
                                        , string celular
                                        , string oficina
                                        , string email
                                        , string estado
                                        , string direccion
                                         )
        {

            try
            {
                Trace.WriteLine(String.Format("{0} - EntCobranza: Procesando cuenta:{1},claveCliente:{2},producto:{3},nomCliente:{4}", DateTime.Now, cuenta, claveCliente, producto, nomCliente));

                var instancia = ConnectionDB.Instancia;

                var parametros = new SqlParameter[13];
                parametros[0] = new SqlParameter("@Cuenta", SqlDbType.VarChar, 50) { Value = cuenta };
                parametros[1] = new SqlParameter("@ClaveCliente", SqlDbType.VarChar, 50) { Value = claveCliente };
                parametros[2] = new SqlParameter("@Producto ", SqlDbType.VarChar, 50) { Value = producto };
                parametros[3] = new SqlParameter("@Nom_Cliente ", SqlDbType.VarChar, 50) { Value = nomCliente };
                parametros[4] = new SqlParameter("@Monto1 ", SqlDbType.Decimal ) { Value = monto1 };
                parametros[5] = new SqlParameter("@Monto2 ", SqlDbType.Decimal) { Value = monto2 };
                parametros[6] = new SqlParameter("@Dia_Pago", SqlDbType.DateTime) { Value = diaPago };
                parametros[7] = new SqlParameter("@Casa  ", SqlDbType.VarChar, 15) { Value = casa };
                parametros[8] = new SqlParameter("@Celular ", SqlDbType.VarChar, 15) { Value = celular };
                parametros[9] = new SqlParameter("@Oficina", SqlDbType.VarChar, 15) { Value = oficina };
                parametros[10] = new SqlParameter("@Email ", SqlDbType.VarChar, 100) { Value = email };
                parametros[11] = new SqlParameter("@Estado ", SqlDbType.VarChar, 50) { Value = estado };
                parametros[12] = new SqlParameter("@Direccion ", SqlDbType.VarChar, 500) { Value = direccion };
                

                instancia.EjecutarDataSet("SqlDefault", "InsCarteraPreventiva", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntCobranza", "Error: InsCarteraVencida: " + ex.Message);
            }

        }

        public DataSet ObtenerCarteraPreventiva()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                return instancia.EjecutarDataSet("SqlDefault", "ObtenerCarteraPreventiva");
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntCobranza", "Error: ObtenerCarteraPreventiva: " + ex.Message);
            }
            return null;
        }

        public void ProcesaAlertasPreventivas(int idCarteraPreventiva)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idCarteraPreventiva", SqlDbType.VarChar) { Value = idCarteraPreventiva };
                instancia.EjecutarDataSet("SqlDefault", "ProcesaAlertasPreventivas", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntCobranza", "Error: ProcesaAlertasPreventivas: " + ex.Message);
            }
        }
    }
}
