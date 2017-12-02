using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SICOGUA.Datos;
using SICOGUA.Seguridad;

namespace SICOGUA.Negocio
{
    public class clsNegAsignacionLunes
    {
        public static clsDatAsignacionLunes.regresaValidacionLunes validaFechaAsignacion(clsEntSesion objSesion)
        {
            
            clsDatAsignacionLunes.regresaValidacionLunes regresa = clsDatAsignacionLunes.regresaValidacionLunes.Normal;
            // si la fecha de hoy no es lunes no se realiza la validación 
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {
                // se valida si el viernes fue festivo
                clsDatAsignacionLunes.regresaValidacionLunes regresaTemporal = clsDatAsignacionLunes.validaDiaFestivo(DateTime.Today.AddDays(-3), objSesion);
                if (regresaTemporal == clsDatAsignacionLunes.regresaValidacionLunes.Festivo)
                {
                    regresa = clsDatAsignacionLunes.regresaValidacionLunes.LunesFestivo;
                }
                else
                {
                    regresa = clsDatAsignacionLunes.regresaValidacionLunes.Lunes;
                }
            }
            else
            {
                //si la fecha de hoy no un dia despues de una fecha festiva
                // valido si el dia de ayer fue lunes
                if (DateTime.Today.AddDays(-1).DayOfWeek == DayOfWeek.Monday)
                {
                    clsDatAsignacionLunes.regresaValidacionLunes regresaTemporal = clsDatAsignacionLunes.validaDiaFestivo(DateTime.Today.AddDays(-1), objSesion);
                    if (regresaTemporal == clsDatAsignacionLunes.regresaValidacionLunes.Festivo)
                    {
                        regresa = clsDatAsignacionLunes.regresaValidacionLunes.LunesFestivo;
                    }
                    else
                    {
                        regresa = clsDatAsignacionLunes.regresaValidacionLunes.Normal;
                    }
                }
                else
                {
                    regresa = clsDatAsignacionLunes.validaDiaFestivo(DateTime.Today.AddDays(-1), objSesion);
                }

            }


            return regresa;
        }

        public static string  validaFechas(DateTime datInicioAsignacion, string datFinAsignacion, clsEntSesion objSesion, clsDatAsignacionLunes.regresaValidacionLunes enumValida)
        {
            string strRegresa = "";
            
            if (enumValida == clsDatAsignacionLunes.regresaValidacionLunes.Lunes)
            {
               
                    if (datFinAsignacion.Length >0  && DateTime.Parse( datFinAsignacion) < DateTime.Today.AddDays(-3))
                    {

                        strRegresa= "No es posible continuar la fecha de cierre tiene que ser mayor o igual al día viernes pasado próximo";
                       
                    }
            }
            
            
            if (enumValida == clsDatAsignacionLunes.regresaValidacionLunes.Festivo)
            {
               
                    if (datFinAsignacion.Length > 0 && DateTime.Parse(datFinAsignacion) < DateTime.Today.AddDays(-2))
                    {

                        strRegresa= "No es posible continuar la fecha de cierre tiene que ser mayor o igual al día previo al día festivo de ayer";
                       
                    }
                }

            if (enumValida == clsDatAsignacionLunes.regresaValidacionLunes.LunesFestivo)
            {

                if (datFinAsignacion.Length > 0 && DateTime.Parse(datFinAsignacion) < DateTime.Today.AddDays(-4))
                {

                    strRegresa = "No es posible continuar la fecha de cierre tiene que ser mayor al último día laboral";

                }
            }

            return strRegresa;
        }
    }
}
