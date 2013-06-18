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
    public class RiigidController : Controller
    {
        [HttpGet]
        public ActionResult LisaRiik()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LisaRiik(RiikModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("LisaLinn", "Linnad");
            }
            else return View(model);
        }
    }
}
