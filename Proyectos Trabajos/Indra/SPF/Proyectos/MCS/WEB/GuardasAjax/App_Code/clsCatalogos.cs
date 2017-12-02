using System;
using System.Web.UI.WebControls;
using SICOGUA.Datos;
using SICOGUA.Seguridad;
using System.Collections.Generic;
using System.Data;
using proEntidades;
using SICOGUA.Entidades;

public class clsCatalogos
{
    #region anexo tecnico
    public static void llenarTipoHorario(DropDownList ddlCombo, string strProcedimiento, string strCampo, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaTipoHorario(strProcedimiento, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }
    public static void llenarTipoHorario(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strTipoHorario, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaTurnoXTipoHorario(strProcedimiento, strTipoHorario, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }
    public static void llenarAutoriza(DropDownList ddlCombo, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultarAutoriza(objSesion);
        ddlCombo.DataTextField = "nombreCompleto";
        ddlCombo.DataValueField = "idEmpleado";
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    #endregion anexo tecnico
    public static void llenarCatalogoTipoAsignacion(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultarTipoAsignacion(strProcedimiento, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }
    public static void llenarCatalogoZona(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoZona(strProcedimiento, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoAgrupamiento(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idZona, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoAgrupamiento(strProcedimiento, idZona, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoCompania(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idAgrupamiento, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoCompania(strProcedimiento, idAgrupamiento, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoSeccion(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idCompania, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoSeccion(strProcedimiento, idCompania, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoPeloton(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idSeccion, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoPeloton(strProcedimiento, idSeccion, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoTipoServicio(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoTipoServicio(strProcedimiento, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoServicio(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoServicio(strProcedimiento, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoInstalacion(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idServicio, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoInstalacion(strProcedimiento, idServicio, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoServicioPorTipoServicio(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idTipoServicio, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoServicioPorTipoServicio(strProcedimiento, idTipoServicio, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoServicioPorUsuarioyTipoServicio(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idTipoServicio, clsEntSesion objSesion)
    {
        int idUsuario = objSesion.usuario.IdUsuario;
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoServicioPorUsuarioyTipoServicio(strProcedimiento, idTipoServicio, idUsuario, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoServicioPorUsuario(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, clsEntSesion objSesion)
    {
        int idUsuario = objSesion.usuario.IdUsuario;
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoServicioPorUsuario(strProcedimiento, idUsuario, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }
    public static void llenarCatalogoEstatusREA(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, clsEntSesion objSesion)
    {        
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoEstatusREA(strProcedimiento,  objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }
    public static void llenarHoarioServicioInstalacion(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idServicio, int idInstalacion, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultarHorarioSerIns(strProcedimiento, idServicio, idInstalacion, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoInstalacionPorUsuario(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idServicio, clsEntSesion objSesion)
    {
        int idUsuario = objSesion.usuario.IdUsuario;
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoInstalacionPorUsuario(strProcedimiento, idServicio, idUsuario, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }



    public static void llenarCatalogoInstalacionPorUsuarioyZona(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idZona, int idServicio, clsEntSesion objSesion)
    {
        int idUsuario = objSesion.usuario.IdUsuario;
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoInstalacionPorUsuarioyZona(strProcedimiento, idZona, idServicio, idUsuario, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoInstalacionPorServicioZona(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idZona, int idServicio, clsEntSesion objSesion)
    {
        //int idUsuario = objSesion.usuario.IdUsuario;
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoInstalacionesPorServicioZona(strProcedimiento, idZona, idServicio, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogo(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogo(strProcedimiento, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoConParametros(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, Dictionary<string, clsEntParameter> parameters, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;

        DataTable dt = clsDatCatalogos.consultaCatalogo(strProcedimiento, parameters, objSesion);
        if (dt != null)
        {
            ddlCombo.DataSource = dt;

            ddlCombo.DataTextField = strCampo;
            ddlCombo.DataValueField = strValor;
            ddlCombo.DataBind();
        }
        
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoMunicipio(DropDownList ddlCombo, int idEstado, string strProcedimiento, string strCampo, string strValor, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaMunicipio(strProcedimiento, idEstado, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoAsentamiento(DropDownList ddlCombo, int idEstado, int idMunicipio, string strProcedimiento, string strCampo, string strValor, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaAsentamiento(strProcedimiento, idEstado, idMunicipio, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoEmpleadoJerarquiaZona(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, Int16 idJerarquia, Int16 idZona, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoEmpleadoJerarquiaZona(strProcedimiento, idJerarquia, idZona, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarNombreCompleto(DropDownList dJerarquia, DropDownList dZona, DropDownList dNombre, clsEntSesion objSesion)
    {
        Int16 idZona = 0;
        Int16 idJerarquia = 0;

        if (dJerarquia.SelectedIndex > 0)
        {
            idJerarquia = Convert.ToInt16(dJerarquia.SelectedValue);
        }
        if (dZona.SelectedIndex > 0)
        {
            idZona = Convert.ToInt16(dZona.SelectedValue);
        }

        llenarCatalogoEmpleadoJerarquiaZona(dNombre, "empleado.spBuscarEmpleadoPorJerarquiaZona", "empNombre", "idEmpleado", idJerarquia, idZona, objSesion);
        dNombre.Enabled = true;

        return;
    }

    public static void llenarCatalogoEmpleadoJerarquiaZonaPorServicio(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, Int16 idJerarquia, Int16 idZona, Int32 idServicio, Int32 idInstalacion, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoEmpleadoJerarquiaZonaServicio(strProcedimiento, idJerarquia, idZona, idServicio, idInstalacion, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarNombreCompletoPorServicio(DropDownList dJerarquia, DropDownList dZona, DropDownList dServicio, DropDownList dInstalacion, DropDownList dNombre, clsEntSesion objSesion)
    {
        Int16 idZona = 0;
        Int16 idJerarquia = 0;
        Int32 idServicio = 0;
        Int32 idInstalacion = 0;

        if (dJerarquia.SelectedIndex > 0)
        {
            idJerarquia = Convert.ToInt16(dJerarquia.SelectedValue);
        }
        if (dZona.SelectedIndex > 0)
        {
            idZona = Convert.ToInt16(dZona.SelectedValue);
        }
        if (dServicio.SelectedIndex > 0)
        {
            idServicio = Convert.ToInt16(dServicio.SelectedValue);
        }
        if (dInstalacion.SelectedIndex > 0)
        {
            idInstalacion = Convert.ToInt16(dInstalacion.SelectedValue);
        }

        llenarCatalogoEmpleadoJerarquiaZonaPorServicio(dNombre, "empleado.spBuscarEmpleadosPorJerarquiaServicioInst", "empNombre", "idEmpleado", idJerarquia, idZona, idServicio, idInstalacion, objSesion);
        dNombre.Enabled = true;

        return;
    }

    public static void llenarCatalogoJerarquia(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoJerarquia(strProcedimiento, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoPuesto(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, int idJerarquia, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoPuesto(strProcedimiento, idJerarquia, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    public static void llenarCatalogoTipoHorario(DropDownList ddlCombo, string strProcedimiento, string strCampo, string strValor, clsEntSesion objSesion)
    {
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaCatalogoTipoHorario(strProcedimiento, objSesion);
        ddlCombo.DataTextField = strCampo;
        ddlCombo.DataValueField = strValor;
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }
    public static void llenarCatalogoHorarioREA(DropDownList ddlCombo, int idServicio, int idInstalacion, clsEntSesion objSesion)
    {
        int idUsuario = objSesion.usuario.IdUsuario;
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaHorarioRea(idServicio, idInstalacion, objSesion);
        ddlCombo.DataTextField = "horDescripcion";
        ddlCombo.DataValueField = "idHorario";
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

    #region Pase Lista generica
    public static void llenarCatalogoHorarioREALista(DropDownList ddlCombo, int idServicio,int idInstalacion,DateTime fecha, clsEntSesion objSesion)
    {
        int idUsuario = objSesion.usuario.IdUsuario;
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;


        List<clsEntCatHorario> lst = new List<clsEntCatHorario>();
        DataTable dt = clsDatCatalogos.consultaHorarioReaLista(idServicio, idInstalacion,fecha, objSesion);
        foreach (DataRow dr in dt.Rows)
        {
            lst.Add(
             new clsEntCatHorario
            {
                idHorario = (Convert.ToString(dr["idHorario"]) + '|' + Convert.ToString(dr["hdEntrada"]) + "|" + Convert.ToString(dr["hdSalida"]) +"|" + Convert.ToString(dr["horJornada"])),
                horDescripcion=(string)dr["horNombre"]
            });

        }



        ddlCombo.DataSource = lst;
        ddlCombo.DataTextField = "horDescripcion";
        ddlCombo.DataValueField = "idHorario";
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }

   
    #endregion

    public static void llenarMotivoInmovilidad(DropDownList ddlCombo, clsEntSesion objSesion)
    {
        int idUsuario = objSesion.usuario.IdUsuario;
        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = clsDatCatalogos.consultaTipoHorario("catalogo.spuConsultarMotivoInmovilidad", objSesion);
        ddlCombo.DataTextField = "miDescripcion";
        ddlCombo.DataValueField = "idMotivoInmovilidad";
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();
    }


    public static List<clsEntFirmanteOficioAsignacion> llenarCatalogoFirmantesPorZona(DropDownList ddlCombo, int idServicio, int idInstalacion, clsEntSesion objSesion)
    {
        List<clsEntFirmanteOficioAsignacion> listaFirmantes = new List<clsEntFirmanteOficioAsignacion>();


        DataTable firmantes = new DataTable();

        firmantes = clsDatCatalogos.consultaCatalogoFirmantesPorZona(idServicio, idInstalacion, objSesion);

        for (int i = 0; i < firmantes.Rows.Count; i++)
        {
            clsEntFirmanteOficioAsignacion empleadoFirmante = new clsEntFirmanteOficioAsignacion();

            empleadoFirmante.IdEmpleado = new Guid(firmantes.Rows[i]["idEmpleado"].ToString());
            empleadoFirmante.empNombreCompleto = firmantes.Rows[i]["nombreCompletoFirmante"].ToString();
            empleadoFirmante.jerDescripcion = firmantes.Rows[i]["jerDescripcion"].ToString();
            empleadoFirmante.puestoDescripcion = firmantes.Rows[i]["foaPuestoDescripcion"].ToString();
            empleadoFirmante.citaAusencia = firmantes.Rows[i]["foaCitaAusencia"].ToString();

            listaFirmantes.Add(empleadoFirmante);

        }


        ddlCombo.Items.Clear();
        ddlCombo.Items.Add(new ListItem("SELECCIONAR", ""));
        ddlCombo.AppendDataBoundItems = true;
        ddlCombo.DataSource = firmantes;
        ddlCombo.DataTextField = "nombreCompJer";
        ddlCombo.DataValueField = "idEmpleado";
        ddlCombo.DataBind();
        ddlCombo.ClearSelection();


        return listaFirmantes;


    }
}
