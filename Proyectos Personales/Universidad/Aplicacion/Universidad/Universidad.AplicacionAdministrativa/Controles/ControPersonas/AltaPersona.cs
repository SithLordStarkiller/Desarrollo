﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private readonly SVC_GestionCatalogos _servicioCatalogos;

        private List<DIR_CAT_COLONIAS> _listaColonias;
        private List<DIR_CAT_ESTADO> _listaEstados;
        private List<DIR_CAT_DELG_MUNICIPIO> _listaMunicipios;

        public delegate void MunicipioCargado(int seleccion);
        private event MunicipioCargado MunicipiosCargados;

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private VideoCapabilities[] videoCapabilities;
        private VideoCapabilities[] snapshotCapabilities;

        public void MunicipiosCargadospAsync(int seleccion)
        {
            MunicipiosCargados(seleccion);
        }
        public AltaPersona(Sesion sesion)
        {
            _sesion = sesion;
            _servicioCatalogos = new SVC_GestionCatalogos(_sesion);
            InitializeComponent();
        }

        private void AltaPersona_Load(object sender, EventArgs e)
        {
            dtpFechaNacimiento.MaxDate = DateTime.Now;
            dtpFechaNacimiento.MinDate = new DateTime(1850, 1, 1);

            cbxSexo.SelectedIndex = 0;

            rbImagen.Select();

            _servicioCatalogos.ObtenCatNacionalidad();
            _servicioCatalogos.ObtenCatNacionalidadFinalizado += servicios_ObtenCatNacionalidadFinalizado;

            _servicioCatalogos.ObtenCatTipoPersona();
            _servicioCatalogos.ObtenCatTipoPersonaFinalizado += servicio_ObtenCatTipoPersonaFinalizado;

            _servicioCatalogos.ObtenCatEstados();
            _servicioCatalogos.ObtenCatEstadosFinalizado += _servicioCatalogos_ObtenCatEstadosFinalizado;

            cbxMunicipio.Enabled = false;
            cbxColonia.Enabled = false;


            //ActualizaDispositivos();
        }

        private void _servicioCatalogos_ObtenCatEstadosFinalizado(List<DIR_CAT_ESTADO> lista)
        {
            _listaEstados = lista;
            cbxEstado.ValueMember = "IDESTADO";
            cbxEstado.DisplayMember = "NOMBREESTADO";
            cbxEstado.DataSource = _listaEstados;
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
        }

        private void btnBuscarCp_Click(object sender, EventArgs e)
        {
            _servicioCatalogos.ObtenColoniasPorCpPersona(Convert.ToInt32(txbCodigoPostal.Text));
            _servicioCatalogos.ObtenColoniasPorCpFinalizado += _servicioCatalogos_ObtenColoniasPorCpFinalizado;
        }

        private void _servicioCatalogos_ObtenColoniasPorCpFinalizado(List<DIR_CAT_COLONIAS> lista)
        {
            _listaColonias = lista.OrderBy(r => r.NOMBRECOLONIA).ToList();

            cbxColonia.ValueMember = "IDCOLONIA";
            cbxColonia.DisplayMember = "NOMBRECOLONIA";
            cbxColonia.DataSource = _listaColonias;

            var estado = _listaColonias.First().IDESTADO;
            var municipio = _listaColonias.First().IDMUNICIPIO;
            this.MunicipiosCargadospAsync((int)municipio);
            cbxColonia.Enabled = true;
            cbxEstado.SelectedValue = estado;
            ActualizaMunicipio();
        }

        private void ActualizaMunicipio()
        {
            _servicioCatalogos.ObtenMunicipios(Convert.ToInt32(cbxEstado.SelectedValue));
            _servicioCatalogos.ObtenMunicipiosFinalizado += _servicioCatalogos_ObtenMunicipiosFinalizado;
        }

        private void ActualizaColonia()
        {
            _servicioCatalogos.ObtenColonias(Convert.ToInt32(cbxEstado.SelectedValue), Convert.ToInt32(cbxMunicipio.SelectedValue));
            _servicioCatalogos.ObtenColoniasFinalizado += _servicioCatalogos_ObtenColoniasFinalizado;
        }

        private void _servicioCatalogos_ObtenColoniasFinalizado(List<DIR_CAT_COLONIAS> lista)
        {
            _listaColonias = lista.OrderBy(r => r.NOMBRECOLONIA).ToList();
            cbxColonia.ValueMember = "IDCOLONIA";
            cbxColonia.DisplayMember = "NOMBRECOLONIA";
            cbxColonia.DataSource = _listaColonias;
            cbxColonia.Enabled = true;

            
        }

        private void AltaPersona_MunicipiosCargados(int seleccion)
        {
            cbxMunicipio.SelectedValue = seleccion;
        }

        private void _servicioCatalogos_ObtenMunicipiosFinalizado(List<DIR_CAT_DELG_MUNICIPIO> lista)
        {
            _listaMunicipios = lista;
            cbxMunicipio.ValueMember = "IDMUNICIPIO";
            cbxMunicipio.DisplayMember = "NOMBREDELGMUNICIPIO";
            cbxMunicipio.DataSource = _listaMunicipios;
            cbxMunicipio.Enabled = true;
            MunicipiosCargados += AltaPersona_MunicipiosCargados;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        #region WebCam

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

                btnActivar.Enabled = true;
                btnDetener.Enabled = true;
                btnTomarFoto.Enabled = true;
                rbCamara.Enabled = true;
            }
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


                vspCamara.VideoSource = videoDevice;
                vspCamara.Start();
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
            vspCamara.SignalToStop();
        }

        private void cbxCamaraDisp_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (videoDevices.Count != 0)
            {
                videoDevice = new VideoCaptureDevice(videoDevices[cbxCamaraDisp.SelectedIndex].MonikerString);
                EnumeratedSupportedFrameSizes(videoDevice);
            }
        }

        #endregion

        #region Validaciones
        private void txbNombre_Validating(object sender, CancelEventArgs e)
        {
            ValidaNombre(sender, e);
        }

        private void tbxApellidoP_Validating(object sender, CancelEventArgs e)
        {
            ValidaNombre(sender, e);
        }

        private void tbxApellidoM_Validating(object sender, CancelEventArgs e)
        {
            ValidaNombre(sender, e);
        }

        private void ValidaNombre(object sender, CancelEventArgs e)
        {
            var cadenaPermitida = new Regex(@"^[A-Za-z]*$");
            var textbox = (TextBox)sender;

            if (cadenaPermitida.IsMatch(textbox.Text) && textbox.Text != string.Empty && textbox.Text.Length <= 30)
            {
                erpError.SetError(textbox, "");
                erpCuidado.SetError(textbox, "");
                erpCorrecto.SetError(textbox, "Correcto");
            }
            else if (cadenaPermitida.IsMatch(textbox.Text) && textbox.Text.Length >= 30)
            {
                erpError.SetError(textbox, "");
                erpCuidado.SetError(textbox, "El texto ingresado es muy largo");
                erpCorrecto.SetError(textbox, "");
            }
            else if (!cadenaPermitida.IsMatch(textbox.Text))
            {
                erpError.SetError(textbox, "Solo se permiten letras");
                erpCuidado.SetError(textbox, "");
                erpCorrecto.SetError(textbox, "");
            }
        }

        private void tbxCurp_Validating(object sender, CancelEventArgs e)
        {
            var cadenaPermitida =
                new Regex(
                    "^[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}$");

            if (cadenaPermitida.IsMatch(tbxCurp.Text) && tbxCurp.Text != string.Empty)
            {
                erpError.SetError(tbxCurp, "");
                erpCuidado.SetError(tbxCurp, "");
                erpCorrecto.SetError(tbxCurp, "Correcto");
            }
            else if (string.IsNullOrEmpty(tbxCurp.Text))
            {
                erpError.SetError(tbxCurp, "");
                erpCuidado.SetError(tbxCurp, "Es recomendable ingresar el CURP");
                erpCorrecto.SetError(tbxCurp, "");
            }
            else if (!cadenaPermitida.IsMatch(tbxCurp.Text))
            {
                erpError.SetError(tbxCurp, "El Formato del CURP es incorreto");
                erpCuidado.SetError(tbxCurp, "");
                erpCorrecto.SetError(tbxCurp, "");
            }
        }

        private void tbxRfc_Validating(object sender, CancelEventArgs e)
        {
            var cadenaPermitida =
                new Regex("^[A-Z]{4}([0-9]{2})(1[0-2]|0[1-9])([0-3][0-9])([ -]?)([A-Z0-9]{3,4})$");

            if (cadenaPermitida.IsMatch(tbxRfc.Text) && tbxRfc.Text != string.Empty)
            {
                erpError.SetError(tbxRfc, "");
                erpCuidado.SetError(tbxRfc, "");
                erpCorrecto.SetError(tbxRfc, "Correcto");
            }
            else if (string.IsNullOrEmpty(tbxRfc.Text))
            {
                erpError.SetError(tbxRfc, "");
                erpCuidado.SetError(tbxRfc, "Es recomendable ingresar el RFC");
                erpCorrecto.SetError(tbxRfc, "");
            }
            else if (!cadenaPermitida.IsMatch(tbxRfc.Text))
            {
                erpError.SetError(tbxRfc, "El Formato del RFC es incorreto");
                erpCuidado.SetError(tbxRfc, "");
                erpCorrecto.SetError(tbxRfc, "");
            }
        }

        private void tbxNss_Validating(object sender, CancelEventArgs e)
        {
            var cadenaPermitida =
                new Regex("^[0-9]{11}$");

            if (cadenaPermitida.IsMatch(tbxNss.Text) && tbxNss.Text != string.Empty)
            {
                erpError.SetError(tbxNss, "");
                erpCuidado.SetError(tbxNss, "");
                erpCorrecto.SetError(tbxNss, "Correcto");
            }
            else if (string.IsNullOrEmpty(tbxNss.Text))
            {
                erpError.SetError(tbxNss, "");
                erpCuidado.SetError(tbxNss, "Es recomendable ingresar el Numero de seguro social");
                erpCorrecto.SetError(tbxNss, "");
            }
            else if (!cadenaPermitida.IsMatch(tbxNss.Text))
            {
                erpError.SetError(tbxNss, "El Formato del NSS es incorreto suele ser de once digitos");
                erpCuidado.SetError(tbxNss, "");
                erpCorrecto.SetError(tbxNss, "");
            }
        }

        #endregion

        private void cbxEstado_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ActualizaMunicipio();
        }

        private void cbxMunicipio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ActualizaColonia();
        }

        private void cbxColonia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var idColonia = (int)cbxColonia.SelectedValue;

            var colonia = _listaColonias.SingleOrDefault(r => r.IDCOLONIA == idColonia);
            if (colonia != null) txbCodigoPostal.Text = colonia.CODIGOPOSTAL.ToString();
        }
    }
}