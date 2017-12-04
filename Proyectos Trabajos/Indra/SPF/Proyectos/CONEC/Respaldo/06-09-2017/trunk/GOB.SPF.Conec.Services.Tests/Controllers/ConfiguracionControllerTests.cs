namespace GOB.SPF.Conec.Services.Tests.Controllers
{
    using ConecII.Business;
    using ConecII.Entities;
    using ConecII.Entities.Request;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                    EsCorreo=true,
                    EsSistema=true,
                    EmitirAlerta=true,
                    TiempoAlerta = 4,
                    Frecuencia=20,
                    AlertaEsCorreo=true,
                    AlertaEsSistema = true,
                    CuerpoAlerta = "Cuerpo Alerta"
                },
                Paging = new Paging
                {
                    All = true
                },
                Usuario = "Usuario"
            };

            var business = new NotificacionesBusiness();
            var exitoso = business.Guardar(entity.Item);

            Assert.IsTrue(exitoso);
        }
    }
}