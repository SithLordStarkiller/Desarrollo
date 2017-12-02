using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GOB.SPF.ConecII.Web.Servicios;

namespace GOB.SPF.ConecII.Web.Controllers.Tests
{
    [TestClass()]
    public class CatalogoControllerTests
    {
        [TestMethod()]
        public void ObtenerDivisionTest()
        {
            var divList = new UiResultPage<UiDivision>();

            var clientService = new ServicesCatalog();

            divList.List = clientService.ObtenerDivisiones(1, 20);

           
            Assert.IsTrue(divList.List.Count > 0);
        }

        [TestMethod()]
        public void ObtenerGrupoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerTipoServicioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerClasificacionTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerFactorTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerAnioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerDependenciaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerTipoDocumentoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerPeriodoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerReferenciasTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerGastosInherentesTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloControlTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloControlConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesModulosControlObtenerListadoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesmodulosControlConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloControlTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloControlGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloControlCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlesConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlesObtenerListadoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlesConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ControlCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesModulosConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesModulosObtenerListadoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesModulosConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolModuloCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolUsuarioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesUsuariosConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesUsuariosObtenerListadoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesUsuariosConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolUsuarioTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolUsuarioGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolUsuarioCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuarioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuariosConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuariosObtenerListadoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuariosConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuarioTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuarioGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UsuarioCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesObtenerListadoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolesConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RolCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModulosTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModulosConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModulosObtenerListadoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModulosConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModuloTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModuloGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ModuloCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DivisionesTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DivisionesConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DivisionesObtenerListadoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DivisionesConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DivisionTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DivisionGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DivisionCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TiposDocumentoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TipoDocumentoConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TipoDocumentoConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TipoDocumentoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TipoDocumentoGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TipoDocumentoCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GrupoConsultaPorIdDivisionTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TiposServiciosTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TiposServicioConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TiposServicioConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TiposServicioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TiposServicioGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TiposServicioCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GruposTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GrupoConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GrupoConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GrupoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GrupoGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GrupoCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ClasificacionesFactorTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ClasificacionFactorConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ClasificacionFactorConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ClasificacionFactorTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ClasificacionFactorGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ClasificacionFactorCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DependenciasTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DependenciaConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DependenciaConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DependenciaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DependenciaGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DependenciaCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorEntidadesFederativasTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorEntidadFederativaConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorEntidadFederativaConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerFactoresTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorEntidadFederativaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorEntidadFederativaGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorEntidadFederativaCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ClasificacionObtieneEstadosTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorLeyIngresosTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorLeyIngresoConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorLeyIngresoConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorLeyIngresoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorLeyIngresoGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorLeyIngresoCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactoresTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorObtenerPorClasificacionTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ClasificacionObtieneFactorTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactoresMunicipioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorMunicipioConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorMunicipioConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorMunicipioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorMunicipioGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FactorMunicipioCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ReferenciasTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ReferenciasConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ReferenciasConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ReferenciaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ReferenciaGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ReferenciaCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GastosInherentesTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GastosInherentesConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GastosInherentesConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GastosInherenteTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GastosInherenteGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GastosInherenteCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PeriodosTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PeriodosConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PeriodosConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PeriodoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PeriodoGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void PeriodoCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FraccionesTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FraccionTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FraccionConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FraccionConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FraccionGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FraccionCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void FraccionEnlistarGrupoPorDivisionTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CuotasTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CuotasConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CuotaConsultaCriterioTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CuotaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CuotaGuardarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CuotaCambiarEstatusTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void MunicipiosObtenerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerEstadoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void AreasConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RegimenFiscalConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TipoPagoConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ActividadesConsultaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ObtenerMensajeOperacionTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SectorConsultaTest()
        {
            throw new NotImplementedException();
        }
    }
}