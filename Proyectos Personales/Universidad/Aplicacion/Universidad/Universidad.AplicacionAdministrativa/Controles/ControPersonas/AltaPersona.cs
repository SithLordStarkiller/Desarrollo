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

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private VideoCapabilities[] videoCapabilities;
        private VideoCapabilities[] snapshotCapabilities;
        //private SnapshotForm _snapshotForm;


        public AltaPersona(Sesion sesion)
        {
            _sesion = sesion;
            InitializeComponent();
        }

        private void AltaPersona_Load(object sender, EventArgs e)
        {
            dtpFechaNacimiento.MaxDate = DateTime.Now;
            dtpFechaNacimiento.MinDate = new DateTime(1850, 1, 1);

            cbxSexo.SelectedIndex = 0;

            rbImagen.Select();

            var servicio = new SVC_GestionCatalogos(_sesion);
            servicio.ObtenCatNacionalidad();
            servicio.ObtenCatNacionalidadFinalizado += servicios_ObtenCatNacionalidadFinalizado;

            servicio.ObtenCatTipoPersona();
            servicio.ObtenCatTipoPersonaFinalizado += servicio_ObtenCatTipoPersonaFinalizado;


            //ActualizaDispositivos();

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count != 0)
            {
                // add all devices to combo
                foreach (FilterInfo device in videoDevices)
                {
                    cbxCamaraDisp.Items.Add(device.Name);
                }
            }
            else
            {
                cbxCamaraDisp.Items.Add("No DirectShow devices found");
            }

            cbxCamaraDisp.SelectedIndex = 0;


        }

        private void ActualizaDispositivos()
        {

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count < 0)
            {

                btnActivar.Enabled = false;
                btnDetener.Enabled = false;
                btnTomarFoto.Enabled = false;
                rbCamara.Enabled = false;
            }
            else
            {
                foreach (FilterInfo item in videoDevices)
                {
                    cbxCamaraDisp.Items.Add(item.Name);
                }

                cbxCamaraDisp.SelectedIndex = 0;

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
            if (videoDevice != null)
            {
                if ((videoCapabilities != null) && (videoCapabilities.Length != 0))
                {
                    videoDevice.VideoResolution = videoCapabilities[cbxResolucion.SelectedIndex];
                }

                if ((snapshotCapabilities != null) && (snapshotCapabilities.Length != 0))
                {
                    videoDevice.ProvideSnapshots = true;
                    videoDevice.SnapshotResolution = snapshotCapabilities[cbxSnapshot.SelectedIndex];
                    videoDevice.SnapshotFrame += new NewFrameEventHandler(videoDevice_SnapshotFrame);
                }


                vspCamara2.VideoSource = videoDevice;
                vspCamara2.Start();
            }
        }

        private void EnumeratedSupportedFrameSizes(VideoCaptureDevice videoDevice)
        {
            this.Cursor = Cursors.WaitCursor;

            cbxResolucion.Items.Clear();
            cbxSnapshot.Items.Clear();

            try
            {
                videoCapabilities = videoDevice.VideoCapabilities;
                snapshotCapabilities = videoDevice.SnapshotCapabilities;

                foreach (VideoCapabilities capabilty in videoCapabilities)
                {
                    cbxResolucion.Items.Add(string.Format("{0} x {1}",
                        capabilty.FrameSize.Width, capabilty.FrameSize.Height));
                }

                foreach (VideoCapabilities capabilty in snapshotCapabilities)
                {
                    cbxResolucion.Items.Add(string.Format("{0} x {1}",
                        capabilty.FrameSize.Width, capabilty.FrameSize.Height));
                }

                if (videoCapabilities.Length == 0)
                {
                    cbxResolucion.Items.Add("Not supported");
                }
                if (snapshotCapabilities.Length == 0)
                {
                    cbxSnapshot.Items.Add("Not supported");
                }

                cbxResolucion.SelectedIndex = 0;
                cbxSnapshot.SelectedIndex = 0;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void videoDevice_SnapshotFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Console.WriteLine(eventArgs.Frame.Size);

            ShowSnapshot((Bitmap)eventArgs.Frame.Clone());
        }

        private void ShowSnapshot(Bitmap snapshot)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Bitmap>(ShowSnapshot), snapshot);
            }
            else
            {
                //if (snapshotForm == null)
                //{
                //    snapshotForm = new SnapshotForm();
                //    snapshotForm.FormClosed += new FormClosedEventHandler(snapshotForm_FormClosed);
                //    snapshotForm.Show();
                //}

                //snapshotForm.SetImage(snapshot);
            }
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            vspCamara2.SignalToStop();
        }

        private void cbxCamaraDisp_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (videoDevices.Count != 0)
            {
                videoDevice = new VideoCaptureDevice(videoDevices[cbxCamaraDisp.SelectedIndex].MonikerString);
                EnumeratedSupportedFrameSizes(videoDevice);
            }
        }
    }
}
