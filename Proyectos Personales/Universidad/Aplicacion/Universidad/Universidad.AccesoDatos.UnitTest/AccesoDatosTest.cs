using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Universidad.AccesoDatos.UnitTest
{
    [TestClass]
    public class AccesoDatosTest
    {
        [TestMethod]
        public void CrearClasePersonas()
        {
            var clase = new Universidad.AccesoDatos.Personas.Personas();
        }
    }
}
