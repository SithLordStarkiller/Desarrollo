using System;
using System.Data;
using System.Data.Common;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using SICOGUA.Datos;
using REA.Entidades;
using System.Collections.Generic;

namespace SICOGUA.Datos
{
    public class clsDataAnexoTecnico
    {

        public static List<clsEntAnexoTecnico> consultarAnexoTecnico(int intIdServicio, int intIdInstalacion, clsEntSesion objSesion)
        {

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            List<clsEntAnexoTecnico> lisAnexos = new List<clsEntAnexoTecnico>();
            
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spuConsultarAnexo";
                dbComando.Connection = dbConexion;
                dbComando.Parameters.Clear();

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdInstalacion.Direction = ParameterDirection.Input;
                dbpIdInstalacion.Value = intIdInstalacion;
                dbpIdServicio.Value = intIdServicio;
                dbComando.Parameters.Add(dbpIdServicio);
                dbComando.Parameters.Add(dbpIdInstalacion);

                dbComando.ExecuteNonQuery();
                IDataReader idrAnexo = dbComando.ExecuteReader();
                while (idrAnexo.Read())
                {

                    clsEntServicio objServicio = new clsEntServicio();
                    objServicio.idServicio = Convert.ToInt32(idrAnexo["idServicio"]);
                    objServicio.serDescripcion = idrAnexo["serDescripcion"].ToString();
                    clsEntInstalacion objInstalacion = new clsEntInstalacion();
                    objInstalacion.IdInstalacion = Convert.ToInt32(idrAnexo["idInstalacion"]);
                    objInstalacion.InsNombre = idrAnexo["insNombre"].ToString();
                    objInstalacion.insFechaInicio  = Convert.ToDateTime(idrAnexo["insFechaInicio"]);
                    objInstalacion.insFechaFin = Convert.ToDateTime(idrAnexo["insFechaFin"].ToString() == "" ? "01/01/1900" : idrAnexo["insFechaFin"].ToString());
                    clsEntZona objZona = new clsEntZona();
                    objZona.IdZona = Convert.ToInt16( idrAnexo["idZona"]);
                    objZona.ZonDescripcion = idrAnexo["zonDescripcion"].ToString();
                    clsEntAnexoTecnico objAnexo = new clsEntAnexoTecnico();
                    objAnexo.instalacion = objInstalacion;
                    objAnexo.servicio = objServicio;
                    objAnexo.zona = objZona;
                    objAnexo.fechaInicio = Convert.ToDateTime(idrAnexo["iaFechaInicio"]);
                    objAnexo.fechaFin = Convert.ToDateTime(idrAnexo["iaFechaFin"].ToString()==""? "01/01/1900": idrAnexo["iaFechaFin"].ToString());
                    objAnexo.strConvenio = idrAnexo["iaConvenio"].ToString();
                    lisAnexos.Add(objAnexo);
                    

                 

                }

                idrAnexo.Close();

                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return lisAnexos;


        }

        public static List<clsEntAnexoTecnico> consultarServicioInstalacion(int intIdServicio, int intIdInstalacion, clsEntSesion objSesion)
        {

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            List<clsEntAnexoTecnico> lisAnexos = new List<clsEntAnexoTecnico>();

            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spuConsultarAnexoServicioInstalacion";
                dbComando.Connection = dbConexion;
                dbComando.Parameters.Clear();

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdInstalacion.Direction = ParameterDirection.Input;
                dbpIdInstalacion.Value = intIdInstalacion;
                dbpIdServicio.Value = intIdServicio;
                dbComando.Parameters.Add(dbpIdServicio);
                dbComando.Parameters.Add(dbpIdInstalacion);

                dbComando.ExecuteNonQuery();
                IDataReader idrAnexo = dbComando.ExecuteReader();
                while (idrAnexo.Read())
                {

                    clsEntServicio objServicio = new clsEntServicio();
                    objServicio.idServicio = Convert.ToInt32(idrAnexo["idServicio"]);
                    objServicio.serDescripcion = idrAnexo["serDescripcion"].ToString();
                    clsEntInstalacion objInstalacion = new clsEntInstalacion();
                    objInstalacion.IdInstalacion = Convert.ToInt32(idrAnexo["idInstalacion"]);
                    objInstalacion.InsNombre = idrAnexo["insNombre"].ToString();
                    objInstalacion.insFechaInicio = Convert.ToDateTime(idrAnexo["insFechaInicio"]);
                    objInstalacion.insFechaFin = Convert.ToDateTime(idrAnexo["insFechaFin"].ToString() == "" ? "01/01/1900" : idrAnexo["insFechaFin"].ToString());
                    clsEntZona objZona = new clsEntZona();
                    objZona.IdZona = Convert.ToInt16(idrAnexo["idZona"]);
                    objZona.ZonDescripcion = idrAnexo["zonDescripcion"].ToString();
                    clsEntAnexoTecnico objAnexo = new clsEntAnexoTecnico();
                    objAnexo.instalacion = objInstalacion;
                    objAnexo.servicio = objServicio;
                    objAnexo.zona = objZona;                   
                    lisAnexos.Add(objAnexo);
                }

                idrAnexo.Close();

                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return lisAnexos;


        }

        public static void consultarAnexoTecnicoDetalle(clsEntSesion objSesion,ref clsEntAnexoTecnico objAnexo)
        {

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            List<clsEntAnexoJerarquiaHorario> lisAnexosJerarquia = new List<clsEntAnexoJerarquiaHorario>();

            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spuConsultarAnexoDetalle";
                dbComando.Connection = dbConexion;
                dbComando.Parameters.Clear();

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFechaInicio = objConexion.dbProvider.CreateParameter();

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdInstalacion.DbType = DbType.Int32;
                dbpFechaInicio.DbType = DbType.DateTime;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpFechaInicio.ParameterName = "@iaFechaInicio";
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdInstalacion.Direction = ParameterDirection.Input;
                dbpFechaInicio.Direction = ParameterDirection.Input;
                dbpIdInstalacion.Value = objAnexo.instalacion.IdInstalacion;
                dbpIdServicio.Value = objAnexo.servicio.idServicio;
                dbpFechaInicio.Value = objAnexo.fechaInicio;
                dbComando.Parameters.Add(dbpIdServicio);
                dbComando.Parameters.Add(dbpIdInstalacion);
                dbComando.Parameters.Add(dbpFechaInicio);
                
                DataTable dTable = new DataTable();
                DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();
                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;
                dbAdapter.Fill(dTable);
                dbTrans.Commit();
                int intRenglones = dTable.Rows.Count;
                if (intRenglones != 0)
                {
                    objAnexo.anexoJerarquiaHorario = new List<clsEntAnexoJerarquiaHorario>();
                    int intServicio = 0, intInstalacion = 0, intTipoHorario = 0, intJerarquia = 0;
                    string iaConvenio;
                    DateTime datFechaInicio;
                    intServicio = Convert.ToInt32(dTable.Rows[0]["idServicio"]);
                    intInstalacion = Convert.ToInt32(dTable.Rows[0]["idInstalacion"]);
                    intTipoHorario = Convert.ToInt32(dTable.Rows[0]["idTipoHorario"]);
                    intJerarquia = Convert.ToInt32(dTable.Rows[0]["idJerarquia"]);
                    datFechaInicio = Convert.ToDateTime(dTable.Rows[0]["iaFechaInicio"]);
                    iaConvenio = dTable.Rows[0]["iaConvenio"].ToString();
                    clsEntAnexoJerarquiaHorario objAnexoJerarquia = new clsEntAnexoJerarquiaHorario();
                    objAnexoJerarquia.dia = new clsEntDia();

                    int TotalHombres = 0;
                    int TotalMujeres = 0;
                    int TotalIndistinto = 0;

                    foreach (DataRow drEmpleado in dTable.Rows)
                    {
                        int intDia = Convert.ToInt32(drEmpleado["idDia"]);
                        objAnexoJerarquia.dia.idDia = intDia;
                        objAnexoJerarquia.jerDescripcion = drEmpleado["jerDescripcion"].ToString();
                        objAnexoJerarquia.thTurno = drEmpleado["thTurno"].ToString();
                        objAnexoJerarquia.thHorario = drEmpleado["thDescripcion"].ToString();
                                        
                        #region case
                        switch (intDia)
                        {
                            case 1:
                                objAnexoJerarquia.masculinoLunes = Convert.ToInt32(drEmpleado["iajMasculino"]);
                                objAnexoJerarquia.femeninoLunes = Convert.ToInt32(drEmpleado["iajFemenino"]);
                                objAnexoJerarquia.indistintoLunes = Convert.ToInt32(drEmpleado["iajIndistinto"]);

                                TotalHombres = TotalHombres + objAnexoJerarquia.masculinoLunes;
                                TotalMujeres = TotalMujeres + objAnexoJerarquia.femeninoLunes;
                                TotalIndistinto = TotalIndistinto + objAnexoJerarquia.indistintoLunes;

                                break;
                            case 2:
                                objAnexoJerarquia.masculinoMartes = Convert.ToInt32(drEmpleado["iajMasculino"]);
                                objAnexoJerarquia.femeninoMartes = Convert.ToInt32(drEmpleado["iajFemenino"]);
                                objAnexoJerarquia.indistintoMartes = Convert.ToInt32(drEmpleado["iajIndistinto"]);
                                                                
                                TotalHombres = TotalHombres + objAnexoJerarquia.masculinoMartes;
                                TotalMujeres = TotalMujeres + objAnexoJerarquia.femeninoMartes;
                                TotalIndistinto = TotalIndistinto + objAnexoJerarquia.indistintoMartes;

                                break;
                            case 3:
                                objAnexoJerarquia.masculinoMiercoles = Convert.ToInt32(drEmpleado["iajMasculino"]);
                                objAnexoJerarquia.femeninoMiercoles = Convert.ToInt32(drEmpleado["iajFemenino"]);
                                objAnexoJerarquia.indistintoMiercoles = Convert.ToInt32(drEmpleado["iajIndistinto"]);
                                                                
                                TotalHombres = TotalHombres + objAnexoJerarquia.masculinoMiercoles;
                                TotalMujeres = TotalMujeres + objAnexoJerarquia.femeninoMiercoles;
                                TotalIndistinto = TotalIndistinto + objAnexoJerarquia.indistintoMiercoles;

                                break;
                            case 4:
                                objAnexoJerarquia.masculinoJueves = Convert.ToInt32(drEmpleado["iajMasculino"]);
                                objAnexoJerarquia.femeninoJueves = Convert.ToInt32(drEmpleado["iajFemenino"]);
                                objAnexoJerarquia.indistintoJueves = Convert.ToInt32(drEmpleado["iajIndistinto"]);

                                TotalHombres = TotalHombres + objAnexoJerarquia.masculinoJueves;
                                TotalMujeres = TotalMujeres + objAnexoJerarquia.femeninoJueves;
                                TotalIndistinto = TotalIndistinto + objAnexoJerarquia.indistintoJueves;

                                break;
                            case 5:
                                objAnexoJerarquia.masculinoViernes = Convert.ToInt32(drEmpleado["iajMasculino"]);
                                objAnexoJerarquia.femeninoViernes = Convert.ToInt32(drEmpleado["iajFemenino"]);
                                objAnexoJerarquia.indistintoViernes = Convert.ToInt32(drEmpleado["iajIndistinto"]);

                                TotalHombres = TotalHombres + objAnexoJerarquia.masculinoViernes;
                                TotalMujeres = TotalMujeres + objAnexoJerarquia.femeninoViernes;
                                TotalIndistinto = TotalIndistinto + objAnexoJerarquia.indistintoViernes;

                                break;
                            case 6:
                                objAnexoJerarquia.masculinoSabado = Convert.ToInt32(drEmpleado["iajMasculino"]);
                                objAnexoJerarquia.femeninoSabado = Convert.ToInt32(drEmpleado["iajFemenino"]);
                                objAnexoJerarquia.indistintoSabado = Convert.ToInt32(drEmpleado["iajIndistinto"]);

                                TotalHombres = TotalHombres + objAnexoJerarquia.masculinoSabado;
                                TotalMujeres = TotalMujeres + objAnexoJerarquia.femeninoSabado;
                                TotalIndistinto = TotalIndistinto + objAnexoJerarquia.indistintoSabado;

                                break;
                            case 7:
                                objAnexoJerarquia.masculinoDomingo = Convert.ToInt32(drEmpleado["iajMasculino"]);
                                objAnexoJerarquia.femeninoDomingo = Convert.ToInt32(drEmpleado["iajFemenino"]);
                                objAnexoJerarquia.indistintoDomingo = Convert.ToInt32(drEmpleado["iajIndistinto"]);

                                TotalHombres = TotalHombres + objAnexoJerarquia.masculinoDomingo;
                                TotalMujeres = TotalMujeres + objAnexoJerarquia.femeninoDomingo;
                                TotalIndistinto = TotalIndistinto + objAnexoJerarquia.indistintoDomingo;

                                break;
                            default:
                                break;
                        }
                        #endregion
                        objAnexoJerarquia.totalHombres = TotalHombres;
                        objAnexoJerarquia.totalMujeres = TotalMujeres;
                        objAnexoJerarquia.totalIndistinto = TotalIndistinto;
                        if(intDia == 7)
                        {
                            TotalMujeres = TotalHombres = TotalIndistinto = 0;    
                            objAnexo.anexoJerarquiaHorario.Add(objAnexoJerarquia);
                            objAnexoJerarquia = new clsEntAnexoJerarquiaHorario();
                            objAnexoJerarquia.dia = new clsEntDia();
                            intServicio = Convert.ToInt32(drEmpleado["idServicio"]);
                            intInstalacion = Convert.ToInt32(drEmpleado["idInstalacion"]);
                            intTipoHorario = Convert.ToInt32(drEmpleado["idTipoHorario"]);
                            intJerarquia = Convert.ToInt32(drEmpleado["idJerarquia"]);
                            datFechaInicio = Convert.ToDateTime(drEmpleado["iaFechaInicio"]);
                            objAnexoJerarquia.dia.idDia = intDia;                           
                        }

                    }
                }
            }
           catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
        }

        public static int consultaIdTipoHorario(string procedimientoAlmacenado, string thDescripcion, string thTurno, clsEntSesion objSesion)
        {
            int idTipoHorario = 0;
            DataTable dTable;
            DataSet ds = new DataSet();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbParamThTipoHorario = objConexion.dbProvider.CreateParameter();
                DbParameter dbParamThTurno = objConexion.dbProvider.CreateParameter();

                dbParamThTipoHorario.DbType = DbType.String;
                dbParamThTipoHorario.ParameterName = "@thDescripcion";
                dbParamThTipoHorario.Value = thDescripcion;
                dbComando.Parameters.Add(dbParamThTipoHorario);
                dbParamThTurno.DbType = DbType.String;
                dbParamThTurno.ParameterName = "@thTurno";
                dbParamThTurno.Value = thTurno;
                dbComando.Parameters.Add(dbParamThTurno);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;
                dbAdapter.SelectCommand = dbComando;
                                
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();

                int intRenglones = dTable.Rows.Count;
               
                if (intRenglones != 0)
                {
                    idTipoHorario = Convert.ToInt32(dTable.Rows[0]["idTipoHorario"]);
                }
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return idTipoHorario;
        }

        public static bool insertarAnexo(ref List<clsEntAnexoTecnico> lisAnexos, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            int idUsuario = objSesion.usuario.IdUsuario;
            DbTransaction dbTrans = null;

            DbCommand dbAnexo = objConexion.dbProvider.CreateCommand();    
            try
            {                               
                foreach (clsEntAnexoTecnico objAnexo in lisAnexos)
                {
                    dbTrans = dbConexion.BeginTransaction();
                    dbAnexo.Transaction = dbTrans;
                    dbAnexo.CommandType = CommandType.StoredProcedure;
                    dbAnexo.Connection = dbConexion;
                    dbAnexo.CommandText = "servicio.spuInsertarActualizarAnexoTecnico";
                    dbAnexo.Parameters.Clear();

                    DbParameter dbpFechaInicio = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpConvenio = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpidServicio = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpidInstalacion = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpInserto = objConexion.dbProvider.CreateParameter();

                    dbpidServicio.DbType = DbType.Int32;
                    dbpidInstalacion.DbType = DbType.Int32;
                    dbpFechaInicio.DbType = DbType.DateTime;
                    dbpConvenio.DbType = DbType.String;
                    dbpInserto.DbType = DbType.Boolean;

                    dbpFechaInicio.ParameterName = "@fechaInicio";
                    dbpConvenio.ParameterName = "@convenio";
                    dbpidServicio.ParameterName = "@idServicio";
                    dbpidInstalacion.ParameterName = "@idInstalacion";
                    dbpInserto.ParameterName = "@inserto";

                    dbpidServicio.Direction = ParameterDirection.Input;
                    dbpidInstalacion.Direction = ParameterDirection.Input;
                    dbpFechaInicio.Direction = ParameterDirection.Input;
                    dbpConvenio.Direction = ParameterDirection.Input;
                    dbpInserto.Direction = ParameterDirection.Output;

                    dbpFechaInicio.Value = objAnexo.fechaInicio.ToShortDateString();
                    dbpConvenio.Value = objAnexo.strConvenio;
                    dbpidServicio.Value = objAnexo.servicio.idServicio;
                    dbpidInstalacion.Value = objAnexo.instalacion.IdInstalacion;

                    dbAnexo.Parameters.Add(dbpFechaInicio);
                    dbAnexo.Parameters.Add(dbpConvenio);
                    dbAnexo.Parameters.Add(dbpidServicio);
                    dbAnexo.Parameters.Add(dbpidInstalacion);
                    dbAnexo.Parameters.Add(dbpInserto);
                    dbAnexo.ExecuteNonQuery();
                    dbTrans.Commit();

                    if (objAnexo.anexoJerarquiaHorario != null)
                    {
                        foreach (clsEntAnexoJerarquiaHorario objHorario in objAnexo.anexoJerarquiaHorario)
                        {
                            if (objHorario.idJerarquia != 0)
                            {
                                insertarAnexoJerarquiaHorario(objAnexo.servicio.idServicio
                                                              , objAnexo.instalacion.IdInstalacion
                                                              , objAnexo.fechaInicio
                                                              , objHorario.idTipoHorario
                                                              , objHorario.idJerarquia
                                                              , 1
                                                              , objHorario.masculinoLunes
                                                              , objHorario.femeninoLunes
                                                              , objHorario.indistintoLunes, objSesion);
                                insertarAnexoJerarquiaHorario(objAnexo.servicio.idServicio
                                                              , objAnexo.instalacion.IdInstalacion
                                                              , objAnexo.fechaInicio
                                                              , objHorario.idTipoHorario
                                                              , objHorario.idJerarquia
                                                              , 2
                                                              , objHorario.masculinoMartes
                                                              , objHorario.femeninoMartes
                                                              , objHorario.indistintoMartes, objSesion);
                                insertarAnexoJerarquiaHorario(objAnexo.servicio.idServicio
                                                             , objAnexo.instalacion.IdInstalacion
                                                             , objAnexo.fechaInicio
                                                             , objHorario.idTipoHorario
                                                             , objHorario.idJerarquia
                                                             , 3
                                                             , objHorario.masculinoMiercoles
                                                             , objHorario.femeninoMiercoles
                                                             , objHorario.indistintoMiercoles, objSesion);
                                insertarAnexoJerarquiaHorario(objAnexo.servicio.idServicio
                                                             , objAnexo.instalacion.IdInstalacion
                                                             , objAnexo.fechaInicio
                                                             , objHorario.idTipoHorario
                                                             , objHorario.idJerarquia
                                                             , 4
                                                             , objHorario.masculinoJueves
                                                             , objHorario.femeninoJueves
                                                             , objHorario.indistintoJueves, objSesion);
                                insertarAnexoJerarquiaHorario(objAnexo.servicio.idServicio
                                                             , objAnexo.instalacion.IdInstalacion
                                                             , objAnexo.fechaInicio
                                                             , objHorario.idTipoHorario
                                                             , objHorario.idJerarquia
                                                             , 5
                                                             , objHorario.masculinoViernes
                                                             , objHorario.femeninoViernes
                                                             , objHorario.indistintoViernes, objSesion);
                                insertarAnexoJerarquiaHorario(objAnexo.servicio.idServicio
                                                             , objAnexo.instalacion.IdInstalacion
                                                             , objAnexo.fechaInicio
                                                             , objHorario.idTipoHorario
                                                             , objHorario.idJerarquia
                                                             , 6
                                                             , objHorario.masculinoSabado
                                                             , objHorario.femeninoSabado
                                                             , objHorario.indistintoSabado, objSesion);
                                insertarAnexoJerarquiaHorario(objAnexo.servicio.idServicio
                                                             , objAnexo.instalacion.IdInstalacion
                                                             , objAnexo.fechaInicio
                                                             , objHorario.idTipoHorario
                                                             , objHorario.idJerarquia
                                                             , 7
                                                             , objHorario.masculinoDomingo
                                                             , objHorario.femeninoDomingo
                                                             , objHorario.indistintoDomingo, objSesion);
                            }
                        }
                    }
                }                

                return true;
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }


        }

        public static void insertarAnexoJerarquiaHorario(int idServicio, int idInstalacion, DateTime fechaInicio, int idTipoHorario, int idJerarquia,  byte idDia, int iajMasculino, int iajFemenino, int iajIndistinto, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.Connection = dbConexion;
                dbComando.CommandText = "servicio.spuInsertarActualizarAnexoHorario";
                dbComando.Parameters.Clear();

                DbParameter dbpidServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpiaFechaInicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdTipoHorario = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdJerarquia = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdDia = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIajMasculino = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIajFemenino = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIajIndistinto = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsercion = objConexion.dbProvider.CreateParameter();

                dbpidServicio.ParameterName = "@idServicio";
                dbpidInstalacion.ParameterName = "@idInstalacion";
                dbpIdTipoHorario.ParameterName = "@idTipoHorario";
                dbpIdJerarquia.ParameterName = "@idJerarquia";
                dbpIdDia.ParameterName = "@idDia";
                dbpIajMasculino.ParameterName = "@iajMasculino";
                dbpIajFemenino.ParameterName = "@iajFemenino";
                dbpIajIndistinto.ParameterName = "@iajIndistinto";
                dbpInsercion.ParameterName = "@INSERCION";
                dbpiaFechaInicio.ParameterName = "@iaFechaInicio";

                dbpidServicio.DbType = DbType.Int32;
                dbpidInstalacion.DbType = DbType.Int32;
                dbpiaFechaInicio.DbType = DbType.DateTime;
                dbpIdTipoHorario.DbType = DbType.Byte;
                dbpIdJerarquia.DbType = DbType.Byte;
                dbpIdDia.DbType = DbType.Byte;
                dbpIajMasculino.DbType = DbType.Int32;
                dbpIajFemenino.DbType = DbType.Int32;
                dbpIajIndistinto.DbType = DbType.Int32;
                dbpInsercion.DbType = DbType.Boolean;

                dbpidServicio.Direction = ParameterDirection.Input;
                dbpidInstalacion.Direction = ParameterDirection.Input;
                dbpiaFechaInicio.Direction = ParameterDirection.Input;
                dbpIdTipoHorario.Direction = ParameterDirection.Input;
                dbpIdJerarquia.Direction = ParameterDirection.Input;
                dbpIdDia.Direction = ParameterDirection.Input;
                dbpIajMasculino.Direction = ParameterDirection.Input;
                dbpIajFemenino.Direction = ParameterDirection.Input;
                dbpIajIndistinto.Direction = ParameterDirection.Input;
                dbpInsercion.Direction = ParameterDirection.Output;

                dbpidServicio.Value = idServicio;
                dbpidInstalacion.Value = idInstalacion;
                dbpiaFechaInicio.Value = fechaInicio.ToShortDateString();
                dbpIdTipoHorario.Value = idTipoHorario;
                dbpIdJerarquia.Value = idJerarquia;
                dbpIdDia.Value = idDia;
                dbpIajMasculino.Value = iajMasculino;
                dbpIajFemenino.Value = iajFemenino;
                dbpIajIndistinto.Value = iajIndistinto;

                dbComando.Parameters.Add(dbpidServicio);
                dbComando.Parameters.Add(dbpidInstalacion);
                dbComando.Parameters.Add(dbpiaFechaInicio);
                dbComando.Parameters.Add(dbpIdTipoHorario);
                dbComando.Parameters.Add(dbpIdJerarquia);
                dbComando.Parameters.Add(dbpIdDia);
                dbComando.Parameters.Add(dbpIajMasculino);
                dbComando.Parameters.Add(dbpIajFemenino);
                dbComando.Parameters.Add(dbpIajIndistinto);
                dbComando.Parameters.Add(dbpInsercion);

                dbComando.ExecuteNonQuery();
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                throw dbEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}