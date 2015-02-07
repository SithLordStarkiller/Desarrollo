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

namespace AppAsyncTask
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

        private Task<IEnumerable<String>> ObtenerArchivosAsync()
        {
            return Task.Run(() =>
            {
                System.Threading.Thread.Sleep(5000);
                var archivos = from archivo in
                                   System.IO.Directory.GetFiles(@"C:\Windows\System32")
                               select archivo;
                return archivos;
            });
        }

        private async void BtnLeerDatos_OnClick(object sender, RoutedEventArgs e)
        {
            BtnLeerDatos.IsEnabled = false;
            LbxListaArchivos.ItemsSource = await ObtenerArchivosAsync();
            BtnLeerDatos.IsEnabled = true;
        }

        private void BtnMensage_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Son las : " +
            DateTime.Now.ToLongTimeString());
        }
    }
}
