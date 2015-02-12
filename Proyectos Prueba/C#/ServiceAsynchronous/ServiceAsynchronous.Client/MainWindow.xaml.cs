using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ServiceAsynchronous.Client.ServicioConsulta;

namespace ServiceAsynchronous.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnInvocar_Click(object sender, RoutedEventArgs e)
        {
            int _cantidadAsincronicos = int.Parse(txtCantidad.Text);
            int _cantidadSincronicos = int.Parse(txtSincronicos.Text);

            var _proxy = new Service1Client();
            object _estado = "No tiene relevancia ahora";
            var Task = Task<List<string>>.Factory.FromAsync(_proxy.BeginConsulta, _proxy.EndConsulta, _cantidadAsincronicos, _estado);

            //Mientras invocamos sincronicamente
            List<string> _resultado = _proxy.Consulta(_cantidadSincronicos);
            //Luego vamos por el resultado
            List<string> _resultadoAsincronico = Task.Result;
            //Cerramos el proxy
            _proxy.Close();
            lstItems.ItemsSource = _resultadoAsincronico;
            lstSincronicos.ItemsSource = _resultado;

        }
    }
}
