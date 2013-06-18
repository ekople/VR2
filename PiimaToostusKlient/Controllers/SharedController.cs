using System.Web.Mvc;

namespace PiimaToostusKlient.Controllers
{
    public class SharedController : Controller
    {
        public ActionResult WelcomePage()
        {
            return View();
        }
    }
}