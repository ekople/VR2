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
    public class AtribuutikaLiigidController : Controller
    {
        [HttpGet]
        public ActionResult LisaAtribuutikaLiik(AtribuutikaLiikModel model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult LisaAtribuutikaLiik(AtribuutikaLiikModel model, bool? nullB)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("LisaAtribuutika", "Atribuutikad");
            }
            else return View(model);
        }
    }
}
