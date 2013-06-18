using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PiimaToostusKlient.Models;
using PiimaToostusKlient.PtServiceRef;
using PiimaToostusKlient.Utils;

namespace PiimaToostusKlient.Controllers
{
    public class GraafikudController : BaseController
    {
        private static IList<IsikGraafik> _allIsikGraafikud;
        private static IList<Atribuutika> _allAtribuutikad;
        private static IList<Osakond> _allOsakonnad;
        private static IList<Isik> _allIsikud;
        
        public ActionResult Graafikud()
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            GraafikudModel model = new GraafikudModel();

            var request = new GetAllIsikuGraafikudRequest();
            request.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllIsikuGraafikudResponse response;
            try
            { response = PtServiceHelper.GetServiceInstance().GetAllIsikuGraafikud(request); }
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
            if (response.GetAllIsikuGraafikudResult.AuthResponse.IsAuthenticated != true)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = response.GetAllIsikuGraafikudResult.AuthResponse.AuthException.Message;
                model.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }
            if (response.GetAllIsikuGraafikudResult.Successful != true)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = response.GetAllIsikuGraafikudResult.Exception.Message;
                model.UserMsg.Pealkiri = "Graafikute pärimine ebaõnnestus!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }


            UpdateAuthContext(response.GetAllIsikuGraafikudResult.AuthResponse);
            model.AllIsikGraafikud = response.GetAllIsikuGraafikudResult.AllIsikuGraafikud;
            _allIsikGraafikud = response.GetAllIsikuGraafikudResult.AllIsikuGraafikud;
            return View(model);
        }

        [HttpGet]
        public ActionResult MuudaGraafikut(int? id)
        {
            GraafikModel model = new GraafikModel();

            foreach (IsikGraafik IskGrph in _allIsikGraafikud)
            {
                if (IskGrph.ID == id)
                {
                    model.ID = IskGrph.ID;
                    if (IskGrph.AtribuutikaID != null)
                    { model.AtribuutikaID = IskGrph.AtribuutikaID.ID; }
                    if (IskGrph.OsakondID != null)
                    { model.OsakondID = IskGrph.OsakondID.ID; }
                    if (IskGrph.IsikID != null)
                    { model.IsikID = IskGrph.IsikID.ID; }
                    model.AlgusKP = Utils.Utils.GetKuvatavDate(IskGrph.AlgusKP);
                    model.LoppKP = Utils.Utils.GetKuvatavDate(IskGrph.LoppKP);
                    model.Kommentaar = IskGrph.Kommentaar;
                    break;
                }
            } return RedirectToAction("LisaGraafik", "Graafikud", model);
        }

        [HttpGet]
        public ActionResult KustutaGraafik(int? id)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }
            return RedirectToAction("Graafikud", "Graafikud");
        }

        [HttpGet]
        public ActionResult LisaGraafik(GraafikModel model)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            GraafikModel passModel = null;
            //tegemist on uue atribuutika loomisega
            if (model.ID == null)
            { passModel = new GraafikModel(); }
            // tegemist on olemasoleva atribuutika muutmisega
            else
            { passModel = model; }

            var request1 = new GetAllAtribuutikaRequest();
            request1.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllAtribuutikaResponse response1;
            try
            { response1 = PtServiceHelper.GetServiceInstance().GetAllAtribuutika(request1); }
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
            if (response1.GetAllAtribuutikaResult.AuthResponse.IsAuthenticated != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response1.GetAllAtribuutikaResult.AuthResponse.AuthException.Message;
                passModel.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response1.GetAllAtribuutikaResult.Successful != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response1.GetAllAtribuutikaResult.Exception.Message;
                passModel.UserMsg.Pealkiri = "Graafikute pärimine ebaõnnestus!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }

            UpdateAuthContext(response1.GetAllAtribuutikaResult.AuthResponse);
            _allAtribuutikad = response1.GetAllAtribuutikaResult.AllAtribuutika;
            passModel.Atribuutikad =
                GetAtribdSelectList(_allAtribuutikad, passModel.AtribuutikaID.GetValueOrDefault(0));

            var request2 = new GetAllOsakonnadRequest();
            request2.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllOsakonnadResponse response2;
            try
            { response2 = PtServiceHelper.GetServiceInstance().GetAllOsakonnad(request2); }
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
            if (response2.GetAllOsakonnadResult.AuthResponse.IsAuthenticated != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response2.GetAllOsakonnadResult.AuthResponse.AuthException.Message;
                passModel.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response2.GetAllOsakonnadResult.Successful != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response2.GetAllOsakonnadResult.Exception.Message;
                passModel.UserMsg.Pealkiri = "Graafikute pärimine ebaõnnestus!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }

            UpdateAuthContext(response2.GetAllOsakonnadResult.AuthResponse);
            _allOsakonnad = response2.GetAllOsakonnadResult.AllOsakonnad;
            passModel.Osakonnad =
                GetOsknndSelectList(_allOsakonnad, passModel.OsakondID.GetValueOrDefault(0));

            var request3 = new GetAllIsikudRequest();
            request3.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllIsikudResponse response3;
            try
            { response3 = PtServiceHelper.GetServiceInstance().GetAllIsikud(request3); }
            catch (Exception exception)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = exception.Message;
                passModel.UserMsg.Pealkiri = "Graafiku pärimisel tekkis tehiline tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response3 == null)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = "Graafikute pärimine ebaõnnestus kuna teenuselt ei õnnestunud vastust saada!";
                passModel.UserMsg.Pealkiri = "Graafiku pärimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response3.GetAllIsikudResult.AuthResponse.IsAuthenticated != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response3.GetAllIsikudResult.AuthResponse.AuthException.Message;
                passModel.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response3.GetAllIsikudResult.Successful != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response3.GetAllIsikudResult.Exception.Message;
                passModel.UserMsg.Pealkiri = "Graafikute pärimine ebaõnnestus!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }

            UpdateAuthContext(response3.GetAllIsikudResult.AuthResponse);
            _allIsikud = response3.GetAllIsikudResult.AllIsikud;
            passModel.Isikud =
                GetIskdSelectList(_allIsikud, passModel.IsikID.GetValueOrDefault(0));

            
            return View(passModel);
        }

        [HttpPost]
        public ActionResult LisaGraafik(GraafikModel model, bool? nullB)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            IsikGraafik attribToCommit = new IsikGraafik();

            if (ModelState.IsValid)
            {
                if (model.ID != null)
                {
                    attribToCommit.ID = (int)model.ID;
                    attribToCommit.AlgusKP = System.DateTime.Parse(model.AlgusKP);
                    attribToCommit.LoppKP = System.DateTime.Parse(model.LoppKP);
                    attribToCommit.Kommentaar = model.Kommentaar;
                    if (model.AtribuutikaID != null || model.AtribuutikaID != 0)
                    {
                        foreach (var atribuutik in _allAtribuutikad)
                        {
                            if (atribuutik.ID == model.AtribuutikaID.Value)
                            {
                                attribToCommit.AtribuutikaID = atribuutik;
                                break;
                            }
                        }
                    }
                    if (model.IsikID != null || model.IsikID != 0)
                    {
                        foreach (var iskk in _allIsikud)
                        {
                            if (iskk.ID == model.IsikID.Value)
                            {
                                attribToCommit.IsikID = iskk;
                                break;
                            }
                        }
                    }
                    if (model.OsakondID != null || model.OsakondID != 0)
                    {
                        foreach (var oskk in _allOsakonnad)
                        {
                            if (oskk.ID == model.OsakondID.Value)
                            {
                                attribToCommit.OsakondID = oskk;
                                break;
                            }
                        }
                    }
                }

                var request = new UpdateIsikGraafikRequest();
                request.isikGraafik = attribToCommit;
                request.sessionHandle = GetCurrentContext().SessionHandle;

                UpdateIsikGraafikResponse response;
                try
                { response = PtServiceHelper.GetServiceInstance().UpdateIsikGraafik(request); }
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
                if (response.UpdateIsikGraafikResult.AuthResponse.IsAuthenticated != true)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = response.UpdateIsikGraafikResult.AuthResponse.AuthException.Message;
                    model.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }
                if (response.UpdateIsikGraafikResult.Successful != true)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = response.UpdateIsikGraafikResult.Exception.Message;
                    model.UserMsg.Pealkiri = "Atribuutika muutmine ebaõnnestus!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }

                return RedirectToAction("Graafikud", "Graafikud");
            }
            return View(model);
        }

        public static SelectList GetAtribdSelectList(IList<Atribuutika> atribd, int selValue)
        {
            IList<Atribuutika> listOfattrib = new List<Atribuutika>();
            var attrib = new Atribuutika();
            attrib.ID = 0;
            attrib.Nimetus = "-";
            listOfattrib.Add(attrib);

            if (atribd != null && atribd.Count > 0)
            {
                foreach (Atribuutika atr in atribd)
                { listOfattrib.Add(atr); }
            }

            return new SelectList(listOfattrib, "ID", "Nimetus", selValue);
        }

        public static SelectList GetIskdSelectList(IList<Isik> iskd, int selValue)
        {
            IList<IsikExt> listOfisk = new List<IsikExt>();
            var isk = new Isik();
            isk.ID = 0;
            isk.Eesnimi = "-";
            isk.Perenimi = "-";
            listOfisk.Add(new IsikExt(isk));

            if (iskd != null && iskd.Count > 0)
            {
                foreach (Isik atr in iskd)
                { listOfisk.Add(new IsikExt(atr)); }
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

        public static SelectList GetOsknndSelectList(IList<Osakond> Osknd, int selValue)
        {
            IList<Osakond> listOfoskd = new List<Osakond>();
            var ossk = new Osakond();
            ossk.ID = 0;
            ossk.Nimetus = "-";
            listOfoskd.Add(ossk);

            if (Osknd != null && Osknd.Count > 0)
            {
                foreach (Osakond atrLiik in Osknd)
                { listOfoskd.Add(atrLiik); }
            }

            return new SelectList(listOfoskd, "ID", "Nimetus", selValue);
        }
    }
}
