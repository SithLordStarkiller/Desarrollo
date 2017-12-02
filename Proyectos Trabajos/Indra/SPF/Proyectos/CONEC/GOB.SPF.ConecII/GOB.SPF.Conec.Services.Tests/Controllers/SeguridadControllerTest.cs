using System;
using System.Linq;
using GOB.SPF.ConecII.Business;
using GOB.SPF.ConecII.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GOB.SPF.Conec.Services.Tests.Controllers
{
    [TestClass]
    public class SeguridadControllerTest
    {
        #region variables privadas
        private ModuloBusiness moduloBusiness = new ModuloBusiness();
        #endregion

        #region Modulos
        [TestMethod]
        public void ObtenerModulosTodosTest()
        {
            var paging = new Paging
            {
                All = true,
                CurrentPage = 1,
                Pages = 1,
                Rows = 20
            };
            var list = moduloBusiness.ObtenerTodos(paging);
            Assert.IsTrue(list.Any());
        }
        #endregion
    }
}
