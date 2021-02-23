using System.Web.Mvc;

namespace QuinelaWeb.Web.Controllers
{
    public class AboutController : QuinelaWebControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}