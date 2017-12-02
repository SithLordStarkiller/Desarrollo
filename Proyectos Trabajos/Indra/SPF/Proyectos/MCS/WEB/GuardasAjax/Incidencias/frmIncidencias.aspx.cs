using System;
using System.Collections.Generic;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using SICOGUA.Datos;
using SICOGUA.Negocio;
using System.Data;
using System.Web.UI.WebControls;


public partial class Incidencias_frmIncidencias : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["impresion" + Session.SessionID] = null;
        if (!IsPostBack)
        {
                     if (Session["objEmpleado" + Session.SessionID] != null)
            {



            #region Catálogos

            clsCatalogos.llenarCatalogo(ddlTipoIncidencia, "catalogo.spLeerTipoIncidencia", "tiDescripcion", "idTipoIncidencia", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlAutoriza, "catalogo.spLeerPersonaAutoriza", "empNombre", "idEmpleado", (clsEntSesion)Session["objSession" + Session.SessionID]);

            #endregion


            #region Solo Consulta
            clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
            clsEntPermiso objPermiso = new clsEntPermiso();
            objPermiso.IdPerfil = objSesion.usuario.Perfil.IdPerfil;
            //Operacion 33 Solo Consulta INCIDENCIAS
            if (clsDatPermiso.consultarPermiso(objPermiso.IdPerfil, 33, objSesion) == false)
            {
                btnGuardar.Visible = false;
                btnAgregar.Enabled = false;
                btnGuardarIncidencia.Visible = false;
                ddlAutoriza.Enabled = ddlTipoIncidencia.Enabled = txbDescripcion.Enabled = false;
                txbFechaFinal.Enabled = txbFechaInicial.Enabled = txbOficioIncidencia.Enabled = calInicial.Enabled = calFinal.Enabled = false;
            }
            #endregion


            #region Cargar Información

            /////////////aqui
            clsEntEmpleadoAsignacion objAsignacion = new clsEntEmpleadoAsignacion();
            objAsignacion.Servicio = new clsEntServicio();
            objAsignacion.Instalacion = new clsEntInstalacion();
            Session["lstAsignaciones" + Session.SessionID] = null;
            if (Session["objEmpleado" + Session.SessionID] != null)
            {
                clsEntEmpleado objEmpleado2 = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];
                #region Asignaciones
                Session["lstAsignaciones" + Session.SessionID] = objEmpleado2.EmpleadoAsignacion;
                #endregion
                //////////////

                if (Session["objEmpleado" + Session.SessionID] != null)
                {
                    clsEntEmpleado objEmpleado = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];

                    hfidEmpleado.Value = objEmpleado.IdEmpleado.ToString();
                    clsUtilerias.llenarLabel(lblCuip, objEmpleado.EmpCUIP);
                    clsUtilerias.llenarLabel(lblPaterno, objEmpleado.EmpPaterno, "-");
                    clsUtilerias.llenarLabel(lblMaterno, objEmpleado.EmpMaterno, "-");
                    clsUtilerias.llenarLabel(lblNombre, objEmpleado.EmpNombre, "-");
                    clsUtilerias.llenarLabel(lblNumero, objEmpleado.EmpNumero, "-");

                    if (objEmpleado.EmpFechaBaja.Date <= DateTime.Now
                && objEmpleado.EmpFechaBaja.ToShortDateString() != "01/01/1900"
                && objEmpleado.EmpFechaBaja.ToShortDateString() != "01/01/0001")
                    {

                        lblEmpleadoBaja.Text = "El empleado ha sido dado de baja. No podrá realizar ningún cambio a la información";
                        lblEmpleadoBaja.Visible = true;
                        divErrorInc.Visible = true;
                        btnAgregar.Enabled = false;
                        btnGuardar.Enabled = false;
                        if (objEmpleado.Incidencias != null)
                        {
                            Session["lstIncidencias" + Session.SessionID] = objEmpleado.Incidencias;
                            grvIncidencias.DataSource = Session["lstIncidencias" + Session.SessionID];
                            grvIncidencias.DataBind();
                        }
                        return;
                    }


                    if (objEmpleado.idRevision != 0)
                    {
                        lblEmpleadoBaja.Visible = true;
                        divErrorInc.Visible = true;
                        if (objEmpleado.idRenuncia != 0)
                        {
                            lblEmpleadoBaja.Text = "El empleado tiene iniciado un procedimiento por lo que no se podrá realizar ningún movimiento adicionalmente tiene reporte de renuncia";

                        }
                        else
                        {
                            lblEmpleadoBaja.Text = "El empleado tiene iniciado un procedimiento por lo que no se podrá realizar ningún movimiento";
                        }

                        if (objEmpleado.Incidencias != null)
                        {
                            Session["lstIncidencias" + Session.SessionID] = objEmpleado.Incidencias;
                            grvIncidencias.DataSource = Session["lstIncidencias" + Session.SessionID];
                            grvIncidencias.DataBind();
                        }

                        btnAgregar.Enabled = false;
                        btnGuardar.Enabled = false;
                        return;
                    }
                    else
                    {
                        if (objEmpleado.idRenuncia != 0)
                        {
                            lblEmpleadoBaja.Text = "El empleado tiene reporte de renuncia";
                            lblEmpleadoBaja.Visible = true;
                            divErrorInc.Visible = true;

                        }
          
                        btnAgregar.Enabled = true;
                        btnGuardar.Enabled = true;
                    }

                  




                    if (objEmpleado.Incidencias != null)
                    {
                        Session["lstIncidencias" + Session.SessionID] = objEmpleado.Incidencias;
                        grvIncidencias.DataSource = Session["lstIncidencias" + Session.SessionID];
                        grvIncidencias.DataBind();
                    }
                }
 
           
            #endregion

                #region Solo consulta en base a Servicios, instalaciones y zonas asignadas
                // consulto permiso de la persona para deshabiliatr botones de OPERACION


               DataSet dsUltima = new DataSet();
                clsDatEmpleado.buscarAsignacionActual(objEmpleado2.IdEmpleado, ref dsUltima, (clsEntSesion)Session["objSession" + Session.SessionID]);

                
                if ((dsUltima.Tables[0].Rows[0]["permiso"]).ToString()=="0")
                {
                    btnGuardar.Visible = false;
                    btnAgregar.Enabled = false;
                    btnGuardarIncidencia.Visible = false;
                    ddlAutoriza.Enabled = ddlTipoIncidencia.Enabled = txbDescripcion.Enabled = false;
                    txbFechaFinal.Enabled = txbFechaInicial.Enabled = txbOficioIncidencia.Enabled = calInicial.Enabled = calFinal.Enabled = false;
                }
                #endregion

                #region Validación

                if (lblNombre.Text.Trim() == "-")
                {
                    string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
                    string script = "if(confirm('¡Para guardar un registro debes seleccionar un Empleado!.')) location.href='./../" + busqueda + "'; else location.href='./../frmInicio.aspx';";
                    ClientScript.RegisterClientScriptBlock(GetType(), "Mensaje", script, true);
                }

                #endregion
            }
        
        }
        else
        {
            string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
            string script = "if(confirm('¡Para guardar un registro es necesario seleccionar un Empleado!.')) location.href='./../" + busqueda + "'; else location.href='./../frmInicio.aspx';";
            ClientScript.RegisterClientScriptBlock(GetType(), "Mensaje", script, true);
        }
    }
    }


    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        ddlTipoIncidencia.SelectedIndex = -1;
        txbFechaInicial.Text = "";
        txbFechaFinal.Text = "";
        ddlAutoriza.SelectedIndex = -1;
        txbOficioIncidencia.Text = "";
        txbDescripcion.Text = "";
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        //AGRUEGUE
        // Validar que fechas no se encimen con otras.
        DateTime dtn1 = clsUtilerias.dtObtenerFecha(txbFechaInicial.Text);
        DateTime dtn2 = clsUtilerias.dtObtenerFecha(txbFechaFinal.Text);
        bool esCorrecto = true;
        clsEntEmpleado objEmpleado = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];
        clsNegEmpleado.consultarEmpleado(ref objEmpleado, (clsEntSesion)Session["objSession" + Session.SessionID]);
        Session["lstIncidencias" + Session.SessionID] = objEmpleado.Incidencias;
        grvIncidencias.DataSource = Session["lstIncidencias" + Session.SessionID];
        grvIncidencias.DataBind();
      

        foreach (GridViewRow row in grvIncidencias.Rows)
        {
            DateTime dta1 = clsUtilerias.dtObtenerFecha(((Label)row.FindControl("lblInicio")).Text);
            DateTime dta2 = clsUtilerias.dtObtenerFecha(((Label)row.FindControl("lblFin")).Text);

            if (dtn2.CompareTo(new DateTime()) == 0)
            {
                dtn2 = DateTime.MaxValue;
            }
            if (dta2.CompareTo(new DateTime()) == 0)
            {
                dta2 = DateTime.MaxValue;
            }

            if (hfRowIndex.Value != "-1")
            {
                if (row.RowIndex == Convert.ToInt32(hfRowIndex.Value))
                {
                    if (row.RowIndex == 0)
                    {
                        dta1 = new DateTime();
                        dta2 = row.RowIndex + 1 == grvIncidencias.Rows.Count - 1 ? clsUtilerias.dtObtenerFecha(((Label)grvIncidencias.Rows[row.RowIndex + 1].FindControl("lblInicio")).Text) : DateTime.Now;
                    }
                    else
                    {
                        dta1 = clsUtilerias.dtObtenerFecha(((Label)grvIncidencias.Rows[row.RowIndex - 1].FindControl("lblFin")).Text);
                        dta2 = row.RowIndex + 1 == grvIncidencias.Rows.Count - 1 ? clsUtilerias.dtObtenerFecha(((Label)grvIncidencias.Rows[row.RowIndex + 1].FindControl("lblInicio")).Text) : DateTime.Now;
                    }
                }
            }
            if (dtn1.CompareTo(dta1) <= 0 && dtn2.CompareTo(dta2) <= 0 && dtn2.CompareTo(dta1) >= 0)
            {
                esCorrecto = false;
            }
            if (dtn1.CompareTo(dta1) <= 0 && dtn2.CompareTo(dta2) >= 0)
            {
                esCorrecto = false;
            }
            if (dtn1.CompareTo(dta1) >= 0 && dtn2.CompareTo(dta2) <= 0 && row.RowIndex != Convert.ToInt32(hfRowIndex.Value))
            {
                esCorrecto = false;
            }
            if (dtn1.CompareTo(dta1) >= 0 && dtn1.CompareTo(dta2) <= 0 && dtn2.CompareTo(dta2) >= 0 && row.RowIndex != Convert.ToInt32(hfRowIndex.Value))
            {
                esCorrecto = false;
            }
        }

        if (!esCorrecto)
        {
            wucMensajeIncidencia.Mensaje("Existen Incidencias en esta Fecha, Verifique la Información.");
            popIncidencia.Show();
            return;
        }

        clsEntIncidencia objIncidencia = new clsEntIncidencia();
        objIncidencia.tipoIncidencia = new clsEntTipoIncidencia();
        objIncidencia.IdIncidencia = Convert.ToInt32(hfIdEmpleadoIncidencia.Value);

        if (!string.IsNullOrEmpty(ddlTipoIncidencia.SelectedValue))
        {
            objIncidencia.IdTipoIncidencia = Convert.ToInt16(ddlTipoIncidencia.SelectedValue);
            objIncidencia.tipoIncidencia.TiDescripcion = ddlTipoIncidencia.SelectedItem.Text;
        }
        objIncidencia.IncFechaInicial = clsUtilerias.dtObtenerFecha(txbFechaInicial.Text);
        objIncidencia.sFechaInicial = clsUtilerias.dtObtenerFecha(txbFechaInicial.Text).ToShortDateString() == "01/01/0001" ? "" : clsUtilerias.dtObtenerFecha(txbFechaInicial.Text).ToShortDateString();
        objIncidencia.IncFechaFinal = clsUtilerias.dtObtenerFecha(txbFechaFinal.Text);
        if (objIncidencia.IncFechaFinal.ToShortTimeString() != "01/01/0001" || objIncidencia.IncFechaFinal.ToShortTimeString() != "01/01/1900")
        {
            DateTime dt = new DateTime(objIncidencia.IncFechaFinal.Year, objIncidencia.IncFechaFinal.Month, objIncidencia.IncFechaFinal.Day, 23, 59, 59);
            objIncidencia.IncFechaFinal = dt;

        }

        objIncidencia.sFechaFinal = clsUtilerias.dtObtenerFecha(txbFechaFinal.Text).ToShortDateString() == "01/01/0001" ? "" : clsUtilerias.dtObtenerFecha(txbFechaFinal.Text).ToShortDateString();
        if (!string.IsNullOrEmpty(ddlAutoriza.SelectedValue))
        {
            objIncidencia.IdEmpleadoAutoriza = new Guid(ddlAutoriza.SelectedValue);
            objIncidencia.sEmpleadoAutoriza = ddlAutoriza.SelectedItem.Text;
        }

        objIncidencia.IncDescripcion = txbDescripcion.Text.Trim();
        objIncidencia.IncNoOficio = txbOficioIncidencia.Text.Trim();

        switch (hfRowIndex.Value)
        {
            case "-1":
                if (!Equals(Session["lstIncidencias" + Session.SessionID], null))
                {
                    ((List<clsEntIncidencia>)Session["lstIncidencias" + Session.SessionID]).Add(objIncidencia);
                }
                else
                {
                    List<clsEntIncidencia> lstIncidencia = new List<clsEntIncidencia>();
                    lstIncidencia.Add(objIncidencia);
                    Session["lstIncidencias" + Session.SessionID] = lstIncidencia;
                }
                break;
            default:
                int indice = Convert.ToInt32(hfRowIndex.Value);
                ((List<clsEntIncidencia>)Session["lstIncidencias" + Session.SessionID])[indice] = objIncidencia;
                hfRowIndex.Value = "-1";
                break;
        }

        btnNuevo_Click(null, null);
        grvIncidencias.DataSource = Session["lstIncidencias" + Session.SessionID];
        grvIncidencias.DataBind();

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session["objEmpleado" + Session.SessionID] = null;
        Session["lstIncidencias" + Session.SessionID] = null;
        Response.Redirect("~/frmInicio.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
        Response.Redirect("~/" + busqueda);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
   
            clsEntEmpleado objEmpleado = new clsEntEmpleado();

            objEmpleado.IdEmpleado = new Guid(hfidEmpleado.Value);

            if (lblNumero.Text != "-") { objEmpleado.EmpNumero = Convert.ToInt32(lblNumero.Text); }
            objEmpleado.EmpCUIP = lblCuip.Text;
            objEmpleado.EmpPaterno = lblPaterno.Text;
            objEmpleado.EmpMaterno = lblMaterno.Text;
            objEmpleado.EmpNombre = lblNombre.Text;

            #region Incidencias

            if (Session["lstIncidencias" + Session.SessionID] != null)
            {
                objEmpleado.Incidencias = (List<clsEntIncidencia>)Session["lstIncidencias" + Session.SessionID];
                Session["lstIncidencias" + Session.SessionID] = null;
            }

            #endregion

            Session["objEmpleado" + Session.SessionID] = objEmpleado;
            Response.Redirect("~/Incidencias/frmIncidenciasConfirmacion.aspx");
       
    }

    protected void grvIncidencias_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        if (Session["lstEliminarIncidencias" + Session.SessionID] == null)
        {
            Session["lstEliminarIncidencias" + Session.SessionID] = new List<clsEntIncidencia>();
        }

        ((List<clsEntIncidencia>)Session["lstEliminarIncidencias" + Session.SessionID]).Add(((List<clsEntIncidencia>)Session["lstIncidencias" + Session.SessionID])[e.RowIndex]);
        ((List<clsEntIncidencia>)Session["lstIncidencias" + Session.SessionID]).RemoveAt(e.RowIndex);

        grvIncidencias.DataSource = Session["lstIncidencias" + Session.SessionID];
        grvIncidencias.DataBind();
    }

    protected void grvIncidencias_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        clsEntIncidencia objIncidencia = ((List<clsEntIncidencia>)Session["lstIncidencias" + Session.SessionID])[e.RowIndex];
        hfIdEmpleadoIncidencia.Value = objIncidencia.IdIncidencia.ToString();

        clsUtilerias.seleccionarDropDownList(ddlAutoriza, objIncidencia.IdEmpleadoAutoriza.ToString());
        clsUtilerias.seleccionarDropDownList(ddlTipoIncidencia, objIncidencia.IdTipoIncidencia);
        clsUtilerias.llenarTextBox(txbFechaInicial, objIncidencia.sFechaInicial);
        clsUtilerias.llenarTextBox(txbFechaFinal, objIncidencia.sFechaFinal);
        clsUtilerias.llenarTextBox(txbOficioIncidencia, objIncidencia.IncNoOficio);
        clsUtilerias.llenarTextBox(txbDescripcion, objIncidencia.IncDescripcion);

        hfRowIndex.Value = e.RowIndex.ToString();
        hfIdEmpleadoIncidencia.Value = objIncidencia.IdIncidencia.ToString();

        popIncidencia.Show();

    }

    protected void btnCancelarIncidencia_Click(object sender, EventArgs e)
    {
        btnNuevo_Click(null, null);
    }
    protected void btnAgregar_Click1(object sender, EventArgs e)
    {

    }
}