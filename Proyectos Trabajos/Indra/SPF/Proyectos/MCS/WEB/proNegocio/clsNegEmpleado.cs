using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;


namespace SICOGUA.Negocio
{
    public class clsNegEmpleado
    {
        public static bool validarEmpleado(clsEntEmpleado objEmpleado)
        {
            if (objEmpleado.EmpNombre != "")
            {
                return true;
            }
            return false;
        }

        private static IEnumerable<clsEntEmpleado> generarListaEnObjetos(DataTable dtAsistencia, DateTime dtInstante, clsEntSesion objSesion)
        {
            List<clsEntEmpleado> lstEmpleados = new List<clsEntEmpleado>();

            foreach (DataRow drEmpleado in dtAsistencia.Rows)
            {
                clsEntEmpleado objEmpleado = new clsEntEmpleado();

                objEmpleado.EmpleadoHorario = new clsEntEmpleadoHorario();
                objEmpleado.EmpleadoHorario.horario = new clsEntHorario();
                objEmpleado.EmpleadoHorario.horario.tipoHorario = new clsEntTipoHorario();
                objEmpleado.EmpleadoHorario.horario.horarioComplemento = new clsEntHorarioComplemento();
                objEmpleado.Incidencias = new List<clsEntIncidencia>();
                //AGREGUE
                objEmpleado.PaseLista = new clsEntPaseLista();


                //objEmpleado.IdEmpleado = (Guid)drEmpleado["idEmpleado"];
                objEmpleado.EmpNumero = (int)drEmpleado["empNumero"];
                objEmpleado.EmpPaterno = (string)drEmpleado["empPaterno"];
                objEmpleado.EmpMaterno = (string)drEmpleado["empMaterno"];
                objEmpleado.EmpNombre = (string)drEmpleado["empNombre"];
                objEmpleado.observaciones = (string)drEmpleado["observaciones"];
                DataTable dtPaseLista = clsDatPaseLista.consultarPaseListaAnterior(objEmpleado.IdEmpleado, dtInstante, objSesion);
                foreach (DataRow drPaseLista in dtPaseLista.Rows)
                {
                    objEmpleado.PaseLista.IdEmpleado = (Guid)drPaseLista["idEmpleado"];
                    objEmpleado.PaseLista.IdEmpleadoHorario = (short)drPaseLista["idEmpleadoHorario"];
                    objEmpleado.PaseLista.IdPaseLista = (int)drPaseLista["idPaseLista"];
                    objEmpleado.PaseLista.IdTipoAsistencia = (byte)drPaseLista["idTipoAsistencia"];
                    objEmpleado.PaseLista.IdIncidencia = (int)drPaseLista["idIncidencia"];
                    objEmpleado.PaseLista.PlFecha = (DateTime)drPaseLista["plFecha"];
                }


                DataTable dtEmpleadoHorario = clsDatEmpleadoHorario.consultarEmpleadoHorario(objEmpleado.IdEmpleado, dtInstante, objSesion);

                foreach (DataRow drHorarioEmpleado in dtEmpleadoHorario.Rows)
                {
                    objEmpleado.EmpleadoHorario.IdEmpleado = (Guid)drHorarioEmpleado["idEmpleado"];
                    objEmpleado.EmpleadoHorario.IdEmpleadoHorario = (short)drHorarioEmpleado["idEmpleadoHorario"];
                    objEmpleado.EmpleadoHorario.IdHorario = (short)drHorarioEmpleado["idHorario"];
                    objEmpleado.EmpleadoHorario.horario.tipoHorario.IdTipoHorario = (short)drHorarioEmpleado["idTipoHorario"];
                    objEmpleado.EmpleadoHorario.EhFechaingreso = (DateTime)drHorarioEmpleado["ehFechaIngreso"];
                    objEmpleado.EmpleadoHorario.EhFechaBaja = (DateTime)drHorarioEmpleado["ehFechaBaja"];

                    DataTable dtHorario = clsDatHorario.consultarHorario(objEmpleado.EmpleadoHorario.IdHorario, objEmpleado.EmpleadoHorario.horario.tipoHorario.IdTipoHorario, objSesion);

                    foreach (DataRow drHorario in dtHorario.Rows)
                    {
                        objEmpleado.EmpleadoHorario.horario.IdHorario = (short)drHorario["idHorario"];
                        objEmpleado.EmpleadoHorario.horario.tipoHorario.IdTipoHorario = (short)drHorario["idTipoHorario"];
                        objEmpleado.EmpleadoHorario.horario.HorHoraEntrada = (string)drHorario["horHoraEntrada"];
                        objEmpleado.EmpleadoHorario.horario.HorHoraComida = (string)drHorario["horHoraComida"];
                        objEmpleado.EmpleadoHorario.horario.HorLunes = (bool)drHorario["horLunes"];
                        objEmpleado.EmpleadoHorario.horario.HorMartes = (bool)drHorario["horMartes"];
                        objEmpleado.EmpleadoHorario.horario.HorMiercoles = (bool)drHorario["horMiercoles"];
                        objEmpleado.EmpleadoHorario.horario.HorJueves = (bool)drHorario["horJueves"];
                        objEmpleado.EmpleadoHorario.horario.HorViernes = (bool)drHorario["horViernes"];
                        objEmpleado.EmpleadoHorario.horario.HorSabado = (bool)drHorario["horSabado"];
                        objEmpleado.EmpleadoHorario.horario.HorDomingo = (bool)drHorario["horDomingo"];
                        objEmpleado.EmpleadoHorario.horario.HorFestivo = (bool)drHorario["horFestivo"];
                        objEmpleado.EmpleadoHorario.horario.HorVigente = (bool)drHorario["horVigente"];

                        DataTable dtTipoHorario = clsDatTipoHorario.consultarTipoHorario(objEmpleado.EmpleadoHorario.horario.tipoHorario.IdTipoHorario, objSesion);

                        foreach (DataRow drTipoHorario in dtTipoHorario.Rows)
                        {
                            objEmpleado.EmpleadoHorario.horario.tipoHorario.IdTipoHorario = (short)drTipoHorario["idTipoHorario"];
                            objEmpleado.EmpleadoHorario.horario.tipoHorario.ThDescripcion = (string)drTipoHorario["thDescripcion"];
                            objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada = (byte)drTipoHorario["thJornada"];
                            objEmpleado.EmpleadoHorario.horario.tipoHorario.ThDescanso = (byte)drTipoHorario["thDescanso"];
                            objEmpleado.EmpleadoHorario.horario.tipoHorario.ThIncluyeComida = (bool)drTipoHorario["thIncluyeComida"];
                            objEmpleado.EmpleadoHorario.horario.tipoHorario.ThTiempoComida = (byte)drTipoHorario["thTiempoComida"];
                            objEmpleado.EmpleadoHorario.horario.tipoHorario.ThTolerancia = (byte)drTipoHorario["thTolerancia"];
                            objEmpleado.EmpleadoHorario.horario.tipoHorario.ThRetardo = (byte)drTipoHorario["thRetardo"];
                            objEmpleado.EmpleadoHorario.horario.tipoHorario.ThMixto = (bool)drTipoHorario["thMixto"];
                            objEmpleado.EmpleadoHorario.horario.tipoHorario.ThVigente = (bool)drTipoHorario["thVigente"];
                        }

                        DataTable dtHorarioComplemento = clsDatHorarioComplemento.consultarHorarioComplemento(objEmpleado.EmpleadoHorario.IdHorario, objEmpleado.EmpleadoHorario.horario.tipoHorario.IdTipoHorario, objSesion);

                        foreach (DataRow drHorarioComplemento in dtHorarioComplemento.Rows)
                        {
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.IdHorario = (short)drHorarioComplemento["idHorario"];
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.IdTipoHorario = (short)drHorarioComplemento["idTipoHorario"];
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.IdHorariocomplemento = (byte)drHorarioComplemento["idHorariocomplemento"];
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcHoraEntrada = (string)drHorarioComplemento["HcHoraEntrada"];
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcHoraComida = (string)drHorarioComplemento["HcHoraComida"];
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcLunes = (bool)drHorarioComplemento["HcLunes"];
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcMartes = (bool)drHorarioComplemento["HcMartes"];
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcMiercoles = (bool)drHorarioComplemento["HcMiercoles"];
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcJueves = (bool)drHorarioComplemento["HcJueves"];
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcViernes = (bool)drHorarioComplemento["HcViernes"];
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcSabado = (bool)drHorarioComplemento["HcSabado"];
                            objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcDomingo = (bool)drHorarioComplemento["HcDomingo"];
                        }
                    }
                }

                //dsGuardas._recursoHumano_incidenciaDataTable dtIncidencia = new dsGuardas._recursoHumano_incidenciaDataTable();

                //clsDatIncidencias.consultarIncidencias(objEmpleado.IdEmpleado, ref dtIncidencia, objSesion);

                //foreach (dsGuardas._recursoHumano_incidenciaRow drIncidencia in dtIncidencia.Rows)
                //{
                //    clsEntIncidencia objIncidencia = new clsEntIncidencia();
                //    objIncidencia.tipoIncidencia = new clsEntTipoIncidencia();

                //    objIncidencia.IdEmpleado = drIncidencia.idEmpleado;
                //    objIncidencia.IdIncidencia = drIncidencia.idIncidencia;
                //    objIncidencia.IdTipoIncidencia = drIncidencia.idTipoIncidencia;
                //    objIncidencia.tipoIncidencia.TiDescripcion = drIncidencia.tipoIncidencia;
                //    objIncidencia.IncFechaInicial = drIncidencia.incFechaInicial;
                //    objIncidencia.IncFechaFinal = drIncidencia.incFechaFinal;
                //    objIncidencia.IncDescripcion = drIncidencia.incDescripcion;

                //    objEmpleado.Incidencias.Add(objIncidencia);
                //}
                List<clsEntIncidencia> lisIncidencias = new List<clsEntIncidencia>();
                clsDatIncidencias.consultarIncidencias(objEmpleado.IdEmpleado, ref lisIncidencias, objSesion);
                objEmpleado.Incidencias = lisIncidencias;
                lstEmpleados.Add(objEmpleado);
            }

            return lstEmpleados;
        }

        public static Int32 buscarPorJornadaDescanso2(clsEntEmpleado objEmpleado, DateTime dtInstante, clsEntSesion objSesion)
        {
            // Información relacionada con el primer dia de trabajo, se basa en la Fecha de Ingreso
            // y la Hora de Entrada
            Int32 dia = objEmpleado.EmpleadoHorario.EhFechaingreso.Day;
            Int32 mes = objEmpleado.EmpleadoHorario.EhFechaingreso.Month;
            Int32 anio = objEmpleado.EmpleadoHorario.EhFechaingreso.Year;
            Int32 hora = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraEntrada.Split(':')[0]);
            Int32 minuto = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraEntrada.Split(':')[1]);

            Int32 jornada = objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada;
            Int32 descanso = objEmpleado.EmpleadoHorario.horario.tipoHorario.ThDescanso;

            // CASO 1: Si el horario del Guarda maneja un valor de Jornada y un valor de Descanso,
            // su horario es de tipo: 24x24, 12x24, 24x36.
            if (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada != 0
                && objEmpleado.EmpleadoHorario.horario.tipoHorario.ThDescanso != 0)
            {
                //Para el horario de Conmutador
                if (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada == 48 && objEmpleado.EmpleadoHorario.horario.tipoHorario.ThDescanso == 24)
                {
                    jornada = (jornada) / 2;
                    descanso = (descanso) / 2;
                    if (hora == 14)
                    {
                        hora = hora - 7;
                    }
                }



                // Primer Dia de Trabajo. Se contruye un objeto DateTime que almacena la información.
                DateTime primerDia = new DateTime(anio, mes, dia, hora, minuto, 0);

                // Objeto TimeSpan que almacena el tiempo que ha transcurrido desde el Primer Dia de Trabaja
                // hasta el momento en el que se desea generar la lista de asistencia.
                TimeSpan lapso = dtInstante.Subtract(primerDia);

                // Se calculan las Jornadas de Trabajo que ha tenido, esto se obtiene, dividiendo
                // el numero total de horas desde el primer dia, entre la suma de las horas de Jornada
                // y las horas de Descanso.
                double jornadas = lapso.TotalHours /
                                  (jornada + descanso);

                // Se aumenta el valor de la fecha del primer dia de trabajo, con el valor que corresponde
                // a una jornada completa de trabajo como tantas jornadas ha trabajado, obteniendo asi la
                // fecha del ultimo dia de trabajo.
                DateTime ultimoDia = primerDia;
                for (int i = 0; i < Convert.ToInt32(jornadas); i++)
                {
                    // Se calcula el ultimo dia de trabajo.
                    ultimoDia =
                        ultimoDia.AddHours(jornada + descanso);
                }

                // Si el dia no corresponde con hoy, no deberia presentarse a trabajar.
                if (ultimoDia.Day != dtInstante.Day || ultimoDia.Month != dtInstante.Month ||
                    ultimoDia.Year != dtInstante.Year)
                {
                    //FRANCO
                    return 2;
                }
                DateTime hoy = ultimoDia;

                // Sumo las horas de la jornada actual.
                ultimoDia = ultimoDia.AddHours(jornadas);

                // Si la fecha del dia de hoy es menor que el fin de jornada, se concluye que el guarda debe estar presente.
                if (dtInstante.CompareTo(hoy) >= 0 && dtInstante.CompareTo(ultimoDia) <= 0)
                {
                    //PRESENTE
                    return 1;
                }
            }

            #region CASO2
            // CASO 2
            else if (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada != 0
                    && objEmpleado.EmpleadoHorario.horario.tipoHorario.ThDescanso == 0
                    && objEmpleado.EmpleadoHorario.horario.tipoHorario.ThMixto == false)
            {
                // Si el Guarda no tiene horas de Descanso, significa que su horario es fijo con cierta hora de entrada y cierta hora de salida.
                DateTime diaLaboral = new DateTime(dtInstante.Year, dtInstante.Month, dtInstante.Day, hora, minuto, 0);
                DateTime hoy = diaLaboral;

                // Se aumentan las horas de jornada laboral.
                diaLaboral = diaLaboral.AddHours(objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada);

                if (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThIncluyeComida)
                {
                    // Se obtiene la hora de comida.
                    int mdHora = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraComida.Split(':')[0]);
                    int mdMinuto = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraComida.Split(':')[1]);

                    // Se crea el objeto de fecha donde se almacena la fecha antes de salir a comer
                    DateTime medioDia = new DateTime(dtInstante.Year, dtInstante.Month, dtInstante.Day, mdHora, mdMinuto, 0);

                    // Si el dia q se genera es menor al dia actual el guarda debe estar presente.
                    if (dtInstante.CompareTo(hoy) >= 0 && dtInstante.CompareTo((medioDia)) <= 0)
                    {
                        return 1;
                    }

                    // La segunda parte del dia.
                    medioDia = medioDia.AddHours(objEmpleado.EmpleadoHorario.horario.tipoHorario.ThTiempoComida);

                    // Se compara la segunda parte del dia.
                    if (dtInstante.CompareTo(medioDia) >= 0 && dtInstante.CompareTo(diaLaboral) <= 0)
                    {
                        return 1;
                    }

                    return 2;
                }

                // Si el dia q se genera es menor al dia actual el guarda debe estar presente.
                if (dtInstante.CompareTo(hoy) >= 0 && dtInstante.CompareTo((diaLaboral)) <= 0)
                {
                    return 1;
                }

                // En caso contrario esta ausente
                return 2;
            }
            #endregion

            #region CASO3
            //// CASO 3
            //else if (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThMixto)
            //{
            //    // Cuando el Guarda tiene horario mixto es necesario verificar su hora de entrada
            //    int cHora = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcHoraEntrada.Split(':')[0]);
            //    int cMinuto = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcHoraEntrada.Split(':')[1]);

            //    // Se crea una objeto DateTime para obtener la fecha de inicio de jornada laboral.
            //    DateTime diaLaboral = new DateTime(dtInstante.Year, dtInstante.Month, dtInstante.Day, cHora, cMinuto, 0);
            //    DateTime hoy = diaLaboral;

            //    // Se aumentan las horas de jornada laboral.
            //    diaLaboral = diaLaboral.AddHours(objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada);

            //    if (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThIncluyeComida)
            //    {
            //        // Se obtiene la hora de comida.
            //        int mdHora = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraComida.Split(':')[0]);
            //        int mdMinuto = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraComida.Split(':')[1]);

            //        // Se crea el objeto de fecha donde se almacena la fecha antes de salir a comer
            //        DateTime medioDia = new DateTime(dtInstante.Year, dtInstante.Month, dtInstante.Day, mdHora, mdMinuto, 0);

            //        // Si el dia q se genera es menor al dia actual el guarda debe estar presente.
            //        if (dtInstante.CompareTo(hoy) >= 0 && dtInstante.CompareTo((medioDia)) <= 0)
            //        {
            //            return 1;
            //        }

            //        // La segunda parte del dia.
            //        medioDia = medioDia.AddHours(objEmpleado.EmpleadoHorario.horario.tipoHorario.ThTiempoComida);

            //        // Se compara la segunda parte del dia.
            //        if (dtInstante.CompareTo(medioDia) >= 0 && dtInstante.CompareTo(diaLaboral) <= 0)
            //        {
            //            return 1;
            //        }

            //        return 2;
            //    }

            //    // Si el dia q se genera es menor al dia actual el guarda debe estar presente.
            //    if (dtInstante.CompareTo(hoy) >= 0 && dtInstante.CompareTo((diaLaboral)) <= 0)
            //    {
            //        return 1;
            //    }

            //    // En caso contrario esta ausente
            //    return 2;
            //}
            #endregion

            return 1;
        }

        private static bool buscarPorJornadaDescanso(clsEntEmpleado objEmpleado, DateTime dtInstante)
        {
            // Información relacionada con el primer dia de trabajo, se basa en la Fecha de Ingreso
            // y la Hora de Entrada
            Int32 dia = objEmpleado.EmpleadoHorario.EhFechaingreso.Day;
            Int32 mes = objEmpleado.EmpleadoHorario.EhFechaingreso.Month;
            Int32 anio = objEmpleado.EmpleadoHorario.EhFechaingreso.Year;
            Int32 hora = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraEntrada.Split(':')[0]);
            Int32 minuto = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraEntrada.Split(':')[1]);

            //// CASO 1: Si el horario del Guarda maneja un valor de Jornada y un valor de Descanso,
            //// su horario es de tipo: 24x24, 12x24, 24x36.
            //if (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada != 0
            //    && objEmpleado.EmpleadoHorario.horario.tipoHorario.ThDescanso != 0)
            //{
            //    // Primer Dia de Trabajo. Se contruye un objeto DateTime que almacena la información.
            //    DateTime primerDia = new DateTime(anio, mes, dia, hora, minuto, 0);

            //    // Objeto TimeSpan que almacena el tiempo que ha transcurrido desde el Primer Dia de Trabaja
            //    // hasta el momento en el que se desea generar la lista de asistencia.
            //    TimeSpan lapso = dtInstante.Subtract(primerDia);

            //    // Se calculan las Jornadas de Trabajo que ha tenido, esto se obtiene, dividiendo
            //    // el numero total de horas desde el primer dia, entre la suma de las horas de Jornada
            //    // y las horas de Descanso.
            //    double jornadas = lapso.TotalHours /
            //                      (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada +
            //                       objEmpleado.EmpleadoHorario.horario.tipoHorario.ThDescanso);

            //    // Se aumenta el valor de la fecha del primer dia de trabajo, con el valor que corresponde
            //    // a una jornada completa de trabajo como tantas jornadas ha trabajado, obteniendo asi la
            //    // fecha del ultimo dia de trabajo.
            //    DateTime ultimoDia = primerDia;
            //    for (int i = 0; i < Convert.ToInt32(jornadas); i++)
            //    {
            //        // Se calcula el ultimo dia de trabajo.
            //        ultimoDia =
            //            ultimoDia.AddHours(objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada +
            //                               objEmpleado.EmpleadoHorario.horario.tipoHorario.ThDescanso);
            //    }

            //    // Si el dia no corresponde con hoy, no deberia presentarse a trabajar.
            //    if (ultimoDia.Day != dtInstante.Day || ultimoDia.Month != dtInstante.Month || ultimoDia.Year != dtInstante.Year)
            //    {
            //        return false;
            //    }
            //    DateTime hoy = ultimoDia;

            //    // Sumo las horas de la jornada actual.
            //    ultimoDia = ultimoDia.AddHours(objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada);
            //    // Si la fecha del dia de hoy es menor que el fin de jornada, se concluye que el guarda debe estar presente.
            //    if (dtInstante.CompareTo(hoy) >= 0 && dtInstante.CompareTo(ultimoDia) <= 0)
            //    {
            //        return true;
            //    }
            //}
            //else if (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada != 0
            //    && objEmpleado.EmpleadoHorario.horario.tipoHorario.ThDescanso == 0
            //    && objEmpleado.EmpleadoHorario.horario.tipoHorario.ThMixto == false)
            //{
            //    // Si el Guarda no tiene horas de Descanso, significa que su horario es fijo con cierta hora de entrada y cierta hora de salida.
            //    DateTime diaLaboral = new DateTime(dtInstante.Year, dtInstante.Month, dtInstante.Day, hora, minuto, 0);
            //    DateTime hoy = diaLaboral;

            //    // Se aumentan las horas de jornada laboral.
            //    diaLaboral = diaLaboral.AddHours(objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada);

            //    if (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThIncluyeComida)
            //    {
            //        // Se obtiene la hora de comida.
            //        int mdHora = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraComida.Split(':')[0]);
            //        int mdMinuto = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraComida.Split(':')[1]);

            //        // Se crea el objeto de fecha donde se almacena la fecha antes de salir a comer
            //        DateTime medioDia = new DateTime(dtInstante.Year, dtInstante.Month, dtInstante.Day, mdHora, mdMinuto, 0);

            //        // Si el dia q se genera es menor al dia actual el guarda debe estar presente.
            //        if (dtInstante.CompareTo(hoy) >= 0 && dtInstante.CompareTo((medioDia)) <= 0)
            //        {
            //            return true;
            //        }

            //        // La segunda parte del dia.
            //        medioDia = medioDia.AddHours(objEmpleado.EmpleadoHorario.horario.tipoHorario.ThTiempoComida);

            //        // Se compara la segunda parte del dia.
            //        if (dtInstante.CompareTo(medioDia) >= 0 && dtInstante.CompareTo(diaLaboral) <= 0)
            //        {
            //            return true;
            //        }

            //        return false;
            //    }

            //    // Si el dia q se genera es menor al dia actual el guarda debe estar presente.
            //    if (dtInstante.CompareTo(hoy) >= 0 && dtInstante.CompareTo((diaLaboral)) <= 0)
            //    {
            //        return true;
            //    }

            //    // En caso contrario esta ausente
            //    return false;
            //}
            //else if (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThMixto)
            //{
            //    // Cuando el Guarda tiene horario mixto es necesario verificar su hora de entrada
            //    int cHora = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcHoraEntrada.Split(':')[0]);
            //    int cMinuto = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.horarioComplemento.HcHoraEntrada.Split(':')[1]);

            //    // Se crea una objeto DateTime para obtener la fecha de inicio de jornada laboral.
            //    DateTime diaLaboral = new DateTime(dtInstante.Year, dtInstante.Month, dtInstante.Day, cHora, cMinuto, 0);
            //    DateTime hoy = diaLaboral;

            //    // Se aumentan las horas de jornada laboral.
            //    diaLaboral = diaLaboral.AddHours(objEmpleado.EmpleadoHorario.horario.tipoHorario.ThJornada);

            //    if (objEmpleado.EmpleadoHorario.horario.tipoHorario.ThIncluyeComida)
            //    {
            //        // Se obtiene la hora de comida.
            //        int mdHora = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraComida.Split(':')[0]);
            //        int mdMinuto = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraComida.Split(':')[1]);

            //        // Se crea el objeto de fecha donde se almacena la fecha antes de salir a comer
            //        DateTime medioDia = new DateTime(dtInstante.Year, dtInstante.Month, dtInstante.Day, mdHora, mdMinuto, 0);

            //        // Si el dia q se genera es menor al dia actual el guarda debe estar presente.
            //        if (dtInstante.CompareTo(hoy) >= 0 && dtInstante.CompareTo((medioDia)) <= 0)
            //        {
            //            return true;
            //        }

            //        // La segunda parte del dia.
            //        medioDia = medioDia.AddHours(objEmpleado.EmpleadoHorario.horario.tipoHorario.ThTiempoComida);

            //        // Se compara la segunda parte del dia.
            //        if (dtInstante.CompareTo(medioDia) >= 0 && dtInstante.CompareTo(diaLaboral) <= 0)
            //        {
            //            return true;
            //        }

            //        return false;
            //    }

            //    // Si el dia q se genera es menor al dia actual el guarda debe estar presente.
            //    if (dtInstante.CompareTo(hoy) >= 0 && dtInstante.CompareTo((diaLaboral)) <= 0)
            //    {
            //        return true;
            //    }

            //    // En caso contrario esta ausente
            //    return false;
            //}

            //return false;
            return true;
        }

        public static Int32 buscarPorPaseListaAnterior(clsEntEmpleado objEmpleado, DateTime dtInstante, clsEntSesion objSesion)
        {
            //// Información relacionada con el primer dia de trabajo, se basa en la Fecha de Ingreso
            //// y la Hora de Entrada
            //Int32 dia = objEmpleado.EmpleadoHorario.EhFechaingreso.Day;
            //Int32 mes = objEmpleado.EmpleadoHorario.EhFechaingreso.Month;
            //Int32 anio = objEmpleado.EmpleadoHorario.EhFechaingreso.Year;
            //Int32 hora = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraEntrada.Split(':')[0]);
            //Int32 minuto = Convert.ToInt32(objEmpleado.EmpleadoHorario.horario.HorHoraEntrada.Split(':')[1]);

            // Comprueba si en el Pase de Lista anterior tiene (4)FALTA y si es verdadero entonces debe estar presente en la lista
            if (objEmpleado.PaseLista.IdTipoAsistencia == 4)
            {
                return 1;
            }

            return 0;
        }

        private static void buscarIncidencias(clsEntEmpleado objEmpleado, DateTime dtInstante)
        {
            foreach (clsEntIncidencia objIncidencia in objEmpleado.Incidencias)
            {
                if (dtInstante.CompareTo(objIncidencia.IncFechaInicial) >= 0 && dtInstante.CompareTo(objIncidencia.IncFechaFinal) <= 0)
                {
                    int a = 0;
                }
            }
        }

        public static List<clsEntEmpleadoAsistencia> generarListaFinal(bool deshabilitarLista, clsEntEmpleadoAsignacion objAsignacion,/* clsEntEmpleadoAsignacionOS objAsignacionOs,*/int idHorario, char entradaSalida, DateTime dtInstante, clsEntSesion objSesion)
        {
            DataTable dTable = new DataTable();
            if (entradaSalida == '1')
                dTable = clsDatPaseLista.consultaListaAsistencia(objAsignacion, idHorario, dtInstante, objSesion);
            else
                dTable = clsDatPaseLista.consultaListaAsistenciaSalida(objAsignacion, idHorario, dtInstante, objSesion);
            //IEnumerable<clsEntEmpleado> lstEmpleados = generarListaEnObjetos(clsDatPaseLista.consultaListaAsistencia(objAsignacion, /*objAsignacionOs, */idHorario,dtInstante, objSesion), dtInstante, objSesion);
            List<clsEntEmpleadoAsistencia> lstLista = new List<clsEntEmpleadoAsistencia>();

            foreach (DataRow objEmpleado in dTable.Rows)
            {
                //if (buscarPorJornadaDescanso(objEmpleado, dtInstante))
                //{
                clsEntEmpleadoAsistencia objLista = new clsEntEmpleadoAsistencia();
                objLista.IdEmpleado = (Guid)objEmpleado[0];
                objLista.EmpPaterno = objEmpleado[1].ToString();
                objLista.EmpMaterno = objEmpleado[2].ToString();
                objLista.EmpNombre = objEmpleado[3].ToString();
                objLista.EmpNumero = (int)objEmpleado[4];
                objLista.horario = objEmpleado[5].ToString();
                objLista.idHorario = Convert.ToInt16(objEmpleado[6]);
                objLista.idAsignacionHorario = (int)objEmpleado[7];
                objLista.desactivarPase = Convert.ToBoolean(objEmpleado[8]);
                objLista.estDescripcion = objEmpleado[9].ToString();
                objLista.entradaHM = objEmpleado[10].ToString().Length < 1 ? objEmpleado[10].ToString() : (objEmpleado[10].ToString()).Substring(0, 5);
                objLista.incDescripcion = objEmpleado[11].ToString();
                objLista.FechaEntrada = Convert.ToDateTime(objEmpleado[12]);
                objLista.idAsistencia = objEmpleado[13].ToString().Length < 1 ? 0 : Convert.ToInt32(objEmpleado[13]);
                objLista.salida = Convert.ToBoolean(objEmpleado[14]);
                objLista.observaciones = objEmpleado[15].ToString();
                objLista.franco = (int)objEmpleado[16];
                objLista.textInfo = Convert.ToBoolean(objEmpleado[17]);
                /*si trae fecha de baja diferente a 1900 es porque se trata de un pase de salida y entonces es preciso comparar para deshactivar el txtbox de Salida del grid view*/
                if (Convert.ToDateTime(objEmpleado[18]) != Convert.ToDateTime("1900-01-01"))
                {
                    if (DateTime.Now <= Convert.ToDateTime(objEmpleado[18]))
                        objLista.deshabilitarLista = true;
                    else
                        objLista.deshabilitarLista = false;
                }
                else
                    objLista.deshabilitarLista = deshabilitarLista;
                lstLista.Add(objLista);
                objLista.retardo = objEmpleado[19].ToString();
                objLista.falta = objEmpleado[20].ToString();

                //}
            }


            return lstLista;
        }
        public static DataTable consultarServicioInstalacionHorarioREA(Guid idEmpleado, DateTime fecha, clsEntSesion objSesion)
        {
            DataTable dTable = new DataTable();
            return dTable = clsDatEmpleado.consultarServicioInstalacionHorarioREA(idEmpleado, fecha, objSesion);
        }

        public static void consultarEmpleado(ref clsEntEmpleado objEmpleado, clsEntSesion objSesion)
        {
            #region DataSet

            dsGuardas._empleado_guardaDataTable dsEmpleado = new dsGuardas._empleado_guardaDataTable();
            clsDatEmpleado.consultarEmpleado(objEmpleado.IdEmpleado, ref dsEmpleado, objSesion);

            #endregion

            #region Empleado

            if (dsEmpleado.Rows.Count > 0)
            {
                foreach (dsGuardas._empleado_guardaRow obj in dsEmpleado.Rows)
                {
                    objEmpleado.IdEmpleado = obj.idEmpleado;
                    objEmpleado.IdPais = obj.idPais;
                    objEmpleado.Pais = obj.paiDescripcion;
                    objEmpleado.IdEstado = obj.idEstado;
                    objEmpleado.Estado = obj.estDescripcion;
                    objEmpleado.IdMunicipio = obj.idMunicipio;
                    objEmpleado.Municipio = obj.munDescripcion;
                    objEmpleado.EmpNombre = obj.empNombre;
                    objEmpleado.EmpPaterno = obj.empPaterno;
                    objEmpleado.EmpMaterno = obj.empMaterno;
                    objEmpleado.EmpSexo = obj.empSexo.ToCharArray()[0];
                    objEmpleado.EmpSexoValor = obj.empSexo;
                    objEmpleado.EmpCURP = obj.empCurp;
                    objEmpleado.EmpNumero = obj.empNumero;
                    objEmpleado.EmpRFC = obj.empRfc;
                    objEmpleado.EmpFechaIngreso = obj.empFechaIngreso;
                    objEmpleado.EmpFechaBaja = obj.empFechaBaja;
                    objEmpleado.EmpFechaNacimiento = obj.empFechaNacimiento;
                    objEmpleado.EmpCUIP = obj.empCuip;
                    objEmpleado.Sangre = obj.mfDescripcion;
                    objEmpleado.EmpLOC = obj.empLoc;
                    objEmpleado.EmpCurso = obj.empCurso;
                    objEmpleado.EmpCartilla = obj.empCartilla;
                    objEmpleado.idRevision = obj.idRevision;
                    objEmpleado.idRenuncia = obj.idRenuncia;



                }
            }

            #endregion

            #region Incidencias

            clsNegIncidencias.consultarIncidencias(ref objEmpleado, objSesion);

            #endregion

            #region Puesto

            clsNegEmpleadoPuesto.consultarEmpleadoPuesto(ref objEmpleado, objSesion);

            #endregion

            #region Asignación

            clsNegEmpleadoAsignacion.consultarEmpleadoAsignacion(ref objEmpleado, objSesion);

            #endregion

            #region Asignación OS

            clsNegEmpleadoAsignacionOS.consultarEmpleadoAsignacionOS(ref objEmpleado, objSesion);

            #endregion


        }
        public static void consultarPaseListaREA(ref clsEntEmpleado objEmpleado, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            clsEntPaseLista objPaseLista = new clsEntPaseLista { IdEmpleado = objEmpleado.IdEmpleado, PlFecha = DateTime.Now };
            if (clsDatAsistencia.consultarOpcionREA(dbConexion, dbTrans, objSesion, objPaseLista) > 0)
            {
                objEmpleado.PaseLista = objPaseLista;
            }

        }


        public static void consultarPaseLista(ref clsEntEmpleado objEmpleado, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            DataTable dtEmpleadoHorario = clsDatEmpleadoHorario.consultarEmpleadoHorario(objEmpleado.IdEmpleado, DateTime.Now, objSesion);
            foreach (DataRow drHorarioEmpleado in dtEmpleadoHorario.Rows)
            {
                objEmpleado.EmpleadoHorario.IdEmpleadoHorario = (short)drHorarioEmpleado["idEmpleadoHorario"];
            }
            clsEntPaseLista objPaseLista = new clsEntPaseLista { IdEmpleado = objEmpleado.IdEmpleado, IdEmpleadoHorario = objEmpleado.EmpleadoHorario.IdEmpleadoHorario, PlFecha = DateTime.Now };
            if (clsDatAsistencia.consultarOpcion(dbConexion, dbTrans, objSesion, objPaseLista) > 0)
            {
                objEmpleado.PaseLista = objPaseLista;
            }

        }
        public static List<clsEntEmpleadoHorarioREA> consultaHorariosREA(clsEntSesion objSesion, Guid idEmplado, DateTime datFechaInicio, DateTime datFechaFin, int idServicio, int idInstalacion)
        {
            return clsDatEmpleadoHorarioREA.consultaHorarios(objSesion, idEmplado, datFechaInicio, datFechaFin,idServicio,idInstalacion);
        }


        public static clsEntEmpleadosListaGenerica consultarIntegrantes(DateTime fechaAsistencia, string empNumero, int idHorario, clsEntSesion objSesion, ref byte existe, int idServicio, int idInstalacion, ref string excep)
        {
            clsEntEmpleadosListaGenerica obj = new clsEntEmpleadosListaGenerica();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            DataSet dsResult = new DataSet();
            clsDatEmpleado.buscarEmpleadoNumero(fechaAsistencia,empNumero, idHorario, ref dsResult, objSesion, ref existe, idServicio, idInstalacion, ref excep);

            if (dsResult.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dsResult.Tables[0].Rows)
                {
                    obj.idEmpleado = new Guid(dataRow[0].ToString());
                    obj.empPaterno = dataRow[1].ToString();
                    obj.empMaterno = dataRow[2].ToString();
                    obj.empNombre = dataRow[3].ToString();
                    obj.empNumero = empNumero;
                }
            }


            return obj.empNumero==null?null:obj;

        }

        public static DataTable consultarInmovilidadPorEmpleado(Guid idEmpleado, clsEntSesion objSesion)
        {
            DataTable dTable = new DataTable();
            return dTable = clsDatEmpleado.consultarInmovilidadPorEmpleado(idEmpleado, objSesion);
        }

        public static bool insertarEmpleadoInmovilidad(List<clsEntEmpleadoInmovilidad> lstEmpleadoInmovilidad, clsEntSesion objSesion)
        {
            foreach (clsEntEmpleadoInmovilidad obj in lstEmpleadoInmovilidad)
            {
                if (obj.estatus == true)
                    clsDatEmpleado.insertarEmpleadoInmovilidad(obj, objSesion);
            }
            return true;
        }
    }
}
