using System;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using System.Globalization;

public partial class Incidentes_frmIncidentes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["impresion" + Session.SessionID] = null;
        if (!IsPostBack)
        {
            #region Catálogos

            covFecha.ValueToCompare = DateTime.Now.ToShortDateString();
            txbHora.Text = DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo);
            txbFecha.Text = DateTime.Now.ToShortDateString();
            //clsCatalogos.llenarCatalogoServicio(ddlServicio, "catalogo.spLeerServicio", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            //clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuario", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuarioAsignacionLimitado", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlGrado, "catalogo.spLeerJerarquia", "jerDescripcion", "idJerarquia", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlAutGra, "catalogo.spLeerJerarquia", "jerDescripcion", "idJerarquia", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlParIniGra, "catalogo.spLeerJerarquia", "jerDescripcion", "idJerarquia", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlSupAutGra, "catalogo.spLeerJerarquia", "jerDescripcion", "idJerarquia", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoZona(ddlZona, "catalogo.spLeerZona", "zonDescripcion", "idZona", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoZona(ddlSupAutZon, "catalogo.spLeerZona", "zonDescripcion", "idZona", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoZona(ddlParIniZon, "catalogo.spLeerZona", "zonDescripcion", "idZona", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoZona(ddlAutZon, "catalogo.spLeerZona", "zonDescripcion", "idZona", (clsEntSesion)Session["objSession" + Session.SessionID]);

            #endregion

            #region Solo Consulta
            clsEntSesion objSesion2 = (clsEntSesion)Session["objSession" + Session.SessionID];
            clsEntPermiso objPermiso2 = new clsEntPermiso();
            objPermiso2.IdPerfil = objSesion2.usuario.Perfil.IdPerfil;
            //Operacion 35 Solo Consulta INCIDENTES (HECHOS)
            if (clsDatPermiso.consultarPermiso(objPermiso2.IdPerfil, 35, objSesion2) == false)
            {
                btnGuardar.Visible = false;
            }
            #endregion


            #region Cargar Información

            if (Session["objIncidente" + Session.SessionID] != null)
            {
                clsEntReporteIncidente objIncidente = (clsEntReporteIncidente)Session["objIncidente" + Session.SessionID];

                #region Incidente

                hfIdIncidente.Value = objIncidente.IdIncidente.ToString();

                clsUtilerias.seleccionarDropDownList(ddlServicio, objIncidente.Servicio.idServicio);
                ddlServicio_SelectedIndexChanged(null, null);
                clsUtilerias.seleccionarDropDownList(ddlInstalacion, objIncidente.Instalacion.IdInstalacion);
                clsUtilerias.llenarTextBox(txbFecha, objIncidente.RiFechaHora.ToShortDateString());
                clsUtilerias.llenarTextBox(txbHora, objIncidente.RiFechaHora.ToLongTimeString().Substring(0, 8));
                clsUtilerias.llenarTextBox(txbLugar, objIncidente.RiLugar);

                txbFecha.ReadOnly = true;
                txbHora.ReadOnly = true;

                #endregion

                #region Personal Involucrado en los Hechos

                clsUtilerias.seleccionarDropDownList(ddlGrado, objIncidente.JerarquiaEmpleadoInvolucrado.IdJerarquia);
                ddlGrado_SelectedIndexChanged(null, null);
                clsUtilerias.seleccionarDropDownList(ddlZona, objIncidente.ZonaEmpleadoInvolucrado.IdZona);
                ddlZona_SelectedIndexChanged(null, null);
                clsUtilerias.seleccionarDropDownList(ddlNomCompleto, objIncidente.IdEmpleadoInvolucrado.ToString().ToUpper() + "&" + objIncidente.NoEmpleadoInvolucrado + "&" + objIncidente.IdEmpleadoPuestoInvolucrado);
                ddlNomCompleto_SelectedIndexChanged(null, null);

                clsUtilerias.llenarTextBox(txbActividad, objIncidente.RiActividad);
                clsUtilerias.llenarTextBox(txbUniformeCivil, objIncidente.RiUniforme);
                clsUtilerias.llenarTextBox(txbHecCon, objIncidente.RiDesarrolloConsecuencia);
                clsUtilerias.llenarTextBox(txbCuaLes, objIncidente.RiLesion);
                clsUtilerias.llenarTextBox(txbCadLes, objIncidente.RiUbicacionCadaverLesionado);
                clsUtilerias.llenarTextBox(txbAccConAgr, objIncidente.RiAccionVsAgresor);

                #endregion

                #region Autoridad que Tomo Nota de los Hechos

                clsUtilerias.seleccionarDropDownList(ddlAutZon, objIncidente.ZonaEmpleadoTomaNota.IdZona);
                ddlAutZon_SelectedIndexChanged(null, null);
                clsUtilerias.seleccionarDropDownList(ddlAutGra, objIncidente.JerarquiaEmpleadoTomaNota.IdJerarquia);
                ddlAutGra_SelectedIndexChanged(null, null);
                clsUtilerias.seleccionarDropDownList(ddlAutNom, objIncidente.IdEmpleadoTomaNota.ToString().ToUpper() + "&" + objIncidente.NoEmpleadoTomaNota + "&" + objIncidente.IdEmpleadoPuestoTomaNota);
                ddlAutNom_SelectedIndexChanged(null, null);
                clsUtilerias.llenarTextBox(txbAccionMando, objIncidente.RiAccionMando);

                #endregion

                #region Autor del Parte Inicial

                clsUtilerias.seleccionarDropDownList(ddlParIniZon, objIncidente.ZonaEmpleadoAutor.IdZona);
                ddlParIniZon_SelectedIndexChanged(null, null);
                clsUtilerias.seleccionarDropDownList(ddlParIniGra, objIncidente.JerarquiaEmpleadoAutor.IdJerarquia);
                ddlParIniGra_SelectedIndexChanged(null, null);
                clsUtilerias.seleccionarDropDownList(ddlParIniNom, objIncidente.IdEmpleadoAutor.ToString().ToUpper() + "&" + objIncidente.NoEmpleadoAutor + "&" + objIncidente.IdEmpleadoPuestoAutor);
                ddlParIniNom_SelectedIndexChanged(null, null);

                #endregion

                #region Superior del Autor del Parte Inicial

                clsUtilerias.seleccionarDropDownList(ddlSupAutZon, objIncidente.ZonaEmpleadoSuperior.IdZona);
                ddlSupAutZon_SelectedIndexChanged(null, null);
                clsUtilerias.seleccionarDropDownList(ddlSupAutGra, objIncidente.JerarquiaEmpleadoSuperior.IdJerarquia);
                ddlSupAutGra_SelectedIndexChanged(null, null);
                clsUtilerias.seleccionarDropDownList(ddlSupAutNom, objIncidente.IdEmpleadoSuperior.ToString().ToUpper() + "&" + objIncidente.NoEmpleadoSuperior + "&" + objIncidente.IdEmpleadoPuestoSuperior);
                ddlSupAutNom_SelectedIndexChanged(null, null);

                #endregion

                #region En caso de accidente Aéreo o Terrestre

                clsUtilerias.llenarTextBox(txbDanMat, objIncidente.RiDanioMaterial);
                clsUtilerias.llenarTextBox(txbMonto, objIncidente.RiMonto);

                #endregion
            }

            #endregion

            #region Solo Consulta
            clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];
            clsEntPermiso objPermiso = new clsEntPermiso();
            objPermiso.IdPerfil = objSesion.usuario.Perfil.IdPerfil;
            //Operacion 35 Solo Consulta INCIDENTES (HECHOS)
            if (clsDatPermiso.consultarPermiso(objPermiso.IdPerfil, 35, objSesion) == false)
            {
                btnGuardar.Visible = false;
                ddlServicio.Enabled = ddlInstalacion.Enabled = txbFecha.Enabled = calFecha.Enabled = txbHora.Enabled = txbLugar.Enabled = false;
                ddlGrado.Enabled = ddlZona.Enabled = ddlNomCompleto.Enabled = false;
                txbActividad.Enabled = txbUniformeCivil.Enabled = txbHecCon.Enabled = false;
                txbCuaLes.Enabled = txbCadLes.Enabled = txbAccConAgr.Enabled = false;
                txbAccionMando.Enabled = false;
                ddlAutGra.Enabled = ddlAutZon.Enabled = ddlAutNom.Enabled = false;
                ddlParIniGra.Enabled = ddlParIniZon.Enabled = ddlParIniNom.Enabled = false;
                ddlSupAutGra.Enabled = ddlSupAutZon.Enabled = ddlSupAutNom.Enabled = false;
                txbDanMat.Enabled = txbMonto.Enabled = false;
            }
            #endregion
        }
    }

    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServicio.SelectedIndex > 0)
        {
            //clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuario", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuarioAsignacionLimitada", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);

            ddlInstalacion.Enabled = true;
            ddlGrado.Items.Clear();
            ddlGrado.Enabled = false;
            ddlNomCompleto.Items.Clear();
            ddlNomCompleto.Enabled = false;
            lblNoEmpleado.Text = "-";
            return;
        }
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
    }

    protected void ddlInstalacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInstalacion.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogo(ddlGrado, "catalogo.spLeerJerarquia", "jerDescripcion", "idJerarquia", (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlGrado.Enabled = true;
            ddlNomCompleto.Items.Clear();
            ddlNomCompleto.Enabled = false;
            lblNoEmpleado.Text= "-";
            return;
        }
        ddlGrado.Items.Clear();
        ddlGrado.Enabled = false;
    }

    protected void ddlGrado_SelectedIndexChanged(object sender, EventArgs e)
    {
        // clsCatalogos.llenarNombreCompleto(ddlGrado, ddlZona, ddlNomCompleto, (clsEntSesion)Session["objSession" + Session.SessionID]);
        clsCatalogos.llenarNombreCompletoPorServicio(ddlGrado, ddlZona, ddlServicio, ddlInstalacion, ddlNomCompleto, (clsEntSesion)Session["objSession" + Session.SessionID]);
    }

    protected void ddlZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCatalogos.llenarNombreCompleto(ddlGrado, ddlZona, ddlNomCompleto, (clsEntSesion)Session["objSession" + Session.SessionID]);
    }

    protected void ddlAutGra_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCatalogos.llenarNombreCompleto(ddlAutGra, ddlAutZon, ddlAutNom, (clsEntSesion)Session["objSession" + Session.SessionID]);
    }

    protected void ddlAutZon_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCatalogos.llenarNombreCompleto(ddlAutGra, ddlAutZon, ddlAutNom, (clsEntSesion)Session["objSession" + Session.SessionID]);
    }

    protected void ddlParIniGra_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCatalogos.llenarNombreCompleto(ddlParIniGra, ddlParIniZon, ddlParIniNom, (clsEntSesion)Session["objSession" + Session.SessionID]);
    }

    protected void ddlParIniZon_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCatalogos.llenarNombreCompleto(ddlParIniGra, ddlParIniZon, ddlParIniNom, (clsEntSesion)Session["objSession" + Session.SessionID]);
    }

    protected void ddlSupAutGra_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCatalogos.llenarNombreCompleto(ddlSupAutGra, ddlSupAutZon, ddlSupAutNom, (clsEntSesion)Session["objSession" + Session.SessionID]);
    }

    protected void ddlSupAutZon_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsCatalogos.llenarNombreCompleto(ddlSupAutGra, ddlSupAutZon, ddlSupAutNom, (clsEntSesion)Session["objSession" + Session.SessionID]);
    }

    protected void ddlNomCompleto_SelectedIndexChanged(object sender, EventArgs e)
    {

    

        if (ddlNomCompleto.SelectedIndex > 0)
        {
            clsUtilerias.llenarLabel(lblNoEmpleado, ddlNomCompleto.SelectedValue.Split('&')[1], "-");
            return;
        }
        lblNoEmpleado.Text = "-";
    }

    protected void ddlAutNom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAutNom.SelectedIndex > 0)
        {
            clsUtilerias.llenarLabel(lblAutNum, ddlAutNom.SelectedValue.Split('&')[1], "-");
            return;
        }
        lblAutNum.Text = "-";
    }

    protected void ddlParIniNom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlParIniNom.SelectedIndex > 0)
        {
            clsUtilerias.llenarLabel(lblParIniNum, ddlParIniNom.SelectedValue.Split('&')[1], "-");
            return;
        }
        lblParIniNum.Text = "-";
    }

    protected void ddlSupAutNom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSupAutNom.SelectedIndex > 0)
        {
            clsUtilerias.llenarLabel(lblSupAutNum, ddlSupAutNom.SelectedValue.Split('&')[1], "-");
            return;
        }
        lblSupAutNum.Text = "-";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        clsEntReporteIncidente objIncidente = new clsEntReporteIncidente();

        #region Incidente

        objIncidente.IdIncidente = Convert.ToInt32(hfIdIncidente.Value);

        objIncidente.Servicio = new clsEntServicio();
        objIncidente.Instalacion = new clsEntInstalacion();

        if (!string.IsNullOrEmpty(ddlServicio.SelectedValue))
        {
            objIncidente.Servicio.idServicio = Convert.ToInt32(ddlServicio.SelectedValue);
            objIncidente.Servicio.serDescripcion = ddlServicio.SelectedItem.Text;
        }

        if (!string.IsNullOrEmpty(ddlInstalacion.SelectedValue))
        {
            objIncidente.Instalacion.IdInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue);
            objIncidente.Instalacion.InsNombre = ddlInstalacion.SelectedItem.Text;
        }

        objIncidente.RiFechaHora = clsUtilerias.dtObtenerFecha(txbFecha.Text);

        if (!string.IsNullOrEmpty(txbHora.Text))
        {
            objIncidente.RiFechaHora = new DateTime(objIncidente.RiFechaHora.Year, objIncidente.RiFechaHora.Month, objIncidente.RiFechaHora.Day, Convert.ToInt32(txbHora.Text.Split(':')[0]), Convert.ToInt32(txbHora.Text.Split(':')[1]), Convert.ToInt32(txbHora.Text.Split(':')[2]));
        }

        objIncidente.RiLugar = txbLugar.Text;

        #endregion

        #region Personal Involucrado En Los Hechos

        objIncidente.ZonaEmpleadoInvolucrado = new clsEntZona();
        objIncidente.JerarquiaEmpleadoInvolucrado = new clsEntJerarquia();

        if (!string.IsNullOrEmpty(ddlZona.SelectedValue))
        {
            objIncidente.ZonaEmpleadoInvolucrado.IdZona = Convert.ToInt16(ddlZona.SelectedValue);
            objIncidente.ZonaEmpleadoInvolucrado.ZonDescripcion = ddlZona.SelectedItem.Text;
        }

        if (!string.IsNullOrEmpty(ddlGrado.SelectedValue))
        {
            objIncidente.JerarquiaEmpleadoInvolucrado.IdJerarquia = Convert.ToByte(ddlGrado.SelectedValue);
            objIncidente.JerarquiaEmpleadoInvolucrado.JerDescripcion = ddlGrado.SelectedItem.Text;
        }

        if (ddlNomCompleto.SelectedIndex > 0)
        {
            objIncidente.NombreEmpleadoInvolucrado = ddlNomCompleto.SelectedItem.Text;
            objIncidente.IdEmpleadoInvolucrado = new Guid(ddlNomCompleto.SelectedValue.Split('&')[0]);
            objIncidente.NoEmpleadoInvolucrado = Convert.ToInt32(ddlNomCompleto.SelectedValue.Split('&')[1]);
            objIncidente.IdEmpleadoPuestoInvolucrado = Convert.ToInt16(ddlNomCompleto.SelectedValue.Split('&')[2]);
        }

        objIncidente.RiActividad = txbActividad.Text;
        objIncidente.RiUniforme = txbUniformeCivil.Text;
        objIncidente.RiDesarrolloConsecuencia = txbHecCon.Text;
        objIncidente.RiLesion = txbCuaLes.Text;
        objIncidente.RiUbicacionCadaverLesionado = txbCadLes.Text;
        objIncidente.RiAccionVsAgresor = txbAccConAgr.Text;

        #endregion

        #region Autoridad que Tomo Nota de los Hechos

        objIncidente.ZonaEmpleadoTomaNota = new clsEntZona();
        objIncidente.JerarquiaEmpleadoTomaNota = new clsEntJerarquia();

        if (!string.IsNullOrEmpty(ddlAutZon.SelectedValue))
        {
            objIncidente.ZonaEmpleadoTomaNota.IdZona = Convert.ToInt16(ddlAutZon.SelectedValue);
            objIncidente.ZonaEmpleadoTomaNota.ZonDescripcion = ddlAutZon.SelectedItem.Text;
        }

        if (!string.IsNullOrEmpty(ddlAutGra.SelectedValue))
        {
            objIncidente.JerarquiaEmpleadoTomaNota.IdJerarquia = Convert.ToByte(ddlAutGra.SelectedValue);
            objIncidente.JerarquiaEmpleadoTomaNota.JerDescripcion = ddlAutGra.SelectedItem.Text;
        }

        if (ddlAutNom.SelectedIndex > 0)
        {
            objIncidente.NombreEmpleadoTomaNota = ddlAutNom.SelectedItem.Text;
            objIncidente.IdEmpleadoTomaNota = new Guid(ddlAutNom.SelectedValue.Split('&')[0]);
            objIncidente.NoEmpleadoTomaNota = Convert.ToInt32(ddlAutNom.SelectedValue.Split('&')[1]);
            objIncidente.IdEmpleadoPuestoTomaNota = Convert.ToInt16(ddlAutNom.SelectedValue.Split('&')[2]);
        }

        objIncidente.RiAccionMando = txbAccionMando.Text;

        #endregion

        #region Autor del Parte Inicial

        objIncidente.ZonaEmpleadoAutor = new clsEntZona();
        objIncidente.JerarquiaEmpleadoAutor = new clsEntJerarquia();

        if (!string.IsNullOrEmpty(ddlParIniZon.SelectedValue))
        {
            objIncidente.ZonaEmpleadoAutor.IdZona = Convert.ToInt16(ddlParIniZon.SelectedValue);
            objIncidente.ZonaEmpleadoAutor.ZonDescripcion = ddlParIniZon.SelectedItem.Text;
        }

        if (!string.IsNullOrEmpty(ddlParIniGra.SelectedValue))
        {
            objIncidente.JerarquiaEmpleadoAutor.IdJerarquia = Convert.ToByte(ddlParIniGra.SelectedValue);
            objIncidente.JerarquiaEmpleadoAutor.JerDescripcion = ddlParIniGra.SelectedItem.Text;
        }

        if (ddlParIniNom.SelectedIndex > 0)
        {
            objIncidente.NombreEmpleadoAutor = ddlParIniNom.SelectedItem.Text;
            objIncidente.IdEmpleadoAutor = new Guid(ddlParIniNom.SelectedValue.Split('&')[0]);
            objIncidente.NoEmpleadoAutor = Convert.ToInt32(ddlParIniNom.SelectedValue.Split('&')[1]);
            objIncidente.IdEmpleadoPuestoAutor = Convert.ToInt16(ddlParIniNom.SelectedValue.Split('&')[2]);
        }

        #endregion

        #region Superior del Autor del Parte Inicial

        objIncidente.ZonaEmpleadoSuperior = new clsEntZona();
        objIncidente.JerarquiaEmpleadoSuperior = new clsEntJerarquia();

        if (!string.IsNullOrEmpty(ddlSupAutZon.SelectedValue))
        {
            objIncidente.ZonaEmpleadoSuperior.IdZona = Convert.ToInt16(ddlSupAutZon.SelectedValue);
            objIncidente.ZonaEmpleadoSuperior.ZonDescripcion = ddlSupAutZon.SelectedItem.Text;
        }

        if (!string.IsNullOrEmpty(ddlSupAutGra.SelectedValue))
        {
            objIncidente.JerarquiaEmpleadoSuperior.IdJerarquia = Convert.ToByte(ddlSupAutGra.SelectedValue);
            objIncidente.JerarquiaEmpleadoSuperior.JerDescripcion = ddlSupAutGra.SelectedItem.Text;
        }

        if (ddlSupAutNom.SelectedIndex > 0)
        {
            objIncidente.NombreEmpleadoSuperior = ddlSupAutNom.SelectedItem.Text;
            objIncidente.IdEmpleadoSuperior = new Guid(ddlSupAutNom.SelectedValue.Split('&')[0]);
            objIncidente.NoEmpleadoSuperior = Convert.ToInt32(ddlSupAutNom.SelectedValue.Split('&')[1]);
            objIncidente.IdEmpleadoPuestoSuperior = Convert.ToInt16(ddlSupAutNom.SelectedValue.Split('&')[2]);
        }

        #endregion

        #region En caso de accidente Aéreo o Terrestre

        objIncidente.RiDanioMaterial = txbDanMat.Text;
        if (!string.IsNullOrEmpty(txbMonto.Text))
        {
            objIncidente.RiMonto = Convert.ToDouble(txbMonto.Text);
        }

        #endregion

        Session["objIncidente" + Session.SessionID] = objIncidente;
        Response.Redirect("~/Incidentes/frmIncidentesConfirmacion.aspx");
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Session["objIncidente" + Session.SessionID] = null;
        Response.Redirect("~/Incidentes/frmIncidentes.aspx");
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session["objIncidente" + Session.SessionID] = null;
        Response.Redirect("~/frmInicio.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Incidentes/frmBusquedaIncidentes.aspx");
    }
}
