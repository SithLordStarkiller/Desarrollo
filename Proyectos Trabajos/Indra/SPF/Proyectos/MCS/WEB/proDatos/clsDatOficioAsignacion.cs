using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;

namespace SICOGUA.Datos
{
    public class clsDatOficioAsignacion
    {

        public static List<string> consultaDatosOficioAsignacion(int idInstalacion, int idServicio, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            List<string> lisResultado = new List<string>();
            string strResultado = "";
            DataSet dsDatos = new DataSet();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spuConsultarDatosServicioOficioAsignacion";
                dbComando.Parameters.Clear();

                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.Direction = ParameterDirection.Input;
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = idServicio;
                dbComando.Parameters.Add(dbpIdServicio);


                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsDatos);


                dbTrans.Commit();
                strResultado = dsDatos.Tables[0].Rows[0]["zonaServicio"].ToString();//0
                lisResultado.Add(strResultado);
                strResultado = dsDatos.Tables[0].Rows[0]["DGADO"].ToString();//1
                lisResultado.Add(strResultado);
                strResultado = dsDatos.Tables[0].Rows[0]["nombreServicio"].ToString();//2
                lisResultado.Add(strResultado);
                strResultado = dsDatos.Tables[0].Rows[0]["nombreInstalacion"].ToString();//3
                lisResultado.Add(strResultado);
                strResultado = dsDatos.Tables[0].Rows[0]["domicilioInstalacion"].ToString();//4
                lisResultado.Add(strResultado);
                strResultado = dsDatos.Tables[0].Rows[0]["dirGeneral"].ToString();//4
                lisResultado.Add(strResultado);

                return lisResultado;
            }
            catch (DbException dbEx)
            {

                throw dbEx;
            }

            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);

            }

        }
        public static List<clsEntEmpleado> consultaIntegranteOficioAsignacion(List<clsEntAsignacionMasiva> lisIntegrantes, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            //List<string> lisResultado = new List<string>();
            List<clsEntEmpleado> lisResultado = new List<clsEntEmpleado>();


            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();



            try
            {


                for (int i = 0; i < lisIntegrantes.Count; i++)
                {
                    DataSet dsDatos = new DataSet();
                    clsEntEmpleado datEmpleado = new clsEntEmpleado();

                    DbTransaction dbTrans = dbConexion.BeginTransaction();
                    dbComando.Connection = dbConexion;
                    dbComando.Transaction = dbTrans;
                    dbComando.CommandType = CommandType.StoredProcedure;
                    dbComando.CommandText = "empleado.spuConsultarDatosintegranteOficio";

                    dbComando.Parameters.Clear();

                    DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();
                    dbpIdEmpleado.DbType = DbType.Guid;
                    dbpIdEmpleado.Direction = ParameterDirection.Input;
                    dbpIdEmpleado.ParameterName = "@idEmpleado";
                    dbpIdEmpleado.Value = lisIntegrantes[i].idEmpleado;
                    dbComando.Parameters.Add(dbpIdEmpleado);

                    DbParameter dbpIdEmpleadoAsignacion = objConexion.dbProvider.CreateParameter();
                    dbpIdEmpleadoAsignacion.DbType = DbType.Int16;
                    dbpIdEmpleadoAsignacion.Direction = ParameterDirection.Input;
                    dbpIdEmpleadoAsignacion.ParameterName = "@idEmpleadoAsignacion";
                    dbpIdEmpleadoAsignacion.Value = lisIntegrantes[i].idEmpleadoAsignacion;
                    dbComando.Parameters.Add(dbpIdEmpleadoAsignacion);

                   
                    dbAdapter.SelectCommand = dbComando;

                    dbAdapter.Fill(dsDatos);

                    dbTrans.Commit();
                    datEmpleado.EmpPaterno = dsDatos.Tables[0].Rows[0]["empPaterno"].ToString();

                    datEmpleado.EmpMaterno = dsDatos.Tables[0].Rows[0]["empMaterno"].ToString();

                    datEmpleado.EmpNombre = dsDatos.Tables[0].Rows[0]["empNombre"].ToString();

                    datEmpleado.jerDescripcion = dsDatos.Tables[0].Rows[0]["jerDescripcion"].ToString();

                    datEmpleado.EmpNumero = Convert.ToInt32(dsDatos.Tables[0].Rows[0]["empNumero"]);

                    clsEntServicio servicio = new clsEntServicio() { idServicio = Convert.ToInt32(dsDatos.Tables[0].Rows[0]["idServicio"]) };
                    clsEntInstalacion instalacion = new clsEntInstalacion() { IdInstalacion = Convert.ToInt32(dsDatos.Tables[0].Rows[0]["idInstalacion"]) };

                    clsEntEmpleadoAsignacion empAsignacion = new clsEntEmpleadoAsignacion() { Servicio = servicio, Instalacion = instalacion };

                    datEmpleado.EmpleadoAsignacion2 = empAsignacion;

                    lisResultado.Add(datEmpleado);
                }


                return lisResultado;
            }
            catch (DbException dbEx)
            {

                throw dbEx;
            }

            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);

            }

        }
        public static List<string> consultaContenidoOficio(clsEntSesion objSesion)
        //public static string consultaContenidoOficio(int intTipo, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();

            List<string> strResultado = new List<string>();
            //string strResultado ="";
            DataSet dsDatos = new DataSet();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
               
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "catalogo.spuConsultarContenidoOficio";
                dbComando.Parameters.Clear();

               
                DbParameter dbpContenido = objConexion.dbProvider.CreateParameter();
                dbpContenido.DbType = DbType.String;
                dbpContenido.Direction = ParameterDirection.Output;
                dbpContenido.ParameterName = "@contenido";
                dbpContenido.Size = 20000;
                dbComando.Parameters.Add(dbpContenido);

                DbParameter dbpEncabezado = objConexion.dbProvider.CreateParameter();
                dbpEncabezado.DbType = DbType.String;
                dbpEncabezado.Direction = ParameterDirection.Output;
                dbpEncabezado.ParameterName = "@encabezado";
                dbpEncabezado.Size = 200;
                dbComando.Parameters.Add(dbpEncabezado);


                DbParameter dbpPie = objConexion.dbProvider.CreateParameter();
                dbpPie.DbType = DbType.String;
                dbpPie.Direction = ParameterDirection.Output;
                dbpPie.ParameterName = "@pie";
                dbpPie.Size = 200;
                dbComando.Parameters.Add(dbpPie);



                dbComando.ExecuteNonQuery();

                strResultado.Add(Convert.ToString(dbComando.Parameters["@contenido"].Value));
                strResultado.Add(Convert.ToString(dbComando.Parameters["@encabezado"].Value));
                strResultado.Add(Convert.ToString(dbComando.Parameters["@pie"].Value));

                dbTrans.Commit();


                return strResultado;
                
                
                /*
                
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "catalogo.spuConsultarTextoOficio";
                dbComando.Parameters.Clear();

                DbParameter dbpIdTipo = objConexion.dbProvider.CreateParameter();
                dbpIdTipo.DbType = DbType.Int16;
                dbpIdTipo.Direction = ParameterDirection.Input;
                dbpIdTipo.ParameterName = "@intTipo";
                dbpIdTipo.Value = intTipo;
                dbComando.Parameters.Add(dbpIdTipo);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsDatos);


                dbTrans.Commit();
                strResultado = dsDatos.Tables[0].Rows[0]["oaTexto"].ToString();


                return strResultado;*/
            }
            catch (DbException dbEx)
            {

                throw dbEx;
            }

            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);

            }

        }
        /*
        public static string consultaZonaValidaParaOficio(int idServicio, int idInstalacion, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();

            string strResultado = "";
            DataSet dsDatos = new DataSet();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "catalogo.spuConsultarTextoOficio";
                dbComando.Parameters.Clear();

                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.Direction = ParameterDirection.Input;
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = idServicio;
                dbComando.Parameters.Add(dbpIdServicio);


                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsDatos);


                dbTrans.Commit();
                strResultado = dsDatos.Tables[0].Rows[0]["oaTexto"].ToString();

                return strResultado;
            }
            catch (DbException dbEx)
            {

                throw dbEx;
            }

            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);

            }

        }*/
        public static string consultaHorarioDiaREA(Int16 idDia, int idHorario, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();

            string strResultado = "_____";
            DataSet dsDatos = new DataSet();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "catalogo.spConsultarHorarioDiaREA";
                dbComando.Parameters.Clear();

                DbParameter dbpIdDia = objConexion.dbProvider.CreateParameter();
                dbpIdDia.DbType = DbType.Int16;
                dbpIdDia.Direction = ParameterDirection.Input;
                dbpIdDia.ParameterName = "@idDia";
                dbpIdDia.Value = idDia;
                dbComando.Parameters.Add(dbpIdDia);

                DbParameter dbpIdHorario = objConexion.dbProvider.CreateParameter();
                dbpIdHorario.DbType = DbType.Int32;
                dbpIdHorario.Direction = ParameterDirection.Input;
                dbpIdHorario.ParameterName = "@idHorario";
                dbpIdHorario.Value = idHorario;
                dbComando.Parameters.Add(dbpIdHorario);


                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsDatos);


                dbTrans.Commit();

                if (dsDatos.Tables[0].Rows.Count>0)
                {
                    strResultado = dsDatos.Tables[0].Rows[0]["hdEntrada"].ToString();
                }


                return strResultado;
            }
            catch (DbException dbEx)
            {

                throw dbEx;
            }

            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);

            }

        }

        public static List<string> consultaTitularEstacion(int idServicio, int idInstalacion, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();

            List<string> strResultado = new List<string>();
            DataSet dsDatos = new DataSet();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spConsultarTitularEstacion";
                dbComando.Parameters.Clear();

                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.Direction = ParameterDirection.Input;
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = idServicio;
                dbComando.Parameters.Add(dbpIdServicio);


                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsDatos);


                dbTrans.Commit();

                if (dsDatos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsDatos.Tables[0].Rows.Count; i++)
                    {
                        strResultado.Add(dsDatos.Tables[0].Rows[i]["Nombre"].ToString());
                    }
                }
                else
                {
                    strResultado.Add( "");
                }

                return strResultado;
            }
            catch (DbException dbEx)
            {

                throw dbEx;
            }

            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);

            }

        }

        public static List<string> consultaJefeServicio(int idServicio, int idInstalacion, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();

            List<string> strResultado = new List<string>();
            DataSet dsDatos = new DataSet();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spConsultarJefeServicio";
                dbComando.Parameters.Clear();

                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.Direction = ParameterDirection.Input;
                dbpIdInstalacion.ParameterName = "@idinstalacion";
                dbpIdInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdServicio.ParameterName = "@idservicio";
                dbpIdServicio.Value = idServicio;
                dbComando.Parameters.Add(dbpIdServicio);


                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsDatos);


                dbTrans.Commit();

                if (dsDatos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsDatos.Tables[0].Rows.Count; i++)
                    {
                        strResultado.Add(dsDatos.Tables[0].Rows[i]["Nombre"].ToString());
                    }
                }
                else
                {
                    strResultado.Add("");
                }

                return strResultado;
            }
            catch (DbException dbEx)
            {

                throw dbEx;
            }

            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);

            }

        }



        public static Boolean verificaZona(int idServicio, int idInstalacion, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();

            Boolean blResultado = false;
            DataSet dsDatos = new DataSet();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spVerificaZonaOficioAsignacion";
                dbComando.Parameters.Clear();

                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.Direction = ParameterDirection.Input;
                dbpIdInstalacion.ParameterName = "@idinstalacion";
                dbpIdInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdServicio.ParameterName = "@idservicio";
                dbpIdServicio.Value = idServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                DbParameter dbpZonaExiste = objConexion.dbProvider.CreateParameter();
                dbpZonaExiste.DbType = DbType.Boolean;
                dbpZonaExiste.Direction = ParameterDirection.Output;
                dbpZonaExiste.ParameterName = "@blZonaExiste";
                dbComando.Parameters.Add(dbpZonaExiste);



                dbComando.ExecuteNonQuery();
                blResultado = Convert.ToBoolean(dbComando.Parameters["@blZonaExiste"].Value);


                dbTrans.Commit();


                return blResultado;
            }
            catch (DbException dbEx)
            {

                throw dbEx;
            }

            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);

            }

        }


        public static DataTable obtieneOficioAsignacion(Guid idEmpleado, Int16 idEmpleadoAsignacion, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();

            DataTable dtDatos = new DataTable();

            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spuConsultarEmpleadoOficio";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();
                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.Direction = ParameterDirection.Input;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                DbParameter dbpIdEmpleadoAsignacion = objConexion.dbProvider.CreateParameter();
                dbpIdEmpleadoAsignacion.DbType = DbType.Int16;
                dbpIdEmpleadoAsignacion.Direction = ParameterDirection.Input;
                dbpIdEmpleadoAsignacion.ParameterName = "@idEmpleadoAsignacion";
                dbpIdEmpleadoAsignacion.Value = idEmpleadoAsignacion;
                dbComando.Parameters.Add(dbpIdEmpleadoAsignacion);


                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dtDatos);


                dbTrans.Commit();
                

                return dtDatos;
            }
            catch (DbException dbEx)
            {

                throw dbEx;
            }

            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);

            }

        }


        public static bool insertarOficioAsignacion(clsEntOficioAsignacion objOficio, clsEntSesion objSesion)
        {

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbTransaction dbTrans = dbConexion.BeginTransaction();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spuInsertarEmpleadoOficio";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objOficio.idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoAsignacion", objOficio.idEmpleadoAsignacion);
                objParametro.llenarParametros(ref dbComando, DbType.Binary, "@eoImagen", objOficio.oficioAsignacion);
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idUsuario", objSesion.usuario.IdEmpleado);


                dbComando.ExecuteNonQuery();
                dbTrans.Commit();
                return true;
            }
            catch (Exception Ex)
            {
                dbComando.Transaction.Rollback();
                return false;
                throw Ex;

            }


        }


    }




}
