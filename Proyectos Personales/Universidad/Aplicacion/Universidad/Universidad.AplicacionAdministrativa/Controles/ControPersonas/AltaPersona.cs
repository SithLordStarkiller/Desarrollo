using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Universidad.AplicacionAdministrativa.Vistas;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Controlador.Personas;
using Universidad.Entidades.ControlUsuario;
using Universidad.Entidades;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Universidad.AplicacionAdministrativa.Controles.ControPersonas
{

    public partial class AltaPersona : UserControl
    {
        #region Propiedades

        private Evento _eventos;
        private readonly Sesion _sesion;
        private readonly SVC_GestionCatalogos _servicioCatalogos;
        private readonly SvcPersonas _serviciosPersonas;

        private List<DIR_CAT_COLONIAS> _listaColonias;
        private List<DIR_CAT_ESTADO> _listaEstados;
        private List<DIR_CAT_DELG_MUNICIPIO> _listaMunicipios;

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private VideoCapabilities[] videoCapabilities;
        private VideoCapabilities[] snapshotCapabilities;

        private PER_PERSONAS _personaDatos;
        private DIR_DIRECCIONES _personaDireccion;
        private PER_CAT_TELEFONOS _personaTelefonos;
        private PER_MEDIOS_ELECTRONICOS _personaMediosElectronicos;
        private PER_FOTOGRAFIA _personaFotografia;
        private byte[] _fotografiaBinarios;
        private string _nombreFotografia;

        public AltaPersona(Sesion sesion)
        {
            _sesion = sesion;
            _servicioCatalogos = new SVC_GestionCatalogos(_sesion);
            _serviciosPersonas = new SvcPersonas(_sesion);
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
            rbCamara.Enabled = false;
            rbImagen.Enabled = false;
            btnCargarFotografia.Enabled = false;
            btnTomarFoto.Enabled = false;
            btnRegistrar.Enabled = false;

            ActualizaDispositivos();
        }

        #endregion

        #region Operaciones del Registro

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            var barraProgreso = (((StatusStrip)((Form)(this).Parent.Parent.Parent.GetContainerControl()).Controls["StatusStrip1"])).Items["tspProgreso"] as ToolStripProgressBar;
            var labelStatus = (((StatusStrip)((Form)(this).Parent.Parent.Parent.GetContainerControl()).Controls["StatusStrip1"])).Items["tsslInformacion"] as ToolStripStatusLabel;

            try
            {
                var msgResultado = MessageBox.Show(text: @"Esta seguro que decea guardar este usuario",
                          caption: @"Informacion al usuario", buttons: MessageBoxButtons.YesNo,
                          icon: MessageBoxIcon.Warning);

                switch (msgResultado)
                {
                    case DialogResult.Yes:

                        labelStatus.Text = @"Guardando Datos";
                        barraProgreso.Maximum = 100;
                        barraProgreso.Minimum = 1;
                        barraProgreso.Value = 1;
                        barraProgreso.Visible = true;

                        barraProgreso.Value = 10;
                        CapturaDatosPersona();

                        barraProgreso.Value = 20;
                        CapturaDireccionPersona();

                        barraProgreso.Value = 30;
                        CapturaTelefonosPersona();

                        barraProgreso.Value = 40;
                        CapturaMediosElectronicos();


                        barraProgreso.Value = 70;
                        _personaDatos = await _serviciosPersonas.InsertarPersona(_personaTelefonos, _personaMediosElectronicos,
                            _personaFotografia, _personaDatos, _personaDireccion);

                        barraProgreso.Value = 100;
                        MessageBox.Show(text: @"Se guardaron lo datos correctamente" + Environment.NewLine + @"La clave de identificacion es: " + _personaDatos.ID_PER_LINKID,
                          caption: @"Informacion al usuario", buttons: MessageBoxButtons.OK,
                          icon: MessageBoxIcon.Information);
                        Limpiar();
                        barraProgreso.Visible = false;
                        labelStatus.Text = string.Empty;
                        break;

                    case DialogResult.No:
                        break;
                }

                btnRegistrar.Enabled = true;

            }

            catch (Exception)
            {
                MessageBox.Show(text: @"Error al guardar datos",
                          caption: @"Error de usuario", buttons: MessageBoxButtons.OK,
                          icon: MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }


        private void Limpiar()
        {
            txbNombre.Text = string.Empty;
            tbxApellidoP.Text = string.Empty;
            tbxApellidoM.Text = string.Empty;
            tbxCurp.Text = string.Empty;
            tbxRfc.Text = string.Empty;
            tbxNss.Text = string.Empty;
            dtpFechaNacimiento.Value = DateTime.Now.Date;
            cbxNacionalidad.SelectedValue = 117;
            cbxTipoPersona.SelectedValue = 1;

            cbxEstado.SelectedValue = 1;
            cbxMunicipio.DataSource = null;
            cbxMunicipio.Enabled = false;
            cbxColonia.DataSource = null;
            cbxColonia.Enabled = false;
            txbCodigoPostal.Text = string.Empty;
            txbCodigoPostal.ForeColor = Color.Black;
            txbCalle.Text = string.Empty;
            tbxNoExt.Text = string.Empty;
            tbxNoInt.Text = string.Empty;
            tbxReferencias.Text = string.Empty;

            tbxTelCelPersonal.Text = string.Empty;
            tbxTelFijoDomicilio.Text = string.Empty;
            tbxTelFijoTrabajo.Text = string.Empty;
            tbxCelTrabajo.Text = string.Empty;
            tbxFax.Text = string.Empty;

            tbxCorreoUniversidad.Text = string.Empty;
            tbxCorreoUniversidad.ForeColor = Color.Black;
            txbCorreoPersonal.Text = string.Empty;
            tbxFacebook.Text = string.Empty;
            tbxTwitter.Text = string.Empty;

            btnCargarFotografia.Enabled = false;
            btnTomarFoto.Enabled = false;
            rbCamara.Enabled = rbImagen.Enabled = false;
            pcbFotografia.Image = null;

            btnRegistrar.Enabled = false;
        }

        private void CapturaDatosPersona()
        {
            _personaDatos = new PER_PERSONAS
            {
                NOMBRE = txbNombre.Text,
                A_PATERNO = tbxApellidoP.Text,
                A_MATERNO = tbxApellidoM.Text,
                FECHA_NAC = dtpFechaNacimiento.Value,
                SEXO = cbxSexo.Text,
                CURP = tbxCurp.Text,
                RFC = tbxRfc.Text,
                IMSS = tbxNss.Text,
                CVE_NACIONALIDAD = Convert.ToInt32(cbxNacionalidad.SelectedValue),
                ID_TIPO_PERSONA = Convert.ToInt32(cbxTipoPersona.SelectedValue)
            };
        }

        private void CapturaDireccionPersona()
        {
            _personaDireccion = new DIR_DIRECCIONES
            {
                IDESTADO = Convert.ToInt32(cbxEstado.SelectedValue),
                IDMUNICIPIO = Convert.ToInt32(cbxMunicipio.SelectedValue),
                IDCOLONIA = Convert.ToInt32(cbxColonia.SelectedValue),
                CALLE = txbCalle.Text,
                NOEXT = tbxNoExt.Text,
                NOINT = tbxNoInt.Text,
                REFERENCIAS = tbxReferencias.Text
            };
        }

        private void CapturaTelefonosPersona()
        {
            _personaTelefonos = new PER_CAT_TELEFONOS
            {
                TELEFONO_FIJO_DOMICILIO = tbxTelFijoDomicilio.Text,
                TELEFONO_FIJO_TRABAJO = tbxTelFijoTrabajo.Text,
                TELEFONO_CELULAR_PERSONAL = tbxTelCelPersonal.Text,
                TELEFONO_CELULAR_TRABAJO = tbxCelTrabajo.Text,
                FAX = tbxFax.Text
            };
        }

        private void CapturaMediosElectronicos()
        {
            _personaMediosElectronicos = new PER_MEDIOS_ELECTRONICOS
            {
                CORREO_ELECTRONICO_PERSONAL = txbCorreoPersonal.Text,
                CORREO_ELECTRONICO_UNIVERSIDAD = tbxCorreoUniversidad.Text + lblDominio.Text,
                FACEBOOK = tbxFacebook.Text,
                TWITTER = tbxTwitter.Text
            };
        }

        #endregion

        #region Operaciones Datos personales

        private void servicio_ObtenCatTipoPersonaFinalizado(List<PER_CAT_TIPO_PERSONA> lista)
        {
            cbxTipoPersona.ValueMember = "ID_TIPO_PERSONA";
            cbxTipoPersona.DisplayMember = "TIPO_PERSONA";
            cbxTipoPersona.DataSource = lista;
            cbxTipoPersona.SelectedValue = 1;
            _servicioCatalogos.ObtenCatTipoPersonaFinalizado -= servicio_ObtenCatTipoPersonaFinalizado;
        }

        private void servicios_ObtenCatNacionalidadFinalizado(List<PER_CAT_NACIONALIDAD> lista)
        {
            cbxNacionalidad.ValueMember = "CVE_NACIONALIDAD";
            cbxNacionalidad.DisplayMember = "NOMBRE_PAIS";
            cbxNacionalidad.DataSource = lista;
            cbxNacionalidad.SelectedValue = 117;
            _servicioCatalogos.ObtenCatNacionalidadFinalizado -= servicios_ObtenCatNacionalidadFinalizado;
        }


        #endregion

        #region Operaciones Direcciones

        private void _servicioCatalogos_ObtenCatEstadosFinalizado(List<DIR_CAT_ESTADO> lista)
        {
            _listaEstados = lista;
            cbxEstado.ValueMember = "IDESTADO";
            cbxEstado.DisplayMember = "NOMBREESTADO";
            cbxEstado.DataSource = _listaEstados;

            _servicioCatalogos.ObtenCatEstadosFinalizado -= _servicioCatalogos_ObtenCatEstadosFinalizado;
        }

        private void btnBuscarCp_Click(object sender, EventArgs e)
        {
            _servicioCatalogos.ObtenColoniasPorCpPersona(Convert.ToInt32(txbCodigoPostal.Text));
            _servicioCatalogos.ObtenColoniasPorCpFinalizado += _servicioCatalogos_ObtenColoniasPorCpFinalizado;
        }

        private void _servicioCatalogos_ObtenColoniasPorCpFinalizado(List<DIR_CAT_COLONIAS> lista)
        {
            if (!lista.Any())
            {
                MessageBox.Show(text: @"No se encontro el codigo postal",
                        caption: @"Informe de usuario", buttons: MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Error);
                txbCodigoPostal.ForeColor = Color.Red;
                btnRegistrar.Enabled = false;
            }

            else
            {
                _listaColonias = lista.OrderBy(r => r.NOMBRECOLONIA).ToList();
                var colonia = lista.OrderBy(r => r.NOMBRECOLONIA).ToList();

                cbxColonia.ValueMember = "IDCOLONIA";
                cbxColonia.DisplayMember = "NOMBRECOLONIA";
                _listaColonias.OrderBy(r => r.NOMBRECOLONIA);
                cbxColonia.DataSource = _listaColonias;

                var estado = colonia.First().IDESTADO;
                cbxEstado.SelectedIndex = (int)estado;
                var municipio = colonia.First().IDMUNICIPIO;

                _eventos = new Evento();
                _eventos.Inicia((int)municipio);
                _eventos.AlfinalizarActualizacion += Eventos_AlfinalizarActualizacion;

                cbxEstado.SelectedValue = estado;
                cbxColonia.Enabled = true;

                ActualizaMunicipio();

                txbCodigoPostal.ForeColor = Color.ForestGreen;
                btnRegistrar.Enabled = true;
            }


            _servicioCatalogos.ObtenColoniasPorCpFinalizado -= _servicioCatalogos_ObtenColoniasPorCpFinalizado;
        }

        private void ActualizaMunicipio()
        {
            _servicioCatalogos.ObtenMunicipios(Convert.ToInt32(cbxEstado.SelectedValue));
            _servicioCatalogos.ObtenMunicipiosFinalizado += _servicioCatalogos_ObtenMunicipiosFinalizado;
        }
        private void _servicioCatalogos_ObtenMunicipiosFinalizado(List<DIR_CAT_DELG_MUNICIPIO> lista)
        {
            _listaMunicipios = lista;
            cbxMunicipio.ValueMember = "IDMUNICIPIO";
            cbxMunicipio.DisplayMember = "NOMBREDELGMUNICIPIO";
            cbxMunicipio.DataSource = _listaMunicipios;
            cbxMunicipio.Enabled = true;
            if (_eventos != null)
                _eventos.Finalizado();

            _servicioCatalogos.ObtenMunicipiosFinalizado -= _servicioCatalogos_ObtenMunicipiosFinalizado;
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

            _servicioCatalogos.ObtenColoniasFinalizado -= _servicioCatalogos_ObtenColoniasFinalizado;
        }

        private void Eventos_AlfinalizarActualizacion(int cbx)
        {
            cbxMunicipio.SelectedValue = cbx;
        }

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
            txbCodigoPostal.ForeColor = Color.ForestGreen;
            btnRegistrar.Enabled = true;
        }

        #endregion

        #region Operacion Medios Electronicos

        private async void btnVerificarCorreo_Click(object sender, EventArgs e)
        {
            btnVerificarCorreo.Enabled = false;
            var existe = await _serviciosPersonas.ExisteCorreoUniversidad(tbxCorreoUniversidad.Text + lblDominio.Text);
            btnVerificarCorreo.Enabled = true;

            if (existe)
            {
                tbxCorreoUniversidad.ForeColor = Color.Red;

                MessageBox.Show(text: @"Ya hay un correo identico elige otro",
                        caption: @"Informe de usuario", buttons: MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Error);
                btnRegistrar.Enabled = false;
            }
            else
            {
                tbxCorreoUniversidad.ForeColor = Color.ForestGreen;

                MessageBox.Show(text: @"El correo electronico esta disponible",
                        caption: @"Informe de usuario", buttons: MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Information);
                btnRegistrar.Enabled = true;
            }
        }

        #endregion

        #region Operaciones Fotografia

        #region Cargar imagen

        private void btnBuscarImagen_Click(object sender, EventArgs e)
        {
            ofdRutaFotografia.ShowDialog();
        }

        private void ofdRutaFotografia_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                var ruta = ((OpenFileDialog)sender).FileName;
                tbxRutaImagen.Text = ruta;
                var imagen = Image.FromFile(ruta);
                pcbFotografia.Image = imagen;
                var archivoStream = new MemoryStream();
                pcbFotografia.Image.Save(archivoStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                _fotografiaBinarios = new byte[archivoStream.Length];

                btnCargarFotografia.Enabled = true;
                rbImagen.Enabled = true;
                rbImagen.Select();

                var diagonal = 0;
                int i;

                for (i = 0; i < ruta.Length; i++)
                {
                    var caracter = ruta[i];
                    if (caracter == '\\')
                        diagonal = i;
                }

                ruta = ruta.Remove(0, diagonal + 1);

                var punto = 0;

                for (i = 0; i < ruta.Length; i++)
                {
                    var caracter = ruta[i];
                    if (caracter == '.')
                        punto = i;
                }
                var extencion = ruta.Length - punto;

                _nombreFotografia = ruta.Remove(punto, extencion);

            }
            catch (Exception er)
            {
                MessageBox.Show(@"El archivo no es valido");
            }
        }

        private void btnCargarFotografia_Click(object sender, EventArgs e)
        {
            if (rbImagen.Enabled && rbImagen.Checked)
            {
                _personaFotografia = new PER_FOTOGRAFIA
                {
                    EXTENCION = System.Drawing.Imaging.ImageFormat.Jpeg.ToString(),
                    FOTOGRAFIA = _fotografiaBinarios,
                    LONGITUD = _fotografiaBinarios.Length,
                    NOMBRE = _nombreFotografia
                };
            }
            else if (rbCamara.Enabled && rbCamara.Checked)
            {

            }

            MessageBox.Show(text: rbImagen.Checked ? @"Se Guardo la imagen del Archivo" : @"Se Guardo la imagen de la camara",
                        caption: @"Informe de usuario", buttons: MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Information);
            btnRegistrar.Enabled = true;
        }

        #endregion

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
        private void btnActualizaDispositivos_Click(object sender, EventArgs e)
        {
            ActualizaDispositivos();
        }

        #endregion

        #endregion

        #region Validaciones

        #region Validacion Datos peronales

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
                btnRegistrar.Enabled = true;
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
                btnRegistrar.Enabled = false;
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
                btnRegistrar.Enabled = true;
            }
            else if (string.IsNullOrEmpty(tbxCurp.Text))
            {
                erpError.SetError(tbxCurp, "");
                erpCuidado.SetError(tbxCurp, "Es recomendable ingresar el CURP");
                erpCorrecto.SetError(tbxCurp, "");
                btnRegistrar.Enabled = true;
            }
            else if (!cadenaPermitida.IsMatch(tbxCurp.Text))
            {
                erpError.SetError(tbxCurp, "El Formato del CURP es incorreto");
                erpCuidado.SetError(tbxCurp, "");
                erpCorrecto.SetError(tbxCurp, "");
                btnRegistrar.Enabled = false;
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
                btnRegistrar.Enabled = true;
            }
            else if (string.IsNullOrEmpty(tbxRfc.Text))
            {
                erpError.SetError(tbxRfc, "");
                erpCuidado.SetError(tbxRfc, "Es recomendable ingresar el RFC");
                erpCorrecto.SetError(tbxRfc, "");
                btnRegistrar.Enabled = true;
            }
            else if (!cadenaPermitida.IsMatch(tbxRfc.Text))
            {
                erpError.SetError(tbxRfc, "El Formato del RFC es incorreto");
                erpCuidado.SetError(tbxRfc, "");
                erpCorrecto.SetError(tbxRfc, "");
                btnRegistrar.Enabled = false;
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
                btnRegistrar.Enabled = true;
            }
            else if (string.IsNullOrEmpty(tbxNss.Text))
            {
                erpError.SetError(tbxNss, "");
                erpCuidado.SetError(tbxNss, "Es recomendable ingresar el Numero de seguro social");
                erpCorrecto.SetError(tbxNss, "");
                btnRegistrar.Enabled = true;
            }
            else if (!cadenaPermitida.IsMatch(tbxNss.Text))
            {
                erpError.SetError(tbxNss, "El Formato del NSS es incorreto suele ser de once digitos");
                erpCuidado.SetError(tbxNss, "");
                erpCorrecto.SetError(tbxNss, "");
                btnRegistrar.Enabled = false;
            }
        }

        #endregion

        #region Validacion Direcciones

        private void txbCalle_Validating(object sender, CancelEventArgs e)
        {
            var textbox = (TextBox)sender;

            if (textbox.Text != string.Empty && textbox.Text.Length <= 100)
            {
                erpError.SetError(textbox, "");
                erpCuidado.SetError(textbox, "");
                erpCorrecto.SetError(textbox, "Correcto");
                btnRegistrar.Enabled = true;
            }
            else if (textbox.Text.Length >= 50)
            {
                erpError.SetError(textbox, "");
                erpCuidado.SetError(textbox, "El texto ingresado es muy largo");
                erpCorrecto.SetError(textbox, "");
                btnRegistrar.Enabled = false;
            }
            else if (string.IsNullOrEmpty(textbox.Text))
            {
                erpError.SetError(textbox, "Se debe ingresar la calle");
                erpCuidado.SetError(textbox, "");
                erpCorrecto.SetError(textbox, "");
                btnRegistrar.Enabled = false;
            }
        }

        private void tbxNoExt_Validating(object sender, CancelEventArgs e)
        {
            var textbox = (TextBox)sender;

            if (textbox.Text != string.Empty && textbox.Text.Length <= 10)
            {
                erpError.SetError(textbox, "");
                erpCuidado.SetError(textbox, "");
                erpCorrecto.SetError(textbox, "Correcto");
                btnRegistrar.Enabled = true;
            }

            else if (string.IsNullOrEmpty(textbox.Text))
            {
                erpError.SetError(textbox, "Se debe ingresar el No Exterior");
                erpCuidado.SetError(textbox, "");
                erpCorrecto.SetError(textbox, "");
                btnRegistrar.Enabled = false;
            }
        }

        private void tbxNoInt_Validating(object sender, CancelEventArgs e)
        {
            var textbox = (TextBox)sender;

            if (textbox.Text != string.Empty && textbox.Text.Length <= 10)
            {
                erpError.SetError(textbox, "");
                erpCuidado.SetError(textbox, "");
                erpCorrecto.SetError(textbox, "Correcto");
                btnRegistrar.Enabled = true;
            }

            else if (string.IsNullOrEmpty(textbox.Text))
            {
                erpError.SetError(textbox, "");
                erpCuidado.SetError(textbox,
                    "Es recomendable que se ingresar el No Interior" + Environment.NewLine +
                    "En caso de no contar con una puede dejarlo en blanco");
                erpCorrecto.SetError(textbox, "");
                btnRegistrar.Enabled = true;
            }
        }

        #endregion

        #region Telefonos

        private void tbxTelFijoDomicilio_Validating(object sender, CancelEventArgs e)
        {
            var cadenaPermitida = new Regex(@"\(?\d{2,3}\)?-? *\d{3}-? *-?\d{4}");
            ValidaTelefonos(sender, e, cadenaPermitida);
        }
        private void tbxTelFijoTrabajo_Validating(object sender, CancelEventArgs e)
        {
            var cadenaPermitida = new Regex(@"\(?\d{2,3}\)?-? *\d{3}-? *-?\d{4}");
            ValidaTelefonos(sender, e, cadenaPermitida);
        }

        private void tbxTelCelPersonal_Validating(object sender, CancelEventArgs e)
        {
            var cadenaPermitida = new Regex(@"\(?\d{2,3}\)?-? *\d{3}-? *-?\d{4}");
            ValidaTelefonos(sender, e, cadenaPermitida);
        }

        private void tbxCelTrabajo_Validating(object sender, CancelEventArgs e)
        {
            var cadenaPermitida = new Regex(@"\(?\d{2,3}\)?-? *\d{3}-? *-?\d{4}");
            ValidaTelefonos(sender, e, cadenaPermitida);
        }

        private void tbxFax_Validating(object sender, CancelEventArgs e)
        {
            var cadenaPermitida = new Regex(@"\(?\d{2,3}\)?-? *\d{3}-? *-?\d{4}");
            ValidaTelefonos(sender, e, cadenaPermitida);
        }

        private void ValidaTelefonos(object sender, CancelEventArgs e, Regex cadenaPermitida)
        {
            var textbox = (TextBox)sender;

            if (cadenaPermitida.IsMatch(textbox.Text))
            {
                erpError.SetError(textbox, "");
                erpCuidado.SetError(textbox, "");
                erpCorrecto.SetError(textbox, "Correcto");
                btnRegistrar.Enabled = true;
            }
            else if (textbox.Text.Length == 0)
            {
                erpError.SetError(textbox, "");
                erpCuidado.SetError(textbox, "Es recomendable ingresar un telefono");
                erpCorrecto.SetError(textbox, "");
                btnRegistrar.Enabled = true;
            }
            else if (!cadenaPermitida.IsMatch(textbox.Text))
            {
                erpError.SetError(textbox, "Solo se permiten letras");
                erpCuidado.SetError(textbox, "");
                erpCorrecto.SetError(textbox, "");
                btnRegistrar.Enabled = false;
            }
        }

        #endregion

        #region Validacion Medios Electronicos

        private void txbCorreoPersonal_Validating(object sender, CancelEventArgs e)
        {
            var cadenaPermitida = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var textbox = (TextBox)sender;

            if (cadenaPermitida.IsMatch(textbox.Text))
            {
                erpError.SetError(textbox, "");
                erpCuidado.SetError(textbox, "");
                erpCorrecto.SetError(textbox, "Correcto");
                btnRegistrar.Enabled = true;
            }
            else if (textbox.Text.Length == 0)
            {
                erpError.SetError(textbox, "");
                erpCuidado.SetError(textbox, "Es recomendable ingresar el correo electronico personal");
                erpCorrecto.SetError(textbox, "");
                btnRegistrar.Enabled = true;
            }
            else if (!cadenaPermitida.IsMatch(textbox.Text))
            {
                erpError.SetError(textbox, "El correo no es valido");
                erpCuidado.SetError(textbox, "");
                erpCorrecto.SetError(textbox, "");
                btnRegistrar.Enabled = false;
            }
        }

        #endregion

        #endregion
    }
}