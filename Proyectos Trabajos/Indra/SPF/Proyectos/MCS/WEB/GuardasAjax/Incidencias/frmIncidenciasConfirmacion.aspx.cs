using System;
using System.Collections.Generic;
using System.Text;
using proUtilerias;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;

public partial class Incidencias_frmIncidenciasConfirmacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["objEmpleado" + Session.SessionID] != null)
            {
                clsEntEmpleado objEmpleado = new clsEntEmpleado();

                objEmpleado = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];

                clsUtilerias.llenarLabel(lblNumero, objEmpleado.EmpNumero, "-");
                clsUtilerias.llenarLabel(lblCuip, objEmpleado.EmpCUIP, "-");
                clsUtilerias.llenarLabel(lblPaterno, objEmpleado.EmpPaterno, "-");
                clsUtilerias.llenarLabel(lblMaterno, objEmpleado.EmpMaterno, "-");
                clsUtilerias.llenarLabel(lblNombre, objEmpleado.EmpNombre, "-");

                #region Incidencias

                grvIncidencias.DataSource = objEmpleado.Incidencias;
                grvIncidencias.DataBind();

                #endregion
            }
            else
            {
                Response.Redirect("~/Incidencias/frmIncidencias.aspx");
            }
        }
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        panIncidencias.Visible = false;
        StringBuilder sbUrl = new StringBuilder();

        try
        {
            List<clsEntIncidencia> lstEliminarIncidencias = Session["lstEliminarIncidencias" + Session.SessionID] != null
                                              ? (List<clsEntIncidencia>)Session["lstEliminarIncidencias" + Session.SessionID]
                                              : new List<clsEntIncidencia>();
            clsEntEmpleado objEmpleado = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];
            if (clsNegIncidencias.insertarRecursoHumanoIncidencias(objEmpleado, lstEliminarIncidencias, (clsEntSesion)Session["objSession" + Session.SessionID]))
            {
                clsNegEmpleado.consultarEmpleado(ref objEmpleado, (clsEntSesion)Session["objSession" + Session.SessionID]);
                Session["objEmpleado" + Session.SessionID] = objEmpleado;

                Session["lstIncidencias" + Session.SessionID] = null;
                Session["lstEliminarIncidencias" + Session.SessionID] = null;

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
        Session["objEmpleado" + Session.SessionID] = null;
        Response.Redirect("~/frmInicio.aspx");
    }
}
