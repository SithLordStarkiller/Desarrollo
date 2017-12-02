


using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SICOGUA.Datos;
using SICOGUA.Entidades;
using SICOGUA.Negocio;
using System.Text;
using SICOGUA.Seguridad;
using System.Globalization;
using System.Web.UI;
using System.Data;
using SICOGUA.proNegocio;

public partial class PaseLista_frmListaAsistencia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
            
            #region Solo Consulta
            clsEntPermiso objPermiso = new clsEntPermiso();
            objPermiso.IdPerfil = objSesion.usuario.Perfil.IdPerfil;
            //Operacion 36 Solo Consulta-LISTA ASISTENCIA
            if (clsDatPermiso.consultarPermiso(objPermiso.IdPerfil, 36, objSesion) == false)
            {
                btnGuardar.Visible = false;
            }
            #endregion

            lblFecha.Text = DateTime.Now.ToShortDateString();
            lblHora.Text = DateTime.Now.ToString("HH:mm");
            lblAsistencia.Text = objSesion.usuario.UsuNombre + " " + objSesion.usuario.UsuPaterno + " " + objSesion.usuario.UsuMaterno;
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuarioAsignacionLimitada", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
        }
    }

    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        grvAsistencia.DataSource = "";
        grvAsistencia.DataBind();
        if (ddlServicio.SelectedIndex > 0)
        {           
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuarioAsignacionLimitada", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);

            ddlHorario.Items.Clear();
            ddlInstalacion.Enabled = true;
            tOperaciones.Visible = false;            
            rblEntradaSalida.SelectedIndex = -1;            
            return;
        }
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
        tOperaciones.Visible = false;
        rblEntradaSalida.SelectedIndex = -1; 
        ddlHorario.Items.Clear();
        ddlHorario.Enabled = false;
    }
  
    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        
        bool MCS, DesactivarAsistencia, hibrido;
        
        /*Cambio integración CONEC mayo 2016*/

        if (clsNegAsistencia.validaInstalacionCONEC(Convert.ToInt32(ddlServicio.SelectedValue), Convert.ToInt32(ddlInstalacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]))
        {

            string script = @"<script type='text/javascript'>
                                alert('Para esta instalación el registro de asistencia se realiza en CONEC');</script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

        }
        else
        {
            // si la instalación es de la zona de servicios entrará a la validación, de lo contrario permite la asistencia por biométrico y por pase de lista
            // 7 es el id de Zona de Servicios
            hibrido = clsNegAsistencia.paseListaHibrido(Convert.ToInt32(ddlServicio.SelectedValue), 7, Convert.ToInt32(ddlInstalacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);

            MCS = clsNegAsistencia.asistenciaBiometriocMCS(Convert.ToInt32(ddlServicio.SelectedValue), Convert.ToInt32(ddlInstalacion.SelectedValue),
                        Convert.ToInt32(ddlHorario.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);

            if (!hibrido)
            {
                if (MCS != true)
                {
                    string script = @"<script type='text/javascript'>
                                alert('Para el Servicio, Instalación y horario seleccionado, la asistencia se realiza por biometrico');
                                </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                    tOperaciones.Visible = false;
                    return;
                }
            }
            
            /*para saber si se trata de un horario abierto y que ponga presente siempre*/
            hffHorarioAbierto.Value = clsNegAsistencia.horarioAbiertoMCS(Convert.ToInt32(ddlHorario.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]).ToString();
            /*fin*/
            clsEntEmpleadoAsignacion objAsignacion = new clsEntEmpleadoAsignacion();
            objAsignacion.Servicio = new clsEntServicio();
            objAsignacion.Instalacion = new clsEntInstalacion();

            if (ddlServicio.SelectedIndex > 0) objAsignacion.Servicio.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
            if (ddlInstalacion.SelectedIndex > 0) objAsignacion.Instalacion.IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);

            int dia = DateTime.Now.Day;
            int mes = DateTime.Now.Month;
            int anio = DateTime.Now.Year;
            int hora = DateTime.Now.Hour;
            int minuto = DateTime.Now.Minute;
            int segundo = DateTime.Now.Second;

            DateTime dtInstante = new DateTime(anio, mes, dia, hora, minuto, segundo); //
            Session["dtInstante" + Session.SessionID] = dtInstante;
            /*para revisar si se debe deshabilitar la lista*/
            DesactivarAsistencia = clsNegAsistencia.desabilitarAsistenciaTiempo(Convert.ToDateTime(Session["dtInstante" + Session.SessionID]),
                      Convert.ToInt32(ddlHorario.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            Session["objAsistencia" + Session.SessionID] = clsNegEmpleado.generarListaFinal(DesactivarAsistencia, objAsignacion, /*objAsignacionOs,*/ Convert.ToInt32(ddlHorario.SelectedValue == "" ? "0" : ddlHorario.SelectedValue), Convert.ToChar(rblEntradaSalida.SelectedValue), dtInstante, (clsEntSesion)Session["objSession" + Session.SessionID]);

            if (Convert.ToInt32(rblEntradaSalida.SelectedValue) == 1)
            {
                grvAsistencia.Columns[8].Visible = true;
                grvAsistencia.Columns[9].Visible = false;
                grvAsistencia.Columns[10].Visible = false;
                grvAsistencia.Columns[6].Visible = true;
                grvAsistencia.Columns[7].Visible = true;
                lblEntradaSalida.Text = "Asistencia - Entrada";
                lblES.Text = "Entrada";
            }
            else
            {
                grvAsistencia.Columns[6].Visible = false;
                grvAsistencia.Columns[7].Visible = true;
                grvAsistencia.Columns[8].Visible = true;
                grvAsistencia.Columns[9].Visible = true;
                grvAsistencia.Columns[10].Visible = true;
                lblEntradaSalida.Text = "Asistencia - Salida";
                lblES.Text = "Salida";
            }
            grvAsistencia.DataSource = Session["objAsistencia" + Session.SessionID];
            grvAsistencia.DataBind();


            if (DesactivarAsistencia != false && rblEntradaSalida.SelectedValue == "1")
            {
                string script = @"<script type='text/javascript'>
                                alert('Ya ha terminado el tiempo para tomar asistencia');</script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                btnGuardar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = true;
            }
            tOperaciones.Visible = true;

        }
    }
    
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        /*se crea la tabla que se enviara al procedimiento para la asistencia de entrada*/
        DataTable dtPaseLista = new DataTable("dtPaseLista");
        DataColumn idEmpleado = new DataColumn("idEmpleado", typeof(Guid));
        DataColumn idAsignacionhorario = new DataColumn("idAsignacionhorario", typeof(int));
        DataColumn idHorario = new DataColumn("idHorario", typeof(int));
        DataColumn asiEntrada = new DataColumn("asiEntrada", typeof(string));
        DataColumn estatus = new DataColumn("estatus", typeof(string));
        DataColumn idAsistencia = new DataColumn("idAsistencia", typeof(int));
        DataColumn asiSalida = new DataColumn("asiSalida", typeof(string));
        DataColumn franco = new DataColumn("franco", typeof(int)); 
        
        /*fin tabla*/

        /*se agregan las columnas a la tabla*/
        dtPaseLista.Columns.Add(idEmpleado);
        dtPaseLista.Columns.Add(idAsignacionhorario);
        dtPaseLista.Columns.Add(idHorario);
        dtPaseLista.Columns.Add(asiEntrada); 
        dtPaseLista.Columns.Add(estatus);
        dtPaseLista.Columns.Add(idAsistencia);
        dtPaseLista.Columns.Add(asiSalida);
        dtPaseLista.Columns.Add(franco);
        /*finally*/

        StringBuilder sbUrl = new StringBuilder();
        List<clsEntPaseLista> lstPaseLista = new List<clsEntPaseLista>();

        DateTime dtInstante = (DateTime)Session["dtInstante" + Session.SessionID];

        foreach (GridViewRow gvr in grvAsistencia.Rows)
        {
            DataRow drCustomer = dtPaseLista.NewRow();            

          
            drCustomer["idEmpleado"] = new Guid(((Label)gvr.FindControl("lblIdEmpleado")).Text);
            drCustomer["idAsignacionhorario"] = Convert.ToInt32(((Label)gvr.FindControl("lblIdAsignHorario")).Text);
            drCustomer["idHorario"] = Convert.ToInt32(((Label)gvr.FindControl("lblIdHorario")).Text);
            drCustomer["asiEntrada"] = ((TextBox)gvr.FindControl("txtHoraEntrada")).Text;
            drCustomer["estatus"] = ((Label)gvr.FindControl("lblEstatusEntrada")).Text;
            drCustomer["idAsistencia"] = ((Label)gvr.FindControl("lblidAsistencia")).Text;
            drCustomer["asiSalida"] = ((TextBox)gvr.FindControl("txtHoraSalida")).Text;
            drCustomer["franco"] = Convert.ToInt32(((Label)gvr.FindControl("lblFranco")).Text);

            if (rblEntradaSalida.SelectedValue == "1")
            { /*revisar que tenga información en el cuadro de texto y que se encuentre activo o presente*/
                if ((((TextBox)gvr.FindControl("txtHoraEntrada")).Text != string.Empty) && ((Label)gvr.FindControl("lblDesactivarPase")).Text == "False")
                {
                    /*la hora debe ser menor a dos horas de anticipación*/

                    //TimeSpan diff1 = 
                    //    Convert.ToDateTime(((Label)gvr.FindControl("lblFranco")).Text)(Convert.ToDateTime((TextBox)gvr.FindControl("txtHoraEntrada")));
                    /*mostra mensaje cuando no sea una hora adecuada*/
                    try
                    {
                        DateTime fech = Convert.ToDateTime(((TextBox)gvr.FindControl("txtHoraEntrada")).Text);
                    }
                    catch (Exception)
                    {
                        string script = @"<script type='text/javascript'>
                                alert('La hora para: " + ((Label)gvr.FindControl("lblNombre")).Text + " debe tener el formato: 24 hrs'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                        return;
                    }
                    finally
                    {
                        dtPaseLista.Rows.Add(drCustomer);
                    }
                }
                /*este es para el caso de los que son francos, para que tambien los guarde*/
                /*es posible que franco sea igual a dos lo que significa que ya se ha guardado el regitro en la tabla de REA
                 si el valor es 0 significa que no se trata de una persona que es franco*/
                if (((Label)gvr.FindControl("lblFranco")).Text == "1")
                    dtPaseLista.Rows.Add(drCustomer);
            }
            else
            {
                if (((TextBox)gvr.FindControl("txtHoraSalida")).Text != string.Empty)
                {
                    /*el siguiente try espara corroborar que la hora se este escribiendo en el formato de 24 hrs*/
                    try
                    {
                        DateTime fech = Convert.ToDateTime(((TextBox)gvr.FindControl("txtHoraSalida")).Text);
                    }
                    catch (Exception)
                    {
                        string script = @"<script type='text/javascript'>
                                alert('La hora para: " + ((Label)gvr.FindControl("lblNombre")).Text + " debe tener el formato: 24 hrs'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                        return;
                    }
                    finally
                    {
                        dtPaseLista.Rows.Add(drCustomer);
                    }
                    if (Convert.ToDateTime(((TextBox)gvr.FindControl("txtHoraSalida")).Text) < Convert.ToDateTime(((Label)gvr.FindControl("lblHoraEntrada")).Text))
                    {
                        string script = @"<script type='text/javascript'>
                                alert('La hora de: " + ((Label)gvr.FindControl("lblNombre")).Text + " debe ser mayor o igual a fecha y hora: " + ((Label)gvr.FindControl("lblHoraEntrada")).Text + "'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                        return;
                    }

                    if (hffHorarioAbierto.Value != "True")
                    {
                        
                        if (Convert.ToDateTime(((TextBox)gvr.FindControl("txtHoraSalida")).Text) < Convert.ToDateTime(((Label)gvr.FindControl("lblAuxHora")).Text))
                        {
                            string script = @"<script type='text/javascript'>
                                alert('La hora de: " + ((Label)gvr.FindControl("lblNombre")).Text + " debe ser mayor o igual a: " + ((Label)gvr.FindControl("lblAuxHora")).Text + "'); </script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                            return;
                        }   
                    }
                }
            }
        }
            try
            {
                if (rblEntradaSalida.SelectedValue == "1")
                    clsNegAsistencia.insertarPaseListaCompleto(dtPaseLista, (DateTime)Session["dtInstante" + Session.SessionID], (clsEntSesion)Session["objSession" + Session.SessionID]);
                else
                    clsNegAsistencia.insertarAsistenciaSalidaCompleto(dtPaseLista, (DateTime)Session["dtInstante" + Session.SessionID], (clsEntSesion)Session["objSession" + Session.SessionID]);
                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito"));
            }
            catch (Exception)
            {
                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est101&strMensaje=" + Server.UrlEncode("Ha ocurrido un error durante la operación. Intentelo más tarde ó contacte a un Administrador."));
            }
            finally
            {                
                tOperaciones.Visible = false;
                Response.Redirect(sbUrl.ToString());
            }

          
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {       

        ddlServicio.SelectedIndex = 0;
        ddlServicio_SelectedIndexChanged(null, null);
        ddlInstalacion.ClearSelection();
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
        ddlHorario.ClearSelection();
        ddlHorario.Items.Clear();
        ddlHorario.Enabled = false;
        lblES.Text = string.Empty;
        lblEntradaSalida.Text = "Asistencia";



        grvAsistencia.DataSource = null;
        grvAsistencia.DataBind();

        tOperaciones.Visible = false;

        Session["dtInstante" + Session.SessionID] = null;
        Session["objAsistencia" + Session.SessionID] = null;
    }
      
    protected void grvAsistencia_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void ddlInstalacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        grvAsistencia.DataSource = "";
        grvAsistencia.DataBind();
        if (ddlInstalacion.SelectedIndex > 0)
        {
            clsCatalogos.llenarHoarioServicioInstalacion(ddlHorario, "catalogo.spConsultarHorarioSerInsREA", "horNombre", "idHorario", Convert.ToInt32(ddlServicio.SelectedValue), Convert.ToInt32(ddlInstalacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            
            ddlHorario.Enabled = true;
            tOperaciones.Visible = false;
            rblEntradaSalida.SelectedIndex = -1; 
            return;
        }
        ddlHorario.Items.Clear();
        ddlHorario.Enabled = false;
        tOperaciones.Visible = false;        
    }
    
    protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
    {
        tOperaciones.Visible = false;
        grvAsistencia.DataSource = "";
        grvAsistencia.DataBind();
        rblEntradaSalida.SelectedIndex = -1; 
    }
    
    protected void grvAsistencia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string arreglo = (string)e.CommandArgument;
            char[] splitchar = { '@' };
            string[] strArreglo = arreglo.Split(splitchar);
            DataTable dTable = new DataTable();
            Guid idEmpleado = new Guid(strArreglo[0]);

            dTable = clsNegEmpleado.consultarServicioInstalacionHorarioREA(idEmpleado, Convert.ToDateTime(Session["dtInstante" + Session.SessionID]), (clsEntSesion)Session["objSession" + Session.SessionID]);
            lblNomCompleto.Text = dTable.Rows[0].ItemArray[0].ToString();

            /* ACTUALIZACIÓN marzo 2017 */

            //lblZona.Text = dTable.Rows[0].ItemArray[1].ToString();
            //lblServicio.Text = dTable.Rows[0].ItemArray[2].ToString();
            //lblInstalacion.Text = dTable.Rows[0].ItemArray[3].ToString();
            //lblFuncion.Text = dTable.Rows[0].ItemArray[4].ToString();

            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                if (dTable.Rows[i].ItemArray[5].ToString() == ddlHorario.SelectedItem.Text.ToString())
                {
                    lblZona.Text = dTable.Rows[i].ItemArray[1].ToString();
                    lblServicio.Text = dTable.Rows[i].ItemArray[2].ToString();
                    lblInstalacion.Text = dTable.Rows[i].ItemArray[3].ToString();
                    lblFuncion.Text = dTable.Rows[i].ItemArray[4].ToString();
                    lblHorario.Text = dTable.Rows[i].ItemArray[5].ToString();
                    lblDescHorario.Text = dTable.Rows[i].ItemArray[6].ToString();
                    lblFechaInicio.Text = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(dTable.Rows[i].ItemArray[7].ToString()));
                    lblHoraEntrada.Text = string.Format("{0:HH:mm}", Convert.ToDateTime(dTable.Rows[i].ItemArray[8].ToString()));//dTable.Rows[0].ItemArray[8].ToString(); //
                    lblHoraSalida.Text = string.Format("{0:HH:mm}", Convert.ToDateTime(dTable.Rows[i].ItemArray[9].ToString()));//string.Format("{0:t}",dTable.Rows[0].ItemArray[9].ToString());

                    break;
                }
            }



            //lblHorario.Text = dTable.Rows[0].ItemArray[5].ToString();



            //lblDescHorario.Text = dTable.Rows[0].ItemArray[6].ToString();
            //lblFechaInicio.Text = string.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(dTable.Rows[0].ItemArray[7].ToString()));
            //lblHoraEntrada.Text = string.Format("{0:HH:mm}", Convert.ToDateTime(dTable.Rows[0].ItemArray[8].ToString()));//dTable.Rows[0].ItemArray[8].ToString(); //
            //lblHoraSalida.Text = string.Format("{0:HH:mm}", Convert.ToDateTime(dTable.Rows[0].ItemArray[9].ToString()));//string.Format("{0:t}",dTable.Rows[0].ItemArray[9].ToString());

            /* FIN ACTUALIZACIÓN */

            mpeDetalle.Show();
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        btnNuevo_Click(null, null);
        Response.Redirect("~/frmInicio.aspx");
    }
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        
        clsEntReporteListado objReporteListado = new clsEntReporteListado();
        objReporteListado.Servicio = new clsEntServicio();
        objReporteListado.Instalacion = new clsEntInstalacion();

        objReporteListado.FechaReporte = DateTime.Now;
        objReporteListado.Servicio.idServicio = string.IsNullOrEmpty(ddlServicio.SelectedValue) ? 0 : Convert.ToInt32(ddlServicio.SelectedValue);
        objReporteListado.Instalacion.IdInstalacion = string.IsNullOrEmpty(ddlInstalacion.SelectedValue) ? 0 : Convert.ToInt32(ddlInstalacion.SelectedValue);
        objReporteListado.idEstatus =0;
        Session["objReporteListado" + Session.SessionID] = objReporteListado;
        btnNuevo_Click(null, null);
        Response.Redirect("~/Reportes/frmRvAsistenciaREA.aspx?hyper=Asistencia");
    }
    
}