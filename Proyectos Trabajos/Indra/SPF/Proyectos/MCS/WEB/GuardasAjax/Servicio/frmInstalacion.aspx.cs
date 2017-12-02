using System;
using SICOGUA.Seguridad;
using SICOGUA.Datos;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SICOGUA.Entidades;
using SICOGUA.Negocio;
using System.Text;

public partial class Servicio_frmInstalacion : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            Session["impresion" + Session.SessionID] = null;
            clsCatalogos.llenarCatalogo(ddlEstado, "catalogo.spLeerEstado", "estDescripcion", "idEstado", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlZona, "catalogo.spLeerZona", "zonDescripcion", "idZona", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlServicio, "catalogo.spLeerServicio", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlRaiz, "empleado.spConsultarCatalogoEmpleadoServicio", "empEmpleado", "idEmpleado", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlClasificacion, "catalogo.spLeerClasificacionInstalacion", "ciDescripcion", "ciClave", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlTipoInstalacion, "catalogo.spLeerInstalacionesTipo", "tiDescripcion", "idTipoInstalacion", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlZonaHoraria, "catalogo.spConsultarZonaHoraria", "zhDescripcion", "idZonaHoraria", (clsEntSesion)Session["objSession" + Session.SessionID]);
        }
    }
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAsentamiento.Items.Clear();
        ddlMunicipio.Items.Clear();
        ddlAsentamiento.Enabled = false;
        ddlMunicipio.Enabled = true;
        txbCP.Text = string.Empty;
        clsCatalogos.llenarCatalogoMunicipio(ddlMunicipio,Convert.ToInt32(ddlEstado.SelectedValue), "catalogo.spLeerMunicipio", "munDescripcion", "idMunicipio", (clsEntSesion)Session["objSession" + Session.SessionID]);
    }
    protected void ddlMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAsentamiento.Items.Clear();
        ddlAsentamiento.Enabled = true;
        txbCP.Text = string.Empty;
        clsCatalogos.llenarCatalogoAsentamiento(ddlAsentamiento, Convert.ToInt32(ddlEstado.SelectedValue),Convert.ToInt32(ddlMunicipio.SelectedValue), "catalogo.spLeerAsentamiento", "aseDescripcion", "idAsentamiento", (clsEntSesion)Session["objSession" + Session.SessionID]);
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        clsEntInstalacion objInstalacion=new clsEntInstalacion();

        objInstalacion.operacion = Convert.ToInt32(hfIdInstalacion.Value);

        if (ddlClasificacion.SelectedIndex > 0) { objInstalacion.idClasificacion = Convert.ToInt32(ddlClasificacion.SelectedValue); }
          if (!string.IsNullOrEmpty(txbNombre.Text.Trim()))  {objInstalacion.InsNombre=txbNombre.Text;}
          if (!string.IsNullOrEmpty(txbConvenio.Text.Trim())) { objInstalacion.insConvenio = txbConvenio.Text; }
          if (!string.IsNullOrEmpty(txbFechaIni.Text.Trim())) { objInstalacion.insFechaInicio =Convert.ToDateTime(txbFechaIni.Text.Trim()); }
          if (!string.IsNullOrEmpty(txbFechFinal.Text.Trim())) { objInstalacion.insFechaFin= Convert.ToDateTime(txbFechFinal.Text.Trim()); }
          if (!string.IsNullOrEmpty(txbLongitud.Text.Trim())) { objInstalacion.insLogitud= double.Parse(txbLongitud.Text.Trim()); }
          if (!string.IsNullOrEmpty(txbLatitud.Text.Trim())) { objInstalacion.insLatitud = double.Parse(txbLatitud.Text.Trim()); }
          if (!string.IsNullOrEmpty(txbColindancia.Text.Trim())) { objInstalacion.insColindancias = txbColindancia.Text; }
          if (!string.IsNullOrEmpty(txbDescripcion.Text.Trim())) { objInstalacion.insDescripcion = txbDescripcion.Text; }
          if (!string.IsNullOrEmpty(txbFunciones.Text.Trim())) { objInstalacion.insFunciones = txbFunciones.Text; }
          if (!string.IsNullOrEmpty(txbTurno.Text.Trim())) { objInstalacion.insElementosTurno = Convert.ToInt32(txbTurno.Text.Trim()); }
          if (!string.IsNullOrEmpty(txbArmados.Text.Trim())) { objInstalacion.insElementosArmados = Convert.ToInt32(txbArmados.Text.Trim()); }
          if (!string.IsNullOrEmpty(txbMasculino.Text.Trim())) { objInstalacion.insElementosMasculino = Convert.ToInt32(txbMasculino.Text); }
          if (!string.IsNullOrEmpty(txbFemenino.Text.Trim())) { objInstalacion.insElementosFemenino = Convert.ToInt32(txbFemenino.Text); }
          if (ddlTipoInstalacion.SelectedIndex > 0) { objInstalacion.idTipoInstalacion = Convert.ToInt32(ddlTipoInstalacion.SelectedValue); }

          if (ddlServicio.SelectedIndex > 0) { objInstalacion.IdServicio = Convert.ToInt32(ddlServicio.SelectedValue); }
          if (ddlZona.SelectedIndex > 0) { objInstalacion.idZona = Convert.ToInt32(ddlZona.SelectedValue); }

          clsEntDomicilio objDomicilio = new clsEntDomicilio();
          objDomicilio.idDomicilio = Convert.ToInt32(hfIdDomicilio.Value);
          if (!string.IsNullOrEmpty(txbCalle.Text.Trim())) { objDomicilio.domCalle = txbCalle.Text; }
          if (!string.IsNullOrEmpty(txbNoExt.Text.Trim())) { objDomicilio.domNumeroExterior = txbNoExt.Text; }
          if (!string.IsNullOrEmpty(txbNoExt.Text.Trim())) { objDomicilio.domNumeroInterior= txbNoInt.Text; }
          if (ddlAsentamiento.SelectedIndex > 0) { objDomicilio.idAsentamiento = Convert.ToInt32(ddlAsentamiento.SelectedValue); }

          if (!string.IsNullOrEmpty(txbCP.Text.Trim())) { objDomicilio.domCp = txbCP.Text; }
          if (ddlEstado.SelectedIndex > 0) { objDomicilio.idEstado =Convert.ToInt32(ddlEstado.SelectedValue); }
          if (ddlMunicipio.SelectedIndex > 0) { objDomicilio.idMunicipio =Convert.ToInt32(ddlMunicipio.SelectedValue); }

          List<clsEntResponsable> lstResponsable = new List<clsEntResponsable>();
          if (Session["lstResponsable" + Session.SessionID] != null) 
          {
             lstResponsable = (List<clsEntResponsable>)Session["lstResponsable" + Session.SessionID];
          }
        clsEntZonaHoraria objZonaHoraria = new clsEntZonaHoraria();
        objZonaHoraria.idZonaHoraria = Convert.ToInt32(ddlZonaHoraria.SelectedValue);

        if (clsDatInstalacion.insertarInstalacionMod(objInstalacion,objZonaHoraria, objDomicilio,lstResponsable, (clsEntSesion)Session["objSession" + Session.SessionID]))
          {
              limpiarForma();
              string msgFinalizado = string.Empty;
              if (objInstalacion.operacion == 0) { msgFinalizado = "Registro guardado corectamente"; } else { msgFinalizado = "Registro modificado correctamente"; }
              Response.Redirect("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + msgFinalizado);
              return;
          }




    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        limpiarForma();
        Response.Redirect("~/frmInicio.aspx");
    }

    public void limpiarForma()
    {
        txbNombre.Text=string.Empty;
        ddlServicio.SelectedIndex=0;
        ddlZona.SelectedIndex=0;
        txbConvenio.Text=string.Empty;
        txbFechaIni.Text=string.Empty;
        txbFechFinal.Text=string.Empty;
        txbCalle.Text=string.Empty;
        txbNoExt.Text=string.Empty;
        txbNoInt.Text=string.Empty;
        txbCP.Text=string.Empty;
        ddlEstado.SelectedIndex=0;
        ddlMunicipio.Items.Clear();
        ddlAsentamiento.Items.Clear();
        txbLongitud.Text=string.Empty;
        txbLatitud.Text=string.Empty;
        txbColindancia.Text=string.Empty;
        txbDescripcion.Text=string.Empty;
        txbTurno.Text=string.Empty;
        txbArmados.Text=string.Empty;
        txbMasculino.Text=string.Empty;
        txbFemenino.Text=string.Empty;
        txbFunciones.Text = string.Empty;
        ddlRaiz.SelectedIndex = 0;
        txbObservacion.Text = string.Empty;
        Session["lstResponsable" + Session.SessionID] = null;
        grvCatalogo.DataSource = null;
        grvCatalogo.DataBind();
        hfIdInstalacion.Value = "0";
        hfIdDomicilio.Value = "0";
    }
    protected void ddlAsentamiento_SelectedIndexChanged(object sender, EventArgs e)
    {   
        txbCP.Text = string.Empty;
        DataSet aseCodigo = new DataSet("aseCodigo");
        clsNegDomicilio.buscarCodigoPostalNeg(Convert.ToInt32(ddlAsentamiento.SelectedValue),ref aseCodigo, (clsEntSesion)Session["objSession" + Session.SessionID]);
        txbCP.Text = Convert.ToString(aseCodigo.Tables[0].Rows[0]["aseCodigoPostal"]);

    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        clsEntResponsable objResponsable = new clsEntResponsable();

        if (ddlRaiz.SelectedIndex > 0) 
        { 
            objResponsable.IdEmpleado = new Guid(ddlRaiz.SelectedValue);
            objResponsable.riNombre = ddlRaiz.SelectedItem.Text;
            objResponsable.riObservaciones = txbObservacion.Text;

        
            if (!Equals(Session["lstResponsable" + Session.SessionID], null))
            {
                ((List<clsEntResponsable>)Session["lstResponsable" + Session.SessionID]).Add(objResponsable);
            }
            else
            {
                List<clsEntResponsable> lstResponsable = new List<clsEntResponsable>();
                lstResponsable.Add(objResponsable);
                Session["lstResponsable" + Session.SessionID] = lstResponsable;
            }

            grvCatalogo.DataSource = Session["lstResponsable" + Session.SessionID];
            grvCatalogo.DataBind();

            ddlRaiz.SelectedIndex = 0;
            txbObservacion.Text = string.Empty;

        }
       
    }

    protected void grvIncidencias_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        ((List<clsEntResponsable>)Session["lstResponsable" + Session.SessionID]).RemoveAt(e.RowIndex);

        grvCatalogo.DataSource = Session["lstResponsable" + Session.SessionID];
        grvCatalogo.DataBind();
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

    public void paginacion(int pagina)
    {
        if (pagina > -1)
        {
            lblPagina.Text = ((int)ViewState["pagina"] + 1).ToString();
            if (ViewState["dsBuscar"] != null)
            {
                DataTable dt = ViewState["dsBuscar"] as DataTable;
                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        DataTable dp = dt.Clone();
                        int pageSize = grvCatalogo.PageSize;
                        int indice = pageSize * pagina;

                        for (int i = indice; i < indice + pageSize; i++)
                        {
                            if (i < dt.Rows.Count)
                            {
                                dp.ImportRow(dt.Rows[i]);
                            }
                        }
                        grvCatalogo1.DataSource = dp;
                        grvCatalogo1.DataBind();
                    }
                }
            }
        }
    }

    protected void grvCatalogo1_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in grvCatalogo1.Rows)
        {
            Label lbl = (Label)gvr.FindControl("lblIndice1");
            lbl.Text = Convert.ToString(gvr.RowIndex + 1);
        }
    }

    protected void grvCatalogo1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    
            GridViewRow gvr = grvCatalogo1.Rows[e.RowIndex];
            StringBuilder sbUrl = new StringBuilder();

      
                    clsEntInstalacion objInstalacion = new clsEntInstalacion();
                    clsEntServicio objServicio2 = new clsEntServicio();
                    objServicio2.serDescripcion = ((Label)gvr.FindControl("lblRaiz")).Text;
                    objInstalacion.IdInstalacion = Convert.ToInt32(((Label)gvr.FindControl("lblIdCatalogo")).Text);
                    if (clsDatInstalacion.eliminarInstalacion(objInstalacion, objServicio2, (clsEntSesion)Session["objSession" + Session.SessionID]))
                    {
                   
                       
                    }
           
    }

    protected void grvCatalogo1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        limpiarForma();
        GridViewRow gvr = grvCatalogo1.Rows[e.RowIndex];
        clsEntInstalacion objInstalacion = new clsEntInstalacion();
        DataSet dsDetalle=new DataSet();
        DataSet dsResponsables=new DataSet(); 

        objInstalacion.IdServicio = Convert.ToInt32(((Label)gvr.FindControl("lblIdServicio")).Text);
        objInstalacion.IdInstalacion = Convert.ToInt32(((Label)gvr.FindControl("lblIdInstalacion")).Text);
        objInstalacion.idZona = Convert.ToInt32(((Label)gvr.FindControl("lblIdZona")).Text);

        clsDatInstalacion.consultaInstalacion(objInstalacion, ref dsDetalle, (clsEntSesion)Session["objSession" + Session.SessionID]);
        clsDatInstalacion.buscaResponsableInstalacion(objInstalacion,ref dsResponsables,(clsEntSesion)Session["objSession" + Session.SessionID]);

        grvCatalogo.DataSource = dsResponsables;
        grvCatalogo.DataBind();

        List<clsEntResponsable> lstResponsable = new List<clsEntResponsable>();
        foreach(DataRow objRes in dsResponsables.Tables[0].Rows)
        {
            clsEntResponsable objResp=new clsEntResponsable();
            objResp.IdEmpleado = (Guid) objRes["IdEmpleado"];
            objResp.riObservaciones = (string) objRes["riObservaciones"];
            objResp.riNombre = (string)objRes["riNombre"];
            lstResponsable.Add(objResp);
        }


        
        Session["lstResponsable" + Session.SessionID] = lstResponsable;



        txbNombre.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["insNombre"]);
        txbConvenio.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["insConvenio"]);
        txbFechaIni.Text = Convert.ToDateTime(dsDetalle.Tables[0].Rows[0]["insFechaInicio"]).ToShortDateString().ToString(); if(txbFechaIni.Text =="01/01/1900"){txbFechaIni.Text=string.Empty;}
        txbFechFinal.Text = Convert.ToDateTime(dsDetalle.Tables[0].Rows[0]["insFechaFin"]).ToShortDateString().ToString(); if (txbFechFinal.Text == "01/01/1900") { txbFechFinal.Text = string.Empty; }
        txbLongitud.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["insLongitud"]); if (txbLongitud.Text == "0") { txbLongitud.Text = string.Empty; }
        txbLatitud.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["insLatitud"]); if (txbLatitud.Text == "0") { txbLatitud.Text = string.Empty; }
        txbColindancia.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["insColindancias"]);
        txbDescripcion.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["insDescripcion"]);
        txbFunciones.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["insFunciones"]);
        txbTurno.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["insElementosTurno"]); if (txbTurno.Text == "0") { txbTurno.Text = string.Empty; }
        txbArmados.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["insElementosArmados"]); if (txbArmados.Text == "0") { txbArmados.Text = string.Empty; }
        txbMasculino.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["insElementosMasculino"]); if (txbMasculino.Text == "0") { txbMasculino.Text = string.Empty; }
        txbFemenino.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["insElementosFemenino"]); if (txbFemenino.Text == "0") { txbFemenino.Text = string.Empty; }
        txbCalle.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["domCalle"]);
        txbNoExt.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["domNumeroExterior"]);
        txbNoInt.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["domNumeroInterior"]);
        txbCP.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["aseCodigoPostal"]);
        try { ddlTipoInstalacion.SelectedValue = Convert.ToString(dsDetalle.Tables[0].Rows[0]["idTipoInstalacion"]); }
        catch { ddlTipoInstalacion.SelectedIndex = 0; }


        try { ddlZonaHoraria.SelectedValue = Convert.ToString(dsDetalle.Tables[0].Rows[0]["idZonaHoraria"]); }
        catch { ddlZonaHoraria.SelectedIndex = 0; }


        try { ddlClasificacion.SelectedValue = dsDetalle.Tables[0].Rows[0]["ciClave"].ToString(); }
        catch { ddlClasificacion.SelectedIndex = 0; }
        try { ddlServicio.SelectedValue = dsDetalle.Tables[0].Rows[0]["idServicio"].ToString(); } catch { ddlServicio.SelectedIndex = 0; }
        try { ddlZona.SelectedValue = dsDetalle.Tables[0].Rows[0]["idZona"].ToString(); } catch { ddlZona.SelectedIndex = 0; }

        if (dsDetalle.Tables[0].Rows[0]["idEstado"].ToString() == "0") { ddlEstado.SelectedIndex = 0; } else { ddlEstado.SelectedValue = dsDetalle.Tables[0].Rows[0]["idEstado"].ToString(); }
        if (dsDetalle.Tables[0].Rows[0]["idMunicipio"].ToString() == "0") 
        { 
            ddlMunicipio.Items.Clear();
            ddlMunicipio.Enabled = false;
        }
        else 
        {
            ddlMunicipio.Items.Clear();
            clsCatalogos.llenarCatalogoMunicipio(ddlMunicipio, Convert.ToInt32(ddlEstado.SelectedValue), "catalogo.spLeerMunicipio", "munDescripcion", "idMunicipio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlMunicipio.SelectedValue = dsDetalle.Tables[0].Rows[0]["idMunicipio"].ToString();
            ddlMunicipio.Enabled = true;
        }

        if (dsDetalle.Tables[0].Rows[0]["idAsentamiento"].ToString() == "0") 
        { 
            ddlAsentamiento.Items.Clear();
            ddlAsentamiento.Enabled = false;
        } 
        else 
        {
            ddlAsentamiento.Items.Clear();
            clsCatalogos.llenarCatalogoAsentamiento(ddlAsentamiento, Convert.ToInt32(ddlEstado.SelectedValue), Convert.ToInt32(ddlMunicipio.SelectedValue), "catalogo.spLeerAsentamiento", "aseDescripcion", "idAsentamiento", (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlAsentamiento.SelectedValue = dsDetalle.Tables[0].Rows[0]["idAsentamiento"].ToString();
            ddlAsentamiento.Enabled = true;
        }

        hfIdInstalacion.Value = Convert.ToString(dsDetalle.Tables[0].Rows[0]["idInstalacion"]);
        hfIdDomicilio.Value = Convert.ToString(dsDetalle.Tables[0].Rows[0]["idDomicilio"]);
    }

    protected void imgAtras_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        paginacion(disminuirPagina());
    }
    protected void imgAdelante_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        paginacion(aumentarPagina());
    }




    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        clsEntInstalacion objInstalacion=new clsEntInstalacion();
        clsEntDomicilio objDomicilio=new clsEntDomicilio();
        clsEntResponsable objResponsable=new clsEntResponsable();
        clsEntZonaHoraria objZonaHoraria = new clsEntZonaHoraria();
        DataTable dsResultado=new DataTable();







        if (!string.IsNullOrEmpty(txbNombre.Text.Trim())) { objInstalacion.InsNombre = txbNombre.Text; }
        if (!string.IsNullOrEmpty(txbConvenio.Text.Trim())) { objInstalacion.insConvenio = txbConvenio.Text; }
        if (!string.IsNullOrEmpty(txbFechaIni.Text.Trim())) { objInstalacion.insFechaInicio = Convert.ToDateTime(txbFechaIni.Text.Trim()); }
        if (!string.IsNullOrEmpty(txbFechFinal.Text.Trim())) { objInstalacion.insFechaFin = Convert.ToDateTime(txbFechFinal.Text.Trim()); }
        if (!string.IsNullOrEmpty(txbLongitud.Text.Trim())) { objInstalacion.insLogitud = double.Parse(txbLongitud.Text.Trim()); }
        if (!string.IsNullOrEmpty(txbLatitud.Text.Trim())) { objInstalacion.insLatitud = double.Parse(txbLatitud.Text.Trim()); }
        if (!string.IsNullOrEmpty(txbColindancia.Text.Trim())) { objInstalacion.insColindancias = txbColindancia.Text; }
        if (!string.IsNullOrEmpty(txbDescripcion.Text.Trim())) { objInstalacion.insDescripcion = txbDescripcion.Text; }
        if (!string.IsNullOrEmpty(txbFunciones.Text.Trim())) { objInstalacion.insFunciones = txbFunciones.Text; }
        if (!string.IsNullOrEmpty(txbTurno.Text.Trim())) { objInstalacion.insElementosTurno = Convert.ToInt32(txbTurno.Text.Trim()); }
        if (!string.IsNullOrEmpty(txbArmados.Text.Trim())) { objInstalacion.insElementosArmados = Convert.ToInt32(txbArmados.Text.Trim()); }
        if (!string.IsNullOrEmpty(txbMasculino.Text.Trim())) { objInstalacion.insElementosMasculino = Convert.ToInt32(txbMasculino.Text); }
        if (!string.IsNullOrEmpty(txbFemenino.Text.Trim())) { objInstalacion.insElementosFemenino = Convert.ToInt32(txbFemenino.Text); }
        if (ddlServicio.SelectedIndex > 0) { objInstalacion.IdServicio = Convert.ToInt32(ddlServicio.SelectedValue); }
        if (ddlServicio.SelectedIndex > 0) { objInstalacion.IdServicio = Convert.ToInt32(ddlServicio.SelectedValue); }
        if (ddlZona.SelectedIndex > 0) { objInstalacion.idZona = Convert.ToInt32(ddlZona.SelectedValue); }
        if (ddlTipoInstalacion.SelectedIndex > 0) { objInstalacion.idTipoInstalacion = Convert.ToInt32(ddlTipoInstalacion.SelectedValue); }


        if (!string.IsNullOrEmpty(txbCalle.Text.Trim())) { objDomicilio.domCalle = txbCalle.Text; }
        if (!string.IsNullOrEmpty(txbNoExt.Text.Trim())) { objDomicilio.domNumeroExterior = txbNoExt.Text; }
        if (!string.IsNullOrEmpty(txbNoExt.Text.Trim())) { objDomicilio.domNumeroInterior = txbNoInt.Text; }
        if (ddlAsentamiento.SelectedIndex > 0) { objDomicilio.idAsentamiento = Convert.ToInt32(ddlAsentamiento.SelectedValue); }
        if (!string.IsNullOrEmpty(txbCP.Text.Trim())) { objDomicilio.domCp = txbCP.Text; }
        if (ddlEstado.SelectedIndex > 0) { objDomicilio.idEstado = Convert.ToInt32(ddlEstado.SelectedValue); }
        if (ddlMunicipio.SelectedIndex > 0) { objDomicilio.idMunicipio = Convert.ToInt32(ddlMunicipio.SelectedValue); }


        if (ddlRaiz.SelectedIndex > 0) { objResponsable.IdEmpleado =new Guid(ddlRaiz.SelectedValue); }

        if (ddlZonaHoraria.SelectedIndex > 0) { objZonaHoraria.idZonaHoraria = Convert.ToInt32(ddlZonaHoraria.SelectedValue); }



        clsDatInstalacion.consultaInstalacionGeneral(objInstalacion,objDomicilio,objResponsable,ref dsResultado,(clsEntSesion)Session["objSession" + Session.SessionID]);
        grvCatalogo1.DataSource = dsResultado;
        grvCatalogo1.DataBind();

        trResultados.Visible = true;
        trGrid.Visible = true;

        double registros = dsResultado.Rows.Count;
        double porPagina = grvCatalogo.PageSize;
        double paginas = registros / porPagina;
        int total = (int)Math.Ceiling(paginas);

        lblPaginas.Text = total.ToString();

        ViewState["totalPaginas"] = total;

        ViewState["dsBuscar"] = dsResultado;
        lblCount.Text = dsResultado.Rows.Count.ToString();

        ViewState["pagina"] = 0;

        lblPagina.Text = ((int)ViewState["pagina"] + 1).ToString();

        paginacion((int)ViewState["pagina"]);

        if (lblCount.Text.Trim() == "0")
        {
            trGrid.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        limpiarForma();
        grvCatalogo1.DataSource = null;
        grvCatalogo1.DataBind();
        lblCount.Text="0";
        lblPagina.Text = "0";
        lblPaginas.Text="0";
        ddlTipoInstalacion.SelectedIndex = 0;
    }

    //protected void ddlZonaHoraria_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlZonaHoraria.Items.Clear();
    //    clsCatalogos.llenarCatalogo(ddlZonaHoraria, "catalogo.spConsultarZonaHoraria", "zhDescripcion", "idZonaHoraria", (clsEntSesion)Session["objSession" + Session.SessionID]);
    //}
}