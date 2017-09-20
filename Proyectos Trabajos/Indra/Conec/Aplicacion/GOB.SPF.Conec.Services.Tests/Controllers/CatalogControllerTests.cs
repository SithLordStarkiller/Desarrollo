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

namespace GOB.SPF.Conec.Services.Controllers.Tests
{
    [TestClass()]
    public class CatalogControllerTests
    {
        #region SERVICIOS_INSTANCIADOS

        private business.AniosBusiness negocioAnios = new business.AniosBusiness();
        private business.MesesBusiness negocioMeses = new business.MesesBusiness();
        private business.DivisionBusiness negocioDivision = new business.DivisionBusiness();
        private business.GruposBusiness negocioGrupos = new business.GruposBusiness();
        private business.FraccionesBusiness negocioFracciones = new business.FraccionesBusiness();
        private business.GastosInherentesBusiness negocioGastosInherentes = new business.GastosInherentesBusiness();
        private business.TiposServicioBusiness negocioTiposServicio = new business.TiposServicioBusiness();
        private business.TiposDocumentoBusiness negocioTiposDocumento = new business.TiposDocumentoBusiness();
        private business.PeriodosBusiness negocioPeriodos = new business.PeriodosBusiness();
        private business.ClasificacionFactorBusiness negocioClasificacionFactor = new business.ClasificacionFactorBusiness();
        private business.FactoresBusiness negocioFactores = new business.FactoresBusiness();
        private business.ReferenciasBusiness negocioReferencias = new business.ReferenciasBusiness();
        private business.MedidasCobroBusiness negocioMedidasCobro = new business.MedidasCobroBusiness();
        private business.CuotasBusiness negocioCuotas = new business.CuotasBusiness();
        private business.DependenciasBusiness negocioDependencias = new business.DependenciasBusiness();
        private business.FactoresEntidadFederativaBusiness negocioFactoresEntidadFederativa = new business.FactoresEntidadFederativaBusiness();
        private business.FactoresMunicipioBusiness negocioFactoresMunicipio = new business.FactoresMunicipioBusiness();
        private business.FactoresLeyIngresoBusiness negocioFactoresLeyIngreso = new business.FactoresLeyIngresoBusiness();
        private business.FactoresActividadEconomicaBusiness negocioFactoresActividadEconomica = new business.FactoresActividadEconomicaBusiness();
        private business.TipoContactoBusiness negocioTipoContacto=new business.TipoContactoBusiness();
        #endregion SERVICIOS_INSTANCIADOS
        
        #region DIVISION

        [TestMethod]
        public void TestGuardarDivisiones()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Division entity = new Division();
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

            entity.NombreDivision = "P" + strTestName;
            entity.DescDivision = "Test pruebas, fecha creacion:" + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;

            negocioDivision.Guardar(entity);

            List<Division> listaGuardados = negocioDivision.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.NombreDivision == strTestName).NombreDivision, dtTest.ToString());
        }

        [TestMethod]
        public void TestUpdateDivisiones()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Division entity = new Division();
            entity.NombreDivision = strTestName;
            entity.DescDivision = "Test desde proyecto de pruebas " + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioDivision.Guardar(entity);

            List<Division> listaGuardados = negocioDivision.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.NombreDivision == strTestName);

            if (obj != null)
            {
                obj.NombreDivision = "XXX";
                negocioDivision.Guardar(obj);
            }

            var statusObj = negocioDivision.CambiarEstatus(obj);

            var foundObj = negocioDivision.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.NombreDivision, obj.NombreDivision);
        }

        [TestMethod()]
        public void DivisionObtenerTest()
        {
            var entity = new RequestDivision
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 }
            };
            ConecII.Business.DivisionBusiness business = new ConecII.Business.DivisionBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void DivisionObtenerListadoTest()
        {

            ConecII.Business.DivisionBusiness business = new ConecII.Business.DivisionBusiness();
            var lista = business.ObtenerListado();

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void DivisionObtenerPorCriterioTest()
        {
            var entity = new RequestDivision
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 },
                Item = new Division { Activo = true }
            };
            ConecII.Business.DivisionBusiness business = new ConecII.Business.DivisionBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void DivisionObtenerPorIdTest()
        {
            ConecII.Business.DivisionBusiness business = new ConecII.Business.DivisionBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }

        [TestMethod()]
        public void DivisionCambiarEstatusTest()
        {

            var entity = new RequestDivision
            {
                Item = new Division { Activo = false, Identificador = 13 }
            };

            ConecII.Business.DivisionBusiness business = new ConecII.Business.DivisionBusiness();
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result == 1);
        }

        #endregion

        #region PERIODOS

        [TestMethod]
        public void TestGuardarPeriodos()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Periodo entity = new Periodo();
            entity.Nombre = "PRUEBA" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas, a la fecha de creación:" + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioPeriodos.Guardar(entity);

            List<Periodo> listaGuardados = negocioPeriodos.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod]
        public void TestUpdatePeriodos()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Periodo entity = new Periodo();
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            entity.Identificador = 6;
            entity.Nombre = "P/" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas, actualizacion, a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;

            negocioPeriodos.Guardar(entity);

            List<Periodo> listaGuardados = negocioPeriodos.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Identificador == entity.Identificador).Nombre, entity.Nombre);
        }

        [TestMethod()]
        public void PeriodoCambiarEstatusTest()
        {
            var entity = new RequestPeriodo
            {
                Item = new Periodo { Activo = false, Identificador = 6 }
            };

            ConecII.Business.PeriodosBusiness business = new ConecII.Business.PeriodosBusiness();
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result == 1);
        }

        [TestMethod()]
        public void PeriodoObtenerTest()
        {
            var entity = new RequestPeriodo
            {
                Paging = new Paging { All = true, Rows = 10 }
            };
            ConecII.Business.PeriodosBusiness business = new ConecII.Business.PeriodosBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void PeriodoObtenerPorIdTest()
        {
            ConecII.Business.PeriodosBusiness business = new ConecII.Business.PeriodosBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }

        [TestMethod()]
        public void PeriodoObtenerPorCriterioTest()
        {
            var entity = new RequestPeriodo
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 },
                Item = new Periodo { Activo = true }
            };
            ConecII.Business.PeriodosBusiness business = new ConecII.Business.PeriodosBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        #endregion

        #region TIPOS_SERVICIO

        [TestMethod]
        public void TestGuardarTiposServicio()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            TipoServicio entity = new TipoServicio();
            entity.Nombre = "PRUEBA" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.Clave = "PBA";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioTiposServicio.Guardar(entity);

            List<TipoServicio> listaGuardados = negocioTiposServicio.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod]
        public void TestUpdateTiposServicio()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            TipoServicio entity = new TipoServicio();

            entity.Identificador = 6;
            entity.Nombre = "PRUEBA" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  actualiza, la fecha de creación" + strTestName;
            entity.Clave = "PBA";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioTiposServicio.Guardar(entity);

            List<TipoServicio> listaGuardados = negocioTiposServicio.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Identificador == entity.Identificador).Nombre, entity.Nombre);
        }

        [TestMethod()]
        public void TestCambiarEstatusTipoServicio()
        {
            var entity = new RequestTipoServicio
            {
                Item = new TipoServicio { Activo = false, Identificador = 6 }
            };

            ConecII.Business.TiposServicioBusiness business = new ConecII.Business.TiposServicioBusiness();
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result == true);
        }

        [TestMethod]
        public void TestObtenerTodosTipoServicio()
        {
            var entity = new RequestTipoServicio
            {
                Paging = new Paging { All = true, CurrentPage = 1, Rows = 10 }
            };
            ConecII.Business.TiposServicioBusiness business = new ConecII.Business.TiposServicioBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod]
        public void TestObtenerPorCriterioTipoServicio()
        {
            var entity = new RequestTipoServicio
            {
                Paging = new Paging { All = true, CurrentPage = 1, Rows = 10 },
                Item = new TipoServicio { Activo = true }
            };
            ConecII.Business.TiposServicioBusiness business = new ConecII.Business.TiposServicioBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod]
        public void TestObtenerIdTipoServicio()
        {
            ConecII.Business.TiposServicioBusiness business = new ConecII.Business.TiposServicioBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }


        #endregion

        #region GRUPOS

        [TestMethod]
        public void TestGuardarGrupos()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Grupo entity = new Grupo();
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            entity.IdDivision = 1;
            entity.Nombre = "PRUEBA" + strTestName;
            entity.Descripcion = "Test de pruebas. Fecha:" + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;

            negocioGrupos.Guardar(entity);

            List<Grupo> listaGuardados = negocioGrupos.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == strTestName).Nombre, dtTest.ToString());
        }

        [TestMethod]
        public void TestUpdateGrupos()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Grupo entity = new Grupo();
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

            entity.IdDivision = 1;
            entity.Identificador = 64;
            entity.Nombre = "PRUEBA 14/09/2017";
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;

            negocioGrupos.Guardar(entity);

            List<Grupo> listaGuardados = negocioGrupos.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod()]
        public void TestCambiarEstatusGrupo()
        {
            var entity = new RequestGrupo
            {
                Item = new Grupo { Activo = false, Identificador = 64 }
            };

            ConecII.Business.GruposBusiness business = new ConecII.Business.GruposBusiness();
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestObtenerPorIdDivisionGrupo()
        {
            var entity = new RequestGrupo
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 },
                Item = new Grupo { IdDivision = 1 }
            };
            ConecII.Business.GruposBusiness business = new ConecII.Business.GruposBusiness();
            var lista = business.ObtenerPorIdDivision(entity.Item.IdDivision);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerTodosGrupo()
        {
            var entity = new RequestGrupo
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 }
            };
            ConecII.Business.GruposBusiness business = new ConecII.Business.GruposBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerPorCriterioGrupo()
        {
            var entity = new RequestGrupo
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 },
                Item = new Grupo { Activo = true, Identificador = 0, IdDivision = 1 }
            };
            ConecII.Business.GruposBusiness business = new ConecII.Business.GruposBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerIdGrupo()
        {
            ConecII.Business.GruposBusiness business = new ConecII.Business.GruposBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }
        #endregion

        #region MESES

        [TestMethod]
        public void TestGuardarMeses()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Meses entity = new Meses();
            //entity.Mes = 1;
            entity.Identificador = 1;
            entity.DescMes = strTestName;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

            List<Meses> listaGuardados = negocioMeses.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.DescMes == strTestName).DescMes, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateMeses()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Meses entity = new Meses();
            //entity.Mes = 1;
            entity.Identificador = 1;
            entity.DescMes = strTestName;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

            List<Meses> listaGuardados = negocioMeses.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.DescMes == strTestName);

            if (obj != null)
            {
                obj.DescMes = "XXX";
            }

            //var statusObj = negocioMeses.CambiarEstatus(obj);

            var foundObj = negocioMeses.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.DescMes, obj.DescMes);
        }

        #endregion

        #region ANIOS

        //[TestMethod]
        //public void TestGuardarAnios()
        //{
        //    var dtTest = DateTime.Now;
        //    var strTestName = DateTime.Now.ToShortDateString();

        //    Anio entity = new Anio();

        //    entity.DesAnio = strTestName;

        //    Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

        //    List<Anio> listaGuardados = negocioAnios.ObtenerPorCriterio(paging, entity).ToList();

        //    Assert.AreEqual(listaGuardados.Find(x => x.DesAnio == strTestName).DesAnio, strTestName.ToString());
        //}

        //[TestMethod]
        //public void TestUpdateAnios()
        //{
        //    var dtTest = DateTime.Now;
        //    var strTestName = DateTime.Now.ToShortDateString();

        //    Anio entity = new Anio();

        //    entity.DesAnio = strTestName;

        //    Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

        //    List<Anio> listaGuardados = negocioAnios.ObtenerTodos(paging).ToList();

        //    var obj = listaGuardados.Find(x => x.DesAnio == strTestName);

        //    if (obj != null)
        //    {
        //        obj.DesAnio = "XXX";
        //    }

        //    var statusObj = negocioAnios.CambiarEstatus(obj);

        //    var foundObj = negocioAnios.ObtenerPorId(obj.Identificador);

        //    Assert.AreEqual(foundObj.DesAnio, obj.DesAnio);
        //}

        #endregion

        #region REFERENCIAS

        [TestMethod]
        public void TestGuardarReferencia()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Referencia entity = new Referencia();

            entity.ClaveReferencia = 1;
            entity.Descripcion = "PRUEBA/" + strTestName + "/INSERTA";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioReferencias.Guardar(entity);

            List<Referencia> listaGuardados = negocioReferencias.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.ClaveReferencia == entity.ClaveReferencia).ClaveReferencia, entity.ClaveReferencia);
        }

        [TestMethod]
        public void TestUpdateReferencia()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Referencia entity = new Referencia();

            //entity.Numero = strTestName;
            entity.Identificador = 5;
            entity.ClaveReferencia = 2;
            entity.Descripcion = "PRUEBA/" + strTestName + "/ACTUALIZA";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioReferencias.Guardar(entity);

            List<Referencia> listaGuardados = negocioReferencias.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.ClaveReferencia == entity.ClaveReferencia).ClaveReferencia, entity.ClaveReferencia);
        }

        [TestMethod()]
        public void TestCambiarEstatusReferencia()
        {
            var entity = new RequestReferencia
            {
                Item = new Referencia { Activo = false, Identificador = 5 }
            };

            ConecII.Business.ReferenciasBusiness business = new ConecII.Business.ReferenciasBusiness();
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestObtenerTodosReferencia()
        {
            var entity = new RequestReferencia
            {
                Paging = new Paging { All = true, CurrentPage = 1, Rows = 10 }
            };
            ConecII.Business.ReferenciasBusiness business = new ConecII.Business.ReferenciasBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerPorCriterioReferencia()
        {
            var entity = new RequestReferencia
            {
                Paging = new Paging { All = true, CurrentPage = 1, Rows = 10 },
                Item = new Referencia { Activo = true }
            };
            ConecII.Business.ReferenciasBusiness business = new ConecII.Business.ReferenciasBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerIdReferencia()
        {
            ConecII.Business.ReferenciasBusiness business = new ConecII.Business.ReferenciasBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }

        #endregion
        
        #region DEPENDENCIAS

        [TestMethod]
        public void TestGuardarDependencias()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Dependencia entity = new Dependencia();

            entity.Nombre = "PRUEBA/" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas, la fecha de creación:" + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioDependencias.Guardar(entity);

            List<Dependencia> listaGuardados = negocioDependencias.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod]
        public void TestUpdateDependencias()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Dependencia entity = new Dependencia();

            entity.Identificador = 5;
            entity.Nombre = "P/" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas, actualizacion,la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioDependencias.Guardar(entity);

            List<Dependencia> listaGuardados = negocioDependencias.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod()]
        public void TestCambiarEstatusDependencia()
        {
            var entity = new RequestDependencia
            {
                Item = new Dependencia { Activo = false, Identificador = 5 }
            };

            ConecII.Business.DependenciasBusiness business = new ConecII.Business.DependenciasBusiness();
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestObtenerTodosDependencia()
        {
            var entity = new RequestDependencia
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 }
            };
            ConecII.Business.DependenciasBusiness business = new ConecII.Business.DependenciasBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerPorCriterioDependencia()
        {
            var entity = new RequestDependencia
            {
                Paging = new Paging { All = true, CurrentPage = 1, Rows = 10 },
                Item = new Dependencia { Activo = true }
            };
            ConecII.Business.DependenciasBusiness business = new ConecII.Business.DependenciasBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerIdDependencia()
        {
            ConecII.Business.DependenciasBusiness business = new ConecII.Business.DependenciasBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }

        #endregion

        #region MEDIDAS_COBRO

        [TestMethod]
        public void TestObtenerPorCriterioMedidasCobro()
        {
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            MedidaCobro entity = new MedidaCobro();
            entity.Activo = true;

            negocioMedidasCobro.Guardar(entity);

            List<MedidaCobro> listaGuardados = negocioMedidasCobro.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod]
        public void TestUpdateMedidasCobro()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            MedidaCobro entity = new MedidaCobro();

            entity.Nombre = "PRUEBA" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioMedidasCobro.Guardar(entity);

            List<MedidaCobro> listaGuardados = negocioMedidasCobro.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.Nombre == strTestName);

            if (obj != null)
            {
                obj.Nombre = "XXX";
                negocioMedidasCobro.Guardar(obj);
            }

            var statusObj = negocioMedidasCobro.CambiarEstatus(obj);

            var foundObj = negocioMedidasCobro.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.Nombre, obj.Nombre);
        }

        #endregion

        #region CUOTAS

        [TestMethod]
        public void TestGuardarCuotas()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();
            decimal cuotaBase = 1;

            Cuota entity = new Cuota();

            entity.IdTipoServicio = 1;
            entity.IdReferencia = 1;
            entity.IdDependencia = 1;
            entity.IdJerarquia = 1;
            //entity.IdClasifVehiculo = 1;
            //entity.IdGpoTarifario = 1;
            entity.IdMedidaCobro = 1;
            entity.Iva = 16;
            entity.FechaAutorizacion = dtTest.AddDays(1);
            entity.FechaEntradaVigor = dtTest.AddDays(1);
            entity.FechaTermino = dtTest.AddDays(3);
            entity.FechaPublicaDof = dtTest.AddDays(1);
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioCuotas.Guardar(entity);

            List<Cuota> listaGuardados = negocioCuotas.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.CuotaBase == cuotaBase).CuotaBase, cuotaBase);
        }

        [TestMethod]
        public void TestUpdateCuotas()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();
            decimal cuotaBase = 1;

            Cuota entity = new Cuota();

            entity.IdTipoServicio = 1;
            entity.IdReferencia = 1;
            entity.IdDependencia = 1;
            entity.IdJerarquia = 1;
            //entity.IdClasifVehiculo = 1;
            //entity.IdGpoTarifario = 1;
            entity.CuotaBase = 1;
            entity.IdMedidaCobro = 1;
            entity.Iva = 16;
            entity.FechaAutorizacion = dtTest.AddDays(1);
            entity.FechaEntradaVigor = dtTest.AddDays(1);
            entity.FechaTermino = dtTest.AddDays(3);
            entity.FechaPublicaDof = dtTest.AddDays(1);
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioCuotas.Guardar(entity);

            List<Cuota> listaGuardados = negocioCuotas.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.CuotaBase == cuotaBase);

            if (obj != null)
            {
                obj.CuotaBase = 2;
                negocioCuotas.Guardar(obj);
            }

            var statusObj = negocioCuotas.CambiarEstatus(obj);

            var foundObj = negocioCuotas.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.CuotaBase, obj.CuotaBase);
        }

        #endregion

        #region TIPOS_DOCUMENTO

        [TestMethod]
        public void TestGuardarTiposDocumento()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            TipoDocumento entity = new TipoDocumento();

            entity.Nombre = "PRUEBA" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas, la fecha de creación:" + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            entity.IdActividad = 1;
            entity.Confidencial = true;

            negocioTiposDocumento.Guardar(entity);
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            List<TipoDocumento> listaGuardados = negocioTiposDocumento.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod]
        public void TestUpdateTiposDocumento()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            TipoDocumento entity = new TipoDocumento();

            entity.Identificador = 255;
            entity.Nombre = "P" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,actualizacion, la fecha de creación:" + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            entity.IdActividad = 2;
            entity.Confidencial = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioTiposDocumento.Guardar(entity);

            List<TipoDocumento> listaGuardados = negocioTiposDocumento.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Identificador == entity.Identificador).Nombre, entity.Nombre);
        }

        [TestMethod()]
        public void TestCambiarEstatusTipoDocumento()
        {
            var entity = new RequestTipoDocumento
            {
                Item = new TipoDocumento { Activo = false, Identificador = 255 }
            };

            ConecII.Business.TiposDocumentoBusiness business = new ConecII.Business.TiposDocumentoBusiness();
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestObtenerTodosTipoDocumento()
        {
            var entity = new RequestTipoDocumento
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 }
            };
            ConecII.Business.TiposDocumentoBusiness business = new ConecII.Business.TiposDocumentoBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerPorCriterioDocumento()
        {
            var entity = new RequestTipoDocumento
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 },
                Item = new TipoDocumento { Activo = true }
            };
            ConecII.Business.TiposDocumentoBusiness business = new ConecII.Business.TiposDocumentoBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerIdTipDocumento()
        {
            ConecII.Business.TiposDocumentoBusiness business = new ConecII.Business.TiposDocumentoBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }



        #endregion

        #region FRACCIONES

        [TestMethod]
        public void TestGuardarFraccion()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Fraccion entity = new Fraccion();

            entity.IdGrupo = 64;
            entity.Nombre = "PRUEBA_" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  fecha de creación" + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFracciones.Guardar(entity);

            List<Fraccion> listaGuardados = negocioFracciones.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod]
        public void TestUpdateFraccion()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Fraccion entity = new Fraccion();

            entity.Identificador = 4;
            entity.IdGrupo = 64;
            entity.Nombre = "FRACCION04_" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  fecha de creación" + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFracciones.Guardar(entity);

            List<Fraccion> listaGuardados = negocioFracciones.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod()]
        public void TestCambiarEstatusFraccion()
        {
            var entity = new RequestFraccion
            {
                Item = new Fraccion { Activo = false, Identificador = 4 }
            };

            ConecII.Business.FraccionesBusiness business = new ConecII.Business.FraccionesBusiness();
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestObtenerTodosFraccion()
        {
            var entity = new RequestFraccion
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 }
            };
            ConecII.Business.FraccionesBusiness business = new ConecII.Business.FraccionesBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerPorCriterioFraccion()
        {
            var entity = new RequestFraccion
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 },
                Item = new Fraccion { Activo = true }
            };
            ConecII.Business.FraccionesBusiness business = new ConecII.Business.FraccionesBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerIdFraccion()
        {
            ConecII.Business.FraccionesBusiness business = new ConecII.Business.FraccionesBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }

        #endregion

        #region GASTOS_INHERENTES

        [TestMethod]
        public void TestGuardarGastosInherentes()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            GastoInherente entity = new GastoInherente();

            entity.Nombre = "PRUEBA" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  la fecha de creación" + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioGastosInherentes.Guardar(entity);

            List<GastoInherente> listaGuardados = negocioGastosInherentes.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod]
        public void TestUpdateGastosInherentes()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            GastoInherente entity = new GastoInherente();

            entity.Identificador = 4;
            entity.Nombre = "P/" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas, actualización, la fecha de creación" + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioGastosInherentes.Guardar(entity);

            List<GastoInherente> listaGuardados = negocioGastosInherentes.ObtenerTodos(paging).ToList();
            Assert.AreEqual(listaGuardados.Find(x => x.Identificador == entity.Identificador).Nombre, entity.Nombre);

        }

        [TestMethod()]
        public void TestCambiarEstatusGastosInherentes()
        {
            var entity = new RequestGastoInherente
            {
                Item = new GastoInherente { Activo = false, Identificador = 4 }
            };

            ConecII.Business.GastosInherentesBusiness business = new ConecII.Business.GastosInherentesBusiness();
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestObtenerTodosGastosInherentes()
        {
            var entity = new RequestGastoInherente
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 }
            };
            ConecII.Business.GastosInherentesBusiness business = new ConecII.Business.GastosInherentesBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerPorCriterioGastosInherentes()
        {
            var entity = new RequestGastoInherente
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 },
                Item = new GastoInherente { Activo = true }
            };
            ConecII.Business.GastosInherentesBusiness business = new ConecII.Business.GastosInherentesBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerIdGastosInherentes()
        {
            ConecII.Business.GastosInherentesBusiness business = new ConecII.Business.GastosInherentesBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }



        #endregion

        #region FACTORES

        [TestMethod]
        public void TestGuardarFactor()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Factor entity = new Factor();

            entity.IdTipoServicio = 2;
            entity.IdClasificacionFactor = 2;
            entity.IdMedidaCobro = 2;
            entity.Nombre = "PRUEBA/" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.Cuota = 1;
            entity.FechaAutorizacion = dtTest.AddDays(1);
            entity.FechaEntradaVigor = dtTest.AddDays(2);
            entity.FechaTermino = dtTest.AddDays(20);
            entity.FechaPublicacionDof = dtTest;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 10 };
            negocioFactores.Guardar(entity);

            List<Factor> listaGuardados = negocioFactores.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod]
        public void TestUpdateFactor()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            Factor entity = new Factor();

            entity.Identificador = 6;
            entity.IdTipoServicio = 1;
            entity.IdClasificacionFactor = 1;
            entity.IdMedidaCobro = 1;
            entity.Nombre = "P" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  actualizacion";
            entity.Cuota = 1;
            entity.FechaAutorizacion = dtTest.AddDays(2);
            entity.FechaEntradaVigor = dtTest.AddDays(3);
            entity.FechaTermino = dtTest.AddDays(21);
            entity.FechaPublicacionDof = dtTest;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = false;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFactores.Guardar(entity);

            List<Factor> listaGuardados = negocioFactores.ObtenerTodos(paging).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod()]
        public void TestCambiarEstatusFactor()
        {
            var entity = new RequestFactor
            {
                Item = new Factor { Activo = false, Identificador = 6 }
            };

            ConecII.Business.FactoresBusiness business = new ConecII.Business.FactoresBusiness();
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void FactorObtenerPorClasificacionTest()
        {
            var entity = new RequestFactor
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 },
                Item = new Factor { IdClasificacionFactor = 1 }
            };
            ConecII.Business.FactoresBusiness business = new ConecII.Business.FactoresBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerTodosFactor()
        {
            var entity = new RequestFactor
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 }
            };
            ConecII.Business.FactoresBusiness business = new ConecII.Business.FactoresBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerPorCriterioFactor()
        {
            var entity = new RequestFactor
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 },
                Item = new Factor { Activo = true }
            };
            ConecII.Business.FactoresBusiness business = new ConecII.Business.FactoresBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerIdFactor()
        {
            ConecII.Business.FactoresBusiness business = new ConecII.Business.FactoresBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }


        #endregion
        
        #region CLASIFICACION_FACTOR

        [TestMethod]
        public void TestGuardarClasificacionFactor()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            ClasificacionFactor entity = new ClasificacionFactor();

            entity.Nombre = "PRUEBAS" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas, la fecha de creación:" + strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioClasificacionFactor.Guardar(entity);

            List<ClasificacionFactor> listaGuardados = negocioClasificacionFactor.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod]
        public void TestUpdateClasificacionFactor()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            ClasificacionFactor entity = new ClasificacionFactor();

            entity.Identificador = 3;
            entity.Nombre = "P/" + strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas, actualizacion, el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioClasificacionFactor.Guardar(entity);


            List<ClasificacionFactor> listaGuardados = negocioClasificacionFactor.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == entity.Nombre).Nombre, entity.Nombre);
        }

        [TestMethod()]
        public void TestCambiarEstatusClasificacionFactor()
        {
            var entity = new RequestClasificacionFactor
            {
                Item = new ClasificacionFactor { Activo = false, Identificador = 3 }
            };

            ConecII.Business.ClasificacionFactorBusiness business = new ConecII.Business.ClasificacionFactorBusiness();
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result);
        }


        [TestMethod()]
        public void TestObtenerTodosClasificacionFactor()
        {
            var entity = new RequestClasificacionFactor
            {
                Paging = new Paging { All = true, CurrentPage = 1, Rows = 10 }
            };
            ConecII.Business.ClasificacionFactorBusiness business = new ConecII.Business.ClasificacionFactorBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerPorCriterioClasificacionFactor()
        {
            var entity = new RequestClasificacionFactor
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 },
                Item = new ClasificacionFactor { Activo = true }
            };
            ConecII.Business.ClasificacionFactorBusiness business = new ConecII.Business.ClasificacionFactorBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerIdClasificacionFactor()
        {
            ConecII.Business.ClasificacionFactorBusiness business = new ConecII.Business.ClasificacionFactorBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }

        #endregion

        #region FACTOR_ENTIDAD_FEDERATIVA

        //[TestMethod]
        //public void TestGuardarFactorEntidadFederativa()
        //{
        //    var dtTest = DateTime.Now;
        //    var strTestName = DateTime.Now.ToShortDateString();

        //    FactorEntidadFederativa entity = new FactorEntidadFederativa();

        //    entity.Clasificacion.Identificador = 1;
        //    entity.Estado.Identificador = 1;
        //    entity.Activo = true;
        //    entity.Factor.Identificador = 1;
        //    Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
        //    negocioFactoresEntidadFederativa.Guardar(entity);

        //    List<FactorEntidadFederativa> listaGuardados = negocioFactoresEntidadFederativa.ObtenerPorCriterio(paging, entity).ToList();

        //    // Assert.AreEqual(listaGuardados.Find(x => x.DescFactEntidFed == strTestName).DescFactEntidFed, strTestName.ToString());
        //}

        //[TestMethod]
        //public void TestUpdateFactorEntidadFederativa()
        //{
        //    var dtTest = DateTime.Now;
        //    var strTestName = DateTime.Now.ToShortDateString();

        //    FactorEntidadFederativa entity = new FactorEntidadFederativa();

        //    entity.IdFactor = 1;
        //    entity.IdEntidFed = 1;
        //    Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
        //    negocioFactoresEntidadFederativa.Guardar(entity);

        //    List<FactorEntidadFederativa> listaGuardados = negocioFactoresEntidadFederativa.ObtenerTodos(paging).ToList();

        //    //var obj = listaGuardados.Find(x => x.DescFactEntidFed == strTestName);

        //    //if (obj != null)
        //    //{
        //    //    obj.DescFactEntidFed = "XXX";
        //    //    negocioFactoresEntidadFederativa.Guardar(obj);
        //    //}

        //    //var statusObj = negocioFactoresEntidadFederativa.CambiarEstatus(obj);

        //    //var foundObj = negocioFactoresEntidadFederativa.ObtenerPorId(obj.Identificador);

        //    // Assert.AreEqual(foundObj.DescFactEntidFed, obj.DescFactEntidFed);
        //}

        //#endregion

        //#region FACTORES_MUNICIPIO

        //[TestMethod]
        //public void TestGuardarFactoresMunicipio()
        //{
        //    var dtTest = DateTime.Now;
        //    var strTestName = DateTime.Now.ToShortDateString();

        //    FactorMunicipio entity = new FactorMunicipio();

        //    entity.DescFactMpio = strTestName;
        //    entity.IdClasificadorFactor = 1;
        //    entity.IdGrupo = 1;
        //    entity.IdEntidFed = 1;
        //    entity.DescEntidFed = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
        //    entity.FechaInicio = dtTest.AddDays(1);
        //    entity.FechaFin = entity.FechaInicio.AddDays(2);
        //    entity.Activo = true;
        //    entity.IdFactor = 1;
        //    Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
        //    FactorMunicipioDTO entit1y = new FactorMunicipioDTO();
        //    negocioFactoresMunicipio.Guardar(entit1y);

        //    List<FactorMunicipio> listaGuardados = negocioFactoresMunicipio.ObtenerPorCriterio(paging, entity).ToList();

        //    Assert.AreEqual(listaGuardados.Find(x => x.DescFactMpio == strTestName).DescFactMpio, strTestName.ToString());
        //}

        //[TestMethod]
        //public void TestUpdateFactoresMunicipio()
        //{
        //    var dtTest = DateTime.Now;
        //    var strTestName = DateTime.Now.ToShortDateString();

        //    FactorMunicipio entity = new FactorMunicipio();

        //    entity.DescFactMpio = strTestName;
        //    entity.IdClasificadorFactor = 1;
        //    entity.IdGrupo = 1;
        //    entity.IdEntidFed = 1;
        //    entity.DescEntidFed = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
        //    entity.FechaInicio = dtTest.AddDays(1);
        //    entity.FechaFin = entity.FechaInicio.AddDays(2);
        //    entity.Activo = true;
        //    entity.IdFactor = 1;
        //    Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
        //    FactorMunicipioDTO entity1 = new FactorMunicipioDTO();
        //    negocioFactoresMunicipio.Guardar(entity1);

        //    List<FactorMunicipio> listaGuardados = negocioFactoresMunicipio.ObtenerTodos(paging).ToList();

        //    var obj = listaGuardados.Find(x => x.DescFactMpio == strTestName);
        //    if (obj != null)
        //    {
        //        obj.DescFactMpio = "XXX";

        //        negocioFactoresMunicipio.Guardar(entity1);
        //    }

        //    var statusObj = negocioFactoresMunicipio.CambiarEstatus(obj);

        //    var foundObj = negocioFactoresMunicipio.ObtenerPorId(obj.Identificador);

        //    Assert.AreEqual(foundObj.DescFactMpio, obj.DescFactMpio);
        //}

        #endregion

        #region FACTORES_LEY_INGRESO

        [TestMethod]
        public void TestGuardarFactoresLeyIngreso()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            FactorLeyIngreso entity = new FactorLeyIngreso();
            
            entity.IdAnio = 1;
            entity.IdMes = 2;
            entity.Factor = 15;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFactoresLeyIngreso.Guardar(entity);
        
            List<FactorLeyIngreso> listaGuardados = negocioFactoresLeyIngreso.ObtenerPorCriterio(paging, entity).Where(x => x.IdAnio == entity.IdAnio && x.IdMes == entity.IdMes).ToList();

            Assert.IsTrue(listaGuardados.Any());
        }

        [TestMethod]
        public void TestUpdateFactoresLeyIngreso()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            FactorLeyIngreso entity = new FactorLeyIngreso();

            entity.Identificador = 5;
            entity.IdAnio = 2;
            entity.IdMes = 2;
            entity.Factor = 10;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFactoresLeyIngreso.Guardar(entity);

            List<FactorLeyIngreso> listaGuardados = negocioFactoresLeyIngreso.ObtenerPorCriterio(paging, entity).Where(x => x.IdAnio == entity.IdAnio && x.IdMes == entity.IdMes).ToList();

            var isOK = listaGuardados.Count() > 0 ? true : false;
            Assert.IsTrue(isOK);
        }

        [TestMethod()]
        public void TestCambiarEstatusFactorLeyIngreso()
        {
            var entity = new RequestFactorLeyIngreso
            {
                Item = new FactorLeyIngreso { Activo = false, Identificador = 5 }
            };

            ConecII.Business.FactoresLeyIngresoBusiness business = new ConecII.Business.FactoresLeyIngresoBusiness();
            var result = business.CambiarEstatus(entity.Item);

            Assert.IsTrue(result == true);
        }

        [TestMethod()]
        public void TestObtenerIdFactorLeyIngreso()
        {
            var entity = new RequestFactorLeyIngreso
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 }
            };
            ConecII.Business.FactoresLeyIngresoBusiness business = new ConecII.Business.FactoresLeyIngresoBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerPorCriterioFactorLeyIngreso()
        {
            var entity = new RequestFactorLeyIngreso
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 },
                Item = new FactorLeyIngreso { Activo = true }
            };
            ConecII.Business.FactoresLeyIngresoBusiness business = new ConecII.Business.FactoresLeyIngresoBusiness();
            var lista = business.ObtenerPorCriterio(entity.Paging, entity.Item);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void TestObtenerTodosFactorLeyIngreso()
        {
            ConecII.Business.FactoresLeyIngresoBusiness business = new ConecII.Business.FactoresLeyIngresoBusiness();
            var objeto = business.ObtenerPorId(1);

            Assert.IsTrue(objeto != null);
        }

        #endregion

        #region FACTORES_ACTIVIDAD_ECONOMICA

        [TestMethod]
        public void TestGuardarFactoresActividadEconomica()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            FactorActividadEconomica entity = new FactorActividadEconomica();

            entity.DescFacActividadEconomica = strTestName;
            entity.IdFraccion = 1;
            entity.IdFactor = 1;
            entity.FechaInicio = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFactoresActividadEconomica.Guardar(entity);

            List<FactorActividadEconomica> listaGuardados = negocioFactoresActividadEconomica.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.DescFacActividadEconomica == strTestName).DescFacActividadEconomica, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateFactoresActividadEconomica()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToShortDateString();

            FactorActividadEconomica entity = new FactorActividadEconomica();

            entity.DescFacActividadEconomica = strTestName;
            entity.IdFraccion = 1;
            entity.IdFactor = 1;
            entity.FechaInicio = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFactoresActividadEconomica.Guardar(entity);

            List<FactorActividadEconomica> listaGuardados = negocioFactoresActividadEconomica.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.DescFacActividadEconomica == strTestName);

            if (obj != null)
            {
                obj.DescFacActividadEconomica = "XXX";
                negocioFactoresActividadEconomica.Guardar(obj);
            }
            var statusObj = negocioFactoresActividadEconomica.CambiarEstatus(obj);

            var foundObj = negocioFactoresActividadEconomica.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.DescFacActividadEconomica, obj.DescFacActividadEconomica);
        }

        #endregion


        /*
        [TestMethod()]
        public void RolesModulosControlObtenerTest()
        {
            var entity = new RequestRolModuloControl
            {
                Paging = new Paging { All = true, CurrentPage = 1, Pages = 10, Rows = 10 }
            };
            ConecII.Business.RolModuloControlBusiness business = new ConecII.Business.RolModuloControlBusiness();
            var lista = business.ObtenerTodos(entity.Paging);

            Assert.IsTrue(lista.Any());
        }

        [TestMethod()]
        public void RolesModulosControlObtenerPorCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesModulosControlObtenerPorIdTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesModulosControlCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesModulosControlGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TiposControlObtenerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TiposControlObtenerPorCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TiposControlObtenerPorIdTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TipoControlCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TipoControlGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlesObtenerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlesObtenerPorCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlObtenerPorIdTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesModulosObtenerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesModulosObtenerPorCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloObtenerPorIdTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesUsuariosObtenerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesUsuariosObtenerPorCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolUsuarioObtenerPorIdTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolUsuarioCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolUsuarioGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuariosObtenerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuariosObtenerPorCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuarioObtenerPorIdTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuarioCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuarioGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesObtenerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesObtenerPorCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolObtenerPorIdTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModulosObtenerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModulosObtenerPorCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModuloObtenerPorIdTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModuloCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModuloGuardarTest()
        {
            throw new NotImplementedException();
        }
        */

        #region Tipo Contacto

        [TestMethod]
        public void ObtenerTipocontacto()
        {
           var list= negocioTipoContacto.ObtenerTodos();
            Assert.IsTrue(list.Any());
        }
        #endregion
    }
}