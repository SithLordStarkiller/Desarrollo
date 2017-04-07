using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades;
using PubliPayments.Negocios.Originacion;
using PubliPayments.Utiles;

namespace PubliPayments.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PubliPayments.Entidades.ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            PubliPayments.Entidades.ConnectionDB.EstalecerConnectionString("SqlDefault", ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            Inicializa.Inicializar(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

            var documentosOrden = new DocumentosOrden("279457");
            documentosOrden.DocumentosCompletos(2);

            new EntGestionadas().AgregarDocumentoOrden(279457, "DocBuroCredito",
                                documentosOrden.GenerarDocumentos("DocBuroCredito"));
        }
    }
}
