namespace Broxel.Site.Controllers
{
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Controller.ControladorUsuarios;


    public class IndexController : Controller
    {
        // GET: Index
        public async Task<ActionResult> Index()
        {
            var user = await new ControladorUsuarios().ObtenUsUsuarionPorLogin("ecruzlagunes", "A@141516182235");
            return View(user);
        }
    }
}