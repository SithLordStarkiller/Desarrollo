using System;
using System.Data;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;
using SICOGUA.Datos;
using SICOGUA.Seguridad;

public partial class PaseLista_frmBusquedaListaAsistencia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
           Session["impresion" + Session.SessionID] = null; covFechaPaseLista.ValueToCompare = DateTime.Now.ToShortDateString();
            //clsCatalogos.llenarCatalogoZona(ddlZona, "catalogo.spLeerZona", "zonDescripcion", "idZona", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuarioAbierto", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            //clsCatalogos.llenarCatalogoServicio(ddlServicio, "catalogo.spLeerServicio", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
        }
    }

    //protected void ddlZona_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlZona.SelectedIndex > 0)
    //    {
    //        clsCatalogos.llenarCatalogoAgrupamiento(ddlAgrupamiento, "catalogo.spLeerAgrupamiento", "agrDescripcion", "idAgrupamiento", Convert.ToInt32(ddlZona.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
    //        ddlAgrupamiento.Enabled = true;
    //        ddlAgrupamiento_SelectedIndexChanged(null, null);
    //        return;
    //    }
    //    ddlAgrupamiento.Items.Clear();
    //    ddlAgrupamiento.Enabled = false;
    //    ddlAgrupamiento_SelectedIndexChanged(null, null);
    //}

    //protected void ddlAgrupamiento_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlAgrupamiento.SelectedIndex > 0)
    //    {
    //        clsCatalogos.llenarCatalogoCompania(ddlCompania, "catalogo.spLeerCompania", "comDescripcion", "idCompania", Convert.ToInt32(ddlAgrupamiento.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
    //        ddlCompania.Enabled = true;
    //        ddlCompania_SelectedIndexChanged(null, null);
    //        return;
    //    }
    //    ddlCompania.Items.Clear();
    //    ddlCompania.Enabled = false;
    //    ddlCompania_SelectedIndexChanged(null, null);
    //}

    //protected void ddlCompania_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCompania.SelectedIndex > 0)
    //    {
    //        clsCatalogos.llenarCatalogoSeccion(ddlSeccion, "catalogo.spLeerSeccion", "secDescripcion", "idSeccion", Convert.ToInt32(ddlCompania.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
    //        ddlSeccion.Enabled = true;
    //        ddlSeccion_SelectedIndexChanged(null, null);
    //        return;
    //    }
    //    ddlSeccion.Items.Clear();
    //    ddlSeccion.Enabled = false;
    //    ddlSeccion_SelectedIndexChanged(null, null);
    //}

    //protected void ddlSeccion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSeccion.SelectedIndex > 0)
    //    {
    //        clsCatalogos.llenarCatalogoPeloton(ddlPeloton, "catalogo.spLeerPeloton", "pelDescripcion", "idPeloton", Convert.ToInt32(ddlSeccion.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
    //        ddlPeloton.Enabled = true;
    //        return;
    //    }
    //    ddlPeloton.Items.Clear();
    //    ddlPeloton.Enabled = false;
    //}

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
        //

    }

    protected void btnNuevaBusqueda_Click(object sender, EventArgs e)
    {
        txbFechaPaseLista.Text = "";
        txbHoraPaseLista.Text = "";
        trResultados.Visible = false;

        ddlServicio.SelectedIndex = -1;
        ddlServicio_SelectedIndexChanged(null, null);
        ddlInstalacion.SelectedIndex = -1;

        //ddlZona.SelectedIndex = -1;
        //ddlZona_SelectedIndexChanged(null, null);
        //ddlAgrupamiento.SelectedIndex = -1;
        //ddlAgrupamiento_SelectedIndexChanged(null, null);
        //ddlCompania.SelectedIndex = -1;
        //ddlCompania_SelectedIndexChanged(null, null);
        //ddlSeccion.SelectedIndex = -1;
        //ddlSeccion_SelectedIndexChanged(null, null);
        //ddlPeloton.SelectedIndex = -1;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmInicio.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        clsEntEmpleadoAsignacion objAsignacion = new clsEntEmpleadoAsignacion();
        objAsignacion.Servicio = new clsEntServicio();
        objAsignacion.Instalacion = new clsEntInstalacion();

        clsEntEmpleadoAsignacionOS objAsignacionOs = new clsEntEmpleadoAsignacionOS();
        objAsignacionOs.Zona = new clsEntZona();
        objAsignacionOs.Agrupamiento = new clsEntAgrupamiento();
        objAsignacionOs.Compania = new clsEntCompania();
        objAsignacionOs.Seccion = new clsEntSeccion();
        objAsignacionOs.Peloton = new clsEntPeloton();

        //if (ddlZona.SelectedIndex > 0) objAsignacionOs.Zona.IdZona = Convert.ToInt16(ddlZona.SelectedValue);
        //if (ddlAgrupamiento.SelectedIndex > 0) objAsignacionOs.Agrupamiento.IdAgrupamiento = Convert.ToInt16(ddlAgrupamiento.SelectedValue);
        //if (ddlCompania.SelectedIndex > 0) objAsignacionOs.Compania.IdCompania = Convert.ToInt16(ddlCompania.SelectedValue);
        //if (ddlSeccion.SelectedIndex > 0) objAsignacionOs.Seccion.IdSeccion = Convert.ToInt16(ddlSeccion.SelectedValue);
        //if (ddlPeloton.SelectedIndex > 0) objAsignacionOs.Peloton.IdPeloton = Convert.ToInt16(ddlPeloton.SelectedValue);

        if (ddlServicio.SelectedIndex > 0) objAsignacion.Servicio.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
        if (ddlInstalacion.SelectedIndex > 0) objAsignacion.Instalacion.IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);

        int dia = new DateTime().Day;
        int mes = new DateTime().Month;
        int anio = 1900;

        int hora = new DateTime().Hour;
        int minuto = new DateTime().Minute;
        int segundo = new DateTime().Second;

        if (!string.IsNullOrEmpty(txbFechaPaseLista.Text))
        {
            dia = Convert.ToInt32(txbFechaPaseLista.Text.Split('/')[0]);
            mes = Convert.ToInt32(txbFechaPaseLista.Text.Split('/')[1]);
            anio = Convert.ToInt32(txbFechaPaseLista.Text.Split('/')[2]);
        }

        if (!string.IsNullOrEmpty(txbHoraPaseLista.Text))
        {
            hora = Convert.ToInt32(txbHoraPaseLista.Text.Split(':')[0]);
            minuto = Convert.ToInt32(txbHoraPaseLista.Text.Split(':')[1]);
        }

        DateTime dtFecha = new DateTime(anio, mes, dia, hora, minuto, segundo);
        DateTime dtHora = new DateTime(anio, mes, dia, hora, minuto, segundo);

        DataTable dtBuscar = clsDatPaseLista.buscarListaAsistencia(objAsignacion, objAsignacionOs, dtFecha, dtHora, (clsEntSesion)Session["objSession" + Session.SessionID]);

        double registros = dtBuscar.Rows.Count;
        double porPagina = grvAsistencia.PageSize;
        double paginas = registros / porPagina;
        int total = (int)Math.Ceiling(paginas);

        lblPaginas.Text = total.ToString();

        ViewState["totalPaginas" + Session.SessionID] = total;

        ViewState["dsBuscar" + Session.SessionID] = dtBuscar;
        lblCount.Text = dtBuscar.Rows.Count.ToString();

        ViewState["pagina" + Session.SessionID] = 0;

        lblPagina.Text = ((int)ViewState["pagina" + Session.SessionID] + 1).ToString();

        paginacion((int)ViewState["pagina" + Session.SessionID]);

        trResultados.Visible = true;
    }

    

    public void paginacion(int pagina)
    {
        if (pagina > -1)
        {
            lblPagina.Text = ((int)ViewState["pagina" + Session.SessionID] + 1).ToString();
            if (ViewState["dsBuscar" + Session.SessionID] != null)
            {
                DataTable dt = ViewState["dsBuscar" + Session.SessionID] as DataTable;
                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        DataTable dp = dt.Clone();
                        int pageSize = grvAsistencia.PageSize;
                        int indice = pageSize * pagina;

                        for (int i = indice; i < indice + pageSize; i++)
                        {
                            if (i < dt.Rows.Count)
                            {
                                dp.ImportRow(dt.Rows[i]);
                            }
                        }
                        grvAsistencia.DataSource = dp;
                        grvAsistencia.DataBind();
                    }
                    else
                    {
                        grvAsistencia.DataSource = null;
                        grvAsistencia.DataBind();
                    }
                }
            }
        }
    }

    public int disminuirPagina()
    {
        int pagina = (int)ViewState["pagina" + Session.SessionID];
        if (pagina <= 0)
        {
            ViewState["pagina" + Session.SessionID] = 0;
            return 0;
        }
        ViewState["pagina" + Session.SessionID] = pagina - 1;
        return (pagina - 1);
    }

    public int aumentarPagina()
    {
        int pagina = (int)ViewState["pagina" + Session.SessionID];
        if (pagina >= (int)ViewState["totalPaginas" + Session.SessionID] - 1)
        {
            ViewState["pagina" + Session.SessionID] = (int)ViewState["totalPaginas" + Session.SessionID] - 1;
            return (int)ViewState["totalPaginas" + Session.SessionID] - 1;
        }
        ViewState["pagina" + Session.SessionID] = pagina + 1;
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
}
