using System;
using System.Data;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;
using SICOGUA.Datos;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;
using System.Text;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Collections.Specialized;

public partial class Reemplazos_frmReemplazo : System.Web.UI.Page
{
    static private List<clsEntReemplazo> lisReemplazo = null;
    static private List<clsEntReemplazoTabla> lisReemplazoTabla = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblFecha.Text = DateTime.Today.ToShortDateString();
        if (!IsPostBack)
        {
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicioReemplazar, "catalogo.spLeerServiciosPorUsuario", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuario", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            lisReemplazo = clsNegReemplazos.listaReemplazo((clsEntSesion)Session["objSession" + Session.SessionID]);
            lisReemplazoTabla = clsNegReemplazos.lisReemplazoTabla(lisReemplazo);
            gvReemplazos.DataSource = lisReemplazoTabla;
            gvReemplazos.DataBind();
            Session["lisReemplazo"] = lisReemplazo;
            Session["lisReemplazoTabla"] = lisReemplazoTabla;
            lisReemplazo = lisReemplazo.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazar.EmpPaterno.Contains(txbPaternoReemplazar.Text); });
            lblAReemplazar.Text = lisReemplazo.Count().ToString();
            lblNoReemplazos.Text = "0";
            lblPaginaReemplazos.Text = (gvReemplazos.PageIndex+1).ToString();
            lblPaginasReemplazos.Text = gvReemplazos.PageCount.ToString();
        }
 
     
    }
    protected void gvReemplazos_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        
        int index = Convert.ToInt32(hfRenglon.Value);
        lisReemplazoTabla = (List<clsEntReemplazoTabla>)Session["lisReemplazoTabla"];
        clsEntReemplazoTabla objReemplazo = lisReemplazoTabla[index];
        List<clsEntReemplazo> lisResultadoReemplazo = lisReemplazo.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazar.IdEmpleado == objReemplazo.idEmpleado; });
        if (lisResultadoReemplazo.Count != null && lisResultadoReemplazo.Count > 0)
        {
            lblNombre.Text = lisResultadoReemplazo[0].integranteReemplazar.EmpPaterno + " " + lisResultadoReemplazo[0].integranteReemplazar.EmpMaterno + " " + lisResultadoReemplazo[0].integranteReemplazar.EmpNombre;
            lblNumeroEmpleado.Text = lisResultadoReemplazo[0].integranteReemplazar.EmpNumero.ToString();
            lblCargo.Text = lisResultadoReemplazo[0].integranteReemplazar.EmpleadoPuesto.Puesto.PueDescripcion;
            lblJerarquia.Text = lisResultadoReemplazo[0].integranteReemplazar.EmpleadoPuesto.Puesto.Jerarquia.JerDescripcion;
            lblLoc.Text = lisResultadoReemplazo[0].integranteReemplazar.EmpLOC.ToString() == "1" ? "SI" : "NO";
            lblServicio.Text = lisResultadoReemplazo[0].integranteReemplazar.EmpleadoAsignacion[0].Servicio.serDescripcion;
            lblInstalacion.Text = lisResultadoReemplazo[0].integranteReemplazar.EmpleadoAsignacion[0].Instalacion.InsNombre;
            lblFuncion.Text = lisResultadoReemplazo[0].integranteReemplazar.EmpleadoAsignacion[0].funcionAsignacion;
            lblInicioAsignacion.Text = lisResultadoReemplazo[0].integranteReemplazar.EmpleadoAsignacion[0].EaFechaIngreso.ToShortDateString();
            lblFinAsignacion.Text = lisResultadoReemplazo[0].integranteReemplazar.EmpleadoAsignacion[0].EaFechaBaja.ToShortDateString() == "01/01/1900" ? "" : lisResultadoReemplazo[0].integranteReemplazar.EmpleadoAsignacion[0].EaFechaBaja.ToShortDateString();
            lblHorario.Text = lisResultadoReemplazo[0].integranteReemplazar.horarioREA.horNombre;
            lblInicioHorario.Text = lisResultadoReemplazo[0].integranteReemplazar.horarioREA.ahFechaInicio.ToShortDateString();
            lblFinHorario.Text = lisResultadoReemplazo[0].integranteReemplazar.horarioREA.ahFechaFin.ToShortDateString() == "01/01/1900" ? "" : lisResultadoReemplazo[0].integranteReemplazar.horarioREA.ahFechaFin.ToShortDateString();
            lblIncidencia.Text = lisResultadoReemplazo[0].integranteReemplazar.incidenciaREA[0].tiDescripcion;
            lblInicioIncidencia.Text = lisResultadoReemplazo[0].integranteReemplazar.incidenciaREA[0].incFechaInicio.ToShortDateString();
            lblFinInicidencia.Text = lisResultadoReemplazo[0].integranteReemplazar.incidenciaREA[0].incFechaFin.ToShortDateString() == "01/01/1900" ? "" : lisResultadoReemplazo[0].integranteReemplazar.incidenciaREA[0].incFechaFin.ToShortDateString();
          
        }
        lblError.Visible = false;
        divErrorReemplazo.Visible = false;
        lblError.Text = "";
        popDetalle.Show();
    }
    protected void btnCerrar_Click(object sender, EventArgs e)
    {
       
        popDetalle.Hide();
    }
    protected void btnNuevaBusqueda_Click(object sender, EventArgs e)
    {
        txbNumero.Text = "";
        txbBaja.Text = "";
        txbCuip.Text = "";
        txbCurp.Text = "";
        txbIngreso.Text = "";
        txbMaterno.Text = "";
        txbPaterno.Text = "";
        txbRfc.Text = "";
        txbNacimiento.Text = "";
        txbNombre.Text = "";

        //ddlTipoServicio.SelectedIndex = -1;
        ddlServicio.SelectedIndex = -1;
        ddlServicio_SelectedIndexChanged(null, null);
        ddlInstalacion.SelectedIndex = -1;

   

        txbCartilla.Text = "";
        rblCurso.SelectedIndex = 2;
        rblLOC.SelectedIndex = 2;


        trResultados.Visible = false;
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        trResultados.Visible = true;
        trGrid.Visible = true;

        clsEntEmpleado objBuscar = new clsEntEmpleado();

        objBuscar.EmpleadoAsignacion2 = new clsEntEmpleadoAsignacion();
        objBuscar.EmpleadoAsignacion2.Servicio = new clsEntServicio();
        objBuscar.EmpleadoAsignacion2.Instalacion = new clsEntInstalacion();

        DataSet dsBuscar = new DataSet("dsBuscar");

        DateTime dtNacimiento = new DateTime();
        DateTime dtIngreso = new DateTime();
        DateTime dtBaja = new DateTime();

        if (txbNacimiento.Text.Trim() != "") DateTime.TryParse(txbNacimiento.Text.Trim(), out dtNacimiento);
        if (txbIngreso.Text.Trim() != "") DateTime.TryParse(txbIngreso.Text.Trim(), out dtIngreso);
        if (txbBaja.Text.Trim() != "") DateTime.TryParse(txbBaja.Text.Trim(), out dtBaja);

        objBuscar.EmpPaterno = txbPaterno.Text.Trim();
        objBuscar.EmpMaterno = txbMaterno.Text.Trim();
        objBuscar.EmpNombre = txbNombre.Text.Trim();
        if (!string.IsNullOrEmpty(txbNumero.Text)) objBuscar.EmpNumero = Convert.ToInt32(txbNumero.Text);
        objBuscar.EmpFechaNacimiento = dtNacimiento;
        objBuscar.EmpFechaIngreso = dtIngreso;
        objBuscar.EmpFechaBaja = dtBaja;
        objBuscar.EmpRFC = txbRfc.Text.Trim();
        objBuscar.EmpCURP = txbCurp.Text.Trim();
        objBuscar.EmpCUIP = txbCuip.Text.Trim();
        objBuscar.EmpCartilla = txbCartilla.Text.Trim();
        objBuscar.EmpLOC = Convert.ToInt16(rblLOC.SelectedValue);
        objBuscar.EmpCurso = Convert.ToInt16(rblCurso.SelectedValue);

         if (!string.IsNullOrEmpty(ddlServicio.SelectedValue))
        {
            objBuscar.EmpleadoAsignacion2.Servicio.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
        }


        if (!string.IsNullOrEmpty(ddlInstalacion.SelectedValue))
        {
            objBuscar.EmpleadoAsignacion2.Instalacion.IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);
        }
         string spBuscarEmpleado = null;
        spBuscarEmpleado = "empleado.spConsultarReemplazos";
       

        clsDatEmpleado.buscarEmpleado(objBuscar, spBuscarEmpleado, ref dsBuscar, (clsEntSesion)Session["objSession" + Session.SessionID]);

        double registros = dsBuscar.Tables[0].Rows.Count;
        double porPagina = grvBusqueda.PageSize;
        double paginas = registros / porPagina;
        int total = (int)Math.Ceiling(paginas);

        lblPaginas.Text = total.ToString();

        ViewState["totalPaginas"] = total;

        ViewState["dsBuscar"] = dsBuscar;
        lblCount.Text = dsBuscar.Tables[0].Rows.Count.ToString();

        ViewState["pagina"] = 0;

        lblPagina.Text = ((int)ViewState["pagina"] + 1).ToString();

        paginacion((int)ViewState["pagina"]);

        if (lblCount.Text.Trim() == "0")
        {
            trGrid.Visible = false;
        }
    }
    public void paginacion(int pagina)
    {
        if (pagina > -1)
        {
            lblPagina.Text = ((int)ViewState["pagina"] + 1).ToString();
            if (ViewState["dsBuscar"] != null)
            {
                DataSet ds = ViewState["dsBuscar"] as DataSet;
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        DataTable dp = dt.Clone();
                        int pageSize = grvBusqueda.PageSize;
                        int indice = pageSize * pagina;

                        for (int i = indice; i < indice + pageSize; i++)
                        {
                            if (i < dt.Rows.Count)
                            {
                                dp.ImportRow(dt.Rows[i]);
                            }
                        }
                        grvBusqueda.DataSource = dp;
                        grvBusqueda.DataBind();
                    }
                }
            }
        }
    }
    public int disminuirPagina()
    {
        int pagina = (int)ViewState["pagina"];
        if (pagina <= 0)
        {
            ViewState["pagina"] = 0;
            return 0;
        }
        ViewState["pagina"] = pagina - 1;
        return (pagina - 1);
    }
    public int aumentarPagina()
    {
        int pagina = (int)ViewState["pagina"];
        if (pagina >= (int)ViewState["totalPaginas"] - 1)
        {
            ViewState["pagina"] = (int)ViewState["totalPaginas"] - 1;
            return (int)ViewState["totalPaginas"] - 1;
        }
        ViewState["pagina"] = pagina + 1;
        return (pagina + 1);
    }
    protected void imgAtras_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        paginacion(disminuirPagina());
    }
    protected void imgAdelante_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        paginacion(aumentarPagina());
    }
    protected void grvBusqueda_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        clsEntEmpleado objEmpleado = new clsEntEmpleado();
        Label lblIdEmpleado = (Label)grvBusqueda.Rows[e.RowIndex].FindControl("lblIdEmpleado");
        objEmpleado.IdEmpleado = new Guid(lblIdEmpleado.Text);
       
        clsNegEmpleado.consultarEmpleado(ref objEmpleado, (clsEntSesion)Session["objSession" + Session.SessionID]);
      
        Session["lisReemplazoTabla"] = lisReemplazoTabla;
        lisReemplazo =(List<clsEntReemplazo>) Session["lisReemplazo"];

        List<clsEntReemplazo> lisReemplazaTemporal = lisReemplazo;
        lisReemplazaTemporal = lisReemplazaTemporal.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazo != null; });
        if (lisReemplazaTemporal.Count > 0)
        {
            lisReemplazaTemporal = lisReemplazaTemporal.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazo.IdEmpleado == objEmpleado.IdEmpleado; });
        }
        if (lisReemplazaTemporal.Count == 0)
        {
            int intRenglon = Convert.ToInt32(hfRenglon.Value);
           
            lisReemplazoTabla = (List<clsEntReemplazoTabla>)Session["lisReemplazoTabla"];
            lisReemplazoTabla[intRenglon].empNombreReemplazo = objEmpleado.EmpNombre;
            lisReemplazoTabla[intRenglon].empPaternoReemplazo = objEmpleado.EmpPaterno;
            lisReemplazoTabla[intRenglon].empMaternoReemplazo = objEmpleado.EmpMaterno;
            lisReemplazoTabla[intRenglon].idEmpleadoReemplazo = objEmpleado.IdEmpleado;
            gvReemplazos.DataSource = lisReemplazoTabla;
            gvReemplazos.DataBind();
            lisReemplazo[intRenglon].integranteReemplazo = objEmpleado;
            Session["lisReemplazo"] = lisReemplazo;
            Session["lisReemplazoTabla"] = lisReemplazoTabla;
            List<clsEntReemplazo> lisReemplazoTemporal = null;
            lisReemplazoTemporal = lisReemplazo.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazo != null; });
            lblNoReemplazos.Text = lisReemplazoTemporal.Count().ToString();
        }
        else
        {
            lblError.Visible = true;
            divErrorReemplazo.Visible = true;
            lblError.Text = "El integrante ya fue seleccionado para un reemplazo";
            
        }
        popBusqueda.Hide();
    }
    protected void ddlServicioReemplazar_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServicioReemplazar.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacionReemplazar, "catalogo.spLeerInstalacionesPorUsuario", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicioReemplazar.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacionReemplazar.Enabled = true;
            return;
        }
        ddlInstalacionReemplazar.Items.Clear();
        ddlInstalacionReemplazar.Enabled = false;
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        trParametros.Visible = true;
        trResultados.Visible = false;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {

        popBusqueda.Hide();
    }
    protected void gvReemplazos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
      
        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
        hfRenglon.Value = row.DataItemIndex.ToString();
    
        if (e.CommandName.ToUpper() == "CMDBUSCAR")
        {
            trResultados.Visible = false;
            lblError.Visible = false;
            divErrorReemplazo.Visible = false;
            lblError.Text = "";
            txbNumero.Text = "";
            txbBaja.Text = "";
            txbCuip.Text = "";
            txbCurp.Text = "";
            txbIngreso.Text = "";
            txbMaterno.Text = "";
            txbPaterno.Text = "";
            txbRfc.Text = "";
            txbNacimiento.Text = "";
            txbNombre.Text = "";
            ddlServicio.SelectedIndex = -1;
            ddlServicio_SelectedIndexChanged(null, null);
            ddlInstalacion.SelectedIndex = -1;
            txbCartilla.Text = "";
            rblCurso.SelectedIndex = 2;
            rblLOC.SelectedIndex = 2;
            popBusqueda.Show();
        }
        if (e.CommandName.ToUpper() == "CMDPERIODO")
        {
            
            lisReemplazo = (List<clsEntReemplazo>)Session["lisReemplazo"];
            if (lisReemplazo[row.DataItemIndex].integranteReemplazo != null)
            {
                lblNombrePeriodo.Text = lisReemplazo[row.DataItemIndex].integranteReemplazo.EmpPaterno + " " + lisReemplazo[row.DataItemIndex].integranteReemplazo.EmpMaterno + " " + lisReemplazo[row.DataItemIndex].integranteReemplazo.EmpNombre;
                lblNoEmpleadoPeriodo.Text = lisReemplazo[row.DataItemIndex].integranteReemplazo.EmpNumero.ToString();
                lblCargoPeriodo.Text = lisReemplazo[row.DataItemIndex].integranteReemplazo.EmpleadoPuesto.Puesto.PueDescripcion;
                lblJerarquiaPeriodo.Text = lisReemplazo[row.DataItemIndex].integranteReemplazo.EmpleadoPuesto.Puesto.Jerarquia.JerDescripcion;
                lblLocPeriodo.Text = lisReemplazo[row.DataItemIndex].integranteReemplazo.EmpLOC.ToString() == "1" ? "SI" : "NO";
               
                if (lisReemplazo[row.DataItemIndex].integranteReemplazo.fechaIniCom == DateTime.Parse("01/01/0001"))
                {
                    txbInicioPeriodo.Text = "";
                    txbFinPeriodo.Text = "";
                }
                else
                {
                    txbInicioPeriodo.Text = lisReemplazo[row.DataItemIndex].integranteReemplazo.fechaIniCom.ToShortDateString();
                    txbFinPeriodo.Text = lisReemplazo[row.DataItemIndex].integranteReemplazo.fechaFinAsignacion.ToShortDateString();
                }
                lblError.Visible = false;
                divErrorReemplazo.Visible = false;
                lblError.Text = "";
                popPeriodo.Show();
            }
            else
            {
                lblError.Visible = true;
                divErrorReemplazo.Visible = true;
                lblError.Text = "Es necesario seleccionar el reemplazo primero";
                return;
            }
        }
        
    }
    protected void btnCerrarPeriodo_Click(object sender, EventArgs e)
    {
      popPeriodo.Hide();
       
    }
    protected void btnAgregarPeriodo_Click(object sender, EventArgs e)
    {
        
        int intRenglon = Convert.ToInt32(hfRenglon.Value);
        lisReemplazo = (List<clsEntReemplazo>)Session["lisReemplazo"];
        if(txbInicioPeriodo.Text.Length ==0 || txbFinPeriodo.Text.Length==0)
        {
           lblErrorPeriodo.Visible = true;
            divErrorPeriodo.Visible = true;
            lblErrorPeriodo.Text= "No se pueden dejar ninguna de las dos fechas vacias";
            return;
        }
        if(DateTime.Parse( txbInicioPeriodo.Text)>DateTime.Parse(txbFinPeriodo.Text))
        {
             lblErrorPeriodo.Visible = true;
            divErrorPeriodo.Visible = true;
            lblErrorPeriodo.Text= "La fecha de fin NO puede ser mayor a la de inicio";
            return;
        }
        if (DateTime.Parse(txbInicioPeriodo.Text) < DateTime.Today)
        {
            lblErrorPeriodo.Visible = true;
            divErrorPeriodo.Visible = true;
            lblErrorPeriodo.Text = "La fecha de inicio NO puede ser menor al día actual";
            return;
        }
       
        Session["lisReemplazo"] = lisReemplazo;
  
         if ((DateTime.Parse(lisReemplazo[intRenglon].integranteReemplazar.incidenciaREA[0].incFechaInicio.ToShortDateString())) > DateTime.Parse(txbInicioPeriodo.Text))
         {
             lblErrorPeriodo.Visible = true;
             divErrorPeriodo.Visible = true;
             lblErrorPeriodo.Text = "El período ingresado no se encuentra dentro de la incidencia. Consulte la información.";
             return;
         }
         if (lisReemplazo[intRenglon].integranteReemplazar.fechaFinAsignacion != DateTime.Parse("01/01/0001"))
         {
             if (lisReemplazo[intRenglon].integranteReemplazar.fechaFinAsignacion > lisReemplazo[intRenglon].integranteReemplazar.Incidencias[0].IncFechaFinal)
             {
                 if ((lisReemplazo[intRenglon].integranteReemplazar.incidenciaREA[0].incFechaFin) < DateTime.Parse(txbFinPeriodo.Text))
                 {
                     lblErrorPeriodo.Visible = true;
                     divErrorPeriodo.Visible = true;
                     lblErrorPeriodo.Text = "El período ingresado no se encuentra dentro de la incidencia. Consulte la información.";
                     return;
                 }
             }
             else
             {
                 if ((lisReemplazo[intRenglon].integranteReemplazar.fechaFinAsignacion) < DateTime.Parse(txbFinPeriodo.Text))
                 {
                     lblErrorPeriodo.Visible = true;
                     divErrorPeriodo.Visible = true;
                     lblErrorPeriodo.Text = "El período ingresado no se encuentra dentro del período de la asignación. Consulte la información.";
                     return;
                 }
             }
         }
         else
         {
             if ((lisReemplazo[intRenglon].integranteReemplazar.incidenciaREA[0].incFechaFin) < DateTime.Parse(txbFinPeriodo.Text))
             {
                 lblErrorPeriodo.Visible = true;
                 divErrorPeriodo.Visible = true;
                 lblErrorPeriodo.Text = "El período ingresado no se encuentra dentro de la incidencia. Consulte la información.";
                 return;
             }
         }

         lisReemplazo[intRenglon].integranteReemplazo.fechaIniCom = DateTime.Parse(txbInicioPeriodo.Text);
         lisReemplazo[intRenglon].integranteReemplazo.fechaFinAsignacion = DateTime.Parse(txbFinPeriodo.Text);
         lisReemplazo[intRenglon].integranteReemplazo.EmpleadoAsignacion = lisReemplazo[intRenglon].integranteReemplazar.EmpleadoAsignacion;
    
         lisReemplazo[intRenglon].integranteReemplazo.horarioREA = lisReemplazo[intRenglon].integranteReemplazar.horarioREA;
         popPeriodo.Hide();
    }
    protected void btnCancelarReemplazo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmInicio.aspx");
    }
    protected void btnAgregaodo_Click(object sender, EventArgs e)
    {

    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string strRegresa = "";
        lisReemplazo = (List<clsEntReemplazo>)Session["lisReemplazo"];
        lisReemplazo = lisReemplazo.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazo != null; });
        lisReemplazo = lisReemplazo.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazo.fechaIniCom== DateTime.Parse("01/01/0001"); });
        if(lisReemplazo==null || lisReemplazo.Count == 0)
        {
            strRegresa = clsNegReemplazos.insertaReemplazo( (List<clsEntReemplazo>)Session["lisReemplazo"], (clsEntSesion)Session["objSession" + Session.SessionID]);
            if (strRegresa.Length > 0)
            {
                lblError.Visible = true;
                divErrorReemplazo.Visible = true;
                lblError.Text = strRegresa;
                return;
            }
            else
            {

                string script = "alert('Se almacenó la información!.');";
                StringBuilder sbUrl = new StringBuilder();

                sbUrl.Append("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + Server.UrlEncode("Operación finalizada con éxito.") + "&hyper=frmReemplazo"); ClientScript.RegisterClientScriptBlock(GetType(), "Mensaje", script, true);
                Response.Redirect(sbUrl.ToString());
             




            }
        }
        else
            {
                lblError.Visible = true;
                divErrorReemplazo.Visible = true;
                lblError.Text = "Es necesario asignar periodo a todos los reemplazos";
                return;
            }

    }
    protected void btnNuevaBusquedaReemplazar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reemplazos/frmReemplazo.aspx");
    }
    protected void btnBuscarReemplazar_Click(object sender, EventArgs e)
    {
        lisReemplazo = clsNegReemplazos.listaReemplazo((clsEntSesion)Session["objSession" + Session.SessionID]);
        
        if (txbPaternoReemplazar.Text.Length > 0)
        {
            lisReemplazo = lisReemplazo.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazar.EmpPaterno.Contains(txbPaternoReemplazar.Text); });
        }
        if (txbMaternoReemplazar.Text.Length > 0)
        {
            lisReemplazo = lisReemplazo.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazar.EmpMaterno.Contains(txbMaternoReemplazar.Text); });
        }
        if (txbNombreReemplazar.Text.Length > 0)
        {
            lisReemplazo = lisReemplazo.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazar.EmpNombre.Contains(txbNombreReemplazar.Text); });
        }
        if (txbNumeroReemplazar.Text.Length > 0)
        {
            lisReemplazo = lisReemplazo.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazar.EmpNumero ==Convert.ToInt32( txbNumeroReemplazar.Text); });
        }
        if (ddlServicioReemplazar.Text.Length > 0)
        {
            lisReemplazo = lisReemplazo.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazar.EmpleadoAsignacion[0].Servicio.idServicio ==Convert.ToUInt32( ddlServicioReemplazar.Text); });
        }
        if (ddlInstalacionReemplazar.Text.Length > 0)
        {
            lisReemplazo = lisReemplazo.FindAll(delegate(clsEntReemplazo p) { return p.integranteReemplazar.EmpleadoAsignacion[0].Instalacion.IdInstalacion == Convert.ToInt32( ddlInstalacionReemplazar.Text); });
        }
        gvReemplazos.DataSource = "";
        lisReemplazoTabla = clsNegReemplazos.lisReemplazoTabla(lisReemplazo);
        gvReemplazos.DataSource = lisReemplazoTabla;
        gvReemplazos.DataBind();
        Session["lisReemplazo"] = lisReemplazo;
        Session["lisReemplazoTabla"] = lisReemplazoTabla;
        lblAReemplazar.Text = lisReemplazo.Count().ToString();
        lblPaginaReemplazos.Text = (gvReemplazos.PageIndex + 1).ToString();
        lblPaginasReemplazos.Text = gvReemplazos.PageCount.ToString();
    }
    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServicio.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuario", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacion.Enabled = true;
            return;
        }
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
    }
    protected void gvReemplazos_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        lisReemplazoTabla = (List<clsEntReemplazoTabla>)Session["lisReemplazoTabla"];
        if (lisReemplazoTabla != null)
        {

            gvReemplazos.PageIndex = e.NewPageIndex;
            gvReemplazos.DataSource = lisReemplazoTabla;
            gvReemplazos.DataBind();
            lblPagina.Text = (gvReemplazos.PageIndex + 1).ToString();
            lblPaginas.Text = gvReemplazos.PageCount.ToString();

        }
    }
    protected void imgAtrasReemplazar_Click(object sender, ImageClickEventArgs e)
    {
        if (gvReemplazos.PageIndex > 0)
        {
            gvReemplazos_PageIndexChanging(gvReemplazos, new GridViewPageEventArgs(gvReemplazos.PageIndex - 1));
            lblPaginaReemplazos.Text = (gvReemplazos.PageIndex + 1).ToString();
            lblPaginasReemplazos.Text = gvReemplazos.PageCount.ToString();
        }
    }
    protected void imgAdelanteReemplazar_Click(object sender, ImageClickEventArgs e)
    {
        if (gvReemplazos.PageIndex < gvReemplazos.PageCount)
        {
            gvReemplazos_PageIndexChanging(gvReemplazos, new GridViewPageEventArgs(gvReemplazos.PageIndex + 1));
            lblPaginaReemplazos.Text = (gvReemplazos.PageIndex + 1).ToString();
            lblPaginasReemplazos.Text = gvReemplazos.PageCount.ToString();
        }
    }
  
}