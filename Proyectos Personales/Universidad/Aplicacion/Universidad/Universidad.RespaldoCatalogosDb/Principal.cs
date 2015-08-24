using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Universidad.RespaldoCatalogosDb.Entidades;
using Universidad.RespaldoCatalogosDb.Vistas;

namespace Universidad.RespaldoCatalogosDb
{
    public partial class Principal : Form
    {
        public List<SqlServerInstacia> ListaServidores;
        public SqlConnectionStringBuilder SqlConnection;
        public string UrlExcels;

        public Principal()
        {
            InitializeComponent();
        }

        private void btnBuscarExcel_Click(object sender, EventArgs e)
        {
            try
            {

                var fbd = new FolderBrowserDialog();
                var result = fbd.ShowDialog();

                var fileList = (Directory.EnumerateFiles(fbd.SelectedPath, "*.xlsx", SearchOption.AllDirectories).Select(Path.GetFileName).ToList());

                txtUrlCarpetaExcel.Text = fbd.SelectedPath;

                UrlExcels = fbd.SelectedPath;

                clbListaExcel.DataSource = fileList;

            }
            catch (Exception)
            {
                MessageBox.Show(@"No se encontro ningun archivo");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            EnlistarInstaciasSqlServer();
            cbxDataBase.Enabled = false;
            txtConnectionString.Enabled = false;
        }

        private void ListarDataBase()
        {
            try
            {
                var databases = new List<String>();
                var connection = new SqlConnectionStringBuilder();
                var instacia = ListaServidores.Find(r => r.IdServidor == (int)cbxInstacias.SelectedValue);

                connection.DataSource = instacia.CompleteName;
                // enter credentials if you want
                connection.UserID = txtUsuario.Text;
                connection.Password = txtContrasena.Text;
                //connection.IntegratedSecurity = true;

                var strConn = connection.ToString();

                //create connection
                var sqlConn = new SqlConnection(strConn);

                //open connection
                sqlConn.Open();

                //get databases
                var tblDatabases = sqlConn.GetSchema("Databases");

                //close connection
                sqlConn.Close();

                //add to list
                foreach (DataRow row in tblDatabases.Rows)
                {
                    var dataBaseName = row["database_name"].ToString();
                    databases.Add(dataBaseName);
                }

                cbxDataBase.DisplayMember = "dataBaseName";
                cbxDataBase.DataSource = databases;
                cbxDataBase.Enabled = true;
            }
            catch (Exception)
            {
                //MessageBox.Show(@"Problema con la conexion");
            }
        }

        private void EnlistarInstaciasSqlServer()
        {
            var instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
            var dataTable = instance.GetDataSources();
            ListaServidores = new List<SqlServerInstacia>();
            var count = 1;

            foreach (var item in dataTable.Rows)
            {
                var servicio = new SqlServerInstacia
                {
                    IdServidor = count,
                    ServerName = ((DataRow)item)["ServerName"].ToString(),
                    InstaceName = ((DataRow)item)["InstanceName"].ToString(),
                    IsEncloustered = ((DataRow)item)["IsClustered"].ToString() != "Si",
                    Version = ((DataRow)item)["Version"].ToString(),
                    CompleteName = ((DataRow)item)["ServerName"] + "\\" + ((DataRow)item)["InstanceName"]
                };
                ListaServidores.Add(servicio);
                count++;
            }

            cbxInstacias.DisplayMember = "CompleteName";
            cbxInstacias.ValueMember = "IdServidor";
            cbxInstacias.DataSource = ListaServidores;
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            ListarDataBase();
        }

        private void txtContrasena_Leave(object sender, EventArgs e)
        {
            ListarDataBase();
        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
            try
            {
                var connection = new SqlConnectionStringBuilder();
                var instacia = ListaServidores.Find(r => r.IdServidor == (int)cbxInstacias.SelectedValue);

                connection.DataSource = instacia.CompleteName;
                connection.UserID = txtUsuario.Text;
                connection.Password = txtContrasena.Text;
                var strConn = connection.ToString();
                var sqlConn = new SqlConnection(strConn);
                sqlConn.Open();

                sqlConn.Close();

                MessageBox.Show(@"Conexion Exitosa", @"Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.InitialCatalog = cbxDataBase.SelectedItem.ToString();

                txtConnectionString.Text = connection.ToString();
                SqlConnection = connection;
            }
            catch (Exception)
            {
                MessageBox.Show(@"No se pudo relizar la conexion", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            EnlistarInstaciasSqlServer();
        }

        private void clbListaExcel_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var item = ((ListBox)sender);
            var existe = lbxSeleccionados.Items.Contains(item.Text);
            if (e.NewValue == CheckState.Unchecked)
            {
                lbxSeleccionados.Items.Remove(item.Text);
            }
            if (!existe)
            {
                lbxSeleccionados.Items.Add(item.Text);
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            var lista = (from object item in lbxSeleccionados.Items select item.ToString()).ToList();

            var form = new CargaDatos(SqlConnection, lista, UrlExcels);
            Hide();
            form.Show();
        }
    }
}
