using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Controlador.Login;
using Universidad.Controlador.MenuSistema;
using Universidad.Controlador.Personas;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.AplicacionAdministrativa.Vistas
{
    public partial class Inicio : Form
    {
        private readonly Form _padre;
        private readonly US_USUARIOS _usuario;
        private readonly Sesion _sesion;
        private SvcGestionCatalogos _gestionCatalogos;
        private SvcPersonas _persona;
        private List<MenuSistemaE> _listaSistema;
        private List<SIS_AADM_ARBOLMENUS> _listaArbol;

        public Inicio(Form padre, US_USUARIOS usuario, Sesion sesion)
        {
            _padre = padre;
            _usuario = usuario;
            _sesion = sesion;

            InitializeComponent();
            CargarArbol();

            tspProgreso.Visible = false;
            tsslInformacion.Visible = false;
        }

        private void CargarArbol()
        {
            var menuSistema = new SvcMenuSistema(_sesion);
            menuSistema.MenuArbol();
            menuSistema.MenuSistema(_usuario);

            menuSistema.MenuArbolFinalizado += menuSistema_MenuArbolFinalizado;
            menuSistema.MenuSistemaFinalizado += menuSistema_MenuSistemaFinalizado;
        }

        void menuSistema_MenuArbolFinalizado(List<SIS_AADM_ARBOLMENUS> lista)
        {
            _listaArbol = lista;

            foreach (var item in _listaArbol)
            {
                var padre = new TreeNode(item.NOMBRENODO) { Tag = item };
                padre = InsertaHijo(item, padre);
                TRV_Menu.Nodes.Add(padre);
            }
        }

        void menuSistema_MenuSistemaFinalizado(List<MenuSistemaE> lista)
        {
            _listaSistema = lista;
        }

        private TreeNode InsertaHijo(SIS_AADM_ARBOLMENUS item, TreeNode padre)
        {

            var listaHijos = _listaArbol.Where(r => r.IDMENUPADRE == item.IDMENU);

            foreach (var subItem in listaHijos)
            {
                if (subItem.IDMENUPADRE != 0)
                {
                    var hijo = new TreeNode(subItem.NOMBRENODO);
                    hijo = InsertaHijo(subItem, hijo);
                    padre.Nodes.Add(hijo);
                }
            }

            return padre;
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            _gestionCatalogos = new SvcGestionCatalogos(_sesion);
            _persona = new SvcPersonas(_sesion);

            _persona.ObtenNombreCompleto(_usuario);
            _persona.ObtenNombreCompletoFinalizado += Persona_ObtenNombreCompletoFinalizado;

            if (_usuario.ID_TIPO_USUARIO == null) return;

            _gestionCatalogos.ObtenTipoUsuario((int)_usuario.ID_TIPO_USUARIO);
            _gestionCatalogos.ObtenTipoUsuarioFinalizado += gestioncatalogos_ObtenTipoUsuarioFinalizado;
        }

        void gestioncatalogos_ObtenTipoUsuarioFinalizado(US_CAT_TIPO_USUARIO tipoUsuario)
        {
            LBL_Tipo_Usuario.Text = tipoUsuario.TIPO_USUARIO;
            _gestionCatalogos.ObtenTipoUsuarioFinalizado += gestioncatalogos_ObtenTipoUsuarioFinalizado;
        }

        private void Persona_ObtenNombreCompletoFinalizado(PER_PERSONAS usuario)
        {
            LBL_Nombre_Usuario.Text = usuario.NOMBRE_COMPLETO;
        }

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            _padre.Close();
        }

        private void TRV_Menu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var c = ((SIS_AADM_ARBOLMENUS)((TreeView)sender).SelectedNode.Tag);
            var listaPage = _listaSistema.Where(a => a.IdMenuHijo == c.IDMENU && a.IdMenuPadre == c.IDMENUPADRE).ToList();
            tbcContenido.TabPages.Clear();

            foreach (var item in listaPage)
            {
                var control = new UserControl();

                switch (item.IdTabPage)
                {
                    case 6:
                        {
                            control = new Controles.ControPersonas.AltaPersona(_sesion);
                        }
                        break;
                    case 7:
                        {
                            control = new Controles.ControPersonas.EditarPersona();
                        }
                        break;
                }

                var tabPage = new TabPage(item.NombreTabPage) { BackColor = Color.White };
                tabPage.Controls.Add(control);
                tbcContenido.TabPages.Add(tabPage);
            }
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sesion.RecordarSesion = false;
            new GestionSesion().ActualizaArchivo(_sesion);
            Dispose();
            _padre.Close();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            _padre.Close();
        }
    }
}
