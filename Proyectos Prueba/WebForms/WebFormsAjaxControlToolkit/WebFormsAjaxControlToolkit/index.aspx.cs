using System;
using System.Collections;


namespace WebFormsAjaxControlToolkit
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var alMeses = new ArrayList();
            var alAnualidades = new ArrayList();
            var dtFecha = new DateTime(2000, 1, 1);

            for (var nContador = 1; nContador <= 12; nContador++)
            {
                alMeses.Add(dtFecha.ToString("MMMM"));
                alAnualidades.Add(dtFecha.Year);
                dtFecha = dtFecha.AddMonths(1);
                dtFecha = dtFecha.AddYears(1);
            }


            ddlMeses.DataSource = alMeses;
            ddlMeses.DataBind();

            ddlAnualidades.DataSource = alAnualidades;
            ddlAnualidades.DataBind();
        }
    }
}