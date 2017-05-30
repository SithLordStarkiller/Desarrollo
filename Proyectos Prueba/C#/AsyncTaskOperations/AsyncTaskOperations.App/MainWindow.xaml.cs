using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace AsyncTaskOperations.App
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtContenido.Text="Press 1 to cancel task";
            var cTokenSource = new CancellationTokenSource();
            // Create a cancellation token from CancellationTokenSource
            var cToken = cTokenSource.Token;
            // Create a task and pass the cancellation token
            var t1 = Task<int>.Factory.StartNew(()
                => GenerateNumbers(cToken), cToken);

            // to register a delegate for a callback when a
            // cancellation request is made
            cToken.Register(() => cancelNotification());

            // If user presses 1, request cancellation.
            if (new Random().Next(3) == '1')
            {
                // cancelling task
                cTokenSource.Cancel();
            }
            Console.ReadLine();
        }

        static int GenerateNumbers(CancellationToken ct)
        {
            int i;
            for (i = 0; i < 10; i++)
            {
                Console.WriteLine("Method1 - Number: {0}", i);
                Thread.Sleep(1000);
                // poll the IsCancellationRequested property
                // to check if cancellation was requested
                if (ct.IsCancellationRequested)
                {
                    break;
                }

            }
            return i;
        }

        // Notify when task is cancelled
        static void cancelNotification()
        {
            Console.WriteLine("Cancellation request made!!");
        }

        private void txtContenido_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
