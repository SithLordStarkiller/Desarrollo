using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class GestionMovil
    {


        public DataSet GestionXSolucion(string delegacion, string fechaCarga, string estFinal, string tipoFormulario)
        {
            return new EntGestionMovil().ReporteGestionMovilGestionXSolucion(delegacion, fechaCarga, estFinal, "", "", tipoFormulario, 1);
        }

        public DataSet GestionXSolucionDespacho(string delegacion, string fechaCarga, string estFinal,string despacho, string tipoFormulario)
        {
            return new EntGestionMovil().ReporteGestionMovilGestionXSolucion(delegacion, fechaCarga, estFinal, despacho, "", tipoFormulario, 2);
        }

        public DataSet GestionXSolucionSupervisor(string delegacion, string fechaCarga, string estFinal, string despacho,string supervisor, string tipoFormulario)
        {
            return new EntGestionMovil().ReporteGestionMovilGestionXSolucion(delegacion, fechaCarga, estFinal, despacho,supervisor, tipoFormulario, 3);
        }
        public DataSet GestionXSolucionRdst(string delegacion, string fechaCarga, string estFinal, string tipoFormulario)
        {
            return new EntGestionMovil().ReporteGestionMovilGestionXSolucion(delegacion, fechaCarga, estFinal, "", "", tipoFormulario, 4);
        }

        public DataSet GestionXSolucionSupervisorRdst(string delegacion, string fechaCarga, string estFinal,string despacho, string tipoFormulario)
        {
            return new EntGestionMovil().ReporteGestionMovilGestionXSolucion(delegacion, fechaCarga, estFinal, despacho, "", tipoFormulario, 5);
        }

        /// <summary>
        /// Obtiene el reporte de los pagos que se han realizado
        /// </summary>
        /// <param name="idDominio">Filtro para dominio op=0</param>
        /// <param name="ruta">Filtro para la ruta op=''</param>
        /// <returns></returns>
        public DataSet ReporteGestionMovil_PagosDespachos(int idDominio, string ruta)
        {
            return new EntGestionMovil().ReporteGestionMovil_Pagos(idDominio, ruta, "", 1);
        }

        /// <summary>
        /// Obtiene el reporte de los pagos que se han realizado
        /// </summary>
        /// <param name="idDominio">Filtro para dominio op=0</param>
        /// <param name="ruta">Filtro para la ruta op=''</param>
        /// <param name="delegacion">Filtro para delegación op=''</param>
        /// <returns></returns>
        public DataSet ReporteGestionMovil_PagosDelagaciones(int idDominio, string ruta, string delegacion)
        {
            return new EntGestionMovil().ReporteGestionMovil_Pagos(idDominio, ruta, delegacion, 2);
        }

        /// <summary>
        /// Obtiene el reporte de las llamadas sin éxito
        /// </summary>
        /// <param name="idproceso">Numero de proceso a ejecutar</param>
        /// <param name="idDominio">dominio</param>
        /// <returns></returns>
        public DataSet ReporteGestionMovil_LlamadasNoExito(int idproceso, int idDominio)
        {
            return new EntGestionMovil().ReporteGestionMovil_LlamadasNoExito(idproceso, idDominio);
        }
        
    }
}
