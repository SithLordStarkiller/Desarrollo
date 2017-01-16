using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using PubliPayments.Entidades;

namespace PubliPayments.Reportes.ReporteGestionMovil
{
    public partial class Soluciones : XtraReport
    {
        private readonly string _delegacion;
        private readonly string _fechaCarga;
        private readonly string _despacho;
        private readonly string _supervisor;
        private readonly string _tipoFormulario;
                
        public Soluciones(string delegacion = "", string fechaCarga = "", string despacho = "", string supervisor = "",string tipoFormulario="")
        {
            InitializeComponent();
            xrPanel1.BackColor = Color.FromArgb(65, 51, 57);

            _delegacion = delegacion;
            _fechaCarga = fechaCarga;
            _despacho = despacho;
            _supervisor= supervisor;
            _tipoFormulario = tipoFormulario;

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

            xrLabel3.Text = @"Última Actualización: " + new EntGestionMovil().ObtenerFechaActualizacion();
        }

        DataTable TablaDatosSoluciones(string delegacion = "",string fechaCarga = "",string  despacho = "",string supervisor = "",string tipoFormulario="")
        {
            var lista = new EntGestionMovil().Soluciones(delegacion ?? "", fechaCarga ?? "", despacho ?? "", supervisor ?? "",tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn { ColumnName = "Estatus final" });
            

            

            for (var i = 2; i < 9; i++)
            {
                if (lista.Tables[1].Rows[0][i].ToString() != "0")
                {
                    var columna = new DataColumn { ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture)) };
                    tabla.Columns.Add(columna);
                    columnasCancelar += ((i - 2) + "|");
                }
            }
            var columna1 = new DataColumn { ColumnName = "Total" };
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["Estatus final"] = row["Valor"];
                
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["Estatus final"] = "Total";
            
            for (var d = 0; d < (listaColumnas.Length - 1); d++)
            {
                fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value ? "0" : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                totalFila2 += Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
            }
            totalFila2 += Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            return tabla;
        }

        private void Soluciones_BeforePrint(object sender, PrintEventArgs e)
        {
            Detail.Controls.Add(CreateXrTable());
        }

        public XRTable CreateXrTable()
        {
            var table = new XRTable { Borders = BorderSide.All };

            var titulosTabla = new string[7];
            titulosTabla[0] = "Móvil";
            titulosTabla[1] = "Sin Asignar";
            titulosTabla[2] = "Sin Asignar 2 Visita";
            titulosTabla[3] = "Sin asignar 3 Visita";
            titulosTabla[4] = "Válidas";
            titulosTabla[5] = "Válidas Aprobadas";
            titulosTabla[6] = "Válidas Sin Aprobar";

            var tablaValoresBd = TablaDatosSoluciones(_delegacion, _fechaCarga, _despacho, _supervisor, _tipoFormulario);
            table.BeginInit();

            var rowTitulos = new XRTableRow();
            for (var i = 0; i < tablaValoresBd.Columns.Count; i++)
            {
                var cell = new XRTableCell
                {
                    Text =
                        tablaValoresBd.Columns[i].Caption.IndexOf("Valor", StringComparison.Ordinal) != -1
                            ? titulosTabla[Convert.ToInt32(tablaValoresBd.Columns[i].Caption.Substring(5))]
                            : tablaValoresBd.Columns[i].Caption
                };
                
                rowTitulos.Cells.Add(cell);
                rowTitulos.BackColor = Color.FromArgb(130, 130, 130);
                rowTitulos.ForeColor = Color.FromArgb(255, 255, 255);
            }
            table.Rows.Add(rowTitulos);

            for (int i = 0; i < tablaValoresBd.Rows.Count; i++)
            {
                var row = new XRTableRow();
                int numColumnas = tablaValoresBd.Columns.Count;
                for (int j = 0; j < numColumnas; j++)
                {
                    var cell = new XRTableCell
                    {
                        Text = tablaValoresBd.Rows[i][j].ToString(),
                        CanShrink = true,
                        CanGrow = true
                    };
                    
                    row.Cells.Add(cell);
                }
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
    }
}
