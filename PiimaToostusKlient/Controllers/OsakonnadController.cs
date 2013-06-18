using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PiimaToostusKlient.Models;
using PiimaToostusKlient.PtServiceRef;
using PiimaToostusKlient.Utils;

namespace PiimaToostusKlient.Controllers
{
    public class OsakonnadController : BaseController
    {
        private static IList<Osakond> _allOsakonnad;
        private static IList<OsakondLiik> _allOsakonnaLiigid;
        private static IList<Hoone> _allHooned;
        [HttpGet]
        public ActionResult Osakonnad()
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            OsakonnadModel model = new OsakonnadModel();

            var request = new GetAllOsakonnadRequest();
            request.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllOsakonnadResponse response;
            try
            { response = PtServiceHelper.GetServiceInstance().GetAllOsakonnad(request); }
            catch (Exception exception)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = exception.Message;
                model.UserMsg.Pealkiri = "Graafiku pärimisel tekkis tehiline tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }
            if (response == null)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = "Graafikute pärimine ebaõnnestus kuna teenuselt ei õnnestunud vastust saada!";
                model.UserMsg.Pealkiri = "Graafiku pärimisel tekkis tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }
            if (response.GetAllOsakonnadResult.AuthResponse.IsAuthenticated != true)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = response.GetAllOsakonnadResult.AuthResponse.AuthException.Message;
                model.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }
            if (response.GetAllOsakonnadResult.Successful != true)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = response.GetAllOsakonnadResult.Exception.Message;
                model.UserMsg.Pealkiri = "Graafikute pärimine ebaõnnestus!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }

            UpdateAuthContext(response.GetAllOsakonnadResult.AuthResponse);
            model.AllOsakonnad = response.GetAllOsakonnadResult.AllOsakonnad;
            _allOsakonnad = response.GetAllOsakonnadResult.AllOsakonnad;
            return View(model);
        }

        [HttpGet]
        public ActionResult MuudaOsakonda(int? id)
        {
            OsakondModel model = new OsakondModel();

            foreach (Osakond Osk in _allOsakonnad)
            {
                if (Osk.ID == id)
                {
                    model.ID = Osk.ID;
                    model.Nimetus = Osk.Nimetus;
                    if (Osk.HooneID != null)
                    { model.HooneID = Osk.HooneID.ID; }
                    if (Osk.OsakondLiikID != null)
                    { model.OsakondLiik = Osk.OsakondLiikID.ID; }
                    model.AlgusKP = Utils.Utils.GetKuvatavDate(Osk.AlgusKP);
                    model.LoppKP = Utils.Utils.GetKuvatavDate(Osk.LoppKP);
                    break;
                }
            }

            return RedirectToAction("LisaOsakond", "Osakonnad", model);
        }

        [HttpGet]
        public ActionResult KustutaOsakond(int? id)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }
            return RedirectToAction("Osakonnad", "Osakonnad");
        }

        [HttpGet]
        public ActionResult LisaOsakond(OsakondModel model)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            OsakondModel passModel = null;
            //tegemist on uue atribuutika loomisega
            if (model.ID == null)
            { passModel = new OsakondModel(); }
            // tegemist on olemasoleva atribuutika muutmisega
            else
            { passModel = model; }

            var request1 = new GetAllOsakonnaLiigidRequest();
            request1.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllOsakonnaLiigidResponse response1;
            try
            { response1 = PtServiceHelper.GetServiceInstance().GetAllOsakonnaLiigid(request1); }
            catch (Exception exception)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = exception.Message;
                passModel.UserMsg.Pealkiri = "Graafiku pärimisel tekkis tehiline tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response1 == null)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = "Graafikute pärimine ebaõnnestus kuna teenuselt ei õnnestunud vastust saada!";
                passModel.UserMsg.Pealkiri = "Graafiku pärimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response1.GetAllOsakonnaLiigidResult.AuthResponse.IsAuthenticated != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response1.GetAllOsakonnaLiigidResult.AuthResponse.AuthException.Message;
                passModel.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response1.GetAllOsakonnaLiigidResult.Successful != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response1.GetAllOsakonnaLiigidResult.Exception.Message;
                passModel.UserMsg.Pealkiri = "Graafikute pärimine ebaõnnestus!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }

            UpdateAuthContext(response1.GetAllOsakonnaLiigidResult.AuthResponse);
            _allOsakonnaLiigid = response1.GetAllOsakonnaLiigidResult.AllOsakonnaLiigid;
            passModel.OsakondLiigid =
                GetOskndLiikSelectList(_allOsakonnaLiigid, passModel.OsakondLiik.GetValueOrDefault(0));

            var request2 = new GetAllHoonedRequest();
            request2.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllHoonedResponse response2;
            try
            { response2 = PtServiceHelper.GetServiceInstance().GetAllHooned(request2); }
            catch (Exception exception)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = exception.Message;
                passModel.UserMsg.Pealkiri = "Graafiku pärimisel tekkis tehiline tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response2 == null)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = "Graafikute pärimine ebaõnnestus kuna teenuselt ei õnnestunud vastust saada!";
                passModel.UserMsg.Pealkiri = "Graafiku pärimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response2.GetAllHoonedResult.AuthResponse.IsAuthenticated != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response2.GetAllHoonedResult.AuthResponse.AuthException.Message;
                passModel.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response2.GetAllHoonedResult.Successful != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response2.GetAllHoonedResult.Exception.Message;
                passModel.UserMsg.Pealkiri = "Graafikute pärimine ebaõnnestus!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }

            UpdateAuthContext(response2.GetAllHoonedResult.AuthResponse);
            _allHooned = response2.GetAllHoonedResult.AllHooned;
            passModel.Hooned =
                GetHoonedSelectList(_allHooned, passModel.HooneID.GetValueOrDefault(0));

            return View(passModel);
        }

        [HttpPost]
        public ActionResult LisaOsakond(OsakondModel model, bool? nullB)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            Osakond attribToCommit = new Osakond();

            if (ModelState.IsValid)
            {
                if (model.ID != null)
                {
                    attribToCommit.ID = (int)model.ID;
                    attribToCommit.Nimetus = model.Nimetus;
                    attribToCommit.AlgusKP = System.DateTime.Parse(model.AlgusKP);
                    attribToCommit.LoppKP = System.DateTime.Parse(model.LoppKP);

                    if (model.HooneID != null || model.HooneID != 0)
                    {
                        foreach (var hoone in _allHooned)
                        {
                            if (hoone.ID == model.HooneID.Value)
                            {
                                attribToCommit.HooneID = hoone;
                                break;
                            }
                        }
                    }
                    if (model.OsakondLiik != null || model.OsakondLiik != 0)
                    {
                        foreach (var oskLiigid in _allOsakonnaLiigid)
                        {
                            if (oskLiigid.ID == model.OsakondLiik.Value)
                            {
                                attribToCommit.OsakondLiikID = oskLiigid;
                                break;
                            }
                        }
                    }
                }

                var request = new UpdateOsakondRequest();
                request.osakond = attribToCommit;
                request.sessionHandle = GetCurrentContext().SessionHandle;

                UpdateOsakondResponse response;
                try
                { response = PtServiceHelper.GetServiceInstance().UpdateOsakond(request); }
                catch (Exception exception)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = exception.Message;
                    model.UserMsg.Pealkiri = "Atribuutika muutmisel tekkis tehiline tõrge!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }
                if (response == null)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = "Atribuutika muutmine ebaõnnestus kuna teenuselt ei õnnestunud vastust saada!";
                    model.UserMsg.Pealkiri = "Atribuutika muutmisel tekkis tõrge!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }
                if (response.UpdateOsakondResult.AuthResponse.IsAuthenticated != true)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = response.UpdateOsakondResult.AuthResponse.AuthException.Message;
                    model.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }
                if (response.UpdateOsakondResult.Successful != true)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = response.UpdateOsakondResult.Exception.Message;
                    model.UserMsg.Pealkiri = "Atribuutika muutmine ebaõnnestus!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }

                return RedirectToAction("Osakonnad", "Osakonnad");
            }
            return View(model);
        }

        public static SelectList GetOskndLiikSelectList(IList<OsakondLiik> Osknd, int selValue)
        {
            IList<OsakondLiik> listOfoskd = new List<OsakondLiik>();
            var ossk = new OsakondLiik();
            ossk.ID = 0;
            ossk.Nimetus = "-";
            listOfoskd.Add(ossk);

            if (Osknd != null && Osknd.Count > 0)
            {
                foreach (OsakondLiik atrLiik in Osknd)
                { listOfoskd.Add(atrLiik); }
            }

            return new SelectList(listOfoskd, "ID", "Nimetus", selValue);
        }

        public static SelectList GetHoonedSelectList(IList<Hoone> hone, int selValue)
        {
            IList<Hoone> listOfhoned = new List<Hoone>();
            var ossk = new Hoone();
            ossk.ID = 0;
            ossk.Nimetus = "-";
            listOfhoned.Add(ossk);

            if (hone != null && hone.Count > 0)
            {
                foreach (Hoone atrLiik in hone)
                { listOfhoned.Add(atrLiik); }
            }

            return new SelectList(listOfhoned, "ID", "Nimetus", selValue);
        }
    }
}
