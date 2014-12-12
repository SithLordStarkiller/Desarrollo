using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EscrituraArchivos.APP
{
    public partial class Form1 : Form
    {
        private List<string> _textBoxList;
        public Form1()
        {
            InitializeComponent();

        }

        private void btn_CreaDirectorio_Click(object sender, EventArgs e)
        {
            try
            {
                const string path = @"Datos";
                Directory.CreateDirectory(path);
                textBox1.Text = @"Se creo Carpeta Nuevo";
            }
            catch (Exception er)
            {
                MuestraExcepcion(er);
            }
        }

        private void MuestraExcepcion(Exception error)
        {
            textBox1.Text = string.Empty;
            textBox1.Text += Environment.NewLine + @"Fuente: " + error.Source;
            textBox1.Text += Environment.NewLine + @"Mensage: " + error.Message;
            textBox1.Text += Environment.NewLine + @"Excepcion: " + error.InnerException;
            textBox1.Text += Environment.NewLine + @"Pila: " + error.StackTrace;
        }

        private void btn_ListarArchivos_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;

            try
            {
                var listaArchivos = Directory.GetFiles(".");

                const string extencion = "*.exe";

                textBox1.Text = Environment.NewLine + @"Total de Archivo(s) " + extencion + @" es de :   " + listaArchivos.Length + Environment.NewLine;

                foreach (var line in listaArchivos.Select(item => Environment.NewLine + Directory.GetCurrentDirectory() + item))
                {
                    textBox1.Text += line;
                }

                //foreach (var item in listaArchivos)
                //{
                //    var line = Environment.NewLine + Directory.GetCurrentDirectory() + item;
                //    textBox1.Text += line;
                //}

            }
            catch (Exception er)
            {
                MuestraExcepcion(er);
            }
        }

        private void btn_GuardaTextoArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                _textBoxList = new List<string>();

                foreach (var item in textBox1.Lines)
                {
                    _textBoxList.Add(item);
                }

                TextWriter tw = new StreamWriter(Directory.GetCurrentDirectory() + "\\Datos\\" + "SavedList.txt");

                foreach (var s in _textBoxList)
                    tw.WriteLine(s);

                tw.Close();

            }
            catch (Exception er)
            {
                MuestraExcepcion(er);
            }
        }

        private void btn_LeerArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                using (var sr = new StreamReader(Directory.GetCurrentDirectory() + "\\Datos\\" + "SavedList.txt"))
                {
                    var line = sr.ReadToEnd();
                    textBox1.Text = line;
                }
            }
            catch (Exception err)
            {
                MuestraExcepcion(err);
            }
        }
    }
}
