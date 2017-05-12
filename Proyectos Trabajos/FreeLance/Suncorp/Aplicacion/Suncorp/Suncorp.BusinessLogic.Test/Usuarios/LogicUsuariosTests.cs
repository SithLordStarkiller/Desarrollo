namespace Suncorp.BusinessLogic.Usuarios.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class LogicUsuariosTests
    {
        [TestMethod()]
        public void ObtenerUsuarioLoginTest()
        {
            try
            {
                var result = new LogicUsuarios().ObtenerUsuarioLogin("ecruzlagunes", "A@141516182235");
                Assert.IsTrue(result != null);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}