using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Controllers
{
    public interface IControllerCrud<TEntity, TViewModel>
    {
        ActionResult Index();
        IPagedView FindItems(TViewModel find);
        PartialViewResult FindPartial(TViewModel find);
        PartialViewResult Find(TViewModel find);

        List<TEntity> FindPaged(TViewModel find);

        PartialViewResult AddModifyItem(TViewModel find);

        [HttpPost]
        ActionResult Index(TEntity item, TViewModel find);

        bool Save(TEntity item, TViewModel find);
    }
}
