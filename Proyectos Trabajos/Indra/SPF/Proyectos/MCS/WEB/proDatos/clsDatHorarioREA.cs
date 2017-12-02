using System;
using System.Data;
using System.Data.Common;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using SICOGUA.Datos;
using REA.Entidades;
using System.Collections.Generic;

namespace REA.Datos
{
   public class clsDatHorarioREA
    {

       public static REA.Entidades.clsEntHorario consultarServicioInstalacion(int intIdServicio, int intIdInstalacion, clsEntSesion objSesion)
       {

           clsDatConexion objConexion = new clsDatConexion();
           DbConnection dbConexion = objConexion.getConexion(objSesion);

           DbCommand dbComando = objConexion.dbProvider.CreateCommand();
           DbTransaction dbTrans = dbConexion.BeginTransaction();
           REA.Entidades.clsEntHorario objHorario = new REA.Entidades.clsEntHorario();
           int idUsuario = objSesion.usuario.IdUsuario;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "catalogo.spuConsultarSerInst";
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
                IDataReader idrHorario = dbComando.ExecuteReader();
                if (idrHorario.Read())
                {

                    clsEntServicio objServicio = new clsEntServicio();
                    objHorario.Servicio = objServicio;
                    clsEntInstalacion objInstalacion = new clsEntInstalacion();
                    objHorario.Instalacion = objInstalacion;
                    clsEntZona objZona = new clsEntZona();
                    objHorario.Zona = objZona;
                    objHorario.Servicio.serDescripcion = conversiones.cadena(idrHorario["serDescripcion"]);
                    objHorario.Instalacion.InsNombre = conversiones.cadena(idrHorario["insNombre"]);
                    objHorario.Zona.ZonDescripcion = conversiones.cadena(idrHorario["zonDescripcion"]);
                    objHorario.tipoInstalacion = conversiones.cadena(idrHorario["tiDescripcion"]);
                    objHorario.horVigente = conversiones.boleanoNoNulo(idrHorario["vigente"]);

                }

                idrHorario.Close();
                            
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
           return objHorario;
                

       }

       public static int insertarHorario(REA.Entidades.clsEntHorario objHorario, clsEntHorarioComidaREA objHorarioComida, clsEntSesion objSesion)
       {
           clsDatConexion objConexion = new clsDatConexion();
           DbConnection dbConexion = objConexion.getConexionREA(objSesion);
           int idHorario = 0;
           int idUsuario = objSesion.usuario.IdUsuario;


           DbCommand dbHorario = objConexion.dbProvider.CreateCommand();
           DbTransaction dbTrans = dbConexion.BeginTransaction();
           dbHorario.Transaction = dbTrans;
           try
           {
               dbHorario.CommandType = CommandType.StoredProcedure;
               dbHorario.Connection = dbConexion;
               dbHorario.CommandText = "horario.spuInsertarActualizarHorario";
               dbHorario.Parameters.Clear();


               DbParameter dbpidHorario = objConexion.dbProvider.CreateParameter();
               DbParameter dbpidServicio = objConexion.dbProvider.CreateParameter();
               DbParameter dbpidInstalacion = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorNombre = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorDescripcion = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorTipo = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorDiaFestivo = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorFechaInicio = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorJornada = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorDescanso = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorTolerancia = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorRetardo = objConexion.dbProvider.CreateParameter();
               DbParameter dbpSalidaLaboral = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorModulo = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorTiempoMcs = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorVigente = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorComidaEntrada = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorComidaSalida = objConexion.dbProvider.CreateParameter();
               DbParameter dbphcMinuto = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdEntradaL = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdSalidaL = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdEntradaM = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdSalidaM = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdEntradaMI = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdSalidaMI = objConexion.dbProvider.CreateParameter();
               DbParameter dbpdhdEntradaJ = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdSalidaJ = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdEntradaV = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdSalidaV = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdEntradaS = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdSalidaS = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdEntradaD = objConexion.dbProvider.CreateParameter();
               DbParameter dbphdSalidaD = objConexion.dbProvider.CreateParameter();
               DbParameter dbpidDiaL = objConexion.dbProvider.CreateParameter();
               DbParameter dbpidDiaM = objConexion.dbProvider.CreateParameter();
               DbParameter dbpidDiaMi = objConexion.dbProvider.CreateParameter();
               DbParameter dbpidDiaJu = objConexion.dbProvider.CreateParameter();
               DbParameter dbpidDiaV = objConexion.dbProvider.CreateParameter();
               DbParameter dbpidDiaS = objConexion.dbProvider.CreateParameter();
               DbParameter dbpidDiaD = objConexion.dbProvider.CreateParameter();
               DbParameter dbptiempoComida = objConexion.dbProvider.CreateParameter();
               DbParameter dbpact = objConexion.dbProvider.CreateParameter();
               DbParameter dbpFinesSemana = objConexion.dbProvider.CreateParameter();
               DbParameter dbphorAbierto = objConexion.dbProvider.CreateParameter();
               DbParameter dbpidTipoHorario = objConexion.dbProvider.CreateParameter();


               dbpidHorario.DbType = DbType.Int32;
               dbpidServicio.DbType = DbType.Int32;
               dbpidInstalacion.DbType = DbType.Int32;
               dbphorNombre.DbType = DbType.String;
               dbphorDescripcion.DbType = DbType.String;
               dbphorTipo.DbType = DbType.String;
               dbphorDiaFestivo.DbType = DbType.Boolean;
               dbphorFechaInicio.DbType = DbType.Time;
               dbphorJornada.DbType = DbType.Byte;
               dbphorDescanso.DbType = DbType.Byte;
               dbphorTolerancia.DbType = DbType.Byte;
               dbphorRetardo.DbType = DbType.Byte;
               dbpSalidaLaboral.DbType = DbType.Boolean;
               dbphorModulo.DbType = DbType.String;
               dbphorTiempoMcs.DbType = DbType.Byte;
               dbphorVigente.DbType = DbType.Boolean;
               dbphorComidaEntrada.DbType = DbType.Time;
               dbphorComidaSalida.DbType = DbType.Time;
               dbphcMinuto.DbType = DbType.Byte;
               dbphdEntradaL.DbType = DbType.Time;
               dbphdSalidaL.DbType = DbType.Time;
               dbphdEntradaM.DbType = DbType.Time;
               dbphdSalidaM.DbType = DbType.Time;
               dbphdEntradaMI.DbType = DbType.Time;
               dbphdSalidaMI.DbType = DbType.Time;
               dbpdhdEntradaJ.DbType = DbType.Time;
               dbphdSalidaJ.DbType = DbType.Time;
               dbphdEntradaV.DbType = DbType.Time;
               dbphdSalidaV.DbType = DbType.Time;
               dbphdEntradaS.DbType = DbType.Time;
               dbphdSalidaS.DbType = DbType.Time;
               dbphdEntradaD.DbType = DbType.Time;
               dbphdSalidaD.DbType = DbType.Time;
               dbpidDiaL.DbType = DbType.Boolean;
               dbpidDiaM.DbType = DbType.Boolean;
               dbpidDiaMi.DbType = DbType.Boolean;
               dbpidDiaJu.DbType = DbType.Boolean;
               dbpidDiaV.DbType = DbType.Boolean;
               dbpidDiaS.DbType = DbType.Boolean;
               dbpidDiaD.DbType = DbType.Boolean;
               dbptiempoComida.DbType = DbType.Boolean;
               dbpact.DbType = DbType.Boolean;
               dbpFinesSemana.DbType = DbType.Boolean;
               dbphorAbierto.DbType = DbType.Boolean;
               dbpidTipoHorario.DbType = DbType.Byte;


               dbpidHorario.ParameterName = "@idHorario";
               dbpidServicio.ParameterName = "@idServicio";
               dbpidInstalacion.ParameterName = "@idInstalacion";
               dbphorNombre.ParameterName = "@horNombre";
               dbphorDescripcion.ParameterName = "@horDescripcion";
               dbphorTipo.ParameterName = "@horTipo";
               dbphorDiaFestivo.ParameterName = "@horDiaFestivo";
               dbphorFechaInicio.ParameterName = "@horFechaInicio";
               dbphorJornada.ParameterName = "@horJornada";
               dbphorDescanso.ParameterName = "@horDescanso";
               dbphorTolerancia.ParameterName = "@horTolerancia";
               dbphorRetardo.ParameterName = "@horRetardo";
               dbpSalidaLaboral.ParameterName = "@horSalidaLaboral";
               dbphorModulo.ParameterName = "@horModulo";
               dbphorTiempoMcs.ParameterName = "@horTiempoMcs";
               dbphorVigente.ParameterName = "@horVigente";
               dbphorAbierto.ParameterName = "@horAbierto";
               dbphorComidaEntrada.ParameterName = "@hcComidaEntrada";
               dbphorComidaSalida.ParameterName = "@hcComidaSalida";
               dbphcMinuto.ParameterName = "@hcMinuto";
               dbphdEntradaL.ParameterName = "@hdEntradaL";
               dbphdSalidaL.ParameterName = "@hdSalidaL";
               dbphdEntradaM.ParameterName = "@hdEntradaM";
               dbphdSalidaM.ParameterName = "@hdSalidaM";
               dbphdEntradaMI.ParameterName = "@hdEntradaMI";
               dbphdSalidaMI.ParameterName = "@hdSalidaMI";
               dbpdhdEntradaJ.ParameterName = "@hdEntradaJ";
               dbphdSalidaJ.ParameterName = "@hdSalidaJ";
               dbphdEntradaV.ParameterName = "@hdEntradaV";
               dbphdSalidaV.ParameterName = "@hdSalidaV";
               dbphdEntradaS.ParameterName = "@hdEntradaS";
               dbphdSalidaS.ParameterName = "@hdSalidaS ";
               dbphdEntradaD.ParameterName = "@hdEntradaD";
               dbphdSalidaD.ParameterName = "@hdSalidaD ";
               dbpidDiaL.ParameterName = "@idDiaL";
               dbpidDiaM.ParameterName = "@idDiaM";
               dbpidDiaMi.ParameterName = "@idDiaMi";
               dbpidDiaJu.ParameterName = "@idDiaJu";
               dbpidDiaV.ParameterName = "@idDiaV";
               dbpidDiaS.ParameterName = "@idDiaS";
               dbpidDiaD.ParameterName = "@idDiaD";
               dbptiempoComida.ParameterName = "@tiempoComida";
               dbpFinesSemana.ParameterName = "@horFinSemana";
               dbpidTipoHorario.ParameterName = "@idTipoHorario";
               dbpact.ParameterName = "@ACT";
             
               

               dbpidHorario.Direction = ParameterDirection.InputOutput;
               dbpidServicio.Direction = ParameterDirection.Input;
               dbpidInstalacion.Direction = ParameterDirection.Input;
               dbphorNombre.Direction = ParameterDirection.Input;
               dbphorDescripcion.Direction = ParameterDirection.Input;
               dbphorTipo.Direction = ParameterDirection.Input;
               dbphorDiaFestivo.Direction = ParameterDirection.Input;
               dbphorFechaInicio.Direction = ParameterDirection.Input;
               dbphorJornada.Direction = ParameterDirection.Input;
               dbphorDescanso.Direction = ParameterDirection.Input;
               dbphorTolerancia.Direction = ParameterDirection.Input;
               dbphorRetardo.Direction = ParameterDirection.Input;
               dbpSalidaLaboral.Direction = ParameterDirection.Input;
               dbphorModulo.Direction = ParameterDirection.Input;
               dbphorTiempoMcs.Direction = ParameterDirection.Input;
               dbphorVigente.Direction = ParameterDirection.Input;
               dbphorAbierto.Direction = ParameterDirection.Input;
               dbphorComidaEntrada.Direction = ParameterDirection.Input;
               dbphorComidaSalida.Direction = ParameterDirection.Input;
               dbphcMinuto.Direction = ParameterDirection.Input;
               dbphdEntradaL.Direction = ParameterDirection.Input;
               dbphdSalidaL.Direction = ParameterDirection.Input;
               dbphdEntradaM.Direction = ParameterDirection.Input;
               dbphdSalidaM.Direction = ParameterDirection.Input;
               dbphdEntradaMI.Direction = ParameterDirection.Input;
               dbphdSalidaMI.Direction = ParameterDirection.Input;
               dbpdhdEntradaJ.Direction = ParameterDirection.Input;
               dbphdSalidaJ.Direction = ParameterDirection.Input;
               dbphdEntradaV.Direction = ParameterDirection.Input;
               dbphdSalidaV.Direction = ParameterDirection.Input;
               dbphdEntradaS.Direction = ParameterDirection.Input;
               dbphdSalidaS.Direction = ParameterDirection.Input;
               dbphdEntradaD.Direction = ParameterDirection.Input;
               dbphdSalidaD.Direction = ParameterDirection.Input;
               dbpidDiaL.Direction = ParameterDirection.Input;
               dbpidDiaM.Direction = ParameterDirection.Input;
               dbpidDiaMi.Direction = ParameterDirection.Input;
               dbpidDiaJu.Direction = ParameterDirection.Input;
               dbpidDiaV.Direction = ParameterDirection.Input;
               dbpidDiaS.Direction = ParameterDirection.Input;
               dbpidDiaD.Direction = ParameterDirection.Input;
               dbptiempoComida.Direction = ParameterDirection.Input;
               dbpFinesSemana.Direction = ParameterDirection.Input;
               dbpidTipoHorario.Direction = ParameterDirection.Input;
               dbpact.Direction = ParameterDirection.Output;

               

               dbpidHorario.Value = objHorario.idHorario;
               dbpidServicio.Value = objHorario.Servicio.idServicio;
               dbpidInstalacion.Value = objHorario.Instalacion.IdInstalacion;
               dbphorNombre.Value = objHorario.horNombre;
               dbphorDescripcion.Value = objHorario.horDescripcion;
               dbphorTipo.Value = objHorario.horTipo;
               dbphorDiaFestivo.Value = objHorario.horDiaFestivo;
               dbphorFechaInicio.Value = objHorario.horFechaInicio.ToShortDateString();
               dbphorJornada.Value = objHorario.horJornada;
               dbphorDescanso.Value = objHorario.horDescanso;
               dbphorTolerancia.Value = objHorario.horTolerancia;
               dbphorRetardo.Value = objHorario.horRetardo;
               dbpSalidaLaboral.Value = objHorario.horSalidaLaboral;
               dbphorModulo.Value = objHorario.horModulo;
               dbphorTiempoMcs.Value = objHorario.horTiempoMCS;
               dbphorVigente.Value = objHorario.horVigente;
               dbphorAbierto.Value = objHorario.horAbierto;
               dbphorComidaEntrada.Value = objHorarioComida.hcComidaEntrada.ToShortTimeString();
               dbphorComidaSalida.Value = objHorarioComida.hcComidaSalida.ToShortTimeString();
               dbphcMinuto.Value = objHorarioComida.hcMinuto;
               dbphdEntradaL.Value = objHorario.horHoraEntradaL.ToShortTimeString();
               dbphdSalidaL.Value = objHorario.horHoraSalidaL.ToShortTimeString();
               dbphdEntradaM.Value = objHorario.horHoraEntradaM.ToShortTimeString();
               dbphdSalidaM.Value = objHorario.horHoraSalidaM.ToShortTimeString();
               dbphdEntradaMI.Value = objHorario.horHoraEntradaMi.ToShortTimeString();
               dbphdSalidaMI.Value = objHorario.horHoraSalidaMi.ToShortTimeString();
               dbpdhdEntradaJ.Value = objHorario.horHoraEntradaJue.ToShortTimeString();
               dbphdSalidaJ.Value = objHorario.horHoraSalidaJue.ToShortTimeString();
               dbphdEntradaV.Value = objHorario.horHoraEntradaVie.ToShortTimeString();
               dbphdSalidaV.Value = objHorario.horHoraSalidaVie.ToShortTimeString();
               dbphdEntradaS.Value = objHorario.horHoraEntradaSa.ToShortTimeString();
               dbphdSalidaS.Value = objHorario.horHoraSalidaSa.ToShortTimeString();
               dbphdEntradaD.Value = objHorario.horHoraEntradaDom.ToShortTimeString();
               dbphdSalidaD.Value = objHorario.horHoraSalidaDom.ToShortTimeString();
               dbpidDiaL.Value = objHorario.horLunes;
               dbpidDiaM.Value = objHorario.horMartes;
               dbpidDiaMi.Value = objHorario.horMiercoles;
               dbpidDiaJu.Value = objHorario.horJueves;
               dbpidDiaV.Value = objHorario.horViernes;
               dbpidDiaS.Value = objHorario.horSabado;
               dbpidDiaD.Value = objHorario.horDomingo;
               dbptiempoComida.Value = objHorarioComida.hcTiempoComida;
               dbpFinesSemana.Value = objHorario.horFinesSemana;
               dbpidTipoHorario.Value = objHorario.tipoHorarioREA.idTipoHorario;

               dbHorario.Parameters.Add(dbpidHorario);
               dbHorario.Parameters.Add(dbpidServicio);
               dbHorario.Parameters.Add(dbpidInstalacion);
               dbHorario.Parameters.Add(dbphorNombre);
               dbHorario.Parameters.Add(dbphorDescripcion);
               dbHorario.Parameters.Add(dbphorTipo);
               dbHorario.Parameters.Add(dbphorDiaFestivo);
               dbHorario.Parameters.Add(dbphorFechaInicio);
               dbHorario.Parameters.Add(dbphorJornada);
               dbHorario.Parameters.Add(dbphorDescanso);
               dbHorario.Parameters.Add(dbphorTolerancia);
               dbHorario.Parameters.Add(dbphorRetardo);
               dbHorario.Parameters.Add(dbpSalidaLaboral);
               dbHorario.Parameters.Add(dbphorModulo);
               dbHorario.Parameters.Add(dbphorTiempoMcs);
               dbHorario.Parameters.Add(dbphorVigente);
               dbHorario.Parameters.Add(dbphorAbierto);
               dbHorario.Parameters.Add(dbphorComidaEntrada);
               dbHorario.Parameters.Add(dbphorComidaSalida);
               dbHorario.Parameters.Add(dbphcMinuto);
               dbHorario.Parameters.Add(dbphdEntradaL);
               dbHorario.Parameters.Add(dbphdSalidaL);
               dbHorario.Parameters.Add(dbphdEntradaM);
               dbHorario.Parameters.Add(dbphdSalidaM);
                dbHorario.Parameters.Add(dbphdEntradaMI);
               dbHorario.Parameters.Add(dbphdSalidaMI);
               dbHorario.Parameters.Add(dbpdhdEntradaJ);
               dbHorario.Parameters.Add(dbphdSalidaJ);
               dbHorario.Parameters.Add(dbphdEntradaV);
               dbHorario.Parameters.Add(dbphdSalidaV);
               dbHorario.Parameters.Add(dbphdEntradaS);
               dbHorario.Parameters.Add(dbphdSalidaS);
               dbHorario.Parameters.Add(dbphdEntradaD);
               dbHorario.Parameters.Add(dbphdSalidaD);
               dbHorario.Parameters.Add(dbpidDiaL);
               dbHorario.Parameters.Add(dbpidDiaM);
               dbHorario.Parameters.Add(dbpidDiaMi);
               dbHorario.Parameters.Add(dbpidDiaJu);
               dbHorario.Parameters.Add(dbpidDiaV);
               dbHorario.Parameters.Add(dbpidDiaS);
               dbHorario.Parameters.Add(dbpidDiaD);
               dbHorario.Parameters.Add(dbptiempoComida);
               dbHorario.Parameters.Add(dbpFinesSemana);
               dbHorario.Parameters.Add(dbpidTipoHorario);
               dbHorario.Parameters.Add(dbpact);





               dbHorario.ExecuteNonQuery();
               idHorario = conversiones.enteroNoNulo(dbpidHorario.Value);
                dbTrans.Commit();

                return idHorario;
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
       public static List<REA.Entidades.clsEntHorario> obtenerHorarioServicioInstalacion(int intIdServicio, int intIdInstalacion, clsEntSesion objSesion)
       {
           clsDatConexion objConexion = new clsDatConexion();
           DbConnection dbConexion = objConexion.getConexionREA(objSesion);
           DbCommand dbComando = objConexion.dbProvider.CreateCommand();
           DbTransaction dbTrans = dbConexion.BeginTransaction();
           
           int idUsuario = objSesion.usuario.IdUsuario;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "horario.spuObtenerHorarioServicioInstalacion";
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

               IDataReader idrDatosHorario = dbComando.ExecuteReader();
               List<REA.Entidades.clsEntHorario> lsHorario = new List<REA.Entidades.clsEntHorario>();

               while (idrDatosHorario.Read())
               {
                   REA.Entidades.clsEntHorario objDatosHorario = new REA.Entidades.clsEntHorario();
                   clsEntServicio  objServicio=new clsEntServicio();
                   objDatosHorario.Servicio=objServicio;
                   clsEntInstalacion objInstalacion= new clsEntInstalacion();
                   objDatosHorario.Instalacion=objInstalacion;

                   objDatosHorario.idHorario = conversiones.enteroNoNulo(idrDatosHorario["idHorario"]);
                   objDatosHorario.Servicio.idServicio=conversiones.enteroNoNulo(idrDatosHorario["idServicio"]);
                   objDatosHorario.Instalacion.IdInstalacion = conversiones.enteroNoNulo(idrDatosHorario["idInstalacion"]);
                   objDatosHorario.horNombre = conversiones.cadena(idrDatosHorario["horNombre"]);
                   objDatosHorario.horFechaInicio = conversiones.fechaNoNulo(idrDatosHorario["horFechaInicio"]);
                   objDatosHorario.horVigente = conversiones.boleanoNoNulo(idrDatosHorario["horVigente"]);
                   lsHorario.Add(objDatosHorario);

               }
               idrDatosHorario.Close();
               return lsHorario;


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
       public static REA.Entidades.clsEntHorario obtenerDatosHorario( int intidHorario, clsEntSesion objSesion)
       {
           clsDatConexion objConexion = new clsDatConexion();
           DbConnection dbConexion = objConexion.getConexionREA(objSesion);
           DbCommand dbComando = objConexion.dbProvider.CreateCommand();
           DbTransaction dbTrans = dbConexion.BeginTransaction();

           int idUsuario = objSesion.usuario.IdUsuario;
           dbComando.Transaction = dbTrans;
           try
           {
               dbComando.CommandType = CommandType.StoredProcedure;
               dbComando.CommandText = "horario.spuConsultarHorarioPorId";
               dbComando.Connection = dbConexion;
               dbComando.Parameters.Clear();

               DbParameter dbpidHorario = objConexion.dbProvider.CreateParameter();
               dbpidHorario.DbType = DbType.Int32;
               dbpidHorario.ParameterName = "@idHorario";
               dbpidHorario.Direction = ParameterDirection.Input;
               dbpidHorario.Value = intidHorario;
               dbComando.Parameters.Add(dbpidHorario);
               IDataReader idrDatosHorario = dbComando.ExecuteReader();
               REA.Entidades.clsEntHorario objDatosHorario = new REA.Entidades.clsEntHorario();
            
               if (idrDatosHorario.Read())
               {
                   // clsEntTipoHorarioREA objTipoHorario = new clsEntTipoHorarioREA();
                    clsEntHorarioComidaREA objHorarioComida= new clsEntHorarioComidaREA();
                   objDatosHorario.Comida= objHorarioComida;
                   objDatosHorario.idHorario=conversiones.enteroNoNulo(idrDatosHorario["idHorario"]);
                   objDatosHorario.horNombre=conversiones.cadena(idrDatosHorario["horNombre"]);
                   objDatosHorario.horDescripcion=conversiones.cadena(idrDatosHorario["horDescripcion"]);
                   objDatosHorario.horTipo =conversiones.caracterNoNulo(idrDatosHorario["horTipo"]);
                   objDatosHorario.horFechaInicio = conversiones.fechaNoNulo(idrDatosHorario["horFechaInicio"]);
                   objDatosHorario.horJornada = conversiones.objetoByteNoNulo(idrDatosHorario["horJornada"]);
                   objDatosHorario.horDescanso = conversiones.objetoByteNoNulo(idrDatosHorario["horDescanso"]);
                   objDatosHorario.horTolerancia = conversiones.objetoByteNoNulo(idrDatosHorario["horTolerancia"]);
                   objDatosHorario.horRetardo = conversiones.objetoByteNoNulo(idrDatosHorario["horRetardo"]);
                   objDatosHorario.horSalidaLaboral = conversiones.boleanoNoNulo(idrDatosHorario["horSalidaLaboral"]);
                   objDatosHorario.horModulo = conversiones.caracterNoNulo(idrDatosHorario["horModulo"]);
                   objDatosHorario.horTiempoMCS = conversiones.objetoByteNoNulo(idrDatosHorario["horTiempoMCS"]);
                   objDatosHorario.horDiaFestivo = conversiones.boleanoNoNulo(idrDatosHorario["horDiaFestivo"]);
                   objDatosHorario.Comida.hcComidaEntrada = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["horComidaEntrada"]);
                   objDatosHorario.Comida.hcComidaSalida = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["horComidaSalida"]);
                   objDatosHorario.Comida.hcMinuto = conversiones.objetoByteNoNulo(idrDatosHorario["HORMINUTO"]);
                   objDatosHorario.horHoraEntradaL = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdEntradaL"]);
                   objDatosHorario.horHoraSalidaL = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdSalidaL"]);
                   objDatosHorario.horHoraEntradaM = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdEntradaM"]);
                   objDatosHorario.horHoraSalidaM = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdSalidaM"]);
                   objDatosHorario.horHoraEntradaMi = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdEntradaMi"]);
                   objDatosHorario.horHoraSalidaMi = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdSalidaMi"]);
                   objDatosHorario.horHoraEntradaJue = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdEntradaJ"]);
                   objDatosHorario.horHoraSalidaJue = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdSalidaJ"]);
                   objDatosHorario.horHoraEntradaVie = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdEntradaV"]);
                   objDatosHorario.horHoraSalidaVie = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdSalidaV"]);
                   objDatosHorario.horHoraEntradaSa = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdEntradaS"]);
                   objDatosHorario.horHoraSalidaSa = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdSalidaS"]);
                   objDatosHorario.horHoraEntradaDom = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdEntradaD"]);
                   objDatosHorario.horHoraSalidaDom = conversiones.fechaNoNulo("1/1/0001 " + idrDatosHorario["hdSalidaD"]);
                   objDatosHorario.horVigente = conversiones.boleanoNoNulo(idrDatosHorario["hdVigente"]);
                   objDatosHorario.horAbierto = conversiones.boleanoNoNulo(idrDatosHorario["horAbierto"]);
                   objDatosHorario.horFinesSemana = conversiones.boleanoNoNulo(idrDatosHorario["horFinesSemana"]);
                   objDatosHorario.tipoHorarioREA = new clsEntTipoHorarioREA();
                   objDatosHorario.tipoHorarioREA.idTipoHorario = conversiones.objetoByteNoNulo(idrDatosHorario["idTipoHorario"]);

               }

               idrDatosHorario.Close();
               return objDatosHorario;
                 

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
       
    }
}
