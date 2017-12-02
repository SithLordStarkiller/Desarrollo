using System;
using SICOGUA.Entidades;
using SICOGUA.Datos;
using SICOGUA.Seguridad;

namespace SICOGUA.Negocio
{
    public class clsNegReporteIncidente
    {
        public static void consultarReporteIncidente(ref clsEntReporteIncidente objIncidente, clsEntSesion objSesion)
        {
            dsGuardas._operacionServicio_ReporteIncidenteDataTable dtReporte = clsDatReporteIncidente.consultarReporteIncidente(objIncidente, objSesion);

            objIncidente.Servicio = new clsEntServicio();
            objIncidente.Instalacion = new clsEntInstalacion();

            objIncidente.JerarquiaEmpleadoInvolucrado = new clsEntJerarquia();
            objIncidente.ZonaEmpleadoInvolucrado = new clsEntZona();

            objIncidente.JerarquiaEmpleadoTomaNota = new clsEntJerarquia();
            objIncidente.ZonaEmpleadoTomaNota = new clsEntZona();

            objIncidente.JerarquiaEmpleadoAutor = new clsEntJerarquia();
            objIncidente.ZonaEmpleadoAutor = new clsEntZona();

            objIncidente.JerarquiaEmpleadoSuperior = new clsEntJerarquia();
            objIncidente.ZonaEmpleadoSuperior = new clsEntZona();

            if(dtReporte.Rows.Count == 1)
            {
                foreach (dsGuardas._operacionServicio_ReporteIncidenteRow dr in dtReporte.Rows)
                {
                    #region Incidente
                    
                    objIncidente.Servicio.idServicio = dr.idServicio;
                    objIncidente.Instalacion.IdInstalacion = dr.idInstalacion;
                    objIncidente.RiFechaHora = dr.riFechaHora;
                    objIncidente.RiLugar = dr.riLugar; 

                    #endregion

                    #region Personal Involucrado En Los Hechos

                    objIncidente.JerarquiaEmpleadoInvolucrado.IdJerarquia = dr.idJerarquiaInvo;
                    objIncidente.ZonaEmpleadoInvolucrado.IdZona = dr.idZonaInvo;
                    if (!string.IsNullOrEmpty(dr.idEmpleadoInvo))
                    {
                        objIncidente.IdEmpleadoInvolucrado = new Guid(dr.idEmpleadoInvo.Split('&')[0]);
                        objIncidente.NoEmpleadoInvolucrado = Convert.ToInt32(dr.idEmpleadoInvo.Split('&')[1]);
                        objIncidente.IdEmpleadoPuestoInvolucrado = Convert.ToInt16(dr.idEmpleadoInvo.Split('&')[2]);
                    }

                    objIncidente.RiActividad = dr.riActividad;
                    objIncidente.RiUniforme = dr.riUniforme;
                    objIncidente.RiDesarrolloConsecuencia = dr.riDesarrolloConsecuencia;
                    objIncidente.RiLesion = dr.riLesion;
                    objIncidente.RiUbicacionCadaverLesionado = dr.riUbicacionCadaverLesionado;
                    objIncidente.RiAccionVsAgresor = dr.riAccionVSAgresor;

                    #endregion

                    #region Autoridad que Tomo Nota de los Hechos

                    objIncidente.JerarquiaEmpleadoTomaNota.IdJerarquia = dr.idJerarquiaNota;
                    objIncidente.ZonaEmpleadoTomaNota.IdZona = dr.idZonaNota;
                    if (!string.IsNullOrEmpty(dr.idEmpleadoNota))
                    {
                        objIncidente.IdEmpleadoTomaNota = new Guid(dr.idEmpleadoNota.Split('&')[0]);
                        objIncidente.NoEmpleadoTomaNota = Convert.ToInt32(dr.idEmpleadoNota.Split('&')[1]);
                        objIncidente.IdEmpleadoPuestoTomaNota = Convert.ToInt16(dr.idEmpleadoNota.Split('&')[2]);
                    }
                    objIncidente.RiAccionMando = dr.riAccionMando;

                    #endregion

                    #region Autor del Parte Inicial

                    objIncidente.JerarquiaEmpleadoAutor.IdJerarquia = dr.idJerarquiaAutor;
                    objIncidente.ZonaEmpleadoAutor.IdZona = dr.idZonaAutor;
                    if (!string.IsNullOrEmpty(dr.idEmpleadoAutor))
                    {
                        objIncidente.IdEmpleadoAutor = new Guid(dr.idEmpleadoAutor.Split('&')[0]);
                        objIncidente.NoEmpleadoAutor = Convert.ToInt32(dr.idEmpleadoAutor.Split('&')[1]);
                        objIncidente.IdEmpleadoPuestoAutor = Convert.ToInt16(dr.idEmpleadoAutor.Split('&')[2]);
                    }
                    
                    #endregion

                    #region Superior del Autor del Parte Inicial

                    objIncidente.JerarquiaEmpleadoSuperior.IdJerarquia = dr.idJerarquiaSuperior;
                    objIncidente.ZonaEmpleadoSuperior.IdZona = dr.idZonaSuperior;
                    if (!string.IsNullOrEmpty(dr.idEmpleadoSuperior))
                    {
                        objIncidente.IdEmpleadoSuperior = new Guid(dr.idEmpleadoSuperior.Split('&')[0]);
                        objIncidente.NoEmpleadoSuperior = Convert.ToInt32(dr.idEmpleadoSuperior.Split('&')[1]);
                        objIncidente.IdEmpleadoPuestoSuperior = Convert.ToInt16(dr.idEmpleadoSuperior.Split('&')[2]);
                    }

                    #endregion

                    #region En caso de accidente Aéreo o Terrestre

                    objIncidente.RiDanioMaterial = dr.riDanioMaterial;
                    objIncidente.RiMonto = dr.riMonto;
                    
                    #endregion
                }
            }
        }
    }
}
