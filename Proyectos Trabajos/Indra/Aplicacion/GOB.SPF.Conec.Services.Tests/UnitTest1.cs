using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOB.SPF.ConecII.Entities;
using System.Collections.Generic;

using business = GOB.SPF.ConecII.Business;
using GOB.SPF.Conec.Services.Controllers;

using System.Threading.Tasks;
using System.Net.Http;
using GOB.SPF.ConecII.Entities.DTO;
//using System.Web.Http;


namespace GOB.SPF.Conec.Services.Tests
{

    [TestClass]
    public class UnitTest1
    {

        private business.DivisionBusiness negocioDivision = new business.DivisionBusiness();
        private business.TiposServicioBusiness negocioTiposServicio = new business.TiposServicioBusiness();
        private business.PeriodosBusiness negocioPeriodos = new business.PeriodosBusiness();
        private business.GruposBusiness negocioGrupos = new business.GruposBusiness();
        private business.CuotasBusiness negocioCuotas = new business.CuotasBusiness();
        private business.TiposDocumentoBusiness negocioTiposDocumento = new business.TiposDocumentoBusiness();
        private business.FraccionesBusiness negocioFracciones = new business.FraccionesBusiness();
        private business.FactoresEntidadFederativaBusiness negocioFactoresEntidadFederativa = new business.FactoresEntidadFederativaBusiness();
        private business.FactoresMunicipioBusiness negocioFactoresMunicipio = new business.FactoresMunicipioBusiness();
        private business.FactoresLeyIngresoBusiness negocioFactoresLeyIngreso = new business.FactoresLeyIngresoBusiness();
        private business.FactoresActividadEconomicaBusiness negocioFactoresActividadEconomica = new business.FactoresActividadEconomicaBusiness();
        private business.GastosInherentesBusiness negocioGastosInherentes = new business.GastosInherentesBusiness();
        private business.FactoresBusiness negocioFactores = new business.FactoresBusiness();
        private business.MedidasCobroBusiness negocioMedidasCobro = new business.MedidasCobroBusiness();
        private business.ReferenciasBusiness negocioReferencias = new business.ReferenciasBusiness();
        private business.ClasificacionFactorBusiness negocioClasificacionFactor = new business.ClasificacionFactorBusiness();
        private business.DependenciasBusiness negocioDependencias = new business.DependenciasBusiness();
        private business.MesesBusiness negocioMeses = new business.MesesBusiness();
        private business.GruposTarifarioBusiness negocioGruposTarifario = new business.GruposTarifarioBusiness();
        private business.JerarquiasBusiness negocioJerarquias = new business.JerarquiasBusiness();
        private business.AniosBusiness negocioAnios = new business.AniosBusiness();
        //private CatalogController servicio = new CatalogController();

        #region DIVISION

        [TestMethod]
        public void TestGuardarDivisiones()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Division entity = new Division();
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            entity.NombreDivision = strTestName;
            entity.DescDivision = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
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
            var strTestName = DateTime.Now.ToString();

            Division entity = new Division();
            entity.NombreDivision = strTestName;
            entity.DescDivision = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
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

        #endregion

        #region PERIODOS

        [TestMethod]
        public void TestGuardarPeriodos()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Periodo entity = new Periodo();
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioPeriodos.Guardar(entity);

            List<Periodo> listaGuardados = negocioPeriodos.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == strTestName).Nombre, dtTest.ToString());
        }

        [TestMethod]
        public void TestUpdatePeriodos()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Periodo entity = new Periodo();
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;

            negocioPeriodos.Guardar(entity);

            List<Periodo> listaGuardados = negocioPeriodos.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.Nombre == strTestName);

            if (obj != null)
            {
                obj.Nombre = "XXX";
                negocioPeriodos.Guardar(obj);
            }

            var statusObj = negocioPeriodos.CambiarEstatus(obj);

            var foundObj = negocioPeriodos.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.Nombre, obj.Nombre);
        }

        #endregion

        #region TIPOS_SERVICIO

        [TestMethod]
        public void TestGuardarTiposServicio()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            TipoServicio entity = new TipoServicio();
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioTiposServicio.Guardar(entity);

            List<TipoServicio> listaGuardados = negocioTiposServicio.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == strTestName).Nombre, dtTest.ToString());
        }

        [TestMethod]
        public void TestUpdateTiposServicio()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            TipoServicio entity = new TipoServicio();
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioTiposServicio.Guardar(entity);

            List<TipoServicio> listaGuardados = negocioTiposServicio.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.Nombre == strTestName);

            if (obj != null)
            {
                obj.Nombre = "XXX";
                negocioTiposServicio.Guardar(obj);
            }

            var statusObj = negocioTiposServicio.CambiarEstatus(obj);

            var foundObj = negocioTiposServicio.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.Nombre, obj.Nombre);
        }

        #endregion

        #region GRUPOS

        [TestMethod]
        public void TestGuardarGrupos()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Grupo entity = new Grupo();
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            entity.IdDivision = 1;
            //entity.NombreGrupo = strTestName;
            //entity.DescGrupo = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
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
            var strTestName = DateTime.Now.ToString();

            Grupo entity = new Grupo();

            entity.IdDivision = 1;
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;

            negocioGrupos.Guardar(entity);
            Paging paging = new Paging();
            List<Grupo> listaGuardados = negocioGrupos.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.Nombre == strTestName);

            if (obj != null)
            {
                obj.Nombre = "XXX";
                negocioGrupos.Guardar(obj);
            }

            var statusObj = negocioGrupos.CambiarEstatus(obj);

            var foundObj = negocioGrupos.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.Nombre, obj.Nombre);
        }

        #endregion

        #region MESES

        [TestMethod]
        public void TestGuardarMeses()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

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
            var strTestName = DateTime.Now.ToString();

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

        // Josue Zaragoza

        #region ANIOS

        [TestMethod]
        public void TestGuardarAnios()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Anio entity = new Anio();

            entity.DesAnio = strTestName;

            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

            List<Anio> listaGuardados = negocioAnios.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.DesAnio == strTestName).DesAnio, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateAnios()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Anio entity = new Anio();

            entity.DesAnio = strTestName;

            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

            List<Anio> listaGuardados = negocioAnios.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.DesAnio == strTestName);

            if (obj != null)
            {
                obj.DesAnio = "XXX";
            }

            //var statusObj = negocioAnios.CambiarEstatus(obj);

            var foundObj = negocioAnios.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.DesAnio, obj.DesAnio);
        }

        #endregion

        #region REFERENCIAS

        [TestMethod]
        public void TestGuardarReferencias()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Referencia entity = new Referencia();

            //entity.ClaveReferencia = strTestName;
            entity.Descripcion = strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioReferencias.Guardar(entity);

            List<Referencia> listaGuardados = negocioReferencias.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Descripcion == strTestName).Descripcion, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateReferencias()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Referencia entity = new Referencia();

            //entity.Numero = strTestName;
            entity.Descripcion = strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioReferencias.Guardar(entity);

            List<Referencia> listaGuardados = negocioReferencias.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.Descripcion == strTestName);

            if (obj != null)
            {
                obj.Descripcion = "XXX";
                negocioReferencias.Guardar(obj);
            }

            var statusObj = negocioReferencias.CambiarEstatus(obj);

            var foundObj = negocioReferencias.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.Descripcion, obj.Descripcion);
        }

        #endregion

        #region GRUPOS_TARIFARIO

        [TestMethod]
        public void TestGuardarGruposTarifario()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            VehiculoTarifario entity = new VehiculoTarifario();


            entity.NombreGpoTarifario = strTestName;
            entity.DescGpoTarifario = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

            List<VehiculoTarifario> listaGuardados = negocioGruposTarifario.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.NombreGpoTarifario == strTestName).NombreGpoTarifario, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateGruposTarifario()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            VehiculoTarifario entity = new VehiculoTarifario();

            entity.NombreGpoTarifario = strTestName;
            entity.DescGpoTarifario = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

            List<VehiculoTarifario> listaGuardados = negocioGruposTarifario.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.NombreGpoTarifario == strTestName);

            if (obj != null)
            {
                obj.NombreGpoTarifario = "XXX";
            }

            var foundObj = negocioGruposTarifario.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.NombreGpoTarifario, obj.NombreGpoTarifario);
        }

        #endregion

        #region DEPENDENCIAS

        [TestMethod]
        public void TestGuardarDependencias()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Dependencia entity = new Dependencia();

            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioDependencias.Guardar(entity);

            List<Dependencia> listaGuardados = negocioDependencias.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == strTestName).Nombre, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateDependencias()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Dependencia entity = new Dependencia();

            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioDependencias.Guardar(entity);

            List<Dependencia> listaGuardados = negocioDependencias.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.Nombre == strTestName);

            if (obj != null)
            {
                obj.Nombre = "XXX";
                negocioDependencias.Guardar(obj);
            }

            var statusObj = negocioDependencias.CambiarEstatus(obj);

            var foundObj = negocioDependencias.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.Nombre, obj.Nombre);
        }

        #endregion

        #region JERARQUIAS

        [TestMethod]
        public void TestGuardarJerarquias()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Jerarquia entity = new Jerarquia();
            //entity..DescJerarquia = strTestName;            
            entity.Descripcion = strTestName;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            //entity.Activo = true;
            entity.IsActive = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

            List<Jerarquia> listaGuardados = negocioJerarquias.ObtenerPorCriterio(paging, entity).ToList();

            //Assert.AreEqual(listaGuardados.Find(x => x.DescJerarquia == strTestName).DescJerarquia, strTestName.ToString());
            Assert.AreEqual(listaGuardados.Find(x => x.Descripcion == strTestName).Descripcion, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateJerarquias()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Jerarquia entity = new Jerarquia();
           // entity.DescJerarquia = strTestName;
            entity.Descripcion = strTestName;
            //entity.Activo = true;
            //entity.Activo = true;
            entity.IsActive = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };

            List<Jerarquia> listaGuardados = negocioJerarquias.ObtenerTodos(paging).ToList();
            //var obj = listaGuardados.Find(x => x.DescJerarquia == strTestName);
            //if (obj != null)
            //{
            //    //obj.DescJerarquia = "XXX";
            //}
            //var statusObj = negocioJerarquias.CambiarEstatus(obj);

            //var foundObj = negocioJerarquias.ObtenerPorId(obj.Identificador);
            //Assert.AreEqual(foundObj.DescJerarquia, obj.DescJerarquia);            
        }

        #endregion

        #region CUOTAS

        [TestMethod]
        public void TestGuardarCuotas()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();
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
            var strTestName = DateTime.Now.ToString();
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
            var strTestName = DateTime.Now.ToString();

            TipoDocumento entity = new TipoDocumento();
            //entity.NombreTipoDocuemento = strTestName;
            //entity.DescTipoDocumento = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            entity.IdActividad = 1;
            entity.Confidencial = true;

            negocioTiposDocumento.Guardar(entity);
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            List<TipoDocumento> listaGuardados = negocioTiposDocumento.ObtenerPorCriterio(paging, entity).ToList();
            //Assert.AreEqual(listaGuardados.Find(x => x.NombreTipoDocuemento == strTestName).NombreTipoDocuemento, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateTiposDocumento()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            TipoDocumento entity = new TipoDocumento();
            //entity.NombreTipoDocuemento = strTestName;
            //entity.DescTipoDocumento = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            entity.IdActividad = 1;
            entity.Confidencial = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioTiposDocumento.Guardar(entity);

            List<TipoDocumento> listaGuardados = negocioTiposDocumento.ObtenerTodos(paging).ToList();


            //var obj = listaGuardados.Find(x => x.NombreTipoDocuemento == strTestName);

            //if (obj != null)
            //{
            //    //obj.NombreTipoDocuemento = "XXX";
            //    //negocioTiposDocumento.Guardar(obj);
            //}

            //var statusObj = negocioTiposDocumento.CambiarEstatus(obj);

            //var foundObj = negocioTiposDocumento.ObtenerPorId(obj.Identificador);


            //Assert.AreEqual(foundObj.NombreTipoDocuemento, obj.NombreTipoDocuemento);
        }

        #endregion

        #region FRACCIONES

        [TestMethod]
        public void TestGuardarFracciones()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Fraccion entity = new Fraccion();

            entity.IdGrupo = 1;
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFracciones.Guardar(entity);

            List<Fraccion> listaGuardados = negocioFracciones.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == strTestName).Nombre, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateFracciones()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Fraccion entity = new Fraccion();

            entity.IdGrupo = 1;
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFracciones.Guardar(entity);

            List<Fraccion> listaGuardados = negocioFracciones.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.Nombre == strTestName);

            if (obj != null)
            {
                obj.Nombre = "XXX";
                negocioFracciones.Guardar(obj);
            }

            var statusObj = negocioFracciones.CambiarEstatus(obj);

            var foundObj = negocioFracciones.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.Nombre, obj.Nombre);
        }

        #endregion

        #region GASTOS_INHERENTES

        [TestMethod]
        public void TestGuardarGastosInherentes()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            GastoInherente entity = new GastoInherente();

            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioGastosInherentes.Guardar(entity);

            List<GastoInherente> listaGuardados = negocioGastosInherentes.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == strTestName).Nombre, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateGastosInherentes()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            GastoInherente entity = new GastoInherente();

            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioGastosInherentes.Guardar(entity);

            List<GastoInherente> listaGuardados = negocioGastosInherentes.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.Nombre == strTestName);

            //var obtenerCriterioObj = negocioGastosInherentes.ObtenerPorCriterio(1, 20, obj);

            if (obj != null)
            {
                obj.Nombre = "XXX";
                negocioGastosInherentes.Guardar(obj);
            }

            var foundObj = negocioGastosInherentes.ObtenerPorId(obj.Identificador);

            var statusObj = negocioGastosInherentes.CambiarEstatus(obj);

            Assert.AreEqual(foundObj.Nombre, obj.Nombre);
        }

        #endregion

        #region FACTORES

        [TestMethod]
        public void TestGuardarFactores()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Factor entity = new Factor();

            entity.IdTipoServicio = 1;
            entity.IdClasificacionFactor = 1;
            entity.IdMedidaCobro = 1;
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.CuotaFactor = 1;
            entity.FechaAutorizacion = dtTest;
            entity.FechaEntradaVigor = dtTest;
            entity.FechaTermino = dtTest;
            entity.FechaPublicacionDof = dtTest;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFactores.Guardar(entity);

            List<Factor> listaGuardados = negocioFactores.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == strTestName).Nombre, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateFactores()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            Factor entity = new Factor();

            entity.IdTipoServicio = 1;
            entity.IdClasificacionFactor = 1;
            entity.IdMedidaCobro = 1;
            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.CuotaFactor = 1;
            entity.FechaAutorizacion = dtTest;
            entity.FechaEntradaVigor = dtTest;
            entity.FechaTermino = dtTest;
            entity.FechaPublicacionDof = dtTest;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = false;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFactores.Guardar(entity);

            List<Factor> listaGuardados = negocioFactores.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.Nombre == strTestName);

            if (obj != null)
            {
                obj.Nombre = "XXX";
                negocioFactores.Guardar(obj);
            }

            var statusObj = negocioFactores.CambiarEstatus(obj);

            var foundObj = negocioFactores.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.Nombre, obj.Nombre);
        }

        #endregion

        #region MEDIDAS_COBRO

        [TestMethod]
        public void TestGuardarMedidasCobro()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            MedidaCobro entity = new MedidaCobro();

            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioMedidasCobro.Guardar(entity);

            List<MedidaCobro> listaGuardados = negocioMedidasCobro.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == strTestName).Nombre, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateMedidasCobro()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            MedidaCobro entity = new MedidaCobro();

            entity.Nombre = strTestName;
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

        #region CLASIFICACION_FACTOR

        [TestMethod]
        public void TestGuardarClasificacionFactor()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            ClasificacionFactor entity = new ClasificacionFactor();

            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioClasificacionFactor.Guardar(entity);

            List<ClasificacionFactor> listaGuardados = negocioClasificacionFactor.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.Nombre == strTestName).Nombre, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateClasificacionFactor()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            ClasificacionFactor entity = new ClasificacionFactor();

            entity.Nombre = strTestName;
            entity.Descripcion = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = dtTest.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioClasificacionFactor.Guardar(entity);

            List<ClasificacionFactor> listaGuardados = negocioClasificacionFactor.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.Nombre == strTestName);

            if (obj != null)
            {
                obj.Nombre = "XXX";
                negocioClasificacionFactor.Guardar(obj);
            }

            var statusObj = negocioClasificacionFactor.CambiarEstatus(obj);

            var foundObj = negocioClasificacionFactor.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.Nombre, obj.Nombre);
        }

        #endregion

        // Daniel Gil

        #region FACTOR_ENTIDAD_FEDERATIVA

        //[TestMethod]
        //public void TestGuardarFactorEntidadFederativa()
        //{
        //    var dtTest = DateTime.Now;
        //    var strTestName = DateTime.Now.ToString();

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
        //    var strTestName = DateTime.Now.ToString();

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
        //    var strTestName = DateTime.Now.ToString();

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
        //    var strTestName = DateTime.Now.ToString();

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
            var strTestName = DateTime.Now.ToString();

            FactorLeyIngreso entity = new FactorLeyIngreso();

            entity.NombreLI = strTestName;
            entity.DescLI = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.IdAnio = 1;
            entity.IdMes = 1;
            entity.Factor = 1;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFactoresLeyIngreso.Guardar(entity);

            List<FactorLeyIngreso> listaGuardados = negocioFactoresLeyIngreso.ObtenerPorCriterio(paging, entity).ToList();

            Assert.AreEqual(listaGuardados.Find(x => x.NombreLI == strTestName).NombreLI, strTestName.ToString());
        }

        [TestMethod]
        public void TestUpdateFactoresLeyIngreso()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

            FactorLeyIngreso entity = new FactorLeyIngreso();

            entity.NombreLI = strTestName;
            entity.DescLI = "Test desde proyecto de pruebas,  el nombre es igual a la fecha de creación";
            entity.IdAnio = 1;
            entity.IdMes = 1;
            entity.Factor = 1;
            entity.FechaInicial = dtTest.AddDays(1);
            entity.FechaFinal = entity.FechaInicial.AddDays(2);
            entity.Activo = true;
            Paging paging = new Paging { CurrentPage = 1, Rows = 20 };
            negocioFactoresLeyIngreso.Guardar(entity);

            List<FactorLeyIngreso> listaGuardados = negocioFactoresLeyIngreso.ObtenerTodos(paging).ToList();

            var obj = listaGuardados.Find(x => x.NombreLI == strTestName);

            if (obj != null)
            {
                obj.NombreLI = "XXX";
            }

            var statusObj = negocioFactoresLeyIngreso.CambiarEstatus(obj);

            var foundObj = negocioFactoresLeyIngreso.ObtenerPorId(obj.Identificador);

            Assert.AreEqual(foundObj.NombreLI, obj.NombreLI);
        }

        #endregion

        #region FACTORES_ACTIVIDAD_ECONOMICA

        [TestMethod]
        public void TestGuardarFactoresActividadEconomica()
        {
            var dtTest = DateTime.Now;
            var strTestName = DateTime.Now.ToString();

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
            var strTestName = DateTime.Now.ToString();

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
    }
}
