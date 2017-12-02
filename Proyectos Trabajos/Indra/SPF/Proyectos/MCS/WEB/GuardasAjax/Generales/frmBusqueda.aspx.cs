using System;
using System.Data;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;
using SICOGUA.Datos;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;

using System.Collections.Generic;
using proUtilerias;




namespace SICOGUA.Generales
{
    public partial class Escoltas_frmPopUps_frmBusqueda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["impresion" + Session.SessionID] = null;
            if (!IsPostBack)
            {
                //clsCatalogos.llenarCatalogoTipoServicio(ddlTipoServicio, "catalogo.spLeerTipoServicio", "tsDescripcion", "idTipoServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
                clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuario", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);

                trResultados.Visible = false;
                if (Request.QueryString["Redirect"] != null)
                {
                    hfRedirect.Value = Request.QueryString["Redirect"];
                }
                if (Request.QueryString["Cancel"] != null)
                {
                    hfCancel.Value = Request.QueryString["Cancel"];
                }
            }
        }

        protected void limpiaCampos()
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

            rbtActivos.Checked = true;
            rbtInactivos.Checked = false;
            rbtTodos.Checked = false;

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

            //if (!string.IsNullOrEmpty(ddlTipoServicio.SelectedValue))
            //{
            //    objBuscar.EmpleadoAsignacion2.Servicio.IdTipoServicio = Convert.ToInt32(ddlTipoServicio.SelectedValue);
            //}

            if (!string.IsNullOrEmpty(ddlServicio.SelectedValue))
            {
                objBuscar.EmpleadoAsignacion2.Servicio.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
            }
          

            if (!string.IsNullOrEmpty(ddlInstalacion.SelectedValue))
            {
                objBuscar.EmpleadoAsignacion2.Instalacion.IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);
            }

            string spBuscarEmpleado = null;
            if (rbtActivos.Checked)
                spBuscarEmpleado = "empleado.spBuscarEmpleadoActivoPermisoConsulta";
            else if (rbtInactivos.Checked)
                spBuscarEmpleado = "empleado.spBuscarEmpleadoInactivoPermisoConsulta";
            else if (rbtTodos.Checked)
                spBuscarEmpleado = "empleado.spBuscarEmpleadoPermisoConsulta";

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

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            trParametros.Visible = true;
            trResultados.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            Session["objEmpleado" + Session.SessionID] = null;
            Session["lisHorarios"] = null;
            Response.Redirect("~/frmInicio.aspx");
        }

        protected void btnNuevaBusqueda_Click(object sender, EventArgs e)
        {
            limpiaCampos();
        }

        protected void grvBusqueda_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            clsEntEmpleado objEmpleado = new clsEntEmpleado();
            Label lblIdEmpleado = (Label)grvBusqueda.Rows[e.RowIndex].FindControl("lblIdEmpleado");
            objEmpleado.IdEmpleado = new Guid(lblIdEmpleado.Text);
            clsNegEmpleado.consultarEmpleado(ref objEmpleado, (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsNegFotografia.consultarPersonaFoto(objEmpleado.IdEmpleado, ref objEmpleado, (clsEntSesion)Session["objSession" + Session.SessionID]);
            Session["objEmpleado" + Session.SessionID] = objEmpleado;
            Session["empleadoFoto" + Session.SessionID] = objEmpleado.empFoto;
            Response.Redirect(hfRedirect.Value);
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


        //protected void ddlTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlTipoServicio.SelectedIndex > 0)
        //    {
        //        clsCatalogos.llenarCatalogoServicioPorTipoServicio(ddlServicio, "catalogo.spLeerServicioPorTipoServicio", "serDescripcion", "idServicio", Convert.ToInt32(ddlTipoServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
        //        ddlServicio.Enabled = true;
        //        ddlInstalacion.Items.Clear();
        //        ddlInstalacion.Enabled = false;
        //        return;
        //    }
        //    ddlServicio.Items.Clear();
        //    ddlServicio.Enabled = false;
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
        }



        protected void grvBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
}
}