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
    public class HoonedController : Controller
    {
        [HttpGet]
        public ActionResult LisaHoone()
        {
            var passedModel = new HooneModel();
            passedModel.Linnad = new SelectList(new[] { "Tallinn", "Tartu", "Valga" }, "Tallinn");
            return View(passedModel);
        }

        [HttpPost]
        public ActionResult LisaHoone(HooneModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("LisaOsakond", "Osakonnad");
            }
            else return View(model);
        }
    }
}
