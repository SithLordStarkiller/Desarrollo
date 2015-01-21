using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Entidades.ControlUsuario;
using Universidad.Entidades;

namespace Universidad.AplicacionAdministrativa.Controles.ControPersonas
{
    public partial class AltaPersona : UserControl
    {
        private Sesion _sesion;
        public AltaPersona(Sesion sesion)
        {
            _sesion = sesion;
            InitializeComponent();
        }

        private void AltaPersona_Load(object sender, EventArgs e)
        {
            mcrFechaNacimiento.MaxDate = DateTime.Now;
            mcrFechaNacimiento.MinDate = new DateTime(1850,1,1);

            cbxSexo.SelectedIndex = 0;

            var servicio = new SVC_GestionCatalogos(_sesion);
            servicio.ObtenCatNacionalidad();
            servicio.ObtenCatNacionalidadFinalizado += servicios_ObtenCatNacionalidadFinalizado;

            servicio.ObtenCatTipoPersona();
            servicio.ObtenCatTipoPersonaFinalizado += servicio_ObtenCatTipoPersonaFinalizado; 
        }

        private void servicio_ObtenCatTipoPersonaFinalizado(List<PER_CAT_TIPO_PERSONA> lista)
        {
            cbxTipoPersona.ValueMember = "ID_TIPO_PERSONA";
            cbxTipoPersona.DisplayMember = "TIPO_PERSONA";
            cbxTipoPersona.DataSource = lista;
            cbxTipoPersona.SelectedValue = 1;
        }

        private void servicios_ObtenCatNacionalidadFinalizado(List<PER_CAT_NACIONALIDAD> lista)
        {
            cbxNacionalidad.ValueMember = "CVE_NACIONALIDAD";
            cbxNacionalidad.DisplayMember = "NOMBRE_PAIS";
            cbxNacionalidad.DataSource = lista;
            cbxNacionalidad.SelectedValue = 117;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            var fecha = Convert.ToDateTime(mcrFechaNacimiento.SelectionStart);
            tbxApellidoM.Text = fecha.ToString("d");
        }

        private void txbCodigoPostal_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
