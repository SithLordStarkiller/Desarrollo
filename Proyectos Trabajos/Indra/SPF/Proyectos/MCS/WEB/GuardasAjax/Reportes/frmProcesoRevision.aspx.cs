using System;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;
using System.Web.UI;

public partial class Reportes_frmProcesoRevision : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            clsCatalogos.llenarCatalogoServicioPorUsuario(ddlServicio, "catalogo.spLeerServiciosPorUsuario", "serDescripcion", "idServicio", (clsEntSesion)Session["objSession" + Session.SessionID]);

          
        }
    }
    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        clsEntActaAdministrativa objProceso = new clsEntActaAdministrativa();


        objProceso.idServicio = Convert.ToInt32(ddlServicio.SelectedValue == "" ? 0 :Convert.ToInt32(ddlServicio.SelectedValue));
        objProceso.idInstalacion = Convert.ToInt32(ddlInstalacion.SelectedValue == "" ? 0 : Convert.ToInt32(ddlInstalacion.SelectedValue));

        objProceso.fechaActaPr = txbFechaPrimerActa.Text;
        objProceso.NoOficioPr = txbPrimerActaNoOficio.Text;
        objProceso.fechaOficioPr = txbPrimerActaFechaOficio.Text;
        objProceso.fechaCancelacion = txbCancelacionFecha.Text.Trim();


        objProceso.tipoBusquedaProceso =Convert.ToInt32(rblTipo.SelectedValue);

        objProceso.objRenuncia.fechaRenuncia = Convert.ToDateTime(txbFechaRenuncia.Text=="" ? Convert.ToDateTime("01/01/1900") :Convert.ToDateTime(txbFechaRenuncia.Text));
        objProceso.objRenuncia.noOficio = txbRenunciaNoOficio.Text;
        objProceso.objRenuncia.fechaOficio = Convert.ToDateTime(txbRenunciaFechaOficio.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txbRenunciaFechaOficio.Text));
        
        objProceso.movimiento =Convert.ToInt32(rblTipo.SelectedValue);

        if(cbPrimerActa.Checked==true) {objProceso.acta = 1;}
        if (cbCancelacion.Checked == true) { objProceso.acta = 2; }
        if (cbPrimerActa.Checked == true && cbCancelacion.Checked == true) { objProceso.acta = 3; }
        if (cbPrimerActa.Checked == false && cbCancelacion.Checked == false) { objProceso.acta = 3; }

        if (rbActa.Checked == true) { objProceso.proceso = 1; objProceso.columnaReporte = true; } else { objProceso.proceso = 2; objProceso.columnaReporte = false; }
 


        Session["objReporteRevision" + Session.SessionID] = objProceso;
        Response.Redirect("~/Reportes/frmRvProcesoRevision.aspx");


    }

    

    protected void ddlServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
 
        if (ddlServicio.SelectedIndex > 0)
        {
            clsCatalogos.llenarCatalogoInstalacionPorUsuario(ddlInstalacion, "catalogo.spLeerInstalacionesPorUsuario", "insNombre", "idInstalacion", Convert.ToInt32(ddlServicio.SelectedValue), (clsEntSesion)Session["objSession" + Session.SessionID]);
            ddlInstalacion.Enabled = true;


            if (rbRenuncias.Checked == true)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "renuncia", "activaRenunciaNoClean()", true);
            }
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "acta", "activaActaNoClean()", true);
      
        //}
      
            return;
        }
        ddlInstalacion.Items.Clear();
        ddlInstalacion.Enabled = false;
 


    }
    protected void ddlInstalacion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}