using System;
using System.IO;
using System.Security.Cryptography;
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

                if (string.IsNullOrEmpty(sesion.Conexion))
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

            catch (DirectoryNotFoundException)
            {
                CreaWizard();
            }

            catch (CryptographicException)
            {
                CreaWizard();
            }

            catch (Exception ex)
            {
                Error(ex);
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
