using System;
using SICOGUA.Seguridad;
using SICOGUA.Datos;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SICOGUA.Entidades;
using SICOGUA.Negocio;
using System.Text;

public partial class Servicio_frmServicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["impresion" + Session.SessionID] = null;
            clsCatalogos.llenarCatalogo(ddlTipoServicio, "catalogo.spLeerTipoServicio", "tsDescripcion", "idTipoServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlEstado, "catalogo.spLeerEstado", "estDescripcion", "idEstado", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlRaiz, "empleado.spConsultarCatalogoEmpleadoServicio", "empEmpleado", "idEmpleado", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlClasificacionServ, "catalogo.spLeerCategoriaServicio", "csDescripcion", "idCategoriaServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
        
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
          clsEntServicio objServicio = new clsEntServicio();
          objServicio.operacion = Convert.ToInt32(hfIdServicio.Value);
          if (ddlTipoServicio.SelectedIndex > 0) { objServicio.IdTipoServicio = Convert.ToInt32(ddlTipoServicio.SelectedValue); }
      
          if (!string.IsNullOrEmpty(txbRazonSocial.Text.Trim())) { objServicio.serRazonSocial = txbRazonSocial.Text; } 
          if (!string.IsNullOrEmpty(txbAbreviado.Text.Trim()))  {objServicio.serDescripcion=txbAbreviado.Text;}
          if (!string.IsNullOrEmpty(txbPaginaWeb.Text.Trim()))  {objServicio.serPaginaWeb=txbPaginaWeb.Text;}
          if (!string.IsNullOrEmpty(txbFechaIni.Text.Trim()))  {objServicio.serFechaInicio=Convert.ToDateTime(txbFechaIni.Text);}
          if (!string.IsNullOrEmpty(txbFechFinal.Text.Trim())) { objServicio.serFechaFin = Convert.ToDateTime(txbFechFinal.Text); } 
          if (!string.IsNullOrEmpty(txbObservaciones.Text.Trim())) { objServicio.serObservaciones = txbObservaciones.Text; } 
          if (!string.IsNullOrEmpty(txbRfc.Text.Trim())) { objServicio.serRfc = txbRfc.Text; } 
          if (Session["objByteEmpleado" + Session.SessionID] != null) {objServicio.serLogotipo=(byte[])Session["objByteEmpleado" + Session.SessionID]; }
          if (ddlClasificacionServ.SelectedIndex > 0) { objServicio.idCategoriaServicio = Convert.ToInt32(ddlClasificacionServ.SelectedValue); }


          clsEntDomicilio objDomicilio = new clsEntDomicilio();
          objDomicilio.idDomicilio = Convert.ToInt32(hfIdDomicilio.Value);
          if (!string.IsNullOrEmpty(txbCalle.Text.Trim())) { objDomicilio.domCalle = txbCalle.Text; }
          if (!string.IsNullOrEmpty(txbNoExt.Text.Trim())) { objDomicilio.domNumeroExterior = txbNoExt.Text; }
          if (!string.IsNullOrEmpty(txbNoExt.Text.Trim())) { objDomicilio.domNumeroInterior = txbNoInt.Text; }
          if (ddlAsentamiento.SelectedIndex > 0) { objDomicilio.idAsentamiento =Convert.ToInt32(ddlAsentamiento.SelectedValue); }

          if (!string.IsNullOrEmpty(txbCP.Text.Trim())) { objDomicilio.domCp = txbCP.Text; }
          if (ddlEstado.SelectedIndex > 0) { objDomicilio.idEstado = Convert.ToInt32(ddlEstado.SelectedValue); }
          if (ddlMunicipio.SelectedIndex > 0) { objDomicilio.idMunicipio =Convert.ToInt32(ddlMunicipio.SelectedValue); }

          List<clsEntResponsable> lstResponsable = new List<clsEntResponsable>();
          if (Session["lstResponsable" + Session.SessionID] != null)
          {
              lstResponsable = (List<clsEntResponsable>)Session["lstResponsable" + Session.SessionID];
          }

          if (clsDatServicio.insertarServicioMod(objServicio, objDomicilio, lstResponsable, (clsEntSesion)Session["objSession" + Session.SessionID]))
          {
              limpiarForma();
              string msgFinalizado = string.Empty;
              if (objServicio.operacion == 0) { msgFinalizado = "Registro guardado corectamente"; } else { msgFinalizado = "Registro modificado correctamente"; }
              Response.Redirect("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + msgFinalizado);
              return;
          }
    }
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAsentamiento.Items.Clear();
        ddlMunicipio.Items.Clear();
        ddlAsentamiento.Enabled = false;
        ddlMunicipio.Enabled = true;
        txbCP.Text = string.Empty;
        clsCatalogos.llenarCatalogoMunicipio(ddlMunicipio, Convert.ToInt32(ddlEstado.SelectedValue), "catalogo.spLeerMunicipio", "munDescripcion", "idMunicipio", (clsEntSesion)Session["objSession" + Session.SessionID]);
  
    }
    protected void ddlMunicipio_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAsentamiento.Items.Clear();
        ddlAsentamiento.Enabled = true;
        txbCP.Text = string.Empty;
        clsCatalogos.llenarCatalogoAsentamiento(ddlAsentamiento, Convert.ToInt32(ddlEstado.SelectedValue), Convert.ToInt32(ddlMunicipio.SelectedValue), "catalogo.spLeerAsentamiento", "aseDescripcion", "idAsentamiento", (clsEntSesion)Session["objSession" + Session.SessionID]);
  
    }
    protected void ddlAsentamiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet aseCodigo = new DataSet("aseCodigo");
        txbCP.Text = string.Empty;
        clsNegDomicilio.buscarCodigoPostalNeg(Convert.ToInt32(ddlAsentamiento.SelectedValue), ref aseCodigo, (clsEntSesion)Session["objSession" + Session.SessionID]);
        txbCP.Text = Convert.ToString(aseCodigo.Tables[0].Rows[0]["aseCodigoPostal"]);
  
    }
    protected void btnAgregarFoto_Click(object sender, EventArgs e)
    {
        bulMensajes.Items.Clear();
        if (fiuImagen.HasFile)
        {
            divMensajes.Visible = false;
            string strMime = fiuImagen.PostedFile.ContentType;

            if (strMime == "image/x-png" || strMime == "image/png" || strMime == "image/pjpeg" || strMime == "image/jpeg")
            {
                int intTamanio = fiuImagen.PostedFile.ContentLength;

                if (intTamanio > 0 && intTamanio < 524289)
                {
                    byte[] objByte = fiuImagen.FileBytes;
                    Session["objByteEmpleado" + Session.SessionID] = objByte;
                    imgDoc.ImageUrl = "~/Generales/verFoto.ashx?var=objByteEmpleado";
                    imgDoc.Visible = true;
                }
                else
                {
                    divMensajes.Visible = true;
                    bulMensajes.Items.Add(new ListItem("Se excedio el tamaño máximo permitido: 512 KB."));
                    mpeLogotipo.Show();
                }
            }
            else
            {
                divMensajes.Visible = true;
                bulMensajes.Items.Add(new ListItem("Solo se permiten archivos extension PNG."));
                mpeLogotipo.Show();
            }
        }
        else
        {
            divMensajes.Visible = true;
            bulMensajes.Items.Add(new ListItem("No se selecciono ningún archivo."));
            mpeLogotipo.Show();
        }
    }


    public void limpiarForma()
    {
        txbCalle.Text=string.Empty;
        txbNoExt.Text=string.Empty;
        txbNoInt.Text=string.Empty;
        txbCP.Text=string.Empty;
        ddlEstado.SelectedIndex=0;
        ddlMunicipio.Items.Clear();
        ddlAsentamiento.Items.Clear();
        txbRazonSocial.Text = string.Empty;
        txbAbreviado.Text = string.Empty;
        txbPaginaWeb.Text = string.Empty;
        txbFechaIni.Text = string.Empty;
        txbFechFinal.Text = string.Empty;
        txbObservaciones.Text = string.Empty;
        ddlTipoServicio.SelectedIndex = 0;
        ddlRaiz.SelectedIndex = 0;
        txbObservacion.Text = string.Empty;
        txbRfc.Text = string.Empty;
        ddlClasificacionServ.SelectedIndex = 0;
        Session["objByteEmpleado" + Session.SessionID] = null;
        Session["lstResponsable" + Session.SessionID] = null;
        imgDoc.ImageUrl = "~/Imagenes/nodisponible.jpg";

        grvCatalogo.DataSource = null;
        grvCatalogo.DataBind();
        hfIdDomicilio.Value = "0";
        hfIdServicio.Value = "0";
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        limpiarForma();
        Response.Redirect("~/frmInicio.aspx");

    }

    protected void grvIncidencias_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        ((List<clsEntResponsable>)Session["lstResponsable" + Session.SessionID]).RemoveAt(e.RowIndex);

        grvCatalogo.DataSource = Session["lstResponsable" + Session.SessionID];
        grvCatalogo.DataBind();
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
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        limpiarForma();
        grvCatalogo1.DataSource = null;
        grvCatalogo1.DataBind();
        lblCount.Text = "0";
        lblPagina.Text = "0";
        lblPaginas.Text = "0";
    }
    protected void imgAtras_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        paginacion(disminuirPagina());
    }
    protected void imgAdelante_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        paginacion(aumentarPagina());
    }

    protected void grvCatalogo1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        limpiarForma();
        GridViewRow gvr = grvCatalogo1.Rows[e.RowIndex];
        clsEntServicio objServicio = new clsEntServicio();
        DataSet dsDetalle = new DataSet();
        DataSet dsResponsables = new DataSet();
        objServicio.idServicio = Convert.ToInt32(((Label)gvr.FindControl("lblIdServicio")).Text);
        clsDatServicio.consultaServicio(objServicio, ref dsDetalle, (clsEntSesion)Session["objSession" + Session.SessionID]);



        txbRazonSocial.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["serRazonSocial"]);
        txbAbreviado.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["serDescripcion"]);
        txbPaginaWeb.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["serPaginaWeb"]);
        try { ddlClasificacionServ.SelectedValue = Convert.ToString(dsDetalle.Tables[0].Rows[0]["idCategoriaServicio"]); }
        catch { ddlClasificacionServ.SelectedIndex = 0; }


        txbFechaIni.Text = Convert.ToDateTime(dsDetalle.Tables[0].Rows[0]["serFechaInicio"]).ToShortDateString().ToString(); if (txbFechaIni.Text == "01/01/1900") { txbFechaIni.Text = string.Empty; }
        txbFechFinal.Text = Convert.ToDateTime(dsDetalle.Tables[0].Rows[0]["serFechaFin"]).ToShortDateString().ToString(); if (txbFechFinal.Text == "01/01/1900") { txbFechFinal.Text = string.Empty; }
        txbObservaciones.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["serObservaciones"]);
        try { ddlTipoServicio.SelectedValue = dsDetalle.Tables[0].Rows[0]["idTipoServicio"].ToString(); }
        catch { ddlTipoServicio.SelectedIndex = 0; }


        txbRfc.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["serRfc"]);



        txbCalle.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["domCalle"]);
        txbNoExt.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["domNumeroExterior"]);
        txbNoInt.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["domNumeroInterior"]);
        txbCP.Text = Convert.ToString(dsDetalle.Tables[0].Rows[0]["aseCodigoPostal"]);



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


     try{
            byte[] objByte = (byte[])dsDetalle.Tables[0].Rows[0]["serLogotipo"];
            Session["objByteEmpleado" + Session.SessionID] = objByte;
            imgDoc.ImageUrl = "~/Generales/verFoto.ashx?var=objByteEmpleado";
        
    }
        catch{
            }





        clsDatServicio.buscaResponsableServicio(objServicio, ref dsResponsables, (clsEntSesion)Session["objSession" + Session.SessionID]);

        grvCatalogo.DataSource = dsResponsables;
        grvCatalogo.DataBind();

        List<clsEntResponsable> lstResponsable = new List<clsEntResponsable>();
        foreach (DataRow objRes in dsResponsables.Tables[0].Rows)
        {
            clsEntResponsable objResp = new clsEntResponsable();
            objResp.IdEmpleado = (Guid)objRes["IdEmpleado"];
            objResp.riObservaciones = (string)objRes["riObservaciones"];
            objResp.riNombre = (string)objRes["riNombre"];
            lstResponsable.Add(objResp);
        }



        Session["lstResponsable" + Session.SessionID] = lstResponsable;







        hfIdServicio.Value = Convert.ToString(dsDetalle.Tables[0].Rows[0]["idServicio"]);
        hfIdDomicilio.Value = Convert.ToString(dsDetalle.Tables[0].Rows[0]["idDomicilio"]);
    }

    protected void grvCatalogo1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void grvCatalogo1_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in grvCatalogo1.Rows)
        {
            Label lbl = (Label)gvr.FindControl("lblIndice1");
            lbl.Text = Convert.ToString(gvr.RowIndex + 1);
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
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        clsEntServicio objServicio = new clsEntServicio();
        clsEntDomicilio objDomicilio = new clsEntDomicilio();
        clsEntResponsable objResponsable = new clsEntResponsable();
        DataTable dsResultado = new DataTable();


        if (ddlTipoServicio.SelectedIndex > 0) { objServicio.IdTipoServicio = Convert.ToInt32(ddlTipoServicio.SelectedValue); }
        if (!string.IsNullOrEmpty(txbRazonSocial.Text.Trim())) { objServicio.serRazonSocial = txbRazonSocial.Text; }
        if (!string.IsNullOrEmpty(txbAbreviado.Text.Trim())) { objServicio.serDescripcion = txbAbreviado.Text; }
        if (!string.IsNullOrEmpty(txbPaginaWeb.Text.Trim())) { objServicio.serPaginaWeb = txbPaginaWeb.Text; }
        if (!string.IsNullOrEmpty(txbFechaIni.Text.Trim())) { objServicio.serFechaInicio = Convert.ToDateTime(txbFechaIni.Text); }
        if (!string.IsNullOrEmpty(txbFechFinal.Text.Trim())) { objServicio.serFechaFin = Convert.ToDateTime(txbFechFinal.Text); }
        if (!string.IsNullOrEmpty(txbObservaciones.Text.Trim())) { objServicio.serObservaciones = txbObservaciones.Text; }
        if (!string.IsNullOrEmpty(txbRfc.Text.Trim())) { objServicio.serRfc = txbRfc.Text; }
        objServicio.idCategoriaServicio = ddlClasificacionServ.SelectedValue != string.Empty ? Convert.ToInt32(ddlClasificacionServ.SelectedValue) : 0;

  



        if (!string.IsNullOrEmpty(txbCalle.Text.Trim())) { objDomicilio.domCalle = txbCalle.Text; }
        if (!string.IsNullOrEmpty(txbNoExt.Text.Trim())) { objDomicilio.domNumeroExterior = txbNoExt.Text; }
        if (!string.IsNullOrEmpty(txbNoExt.Text.Trim())) { objDomicilio.domNumeroInterior = txbNoInt.Text; }
        if (ddlAsentamiento.SelectedIndex > 0) { objDomicilio.idAsentamiento = Convert.ToInt32(ddlAsentamiento.SelectedValue); }
        if (!string.IsNullOrEmpty(txbCP.Text.Trim())) { objDomicilio.domCp = txbCP.Text; }
        if (ddlEstado.SelectedIndex > 0) { objDomicilio.idEstado = Convert.ToInt32(ddlEstado.SelectedValue); }
        if (ddlMunicipio.SelectedIndex > 0) { objDomicilio.idMunicipio = Convert.ToInt32(ddlMunicipio.SelectedValue); }

        if (ddlRaiz.SelectedIndex > 0) { objResponsable.IdEmpleado = new Guid(ddlRaiz.SelectedValue); }





        clsDatServicio.consultaServicioGeneral(objServicio, objDomicilio, objResponsable, ref dsResultado, (clsEntSesion)Session["objSession" + Session.SessionID]);
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
}