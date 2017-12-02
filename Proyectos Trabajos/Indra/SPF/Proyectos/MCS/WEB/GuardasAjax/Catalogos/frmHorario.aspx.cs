using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Negocio;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using SICOGUA.Datos;
using proUtilerias;
using REA.Negocio;
using REA.Datos;
using REA.Entidades;


public partial class Catalogos_frmHorario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            Session["idHorario"] = null;
            Session["impresion" + Session.SessionID] = null;
           
               if (Session["servicio"]!=null && Session["instalacion"]!=null)
               {


                   #region Catalogos
                   clsCatalogos.llenarCatalogo(ddlTipoHorarioR, "catalogo.spuConsultarTipoHorarioRea", "thTurno", "idTipoHorario", (clsEntSesion)Session["objSession" + Session.SessionID]);
                   #endregion
                   REA.Entidades.clsEntHorario objHorario = new REA.Entidades.clsEntHorario();
                    clsEntZona objzona = new clsEntZona();
                    objHorario.Zona = objzona;
                    objHorario = clsNegHorarioREA.consultarServicioInstalacionHor((int)Session["servicio"], (int)Session["instalacion"], (clsEntSesion)Session["objSession" + Session.SessionID]);
                    lblZona.Text = objHorario.Zona.ZonDescripcion;
                    lblServicio.Text = objHorario.Servicio.serDescripcion;
                    lbInstalacion.Text = objHorario.Instalacion.InsNombre;
                    lbTipo.Text = objHorario.tipoInstalacion;
                    if (objHorario.horVigente == true)
                        lblVigente.Text = "SI";
                    else
                        lblVigente.Text = "NO";
                    lbTipo.Text = objHorario.tipoInstalacion;
                    List<REA.Entidades.clsEntHorario> lsHorario = new List<REA.Entidades.clsEntHorario>();
                    lsHorario = clsNegHorarioREA.obtenerHorarioServicioInstalacion((int)Session["servicio"], (int)Session["instalacion"], (clsEntSesion)Session["objSession" + Session.SessionID]);
                    grvHorario.DataSource = lsHorario;
                    grvHorario.DataBind();
                    deshabilitaCampos();
                    btnGuardar.Enabled = false;
                    btnBuscar.Enabled = true;
                }
            
        }
    }

        public void limpiarforma()
        {
            txbNombre.Text = string.Empty;
            txbDescripcion.Text = string.Empty;
            ddlTipoHorario.SelectedValue = "0";
            ckbTolerancia.Checked = false;
            ckbRetardos.Checked = false;
            ckbComida.Checked = false;
            ckbVigente.Checked = false;
            ckbAbierto.Checked = false;
            txbHora.Text = string.Empty;
            txbComidaMin.Text = string.Empty;
            txbDescanso.Text = string.Empty;
            txbEntDo.Text = string.Empty;
            txbEntJu.Text = string.Empty;
            txbEntL.Text = string.Empty;
            txbEntM.Text = string.Empty;
            txbEntMi.Text = string.Empty;
            txbEntSa.Text = string.Empty;
            txbEntSaM.Text = string.Empty;
            txbFechaIni.Text = string.Empty;
            txbHoraFin.Text = string.Empty;
            txbHoraIni.Text = string.Empty;
            txbRetardo.Text = string.Empty;
            txbSaJu.Text = string.Empty;
            txbSaL.Text = string.Empty;
            txbSalDo.Text = string.Empty;
            txbSalSa.Text = string.Empty;
            txbSaMi.Text = string.Empty;
            txbTolerancia.Text = string.Empty;
            txbTrabajo.Text = string.Empty;
            txtbEntVi.Text = string.Empty;
            txtbSalVi.Text = string.Empty;
            ckbDiasf.Checked = false;
            ckbDomingo.Checked = false;
            ckbES.Checked = false;
            ckbJue.Checked = false;
            ckbLunes.Checked = false;
            ckbMartes.Checked = false;
            ckbMiercoles.Checked = false;
            ckbRetardos.Checked = false;
            ckbSabado.Checked = false;
            ckbTolerancia.Checked = false;
            ckbViernes.Checked = false;
            ckbVigente.Checked = true;
            ckbFinesSemana.Checked = false;
            ddlTipoHorarioR.SelectedIndex = -1;
          }

  
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            REA.Entidades.clsEntHorario objHorario = new REA.Entidades.clsEntHorario();
            clsEntHorarioComidaREA objHorarioComida = new clsEntHorarioComidaREA();
            clsEntTipoHorarioREA objTipoHorarioRea = new clsEntTipoHorarioREA();

            if (!string.IsNullOrEmpty(txbNombre.Text.Trim())) { objHorario.horNombre = txbNombre.Text; }
            if (!string.IsNullOrEmpty(txbDescripcion.Text.Trim())) { objHorario.horDescripcion = txbDescripcion.Text; }
            if (!string.IsNullOrEmpty(txbFechaIni.Text.Trim())) { objHorario.horFechaInicio = Convert.ToDateTime(txbFechaIni.Text); }
            if (!string.IsNullOrEmpty(txbTrabajo.Text.Trim())) { objHorario.horJornada = Convert.ToByte(txbTrabajo.Text); }
            if (!string.IsNullOrEmpty(txbDescanso.Text.Trim())) { objHorario.horDescanso = Convert.ToByte(txbDescanso.Text); }
            if (ddlTipoHorario.SelectedIndex > 0) { objHorario.horTipo = Convert.ToChar(ddlTipoHorario.SelectedValue); }
            if (!string.IsNullOrEmpty(txbTolerancia.Text.Trim())) { objHorario.horTolerancia = Convert.ToByte(txbTolerancia.Text); }
            if (!string.IsNullOrEmpty(txbRetardo.Text.Trim())) { objHorario.horRetardo = Convert.ToByte(txbTolerancia.Text); }
            objHorarioComida.hcTiempoComida = Convert.ToBoolean(ckbComida.Checked);
            if (!string.IsNullOrEmpty(txbComidaMin.Text.Trim())) { objHorarioComida.hcMinuto = Convert.ToByte(txbComidaMin.Text); }
            if (!string.IsNullOrEmpty(txbHoraIni.Text.Trim())) { objHorarioComida.hcComidaEntrada = Convert.ToDateTime(txbHoraIni.Text); }
            if (!string.IsNullOrEmpty(txbHoraFin.Text.Trim())) { objHorarioComida.hcComidaSalida = Convert.ToDateTime(txbHoraFin.Text); }
            if (ckbES.Checked == true) { objHorario.horSalidaLaboral = Convert.ToBoolean(ckbES.Checked); }
            if (ckbDiasf.Checked == true) { objHorario.horDiaFestivo = false; } else { objHorario.horDiaFestivo = true; }
            if (rbtBiometrico.Checked == true) { objHorario.horModulo = 'B'; }
            if (rbtMCS.Checked == true) { objHorario.horModulo = 'M'; }
            if (ckbAbierto.Checked == true) { objHorario.horAbierto = Convert.ToBoolean(ckbAbierto.Checked); }
            if (!string.IsNullOrEmpty(txbHora.Text.Trim())) { objHorario.horTiempoMCS = Convert.ToByte(txbHora.Text); }
            if (!string.IsNullOrEmpty(txbEntL.Text.Trim())) { objHorario.horHoraEntradaL = Convert.ToDateTime(txbEntL.Text); }
            if (!string.IsNullOrEmpty(txbEntM.Text.Trim())) { objHorario.horHoraEntradaM = Convert.ToDateTime(txbEntM.Text); }
            if (!string.IsNullOrEmpty(txbEntMi.Text.Trim())) { objHorario.horHoraEntradaMi = Convert.ToDateTime(txbEntMi.Text); }
            if (!string.IsNullOrEmpty(txbEntJu.Text.Trim())) { objHorario.horHoraEntradaJue = Convert.ToDateTime(txbEntJu.Text); }
            if (!string.IsNullOrEmpty(txtbEntVi.Text.Trim())) { objHorario.horHoraEntradaVie = Convert.ToDateTime(txtbEntVi.Text); }
            if (!string.IsNullOrEmpty(txbEntSa.Text.Trim())) { objHorario.horHoraEntradaSa = Convert.ToDateTime(txbEntSa.Text); }
            if (!string.IsNullOrEmpty(txbEntDo.Text.Trim())) { objHorario.horHoraEntradaDom = Convert.ToDateTime(txbEntDo.Text); }
            if (!string.IsNullOrEmpty(txbSaL.Text.Trim())) { objHorario.horHoraSalidaL = Convert.ToDateTime(txbSaL.Text); }
            if (!string.IsNullOrEmpty(txbEntSaM.Text.Trim())) { objHorario.horHoraSalidaM = Convert.ToDateTime(txbEntSaM.Text); }
            if (!string.IsNullOrEmpty(txbSaMi.Text.Trim())) { objHorario.horHoraSalidaMi = Convert.ToDateTime(txbSaMi.Text); }
            if (!string.IsNullOrEmpty(txbSaJu.Text.Trim())) { objHorario.horHoraSalidaJue = Convert.ToDateTime(txbSaJu.Text); }
            if (!string.IsNullOrEmpty(txtbSalVi.Text.Trim())) { objHorario.horHoraSalidaVie = Convert.ToDateTime(txtbSalVi.Text); }
            if (!string.IsNullOrEmpty(txbSalSa.Text.Trim())) { objHorario.horHoraSalidaSa = Convert.ToDateTime(txbSalSa.Text); }
            if (!string.IsNullOrEmpty(txbSalDo.Text.Trim())) { objHorario.horHoraSalidaDom = Convert.ToDateTime(txbSalDo.Text); }
            if (ckbFinesSemana.Checked == true) { objHorario.horFinesSemana = Convert.ToBoolean(ckbFinesSemana.Checked); }
            objHorario.horLunes = Convert.ToBoolean(ckbLunes.Checked);
            objHorario.horMartes = Convert.ToBoolean(ckbMartes.Checked);
            objHorario.horMiercoles = Convert.ToBoolean(ckbMiercoles.Checked);
            objHorario.horJueves = Convert.ToBoolean(ckbJue.Checked);
            objHorario.horViernes = Convert.ToBoolean(ckbViernes.Checked);
            objHorario.horSabado = Convert.ToBoolean(ckbSabado.Checked);
            objHorario.horDomingo = Convert.ToBoolean(ckbDomingo.Checked);
            objHorario.horVigente = Convert.ToBoolean(ckbVigente.Checked);
            objHorario.tipoHorarioREA = new clsEntTipoHorarioREA();
            if (!string.IsNullOrEmpty(ddlTipoHorarioR.SelectedValue)) { objHorario.tipoHorarioREA.idTipoHorario = Convert.ToByte(ddlTipoHorarioR.SelectedValue); }

            if (Session["idHorario"] != null)
            {
                objHorario.idHorario = (int)Session["idHorario"];
            }

            if (!string.IsNullOrEmpty(txbEntL.Text.Trim())) { objHorario.horHoraEntradaL = Convert.ToDateTime(txbEntL.Text); }
            if (!string.IsNullOrEmpty(txbEntL.Text.Trim())) { objHorario.horHoraEntradaL = Convert.ToDateTime(txbEntL.Text); }
            clsEntServicio objServicio = new clsEntServicio();
            objHorario.Servicio = objServicio;
            objHorario.Servicio.idServicio = (int)Session["servicio"];

            clsEntInstalacion objInstalacion = new clsEntInstalacion();
            objHorario.Instalacion = objInstalacion;
            objHorario.Instalacion.IdInstalacion = (int)Session["instalacion"];
            string strMensaje = string.Empty;
            btnAgregar.Enabled = true;
            int intIdHorario = clsNegHorarioREA.insertarHorario(objHorario, objHorarioComida, (clsEntSesion)Session["objSession" + Session.SessionID], strMensaje);
            Response.Redirect("~/Generales/frmFinalizado.aspx?strEstatus=est100&strMensaje=" + strMensaje);
            return;

        }
        



    }
    protected void ckbRetardos_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbRetardos.Checked == true)
        {
            txbRetardo.Enabled = true;
        }
        else
        {
            txbRetardo.Enabled = false;
        }
    }
    protected void ckbComida_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbComida.Checked == true)
        {
            txbComidaMin.Enabled = true;
            txbHoraIni.Enabled = true;
            txbHoraFin.Enabled = true;

        }
        else
        {
            txbComidaMin.Enabled = false;
            txbHoraIni.Enabled = false;
            txbHoraFin.Enabled = false;
            txbHoraFin.Text = "";
            txbHoraIni.Text = "";

        }
    }
    protected void ckbTolerancia_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbTolerancia.Checked == true)
        {
            txbTolerancia.Enabled = true;
        }
        else
        {
            txbTolerancia.Enabled = false;
        }
    }
    protected void ckbLunes_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbLunes.Checked == true)
        {
            txbEntL.Enabled = true;
            txbSaL.Enabled = true;
        }
        else
        {
            txbEntL.Enabled = false;
            txbEntL.Text = "";
            txbSaL.Enabled = false;
            txbSaL.Text = "";
        }
    }
    protected void  ckbMartes_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbMartes.Checked==true)
        {
            txbEntM.Enabled=true;
            txbEntSaM.Enabled=true;
        }
        else
        {
             txbEntM.Enabled=false;
            txbEntSaM.Enabled=false;
            txbEntM.Text = "";
            txbEntSaM.Text = "";
        }
    }
    protected void  ckbJue_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbJue.Checked==true)
        {
            txbEntJu.Enabled=true;
            txbSaJu.Enabled=true;
        }
        else
        {
            txbEntJu.Enabled=false;
            txbSaJu.Enabled=false;
            txbEntJu.Text = "";
            txbSaJu.Text = "";
        }
    }
    protected void  ckbMiercoles_CheckedChanged(object sender, EventArgs e)
    {
        if(ckbMiercoles.Checked==true)
        {
            txbEntMi.Enabled=true;
            txbSaMi.Enabled=true;


        }
        else
        {
            txbEntMi.Enabled=false;
            txbSaMi.Enabled=false;
            txbEntMi.Text = "";
            txbSaMi.Text = "";
        }
    }
    protected void  ckbViernes_CheckedChanged(object sender, EventArgs e)
    {
        if(ckbViernes.Checked==true)
        {
            txtbEntVi.Enabled=true;
            txtbSalVi.Enabled=true;
        }
        else
        {
              txtbEntVi.Enabled=false;
            txtbSalVi.Enabled=false;
            txtbEntVi.Text = "";
            txtbSalVi.Text = "";
        }
    }
    protected void  ckbSabado_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbSabado.Checked==true)
        {
            txbEntSa.Enabled=true;
            txbSalSa.Enabled=true;
        }
        else
        {
            txbEntSa.Enabled=false;
            txbSalSa.Enabled=false;
            txbEntSa.Text = "";
            txbSalSa.Text = "";
        }
    }
    protected void  ckbDomingo_CheckedChanged(object sender, EventArgs e)
    {
        if(ckbDomingo.Checked==true)
        {
            txbEntDo.Enabled=true;
            txbSalDo.Enabled=true;
        }
        else
        {
             txbEntDo.Enabled=false;
            txbSalDo.Enabled=false;
            txbEntDo.Text = string.Empty; ;
            txbSalDo.Text=string.Empty;
           
        }
    }
    protected void  btnAgregar_Click(object sender, EventArgs e)
    {
        limpiarforma();
        habilitaCampos();
        btnGuardar.Enabled = true;
        btnBuscar.Enabled = false;
        btnAgregar.Enabled = false;
        Session["idHorario"] = null;
            
    }
    protected void rbtMCS_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtMCS.Enabled==true)
        {
            txbmEntMi.Enabled = true;
        }
        else
        {
            txbmEntMi.Enabled = false;
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        limpiarforma();
        Response.Redirect("~/frmInicio.aspx");

    }
 
    protected void grvHorario_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        int intIdHorario;
       intIdHorario=Convert.ToInt32(((Label)(grvHorario.Rows[e.RowIndex].Cells[0].FindControl("lblIdHorario"))).Text);
        REA.Entidades.clsEntHorario objHorario = clsNegHorarioREA.obtenerHorarioPorId(intIdHorario, (clsEntSesion)Session["objSession" + Session.SessionID]);
        //clsEntHorarioComidaREA objHorarioComida = new clsEntHorarioComidaREA();
        //objHorario.Comida=objHorarioComida;
        txbNombre.Text = objHorario.horNombre;
        txbDescripcion.Text = objHorario.horDescripcion;
        ddlTipoHorario.SelectedValue = objHorario.horTipo.ToString();
        txbFechaIni.Text = objHorario.horFechaInicio.ToShortDateString();
        txbTrabajo.Text = objHorario.horJornada.ToString();
        txbDescanso.Text = objHorario.horDescanso.ToString();
       // objHorario.tipoHorarioREA = new clsEntTipoHorarioREA();
        ddlTipoHorarioR.SelectedValue =objHorario.tipoHorarioREA.idTipoHorario==0 ?"": objHorario.tipoHorarioREA.idTipoHorario.ToString();
        if (objHorario.horTolerancia == 0)
        {
            ckbTolerancia.Checked = false;
            txbTolerancia.Text = string.Empty;
        }
        else
        {
            ckbTolerancia.Checked = true;
            txbTolerancia.Text = objHorario.horTolerancia.ToString();    
        }

        if (objHorario.horAbierto == true)
        {
            ckbAbierto.Checked = true;
        }
        else
        {
            ckbAbierto.Checked = false;
        }

        if (objHorario.horRetardo == 0)
        {
            ckbRetardos.Checked = false;
            txbRetardo.Text = string.Empty;
        }
        else
        {
            ckbRetardos.Checked = true;
            txbRetardo.Text = objHorario.horRetardo.ToString(); 
        }

        if (objHorario.Comida.hcMinuto == 0)
        {
            txbComidaMin.Text = string.Empty;
            txbHoraIni.Text = string.Empty;
            txbHoraFin.Text = string.Empty;
            ckbComida.Checked = false;

        }
        else
        {
            txbComidaMin.Text = objHorario.Comida.hcMinuto.ToString();
            txbHoraIni.Text = objHorario.Comida.hcComidaEntrada.ToString("HH:mm"); ;
            txbHoraFin.Text = objHorario.Comida.hcComidaSalida.ToString("HH:mm");
            ckbComida.Checked = true;

        }
        if (objHorario.horSalidaLaboral == true)
        {
            ckbES.Checked = true;
        }
        else
        {
            ckbES.Checked = false;
        }

        if (objHorario.horDiaFestivo == true)
        {
            ckbDiasf.Checked = false;
        }
        else
        {
            ckbDiasf.Checked = true;
        }

        if (objHorario.horModulo == 'B')
        {
            rbtBiometrico.Checked = true;
            rbtMCS.Checked = false;
            txbHora.Text = string.Empty;
        }
        else
        {
            rbtBiometrico.Checked = false;
            rbtMCS.Checked = true;
            txbHora.Text = objHorario.horTiempoMCS.ToString();

        }
        if (objHorario.horVigente == true)
        {
            ckbVigente.Checked = true;
            ckbVigente.Enabled = true;
        }
        else
        {
            ckbVigente.Checked = false;
            ckbVigente.Enabled = false;
        }

        if (objHorario.horFinesSemana == true)
        {
            ckbFinesSemana.Checked = true;
         
        }
        else
        {
            ckbFinesSemana.Checked = false;
           
        }
       
        deshabilitaCampos();
        Session["idHorario"]=objHorario.idHorario;
        if (objHorario.horHoraEntradaL ==DateTime.MinValue)
        {
            ckbLunes.Checked = false;
                txbEntL.Text=string.Empty;
            txbSaL.Text=string.Empty;
        }
        else
        {
            ckbLunes.Checked=true;
            txbEntL.Text = objHorario.horHoraEntradaL.ToString("HH:mm");
            txbSaL.Text = objHorario.horHoraSalidaL.ToString("HH:mm");

        }

        if (objHorario.horHoraEntradaM == DateTime.MinValue)
        {
            ckbMartes.Checked = false;
            txbEntM.Text = string.Empty;
            txbEntSaM.Text = string.Empty;
        }
        else
        {
            ckbMartes.Checked = true;
            txbEntM.Text = objHorario.horHoraEntradaM.ToString("HH:mm");
            txbEntSaM.Text = objHorario.horHoraSalidaM.ToString("HH:mm");

        }

        if (objHorario.horHoraEntradaMi == DateTime.MinValue)
        {
            ckbMiercoles.Checked = false;
            txbEntMi.Text = string.Empty;
            txbSaMi.Text = string.Empty;
        }
        else
        {
            ckbMiercoles.Checked = true;
            txbEntMi.Text = objHorario.horHoraEntradaMi.ToString("HH:mm");
            txbSaMi.Text = objHorario.horHoraSalidaMi.ToString("HH:mm");

        }

        if (objHorario.horHoraEntradaJue == DateTime.MinValue)
        {
            ckbJue.Checked = false;
            txbEntJu.Text = string.Empty;
            txbSaJu.Text = string.Empty;
        }
        else
        {
            ckbJue.Checked = true;
            txbEntJu.Text = objHorario.horHoraEntradaJue.ToString("HH:mm");
            txbSaJu.Text = objHorario.horHoraSalidaJue.ToString("HH:mm");

        }

        if (objHorario.horHoraEntradaVie == DateTime.MinValue)
        {
            ckbViernes.Checked = false;
            txtbEntVi.Text = string.Empty;
            txtbSalVi.Text = string.Empty;
        }
        else
        {
            ckbViernes.Checked = true;
            txtbEntVi.Text = objHorario.horHoraEntradaVie.ToString("HH:mm");
            txtbSalVi.Text = objHorario.horHoraSalidaVie.ToString("HH:mm");

        }

        if (objHorario.horHoraEntradaSa == DateTime.MinValue)
        {
            ckbSabado.Checked = false;
            txbEntSa.Text = string.Empty;
            txbSalSa.Text = string.Empty;
        }
        else
        {
            ckbSabado.Checked = true;
            txbEntSa.Text = objHorario.horHoraEntradaSa.ToString("HH:mm");
            txbSalSa.Text = objHorario.horHoraSalidaSa.ToString("HH:mm");

        }

        if (objHorario.horHoraEntradaDom == DateTime.MinValue)
        {
            ckbDomingo.Checked = false;
            txbEntDo.Text = string.Empty; 
            txbSalDo.Text = string.Empty;
        }
        else
        {
            ckbDomingo.Checked = true;
            txbEntDo.Text = objHorario.horHoraEntradaDom.ToString("HH:mm");
            txbSalDo.Text = objHorario.horHoraSalidaDom.ToString("HH:mm");

        }


      

    }

    public void deshabilitaCampos()
    {
        txbNombre.Enabled = false;
        txbDescripcion.Enabled = false;
        ddlTipoHorario.Enabled = false;
        ckbTolerancia.Enabled = false;
        ckbRetardos.Enabled = false;
        ckbComida.Enabled = false;
        txbHora.Enabled = false;
        txbComidaMin.Enabled = false;
        txbDescanso.Enabled = false;
        txbEntDo.Enabled = false;
        txbEntJu.Enabled = false;
        txbEntL.Enabled = false;
        txbEntM.Enabled = false;
        txbEntMi.Enabled = false;
        txbEntSa.Enabled = false;
        txbEntSaM.Enabled = false;
        txbFechaIni.Enabled = false;
        txbHoraFin.Enabled = false;
        txbHoraIni.Enabled = false;
        txbRetardo.Enabled=false;
        txbSaJu.Enabled = false;
        txbSaL.Enabled = false;
        txbSalDo.Enabled = false;
        txbSalSa.Enabled = false;
        txbSaMi.Enabled = false;
        txbTolerancia.Enabled = false;
        txbTrabajo.Enabled = false;
        txtbEntVi.Enabled = false;
        txtbSalVi.Enabled = false;
        ckbDiasf.Enabled = false;
        ckbDomingo.Enabled = false;
        ckbES.Enabled = false;
        ckbJue.Enabled = false;
        ckbLunes.Enabled = false;
        ckbMartes.Enabled = false;
        ckbMiercoles.Enabled = false;
        ckbRetardos.Enabled = false;
        ckbSabado.Enabled = false;
        ckbTolerancia.Enabled = false;
        ckbViernes.Enabled = false;
        ckbFinesSemana.Enabled = false;
        rbtBiometrico.Enabled = false;
        rbtMCS.Enabled = false;
        ckbAbierto.Enabled = false;
        ddlTipoHorarioR.Enabled = false;
       
        
    }

    public void habilitaCampos()
    {
        txbNombre.Enabled = true;
        txbDescripcion.Enabled = true;
        ddlTipoHorario.Enabled = true;
        ckbTolerancia.Enabled = true;
        ckbRetardos.Enabled = true;
        ckbComida.Enabled = true;
        txbHora.Enabled = true;
        txbComidaMin.Enabled = true;
        txbDescanso.Enabled = true;
        txbFechaIni.Enabled = true;
        txbTrabajo.Enabled = true;
        ckbDiasf.Enabled = true;
        ckbDomingo.Enabled = true;
        ckbES.Enabled = true;
        ckbJue.Enabled = true;
        ckbLunes.Enabled = true;
        ckbMartes.Enabled = true;
        ckbMiercoles.Enabled = true;
        ckbRetardos.Enabled = true;
        ckbSabado.Enabled = true;
        ckbTolerancia.Enabled = true;
        ckbViernes.Enabled = true;
        rbtBiometrico.Enabled = true;
        rbtBiometrico.Checked = true;
        rbtMCS.Enabled = true;
        ckbFinesSemana.Enabled = true;
        ckbAbierto.Enabled = true;
        ddlTipoHorarioR.Enabled = true;
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Servicio/frmBuscarInstalacion.aspx");
    }
    protected void cmvValidador_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int multiplo = (Convert.ToInt16(args.Value) +Convert.ToInt16(txbTrabajo.Text))%24;
        if (multiplo == 0)
        {
            args.IsValid = true;
            
        }
        else
        {
            args.IsValid = false;
           

        }
    }
    
}
