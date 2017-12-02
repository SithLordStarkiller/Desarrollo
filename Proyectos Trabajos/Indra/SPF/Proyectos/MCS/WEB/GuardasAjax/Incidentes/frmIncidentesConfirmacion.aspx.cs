using System;
using SICOGUA.Entidades;
using SICOGUA.Datos;
using System.Text;
using proUtilerias;
using SICOGUA.Seguridad;

public partial class Incidentes_frmIncidentesConfirmacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if (Session["objIncidente" + Session.SessionID] != null)
            {
                clsEntReporteIncidente objIncidente = new clsEntReporteIncidente();
                objIncidente = (clsEntReporteIncidente)Session["objIncidente" + Session.SessionID];

                #region Incidencia

                clsUtilerias.llenarLabel(lblServicio, objIncidente.Servicio.serDescripcion, "-");
                clsUtilerias.llenarLabel(lblInstalacion, objIncidente.Instalacion.InsNombre, "-");
                clsUtilerias.llenarLabel(lblFecha, objIncidente.RiFechaHora.ToShortDateString(), "-");
                clsUtilerias.llenarLabel(lblHora, objIncidente.RiFechaHora.ToShortTimeString(), "-");
                clsUtilerias.llenarLabel(lblLugar, objIncidente.RiLugar, "-");

                #endregion

                #region Personal Involucrado En Los Hechos

                clsUtilerias.llenarLabel(lblZona, objIncidente.ZonaEmpleadoInvolucrado.ZonDescripcion, "-");
                clsUtilerias.llenarLabel(lblGrado, objIncidente.JerarquiaEmpleadoInvolucrado.JerDescripcion, "-");
                clsUtilerias.llenarLabel(lblNomCompleto, objIncidente.NombreEmpleadoInvolucrado, "-");
                clsUtilerias.llenarLabel(lblNoEmpleado, objIncidente.NoEmpleadoInvolucrado, "-");
                clsUtilerias.llenarLabel(lblActividad, objIncidente.RiActividad, "-");
                clsUtilerias.llenarLabel(lblUniformeCivil, objIncidente.RiUniforme, "-");
                clsUtilerias.llenarLabel(lblHecCon, objIncidente.RiDesarrolloConsecuencia, "-");
                clsUtilerias.llenarLabel(lblCuaLes, objIncidente.RiLesion, "-");
                clsUtilerias.llenarLabel(lblCadLes, objIncidente.RiUbicacionCadaverLesionado, "-");
                clsUtilerias.llenarLabel(lblAccConAgr, objIncidente.RiAccionVsAgresor, "-");
                
                #endregion

                #region Autoridad que Tomo Nota de los Hechos

                clsUtilerias.llenarLabel(lblAutZon, objIncidente.ZonaEmpleadoTomaNota.ZonDescripcion, "-");
                clsUtilerias.llenarLabel(lblAutGra, objIncidente.JerarquiaEmpleadoTomaNota.JerDescripcion, "-");
                clsUtilerias.llenarLabel(lblAutNom, objIncidente.NombreEmpleadoTomaNota, "-");
                clsUtilerias.llenarLabel(lblAutNum, objIncidente.NoEmpleadoTomaNota, "-");
                clsUtilerias.llenarLabel(lblAccionMando, objIncidente.RiAccionMando, "-");

                #endregion

                #region Autor del Parte Inicial

                clsUtilerias.llenarLabel(lblParIniZon, objIncidente.ZonaEmpleadoAutor.ZonDescripcion, "-");
                clsUtilerias.llenarLabel(lblParIniGra, objIncidente.JerarquiaEmpleadoAutor.JerDescripcion, "-");
                clsUtilerias.llenarLabel(lblParIniNom, objIncidente.NombreEmpleadoAutor, "-");
                clsUtilerias.llenarLabel(lblParIniNum, objIncidente.NoEmpleadoAutor, "-");
                
                #endregion

                #region Superior del Autor del Parte Inicial

                clsUtilerias.llenarLabel(lblSupAutZon, objIncidente.ZonaEmpleadoSuperior.ZonDescripcion, "-");
                clsUtilerias.llenarLabel(lblSupAutGra, objIncidente.JerarquiaEmpleadoSuperior.JerDescripcion, "-");
                clsUtilerias.llenarLabel(lblSupAutNom, objIncidente.NombreEmpleadoSuperior, "-");
                clsUtilerias.llenarLabel(lblSupAutNum, objIncidente.NoEmpleadoSuperior, "-");
                
                #endregion

                #region En caso de accidente Aéreo o Terrestre

                clsUtilerias.llenarLabel(lblDanMat, objIncidente.RiDanioMaterial, "-");
                clsUtilerias.llenarLabel(lblMonto, objIncidente.RiMonto, "-");
                
                #endregion
            }
        }
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        StringBuilder sbUrl = new StringBuilder();

        try
        {
            clsEntReporteIncidente objIncidente = (clsEntReporteIncidente)Session["objIncidente" + Session.SessionID];

            if (clsDatReporteIncidente.insertarActualizarReporteIncidente(objIncidente, (clsEntSesion)Session["objSession" + Session.SessionID]))
            {
                Session["objIncidente" + Session.SessionID] = null;
                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito."));
            }
        }
        catch (Exception)
        {
            sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est101&strMensaje=" + Server.UrlEncode("Ha ocurrido un error durante la operación. Intentelo más tarde ó contacte a un Administrador."));
        }
        finally
        {
            Response.Redirect(sbUrl.ToString());
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session["objIncidente" + Session.SessionID] = null;
        Response.Redirect("~/frmInicio.aspx");
    }


}
