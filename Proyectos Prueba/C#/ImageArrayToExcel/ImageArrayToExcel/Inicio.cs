using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ImageArrayToExcel
{
    public partial class pan : Form
    {
        private byte[] _fotografia;
        private string _nombreFotografia;
        private string _extencionImagen;
        private string _cadena;
        public pan()
        {
            InitializeComponent();
        }

        private void pan_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscarImagen_Click(object sender, EventArgs e)
        {
            var fbd = new OpenFileDialog();
            fbd.ShowDialog();

            var fileList = fbd.FileName;

            txtRutaImagen.Text = fileList;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var rutaImagen = txtRutaImagen.Text;//@"C:\Users\Admin\Pictures\Descargas\Hello.gif";
                //txtRutaImagen.Text;
                var imagen = Image.FromFile(rutaImagen);
                pbxImagen.Image = imagen;
                pbxImagen.Select();
                
                using (var archivoStream = new MemoryStream())
                {
                    pbxImagen.Image.Save(archivoStream, imagen.RawFormat);
                    _fotografia = File.ReadAllBytes(rutaImagen);
                }

                var diagonal = 0;
                int i;

                for (i = 0; i < rutaImagen.Length; i++)
                {
                    var caracter = rutaImagen[i];
                    if (caracter == '\\')
                        diagonal = i;
                }

                rutaImagen = rutaImagen.Remove(0, diagonal + 1);

                var punto = 0;

                for (i = 0; i < rutaImagen.Length; i++)
                {
                    var caracter = rutaImagen[i];
                    if (caracter == '.')
                        punto = i;
                }
                var extencion = rutaImagen.Length - punto;

                _nombreFotografia = rutaImagen.Remove(punto, extencion);
                _extencionImagen = rutaImagen.Remove(0, punto + 1);

                try
                {
                    _cadena = GeneraCadenaBinario(_fotografia);

                    txtBinario.Text = _cadena;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Exception caught in process: {0}", ex.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string GeneraCadenaBinario(byte[] fotografia)
        {
            var cadena = "";

            for (var r = 1; r < fotografia.Length- 1 ; r++)
            {
                try
                {

                    var bytes = "[" + fotografia[r - 1] + "]";
                    cadena = cadena+bytes;
                }
                catch (Exception)
                {
                }
            }
            return cadena;
        }

        private void GeneraImagenPorCadena(string cadena,string extencion)
        {
            byte[] imagen;
            var contadorCadena = 1;
            var listaValoresArray = new List<string>();

            do
            {
                int i;

                for (i = 0; i < cadena.Length; i++)
                {
                    if (cadena[i].ToString() == "[")
                    {
                        i++;
                        var numero = "";

                        do
                        {
                            numero = numero + cadena[i];
                            i++;
                        } while (cadena[i].ToString() != "]");
                        listaValoresArray.Add(numero);
                    }
                    contadorCadena = i;
                }
                
            } while (contadorCadena == cadena.Length);

            imagen = new byte[listaValoresArray.Count];

            var contadorByte = 0;

            foreach (var item in listaValoresArray)
            {
                byte num = Convert.ToByte(item);
                imagen[contadorByte] = num;
                contadorByte++;
            }

            var str = new MemoryStream();
            str.Write(imagen, 0, imagen.Length);
            var bit = new Bitmap(str);

            //bit.Save(@"C:\imagen.gif", ImageFormat.Gif);

            pbxImagenBinario.Image = bit;
        }

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            GeneraImagenPorCadena(_cadena,_extencionImagen);
        }
    }
}
