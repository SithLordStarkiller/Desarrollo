using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using REA.Entidades;
using REA.Datos;

using System.Text;

namespace SICOGUA.Negocio
{
    public class clsNegAnexoTecnico
    {


        public static List<clsEntAnexoTecnico>  consultarAnexos(int intServicio, int intInstalacion, clsEntSesion objSesion)
        {
            List<clsEntAnexoTecnico> lisAnexosRegresa= clsDataAnexoTecnico.consultarAnexoTecnico(intServicio, intInstalacion, objSesion);
            return lisAnexosRegresa;

        }
        public static void consultarAnexoDetalle(ref clsEntAnexoTecnico objAnexo, clsEntSesion objSesion)
        {
            clsDataAnexoTecnico.consultarAnexoTecnicoDetalle(objSesion, ref objAnexo);
            if (objAnexo.anexoJerarquiaHorario.Count != 0)
            {
                for (int i = 0; i < objAnexo.anexoJerarquiaHorario.Count; i++)
                {
                    if (objAnexo.anexoJerarquiaHorario[i].totalHombres != 0 || objAnexo.anexoJerarquiaHorario[i].totalMujeres != 0)
                    {
                        objAnexo.anexoJerarquiaHorario[i].lunes = "H: " + objAnexo.anexoJerarquiaHorario[i].masculinoLunes.ToString() + " M: " + objAnexo.anexoJerarquiaHorario[i].femeninoLunes.ToString();
                        objAnexo.anexoJerarquiaHorario[i].martes = "H: " + objAnexo.anexoJerarquiaHorario[i].masculinoMartes.ToString() + " M: " + objAnexo.anexoJerarquiaHorario[i].femeninoMartes.ToString();
                        objAnexo.anexoJerarquiaHorario[i].miercoles = "H: " + objAnexo.anexoJerarquiaHorario[i].masculinoMiercoles.ToString() + " M: " + objAnexo.anexoJerarquiaHorario[i].femeninoMiercoles.ToString();
                        objAnexo.anexoJerarquiaHorario[i].jueves = "H: " + objAnexo.anexoJerarquiaHorario[i].masculinoJueves.ToString() + " M: " + objAnexo.anexoJerarquiaHorario[i].femeninoJueves.ToString();
                        objAnexo.anexoJerarquiaHorario[i].viernes = "H: " + objAnexo.anexoJerarquiaHorario[i].masculinoViernes.ToString() + " M: " + objAnexo.anexoJerarquiaHorario[i].femeninoViernes.ToString();
                        objAnexo.anexoJerarquiaHorario[i].sabado = "H: " + objAnexo.anexoJerarquiaHorario[i].masculinoSabado.ToString() + " M: " + objAnexo.anexoJerarquiaHorario[i].femeninoSabado.ToString();
                        objAnexo.anexoJerarquiaHorario[i].domingo = "H: " + objAnexo.anexoJerarquiaHorario[i].masculinoDomingo.ToString() + " M: " + objAnexo.anexoJerarquiaHorario[i].femeninoDomingo.ToString();
                    }
                    else
                    {
                        objAnexo.anexoJerarquiaHorario[i].lunes = objAnexo.anexoJerarquiaHorario[i].indistintoLunes.ToString();
                        objAnexo.anexoJerarquiaHorario[i].martes = objAnexo.anexoJerarquiaHorario[i].indistintoMartes.ToString();
                        objAnexo.anexoJerarquiaHorario[i].miercoles = objAnexo.anexoJerarquiaHorario[i].indistintoMiercoles.ToString();
                        objAnexo.anexoJerarquiaHorario[i].jueves = objAnexo.anexoJerarquiaHorario[i].indistintoJueves.ToString();
                        objAnexo.anexoJerarquiaHorario[i].viernes = objAnexo.anexoJerarquiaHorario[i].indistintoViernes.ToString();
                        objAnexo.anexoJerarquiaHorario[i].sabado = objAnexo.anexoJerarquiaHorario[i].indistintoSabado.ToString();
                        objAnexo.anexoJerarquiaHorario[i].domingo = objAnexo.anexoJerarquiaHorario[i].indistintoDomingo.ToString();
                    }
                
                }
            }
        }

        public static List<clsEntAnexoTecnico> consultarServicioInstalacion(int intServicio, int intInstalacion, clsEntSesion objSesion)
        {
            List<clsEntAnexoTecnico> lisServicioInstalacion = clsDataAnexoTecnico.consultarServicioInstalacion(intServicio, intInstalacion, objSesion);
            return lisServicioInstalacion;

        }

        public static int consultaIdTipoHorario(string procedimiento, string thDescripcion, string thTurno, clsEntSesion objSesion)
        {
            return clsDataAnexoTecnico.consultaIdTipoHorario(procedimiento, thDescripcion, thTurno, objSesion);
        }

        public static bool insertarAnexo(ref List<clsEntAnexoTecnico> lisAnexos, clsEntSesion objSesion)
        {
            return clsDataAnexoTecnico.insertarAnexo(ref lisAnexos, objSesion);
        }
    }
}
