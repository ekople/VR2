using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PiimaToostusKlient.Models;
using PiimaToostusKlient.PtServiceRef;
using PiimaToostusKlient.Utils;

namespace PiimaToostusKlient.Controllers
{
    public class KasutajadController : BaseController
    {
        private static IList<Kasutaja> _allKasutajad;
        private static IList<Isik> _allIsikud;
        private static IList<Roll> _allRollid;
        public ActionResult Kasutajad()
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            KasutajadModel model = new KasutajadModel();

            var request = new GetAllUsersRequest();
            request.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllUsersResponse response;
            try
            { response = PtServiceHelper.GetServiceInstance().GetAllUsers(request); }
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
            if (response.GetAllUsersResult.AuthResponse.IsAuthenticated != true)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = response.GetAllUsersResult.AuthResponse.AuthException.Message;
                model.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }
            if (response.GetAllUsersResult.Successful != true)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = response.GetAllUsersResult.Exception.Message;
                model.UserMsg.Pealkiri = "Graafikute pärimine ebaõnnestus!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }


            UpdateAuthContext(response.GetAllUsersResult.AuthResponse);
            model.AllKasutajad = response.GetAllUsersResult.AllUsers;
            _allKasutajad = response.GetAllUsersResult.AllUsers;
            return View(model);
        }

        [HttpGet]
        public ActionResult MuudaKasutajat(int? id)
        {
            KasutajaModel model = new KasutajaModel();

            foreach (Kasutaja kst in _allKasutajad)
            {
                if (kst.ID == id)
                {
                    model.ID = kst.ID;
                    if (kst.IsikID != null)
                    { model.IsikID = kst.IsikID.ID; }
                    if (kst.RollID != null)
                    { model.RollID = kst.RollID.ID; }
                    model.AlgusKP = Utils.Utils.GetKuvatavDate(kst.AlgusKP);
                    model.LoppKP = Utils.Utils.GetKuvatavDate(kst.LoppKP);
                    model.KasutajaNimi = kst.KasutajaNimi;
                    model.Password = "pass";
                    break;
                }
            }

            return RedirectToAction("LisaKasutaja", "Kasutajad", model);
        }

        [HttpGet]
        public ActionResult KustutaKasutaja(int? id)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }
            return RedirectToAction("Kasutajad", "Kasutajad");
        }

        [HttpGet]
        public ActionResult LisaKasutaja(KasutajaModel model)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            KasutajaModel passModel = null;
            //tegemist on uue atribuutika loomisega
            if (model.ID == null)
            { passModel = new KasutajaModel(); }
            // tegemist on olemasoleva atribuutika muutmisega
            else
            { passModel = model; }

            #region rollid

            var request = new GetAllRollidRequest();
            request.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllRollidResponse response;
            try
            { response = PtServiceHelper.GetServiceInstance().GetAllRollid(request); }
            catch (Exception exception)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = exception.Message;
                passModel.UserMsg.Pealkiri = "Atribuutika pärimisel tekkis tehiline tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response == null)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = "Atribuutikate pärimine ebaõnnestus kuna teenuselt ei õnnestunud vastust saada!";
                passModel.UserMsg.Pealkiri = "Atribuutika pärimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response.GetAllRollidResult.AuthResponse.IsAuthenticated != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response.GetAllRollidResult.AuthResponse.AuthException.Message;
                passModel.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response.GetAllRollidResult.Successful != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response.GetAllRollidResult.Exception.Message;
                passModel.UserMsg.Pealkiri = "Atribuutikate pärimine ebaõnnestus!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }

            UpdateAuthContext(response.GetAllRollidResult.AuthResponse);
            _allRollid = response.GetAllRollidResult.AllRollid;
            passModel.RollList =
                GetRollidSelectList(_allRollid, passModel.RollID.GetValueOrDefault(0));

            #endregion

            #region Isikud

            var request2 = new GetAllIsikudRequest();
            request2.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllIsikudResponse response2;
            try
            { response2 = PtServiceHelper.GetServiceInstance().GetAllIsikud(request2); }
            catch (Exception exception)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = exception.Message;
                passModel.UserMsg.Pealkiri = "Atribuutika pärimisel tekkis tehiline tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response2 == null)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = "Atribuutikate pärimine ebaõnnestus kuna teenuselt ei õnnestunud vastust saada!";
                passModel.UserMsg.Pealkiri = "Atribuutika pärimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response2.GetAllIsikudResult.AuthResponse.IsAuthenticated != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response2.GetAllIsikudResult.AuthResponse.AuthException.Message;
                passModel.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response2.GetAllIsikudResult.Successful != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response2.GetAllIsikudResult.Exception.Message;
                passModel.UserMsg.Pealkiri = "Atribuutikate pärimine ebaõnnestus!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }

            UpdateAuthContext(response.GetAllRollidResult.AuthResponse);
            _allIsikud = response2.GetAllIsikudResult.AllIsikud;
            passModel.Isikud =
                GetIskdSelectList(_allIsikud, passModel.RollID.GetValueOrDefault(0));

            #endregion

            return View(passModel);
        }

        [HttpPost]
        public ActionResult LisaKasutaja(KasutajaModel model, bool? nullB)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Kasutajad", "Kasutajad");
            }
            else return View(model);
        }

        public static SelectList GetRollidSelectList(IList<Roll> rollid, int selValue)
        {
            IList<Roll> listOfRollid = new List<Roll>();
            var nullElem = new Roll();
            nullElem.ID = 0;
            nullElem.Nimetus = "-";
            listOfRollid.Add(nullElem);

            if (rollid != null && rollid.Count > 0)
            {
                foreach (Roll roll in rollid)
                { listOfRollid.Add(roll); }
            }

            return new SelectList(listOfRollid, "ID", "Nimetus", selValue);
        }

        public static SelectList GetIskdSelectList(IList<Isik> iskd, int selValue)
        {
            IList<GraafikudController.IsikExt> listOfisk = new List<GraafikudController.IsikExt>();
            var isk = new Isik();
            isk.ID = 0;
            isk.Eesnimi = "-";
            isk.Perenimi = "-";
            listOfisk.Add(new GraafikudController.IsikExt(isk));

            if (iskd != null && iskd.Count > 0)
            {
                foreach (Isik atr in iskd)
                { listOfisk.Add(new GraafikudController.IsikExt(atr)); }
            }

            return new SelectList(listOfisk, "ID", "KuvatavVaartus", selValue);
        }

        public class IsikExt
        {
            private int _iD;
            private string _kuvatavVaartus;

            public IsikExt(Isik isik)
            {
                this._iD = isik.ID;
                if (isik.ID != 0)
                {
                    this._kuvatavVaartus = isik.Eesnimi + " " + isik.Perenimi + " (" + isik.Isikukood + ")";
                }
                else
                { this._kuvatavVaartus = "-"; }
            }

            public string KuvatavVaartus
            { get { return _kuvatavVaartus; } }

            public int ID
            { get { return _iD; } }
        }
    }
}
