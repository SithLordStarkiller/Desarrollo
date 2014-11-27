﻿using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Forms.VisualStyles;
using EjemploTreeView.Clases;
using EjemploTreeView.Modelos;

namespace EjemploTreeView
{
    public partial class Form1 : Form
    {
        private List<CatComboBox> listViews;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargarTreeView_Click(object sender, EventArgs e)
        {
            trvMostrarJefes.Nodes.Clear();

            var gestionEmp = new GestionEmpleados();
            var lista = gestionEmp.NodoHijos(0);

            if (lista != null)
            {
                //Exprecion normal
                //foreach (var item in lista)
                //{
                //    var padre = new TreeNode(item.NombreEmpleado) { Tag = item };
                //    padre = InsertaHijo(item, padre);
                //    trvMostrarJefes.Nodes.Add(padre);
                //}
                foreach (var padre in from item in lista let padre = new TreeNode(item.NombreEmpleado) { Tag = item } select InsertaHijo(item, padre))
                {
                    trvMostrarJefes.Nodes.Add(padre);
                }
            }
            else
            {
                MessageBox.Show(@"No hay registros");
            }

        }

        private static TreeNode InsertaHijo(Empleados item, TreeNode padre)
        {
            var gestionEmp = new GestionEmpleados();

            var listaHijos = gestionEmp.NodoHijos(item.IdEmpleados);

            //Ejemplo sin linq
            //foreach (var subItem in listaHijos)
            //{
            //    if (subItem.IdJeje != 0)
            //    {
            //        var hijo = new TreeNode(subItem.NombreEmpleado);
            //        hijo = InsertaHijo(subItem, hijo);
            //        padre.Nodes.Add(hijo);
            //    }
            //}

            foreach (var hijo in (from subItem in listaHijos
                                  where subItem.IdJeje != 0
                                  let hijo = new TreeNode(subItem.NombreEmpleado) { Tag = subItem }
                                  select InsertaHijo(subItem, hijo)).Where(hijo => hijo != null))
            {
                padre.Nodes.Add(hijo);
            }
            return padre;
        }

        private void btnCargarGrid_Click(object sender, EventArgs e)
        {
            var empleados = new GestionEmpleados().ListaEmpleados;
            dgvEmpleados.DataSource = empleados;
        }

        private void trvMostrarJefes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //var a = ((TreeView)sender).SelectedNode.Text;
            //var b = ((TreeView)sender).Name;
            var c = ((Empleados)((TreeView)sender).SelectedNode.Tag);
            textBox1.Text = c.NombreEmpleado;
            textBox2.Text = c.IdEmpleados.ToString(CultureInfo.InvariantCulture);
            textBox3.Text = c.IdJeje.ToString(CultureInfo.InvariantCulture);
            textBox4.Text = ((TreeView)sender).SelectedNode.Tag.GetType().ToString();
            textBox5.Text = ((TreeView)sender).SelectedNode.FullPath;
        }

        private void btnMuestraTab_Click(object sender, EventArgs e)
        {
            var tabPage = new TabPage("Nueva Pestaña") {BackColor = Color.White};
            var contro1 = new UserControl1();
            tabPage.Controls.Add(contro1);
            tbcPrincipal.TabPages.Add(tabPage);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarListView();
        }

        private void CargarListView()
        {
            var gestionCatListView = new GestionComboBox();
            listViews = gestionCatListView.ListaCatComboBox;
            comboBox1.DisplayMember = "NombrePropiedad";
            comboBox1.ValueMember = "IdItem";
            comboBox1.DataSource = listViews;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var valueMember = ((ComboBox)sender).SelectedValue;
            var displayMember = ((ComboBox) sender).Text;

            txbValueMember.Text = valueMember.ToString();
            txbDisplayMember.Text = displayMember;
            var gestionTabPages = new GestionTabPages();
            var listaTabPages = gestionTabPages.ObtenTabPages((int)valueMember);

        }

        private void CreaTabPages(List<CatTabPages> listaTabPages)
        {
            var listaPages = new List<TabPage>();

            foreach (var items in listaTabPages)
            {
                var tabPageitem = new TabPage(items.NombreTabPage) {BackColor = Color.Wheat};
                var Control = new UserControl();
            }
            
            
            var tabPage = new TabPage() { BackColor = Color.White };
            var contro1 = new UserControl1();
            tabPage.Controls.Add(contro1);
            tbcPrincipal.TabPages.Add(tabPage);
        }
    }
}