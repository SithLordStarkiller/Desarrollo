using System;
using System.Globalization;
using SICOGUA.Seguridad;
using SICOGUA.Entidades;
using proUtilerias;

public partial class Reportes_frmParteNovedad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            covFecha.ValueToCompare = DateTime.Now.ToShortDateString();
            calFecha.SelectedDate = DateTime.Now;
            txbHora.Text = DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo);
            clsCatalogos.llenarCatalogo(ddlEnvia, "servicio.spGenerarReporteEmpleadoEnviaParteNovedad", "NOMBRE_COMPLETO", "idEmpleado", (clsEntSesion)Session["objSession" + Session.SessionID]);
            clsCatalogos.llenarCatalogo(ddlRecibe, "servicio.spGenerarReporteEmpleadoRecibeParteNovedad", "NOMBRE_COMPLETO", "idEmpleado", (clsEntSesion)Session["objSession" + Session.SessionID]);
        }
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        clsEntParteNovedad objNovedades = new clsEntParteNovedad();

        objNovedades.PnFecha = clsUtilerias.dtObtenerFecha(txbFecha.Text);
        if (!string.IsNullOrEmpty(txbHora.Text))
        {
            objNovedades.PnFecha = new DateTime(objNovedades.PnFecha.Year, objNovedades.PnFecha.Month, objNovedades.PnFecha.Day, Convert.ToInt32(txbHora.Text.Split(':')[0]), Convert.ToInt32(txbHora.Text.Split(':')[1]), Convert.ToInt32(txbHora.Text.Split(':')[2]));
        }
        if (!string.IsNullOrEmpty(ddlRecibe.SelectedValue))
        {
            objNovedades.IdEmpleadoRecibe = new Guid(ddlRecibe.SelectedValue);
        }
        if (!string.IsNullOrEmpty(ddlEnvia.SelectedValue))
        {
            objNovedades.IdEmpleadoReporte = new Guid(ddlEnvia.SelectedValue);
        }
        objNovedades.PnEntradaFuerza = txbEntrada.Text;
        objNovedades.PnSalidaFuerza = txbSalida.Text;
        objNovedades.PnCopia = txbCopia.Text;
        objNovedades.PnAltas = txbAltas.Text;
        objNovedades.PnBajas = txbBajas.Text;
        objNovedades.PnNotaFaltistasPrimerDia = txbNotaFaltistasPrimerDia.Text;
        objNovedades.PnNotaFaltistasSegundoDia = txbNotaFaltistasSegundoDia.Text;
        objNovedades.PnNotaFaltistasTercerDia = txbNotaFaltistasTercerDia.Text;
        objNovedades.PnNotaFaltistasCuartoDia = txbNotaFaltistasCuartoDia.Text;
        objNovedades.PnNotaRetardos = txbNotaRetardos.Text;
        objNovedades.PnNotaExceptuados = txbNotaExceptuados.Text;
        objNovedades.PnNotaPresentesPrimerDia = txbNotaPresentesPrimerDia.Text;
        objNovedades.PnNotaPresentesSegundoDia = txbNotaPresentesSegundoDia.Text;
        objNovedades.PnNotaPresentesTercerDia = txbNotaPresentesTercerDia.Text;
        objNovedades.PnNotaPresentesLicenciaMedica = txbNotaPresentesLicenciaMedica.Text;
        objNovedades.PnNotaLicenciasMedicas = txbNotaLicenciasMedicas.Text;
        objNovedades.PnNotaPresentesVacaciones = txbNotaPresentesVacaciones.Text;
        objNovedades.PnNotaVacaciones = txbNotaVacaciones.Text;
      
        Session["objNovedades" + Session.SessionID] = objNovedades;
        Response.Redirect("~/Reportes/frmRvParteNovedades.aspx");
    }

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
}
