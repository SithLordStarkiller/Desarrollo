using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Universidad.RespaldoCatalogosDb.LogicaNegocios;

namespace Universidad.RespaldoCatalogosDb.Vistas
{
    public partial class CargaDatos : Form
    {
        private List<String> _listaArchivos;
        private string _urlExcel;
        private readonly OleDbConnection _service;
        private readonly Form _padre;
        public CargaDatos(Form padre, SqlConnectionStringBuilder connectionString, List<String> listaArchivos, string urlDirectory)
        {
            InitializeComponent();
            _padre = padre;
            _listaArchivos = listaArchivos;
            _urlExcel = urlDirectory;
            var connection = new OleDbConnections(connectionString);
            _service = connection.Connect();
        }

        private void CargaDatos_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            foreach (var item in _listaArchivos)
            {
                ConectarExcel(item);
            }
        }

        private void ConectarExcel(string nombreArchivo)
        {
            try
            {

                var props = new Dictionary<string, string>();

                // XLSX - Excel 2007, 2010, 2012, 2013
                props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
                props["Extended Properties"] = "Excel 12.0 XML";
                props["Data Source"] = _urlExcel + "\\" + nombreArchivo;

                // XLS - Excel 2003 and Older
                //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
                //props["Extended Properties"] = "Excel 8.0";
                //props["Data Source"] = "C:\\MyExcel.xls";

                var sb = new StringBuilder();

                foreach (var prop in props)
                {
                    sb.Append(prop.Key);
                    sb.Append('=');
                    sb.Append(prop.Value);
                    sb.Append(';');
                }

                using (var conn = new OleDbConnection(sb.ToString()))
                {
                    conn.Open();

                    var hojas = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    var excelSheets = (new String[hojas.Rows.Count]);
                    var nombreHojas = new List<string>();

                    var i = 0; // Add the sheet name to the string array. 

                    foreach (DataRow row in hojas.Rows)
                    {
                        nombreHojas.Add(excelSheets[i] = row["TABLE_NAME"].ToString());
                        i++;
                    }

                    var tablas = new List<DataTable>();

                    using (var cmd = conn.CreateCommand())
                    {
                        foreach (var items in nombreHojas)
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM [" + items + "]";
                            var da = new OleDbDataAdapter(cmd);
                            var dt = new DataTable(items);
                            da.Fill(dt);
                            tablas.Add(dt);
                        }
                    }

                    var tabla = tablas.FirstOrDefault(r => r.TableName.Contains("$"));

                    var listaColumnas = (from object item in tabla.Columns select item.ToString()).ToList();

                    var queryColumnas = "";

                    foreach (var item in listaColumnas)
                    {
                        queryColumnas += " " + item + ",";
                    }

                    var nombreTabla = tabla.TableName;

                    nombreTabla = nombreTabla.Remove(nombreTabla.Length - 1);

                    queryColumnas = queryColumnas.Remove(queryColumnas.Length - 1);

                    var queryIdentityStart = "DBCC CHECKIDENT ('" + nombreTabla + "', RESEED, 0) SET IDENTITY_INSERT " + nombreTabla + " ON ";

                    var query = "INSERT INTO " + nombreTabla + " (" + queryColumnas + ") SELECT " + queryColumnas + " FROM OPENROWSET( 'Microsoft.ACE.OLEDB.12.0','Excel 12.0;Database="
                        + _urlExcel + "\\" + nombreArchivo + " ','SELECT " + queryColumnas + " FROM [" + nombreTabla + "$]')";

                    var queryIdentitySEnd = " SET IDENTITY_INSERT " + nombreTabla + " OFF";
                    try
                    {

                        var executeStart = new OleDbCommand(queryIdentityStart, conn);
                        executeStart.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        
                    }

                    try
                    {
                        var executeQuery = new OleDbCommand(query, conn);
                        executeQuery.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        var executeEnd = new OleDbCommand(queryIdentitySEnd, conn);
                        executeEnd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }

            }
            catch (Exception)
            {

            }

            //return sb.ToString();
        }

        private void CargaDatos_FormClosing(object sender, FormClosingEventArgs e)
        {
            _padre.Dispose();
        }
    }
}
