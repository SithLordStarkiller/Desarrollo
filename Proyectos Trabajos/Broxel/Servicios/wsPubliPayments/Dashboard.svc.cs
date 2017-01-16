using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace wsPubliPayments
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Dashboard" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Dashboard.svc o Dashboard.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Dashboard : IDashboard
    {
        public List<IndicadorDashboard> ObtenerIndicadorJson(string token, List<String> valores, String tipoDashboard, String sUser, int nUser, int nRol, int nDominio,String indicador)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, nUser, "Dashboard.svc",
                "Metodo: ObtenerIndicadorJson,tipoDashboard: " + tipoDashboard + ", Indicador: " + indicador);

            if (ValidarToken(token, nUser.ToString(CultureInfo.InvariantCulture)))
            {
                var filtros = new FiltrosDashboard
                {
                    Delegacion = valores[0],
                    Despacho = valores[2],
                    Estado = valores[1],
                    Gestor = valores[4],
                    Supervisor = valores[3]
                };
                var modelo = new IndicadoresDashBoard(filtros, sUser, tipoDashboard, nUser, nRol, nDominio);

                List<IndicadorDashboard> lista = modelo.TablaIndicadores(indicador);

                return lista;
            }
            return null;
        }

        public List<IndicadorDashboard> ObtenerBloqueIndicadorJson(string token, List<String> valores, String tipoDashboard, String sUser, int nUser, int nRol, int nDominio, int parteTabla)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, nUser, "Dashboard.svc",
                "Metodo: ObtenerBloqueIndicadorJson,tipoDashboard: " + tipoDashboard + ", ParteTabla: " + parteTabla);

            if (ValidarToken(token, nUser.ToString(CultureInfo.InvariantCulture)))
            {
                var filtros = new FiltrosDashboard
                {
                    Delegacion = valores[0],
                    Despacho = valores[2],
                    Estado = valores[1],
                    Gestor = valores[4],
                    Supervisor = valores[3]
                };
                var modelo = new IndicadoresDashBoard(filtros, sUser, tipoDashboard, nUser, nRol, nDominio);

                List<IndicadorDashboard> lista = modelo.TablaBloqueIndicadores(parteTabla);

                return lista;
            }

            return null;   
        }

        public List<OpcionesFiltroDashboard> ObtenerListaFiltros(string token, String accion, List<String> valores,String usuario)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, Convert.ToInt32(usuario), "Dashboard.svc","Metodo: ObtenerListaFiltros,accion: " + accion);

            if (ValidarToken(token, usuario))
            {
                var filtros = new FiltrosDashboard
                {
                    Delegacion = valores[0],
                    Despacho = valores[2],
                    Estado = valores[1],
                    Gestor = valores[4],
                    Supervisor = valores[3]
                };
                var modelo = new ComboFiltroDashboard(filtros, accion);
                var lista = modelo.SelectFiltro();
                return lista;
            }
                return null;
        }

        public String Login(string sUser)
        {
            var ultimaCon = "";
            //fechaActual,usuario
            string nUser = Encoding.UTF8.GetString(Convert.FromBase64String(sUser));

            var datosToken = nUser.Split(',');

            Logger.WriteLine(Logger.TipoTraceLog.Trace,Convert.ToInt32(datosToken[1]), "Dashboard.svc","Metodo: Login");

            var context = new SistemasCobranzaEntities();

            var lista = from e in context.ObtenerUltimoLogin(Convert.ToInt32(datosToken[1]))
                select e;

            var obtenerUltimoLoginResult = lista.FirstOrDefault();
            if (obtenerUltimoLoginResult != null)
            {
                ultimaCon = datosToken[0] + "," + datosToken[1] + "," +
                            obtenerUltimoLoginResult.ultimoLogin.ToString(CultureInfo.InvariantCulture);
            }
            //Regresa fechaActual, usuario, fechaUltimoLogin
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(ultimaCon));
        }

        public Boolean ValidarToken(String token, string usuario)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, Convert.ToInt32(usuario), "Dashboard.svc","Metodo: ValidarToken");

            string nUser = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var datosToken = nUser.Split(',');

            if (usuario != datosToken[1])
            {
                return false;
            }

            var context = new SistemasCobranzaEntities();

            var obtenerUltimoLoginResult = context.ObtenerUltimoLogin(Convert.ToInt32(datosToken[1])).FirstOrDefault();

            if (obtenerUltimoLoginResult != null)
            {
                if (obtenerUltimoLoginResult.ultimoLogin.ToString(CultureInfo.InvariantCulture) != datosToken[2])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
