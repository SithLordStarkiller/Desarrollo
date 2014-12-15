using System;
using System.Windows.Forms;

namespace Universidad.AplicacionAdministrativa
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                var configuracion = new GestionSesion();
                var sesion = configuracion.ExisteSesion();

                if (sesion == null)
                {
                    CreaWizard();
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FORM_Login(sesion));
                }
            }

            catch (System.Reflection.TargetInvocationException)
            {
                CreaWizard();
            }

            catch (UriFormatException)
            {
                CreaWizard();
            }

            catch (ArgumentNullException)
            {
                CreaWizard();
            }

            catch (Exception ex)
            {
                if (ex.Source == @"Newtonsoft.Json" ||
                    ex.Message == @"URI no válido: no se puede determinar el formato del URI.")
                {
                    CreaWizard();
                }
                else
                {
                    Error(ex);
                }
            }

        }

        public static void Error(Exception error)
        {
            Application.Run(new Excepcion(error));
        }

        public static void CreaWizard()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Wizard());
        }
    }
}
