using System;
using System.Collections.Generic;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using SICOGUA.Datos;
using SICOGUA.Negocio;
using SICOGUA.proNegocio;
using System.Web.UI.WebControls;


using System.Web.UI;


using System.Collections.Specialized;

using System.Collections;

public partial class Personal_frmDatosLaborales : System.Web.UI.Page
{
    static private List<clsEntFirmanteOficioAsignacion> listaFirmantes = new List<clsEntFirmanteOficioAsignacion>();
    public static List<clsEntEmpleadoHorarioREA> lisHorarioOriginal = new List<clsEntEmpleadoHorarioREA>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["impresion" + Session.SessionID] = null;
            clsCatalogos.llenarCatalogo(ddlJerarquia, "catalogo.spLeerJerarquia", "jerDescripcion", "idJerarquia", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuarioAsignacion", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlFuncion, "catalogo.spLeerFuncionAsignacion", "faDescripcion", "idFuncionAsignacion", (clsEntSesion)Session["objSession" + Session.SessionID]);
            #region Solo Consulta
            clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
            clsEntPermiso objPermiso = new clsEntPermiso();
            objPermiso.IdPerfil = objSesion.usuario.Perfil.IdPerfil;

            //Operacion 32 Solo Consulta-ASIGNACIONES
            if (clsDatPermiso.consultarPermiso(objPermiso.IdPerfil, 32, objSesion) == false)
            {
                btnGuardar.Visible = btnAgregarAsignacion.Enabled = false;
                btnAgregar.Visible = false;

                //ddlZona.Enabled = false;
                ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                txbFechaAlta.Enabled = txbFechaBaja.Enabled = cleFechaBaja.Enabled = false;
                grvAsignacion.Columns[6].Visible = false;

            }
            #endregion
            clsEntEmpleadoAsignacion objAsignacion = new clsEntEmpleadoAsignacion();
            objAsignacion.Servicio = new clsEntServicio();
            objAsignacion.Instalacion = new clsEntInstalacion();

            objAsignacion.IdEmpleadoAsignacion = Convert.ToInt16(hfIdEmpleadoAsignacion.Value);

            Session["lstAsignaciones" + Session.SessionID] = null;
            Session["lisHorario"] = null;
            Session["OficioNuevo" + Session.SessionID] = null;
            Session["OficioNuevoAnterior" + Session.SessionID] = null;
            hfOficioFlag.Value = "-1";

            grvAsignacion.DataSource = null;
            grvAsignacion.DataBind();

            if (Session["objEmpleado" + Session.SessionID] != null)
            {
                clsEntEmpleado objEmpleado = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];
                #region incidencias
                List<clsEntIncidencia> lisIncidencias = objEmpleado.Incidencias;
                if (lisIncidencias != null)
                {
                    List<clsEntIncidencia> lisIncidenciasFuturas = lisIncidencias.FindAll(delegate(clsEntIncidencia p) { return p.IncFechaInicial > DateTime.Today; });
                    lisIncidencias = lisIncidencias.FindAll(delegate(clsEntIncidencia p) { return p.IncFechaInicial <= DateTime.Today && p.IncFechaFinal >= DateTime.Today; });

                    // si la lista regresa mas de cero tiene incidencia y no debo dejar modificar
                    if ((lisIncidencias != null && lisIncidencias.Count > 0) || (lisIncidenciasFuturas != null && lisIncidenciasFuturas.Count > 0))
                    {
                        btnGuardar.Visible = btnAgregarAsignacion.Enabled = false;
                        btnAgregar.Visible = false;
                        ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                        grvAsignacion.Columns[6].Visible = false;
                        lblEmpleadoBaja.Visible = true;
                        divErrorAsi.Visible = true;
                        lblEmpleadoBaja.Text = "El empleado tiene una incidencia, por lo que no se podrá realizar ningún movimiento";
                    }
                }
                #endregion
                #region faltas
                clsNegEmpleado.consultarPaseListaREA(ref objEmpleado, objSesion);
                if (objEmpleado.PaseLista != null && objEmpleado.PaseLista.strTipoAsistencia == "FALTA")
                {

                    btnGuardar.Visible = btnAgregarAsignacion.Enabled = false;
                    btnAgregar.Visible = false;
                    ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                    txbFechaAlta.Enabled = txbFechaBaja.Enabled = cleFechaBaja.Enabled = false;
                    grvAsignacion.Columns[6].Visible = false;
                    lblEmpleadoBaja.Visible = true;
                    divErrorAsi.Visible = true;
                    lblEmpleadoBaja.Text = "El empleado tiene FALTA, por lo que no se podrá realizar ningún movimiento";
                }
                #endregion
                //#region sanciones
                //bool booSuspension = false, booInhabilitacion = false;
                //clsNegAsistencia.validarSancion(objEmpleado.IdEmpleado, ref booSuspension, ref booInhabilitacion, objSesion);
                //if (booSuspension== true)
                //{

                //    btnGuardar.Visible = btnAgregarAsignacion.Enabled = false;
                //    btnAgregar.Visible = false;
                //    ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                //    txbFechaAlta.Enabled = txbFechaBaja.Enabled = cleFechaBaja.Enabled = false;
                //    grvAsignacion.Columns[6].Visible = false;
                //    lblEmpleadoBaja.Visible = true;
                //    divErrorAsi.Visible = true;
                //    lblEmpleadoBaja.Text = "El empleado tiene una SUSPENSIÓN, por lo que no se podrá realizar ningún movimiento";
                //}
                //if (booInhabilitacion == true)
                //{

                //    btnGuardar.Visible = btnAgregarAsignacion.Enabled = false;
                //    btnAgregar.Visible = false;
                //    ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                //    txbFechaAlta.Enabled = txbFechaBaja.Enabled = cleFechaBaja.Enabled = false;
                //    grvAsignacion.Columns[6].Visible = false;
                //    lblEmpleadoBaja.Visible = true;
                //    divErrorAsi.Visible = true;
                //    lblEmpleadoBaja.Text = "El empleado tiene una INHABILITACIÓN, por lo que no se podrá realizar ningún movimiento";
                //}
                //#endregion
                hfIdEmpleado.Value = objEmpleado.IdEmpleado.ToString();
                

                //SI REGRESA 1 SIGNIFICA QUE SOLO SERA CONSULTA
                //paso idEmpleado y idUsuario
                clsEntUsuario objUsuario = new clsEntUsuario();
                objUsuario.IdUsuario = objSesion.usuario.IdUsuario;
                if (clsDatPermiso.consultarPermisoAsignaciones(objEmpleado.IdEmpleado, objUsuario.IdUsuario, objSesion) == 1)
                {
                    //SOLO CONSULTA
                    btnGuardar.Visible = btnAgregarAsignacion.Enabled = false;
                    btnAgregar.Visible = false;
                    ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                    txbFechaAlta.Enabled = txbFechaBaja.Enabled = cleFechaBaja.Enabled = false;
                    grvAsignacion.Columns[6].Visible = false;
                }
                //***********


                //TENGO PERMISO PARA CUNSULTAR LA ULTIMA ASIGNACIÓN INGRESADA
                int servicioUltimo = objEmpleado.EmpleadoAsignacion[0].Servicio.idServicio;
                int instalacionUltimo = objEmpleado.EmpleadoAsignacion[0].Instalacion.IdInstalacion;

                //                int idEmpleadoAsignacion = objEmpleado.EmpleadoAsignacion[0].IdEmpleadoAsignacion;

                if (clsDatPermiso.consultarPermisoPorServicioAsignados(objSesion.usuario.IdUsuario, servicioUltimo, instalacionUltimo, objSesion) == false)
                {
                    btnGuardar.Visible = btnAgregarAsignacion.Enabled = false;
                    btnAgregar.Visible = false;
                    ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                    txbFechaAlta.Enabled = txbFechaBaja.Enabled = cleFechaBaja.Enabled = false;

                    grvAsignacion.Columns[6].Visible = false;

                }


                #region Datos Generales

                clsUtilerias.llenarLabel(lblNombreEmpleado, objEmpleado.EmpPaterno + " " + objEmpleado.EmpMaterno + " " + objEmpleado.EmpNombre, "-");
                clsUtilerias.llenarLabel(lblNumeroEmpleado, objEmpleado.EmpNumero, "-");
                clsUtilerias.llenarLabel(lblCUIP, objEmpleado.EmpCUIP, "-");
                lblFechaAlta.Text = objEmpleado.EmpFechaIngreso.ToShortDateString() != "01/01/1900" &&
                                    objEmpleado.EmpFechaIngreso.ToShortDateString() != "01/01/0001"
                                        ? objEmpleado.EmpFechaIngreso.ToShortDateString()
                                        : "-";

                #endregion

                #region Datos Laborales

                clsUtilerias.seleccionarDropDownList(ddlJerarquia, objEmpleado.EmpleadoPuesto.Puesto.Jerarquia.IdJerarquia);
                ddlJerarquia_SelectedIndexChanged(null, null);
                clsUtilerias.seleccionarDropDownList(ddlPuesto, objEmpleado.EmpleadoPuesto.Puesto.IdPuesto);
                //   clsUtilerias.seleccionarDropDownList(ddlTipoHorario, objEmpleado.EmpleadoHorario.horario.IdHorario + "&" + objEmpleado.EmpleadoHorario.horario.tipoHorario.IdTipoHorario);
                //  txbFechaInicioHorario.Text = objEmpleado.EmpleadoHorario.EhFechaingreso.ToShortDateString() !=
                //"01/01/1900" &&
                //objEmpleado.EmpleadoHorario.EhFechaingreso.ToShortDateString() !=
                //"01/01/0001"
                //    ? objEmpleado.EmpleadoHorario.EhFechaingreso.ToShortDateString()
                //   : "";

                //clsUtilerias.seleccionarDropDownList(ddlZona, objEmpleado.EmpleadoAsignacionOS.Zona.IdZona);
                //ddlZona_SelectedIndexChanged(null, null);





                #endregion

                #region Asignaciones

                Session["lstAsignaciones" + Session.SessionID] = objEmpleado.EmpleadoAsignacion;
                grvAsignacion.DataSource = Session["lstAsignaciones" + Session.SessionID];
                grvAsignacion.DataBind();

                #endregion

                grid();

                #region Solo consulta en base a Servicios, instalaciones y zonas asignadas
                // consulto permiso de la persona para deshabiliatr botones de OPERACION
                int servicioActual;
                int instalacionActual;
                servicioActual = objEmpleado.EmpleadoAsignacion[0].Servicio.idServicio;
                instalacionActual = objEmpleado.EmpleadoAsignacion[0].Instalacion.IdInstalacion;

                if (clsDatPermiso.consultarPermisoPorServicioAsignados(objSesion.usuario.IdUsuario, servicioActual, instalacionActual, objSesion) == false)
                {
                    btnGuardar.Visible = btnAgregarAsignacion.Enabled = false;
                    btnAgregar.Visible = false;
                    //ddlTipoHorario.Enabled = txbFechaInicioHorario.Enabled = false;
                    ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                    txbFechaAlta.Enabled = txbFechaBaja.Enabled = cleFechaBaja.Enabled = false;
                    grvAsignacion.Columns[6].Visible = false;
                }
                #endregion

                #region EMPLEADOS INACTIVOS
                if (objEmpleado.EmpFechaBaja.Date <= DateTime.Now
                && objEmpleado.EmpFechaBaja.ToShortDateString() != "01/01/1900"
                && objEmpleado.EmpFechaBaja.ToShortDateString() != "01/01/0001")
                {
                    lblEmpleadoBaja.Visible = true;
                    divErrorAsi.Visible = true;
                    lblEmpleadoBaja.Text = "El empleado ha sido dado de baja por lo que no se podrá realizar ningún movimiento";
                    //CalendarExtender1.Enabled = false;
                    //ddlTipoHorario.Enabled = false;
                    //txbFechaInicioHorario.Enabled = false;
                    btnAgregarAsignacion.Enabled = false;
                    ddlServicio.Enabled = ddlInstalacion.Enabled = false;

                    ddlFuncion.Enabled = false;
                    txbFechaAlta.Enabled = false;
                    txbFechaBaja.Enabled = false;
                    imbFechaBaja.Enabled = false;
                    imbFechaAlta.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnGuardar.Enabled = false;

                    lblFechaBajas.Text = objEmpleado.EmpFechaBaja.ToShortDateString();
                }

                if (objEmpleado.idRevision != 0)
                {
                    lblEmpleadoBaja.Visible = true;
                    divErrorAsi.Visible = true;
                    if (objEmpleado.idRenuncia != 0)
                    {
                        lblEmpleadoBaja.Text = "El empleado tiene iniciado un procedimiento por lo que no se podrá realizar ningún movimiento adicionalmente tiene reporte de renuncia";

                    }
                    else
                    {
                        lblEmpleadoBaja.Text = "El empleado tiene iniciado un procedimiento por lo que no se podrá realizar ningún movimiento";
                    }
                    //    CalendarExtender1.Enabled = false;
                    //ddlTipoHorario.Enabled = false;
                    //txbFechaInicioHorario.Enabled = false;
                    btnAgregarAsignacion.Enabled = false;
                    ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                    txbFechaHorario.Enabled = imbFechaHorario.Enabled = btnAgregarHorario.Enabled = ddlHorario.Enabled = false;
                    ddlFuncion.Enabled = false;
                    txbFechaAlta.Enabled = false;
                    txbFechaBaja.Enabled = false;
                    imbFechaBaja.Enabled = false;
                    imbFechaAlta.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnGuardar.Enabled = false;
                    hfIdRevision.Value = Convert.ToString(objEmpleado.idRevision);
                }
                else
                {
                    hfIdRevision.Value = "-1";



                    if (objEmpleado.idRenuncia != 0)
                    {
                        lblEmpleadoBaja.Visible = true;
                        divErrorAsi.Visible = true;
                        lblEmpleadoBaja.Text = "El empleado tiene reporte de renuncia";
                        hfIdRenuncia.Value = Convert.ToString(objEmpleado.idRenuncia);

                    }
                }



                #endregion
            }
            else
            {
                string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
                string script = "if(confirm('¡Para guardar un registro es necesario seleccionar un Empleado!.')) location.href='./../" + busqueda + "'; else location.href='./../frmInicio.aspx';";
                ClientScript.RegisterClientScriptBlock(GetType(), "Mensaje", script, true);
            }
        }
    }

    protected void grvCartilla_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hfColumna.Value = e.CommandArgument.ToString();
        if (e.CommandName == "Consultar")
        {
            clsEntEmpleadoAsignacion objAsignacion = ((List<clsEntEmpleadoAsignacion>)Session["lstAsignaciones" + Session.SessionID])[Convert.ToInt32(e.CommandArgument)];

            #region carga Documento
            clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];

            clsEntOficioAsignacion oficioAsignacion = clsNegOficioAsignacion.obtieneOficioAsignacion(new Guid(hfIdEmpleado.Value), objAsignacion.IdEmpleadoAsignacion, objSesion);

            if (Convert.ToInt32(e.CommandArgument) == 0)
            {
               
                    if (Session["OficioNuevo" + Session.SessionID] == null)
                    {
                        if (hfOficioFlag.Value == "1")
                        {
                                Session["byPdfOficioAsig" + Session.SessionID] = null;
                                imbOficio.OnClientClick = "alert('No hay archivo adjunto.');";
                                imgOficioVista.OnClientClick = "alert('No hay archivo adjunto.');";
                        }
                        else{
                                if (oficioAsignacion.oficioAsignacion != null)
                                {
                                    Session["byPdfOficioAsig" + Session.SessionID] = oficioAsignacion.oficioAsignacion;
                                    imbOficio.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                                    imgOficioVista.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                                }
                                else
                                {
                                    Session["byPdfOficioAsig" + Session.SessionID] = null;
                                    imbOficio.OnClientClick = "alert('No hay archivo adjunto.');";
                                    imgOficioVista.OnClientClick = "alert('No hay archivo adjunto.');";
                                }
                        }
                    }
                    else
                    {
                        Session["byPdfOficioAsig" + Session.SessionID] = Session["OficioNuevo" + Session.SessionID];
                        imbOficio.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                        imgOficioVista.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                    }
                
            }
            else
            {
                if (Convert.ToInt32(e.CommandArgument) == 1)
                {
                    if (Session["OficioNuevoAnterior" + Session.SessionID] != null)
                    {
                        Session["byPdfOficioAsig" + Session.SessionID] = Session["OficioNuevoAnterior" + Session.SessionID];
                        imbOficio.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                        imgOficioVista.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                    }
                    else
                    {
                        if (oficioAsignacion.oficioAsignacion != null)
                        {
                            Session["byPdfOficioAsig" + Session.SessionID] = oficioAsignacion.oficioAsignacion;
                            imbOficio.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                            imgOficioVista.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                        }
                        else
                        {
                            Session["byPdfOficioAsig" + Session.SessionID] = null;
                            imbOficio.OnClientClick = "alert('No hay archivo adjunto.');";
                            imgOficioVista.OnClientClick = "alert('No hay archivo adjunto.');";
                        }
                    }
                }
                else
                {
                    if (oficioAsignacion.oficioAsignacion != null)
                    {
                        Session["byPdfOficioAsig" + Session.SessionID] = oficioAsignacion.oficioAsignacion;
                        imbOficio.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                        imgOficioVista.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                    }
                    else
                    {
                        Session["byPdfOficioAsig" + Session.SessionID] = null;
                        imbOficio.OnClientClick = "alert('No hay archivo adjunto.');";
                        imgOficioVista.OnClientClick = "alert('No hay archivo adjunto.');";
                    }
                }
            }

            #endregion


            lblServicio.Text = objAsignacion.Servicio.serDescripcion;
            lblInstalacion.Text = objAsignacion.Instalacion.InsNombre;
            lblFuncion.Text = objAsignacion.funcionAsignacion;
            lblFechaInicio.Text = objAsignacion.EaFechaIngreso.ToShortDateString();
            lblOficio.Text = objAsignacion.oficio;
            lblFechaFin.Text = objAsignacion.EaFechaBaja.ToShortDateString() == "01/01/0001" ? "" : objAsignacion.EaFechaBaja.ToShortDateString() == "01/01/1900" ? "" : objAsignacion.EaFechaBaja.ToShortDateString();
            grvHorarioConsulta.DataSource = objAsignacion.horarios;
            grvHorarioConsulta.DataBind();

             popConsulta.Show();
            }
        }
    

    public void grid()
    {

        int limite = grvAsignacion.Rows.Count;
        int inicio = 0;

        if (limite != 0)
        {
            do
            {
                GridViewRow row = grvAsignacion.Rows[inicio];
                TableCell tcPos = row.Cells[5];
                TableCell tcPos1 = row.Cells[6];
                Control objPos = tcPos.FindControl("imbSeleccionar");
                Control objPos1 = tcPos.FindControl("imbEliminar");
                Control objPos2 = tcPos.FindControl("imbConsultar");
                ImageButton imbUpdate = (ImageButton)objPos;
                ImageButton imbDelete = (ImageButton)objPos1;
                ImageButton imbConsultar = (ImageButton)objPos2;
                if (inicio != 0) { imbUpdate.Visible = false; }
                if (inicio != 0) { imbDelete.Visible = false; }
                if (inicio != 0) { imbConsultar.Visible = true; }
                //else { imbConsultar.Visible = false; }
                if (inicio == 0 && hfEliminar.Value == "1") { imbDelete.Visible = true; } else { imbDelete.Visible = false; }
                inicio++;
            } while (inicio < limite);

            int pos = 0;
            if (limite == 1) { pos = 0; } else { pos = 0; }
            GridViewRow rowIni = grvAsignacion.Rows[pos];
            TableCell tcPosIni = rowIni.Cells[4];
            Control objPosIni = tcPosIni.FindControl("lblFechaBaja");
            Label lblFechaFin = (Label) objPosIni;
            if (lblFechaFin.Text == "" || lblFechaFin.Text == "-")
                btnAgregarAsignacion.Enabled = false;
            
            Label lblFechaIni = (Label)objPosIni;
            if (lblFechaIni.Text != "")
            {
                DateTime dtfechaFinal = Convert.ToDateTime(lblFechaIni.Text);
                DateTime dtfechaAgregada = dtfechaFinal.AddDays(1);
                txbFechaAlta.Text = dtfechaAgregada.ToShortDateString();
                txbOficio.Text = "";
                ddlServicio.Enabled = ddlInstalacion.Enabled = true;
                Session["lisHorario"] = null;
                if (txbFechaBaja.Text.Length == 0)
                {
                    txbFechaHorario.Text = dtfechaAgregada.ToShortDateString();
                }
               txbFechaHorario.Enabled = false;
               imbFechaHorario.Enabled = false;
                List<clsEntEmpleadoHorarioREA> lishorarios = new List<clsEntEmpleadoHorarioREA>();
                grVHorario.DataSource = lishorarios;
                grVHorario.DataBind();
                Session["lisHorario"] = lishorarios;

            }
        }
        else
        {
            txbFechaAlta.Text = string.Empty;
            txbFechaAlta.Enabled = true;

            imbFechaAlta.Enabled = true;
        }
        GridViewRow rowIniF = grvAsignacion.Rows[0];
        TableCell tcPosIniF = rowIniF.Cells[4];
        Control objPosIniF = tcPosIniF.FindControl("lblFechaBaja");
        Label lblFechaFinF = (Label)objPosIniF;
        if (lblFechaFinF.Text == "" || lblFechaFinF.Text == "-")
            btnAgregarAsignacion.Enabled = false;
        else
        {
         
                 btnAgregarAsignacion.Enabled = true;
        }
        if (hfEliminar.Value == "1")
        {
            btnAgregarAsignacion.Enabled = false;
        }
      
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Session["lisHorario"] = null;
        Session["OficioNuevoAnterior" + Session.SessionID]=null;
        Session["OficioNuevo" + Session.SessionID] = null;
        string busqueda = "Generales/frmBusqueda.aspx?Redirect=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "") + "&Cancel=" + Server.UrlEncode(Page.AppRelativeVirtualPath + "");
        Response.Redirect("~/" + busqueda);
    }

    protected void ddlJerarquia_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlJerarquia.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoPuesto(ddlPuesto, "catalogo.spLeerPuesto", "pueDescripcion", "idPuesto", Convert.ToInt32(ddlJerarquia.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlPuesto.Enabled = false;
            return;
        }
        ddlPuesto.Items.Clear();
        ddlPuesto.Enabled = false;
    }

    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlServicio.SelectedIndex > 0)
        {
            //clsCatalogos.llenarCatalogoInstalacion(ddlInstalacion, "catalogo.spLeerInstalacion", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuarioAsignacion", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacion.Enabled = true;
           
                txbOficio.Enabled = true;
            
            popAsignacion.Show(); //1
            return;
        }
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
        popAsignacion.Show();

    }

    protected void ddlZona_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void btnCancelarAsignacion_Click(object sender, EventArgs e)
    {


        if (sender != null)
        {
            if (hfRowIndex.Value == "-1")
            {
                if (Session["OficioNuevoAnterior" + Session.SessionID] != null)
                {
                    Session["OficioNuevo" + Session.SessionID] = Session["OficioNuevoAnterior" + Session.SessionID];
                }
                Session["OficioNuevoAnterior" + Session.SessionID] = null;
                hfOficioFlag.Value = "-1";
            }
            else
            {
              //  Session["OficioNuevo" + Session.SessionID] = null;
            }
        }

        divErrorAsignacion.Visible = false;
        hfRowIndex.Value = "-1";
        

        ddlInstalacion.SelectedIndex = -1;
        ddlInstalacion.Enabled = false;
        ddlServicio.SelectedIndex = -1;
        txbFechaAlta.Text = "";
        txbOficio.Text = "";
        txbFechaBaja.Text = "";
        txbFechaHorario.Text = "";
        Session["lisHorario"] = Session["lisHorarioTemporal"];
        
       

        ddlHorario.Items.Clear(); 
        grVHorario.DataSource = null;
        grVHorario.DataBind();

        ddlFuncion.SelectedValue = "";
            grid();
            popAsignacion.Hide();

    }
     
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        bool validafecha=ValidaFecha.ValidaFe(txbFechaBaja.Text);
        //se agrego el campo de oficio 18/12/2012
        if ((ddlServicio.SelectedItem.Text == "TRAMITES MAQ" || ddlServicio.SelectedItem.Text == "REEMPLAZOS") && txbOficio.Text.Length == 0)
        {
            lblErrorAsignacion.Text = "Para estas instalaciones es obligatorio el número de oficio";
            divErrorAsignacion.Visible = true;
            txbOficio.Focus();
            popAsignacion.Show();
            return;
        }
        //
        if (validafecha == true)
        {
            lblErrorAsignacion.Text = string.Empty;
            divErrorAsignacion.Visible = false;
            int error = 0;
            // Validar que fechas no se encimen con otras.
            DateTime dtn1 = clsUtilerias.dtObtenerFecha(txbFechaAlta.Text);
            DateTime dtn2 = clsUtilerias.dtObtenerFecha(txbFechaBaja.Text);


            //
            //
            //
            //
            //*****************************SE VALIDA SI LA ASIGNACIÓN SE ESTA HACIENDO EL LUNES
            //
            //
            //
            //
            //

            clsDatAsignacionLunes.regresaValidacionLunes valida= clsDatAsignacionLunes.regresaValidacionLunes.Normal;

            valida = clsNegAsignacionLunes.validaFechaAsignacion( (clsEntSesion)Session["objSession" + Session.SessionID]);
            if (valida == clsDatAsignacionLunes.regresaValidacionLunes.Normal)
            {
                // 
                //
                //
                //


                // SE VALIDA LA FECHA DE CIERRE, SI TIENE ASISTENCIA SOLO SE PUEDE CON FECHA MAYOR A LA DE HOY, SI NO TIENE ASISTENCIA CON FECHA DE AYER
                clsEntEmpleado objEmpleadoValidaAsistencia = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];
                if (objEmpleadoValidaAsistencia.PaseLista == null || objEmpleadoValidaAsistencia.PaseLista.strTipoAsistencia.Length == 0)
                {
                    // no tiene Asistencia
                    if (txbFechaBaja.Text.Trim() != "" && DateTime.Parse(txbFechaBaja.Text) < DateTime.Today.AddDays(-1))
                    {

                        lblErrorAsignacion.Text = "No es posible continuar, la fecha de cierre tiene que ser mayor o igual al día de ayer";
                        divErrorAsignacion.Visible = true;
                        popAsignacion.Show();
                        return;
                    }
                }
                else
                {
                    // si tiene asistencia


                    if (txbFechaBaja.Text.Trim() != "" && DateTime.Parse(txbFechaBaja.Text) < DateTime.Today)
                    {
                        lblErrorAsignacion.Text = "No es posible continuar la fecha de cierre tiene que ser mayor o igual a la fecha actual, porque el integrante ya registro ASISTENCIA";
                        divErrorAsignacion.Visible = true;
                        popAsignacion.Show();
                        return;
                    }
                }

            }

            else
            {
              // esta parte tambien es para las validaciones del lunes
              //
              //
              //
                lblErrorAsignacion.Text = clsNegAsignacionLunes.validaFechas(DateTime.Parse(txbFechaAlta.Text), txbFechaBaja.Text=="-"?"": txbFechaBaja.Text, (clsEntSesion)Session["objSession" + Session.SessionID], valida);
                if (lblErrorAsignacion.Text.Length > 0)
                {
                    divErrorAsignacion.Visible = true;
                    popAsignacion.Show();
                    return;
                }
                // 
                //
                //
                //
            }
            foreach (GridViewRow row in grvAsignacion.Rows)
            {

                DateTime dta1 = clsUtilerias.dtObtenerFecha(((Label)row.FindControl("lblFechaAlta")).Text);

                if (dtn2.CompareTo(new DateTime()) == 0)
                {
                    dtn2 = DateTime.MaxValue;
                }


                if (txbFechaAlta.Text.Trim() == "")
                {
                    lblErrorAsignacion.Text = "No es posible continuar ya que existe una asignación previa sin fecha de cierre";
                    divErrorAsignacion.Visible = true;
                    popAsignacion.Show();
                    return;
                }

                if (ddlServicio.SelectedIndex == 0)
                {
                    lblErrorAsignacion.Text = "Debe Seleccionar un Servicio.";
                    divErrorAsignacion.Visible = true;
                    popAsignacion.Show();
                    return;
                }

                if (ddlInstalacion.SelectedIndex == 0)
                {
                    lblErrorAsignacion.Text = "Debe Seleccionar una Instalación.";
                    divErrorAsignacion.Visible = true;
                    popAsignacion.Show();
                    return;
                }

                if (ddlFuncion.SelectedIndex == 0)
                {
                    lblErrorAsignacion.Text = "Debe Seleccionar una Función";
                    divErrorAsignacion.Visible = true;
                    popAsignacion.Show();
                    return;
                }
                List<clsEntEmpleadoHorarioREA> lisHorarios = (List<clsEntEmpleadoHorarioREA>)Session["lisHorario"];
                if (ddlServicio.Text != "1" && ddlServicio.Text != "8")
                {
                    if ((lisHorarios == null || lisHorarios.Count == 0) && txbFechaBaja.Text.Length == 0)
                    {
                        lblErrorAsignacion.Text = "Debe ingresar un horario";
                        divErrorAsignacion.Visible = true;
                        popAsignacion.Show();
                        return;
                    }
                }
                if (ddlServicio.Text != "1" && ddlServicio.Text != "8")
                {
                    List<clsEntEmpleadoHorarioREA> lisHorariosValida = lisHorarios.FindAll(delegate(clsEntEmpleadoHorarioREA p) { return p.intAccion != 3; });
                    if ((lisHorariosValida == null || lisHorariosValida.Count <= 0) && txbFechaBaja.Text.Length == 0)
                    {
                        lblErrorAsignacion.Text = "Debe ingresar un horario";
                        divErrorAsignacion.Visible = true;
                        popAsignacion.Show();
                        return;
                    }
                }
                if (lblFechaBajas.Text != "-")
                {
                    DateTime dta3 = clsUtilerias.dtObtenerFecha(lblFechaBajas.Text);
                    if (dtn2.CompareTo(dta3) >= 0)
                    {
                        if (txbFechaBaja.Text == "")
                        {
                            lblErrorAsignacion.Text = "Debe asignar una fecha de cierre de la asignación que sea menor a la fecha de baja";
                            divErrorAsignacion.Visible = true;
                            popAsignacion.Show();
                        }
                        else
                        {
                            
                            lblErrorAsignacion.Text = "La fecha de cierre de la asignacion no podra ser mayor o igual que la fecha de baja";
                            divErrorAsignacion.Visible = true;
                            popAsignacion.Show();
                        }
                        return;
                    }

                }

                if (dtn2.CompareTo(dtn1) < 0)
                {
                    if (txbFechaBaja.Text.Trim() != "-")
                    {
                        lblErrorAsignacion.Text = "La Fecha de baja de la asignación debe ser mayor que la fecha de inicio del servicio";
                        divErrorAsignacion.Visible = true;
                        popAsignacion.Show();
                        return;
                    }

                }
                if ((txbFechaBaja.Text.Trim() != "" && txbFechaBaja.Text.Trim() != "-" )&& (ddlServicio.Text == "1" || ddlServicio.Text == "8"))
                {
                    lblErrorAsignacion.Text = "En las instalaciones LÓGICAS no es posible el cierre, actualice el servicio e instalción";
                    divErrorAsignacion.Visible = true;
                    popAsignacion.Show();
                    return;
                }



            }

    
            clsEntEmpleadoAsignacion objAsignacion = new clsEntEmpleadoAsignacion();
            objAsignacion.Servicio = new clsEntServicio();
            objAsignacion.Instalacion = new clsEntInstalacion();

            objAsignacion.IdEmpleadoAsignacion = Convert.ToInt16(hfIdEmpleadoAsignacion.Value);
            // se agrego la parte del oficio 18/12/2012
            objAsignacion.oficio = txbOficio.Text;
            //
            if (ddlServicio.SelectedIndex > 0)
            {
                objAsignacion.Servicio.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
                objAsignacion.Servicio.serDescripcion = ddlServicio.SelectedItem.Text;
            }
            if (ddlInstalacion.SelectedIndex > 0)
            {
                objAsignacion.Instalacion.IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);
                objAsignacion.Instalacion.InsNombre = ddlInstalacion.SelectedItem.Text;
            }
            if (!string.IsNullOrEmpty(txbFechaAlta.Text.Trim()))
            {
                objAsignacion.EaFechaIngreso = Convert.ToDateTime(txbFechaAlta.Text);
                objAsignacion.FechaIngreso = Convert.ToDateTime(txbFechaAlta.Text).ToShortDateString();
            }
            if (!string.IsNullOrEmpty(txbFechaBaja.Text.Trim()))
            {
                objAsignacion.EaFechaBaja = Convert.ToDateTime(txbFechaBaja.Text);
                objAsignacion.FechaBaja = Convert.ToDateTime(txbFechaBaja.Text).ToShortDateString();

                //---Agregue Fecha  Baja sea hasta el ultimo minuto
                DateTime dt = new DateTime(objAsignacion.EaFechaBaja.Year, objAsignacion.EaFechaBaja.Month, objAsignacion.EaFechaBaja.Day, 23, 59, 59);
                objAsignacion.EaFechaBaja = dt;


                DateTime fechaAcomparar = DateTime.Parse("01/01/1900");
                List<clsEntEmpleadoHorarioREA> lisHorarios = (List<clsEntEmpleadoHorarioREA>)Session["lisHorario"];
                // se valida si la fecha es valida, para esto tiene que se maxima fecha que se tenga registrada
                // se saca la fecha maxima
                if (lisHorarios != null && lisHorarios.Count > 0)
                {
                    foreach (clsEntEmpleadoHorarioREA obj in lisHorarios)
                    {
                        if (fechaAcomparar < obj.ahFechaInicio)
                            fechaAcomparar = obj.ahFechaInicio;
                        if (obj.strFechaFin == null || obj.strFechaFin.Length <= 0 || obj.ahFechaFin > DateTime.Parse(txbFechaBaja.Text))
                        {
                            obj.ahFechaFin = DateTime.Parse(txbFechaBaja.Text);
                            obj.strFechaFin = obj.ahFechaFin.ToShortDateString();
                            if (obj.intAccion == null || obj.intAccion == 0)
                            {
                                obj.intAccion = 2; // modifica
                            }
                            if (obj.ahFechaInicio > DateTime.Parse(txbFechaBaja.Text))
                            {
                                obj.intAccion = 3; // elimina
                            }

                        }
                    }

                   

                }
                if (lisHorarios == null)
                {
                    lisHorarios = new List<clsEntEmpleadoHorarioREA>();
                }
             
                Session["lisHorario"] = lisHorarios;
                txbFechaHorario.Enabled = true;
                imbFechaHorario.Enabled = true;
                ddlHorario.SelectedValue = "";
                txbFechaHorario.Text = "";


            }

            if (ddlFuncion.SelectedIndex > 0)
            {
                objAsignacion.IdFuncionAsignacion = Convert.ToInt32(ddlFuncion.SelectedValue);
                objAsignacion.funcionAsignacion = ddlFuncion.SelectedItem.Text;
            }
            if (txbFechaBaja.Text.Trim() != "" && DateTime.Parse(txbFechaBaja.Text) < DateTime.Today)
            {
                DateTime datFecha = DateTime.Parse("01/01/1900");
                List<clsEntEmpleadoHorarioREA> lisHorariosRevisar = (List<clsEntEmpleadoHorarioREA>)Session["lisHorario"];
                foreach (clsEntEmpleadoHorarioREA horario in lisHorariosRevisar)
                {
                    if (horario.ahFechaInicio > DateTime.Parse(txbFechaBaja.Text))
                    {
                        horario.intAccion = 3;
                    }
                    else
                    {
                        if (horario.ahFechaInicio > datFecha)
                        {
                            datFecha = horario.ahFechaInicio;
                        }
                    }
                }
                foreach (clsEntEmpleadoHorarioREA horario in lisHorariosRevisar)
                {
                    if (horario.ahFechaInicio == datFecha)
                    {
                        horario.ahFechaFin = DateTime.Parse(txbFechaBaja.Text);
                        if (horario.intAccion == 0)
                        {
                            horario.intAccion = 2;
                        }
                    }
                }
            }

            objAsignacion.horarios = (List<clsEntEmpleadoHorarioREA>)Session["lisHorario"];
            switch (hfRowIndex.Value)
            {
                case "-1":  //Inserta nueva asignación
                    if (Session["lstAsignaciones" + Session.SessionID] != null)
                    {
                        objAsignacion.tipoOperacion = 1;
                        ((List<clsEntEmpleadoAsignacion>)Session["lstAsignaciones" + Session.SessionID]).Insert(0, objAsignacion);
                      
                        grid();
                        btnAgregarAsignacion.Enabled = false;
                        hfEliminar.Value = "1";
                    }
                    else
                    {
                        List<clsEntEmpleadoAsignacion> lstAsignaciones = new List<clsEntEmpleadoAsignacion>();
                        lstAsignaciones.Add(objAsignacion);
                        Session["lstAsignaciones" + Session.SessionID] = lstAsignaciones;
                        // btnAgregarAsignacion.Enabled = true;

                    }
                    break;
                default: //Actualiza una asignación
                    int indice = Convert.ToInt32(hfRowIndex.Value);
                    
                        objAsignacion.tipoOperacion = 2;
                    
                    ((List<clsEntEmpleadoAsignacion>)Session["lstAsignaciones" + Session.SessionID])[indice] = objAsignacion;
                    hfEliminar.Value = "0";
                    btnAgregarAsignacion.Enabled = true;

                    break;
            }
            //*************************se genera una copia de la lista
            List<clsEntEmpleadoHorarioREA> lisHorarioTemporal = new List<clsEntEmpleadoHorarioREA>();

            List<clsEntEmpleadoHorarioREA> lisHorariosGuardar =(List<clsEntEmpleadoHorarioREA> ) Session["lisHorario"];
            foreach (clsEntEmpleadoHorarioREA objHorario in lisHorariosGuardar)
            {
                clsEntEmpleadoHorarioREA obj = new clsEntEmpleadoHorarioREA
                {
                    ahFechaFin = objHorario.ahFechaFin,
                    ahFechaInicio = objHorario.ahFechaInicio,
                    ahVigente = objHorario.ahVigente,
                    horNombre = objHorario.horNombre,
                    idAsignacionHorario = objHorario.idAsignacionHorario,
                    idEmpleado = objHorario.idEmpleado,
                    idHorario = objHorario.idHorario,
                    intAccion = objHorario.intAccion,
                    strFechaFin = objHorario.strFechaFin,
                    strFechaInicio = objHorario.strFechaInicio
                };
                lisHorarioTemporal.Add(obj);
            }

            Session["lisHorarioTemporal"] = lisHorarioTemporal;
            btnCancelarAsignacion_Click(null, null);
            grvAsignacion.DataSource = Session["lstAsignaciones" + Session.SessionID];
            grvAsignacion.DataBind();
            divErrorAsignacion.Visible = false;
            grid();



        }
        else
        {
            lblErrorAsignacion.Text = "La Fecha de termino de comisión es incorrecta";
            divErrorAsignacion.Visible = true;
            popAsignacion.Show();
        }

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        clsEntEmpleado objEmpleado = new clsEntEmpleado();

        clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
        objEmpleado.IdUsuario = objSesion.usuario.IdEmpleado;

        objEmpleado.EmpleadoAsignacion = new List<clsEntEmpleadoAsignacion>();
        objEmpleado.EmpleadoAsignacionOS = new clsEntEmpleadoAsignacionOS();
        objEmpleado.EmpleadoAsignacionOS.Zona = new clsEntZona();
        objEmpleado.EmpleadoAsignacionOS.Agrupamiento = new clsEntAgrupamiento();
        objEmpleado.EmpleadoAsignacionOS.Compania = new clsEntCompania();
        objEmpleado.EmpleadoAsignacionOS.Seccion = new clsEntSeccion();
        objEmpleado.EmpleadoAsignacionOS.Peloton = new clsEntPeloton();
        objEmpleado.EmpleadoPuesto = new clsEntEmpleadoPuesto();
        objEmpleado.EmpleadoPuesto.Puesto = new clsEntPuesto();
        objEmpleado.EmpleadoPuesto.Puesto.Jerarquia = new clsEntJerarquia();
        objEmpleado.EmpleadoHorario = new clsEntEmpleadoHorario();
        objEmpleado.EmpleadoHorario.horario = new clsEntHorario();
        objEmpleado.EmpleadoHorario.horario.tipoHorario = new clsEntTipoHorario();

        objEmpleado.IdEmpleado = new Guid(hfIdEmpleado.Value);

        objEmpleado.EmpNombre = lblNombreEmpleado.Text != "-" ? lblNombreEmpleado.Text : "";
        objEmpleado.EmpNumero = lblNumeroEmpleado.Text != "-" ? Convert.ToInt32(lblNumeroEmpleado.Text) : 0;
        objEmpleado.EmpCUIP = lblCUIP.Text != "-" ? lblCUIP.Text : "";
        objEmpleado.EmpFechaIngreso = clsUtilerias.dtObtenerFecha(lblFechaAlta.Text);

        // *********AGREGUE
        DateTime dtFechaInicioHorario = new DateTime();
        //if (txbFechaInicioHorario.Text.Trim() != "") DateTime.TryParse(txbFechaInicioHorario.Text.Trim(), out dtFechaInicioHorario);

        objEmpleado.EmpleadoHorario.EhFechaingreso = dtFechaInicioHorario;

        if (Session["lstAsignaciones" + Session.SessionID] != null)
        {
            objEmpleado.EmpleadoAsignacion = (List<clsEntEmpleadoAsignacion>)Session["lstAsignaciones" + Session.SessionID];
        }


        //Cambio junio2017 Se restringe el cierre de una asignación

        if (!string.IsNullOrEmpty(hfIdEmpleadoPuesto.Value))
        {
            objEmpleado.EmpleadoPuesto.IdEmpleadoPuesto = Convert.ToInt16(hfIdEmpleadoPuesto.Value);
        }

  
        
       
        if (!string.IsNullOrEmpty(ddlJerarquia.SelectedValue))
        {
            objEmpleado.EmpleadoPuesto.Puesto.Jerarquia.IdJerarquia = Convert.ToByte(ddlJerarquia.SelectedValue);
            objEmpleado.EmpleadoPuesto.Puesto.Jerarquia.JerDescripcion = ddlJerarquia.SelectedItem.Text;
        }
        if (!string.IsNullOrEmpty(ddlPuesto.SelectedValue))
        {
            objEmpleado.EmpleadoPuesto.Puesto.IdPuesto = Convert.ToInt16(ddlPuesto.SelectedValue);
            objEmpleado.EmpleadoPuesto.Puesto.PueDescripcion = ddlPuesto.SelectedItem.Text;
        }

        //if (!string.IsNullOrEmpty(ddlTipoHorario.SelectedValue))
        //{
        //    objEmpleado.EmpleadoHorario.horario.IdHorario = Convert.ToInt16(ddlTipoHorario.SelectedValue.Split('&')[0]);
        //    objEmpleado.EmpleadoHorario.horario.tipoHorario.IdTipoHorario = Convert.ToInt16(ddlTipoHorario.SelectedValue.Split('&')[1]);
        //    objEmpleado.EmpleadoHorario.horario.tipoHorario.ThDescripcion = ddlTipoHorario.SelectedItem.Text;
        //}
        Session["objOficioAsignacionAnterior" + Session.SessionID] = null;
        Session["objOficioAsignacion" + Session.SessionID] = null;


        if (Session["OficioNuevo" + Session.SessionID] != null)
        {

            clsEntOficioAsignacion objOficioAsignacion = new clsEntOficioAsignacion();
            objOficioAsignacion.idEmpleado = objEmpleado.IdEmpleado;
            objOficioAsignacion.idEmpleadoAsignacion = objEmpleado.EmpleadoAsignacion[0].IdEmpleadoAsignacion;
            objOficioAsignacion.oficioAsignacion = (byte[])Session["OficioNuevo" + Session.SessionID];
            objOficioAsignacion.idEmpUsuarioResponsable = objSesion.usuario.IdEmpleado;

            Session["objOficioAsignacion" + Session.SessionID] = objOficioAsignacion;


            if (Session["OficioNuevoAnterior" + Session.SessionID] != null)
            {
                //Si se tiene un oficio para actualizar una asignacion anterior
                clsEntOficioAsignacion objOficioAsignacionAnt = new clsEntOficioAsignacion();
                objOficioAsignacionAnt.idEmpleado = objEmpleado.IdEmpleado;
                objOficioAsignacionAnt.idEmpleadoAsignacion = objEmpleado.EmpleadoAsignacion[1].IdEmpleadoAsignacion;
                objOficioAsignacionAnt.oficioAsignacion = (byte[])Session["OficioNuevoAnterior" + Session.SessionID];
                objOficioAsignacionAnt.idEmpUsuarioResponsable = objSesion.usuario.IdEmpleado;

                Session["objOficioAsignacionAnterior" + Session.SessionID] = objOficioAsignacionAnt;
            }

        }

      
        Session["objEmpleado" + Session.SessionID] = objEmpleado;

        Response.Redirect("~/Personal/frmLaboralesConfirmacion.aspx");
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session["objEmpleado" + Session.SessionID] = null;
        Session["lstAsignaciones" + Session.SessionID] = null;
        Session["lisHorario"] = null;
        Session["OficioNuevoAnterior" + Session.SessionID]=null;
        Session["OficioNuevo" + Session.SessionID] = null;
        Response.Redirect("~/frmInicio.aspx");
    }
    public void generaHorarios(List<clsEntEmpleadoHorarioREA> lisHorarios)
    {
        //*************************se genera una copia de la lista
        List<clsEntEmpleadoHorarioREA> lisHorarioTemporal = new List<clsEntEmpleadoHorarioREA>();
        foreach (clsEntEmpleadoHorarioREA objHorario in lisHorarios)
        {
            clsEntEmpleadoHorarioREA obj = new clsEntEmpleadoHorarioREA
            {
                ahFechaFin = objHorario.ahFechaFin,
                ahFechaInicio = objHorario.ahFechaInicio,
                ahVigente = objHorario.ahVigente,
                horNombre = objHorario.horNombre,
                idAsignacionHorario = objHorario.idAsignacionHorario,
                idEmpleado = objHorario.idEmpleado,
                idHorario = objHorario.idHorario,
                intAccion = objHorario.intAccion,
                strFechaFin = objHorario.strFechaFin,
                strFechaInicio = objHorario.strFechaInicio
            };
            lisHorarioTemporal.Add(obj);
        }

        Session["lisHorarioTemporal"] = lisHorarioTemporal;
    }
    protected void grvAsignacion_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        clsEntEmpleadoAsignacion objAsignacion = ((List<clsEntEmpleadoAsignacion>)Session["lstAsignaciones" + Session.SessionID])[e.RowIndex];
        if (Session["lisHorario"] == null)
        {
            Session["lisHorario"] = objAsignacion.horarios;
        }


        generaHorarios(objAsignacion.horarios);

        //********************

        if (hfColumna.Value != "-1")
        {
            popDetalle.Show();
            lblNombreUsuario.Text= objAsignacion.nombreUsuario;
            lblCreacion.Text = objAsignacion.fechaModificacion;

        }
        else
        {


            clsUtilerias.seleccionarDropDownList(ddlServicio, objAsignacion.Servicio.idServicio);
            ddlServicio_SelectedIndexChanged(null, null);
            clsUtilerias.seleccionarDropDownList(ddlInstalacion, objAsignacion.Instalacion.IdInstalacion);
            clsUtilerias.seleccionarDropDownList(ddlFuncion, objAsignacion.IdFuncionAsignacion);
            clsUtilerias.llenarTextBox(txbFechaAlta, objAsignacion.FechaIngreso);
            clsUtilerias.llenarTextBox(txbFechaBaja, objAsignacion.FechaBaja);
            // se agrego el campo de oficio
            clsUtilerias.llenarTextBox(txbOficio, objAsignacion.oficio);
            //
            if (Session["lisHorario"] == null)
            {
                Session["lisHorario"] = objAsignacion.horarios;
            }
            grVHorario.DataSource = objAsignacion.horarios;
            grVHorario.DataBind();
            // se llena la lista de horarios
            clsCatalogos.llenarHoarioServicioInstalacion(ddlHorario, "catalogo.spConsultarHorarioSerInsREA", "horNombre", "idHorario", Convert.ToInt32(ddlServicio.SelectedValue), Convert.ToInt32(ddlInstalacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            txbFechaHorario.Text = "";
            #region Solo Consulta
            clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
            clsEntPermiso objPermiso = new clsEntPermiso();
            objPermiso.IdPerfil = objSesion.usuario.Perfil.IdPerfil;
            //Operacion 32 Solo Consulta-ASIGNACIONES
            if (clsDatPermiso.consultarPermiso(objPermiso.IdPerfil, 32, objSesion) == false)
            {
               ddlHorario.Enabled =  ddlInstalacion.Enabled = false;
               
            }
            #endregion
            #region despliegue
            if (ddlServicio.Text != "1" && ddlServicio.Text != "8" && ddlServicio.SelectedItem.Text != "TRAMITES MAQ" && ddlServicio.SelectedItem.Text != "REEMPLAZOS")
            {
                ddlServicio.Enabled = ddlInstalacion.Enabled = false;
            }
            else
            {
                ddlServicio.Enabled = ddlInstalacion.Enabled = true;
            }

            #endregion
            hfRowIndex.Value = e.RowIndex.ToString();
            hfIdEmpleadoAsignacion.Value = objAsignacion.IdEmpleadoAsignacion.ToString();

            #region carga Documento

                clsEntOficioAsignacion oficioAsignacion = clsNegOficioAsignacion.obtieneOficioAsignacion(new Guid(hfIdEmpleado.Value), objAsignacion.IdEmpleadoAsignacion, objSesion);

                if (Session["OficioNuevo" + Session.SessionID] == null)
                {
                    //Si la asignacion NO tiene oficio cargado
                    if (hfOficioFlag.Value == "1")   //Bandera que se enciende cuando se agrega una nueva asignacion
                    {
                         //si la nueva asignacion no tiene cargado un oficio, no usa el que obtiene de la base de datos que es el de la ultima asignacion
                        Session["byPdfOficioAsig" + Session.SessionID] = null;
                        imbOficio.OnClientClick = "alert('No hay archivo adjunto.');";
                        imgOficioVista.OnClientClick = "alert('No hay archivo adjunto.');";
                    }
                    else{

                        //Si es una actualizacion de asignacion
                        if (oficioAsignacion.oficioAsignacion == null)
                        {
                            //Si la asignacion no tiene oficio en base de datos
                            Session["byPdfOficioAsig" + Session.SessionID] = null;
                            imbOficio.OnClientClick = "alert('No hay archivo adjunto.');";
                            imgOficioVista.OnClientClick = "alert('No hay archivo adjunto.');";
                        }
                        else
                        {
                            //Si la asignacion si tiene oficio en base de datos
                            Session["byPdfOficioAsig" + Session.SessionID] = oficioAsignacion.oficioAsignacion;
                            imbOficio.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                            imgOficioVista.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                        }
                    }
                }
                else
                {
                    //Si la asignacion SI tiene oficio cargado
                    Session["byPdfOficioAsig" + Session.SessionID] = Session["OficioNuevo" + Session.SessionID];
                    imbOficio.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                    imgOficioVista.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                }

            #endregion

            //popAsignacion.Show();
        

            if (lblFechaBajas.Text != "-")
            {

                if (Convert.ToDateTime(lblFechaBajas.Text) <= DateTime.Now.Date)
                {
                  
                    btnAgregarAsignacion.Enabled = false;
                    ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                    ddlFuncion.Enabled = false;
                    txbFechaAlta.Enabled = false;
                    txbFechaBaja.Enabled = false;
                    imbFechaBaja.Enabled = false;
                    imbFechaAlta.Enabled = false;
                    btnAgregar.Enabled = false;
                    //se agrego el campo de oficio 18/12/2012
                    txbOficio.Enabled = false;
                    //
                }

            }
            if (objAsignacion.tipoOperacion == 1)
            {
             
                btnAgregarAsignacion.Enabled = false;
                ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                ddlFuncion.Enabled = false;
                txbFechaAlta.Enabled = false;
                txbFechaBaja.Enabled = false;
                imbFechaBaja.Enabled = false;
                imbFechaAlta.Enabled = false;
                btnAgregar.Enabled = false;
                //se agrego el campo de oficio 18/12/2012
                txbOficio.Enabled = false;
                //
                lblErrorAsignacion.Text = "Guarde primero la nueva ASIGNACIÓN";
                divErrorAsignacion.Visible = true;
                popAsignacion.Show();
            }
            if (hfIdRevision.Value != "-1" )
            {
              
                btnAgregarAsignacion.Enabled = false;
                ddlServicio.Enabled = ddlInstalacion.Enabled = false;
                ddlFuncion.Enabled = false;
                txbFechaAlta.Enabled = false;
                txbFechaBaja.Enabled = false;
                imbFechaBaja.Enabled = false;
                imbFechaAlta.Enabled = false;
                btnAgregar.Enabled = false;
                //se agrego el campo de oficio 18/12/2012
                txbOficio.Enabled = false;
                //
            }
            else
            {

                if (hfIdRenuncia.Value != "-1")
                {
                    lblEmpleadoBaja.Visible = true;
                    divErrorAsi.Visible = true;
                    lblEmpleadoBaja.Text = "El empleado tiene reporte de renuncia";

                }
            }


        }

    }

    protected void grvAsignacion_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        ((List<clsEntEmpleadoAsignacion>)Session["lstAsignaciones" + Session.SessionID]).RemoveAt(e.RowIndex);

        if (Session["OficioNuevoAnterior" + Session.SessionID] != null)
        {
            //Si la asignacion anterior tiene oficio de asignacion aqui se guarda y se elimina el oficio de la nueva asignacion
            Session["OficioNuevo" + Session.SessionID] = Session["OficioNuevoAnterior" + Session.SessionID];
            Session["OficioNuevoAnterior" + Session.SessionID] = null;
        }
        else
        {
            //si la asignacion anterior no tiene oficio, simplemente se elimina el oficio de la nueva asignacion
            Session["OficioNuevo" + Session.SessionID] = null;
        }


        grvAsignacion.DataSource = Session["lstAsignaciones" + Session.SessionID];
        grvAsignacion.DataBind();

        hfEliminar.Value = "0";
        grid();
        btnAgregarAsignacion.Enabled = true;

    }

    protected void ddlInstalacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInstalacion.SelectedValue.Length > 0)
            clsCatalogos.llenarHoarioServicioInstalacion(ddlHorario, "catalogo.spConsultarHorarioSerInsREA", "horNombre", "idHorario", Convert.ToInt32(ddlServicio.SelectedValue), Convert.ToInt32(ddlInstalacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
        List<clsEntEmpleadoHorarioREA> lisHorario = (List<clsEntEmpleadoHorarioREA>)Session["lisHorario"];
        if (lisHorario.Count > 0)
        {
          foreach( clsEntEmpleadoHorarioREA horario in lisHorario )
          {
              horario.intAccion = 3;
          }

        }
        popAsignacion.Show();
      
    }
    protected void btnAgregarAsignacion_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        List<clsEntEmpleadoAsignacion> lstObjEmpleado = ((List<clsEntEmpleadoAsignacion>)Session["lstAsignaciones" + Session.SessionID]);
        int conteo = lstObjEmpleado.Count;
        int idServicio = 0;
        int idInstalacion = 0;
        if (conteo != 0)
        {
            clsEntEmpleadoAsignacion objAsignacion = ((List<clsEntEmpleadoAsignacion>)Session["lstAsignaciones" + Session.SessionID])[0];
            clsEntEmpleadoAsignacion obj = new clsEntEmpleadoAsignacion();
            idServicio = objAsignacion.Servicio.idServicio;
            idInstalacion = objAsignacion.Instalacion.IdInstalacion;


        }

    }

    public class ValidaFecha
    {
        static public bool ValidaFe(string fecha)
        {
            bool dictamen;
            try
            {
                DateTime fechaVal = Convert.ToDateTime(fecha);
                int dia = Convert.ToInt16(fechaVal.Day);
                int mes = Convert.ToInt16(fechaVal.Month);
                int año = Convert.ToInt16(fechaVal.Year);
                int error = 0;

                string bi16 = Convert.ToString(ValidaFecha.CalcularBi(año));
                if (mes == 2 && dia == 29 && bi16 == "False") { error++; }
                if (año >= 1900 && mes <= 12 && dia <= 31) { } else { error++; }
                if (mes == 1) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 2) { if (dia > 29 || dia == 0) { error++; } }
                if (mes == 3) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 4) { if (dia > 30 || dia == 0) { error++; } }
                if (mes == 5) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 6) { if (dia > 30 || dia == 0) { error++; } }
                if (mes == 7) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 8) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 9) { if (dia > 30 || dia == 0) { error++; } }
                if (mes == 10) { if (dia > 31 || dia == 0) { error++; } }
                if (mes == 11) { if (dia > 30 || dia == 0) { error++; } }
                if (mes == 12) { if (dia > 31 || dia == 0) { error++; } }
                if (error == 0) { dictamen = true; } else { dictamen = false; }



            }
            catch
            {
                if (fecha == "") { dictamen = true; }
                else
                {
                    dictamen = false;
                }
            }


            return dictamen;


        }

        static public string CalcularBi(int anno)
        {
            string alfa = Convert.ToString((anno % 4 == 0) && !(anno % 100 == 0 && anno % 400 != 0)); return alfa;
        }

    }
    protected void btnAgregarAsignacion_Click1(object sender, EventArgs e)
    {
        hfOficioFlag.Value = "1"; //Se enciende bandera de nueva asignación
        hfIdEmpleadoAsignacion.Value = "0";
        //se guarda el oficio anterior para tener la posibilidad de insertar una nueva asignacion
        Session["OficioNuevoAnterior" + Session.SessionID] = Session["OficioNuevo" + Session.SessionID];
        Session["OficioNuevo" + Session.SessionID] = null;

        Session["byPdfOficioAsig" + Session.SessionID] = null;
        imbOficio.OnClientClick = "alert('No hay archivo adjunto.');";
        imgOficioVista.OnClientClick = "alert('No hay archivo adjunto.');";
        
        popAsignacion.Show();
    }
    protected void btnCerrarDetalle_Click(object sender, EventArgs e)
    {
        hfColumna.Value = "-1";
        popDetalle.Hide();
  
        lblNombreEmpleado.Text = string.Empty;
        lblCreacion.Text = string.Empty;
    }
    protected void grvAsignacion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnAgregarHorario_Click(object sender, EventArgs e)
    {
        if (ddlServicio.Text == "1" || ddlServicio.Text == "8")
        {
            lblErrorAsignacion.Text = "No se puede agregar horarios, son instalaciones LÓGICAS";
            divErrorAsignacion.Visible = true;

            popAsignacion.Show();
            return;
        }
    
        if (txbFechaAlta.Text.Length == 0)
        {
            lblErrorAsignacion.Text = "No se pueden hacer asignaciones";
            divErrorAsignacion.Visible = true;

            popAsignacion.Show();
            return;
        }
        if (txbFechaHorario.Text.Length<=0)
        {

            lblErrorAsignacion.Text = "La fecha de inicio de horario es obligatoria";
            divErrorAsignacion.Visible = true;
           
            popAsignacion.Show();
            return;

        }
        DateTime datFechaPrueba;
        if (!DateTime.TryParse(txbFechaHorario.Text, out datFechaPrueba))
        {
            lblErrorAsignacion.Text = "Error en el dato de Fecha de Inicio de Horario";
            divErrorAsignacion.Visible = true;

            popAsignacion.Show();
            return;
        }

        /* ACTUALIZACION Febrero 2017 ASIGNACION */

        // SE VALIDA LA FECHA DE CIERRE, SI TIENE ASISTENCIA SOLO SE PUEDE CON FECHA MAYOR A LA DE HOY, SI NO TIENE ASISTENCIA CON FECHA DE AYER
        clsEntEmpleado objEmpleadoValidaAsistencia = (clsEntEmpleado)Session["objEmpleado" + Session.SessionID];
        if (objEmpleadoValidaAsistencia.PaseLista == null || objEmpleadoValidaAsistencia.PaseLista.strTipoAsistencia.Length == 0)
        {
            // no tiene Asistencia
            if (DateTime.Parse(txbFechaHorario.Text) < DateTime.Today && txbFechaHorario.Enabled == true)
            {

                lblErrorAsignacion.Text = "La fecha de nuevo horario tiene que ser mayor o igual a la fecha actual";
                divErrorAsignacion.Visible = true;
                popAsignacion.Show();
                return;
            }
        }
        else
        {
            // si tiene asistencia

            if (DateTime.Parse(txbFechaHorario.Text) < DateTime.Now && txbFechaHorario.Enabled == true)
            {
                lblErrorAsignacion.Text = "No es posible continuar la fecha de nuevo horario tiene que ser mayor a la fecha actual, porque el integrante ya registro ASISTENCIA";
                divErrorAsignacion.Visible = true;
                popAsignacion.Show();
                return;
            }
        }

        // Esta validación es debido al bug 8651 correspondiente a la actualización de Febrero de 2017, registrado en BugTracker.
        // La razón por la que se comenta es porque en producción hay un Job para evitar las incogruencias generadas en base de datos
        // al momento de asignar un horario con una asistencia abierta.

        //clsEntEmpleadosListaGenerica asistenciaEmpleado = clsDatAsistencia.consultaAsistenciaCompleta(objEmpleadoValidaAsistencia.IdEmpleado, (clsEntSesion)Session["objSession" + Session.SessionID]);

        //if (asistenciaEmpleado.fechaAsistenciaSal == Convert.ToDateTime(null))
        //{
        //    lblErrorAsignacion.Text = "No es posible insertar un nuevo horario porque el integrante no registró salida en su asistencia. Contacte con un administrador.";
        //    divErrorAsignacion.Visible = true;
        //    popAsignacion.Show();
        //    return;
        //}

        /* FIN ACTUALIZACION Febrero 2017 ASIGNACION */


        if (DateTime.Parse(txbFechaAlta.Text) != DateTime.Today)
        {
            if (DateTime.Parse(txbFechaHorario.Text) < DateTime.Today && txbFechaHorario.Enabled== true)
            {
                lblErrorAsignacion.Text = "La fecha de nuevo horario tiene que ser mayor o igual a la fecha actual";
                divErrorAsignacion.Visible = true;

                popAsignacion.Show();
                return;
            }
        }
        else
        {
            if (DateTime.Parse(txbFechaHorario.Text) < DateTime.Today && txbFechaHorario.Enabled==true)
            {
                lblErrorAsignacion.Text = "La fecha de nuevo horario tiene que ser mayor o igual a la fecha actual";
                divErrorAsignacion.Visible = true;

                popAsignacion.Show();
                return;
            }
        }
        if ( DateTime.Parse( txbFechaHorario.Text) <DateTime.Parse(txbFechaAlta.Text))
        {

            lblErrorAsignacion.Text = "La fecha de inicio de horario deber ser mayor o igual a la de inicio de asignación";
            divErrorAsignacion.Visible = true;

            popAsignacion.Show();
            return;

        }
        if (ddlHorario.SelectedValue.Length <=0)
        {

            lblErrorAsignacion.Text = "El horario es obligatorio";
            divErrorAsignacion.Visible = true;
            popAsignacion.Show();
            return;

        }
        DateTime fechaAcomparar = DateTime.Parse("01/01/1900");
        List<clsEntEmpleadoHorarioREA>  lisHorarios= ( List<clsEntEmpleadoHorarioREA>) Session["lisHorario"] ; 
        // se valida si la fecha es valida, para esto tiene que se maxima fecha que se tenga registrada
        // se saca la fecha maxima
        if (lisHorarios != null && lisHorarios.Count > 0)
        {
            foreach(clsEntEmpleadoHorarioREA obj in lisHorarios)
            {
                if (fechaAcomparar < obj.ahFechaInicio)
                    fechaAcomparar = obj.ahFechaInicio;
            }
            if (fechaAcomparar >= DateTime.Parse(txbFechaHorario.Text))
                {

                    lblErrorAsignacion.Text = "La fecha de inicio de horario debe ser mayor a la de la última asignación de horario";
                    divErrorAsignacion.Visible = true;
                    popAsignacion.Show();
                    return;

                }
            foreach (clsEntEmpleadoHorarioREA obj in lisHorarios)
            {
                if (obj.strFechaFin == null || obj.strFechaFin.Length <= 0)
                {
                    obj.ahFechaFin = DateTime.Parse(txbFechaHorario.Text).AddDays(-1);
                    obj.strFechaFin = obj.ahFechaFin.ToShortDateString();
                    if (obj.intAccion == null || obj.intAccion == 0)
                    {
                        obj.intAccion = 2; // modifica
                    }
                }
            }

           

        }
        if (lisHorarios == null)
        {
            lisHorarios = new List<clsEntEmpleadoHorarioREA>();
        }
        clsEntEmpleadoHorarioREA objHorario = new clsEntEmpleadoHorarioREA
        {
            ahFechaInicio = DateTime.Parse(txbFechaHorario.Text),
            idAsignacionHorario = lisHorarios.Count+1,
            idHorario = Convert.ToInt32(ddlHorario.SelectedValue),
            idEmpleado = new Guid(hfIdEmpleado.Value),
            horNombre = ddlHorario.SelectedItem.Text,
            strFechaInicio = txbFechaHorario.Text,
            intAccion = 1 
            
        };
        lisHorarios.Add(objHorario);
        grVHorario.DataSource = lisHorarios;
        grVHorario.DataBind();
        popAsignacion.Show();
        Session["lisHorario"] = lisHorarios;
        txbFechaHorario.Enabled = true;
        imbFechaHorario.Enabled = true;
        ddlHorario.SelectedValue = "";
        txbFechaHorario.Text = "";
    }
    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        hfColumna.Value = "-1";
     

        lblServicio.Text = "";
        lblInstalacion.Text = "";
        lblFuncion.Text = "";
        lblFechaInicio.Text ="";
        lblFechaFin.Text = "";
        lblOficio.Text = "";
        grvHorarioConsulta.DataSource = "";
        grvHorarioConsulta.DataBind();

          popConsulta.Hide();
    }
    protected void txbFechaBaja_TextChanged(object sender, EventArgs e)
    {
             int indice = Convert.ToInt32(hfRowIndex.Value);
             if (indice >= 0)
             {
                 clsEntEmpleadoAsignacion objAsignacion = ((List<clsEntEmpleadoAsignacion>)Session["lstAsignaciones" + Session.SessionID])[indice];
                 List<clsEntEmpleadoHorarioREA> lisHorarios = (List<clsEntEmpleadoHorarioREA>)objAsignacion.horarios;
                 if (ddlServicio.Text != "1" && ddlServicio.Text != "8")
                 {



                     if ((lisHorarios != null && lisHorarios.Count != 0) && txbFechaBaja.Text.Length == 0)
                     {
                         DateTime fechaAcomparar = DateTime.Parse("01/01/1900");
                         foreach (clsEntEmpleadoHorarioREA obj in lisHorarios)
                         {
                             if (fechaAcomparar < obj.ahFechaInicio)
                                 fechaAcomparar = obj.ahFechaInicio;
                         }

                         foreach (clsEntEmpleadoHorarioREA obj in lisHorarios)
                         {
                             if (obj.ahFechaInicio == fechaAcomparar)
                             {
                                 obj.ahFechaFin = DateTime.Parse("01/01/1900");
                                 obj.strFechaFin = "";

                                 obj.intAccion = 0; // modifica

                             }
                         }
                         txbFechaHorario.Enabled = imbFechaHorario.Enabled = true;
                         objAsignacion.EaFechaBaja = DateTime.Parse("01/01/1900");
                     }
                     objAsignacion.horarios = lisHorarios;

                     ((List<clsEntEmpleadoAsignacion>)Session["lstAsignaciones" + Session.SessionID])[indice] = objAsignacion;
                     grvAsignacion.DataSource = Session["lstAsignaciones" + Session.SessionID];

                     Session["lisHorario"] = lisHorarios;
                     grvAsignacion.DataBind();
                     popAsignacion.Show();
                     return;
                 }
                 
             }
             else
             {
                 popAsignacion.Show();
             }
    }
    protected void imbFechaBaja_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imbFechaAlta_Click(object sender, ImageClickEventArgs e)
    {
      
    }
    protected void txbFechaAlta_TextChanged(object sender, EventArgs e)
    {
    
    }
    protected void btnCargarOficioAsig_Click(object sender, EventArgs e)
    {
        if (fuOficioAsig.HasFile)
        {
            if (fuOficioAsig.PostedFile.ContentType == "application/pdf")
            {
                int intTamanio = fuOficioAsig.PostedFile.ContentLength;
                if (intTamanio > 0 && intTamanio < 1000000)
                {
                    byte[] byPdf = fuOficioAsig.FileBytes;
                    Session["byPdfOficioAsig" + Session.SessionID] = byPdf;
                    Session["OficioNuevo" + Session.SessionID] = byPdf;
                    imbOficio.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                    imgOficioVista.OnClientClick = "window.open('../Generales/verArchivo.ashx?idSession=byPdfOficioAsig');";
                    mpeCargarOficio.Hide();
                    popAsignacion.Show();
                    //btnCancelarCargarAmpliacion_Click(null, null);
                    //wucFechaPDAmp.Enabled = true;
                }
                else
                {
                    lblErrorOficioAsig.Text = "El documento excede el tamaño máximo permitido: 1 MB.";
                    mpeCargarOficio.Show();
                }
            }
            else
            {
                lblErrorOficioAsig.Text = "Archivo no válido, unicamente documentos *.pdf.";
                mpeCargarOficio.Show();
            }
        }
        else
        {
            lblErrorOficioAsig.Text = "Seleccione un Documento.";
            mpeCargarOficio.Show();
        }
    }
    protected void btnCargarOficio_Click(object sender, EventArgs e)
    {
        if (hfRowIndex.Value == "-1")
        {
            if (Session["OficioNuevo" + Session.SessionID] != null)
            {
                clsEntOficioAsignacion objOficioAsignacion = new clsEntOficioAsignacion();
                objOficioAsignacion.idEmpleado = new Guid(hfIdEmpleado.Value);
                objOficioAsignacion.idEmpleadoAsignacion = Convert.ToInt16(hfIdEmpleadoAsignacion.Value);
                objOficioAsignacion.oficioAsignacion = (byte[])Session["OficioNuevo" + Session.SessionID];
                objOficioAsignacion.idEmpUsuarioResponsable = ((clsEntSesion)Session["objSession" + Session.SessionID]).usuario.IdEmpleado;

                Session["objOficioAsignacionAnterior" + Session.SessionID] = objOficioAsignacion;
            }
        }

        
        
        
        lblErrorOficioAsig.Text = "";
        popAsignacion.Hide();
        mpeCargarOficio.Show();
    }
    protected void imbOficio_Click(object sender, ImageClickEventArgs e)
    {
         
        
        popAsignacion.Show();
    }
    protected void btnCancelarCargarOficioAsig_Click(object sender, EventArgs e)
    {
        lblErrorOficioAsig.Text = "";
        fuOficioAsig.Dispose();
        mpeCargarOficio.Hide();
        popAsignacion.Show();
        
    }
    protected void imbOficioVista_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnGenerarOficio_Click(object sender, EventArgs e)
    {
        divErrorOficio.Visible = false;
        divErrorAsignacion.Visible = false;
        if (ddlServicio.SelectedIndex == 0)
        {
            lblErrorAsignacion.Text = "Debe Seleccionar un Servicio.";
            divErrorAsignacion.Visible = true;
            popAsignacion.Show();
            return;
        }

        if (ddlInstalacion.SelectedIndex == 0)
        {
            lblErrorAsignacion.Text = "Debe Seleccionar una Instalación.";
            divErrorAsignacion.Visible = true;
            popAsignacion.Show();
            return;
        }

        if (!clsNegOficioAsignacion.verificaZona(Convert.ToInt32(ddlServicio.SelectedValue), Convert.ToInt32(ddlInstalacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]))
        {

            lblErrorAsignacion.Text = "No es posible crear Oficios de Asignación para Zonas no operativas.";
            divErrorAsignacion.Visible = true;
            popAsignacion.Show();
            return;
        }
 
        listaFirmantes = clsCatalogos.llenarCatalogoFirmantesPorZona(ddlPersonaFirma, Convert.ToInt32(ddlServicio.SelectedValue), Convert.ToInt32(ddlInstalacion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
       
        if (listaFirmantes.Count <= 0)
        {
            lblErrorAsignacion.Text = "No se tiene registrada ninguna persona que pueda firmar el oficio, contacte a un administrador.";
            divErrorAsignacion.Visible = true;
            popAsignacion.Show();
            return;
        }

        
        
        popAsignacion.Hide();
        mpuGenOficio.Show();

    }
    protected void btnGenOficio_Click(object sender, EventArgs e)
    {
        
        if(ddlPersonaFirma.SelectedIndex<=0)
        {
            divErrorOficio.Visible = true;
            lblErrorFirmante.Text = "Es necesario seleccionar una persona para firmar el Oficio de Asignación.";
            return;
        }
        
        List<clsEntAsignacionMasiva> lisIntegrantes = new List<clsEntAsignacionMasiva>();
        DateTime fechaModificacion = DateTime.Now.ToLocalTime();
  
        clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];

         

            clsEntAsignacionMasiva objAsigMasiva = new clsEntAsignacionMasiva();
            objAsigMasiva.idEmpleado = new Guid(hfIdEmpleado.Value);
            objAsigMasiva.idEmpleadoAsignacion = Convert.ToInt16(hfIdEmpleadoAsignacion.Value);
            objAsigMasiva.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
            objAsigMasiva.idInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);
            objAsigMasiva.fechaIngreso = Convert.ToDateTime(txbFechaAlta.Text);
            objAsigMasiva.fechaModificacion = fechaModificacion;
            objAsigMasiva.eaOficio = txbOficio.Text.Trim();

            if (ddlHorario.SelectedIndex > 0)
            {
                objAsigMasiva.idHorario = Convert.ToInt32(ddlHorario.SelectedValue);
            }
            else
            {
                objAsigMasiva.idHorario = -1;
            }


            objAsigMasiva.idUsuario = objSesion.usuario.IdEmpleado;
            //objAsigMasiva.idFuncionAsignacion = Convert.ToInt32(ddlFuncion.SelectedValue);
            objAsigMasiva.operacion = 1;


            // clsDatEmpleadoPuesto.insertarEmpleadoPuestoMasivo(objAsigMasiva, objLabMasivo, (clsEntSesion)Session["objSession" + Session.SessionID]);
            lisIntegrantes.Add(objAsigMasiva);



        List<string> arrImagenes = new List<string>();
        arrImagenes.Add(Server.MapPath("~/Imagenes/HeaderOficioAsignacion.png"));
        arrImagenes.Add(Server.MapPath("~/Imagenes/BackgroundOficioAsignacion.png"));

        divErrorOficio.Visible = false;

        Session["ftbMensaje"] = clsNegOficioAsignacion.generaPDFPersonalizado(
            lisIntegrantes,

            listaFirmantes.Find(h => h.IdEmpleado == new Guid(ddlPersonaFirma.SelectedValue)),

            (clsEntSesion)Session["objSession" + Session.SessionID], arrImagenes);



        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn",
                            "window.open('./frmOficioAsignacion.aspx'" +
                            ",'verReporte'," +
                            " 'width=1024,height=768,resizable=no,scrollbars=yes,toolbar=no,status=no,menubar=no,copyhistory=no');", true);

    }
    protected void btnCancelarGO_Click(object sender, EventArgs e)
    {
        mpuGenOficio.Hide();
        popAsignacion.Show();
        
    }
}
