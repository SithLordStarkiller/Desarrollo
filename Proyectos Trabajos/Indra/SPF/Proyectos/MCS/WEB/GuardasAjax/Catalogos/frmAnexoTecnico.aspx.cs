using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Negocio;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using SICOGUA.Datos;
using proUtilerias;
using REA.Negocio;
using REA.Datos;
using REA.Entidades;
using System.Data;
using System.Text;

public partial class Catalogos_frmAnexoTecnico : System.Web.UI.Page
{
    List<clsEntAnexoTecnico> lisAnexoTecnico = new List<clsEntAnexoTecnico>();
    List<clsEntAnexoJerarquiaHorario> lisAnexoJerarquia = new List<clsEntAnexoJerarquiaHorario>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["lisAnexos"] = Session["lisAnexoJerarquia"] = null;
            Session["idHorario"] = null;
            Session["impresion" + Session.SessionID] = null;
            if (Session["servicio"] != null && Session["instalacion"] != null)
            {
                clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
                List<clsEntAnexoTecnico> lisServicioInstalacion = new List<clsEntAnexoTecnico>();
                lisServicioInstalacion = clsNegAnexoTecnico.consultarServicioInstalacion((int)Session["servicio"], (int)Session["instalacion"], (clsEntSesion)Session["objSession" + Session.SessionID]);
                lblZona.Text = lisServicioInstalacion[0].zona.ZonDescripcion;
                lblServicio.Text = lisServicioInstalacion[0].servicio.serDescripcion;
                lbInstalacion.Text = lisServicioInstalacion[0].instalacion.InsNombre;
                ViewState["fechaInicioInstalacion"] =
                lblFechaInicioInstalacion.Text = lisServicioInstalacion[0].instalacion.insFechaInicio.ToShortDateString();
                lblFechaBajaInstalacion.Text = lisServicioInstalacion[0].instalacion.insFechaFin.ToShortDateString() == "01/01/1900" ? "--/--/----" : lisServicioInstalacion[0].instalacion.insFechaFin.ToShortDateString();

                List<clsEntAnexoTecnico> lisAnexoTecnico = new List<clsEntAnexoTecnico>();
                lisAnexoTecnico = clsNegAnexoTecnico.consultarAnexos((int)Session["servicio"], (int)Session["instalacion"], (clsEntSesion)Session["objSession" + Session.SessionID]);
                if (lisAnexoTecnico.Count > 0)
                {
                    Session["lisAnexos"] =
                    grvAnexo.DataSource = lisAnexoTecnico;
                    grvAnexo.DataBind();                    
                }
                if (Session["vigencia"].ToString() == "NO")
                    btnGuardarAnexos.Enabled = btnNuevoRegistro.Enabled = btnNuevo.Enabled = btnGuardar.Enabled = false;
                else
                    btnGuardarAnexos.Enabled = btnNuevoRegistro.Enabled = true;
               
            }
        }
    }
    protected void btnGuardarAnexos_Click(object sender, EventArgs e)
    {
        StringBuilder sbUrl = new StringBuilder();
        lisAnexoTecnico = (List<clsEntAnexoTecnico>)Session["lisAnexos"];
        if (lisAnexoTecnico != null)
        {
            if (clsDataAnexoTecnico.insertarAnexo(ref lisAnexoTecnico, (clsEntSesion)Session["objSession" + Session.SessionID]))
            {
                hfBanderaNuevo.Value = "";
                grvAnexo.DataSource = lisAnexoTecnico;
                grvAnexo.DataBind();
                Session["lisAnexos"] = Session["lisAnexoJerarquia"] = null;
                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito."));
                Response.Redirect(sbUrl.ToString());
            }
        }
        else
        {
            sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito."));
            Response.Redirect(sbUrl.ToString());
        }
    }
    protected void btnCancelarAnexos_Click(object sender, EventArgs e)
    {
        Session["lisAnexos"] = Session["lisAnexoJerarquia"] = null;
        Response.Redirect("~/frmInicio.aspx");
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Servicio/frmBuscarInstalacion.aspx");
    }

    protected void grvAnexo_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        hfBanderaNuevo.Value = "";
        btnNuevo.Enabled = btnGuardar.Enabled = true;

        txbFechaInicioConvenio.Enabled = txbNoConvenio.Enabled = imbNacimiento.Enabled = false;
        clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];

        if (objSesion.usuario.Perfil.IdPerfil == 1 && Session["vigencia"].ToString() == "SI")
            btnNuevo.Enabled = btnGuardar.Enabled = true;
        else
            btnNuevo.Enabled = btnGuardar.Enabled = false;


        lisAnexoTecnico = (List<clsEntAnexoTecnico>)Session["lisAnexos"];
        clsEntAnexoTecnico objAnexo = lisAnexoTecnico[e.RowIndex];
        ViewState["anexoSeleccionado"] = e.RowIndex;
        clsNegAnexoTecnico.consultarAnexoDetalle(ref objAnexo, objSesion);
        lisAnexoJerarquia = objAnexo.anexoJerarquiaHorario;
        Session["lisAnexoJerarquia"] = lisAnexoJerarquia;

        grvJerarquias.DataSource = objAnexo.anexoJerarquiaHorario;
        grvJerarquias.DataBind();
        List<clsEntAnexoTecnico> lisAnexoTemporal = lisAnexoTecnico.FindAll(delegate(clsEntAnexoTecnico p) { return p.fechaInicio > objAnexo.fechaInicio; });
        txbNoConvenio.Text = objAnexo.strConvenio;
        txbFechaInicioConvenio.Text = objAnexo.fechaInicio.ToShortDateString();
        popDetalle.Show();
    }
    protected void grvAnexo_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        hfNuevoAnexo.Value = "";
        popDetalle.Hide();
    }
    protected void btnNuevoRegistro_Click(object sender, EventArgs e)
    {
        clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
        popDetalle.Show();
        Session["lisAnexoJerarquia"] = null;
        //if (/*objSesion.usuario.Perfil.IdPerfil != 1 &&*/ Session["vigencia"].ToString() == "SI")
        btnNuevo.Enabled = btnGuardar.Enabled = true;

        hfBanderaNuevo.Value = "-1";
        txbFechaInicioConvenio.Enabled = txbNoConvenio.Enabled = imbNacimiento.Enabled = true;
        txbFechaInicioConvenio.Text = "";
        txbNoConvenio.Text = "";
        grvJerarquias.DataSource = "";
        grvJerarquias.DataBind();

    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        int i = 0;
        string strMensaje = string.Empty;
        #region validaciones
        if (string.IsNullOrEmpty(txbNoConvenio.Text))
            strMensaje = "Debe ingresar el convenio";
        if (string.IsNullOrEmpty(txbFechaInicioConvenio.Text)) 
            strMensaje = "Debe ingresar la fecha de inicio"; 

       
        if (!string.IsNullOrEmpty(strMensaje))
        {
            wucMensajeAnexo.Mensaje(strMensaje);
            popDetalle.Show();
            return;
        }
        #endregion

        clsEntAnexoTecnico objAnexo = new clsEntAnexoTecnico();
        objAnexo.servicio = new clsEntServicio();
        objAnexo.instalacion = new clsEntInstalacion();
        objAnexo.servicio.idServicio = (int)Session["servicio"];
        objAnexo.instalacion.IdServicio = (int)Session["servicio"];
        objAnexo.instalacion.IdInstalacion = (int)Session["instalacion"];        
        objAnexo.fechaInicio = Convert.ToDateTime(txbFechaInicioConvenio.Text);
        objAnexo.fechaFin = Convert.ToDateTime("01/01/1900");
        objAnexo.strConvenio = txbNoConvenio.Text;
        if (Session["lisAnexoJerarquia"] != null)
        {
            objAnexo.anexoJerarquiaHorario = new List<clsEntAnexoJerarquiaHorario>();
            objAnexo.anexoJerarquiaHorario = (List<clsEntAnexoJerarquiaHorario>)Session["lisAnexoJerarquia"];
        }

        if (hfBanderaNuevo.Value == "-1")/*Registro Nuevo*/
        {
            /*Verificar que la fecha de inicio no sea menor a la fecha de inicio de la instalacion*/
            if (Convert.ToDateTime(ViewState["fechaInicioInstalacion"]) > Convert.ToDateTime(txbFechaInicioConvenio.Text))
                strMensaje = "Revise la información, la fecha de inicio no puede ser menor o igual a: " + ViewState["fechaInicioInstalacion"];  

            if (Session["lisAnexos"] != null)
                ((List<clsEntAnexoTecnico>)Session["lisAnexos"]).FindAll(
                    /*Encuentra el registro repetido en caso de ser nuevo*/
                   delegate(clsEntAnexoTecnico bk)
                   {
                       if (bk.fechaFin == Convert.ToDateTime("01/01/1900") && bk.fechaInicio >= Convert.ToDateTime(txbFechaInicioConvenio.Text))
                       {
                           strMensaje = "Revise la información, la fecha de inicio no puede ser menor o igual a: " + bk.fechaInicio.ToShortDateString();
                           return true;
                       }

                       i++;
                       return false;
                   });
            if (!string.IsNullOrEmpty(strMensaje))
            {
                wucMensajeAnexo.Mensaje(strMensaje);
                popDetalle.Show();
                return;
            }
            else
            {
                List<clsEntAnexoTecnico> lisAnexos = (List<clsEntAnexoTecnico>)Session["lisAnexos"];
                if (lisAnexos == null)
                    /*Permite guardar el primer registro*/
                    lisAnexos = new List<clsEntAnexoTecnico> { objAnexo };
                else
                {
                    lisAnexos.Add(objAnexo);
                }
                
                Session["lisAnexos"] = lisAnexos;
                if (((List<clsEntAnexoTecnico>)Session["lisAnexos"]).Count > 1)
                    ((List<clsEntAnexoTecnico>)Session["lisAnexos"])[i - 1].fechaFin = Convert.ToDateTime(txbFechaInicioConvenio.Text).AddDays(-1);

            }
        }
        else
        {
            ((List<clsEntAnexoTecnico>)Session["lisAnexos"]).FindAll(
                /*Encuentra el registro a modificar*/
                 delegate(clsEntAnexoTecnico bk)
                 {
                     if (bk.fechaFin <= Convert.ToDateTime(txbFechaInicioConvenio.Text) && bk.strConvenio == txbNoConvenio.Text)
                     {
                         ((List<clsEntAnexoTecnico>)Session["lisAnexos"])[i] = objAnexo;
                         return true;
                     }
                     i++;
                     return false;
                 });
        }
        hfBanderaNuevo.Value = "";
        grvAnexo.DataSource = Session["lisAnexos"];
        grvAnexo.DataBind();
        popDetalle.Hide();
    }

    protected void limpiarAnexo()
    {
        ddlJerarquia.SelectedValue = ddlTipoHorario.SelectedValue = ddlTurno.SelectedValue = string.Empty;
        ddlTurno.Items.Clear();
        chbSexo.Checked = false;
        txbHombresDomingo.Text = txbHombresJueves.Text = txbHombresLunes.Text = txbHombresMartes.Text = txbHombresMiercoles.Text = txbHombresSabado.Text = txbHombresViernes.Text =
            txbMujeresDomingo.Text = txbMujeresJueves.Text = txbMujeresLunes.Text = txbMujeresMartes.Text = txbMujeresMiercoles.Text = txbMujeresSabado.Text = txbMujeresViernes.Text =
            txbIndistintoDomingo.Text = txbIndistintoJueves.Text = txbIndistintoLunes.Text = txbIndistintoMartes.Text = txbIndistintoMiercoles.Text = txbIndistintoSabado.Text = txbIndistintoViernes.Text =
            string.Empty;
        txbHombresDomingo.Enabled = txbHombresJueves.Enabled = txbHombresLunes.Enabled = txbHombresMartes.Enabled = txbHombresMiercoles.Enabled = txbHombresSabado.Enabled = txbHombresViernes.Enabled =
         txbMujeresDomingo.Enabled = txbMujeresJueves.Enabled = txbMujeresLunes.Enabled = txbMujeresMartes.Enabled = txbMujeresMiercoles.Enabled = txbMujeresSabado.Enabled = txbMujeresViernes.Enabled = false;
        txbIndistintoDomingo.Enabled = txbIndistintoJueves.Enabled = txbIndistintoLunes.Enabled = txbIndistintoMartes.Enabled = txbIndistintoMiercoles.Enabled = txbIndistintoSabado.Enabled = txbIndistintoViernes.Enabled = true;
    }
    protected string validaciones()
    {
        string strMensaje = string.Empty;
        int totalHombres = 0;
        int totalMujeres = 0;
        int totalIndistinto = 0;

        if (string.IsNullOrEmpty(ddlTipoHorario.SelectedValue)) { strMensaje = "Debe seleccionar Tipo Horario"; return strMensaje; }
        if (string.IsNullOrEmpty(ddlTurno.SelectedValue)) {
            if (ddlTipoHorario.SelectedItem.Text != "24X24")
            {
                strMensaje = "Debe seleccionar Turno Horario"; return strMensaje;
            }
        }
        if (string.IsNullOrEmpty(ddlJerarquia.SelectedValue)) { strMensaje = "Debe seleccionar Jerarquía"; return strMensaje; }

        if (chbSexo.Checked)
        {
            totalHombres = (txbHombresLunes.Text == "" ? 0 : Convert.ToInt32(txbHombresLunes.Text))
                            + (txbHombresMartes.Text == "" ? 0 : Convert.ToInt32(txbHombresMartes.Text))
                            + (txbHombresMiercoles.Text == "" ? 0 : Convert.ToInt32(txbHombresMiercoles.Text))
                            + (txbHombresJueves.Text == "" ? 0 : Convert.ToInt32(txbHombresJueves.Text))
                            + (txbHombresViernes.Text == "" ? 0 : Convert.ToInt32(txbHombresViernes.Text))
                            + (txbHombresSabado.Text == "" ? 0 : Convert.ToInt32(txbHombresSabado.Text))
                            + (txbHombresDomingo.Text == "" ? 0 : Convert.ToInt32(txbHombresDomingo.Text));
            totalMujeres = (txbMujeresLunes.Text == "" ? 0 : Convert.ToInt32(txbMujeresLunes.Text))
                            + (txbMujeresMartes.Text == "" ? 0 : Convert.ToInt32(txbMujeresMartes.Text))
                            + (txbMujeresMiercoles.Text == "" ? 0 : Convert.ToInt32(txbMujeresMiercoles.Text))
                            + (txbMujeresJueves.Text == "" ? 0 : Convert.ToInt32(txbMujeresJueves.Text))
                            + (txbMujeresViernes.Text == "" ? 0 : Convert.ToInt32(txbMujeresViernes.Text))
                            + (txbMujeresSabado.Text == "" ? 0 : Convert.ToInt32(txbMujeresSabado.Text))
                            + (txbMujeresDomingo.Text == "" ? 0 : Convert.ToInt32(txbMujeresDomingo.Text));
            if (totalHombres == 0 && totalMujeres == 0) { strMensaje = "Debe ingresar valores en Hombres o Mujeres"; return strMensaje; }
        }
        else
        {
            totalIndistinto = (txbIndistintoLunes.Text == "" ? 0 : Convert.ToInt32(txbIndistintoLunes.Text))
                           + (txbIndistintoMartes.Text == "" ? 0 : Convert.ToInt32(txbIndistintoMartes.Text))
                           + (txbIndistintoMiercoles.Text == "" ? 0 : Convert.ToInt32(txbIndistintoMiercoles.Text))
                           + (txbIndistintoJueves.Text == "" ? 0 : Convert.ToInt32(txbIndistintoJueves.Text))
                           + (txbIndistintoViernes.Text == "" ? 0 : Convert.ToInt32(txbIndistintoViernes.Text))
                           + (txbIndistintoSabado.Text == "" ? 0 : Convert.ToInt32(txbIndistintoSabado.Text))
                           + (txbIndistintoDomingo.Text == "" ? 0 : Convert.ToInt32(txbIndistintoDomingo.Text));
            if (totalIndistinto == 0) strMensaje = "Debe ingresar valores en Indistinto";
            return strMensaje;
        }
        return strMensaje;
    }
    protected void grvJerarquias_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
        if (objSesion.usuario.Perfil.IdPerfil == 1)
        {
            hfNuevoAnexo.Value = "";
            ddlJerarquia.Enabled = ddlTipoHorario.Enabled = ddlTurno.Enabled = false;
            lisAnexoJerarquia = (List<clsEntAnexoJerarquiaHorario>)Session["lisAnexoJerarquia"];
            clsEntAnexoJerarquiaHorario objAnexoJerarquia = lisAnexoJerarquia[e.RowIndex];
            clsCatalogos.llenarTipoHorario(ddlTipoHorario, "catalogo.spuLeerTipoHorario", "thDescripcion", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarTipoHorario(ddlTurno, "catalogo.spuLeerTurno", "thTurno", objAnexoJerarquia.thHorario, (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoJerarquia(ddlJerarquia, "catalogo.spLeerJerarquia", "jerDescripcion", "idJerarquia", (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlJerarquia.SelectedIndex = ddlJerarquia.Items.IndexOf(ddlJerarquia.Items.FindByText(objAnexoJerarquia.jerDescripcion));
            ddlTipoHorario.SelectedIndex = ddlTipoHorario.Items.IndexOf(ddlTipoHorario.Items.FindByValue(objAnexoJerarquia.thHorario));
            ddlTurno.SelectedIndex = ddlTurno.Items.IndexOf(ddlTurno.Items.FindByValue(objAnexoJerarquia.thTurno));

            #region totalesDia
            if (objAnexoJerarquia.totalHombres > 0 || objAnexoJerarquia.totalMujeres > 0)
            {
                chbSexo.Checked = true;
                chbSexo_CheckedChanged(null, null);
                txbHombresLunes.Text = objAnexoJerarquia.masculinoLunes.ToString();
                txbHombresMartes.Text = objAnexoJerarquia.masculinoMartes.ToString();
                txbHombresMiercoles.Text = objAnexoJerarquia.masculinoMiercoles.ToString();
                txbHombresJueves.Text = objAnexoJerarquia.masculinoJueves.ToString();
                txbHombresViernes.Text = objAnexoJerarquia.masculinoViernes.ToString();
                txbHombresSabado.Text = objAnexoJerarquia.masculinoSabado.ToString();
                txbHombresDomingo.Text = objAnexoJerarquia.masculinoDomingo.ToString();
                txbMujeresLunes.Text = objAnexoJerarquia.femeninoLunes.ToString();
                txbMujeresMartes.Text = objAnexoJerarquia.femeninoMartes.ToString();
                txbMujeresMiercoles.Text = objAnexoJerarquia.femeninoMiercoles.ToString();
                txbMujeresJueves.Text = objAnexoJerarquia.femeninoJueves.ToString();
                txbMujeresViernes.Text = objAnexoJerarquia.femeninoViernes.ToString();
                txbMujeresSabado.Text = objAnexoJerarquia.femeninoSabado.ToString();
                txbMujeresDomingo.Text = objAnexoJerarquia.femeninoDomingo.ToString();
            }
            else
            {
                chbSexo.Checked = false;
                chbSexo_CheckedChanged(null, null);
                txbIndistintoLunes.Text = objAnexoJerarquia.indistintoLunes.ToString();
                txbIndistintoMartes.Text = objAnexoJerarquia.indistintoMartes.ToString();
                txbIndistintoMiercoles.Text = objAnexoJerarquia.indistintoMiercoles.ToString();
                txbIndistintoJueves.Text = objAnexoJerarquia.indistintoJueves.ToString();
                txbIndistintoViernes.Text = objAnexoJerarquia.indistintoViernes.ToString();
                txbIndistintoSabado.Text = objAnexoJerarquia.indistintoSabado.ToString();
                txbIndistintoDomingo.Text = objAnexoJerarquia.indistintoDomingo.ToString();
            }
            #endregion
            hfBanderaNuevo.Value = "0";
            popDetalle.Hide();
            mpeDetalle.Show();
        }
    }
    protected void grvJerarquias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
            lisAnexoTecnico = (List<clsEntAnexoTecnico>)Session["lisAnexos"];
            if (lisAnexoTecnico != null)
            {
                GridViewRow row = (GridViewRow)e.Row;
                TableCell id = row.Cells[11];
                Control imbSeleccionar = id.FindControl("imbSeleccionar");
                Image imaSeleccionar = (Image)imbSeleccionar;
                if ((lisAnexoTecnico.Count - 1) == Convert.ToInt32(ViewState["anexoSeleccionado"] == null ? 0 : ViewState["anexoSeleccionado"]) && objSesion.usuario.Perfil.IdPerfil == 1 && Session["vigencia"].ToString() == "SI")
                {
                    btnNuevo.Enabled = btnGuardar.Enabled =
                    imaSeleccionar.Visible = true;
                }
            }
        }   
    }
    protected void grvJerarquias_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlTipoHorario_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTurno.Items.Clear();
        if (ddlTipoHorario.SelectedValue != "SELECCIONAR")
            if (ddlTipoHorario.SelectedValue != "24X24")
                clsCatalogos.llenarTipoHorario(ddlTurno, "catalogo.spuLeerTurno", "thTurno", ddlTipoHorario.Text, (clsEntSesion)Session["objSession" + Session.SessionID]);
    }
    protected void chbSexo_CheckedChanged(object sender, EventArgs e)
    {
        txbHombresDomingo.Enabled = txbHombresJueves.Enabled = txbHombresLunes.Enabled = txbHombresMartes.Enabled = txbHombresMiercoles.Enabled = txbHombresSabado.Enabled = txbHombresViernes.Enabled =
            txbMujeresDomingo.Enabled = txbMujeresJueves.Enabled = txbMujeresLunes.Enabled = txbMujeresMartes.Enabled = txbMujeresMiercoles.Enabled = txbMujeresSabado.Enabled = txbMujeresViernes.Enabled = chbSexo.Checked;
        txbIndistintoDomingo.Enabled = txbIndistintoJueves.Enabled = txbIndistintoLunes.Enabled = txbIndistintoMartes.Enabled = txbIndistintoMiercoles.Enabled = txbIndistintoSabado.Enabled = txbIndistintoViernes.Enabled = !chbSexo.Checked;
        txbHombresDomingo.Text = txbHombresJueves.Text = txbHombresLunes.Text = txbHombresMartes.Text = txbHombresMiercoles.Text = txbHombresSabado.Text = txbHombresViernes.Text =
        txbMujeresDomingo.Text = txbMujeresJueves.Text = txbMujeresLunes.Text = txbMujeresMartes.Text = txbMujeresMiercoles.Text = txbMujeresSabado.Text = txbMujeresViernes.Text =
        txbIndistintoDomingo.Text = txbIndistintoJueves.Text = txbIndistintoLunes.Text = txbIndistintoMartes.Text = txbIndistintoMiercoles.Text = txbIndistintoSabado.Text = txbIndistintoViernes.Text =
        string.Empty;
    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        limpiarAnexo();
        ddlJerarquia.Enabled = ddlTipoHorario.Enabled = ddlTurno.Enabled = true;
        clsCatalogos.llenarTipoHorario(ddlTipoHorario, "catalogo.spuLeerTipoHorario", "thDescripcion", (clsEntSesion)Session["objSession" + Session.SessionID]);
        clsCatalogos.llenarCatalogoJerarquia(ddlJerarquia, "catalogo.spLeerJerarquia", "jerDescripcion", "idJerarquia", (clsEntSesion)Session["objSession" + Session.SessionID]);
        hfNuevoAnexo.Value = "-1";
        popDetalle.Hide();
        mpeDetalle.Show();
    }
    protected void btnAceptarCaptura_Click(object sender, EventArgs e)
    {
        string strMensaje = validaciones();
        if (string.IsNullOrEmpty(strMensaje))
        {
            clsEntAnexoJerarquiaHorario objHorario = new clsEntAnexoJerarquiaHorario();
            objHorario.idTipoHorario = Convert.ToInt32(clsNegAnexoTecnico.consultaIdTipoHorario("catalogo.spuLeerIdTipoHorario", ddlTipoHorario.SelectedValue, ddlTurno.SelectedValue, (clsEntSesion)Session["objSession" + Session.SessionID]));
            objHorario.idJerarquia = Convert.ToInt32(ddlJerarquia.SelectedValue);
            objHorario.jerDescripcion = ddlJerarquia.SelectedItem.Text;
            objHorario.thHorario = ddlTipoHorario.SelectedItem.Text;
            objHorario.thTurno = ddlTurno.SelectedItem == null ? "" : ddlTurno.SelectedItem.Text;

            objHorario.masculinoLunes = txbHombresLunes.Text == "" ? 0 : Convert.ToInt32(txbHombresLunes.Text);
            objHorario.masculinoMartes = txbHombresMartes.Text == "" ? 0 : Convert.ToInt32(txbHombresMartes.Text);
            objHorario.masculinoMiercoles = txbHombresMiercoles.Text == "" ? 0 : Convert.ToInt32(txbHombresMiercoles.Text);
            objHorario.masculinoJueves = txbHombresJueves.Text == "" ? 0 : Convert.ToInt32(txbHombresJueves.Text);
            objHorario.masculinoViernes = txbHombresViernes.Text == "" ? 0 : Convert.ToInt32(txbHombresViernes.Text);
            objHorario.masculinoSabado = txbHombresSabado.Text == "" ? 0 : Convert.ToInt32(txbHombresSabado.Text);
            objHorario.masculinoDomingo = txbHombresDomingo.Text == "" ? 0 : Convert.ToInt32(txbHombresDomingo.Text);
            objHorario.femeninoLunes = txbMujeresLunes.Text == "" ? 0 : Convert.ToInt32(txbMujeresLunes.Text);
            objHorario.femeninoMartes = txbMujeresMartes.Text == "" ? 0 : Convert.ToInt32(txbMujeresMartes.Text);
            objHorario.femeninoMiercoles = txbMujeresMiercoles.Text == "" ? 0 : Convert.ToInt32(txbMujeresMiercoles.Text);
            objHorario.femeninoJueves = txbMujeresJueves.Text == "" ? 0 : Convert.ToInt32(txbMujeresJueves.Text);
            objHorario.femeninoViernes = txbMujeresViernes.Text == "" ? 0 : Convert.ToInt32(txbMujeresViernes.Text);
            objHorario.femeninoSabado = txbMujeresSabado.Text == "" ? 0 : Convert.ToInt32(txbMujeresSabado.Text);
            objHorario.femeninoDomingo = txbMujeresDomingo.Text == "" ? 0 : Convert.ToInt32(txbMujeresDomingo.Text);
            objHorario.indistintoLunes = txbIndistintoLunes.Text == "" ? 0 : Convert.ToInt32(txbIndistintoLunes.Text);
            objHorario.indistintoMartes = txbIndistintoMartes.Text == "" ? 0 : Convert.ToInt32(txbIndistintoMartes.Text);
            objHorario.indistintoMiercoles = txbIndistintoMiercoles.Text == "" ? 0 : Convert.ToInt32(txbIndistintoMiercoles.Text);
            objHorario.indistintoJueves = txbIndistintoJueves.Text == "" ? 0 : Convert.ToInt32(txbIndistintoJueves.Text);
            objHorario.indistintoViernes = txbIndistintoViernes.Text == "" ? 0 : Convert.ToInt32(txbIndistintoViernes.Text);
            objHorario.indistintoSabado = txbIndistintoSabado.Text == "" ? 0 : Convert.ToInt32(txbIndistintoSabado.Text);
            objHorario.indistintoDomingo = txbIndistintoDomingo.Text == "" ? 0 : Convert.ToInt32(txbIndistintoDomingo.Text);

            if (chbSexo.Checked)
            {
                objHorario.lunes = "H: " + objHorario.masculinoLunes + " M: " + objHorario.femeninoLunes;
                objHorario.martes = "H: " + objHorario.masculinoMartes + " M: " + objHorario.femeninoMartes;
                objHorario.miercoles = "H: " + objHorario.masculinoMiercoles + " M: " + objHorario.femeninoMiercoles;
                objHorario.jueves = "H: " + objHorario.masculinoJueves + " M: " + objHorario.femeninoJueves;
                objHorario.viernes = "H: " + objHorario.masculinoViernes + " M: " + objHorario.femeninoViernes;
                objHorario.sabado = "H: " + objHorario.masculinoSabado + " M: " + objHorario.femeninoSabado;
                objHorario.domingo = "H: " + objHorario.masculinoDomingo + " M: " + objHorario.femeninoDomingo;
                objHorario.totalHombres = objHorario.masculinoLunes + objHorario.masculinoMartes + objHorario.masculinoMiercoles + objHorario.masculinoJueves + objHorario.masculinoViernes + objHorario.masculinoSabado + objHorario.masculinoDomingo;
                objHorario.totalMujeres = objHorario.femeninoLunes + objHorario.femeninoMartes + objHorario.femeninoMiercoles + objHorario.femeninoJueves + objHorario.femeninoViernes + objHorario.femeninoSabado + objHorario.femeninoDomingo;
            }
            else
            {
                objHorario.lunes = objHorario.indistintoLunes.ToString();
                objHorario.martes = objHorario.indistintoMartes.ToString();
                objHorario.miercoles = objHorario.indistintoMiercoles.ToString();
                objHorario.jueves = objHorario.indistintoJueves.ToString();
                objHorario.viernes = objHorario.indistintoViernes.ToString();
                objHorario.sabado = objHorario.indistintoSabado.ToString();
                objHorario.domingo = objHorario.indistintoDomingo.ToString();
                objHorario.totalIndistinto = objHorario.indistintoLunes + objHorario.indistintoMartes + objHorario.indistintoMiercoles + objHorario.indistintoJueves + objHorario.indistintoViernes + objHorario.indistintoSabado + objHorario.indistintoDomingo;
            }

            if (hfNuevoAnexo.Value == "-1")/*Registro Nuevo*/
            {
                if (Session["lisAnexoJerarquia"] != null)
                    ((List<clsEntAnexoJerarquiaHorario>)Session["lisAnexoJerarquia"]).FindAll(
                        /*Encuentra el registro repetido en caso de ser nuevo*/
                       delegate(clsEntAnexoJerarquiaHorario bk)
                       {
                           if (bk.thHorario == ddlTipoHorario.SelectedItem.Text && bk.thTurno == (ddlTurno.SelectedItem == null ? "" : ddlTurno.SelectedItem.Text) && bk.jerDescripcion == ddlJerarquia.SelectedItem.Text)
                           {
                               strMensaje = "Revise los datos Tipo Horario, Turno y Jerarquía, no puede repetir registros";
                               return true;
                           }
                           return false;
                       });

                if (!string.IsNullOrEmpty(strMensaje))
                {
                    wucMensajeHorario.Mensaje(strMensaje);
                    mpeDetalle.Show();
                    return;
                }
                else
                {
                    /*Agregar Registro*/
                    if (Session["lisAnexoJerarquia"] != null)
                    {
                        ((List<clsEntAnexoJerarquiaHorario>)Session["lisAnexoJerarquia"]).Add(objHorario);
                    }
                    else
                    { 
                        /*Primer Registro*/
                        List<clsEntAnexoJerarquiaHorario> lstAnexo = new List<clsEntAnexoJerarquiaHorario> { objHorario };
                        Session["lisAnexoJerarquia"] = lstAnexo;
                    }
                }

            }
            else
            {
                int i = 0;
                ((List<clsEntAnexoJerarquiaHorario>)Session["lisAnexoJerarquia"]).FindAll(
                    /*Encuentra el registro a modificar*/
                     delegate(clsEntAnexoJerarquiaHorario bk)
                     {
                         if (bk.thHorario == ddlTipoHorario.SelectedItem.Text && bk.thTurno == (ddlTurno.SelectedItem.Text == "SELECCIONAR" ? "" : ddlTurno.SelectedItem.Text) && bk.jerDescripcion == ddlJerarquia.SelectedItem.Text)
                         {
                             ((List<clsEntAnexoJerarquiaHorario>)Session["lisAnexoJerarquia"])[i] = objHorario;
                             return true;
                         }
                         i++;
                         return false;
                     });
            }
            hfNuevoAnexo.Value = "";
            grvJerarquias.DataSource = (List<clsEntAnexoJerarquiaHorario>)Session["lisAnexoJerarquia"];
            grvJerarquias.DataBind();
            popDetalle.Show();
            mpeDetalle.Hide();
        }
        else
        {
            wucMensajeHorario.Mensaje(strMensaje);
            mpeDetalle.Show();

        }
    }
    protected void btnCancelar1_Click(object sender, EventArgs e)
    {
        hfNuevoAnexo.Value = "";
        popDetalle.Show();
        mpeDetalle.Hide();
    }
    

}