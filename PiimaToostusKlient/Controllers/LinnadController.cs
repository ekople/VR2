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
    public class LinnadController : Controller
    {
        [HttpGet]
        public ActionResult LisaLinn()
        {
            var passedModel = new LinnModel();
            passedModel.Riigid = new SelectList(new[] { "Eesti", "Vene", "Britti" }, "Eesti");
            return View(passedModel);
        }

        [HttpPost]
        public ActionResult LisaLinn(LinnModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("LisaHoone", "Hooned");
            }
            else return View(model);
        }
    }
}
