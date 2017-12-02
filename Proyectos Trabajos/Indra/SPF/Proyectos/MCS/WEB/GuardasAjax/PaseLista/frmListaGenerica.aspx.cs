using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Seguridad;
using SICOGUA.Negocio;
using SICOGUA.proNegocio;
using SICOGUA.Entidades;
using proEntidades;
using System.Data;
using SICOGUA.Datos;
using System.Globalization;

public partial class PaseLista_frmListaGenerica : System.Web.UI.Page
{
    #region Inicilización de objetos
    public List<clsEntEmpleadosListaGenerica> lstRegistros { get { return (List<clsEntEmpleadosListaGenerica>)Session["lstDatosLista" + Session.SessionID]; } set { Session["lstDatosLista" + Session.SessionID] = value; } }

    /* ACTUALIZACIÓN MARZO 2017 */
    public clsEntEmpleadosListaGenerica objBusqueda { get { return (clsEntEmpleadosListaGenerica)Session["objDatosEmp" + Session.SessionID]; } set { Session["objDatosEmp" + Session.SessionID] = value; } }
    public List<clsEntEmpleadosListaGenerica> lisAsignaciones { get { return (List<clsEntEmpleadosListaGenerica>)Session["lisAsigna" + Session.SessionID]; } set { Session["lisAsigna" + Session.SessionID] = value; } }
    /* FIN ACTUALIZACIÓN */
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        #region isPostBack
        if (!IsPostBack)
        {
            Session["objEmpleado" + Session.SessionID] = Session["empleadoFoto" + Session.SessionID] = null;
            divError.Attributes.Add("class", "divError");
            divErrorWin.Attributes.Add("class", "divError");
            lstRegistros = null;
            List<clsEntEmpleadosListaGenerica> lst = new List<clsEntEmpleadosListaGenerica>();
            lstRegistros = lst;
            lblerror.Text = string.Empty;
            divError.Visible = false;
            clsEntSesion objSession = (clsEntSesion)Session["objSession" + Session.SessionID];
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuarioAsignacionLimitada", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);

            /* ACTUALIZACIÓN MARZO 2017 */
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicioWin, "catalogo.spLeerServiciosPorUsuarioAsignacionLimitada", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlFuncion, "catalogo.spLeerFuncionAsignacion", "faDescripcion", "idFuncionAsignacion", (clsEntSesion)Session["objSession" + Session.SessionID]);
            /* FIN ACTUALIZACIÓN */

            lblResponsable.Text = objSession.usuario.UsuNombre + " " + objSession.usuario.UsuPaterno + " " + objSession.usuario.UsuMaterno;
        }
        #endregion
    }

    protected void ddlInstalacion_SelectedIndexChanged(object sender, EventArgs e)
    {

        /* ACTUALIZACIÓN MARZO 2017 */
        bool MCS, hibrido;

        // si la instalación es de la zona de servicios entrará a la validación, de lo contrario permite la asistencia por biométrico y por pase de lista
        // 7 es el id de Zona de Servicios
        hibrido = clsNegAsistencia.paseListaHibrido(Convert.ToInt32(ddlServicio.SelectedValue), 7, Convert.ToInt32(ddlInstalacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);

        // 1 id de cualquier horario
        MCS = clsNegAsistencia.asistenciaBiometriocMCS(Convert.ToInt32(ddlServicio.SelectedValue), Convert.ToInt32(ddlInstalacion.SelectedValue),
                        1, (clsEntSesion)Session["objSession" + Session.SessionID]);

        if (!hibrido)
        {
            if (MCS != true)
            {

                string script = @"<script type='text/javascript'>
                                alert('Para el Servicio e Instalación seleccionado, la asistencia se realiza solo por biométrico');
                                </script>";
                popConsulta.Hide();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

                ddlServicio.ClearSelection();
                ddlServicio.Enabled = false;
                ddlInstalacion.Items.Clear();
                ddlHorario.Items.Clear();
                ddlHorario.Enabled = false;
                ddlTipoAsistencia.Items.Clear();
                ddlTipoAsistencia.ClearSelection();
                txbNoEmpleado.Text = string.Empty;
                rblMonta.ClearSelection();
                lstRegistros = null;
                grvListado.DataSource = lstRegistros;
                grvListado.DataBind();
                lblerror.Text = string.Empty;
                divError.Visible = false;
                lblErrorWin.Text = string.Empty;
                divErrorWin.Visible = false;
                rblMonta.Enabled = true;
                List<clsEntEmpleadosListaGenerica> lst = new List<clsEntEmpleadosListaGenerica>();
                lstRegistros = lst;
                btnGuardar.Enabled = true;
                divError.Attributes.Add("class", "divError");
                txbFecha.Text = string.Empty;
                lblInconsistencia.Text = string.Empty;
                txbFecha.Enabled = true;
                imbNacimiento.Enabled = true;

                return;
            }
        }
        /* FIN ACTUALIZACIÓN */

        #region Inicialización de objetos 
        lblerror.Text = string.Empty;
        divError.Visible = false;
        ddlHorario.Enabled = false;
        ddlHorario.ClearSelection();
        ddlTipoAsistencia.Enabled = false;
        ddlTipoAsistencia.Items.Clear();
        ddlTipoAsistencia.ClearSelection();
        #endregion


        #region Persiste BD
        if (ddlInstalacion.SelectedIndex != 0)
        {

            clsCatalogos.llenarCatalogoHorarioREALista(ddlHorario, Convert.ToInt32(ddlServicio.SelectedValue), Convert.ToInt32(ddlInstalacion.SelectedValue), Convert.ToDateTime(txbFecha.Text), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlHorario.Enabled = true;

            /*Integracion con CONEC mayo 2016 */
            Dictionary<string, clsEntParameter> parameters = new Dictionary<string, clsEntParameter>();
            parameters.Add("@idServicio", new clsEntParameter { Type = DbType.Int32, Value = Convert.ToInt32(ddlServicio.SelectedValue) });
            parameters.Add("@idInstalacion", new clsEntParameter { Type = DbType.Int32, Value = Convert.ToInt32(ddlInstalacion.SelectedValue) });

            clsCatalogos.llenarCatalogoConParametros(ddlTipoAsistencia, "catalogo.spLeerTipoAsistenciaCONEC", "estDescripcion", "idEstatus", parameters, (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlTipoAsistencia.Enabled = true;
            //Termina Cambio mayo 2016
        }

        #endregion

    }
    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region Inicialización de objetos
        lblerror.Text = string.Empty;
        divError.Visible = false;
        ddlInstalacion.Enabled = false;
        ddlInstalacion.ClearSelection();
        ddlHorario.Enabled = false;
        ddlHorario.ClearSelection();
        ddlTipoAsistencia.Enabled = false;
        ddlTipoAsistencia.ClearSelection();
        #endregion


        #region Persiste BD
        if (ddlServicio.SelectedIndex != 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuarioAsignacionLimitada", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacion.Enabled = true;

        }
        #endregion
    }

    /* ACTUALIZACIÓN MARZO 2017 */
    protected void ddlServicioWin_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region Inicialización de objetos
        lblerror.Text = string.Empty;
        divError.Visible = false;
        ddlInstalacionWin.Enabled = false;
        ddlInstalacionWin.ClearSelection();

        #endregion


        #region Persiste BD
        if (ddlServicio.SelectedIndex != 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacionWin, "catalogo.spLeerInstalacionesPorUsuarioAsignacionLimitada", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicioWin.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacionWin.Enabled = true;

        }
        #endregion
    }
    /* FIN ACTUALIZACIÓN */

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        ddlServicio.ClearSelection();
        ddlServicio.Enabled = false;
        ddlInstalacion.Items.Clear();
        ddlHorario.Items.Clear();
        ddlHorario.Enabled = false;
        ddlTipoAsistencia.Items.Clear();
        ddlTipoAsistencia.ClearSelection();
        txbNoEmpleado.Text = string.Empty;
        rblMonta.ClearSelection();
        lstRegistros = null;
        grvListado.DataSource = lstRegistros;
        grvListado.DataBind();
        lblerror.Text = string.Empty;
        divError.Visible = false;
        lblErrorWin.Text = string.Empty;
        divErrorWin.Visible = false;
        rblMonta.Enabled = true;
        List<clsEntEmpleadosListaGenerica> lst = new List<clsEntEmpleadosListaGenerica>();
        lstRegistros = lst;
        btnGuardar.Enabled = true;
        divError.Attributes.Add("class", "divError");
        txbFecha.Text = string.Empty;
        lblInconsistencia.Text = string.Empty;
        txbFecha.Enabled = true;
        imbNacimiento.Enabled = true;

    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        #region Inicialización de objetos

        string message = string.Empty;
        lblerror.Text = string.Empty;
        lblInconsistencia.Text = string.Empty;
        divError.Visible = false;
        #endregion

        #region Validaciones
        if (txbFecha.Text.Trim() == string.Empty) { message = "Es necesario insertar la fecha de asistencia"; }

        if (message == string.Empty && Convert.ToDateTime(txbFecha.Text.Trim()) > DateTime.Now)
        {
            message = "La fecha de asistencia no puede ser mayor al día de hoy";
        }
        if (ddlServicio.SelectedIndex == 0 && message == string.Empty) { message = "Es necesario seleccionar un servicio para poder continuar"; }
        if (ddlInstalacion.SelectedIndex == 0 && message == string.Empty) { message = "Es necesario seleccionar una instalación para poder continuar"; }
        if (ddlHorario.SelectedIndex == 0 && message == string.Empty) { message = "Es necesario seleccionar un horario para poder continuar "; }
        if (ddlTipoAsistencia.SelectedIndex == 0 && message == string.Empty) { message = "Es necesario seleccionar el tipo de asistencia para poder continuar"; }
        if (txbNoEmpleado.Text.Trim() == string.Empty && message == string.Empty) { message = "Es necesario escribir un número de empleado para poder continuar"; }
        if (message == string.Empty)
        {
            if ((from a in lstRegistros where a.empNumero == txbNoEmpleado.Text.Trim() select a).Count() != 0)
            {
                message = "El integrante ya ha sido agregado";
            }
        }
        if (rblMonta.SelectedValue == "" && message == string.Empty) { message = "Debe selecionar la situación del integrante"; }


        if (message != string.Empty)
        {
            lblerror.Text = message;
            divError.Visible = true;
            txbNoEmpleado.Focus();
            return;
        }

        #endregion

        #region Add Row
        string[] horario = ddlHorario.SelectedValue.Split('|');
        byte existe = 0;
        string excep = string.Empty;


        objBusqueda = clsNegEmpleado.consultarIntegrantes(Convert.ToDateTime(txbFecha.Text.Trim()), txbNoEmpleado.Text.Trim(), Convert.ToInt32(horario[0]), (clsEntSesion)Session["objSession" + Session.SessionID], ref existe, Convert.ToInt32(ddlServicio.SelectedValue), Convert.ToInt32(ddlInstalacion.SelectedValue), ref excep);

        // Regresa un empleado con su idEmpleado 
        // aqui mandar a llamar una funcion que regrese una consulta a la tbla de empleado asignacion para comparar las instalaciones en las que tiene asignacion y en la que
        // pasa lista si esta instalacion es diferente mostrar mensaje de si quiere modificar la asignacion


        if (objBusqueda != null)
        {

            byte idTipoAsistencia = Convert.ToByte(ddlTipoAsistencia.SelectedValue);
            objBusqueda.idHorario = Convert.ToInt32(horario[0]);
            objBusqueda.horDescripcion = ddlHorario.SelectedItem.Text;
            objBusqueda.idEstatus = idTipoAsistencia;
            objBusqueda.tipoAsistencia = ddlTipoAsistencia.SelectedItem.Text;
            objBusqueda.asiHora = horario[1];
            objBusqueda.fechaAsistencia = Convert.ToDateTime(txbFecha.Text.Trim());

            /* ACTUALIZACIÓN MARZO 2017 PARA QUITAR INCONSISTENCIAS */
            objBusqueda.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
            objBusqueda.idInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);

            #region Dias Agregados

            string[] entrada = horario[1].Split(':');
            int hrEntrada = Convert.ToInt32(entrada[0]);
            int hrJornada = Convert.ToInt32(horario[3]);
            objBusqueda.diasAgregados = (hrEntrada + hrJornada) > 24 ? 1 : 0;
            #endregion

            objBusqueda.asiHoraSalida = horario[2];


            objBusqueda.idMonta = Convert.ToByte(rblMonta.SelectedValue);

            lisAsignaciones = clsDatEmpleadoAsignacion.consultaAsignacion(objBusqueda.idEmpleado, (clsEntSesion)Session["objSession" + Session.SessionID]);

            if (lisAsignaciones != null)
            {
                string fecha = lisAsignaciones[0].eaFechaIngreso.Date.ToString("dd/MM/yyyy");

                if (lisAsignaciones[0].eaFechaIngreso.Date == Convert.ToDateTime(txbFecha.Text.Trim()) && lisAsignaciones[0].idServicio != objBusqueda.idServicio)
                {
                    popConsulta.Show();

                    ddlServicioWin.Visible = false;
                    ddlInstalacionWin.Visible = false;
                    ddlFuncion.Visible = false;
                    btnModificar.Visible = false;
                    btnCerrar.Visible = false;
                    btnSalir.Visible = true;
                    lblServicio.Visible = false;
                    lblInstalacion.Visible = false;
                    lblFuncion.Visible = false;

                    lblMensajeWin.Text = "El integrante fue asignado al servicio: " + lisAsignaciones[0].serDescripcion + ", instalación: " + lisAsignaciones[0].insNombre + ". <br> el día de hoy, por lo que no es posible pasarle lista.";

                    return;
                }

                if (lisAsignaciones[0].eaFechaIngreso.Date == Convert.ToDateTime(txbFecha.Text.Trim()) && lisAsignaciones[0].idInstalacion != objBusqueda.idInstalacion)
                {
                    popConsulta.Show();

                    ddlServicioWin.Visible = false;
                    ddlInstalacionWin.Visible = false;
                    ddlFuncion.Visible = false;
                    btnModificar.Visible = false;
                    btnCerrar.Visible = false;
                    btnSalir.Visible = true;
                    lblServicio.Visible = false;
                    lblInstalacion.Visible = false;
                    lblFuncion.Visible = false;

                    lblMensajeWin.Text = "El integrante fue asignado a la instalación: " + lisAsignaciones[0].insNombre + ". <br> el día de hoy, por lo que no es posible pasarle lista.";

                    return;
                }

                if (lisAsignaciones[0].idServicio == objBusqueda.idServicio && lisAsignaciones[0].eaFechaIngreso.Date > Convert.ToDateTime(txbFecha.Text.Trim()))
                {
                    popConsulta.Show();

                    ddlServicioWin.Visible = false;
                    ddlInstalacionWin.Visible = false;
                    ddlFuncion.Visible = false;
                    btnModificar.Visible = false;
                    btnCerrar.Visible = false;
                    btnSalir.Visible = true;
                    lblServicio.Visible = false;
                    lblInstalacion.Visible = false;
                    lblFuncion.Visible = false;

                    lblMensajeWin.Text = "El integrante fue asignado al servicio: " + lisAsignaciones[0].serDescripcion + ", instalación: " + lisAsignaciones[0].insNombre + ". <br> para el dia " + fecha + ", por lo que no es posible pasarle lista el día de hoy.";

                    return;
                }

                if (lisAsignaciones[0].eaFechaIngreso.Date > Convert.ToDateTime(txbFecha.Text.Trim()) && lisAsignaciones[1].idServicio == objBusqueda.idServicio)
                {
                    txbNoEmpleado.Focus();
                }
                else
                {
                    if (lisAsignaciones[0].idServicio != objBusqueda.idServicio)
                    {
                        popConsulta.Show();
                        btnSalir.Visible = false;
                        //pnlConsulta.Visible = true;
                        ddlServicioWin.SelectedIndex = ddlServicio.SelectedIndex;
                        clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacionWin, "catalogo.spLeerInstalacionesPorUsuarioAsignacionLimitada", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicioWin.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
                        ddlInstalacionWin.SelectedIndex = ddlInstalacion.SelectedIndex;
                        lblMensajeWin.Text = "El integrante se encuentra asignado actualmente en el servicio: " + lisAsignaciones[0].serDescripcion + ", instalación: " + lisAsignaciones[0].insNombre + ". <br> Para realizar el pase de lista es necesario cambiar su asignación.";
                        ddlServicioWin.Enabled = false;
                        ddlInstalacionWin.Enabled = false;
                    }
                    else
                    {
                        if(lisAsignaciones[0].idInstalacion != objBusqueda.idInstalacion)
                        {
                            popConsulta.Show();
                            btnSalir.Visible = false;
                            //pnlConsulta.Visible = true;
                            ddlServicioWin.SelectedIndex = ddlServicio.SelectedIndex;
                            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacionWin, "catalogo.spLeerInstalacionesPorUsuarioAsignacionLimitada", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicioWin.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
                            ddlInstalacionWin.SelectedIndex = ddlInstalacion.SelectedIndex;
                            lblMensajeWin.Text = "El integrante se encuentra asignado actualmente en la instalación: " + lisAsignaciones[0].insNombre + ". <br> Para realizar el pase de lista es necesario cambiar su asignación.";
                            ddlServicioWin.Enabled = false;
                            ddlInstalacionWin.Enabled = false;
                        }
                    }

                }


            }



            //#region Dias Agregados

            //string[] entrada = horario[1].Split(':');
            //int hrEntrada = Convert.ToInt32(entrada[0]);
            //int hrJornada = Convert.ToInt32(horario[3]);
            //objBusqueda.diasAgregados = (hrEntrada + hrJornada) > 24 ? 1 : 0;
            //#endregion

            //objBusqueda.asiHoraSalida = horario[2];


            //objBusqueda.idMonta =Convert.ToByte(rblMonta.SelectedValue);

            lstRegistros.Insert(0, objBusqueda);
            grvListado.DataSource = lstRegistros;
            grvListado.DataBind();
            txbNoEmpleado.Text = string.Empty;

            ddlServicio.Enabled = ddlInstalacion.Enabled = rblMonta.Enabled = ddlHorario.Enabled = imbNacimiento.Enabled = ddlTipoAsistencia.Enabled = false;
            txbFecha.Enabled = false;

            /* FIN ACTUALIZACIÓN */
        }
        else
        {
            lblerror.Text = excep.ToLower();
            lblerror.Visible = true;
            divError.Visible = true;
            txbNoEmpleado.Focus();
            return;
        }
        #endregion
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmInicio.aspx");
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        #region Inicialización de variables
        string message = string.Empty;
        #endregion

        #region Validaciones
        if (lstRegistros == null) { message = "No existe ningun registro agregado"; }
        if (message == string.Empty) { if (lstRegistros.Count == 0) { message = "No existe ningun registro agregado"; } }
        if (message != string.Empty)
        {
            lblerror.Text = message;
            divError.Visible = true;
            txbNoEmpleado.Focus();
            return;
        }
        #endregion

        #region Persiste BD
        if (message == string.Empty)
        {

            /* ACTUALIZACIÓN MARZO 2017 PARA QUITAR INCONSISTENCIAS */

            int registro = 0;
            int error = 0;
            int inconsistencia = 0;
            int cambiaInconsistencia = 0;

            message = clsNegAsistencia.insertarListaAsistencia(lstRegistros, (clsEntSesion)Session["objSession" + Session.SessionID], ref registro, ref error, ref inconsistencia, ref cambiaInconsistencia);

            if (registro == 1 && error == 0)
            {
                lblerror.Text = message == string.Empty ? "<b>Registros guardados correctamente<b>" : "Error Inesperado consulte al administrador del sistema </br><b>Error:<b>" + message;
                divError.Attributes.Add("class", "divOk");
                divError.Visible = true;
                btnGuardar.Enabled = false;

                //if (inconsistencia == 1)
                //{
                //    lblInconsistencia.Text = cambiaInconsistencia == 1 ? "<b>Existieron inconsistencias las cuales fueron eliminadas<b>" : "Se generaron inconsistencias y no se pudieron eliminar, consulte al administrador del sistema";
                //    divError.Attributes.Add("class", "divOk");
                //    divError.Visible = true;
                //    btnGuardar.Enabled = false;
                //}
                //else
                //{
                //    lblInconsistencia.Text = "No se detectaron inconsistencias";
                //    divError.Attributes.Add("class", "divOk");
                //    divError.Visible = true;
                //    btnGuardar.Enabled = false;
                //}
            }
            else
            {
                if (error == 1)
                {
                    lblerror.Text = "El integrante ya tiene una asistencia el día de hoy.";
                    divError.Attributes.Add("class", "divError");
                    divError.Visible = true;
                }
                else
                {
                    lblerror.Text = "No pudo insertar el registro en la base de datos, por favor contecte al administrador del sistema";
                    divError.Attributes.Add("class", "divError");
                    divError.Visible = true;
                }
            }
            return;

            //int registro = 0; int error = 0;

            //message =  clsNegAsistencia.insertarListaAsistencia(lstRegistros, (clsEntSesion)Session["objSession" + Session.SessionID],ref registro,ref error);
            //if (registro == 1 && error == 0)
            //{
            //    lblerror.Text = message == string.Empty ? "<b>Registros guardados correctamente<b>" : "Error Inesperado consulte al administrador del sistema </br><b>Error:<b>" + message;
            //    divError.Attributes.Add("class", "divOk");
            //    divError.Visible = true;
            //    btnGuardar.Enabled = false;
            //}
            //else
            //{
            //    lblerror.Text = "No pudo insertar el registro en la base de datos, por favor contecte al administrador del sistema";
            //    divError.Attributes.Add("class", "divError");
            //    divError.Visible = true;
            //}
            //return;

            /* FIN ACTUALIZACIÓN */
        }
        #endregion

    }

    protected void grvListado_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        lstRegistros.RemoveAt(e.RowIndex);
        grvListado.DataSource = lstRegistros;
        grvListado.DataBind();

    }
    protected void txbFecha_TextChanged(object sender, EventArgs e)
    {
        DateTime fecha, fechaMinima;

        if (DateTime.TryParse(txbFecha.Text, out fecha))
        {
            DateTime.TryParse("1/1/1900", out fechaMinima);
            if (fecha > fechaMinima)
                ddlServicio.Enabled = true;
            else
            {
                lblerror.Text = "Inserte una fecha válida";
                divError.Attributes.Add("class", "divError");
                divError.Visible = true;
                ddlServicio.Enabled = false;
            }
        }
        else
        {
            lblerror.Text = "Inserte una fecha válida";
            divError.Attributes.Add("class", "divError");
            divError.Visible = true;
            ddlServicio.Enabled = false;
        }


        ddlServicio.ClearSelection();
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
        ddlHorario.Items.Clear();
        ddlHorario.Enabled = false;
        ddlTipoAsistencia.Items.Clear();
        ddlTipoAsistencia.Enabled = false;
        ddlTipoAsistencia.ClearSelection();
    }

    /* ACTUALIZACIÓN MARZO 2017 */
    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        lstRegistros.RemoveAt(0);
        grvListado.DataSource = lstRegistros;
        grvListado.DataBind();


        ddlServicioWin.ClearSelection();
        ddlInstalacionWin.Items.Clear();
        ddlInstalacionWin.Enabled = false;
        ddlFuncion.ClearSelection();

        popConsulta.Hide();

    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        string messageWin = string.Empty;
        lblErrorWin.Text = string.Empty;
        divErrorWin.Visible = false;

        if (ddlServicioWin.SelectedIndex == 0 && messageWin == string.Empty) { messageWin = "Es necesario seleccionar un servicio para poder continuar"; }
        if (ddlInstalacionWin.SelectedIndex == 0 && messageWin == string.Empty) { messageWin = "Es necesario seleccionar una instalación para poder continuar"; }
        if (ddlFuncion.SelectedIndex == 0 && messageWin == string.Empty) { messageWin = "Es necesario seleccionar una función para poder continuar "; }

        if (messageWin != string.Empty)
        {
            popConsulta.Show();
            lblErrorWin.Text = messageWin;
            divErrorWin.Visible = true;
            return;
        }
        else
        {
            objBusqueda.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
            objBusqueda.idInstalacion = Convert.ToInt32(ddlInstalacionWin.SelectedValue);
            objBusqueda.idFuncionAsignacion = Convert.ToInt32(ddlFuncion.SelectedValue);

            txbNoEmpleado.Text = string.Empty;
            txbNoEmpleado.Focus();

            popConsulta.Hide();

            ddlFuncion.ClearSelection();
        }
    }

    protected void btnSalir_Click(object sender, EventArgs e)
    {
        popConsulta.Hide();

        ddlServicioWin.Visible = true;
        ddlInstalacionWin.Visible = true;
        ddlFuncion.Visible = true;
        lblServicio.Visible = true;
        lblInstalacion.Visible = true;
        lblFuncion.Visible = true;
        btnModificar.Visible = true;
        btnCerrar.Visible = true;
        btnSalir.Visible = false;
    }
}