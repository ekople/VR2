using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PiimaToostusKlient.PtServiceRef;
using System.Web.UI.WebControls;
using PiimaToostusKlient.Models;

namespace PiimaToostusKlient.Controllers
{
    public class OsakonnaLiigidController : Controller
    {
        [HttpGet]
        public ActionResult LisaOsakonnaLiik()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LisaOsakonnaLiik(OsakonnaLiikModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("LisaOsakond", "Osakonnad");
            }
            else return View();
        }
    }
}
