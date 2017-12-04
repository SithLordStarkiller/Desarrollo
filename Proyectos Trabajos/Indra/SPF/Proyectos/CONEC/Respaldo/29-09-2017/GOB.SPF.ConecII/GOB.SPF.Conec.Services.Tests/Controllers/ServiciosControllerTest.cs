using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOB.SPF.Conec.Services.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Request;
using business = GOB.SPF.ConecII.Business;

namespace GOB.SPF.Conec.Services.Tests.Controllers
{
    [TestClass()]
    public class ServiciosControllerTest
    {
        #region SERVICIOS_INSTANCIADOS

        private business.McsBusiness negocioMcs = new business.McsBusiness();
        private business.RepBusiness negocioRep = new business.RepBusiness();

        #endregion SERVICIOS_INSTANCIADOS

        #region REP

        #region JERARQUIAS

        [TestMethod]
        public void TestObtenerJerarquiasREP()
        {
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            List<Jerarquia> listaGuardados = negocioRep.ObtenerJerarquias(paging).ToList();

            Assert.IsTrue(listaGuardados.Any());
        }

        #endregion      

        #endregion REP

        #region MCS

        #region GRUPOS_TARIFARIO

        [TestMethod]
        public void TestObtenerTarifarioMCS()
        {
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

            List<GrupoTarifario> listaGuardados = negocioMcs.ObtenerTarifario(paging).ToList();

            Assert.IsTrue(listaGuardados.Any());
        }

        #endregion

        #region ESTADOS
        [TestMethod]
        public void TestObtenerEstadosMCS()
        {
            business.McsBusiness negocioMcs1 = new business.McsBusiness();
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            List<Estado> listaGuardados = negocioMcs1.ObtenerEstados().ToList();

            Assert.IsTrue(listaGuardados.Any());
        }

        #endregion ESTADOS

        #region MUNICIPIO

        [TestMethod]
        public void TestObtenerMunicipioMCS()
        {
            Estado entity = new Estado();

            entity.Identificador = 9;
            entity.Nombre = "Ciudad de México";
            Paging paging = new Paging { CurrentPage = 1, Rows = 100 };
            List<Municipio> listaGuardados = negocioMcs.ObtenerMunipios(entity).ToList();

            Assert.IsTrue(listaGuardados.Any());
        }

        #endregion MUNICIPIO
       
        #region ASENTAMIENTOS

        [TestMethod]
        public void TestObtenerAsentamientosMcs()
        {
            var asentamiento = new Asentamiento()
            {
                Estado = new Estado() { Identificador = 9 },
                Municipio = new Municipio() { Identificador = 5 },
                CodigoPostal = "07160"
            };
            var listAsentamientos = negocioMcs.ObtenerAsentamientos(asentamiento).ToList();
            Assert.IsTrue(listAsentamientos.All
                (p => p.CodigoPostal == asentamiento.CodigoPostal &&
                p.Estado.Identificador == asentamiento.Estado.Identificador
                && p.Municipio.Identificador == asentamiento.Municipio.Identificador));
        }

        #endregion ASENTAMIENTOS

        #region ZONAS

        [TestMethod]
        public void TestObtenerZonasMcs()
        {
            var listZonas = negocioMcs.ObtenerZonas();
            Assert.IsTrue(listZonas.Any());
        }

        #endregion ZONAS

        #region ESTACION
        [TestMethod]
        public void TestObtenerEstacionesMcs()
        {
            var listEstaciones = negocioMcs.ObtenerEstaciones();
            Assert.IsTrue(listEstaciones.Any());
        }

        #endregion ESTACION

        #region TIPO_INSTALACION
        [TestMethod]
        public void TestObtenerTipoInstalacionesMcs()
        {
            var listTipoInstalacion = negocioMcs.ObtenerTipoInstalacion();
            Assert.IsTrue(listTipoInstalacion.Any());
        }
        #endregion TIPO_INSTALACION
        #endregion MCS

    }
}
