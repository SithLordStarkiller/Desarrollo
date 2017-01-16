using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using PubliPayments.Controllers;
using PubliPayments.Entidades;

namespace PubliPayments.Reportes.ReporteAuditoriaImagenes
{
    public partial class AuditoriaImagenes : XtraReport
    {
        private readonly string _gestor;
        private readonly string _supervisor;
        private readonly string _despacho;
        private readonly string _dictamen;
        private readonly string _status;
        private  string _delegacion;
        private readonly string _autorizacion;
        private readonly string _conexionBd;
        private readonly string _valorOcr;        

        public AuditoriaImagenes(string gestor, string supervisor, string despacho, string dictamen, string status, string delegacion, string tipoFormulario,string autorizacion,string valorOcr,string conexionBd)
        {
            InitializeComponent();
            xrPanel1.BackColor = Color.FromArgb(65, 51, 57);
            
            _gestor=gestor;
            _supervisor=supervisor;
            _despacho=despacho;
            _dictamen=dictamen;
            _status=status;
            _delegacion=delegacion;
            _autorizacion = autorizacion;
            _conexionBd = conexionBd;
            _valorOcr = valorOcr;

            switch (Config.AplicacionActual().idAplicacion)
            {
                case 1:
                    xrLabel5.Text = @"Instituto del Fondo Nacional de la Vivienda para los Trabajadores (Infonavit)";
                    xrLabel2.Text = @"  ©2013  -  ";
                    xrPictureBox3.Visible = true;
                    break;
                default:
                    xrLabel5.Text = "";
                    xrLabel2.Text = "";
                    xrPictureBox3.Visible = false;
                    break;
            }

            var tamanioAnterior = (PageWidth - Margins.Left - Margins.Right);
            Landscape = true;
            var tamanioPagHoriz = PageWidth - Margins.Left - Margins.Right;
            var punto = (tamanioPagHoriz - tamanioAnterior);
            xrLabel3.Location = Point.Add(xrLabel3.Location, new Size(punto, 0));
            xrLabel6.Location = Point.Add(xrLabel6.Location, new Size(punto, 0));
            xrLabel7.Location = Point.Add(xrLabel7.Location, new Size(punto, 0));
            xrPictureBox4.Location = Point.Add(xrPictureBox4.Location, new Size(punto, 0));
            xrPanel1.WidthF = tamanioPagHoriz;
            xrPanel2.WidthF = tamanioPagHoriz;
            xrLabel1.WidthF = tamanioPagHoriz;
            labelFiltros.WidthF = tamanioPagHoriz;
            LabelIndicadorTitulo.WidthF = tamanioPagHoriz;
            xrLabel3.WidthF = tamanioPagHoriz;

        }

        private XRTable CreateXrTable()
        {
            var table = new XRTable { Borders = BorderSide.All };
            
            var tablaValoresBd = TablaDatosMaster();
            
            table.BeginInit();

            var titulosColumna = new[]
            {
                "Delegación", "Despacho", "Supervisor", "Gestor", "Crédito", "Dictamen", "Orden", 
                "Autorización","Imagen 1","Imagen 2", "Imagen 3", "Imagen 4", "Imagen 5", "Imagen 6",
                "Imagen 7", "Imagen 8", "Imagen 9","Imagen 10", "Resultado Auditoria"
            };
            var rowTitulos = new XRTableRow();

            for (int o = 0; o < titulosColumna.Count(); o++)
            {
                var celdaTitulo = new XRTableCell {Text = titulosColumna[o]};

                rowTitulos.Cells.Add(celdaTitulo);
            }
            rowTitulos.BackColor = Color.FromArgb(130, 130, 130);
            rowTitulos.ForeColor = Color.FromArgb(255, 255, 255);
            table.Rows.Add(rowTitulos);

            for (int i = 0; i < tablaValoresBd.Rows.Count; )
            {
                var row = new XRTableRow();
                var celdaDatosDel = new XRTableCell { Text = tablaValoresBd.Rows[i]["Delegacion"].ToString() };
                var celdaDatosDes = new XRTableCell { Text = tablaValoresBd.Rows[i]["NombreDominio"].ToString() };
                var celdaDatosSup = new XRTableCell { Text = tablaValoresBd.Rows[i]["Supervisor"].ToString() };
                var celdaDatosGes = new XRTableCell { Text = tablaValoresBd.Rows[i]["Gestor"].ToString() };
                var celdaDatosCre = new XRTableCell { Text = tablaValoresBd.Rows[i]["num_Cred"].ToString() };
                var celdaDictamen = new XRTableCell { Text = tablaValoresBd.Rows[i]["Valor"].ToString() };
                var celdaOrdenAsi = new XRTableCell { Text = tablaValoresBd.Rows[i]["idOrden"].ToString() };
                var celdaAutoriza = new XRTableCell { Text = tablaValoresBd.Rows[i]["tipoAutorizado"].ToString() };
                var celdaImag1 = new XRTableCell();
                var celdaImag2 = new XRTableCell();
                var celdaImag3 = new XRTableCell();
                var celdaImag4 = new XRTableCell();
                var celdaImag5 = new XRTableCell();
                var celdaImag6 = new XRTableCell();
                var celdaImag7 = new XRTableCell();
                var celdaImag8 = new XRTableCell();
                var celdaImag9 = new XRTableCell();
                var celdaImag10 = new XRTableCell();
                var celdaResultado = new XRTableCell
                {
                    Text = (tablaValoresBd.Rows[i]["resultadoGeneral"].ToString() == "0"
                    ? "No Autorizado"
                    : (tablaValoresBd.Rows[i][8].ToString() == "1" ? "Autorizado" : "Sin Auditar"))};

                var creditoActual = tablaValoresBd.Rows[i][1].ToString();
                i++;
                var imagen = 1;
                while (i < tablaValoresBd.Rows.Count && creditoActual == tablaValoresBd.Rows[i][1].ToString())
                {

                    switch (imagen)
                    {
                        case 1:
                            celdaImag1.Text = tablaValoresBd.Rows[i][2] + @" :: " + (tablaValoresBd.Rows[i][9].ToString() == "0"
                                ? "No Autorizado"
                                : (tablaValoresBd.Rows[i][9].ToString() == "1" ? "Autorizado" : "Sin Auditar"));
                            break;
                        case 2:
                            celdaImag2.Text = tablaValoresBd.Rows[i][2] + @" :: " + (tablaValoresBd.Rows[i][9].ToString() == "0"
                                ? "No Autorizado"
                                : (tablaValoresBd.Rows[i][9].ToString() == "1" ? "Autorizado" : "Sin Auditar"));
                            break;
                        case 3:
                            celdaImag3.Text = tablaValoresBd.Rows[i][2] + @" :: " + (tablaValoresBd.Rows[i][9].ToString() == "0"
                                ? "No Autorizado"
                                : (tablaValoresBd.Rows[i][9].ToString() == "1" ? "Autorizado" : "Sin Auditar"));
                            break;
                        case 4:
                            celdaImag4.Text = tablaValoresBd.Rows[i][2] + @" :: " + (tablaValoresBd.Rows[i][9].ToString() == "0"
                                ? "No Autorizado"
                                : (tablaValoresBd.Rows[i][9].ToString() == "1" ? "Autorizado" : "Sin Auditar"));
                            break;
                        case 5:
                            celdaImag5.Text = tablaValoresBd.Rows[i][2] + @" :: " + (tablaValoresBd.Rows[i][9].ToString() == "0"
                                ? "No Autorizado"
                                : (tablaValoresBd.Rows[i][9].ToString() == "1" ? "Autorizado" : "Sin Auditar"));
                            break;
                        case 6:
                            celdaImag6.Text = tablaValoresBd.Rows[i][2] + @" :: " + (tablaValoresBd.Rows[i][9].ToString() == "0"
                                ? "No Autorizado"
                                : (tablaValoresBd.Rows[i][9].ToString() == "1" ? "Autorizado" : "Sin Auditar"));
                            break;
                        case 7:
                            celdaImag7.Text = tablaValoresBd.Rows[i][2] + @" :: " + (tablaValoresBd.Rows[i][9].ToString() == "0"
                                ? "No Autorizado"
                                : (tablaValoresBd.Rows[i][9].ToString() == "1" ? "Autorizado" : "Sin Auditar"));
                            break;
                        case 8:
                            celdaImag8.Text = tablaValoresBd.Rows[i][2] + @" :: " + (tablaValoresBd.Rows[i][9].ToString() == "0"
                                ? "No Autorizado"
                                : (tablaValoresBd.Rows[i][9].ToString() == "1" ? "Autorizado" : "Sin Auditar"));
                            break;
                        case 9:
                            celdaImag9.Text = tablaValoresBd.Rows[i][2] + @" :: " + (tablaValoresBd.Rows[i][9].ToString() == "0"
                                ? "No Autorizado"
                                : (tablaValoresBd.Rows[i][9].ToString() == "1" ? "Autorizado" : "Sin Auditar"));
                            break;
                        case 10:
                            celdaImag10.Text = tablaValoresBd.Rows[i][2] + @" :: " + (tablaValoresBd.Rows[i][9].ToString() == "0"
                                ? "No Autorizado"
                                : (tablaValoresBd.Rows[i][9].ToString() == "1" ? "Autorizado" : "Sin Auditar"));
                            break;
                    }

                    i++;
                    imagen++;
                }

                row.Cells.Add(celdaDatosDel);
                row.Cells.Add(celdaDatosDes);
                row.Cells.Add(celdaDatosSup);
                row.Cells.Add(celdaDatosGes);
                row.Cells.Add(celdaDatosCre);
                row.Cells.Add(celdaDictamen);
                row.Cells.Add(celdaOrdenAsi);
                row.Cells.Add(celdaAutoriza);
                row.Cells.Add(celdaImag1);
                row.Cells.Add(celdaImag2);
                row.Cells.Add(celdaImag3);
                row.Cells.Add(celdaImag4);
                row.Cells.Add(celdaImag5);
                row.Cells.Add(celdaImag6);
                row.Cells.Add(celdaImag7);
                row.Cells.Add(celdaImag8);
                row.Cells.Add(celdaImag9);
                row.Cells.Add(celdaImag10);
                row.Cells.Add(celdaResultado);
                table.Rows.Add(row);
            }

            table.BeforePrint += table_BeforePrint;
            table.AdjustSize();
            table.EndInit();
            return table;
        }

        private void table_BeforePrint(object sender, PrintEventArgs e)
        {
            var table = ((XRTable)sender);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
            table.WidthF = PageWidth - Margins.Left - Margins.Right;
        }

        private void Detail_BeforePrint(object sender, PrintEventArgs e)
        {
            Detail.Controls.Add(CreateXrTable());
        }

        private DataTable TablaDatosMaster()
        {
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol) == "5")
            {
                _delegacion =
                    new EntRankingIndicadores().ObtenerDelegacionUsuario(
                        SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario))[0];
            }
            var ds = new Negocios.Auditoria().ObtenerTablaAuditoriaImagenes(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario), _delegacion, _despacho, _supervisor, _gestor, _dictamen, _status, _autorizacion, CtrlComboFormulariosController.ObtenerModeloFormularioActivo(new HttpSessionStateWrapper(HttpContext.Current.Session)).Ruta, _valorOcr, _conexionBd);
            var tabla = ds.Tables[0];

            return tabla;
        }

    }
}
