using Microsoft.VisualStudio.TestTools.UnitTesting;
using Universidad.Entidades;
using Universidad.Helpers;
using Universidad.ServidorInterno.Personas;

namespace Universidad.AccesoDatos.UnitTest
{
    [TestClass]
    public class AccesoDatosTest
    {
        [TestMethod]
        public void CrearClasePersonas()
        {
            new Personas.Personas();
        }

        [TestMethod]
        public void CreaServicioPersonas()
        {
            new SPersonas();
        }

        [TestMethod]
        public void Interface()
        {
            new Repositorio<PER_PERSONAS>();
        }

        [TestMethod]
        public void TestEncriptacion()
        {
            new Encriptacion().EncriptarTexto("prueba");
        }

        [TestMethod]
        public void TestDesencriptacion()
        {
            new Encriptacion().DesencriptarTexto("cuAuSbzsPHs5D9Bq33w17w==");
        }
    }
}
