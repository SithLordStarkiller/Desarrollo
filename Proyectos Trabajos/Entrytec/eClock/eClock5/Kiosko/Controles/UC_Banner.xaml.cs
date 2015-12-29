using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

using System.Windows.Threading;
#if !NETFX_CORE
using System.Windows.Media.Animation;
#else
using Windows.UI.Xaml.Media.Animation;
#endif


namespace Kiosko.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Banner.xaml
    /// </summary>
    public partial class UC_Banner : UserControl
    {
        eClockBase.CeC_SesionBase m_Sesion = null;
        public bool FullScreen = false;
        public double Alto = 0;
        public UC_Banner()
        {
            InitializeComponent();
        }
        ~UC_Banner()
        {
            Timer.Stop();
        }

        public void MuestraFullScreen(double AltoTmp = 0)
        {
#if SinPublicidad
            return;
#endif
            if (FullScreen)
                return;

            if (Alto > 0 && Height != Alto)
                return;

            FullScreen = true;
            Alto = Height;
            Height = double.NaN;
            if (AltoTmp > 0)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = ActualHeight;
                da.To = AltoTmp;
                da.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(700));
                this.BeginAnimation(FrameworkElement.HeightProperty, da);
            }
        }
        public void Actualiza()
        {
            m_Sesion = eClock5.CeC_Sesion.ObtenSesion(this);
            eClockBase.Controladores.Publicidad Publicidad = new eClockBase.Controladores.Publicidad(m_Sesion);
            Publicidad.ObtenListadoEvent += Publicidad_ObtenListadoEvent;

            if (Width > Height || double.IsNaN(Width))
            {
                MuestraFullScreen();
                Publicidad.ObtenListado(1);
            }
            else
                Publicidad.ObtenListado(2);
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
#if SinPublicidad
            return;
#endif

            Img_Banner.MouseDown += Img_Banner_MouseDown;
            try
            {

                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                    return;
#if DEBUG
                //     return;
#endif
                Actualiza();

            }
            catch { }

        }



        eClockBase.Modelos.Model_PUBLICIDAD Actual = null;
        List<eClockBase.Modelos.Model_PUBLICIDAD> Listado = null;

        DispatcherTimer Timer = new DispatcherTimer();

        void Publicidad_ObtenListadoEvent(string Resultados)
        {
            Listado = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Model_PUBLICIDAD>>(Resultados);
            if (Actual == null)
                ObtenSiguiente();
        }
        bool Ejecutando = false;

        private void ObtenSiguiente()
        {
            try
            {
                if (Ejecutando)
                    return;
                // Ejecutando = true;
                if (Actual == null)
                    Actual = Listado.First();
                else
                {
                    int Pos = Listado.IndexOf(Actual);
                    if (Pos >= 0)
                    {
                        if (++Pos < Listado.Count)
                            Actual = Listado[Pos];
                        else
                            Actual = Listado[0];
                    }
                    else
                        Actual = Listado[0];
                }
                if (Actual.PUBLICIDAD == null)
                {
                    eClockBase.Controladores.Publicidad Publicidad = new eClockBase.Controladores.Publicidad(m_Sesion);
                    Publicidad.ObtenImagenEvent += Publicidad_ObtenImagenEvent;
                    Publicidad.ObtenImagen(Actual.PUBLICIDAD_ID);
                }
                else
                {
                    ActualizaImagen();
                }
                Timer = new DispatcherTimer();
                Timer.Tick += Timer_Tick;
                Timer.Interval = new TimeSpan(0, 0, Actual.PUBLICIDAD_SEGUNDOS);
                Timer.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }

        void Timer_Tick(object sender, EventArgs e)
        {

            ((DispatcherTimer)sender).Stop();
            ObtenSiguiente();
        }

        byte[] ImagenSiguiente = null;

        void Publicidad_ObtenImagenEvent(byte[] Imagen)
        {
            Actual.PUBLICIDAD = Imagen;
            ImagenSiguiente = Imagen;

            ActualizaImagen();
        }
        void ActualizaImagen()
        {
            if (ImagenSiguiente == null)
                return;
            System.IO.MemoryStream MS = new System.IO.MemoryStream(ImagenSiguiente);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = MS;
            bi.EndInit();
            Img_Banner.Source = bi;
            ImagenSiguiente = null;

        }


        void Img_Banner_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (FullScreen)
            {
                FullScreen = false;
                Img_Banner.Height = ActualHeight;
                DoubleAnimation da = new DoubleAnimation();
                da.From = this.ActualHeight;
                da.To = Alto;

                da.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(1000));

                this.BeginAnimation(FrameworkElement.HeightProperty, da);

            }
        }
    }
}
