namespace GOB.SPF.Conec.Services.Tests.Controllers
{
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

    [TestClass]
    public class ConfiguracionControllerTests
    {

        [TestMethod]
        public void NotificacionesGuardarTest()
        {
            var entity = new RequestNotificaciones
            {
                Item = new Notificaciones
                {
                    IdNotificacion = 0,
                    IdTipoServicio = 1,
                    IdActividad = 1,
                    CuerpoCorreo = "Cuerpo Correo",
                    EsCorreo = true,
                    EsSistema = true,
                    EmitirAlerta = true,
                    TiempoAlerta = 4,
                    Frecuencia = 20,
                    AlertaEsCorreo = true,
                    AlertaEsSistema = true,
                    CuerpoAlerta = "Cuerpo Alerta"
                },
                Paging = new Paging
                {
                    All = true
                },
                Usuario = "Usuario"
            };

            //var business = new NotificacionesBusiness();
            //var exitoso = business.Guardar(entity.Item);

            //Assert.IsTrue(exitoso);
        }

        [TestMethod()]
        public void NotificacionesObtenerPorIdTest()
        {
        }

        [TestMethod()]
        public void NotificacionesObtenerTodosTest()
        {
        }

        [TestMethod()]
        public void ReceptoresAlertaGuardarListaTest()
        { 
        }

        [TestMethod()]
        public void ReceptoresAlertaObtenerTodosTest()
        {
        }

        [TestMethod()]
        public void AreasValidadorasGuadarTest()
        {
            business.AreasValidadorasBusiness business = new business.AreasValidadorasBusiness();

            List<AreasValidadoras> listAreaV = new List<AreasValidadoras>();
            listAreaV.Add(new AreasValidadoras() { IdAreasValidadoras = 0, IdActividad = 6, IdCentroCosto = "10000", IdTipoServicio = 1, CentroCostos = "", TipoServicio = "", Actividad = "", EsActivo = true, Obligatorio = true });
            listAreaV.Add(new AreasValidadoras() { IdAreasValidadoras = 0, IdActividad = 6, IdCentroCosto = "100100", IdTipoServicio = 1, CentroCostos = "", TipoServicio = "", Actividad = "", EsActivo = true, Obligatorio = true });
            listAreaV.Add(new AreasValidadoras() { IdAreasValidadoras = 0, IdActividad = 6, IdCentroCosto = "100200", IdTipoServicio = 1, CentroCostos = "", TipoServicio = "", Actividad = "", EsActivo = true, Obligatorio = true });

            var result = business.InsertarTabla(listAreaV);
        }

        [TestMethod()]
        public void AreasValidadorasObtenerTodosTest()
        {
        }

        [TestMethod()]
        public void AreasValidadorasActualizarTest()
        {
        }

        [TestMethod()]
        public void AreasValidadorasModificarEstatusTest()
        {
        }

        [TestMethod()]
        public void TestObtenerConfiguracionTipoServicioArea()
        {
            business.ConfiguracionServicioBussines negocioConfigTipoServicioArea = new business.ConfiguracionServicioBussines();
            ConfiguracionTipoServicioArea X = negocioConfigTipoServicioArea.ObtenerConfiguracionTipoServicioArea(1, "250310");

            Assert.IsNotNull(X);
        }

    }
}