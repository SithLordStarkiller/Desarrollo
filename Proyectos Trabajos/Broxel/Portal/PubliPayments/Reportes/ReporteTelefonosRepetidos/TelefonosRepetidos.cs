using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using PubliPayments.Controllers;
using PubliPayments.Entidades;

namespace PubliPayments.Reportes.ReporteTelefonosRepetidos
{
    public partial class TelefonosRepetidos : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly string _supervisor;
        private readonly string _despacho;
        private string _delegacion;
        

        public TelefonosRepetidos( string supervisor, string despacho, string delegacion, string tipoFormulario)
        {
            InitializeComponent();
            xrPanel1.BackColor = Color.FromArgb(65, 51, 57);

            
            _supervisor = supervisor;
            _despacho = despacho;
            _delegacion = delegacion;
            
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

            /*var tamanioAnterior = (PageWidth - Margins.Left - Margins.Right);
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
            xrLabel3.WidthF = tamanioPagHoriz;*/
        }

        private XRTable CreateXrTable()
        {
            var table = new XRTable { Borders = BorderSide.All };

            var tablaValoresBd = TablaDatosMaster();

            table.BeginInit();

            var titulosColumna = new[]
            {
                "Orden","Crédito", "Teléfono", "Gestor", "Supervisor", "Asociado A Crédito",              
            };
            var rowTitulos = new XRTableRow();

            for (int o = 0; o < titulosColumna.Count(); o++)
            {
                var celdaTitulo = new XRTableCell { Text = titulosColumna[o] };

                rowTitulos.Cells.Add(celdaTitulo);
            }
            rowTitulos.BackColor = Color.FromArgb(130, 130, 130);
            rowTitulos.ForeColor = Color.FromArgb(255, 255, 255);
            table.Rows.Add(rowTitulos);

            for (int i = 0; i < tablaValoresBd.Rows.Count;i++ )
            {
                var row = new XRTableRow();
                var celdaDatosOrden = new XRTableCell { Text = tablaValoresBd.Rows[i][0].ToString() };
                var celdaDatosCredito = new XRTableCell { Text = tablaValoresBd.Rows[i][1].ToString() };
                var celdaDatosTelefono = new XRTableCell { Text = tablaValoresBd.Rows[i][2].ToString() };
                var celdaDatosGestor = new XRTableCell { Text = tablaValoresBd.Rows[i][3].ToString() };
                var celdaDatosSupervisor = new XRTableCell { Text = tablaValoresBd.Rows[i][4].ToString() };
                var celdaDatosCredAnt = new XRTableCell { Text = tablaValoresBd.Rows[i][5].ToString() };

                row.Cells.Add(celdaDatosOrden);
                row.Cells.Add(celdaDatosCredito);
                row.Cells.Add(celdaDatosTelefono);
                row.Cells.Add(celdaDatosGestor);
                row.Cells.Add(celdaDatosSupervisor);
                row.Cells.Add(celdaDatosCredAnt);
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
            
        }

        private DataTable TablaDatosMaster()
        {
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol) == "5")
            {
                _delegacion =
                    new EntRankingIndicadores().ObtenerDelegacionUsuario(
                        SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario))[0];
            }

            string sql = "exec ObtenerReporteTelefonosDuplicados " +
                      "@idDominio = " + _despacho + "," +
                      "@idUsuarioPadre = " + _supervisor + "," +
                      "@delegacion = " + _delegacion + "," +
                      "@TipoFormulario = N'''" + CtrlComboFormulariosController.ObtenerModeloFormularioActivo(new HttpSessionStateWrapper(HttpContext.Current.Session)).Ruta + "'''";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            var tabla = ds.Tables[0];

            return tabla;
        }

        private void Detail_BeforePrint_1(object sender, PrintEventArgs e)
        {
            Detail.Controls.Add(CreateXrTable());
        }



    }
}
