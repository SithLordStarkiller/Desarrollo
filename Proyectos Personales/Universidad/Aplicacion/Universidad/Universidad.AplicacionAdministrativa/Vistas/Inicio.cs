﻿using System;
using System.Windows.Forms;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Controlador.Login;
using Universidad.Entidades;

namespace Universidad.AplicacionAdministrativa.Vistas
{
    public partial class Inicio : Form
    {
        private readonly Form _padre;
        private readonly US_USUARIOS _usuario;

        public Inicio(Form padre,US_USUARIOS usuario)
        {
            _padre = padre;
            _usuario = usuario;
            InitializeComponent();
            CargarArbol(); 
        }

        private void CargarArbol()
        {
            TRV_Menu.Nodes.Add("Catalogos");

            TRV_Menu.Nodes[0].Nodes.Add("Net-informations.com");
            TRV_Menu.Nodes[0].Nodes[0].Nodes.Add("CLR");

            TRV_Menu.Nodes[0].Nodes.Add("Vb.net-informations.com");
            TRV_Menu.Nodes[0].Nodes[1].Nodes.Add("String Tutorial");
            TRV_Menu.Nodes[0].Nodes[1].Nodes.Add("Excel Tutorial");

            TRV_Menu.Nodes[0].Nodes.Add("Csharp.net-informations.com");
            TRV_Menu.Nodes[0].Nodes[2].Nodes.Add("ADO.NET");
            TRV_Menu.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("Dataset");
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            var persona = new SVC_LoginAdministrativos();


            persona.ObtenNombreCompleto(_usuario);
            persona.ObtenNombreCompletoFinalizado += Persona_ObtenNombreCompletoFinalizado; 

            if (_usuario.ID_TIPO_USUARIO == null) return;
            var tipoUsuario = SVC_GestionCatalogos.ClassInstance.ObtenTipoUsuario((int)_usuario.ID_TIPO_USUARIO);

            LBL_Tipo_Usuario.Text = tipoUsuario.TIPO_USUARIO;
        }

        private void Persona_ObtenNombreCompletoFinalizado(PER_PERSONAS usuario)
        {
            LBL_Nombre_Usuario.Text = usuario.NOMBRE_COMPLETO;
        }

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            _padre.Close();
        }
    }
}
