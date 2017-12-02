using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Seguridad;
using proUtilerias;
using SICOGUA.Datos;
using System.Data;
using System.Text;
using proEntidades;

public partial class Usuarios_frmUsuarioInstalacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string IdUsuario = Request.QueryString["IdUsuario"];

            if (!string.IsNullOrEmpty(IdUsuario))
            {
                hfIdUsuario.Value = IdUsuario;

                clsEntUsuario objUsuario = clsDatUsuario.consultarUsuario(Int16.Parse(IdUsuario), (clsEntSesion)Session["objSession" + Session.SessionID]);

                if (objUsuario != null)
                {
                    DataTable dt = new DataTable();

                    clsUtilerias.llenarLabel(lblPaterno, objUsuario.UsuPaterno, "-");
                    clsUtilerias.llenarLabel(lblMaterno, objUsuario.UsuMaterno, "-");
                    clsUtilerias.llenarLabel(lblNombre, objUsuario.UsuNombre, "-");

                    clsCatalogos.llenarCatalogo(lstZona, "catalogo.spLeerZona", "zonDescripcion", "idZona", (clsEntSesion)Session["objSession" + Session.SessionID]);
                    //clsCatalogos.llenarCatalogo(lstServicio, "catalogo.spLeerServicio", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
                    lstServicio.Items.Clear();
                    lstServicio.Items.Add(new ListItem("SELECCIONAR", ""));
                    lstServicio.AppendDataBoundItems = true;

                    //clsCatalogos.llenarCatalogoInstalacionPorServicioZona(lstInstalacion, "catalogo.spLeerInstalacionesPorServicioZona", "insNombre", "idInstalacion", 0, 0, (clsEntSesion)Session["objSession" + Session.SessionID]);

                    lstInstalacion.Items.Clear();
                    lstInstalacion.Items.Add(new ListItem("SELECCIONAR", ""));
                    lstInstalacion.AppendDataBoundItems = true;
                }
            }
        }
    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Usuarios/frmUsuarios.aspx");
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        // Guarda permisos para instalacion, servicio, zona        
        StringBuilder sbUrl = new StringBuilder();
        string strZoneValue = lstZona.SelectedValue;
        string strServicio = lstServicio.SelectedValue;
        string strInstalacion = lstInstalacion.SelectedValue;

        short idUsuario = string.IsNullOrEmpty(hfIdUsuario.Value) ? (short)0 : short.Parse(hfIdUsuario.Value);
        bool usiConsultar = chklstPermisos.Items[0].Selected;
        bool usiAsignar = chklstPermisos.Items[1].Selected;
        bool esVigente = chklstPermisos.Items[2].Selected;

        if (clsDatUsuario.actualizarUsuarioServicioInstalacion(idUsuario,
            strZoneValue,
            strServicio,
            strInstalacion,
             usiConsultar,
            usiAsignar,
            esVigente,
            (clsEntSesion)Session["objSession" + Session.SessionID]))
        {
            sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("¡Actualización de permisos exitosa!"));
        }
        else
        {
            sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("¡Error al actualizar permisos!"));
        }
        Response.Redirect(sbUrl.ToString());
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Usuarios/frmUsuarios.aspx");
    }
    protected void lstZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        // cambio  para evitar inconsistencia entre catalogos zona y servicio

        lstServicio.SelectedIndex = 0;
        string strZoneValue = lstZona.SelectedValue;
        //string strServicio = lstServicio.SelectedValue;

        short idZona = string.IsNullOrEmpty(strZoneValue) ? (short)0 : short.Parse(strZoneValue);        
        
        Dictionary<string, clsEntParameter> parameters = new Dictionary<string, clsEntParameter>();
        parameters.Add("@idZona", new clsEntParameter { Type = DbType.Int16, Value = idZona });

        if (idZona != 0)
        {
            clsCatalogos.llenarCatalogoConParametros(lstServicio, "catalogo.spLeerServicioPorZona", "serDescripcion", "idServicio", parameters, (clsEntSesion)Session["objSession" + Session.SessionID]);
        }
        else
        {
            lstServicio.Items.Clear();
            lstServicio.Items.Add(new ListItem("SELECCIONAR", ""));
            lstServicio.AppendDataBoundItems = true;
        }

        clsCatalogos.llenarCatalogoInstalacionPorServicioZona(lstInstalacion, "catalogo.spLeerInstalacionesPorServicioZona", "insNombre", "idInstalacion", idZona, 0, (clsEntSesion)Session["objSession" + Session.SessionID]);
    }

    protected void lstServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strServicio = lstServicio.SelectedValue;
        short idServicio = string.IsNullOrEmpty(strServicio) ? (short)0 : short.Parse(strServicio);
        string strZoneValue = lstZona.SelectedValue;
        short idZona = string.IsNullOrEmpty(strZoneValue) ? (short)0 : short.Parse(strZoneValue);        

        clsCatalogos.llenarCatalogoInstalacionPorServicioZona(lstInstalacion, "catalogo.spLeerInstalacionesPorServicioZona", "insNombre", "idInstalacion", idZona, idServicio, (clsEntSesion)Session["objSession" + Session.SessionID]);
    }
}