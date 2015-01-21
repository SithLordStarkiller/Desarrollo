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
using AForge.Video;
using AForge.Video.DirectShow;

namespace Universidad.AplicacionAdministrativa.Controles.ControPersonas
{
    public partial class AltaPersona : UserControl
    {
        private readonly Sesion _sesion;
        private FilterInfoCollection _dispositivos;
        private VideoCaptureDevice _fuenteVideo;
        public AltaPersona(Sesion sesion)
        {
            _sesion = sesion;
            InitializeComponent();
        }

        private void AltaPersona_Load(object sender, EventArgs e)
        {
            dtpFechaNacimiento.MaxDate = DateTime.Now;
            dtpFechaNacimiento.MinDate = new DateTime(1850,1,1);

            cbxSexo.SelectedIndex = 0;

            rbImagen.Select();

            var servicio = new SVC_GestionCatalogos(_sesion);
            servicio.ObtenCatNacionalidad();
            servicio.ObtenCatNacionalidadFinalizado += servicios_ObtenCatNacionalidadFinalizado;

            servicio.ObtenCatTipoPersona();
            servicio.ObtenCatTipoPersonaFinalizado += servicio_ObtenCatTipoPersonaFinalizado;

            _dispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            ActualizaDispositivos();

        }

        private void ActualizaDispositivos()
        {

            foreach (FilterInfo item in _dispositivos)
            {
                cbxCamaraDisp.Items.Add(item.Name);
            }

            cbxCamaraDisp.SelectedIndex = 0;

            if (_dispositivos.Count < 0)
            {
                btnActivar.Enabled = false;
                btnDetener.Enabled = false;
                btnTomarFoto.Enabled = false;
                rbCamara.Enabled = false;
            }
            else
            {
                btnActivar.Enabled = true;
                btnDetener.Enabled = true;
                btnTomarFoto.Enabled = true;
                rbCamara.Enabled = true;
            }
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
            var fecha = Convert.ToDateTime(dtpFechaNacimiento.Value);
            tbxApellidoM.Text = fecha.ToString("d");
        }

        private void txbCodigoPostal_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (_dispositivos.Count >= 0)
            {
                _fuenteVideo = new VideoCaptureDevice(_dispositivos[cbxCamaraDisp.SelectedIndex].MonikerString);
                vspCamara.VideoSource = _fuenteVideo;
                vspCamara.Start();
            }
            
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            vspCamara.Stop();
        }
    }
}
