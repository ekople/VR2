using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PiimaToostusKlient.Models;
using PiimaToostusKlient.PtServiceRef;
using PiimaToostusKlient.Utils;

namespace PiimaToostusKlient.Controllers
{
    public class IsikudController : BaseController
    {
          public AuthResponse UserContext;

          private static IList<Isik> _allIsikud;

        public IsikudController()
        {
           // UserContext = HttpContext.Session["UserContext"] as AuthResponse;
        }

        public ActionResult Isikud()
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            IsikudModel model = new IsikudModel();

            var request = new GetAllIsikudRequest();
            request.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllIsikudResponse response;
            try
            { response = PtServiceHelper.GetServiceInstance().GetAllIsikud(request); }
            catch (Exception exception)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = exception.Message;
                model.UserMsg.Pealkiri = "Atribuutika pärimisel tekkis tehiline tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }
            if (response == null)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = "Atribuutikate pärimine ebaõnnestus kuna teenuselt ei õnnestunud vastust saada!";
                model.UserMsg.Pealkiri = "Atribuutika pärimisel tekkis tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }
            if (response.GetAllIsikudResult.AuthResponse.IsAuthenticated != true)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = response.GetAllIsikudResult.AuthResponse.AuthException.Message;
                model.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }
            if (response.GetAllIsikudResult.Successful != true)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = response.GetAllIsikudResult.Exception.Message;
                model.UserMsg.Pealkiri = "Atribuutikate pärimine ebaõnnestus!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }


            UpdateAuthContext(response.GetAllIsikudResult.AuthResponse);
            model.AllIsikud = response.GetAllIsikudResult.AllIsikud;
            _allIsikud = response.GetAllIsikudResult.AllIsikud;
            return View(model);
        }

        [HttpGet]
        public ActionResult LooIsik(IsikModel model)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            IsikModel passModel = null;
            //tegemist on uue atribuutika loomisega
            if (model.ID == null)
            { passModel = new IsikModel(); }
            // tegemist on olemasoleva atribuutika muutmisega
            else
            { passModel = model; }

            return View(passModel);
        }

        [HttpPost]
        public ActionResult LooIsik(IsikModel model, bool? nullB)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            Isik IsikToCommit = new Isik();

            if (ModelState.IsValid)
            {
                if (model.ID != null)
                {
                    IsikToCommit.ID = (int)model.ID;
                    IsikToCommit.Eesnimi = model.Eesnimi;
                    IsikToCommit.Perenimi = model.Perenimi;
                    IsikToCommit.Isikukood = model.Isikukood;
                    IsikToCommit.EMail = model.EMail;
                    IsikToCommit.KontaktTelefon = (int)model.KontaktTelefon;
                    IsikToCommit.SynniKP = System.DateTime.Parse(model.SynniKP);
                }

                var request = new UpdateIsikRequest();
                request.isik = IsikToCommit;
                request.sessionHandle = GetCurrentContext().SessionHandle;

                UpdateIsikResponse response;
                try
                { response = PtServiceHelper.GetServiceInstance().UpdateIsik(request); }
                catch (Exception exception)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = exception.Message;
                    model.UserMsg.Pealkiri = "Isiku muutmisel tekkis tehiline tõrge!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }
                if (response == null)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = "Isikute muutmine ebaõnnestus kuna teenuselt ei õnnestunud vastust saada!";
                    model.UserMsg.Pealkiri = "Isiku muutmisel tekkis tõrge!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }
                if (response.UpdateIsikResult.AuthResponse.IsAuthenticated != true)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = response.UpdateIsikResult.AuthResponse.AuthException.Message;
                    model.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }
                if (response.UpdateIsikResult.Successful != true)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = response.UpdateIsikResult.Exception.Message;
                    model.UserMsg.Pealkiri = "Isikute muutmine ebaõnnestus!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }

                return RedirectToAction("Isikud", "Isikud");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult MuudaIsikut(int? id)
        {
            IsikModel model = new IsikModel();

            foreach (Isik Isk in _allIsikud)
            {
                if (Isk.ID == id)
                {
                    model.ID = Isk.ID;
                    model.Eesnimi = Isk.Eesnimi;
                    model.Perenimi = Isk.Perenimi;
                    model.Isikukood = Isk.Isikukood;
                    model.EMail = Isk.EMail;
                    model.KontaktTelefon = Isk.KontaktTelefon;
                    model.SynniKP = Utils.Utils.GetKuvatavDate(Isk.SynniKP);
                    break;
                }
            }

            return RedirectToAction("LooIsik", "Isikud", model);
        }

        [HttpGet]
        public ActionResult KustutaIsik(int? id)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }
            return RedirectToAction("Isikud", "Isikud");
        }

        protected void Tagasi_Click(object sender, EventArgs e)
        {
            Response.Write("Zaq");
        }

    }
}
