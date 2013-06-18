using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PiimaToostusKlient.Models;
using PiimaToostusKlient.PtServiceRef;
using PiimaToostusKlient.Utils;

namespace PiimaToostusKlient.Controllers
{
    public class AtribuutikadController : BaseController
    {
        private static IList<Atribuutika> _allAtribuutikad;
        private static IList<AtribuutikaLiik> _allAtribuutikaLiigid;
        public ActionResult Atribuutikad()
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            AtribuutikadModel model = new AtribuutikadModel();

            var request = new GetAllAtribuutikaRequest();
            request.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllAtribuutikaResponse response;
            try
            { response = PtServiceHelper.GetServiceInstance().GetAllAtribuutika(request); }
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
            if (response.GetAllAtribuutikaResult.AuthResponse.IsAuthenticated != true)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = response.GetAllAtribuutikaResult.AuthResponse.AuthException.Message;
                model.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }
            if (response.GetAllAtribuutikaResult.Successful != true)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = response.GetAllAtribuutikaResult.Exception.Message;
                model.UserMsg.Pealkiri = "Atribuutikate pärimine ebaõnnestus!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }

            UpdateAuthContext(response.GetAllAtribuutikaResult.AuthResponse);
            model.AllAtribuutikad = response.GetAllAtribuutikaResult.AllAtribuutika;
            _allAtribuutikad = response.GetAllAtribuutikaResult.AllAtribuutika;
            return View(model);
        }

        [HttpGet]
        public ActionResult MuudaAtribuutikat(int? id)
        {
            AtribuutikaModel model = new AtribuutikaModel();

            foreach (Atribuutika attrib in _allAtribuutikad)
            {
                if (attrib.ID == id)
                {
                    model.ID = attrib.ID;
                    if (attrib.AtribuutikaLiikID != null)
                    { model.AtribuutikaLiikID = attrib.AtribuutikaLiikID.ID; }
                    model.JargmineHooldusKP = Utils.Utils.GetKuvatavDate(attrib.JargmineHooldusKP);
                    model.Kategooria = attrib.Kategooria;
                    model.Kommentaar = attrib.Kommentaar;
                    model.Kood = attrib.Kood;
                    model.MaxVeovoime = attrib.MaxVeovoime;
                    model.Nimetus = attrib.Nimetus;
                    model.RegistriKood = attrib.RegistriKood;
                    model.SeeriaNR_KereNR = attrib.SeeriaNR_KereNR;
                    model.VeovoimeYhikIndikaator = attrib.VeovoimeYhikIndikaator;
                    break;
                }
            }

            return RedirectToAction("LisaAtribuutika", "Atribuutikad", model);
        }

        [HttpGet]
        public ActionResult KustutaAtribuutika(int? id)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }
            
            return RedirectToAction("Atribuutikad", "Atribuutikad");
        }

        [HttpGet]
        public ActionResult LisaAtribuutika(AtribuutikaModel model)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            AtribuutikaModel passModel = null;
            //tegemist on uue atribuutika loomisega
            if (model.ID == null)
            { passModel = new AtribuutikaModel(); }
            // tegemist on olemasoleva atribuutika muutmisega
            else 
            { passModel = model; }

            var request = new GetAllAtribuutikaLiigidRequest();
            request.sessionHandle = GetCurrentContext().SessionHandle;

            GetAllAtribuutikaLiigidResponse response;
            try
            { response = PtServiceHelper.GetServiceInstance().GetAllAtribuutikaLiigid(request); }
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
            if (response.GetAllAtribuutikaLiigidResult.AuthResponse.IsAuthenticated != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response.GetAllAtribuutikaLiigidResult.AuthResponse.AuthException.Message;
                passModel.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }
            if (response.GetAllAtribuutikaLiigidResult.Successful != true)
            {
                passModel.UserMsg = new MsgModel.UserMessages();
                passModel.UserMsg.Msg = response.GetAllAtribuutikaLiigidResult.Exception.Message;
                passModel.UserMsg.Pealkiri = "Atribuutikate pärimine ebaõnnestus!";
                passModel.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(passModel);
            }

            UpdateAuthContext(response.GetAllAtribuutikaLiigidResult.AuthResponse);
            _allAtribuutikaLiigid = response.GetAllAtribuutikaLiigidResult.AllAtribuutikaLiigid;
            passModel.AtribuutikaLiigid = 
                GetAtribLiigidSelectList(_allAtribuutikaLiigid, passModel.AtribuutikaLiikID.GetValueOrDefault(0));
          
            return View(passModel);
        }

        [HttpPost]
        public ActionResult LisaAtribuutika(AtribuutikaModel model, bool? nullB)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }

            Atribuutika attribToCommit= new Atribuutika();

            if (ModelState.IsValid)
            {
                if (model.ID != null)
                {
                    attribToCommit.ID = (int)model.ID;
                    attribToCommit.Nimetus = model.Nimetus;
                    attribToCommit.Kommentaar = model.Kommentaar;
                    attribToCommit.SeeriaNR_KereNR = model.SeeriaNR_KereNR;
                    attribToCommit.Kood = (int)model.Kood;
                    attribToCommit.RegistriKood = model.RegistriKood;
                    attribToCommit.MaxVeovoime = (int)model.MaxVeovoime;
                    attribToCommit.VeovoimeYhikIndikaator = model.VeovoimeYhikIndikaator;
                    attribToCommit.Kategooria = model.Kategooria;
                    attribToCommit.JargmineHooldusKP = System.DateTime.Parse(model.JargmineHooldusKP);

                    if (model.AtribuutikaLiikID != null || model.AtribuutikaLiikID != 0)
                    {
                        foreach (var atribuutikaLiik in _allAtribuutikaLiigid)
                        {
                            if (atribuutikaLiik.ID == model.AtribuutikaLiikID.Value)
                            {
                                attribToCommit.AtribuutikaLiikID = atribuutikaLiik;
                                break;
                            }
                        }
                    }
                }

                var request = new UpdateAtribuutikaRequest();
                request.atribuutika = attribToCommit;
                request.sessionHandle = GetCurrentContext().SessionHandle;

                UpdateAtribuutikaResponse response;
                try
                { response = PtServiceHelper.GetServiceInstance().UpdateAtribuutika(request); }
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
                if (response.UpdateAtribuutikaResult.AuthResponse.IsAuthenticated != true)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = response.UpdateAtribuutikaResult.AuthResponse.AuthException.Message;
                    model.UserMsg.Pealkiri = "Kasutaja autenimisel tekkis tõrge!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }
                if (response.UpdateAtribuutikaResult.Successful != true)
                {
                    model.UserMsg = new MsgModel.UserMessages();
                    model.UserMsg.Msg = response.UpdateAtribuutikaResult.Exception.Message;
                    model.UserMsg.Pealkiri = "Atribuutika muutmine ebaõnnestus!";
                    model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                    return View(model);
                }

                return RedirectToAction("Atribuutikad", "Atribuutikad");
            }
            return View(model);
        }

        public static SelectList GetAtribLiigidSelectList(IList<AtribuutikaLiik> liigid, int selValue)
        {
            IList<AtribuutikaLiik> listOfLiigid = new List<AtribuutikaLiik>();
            var attribLiik = new AtribuutikaLiik();
            attribLiik.ID = 0;
            attribLiik.Nimetus = "-";
            listOfLiigid.Add(attribLiik);

            if (liigid != null && liigid.Count > 0) 
            {
                foreach (AtribuutikaLiik atrLiik in liigid)
                { listOfLiigid.Add(atrLiik); }
            }

            return new SelectList(listOfLiigid, "ID", "Nimetus", selValue);
        }


        [HttpGet]
        public ActionResult MuudaAttribLiik(int? id)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }
            return RedirectToAction("LisaAtribuutika", "Atribuutikad", new AtribuutikaLiikModel() { ID = id });
        }

        [HttpGet]
        public ActionResult KustutaAttribLiik(int? id)
        {
            var sessCheck = CheckContext();
            if (sessCheck != null)
            { return sessCheck; }
            return RedirectToAction("LisaAtribuutika", "Atribuutikad");
        }

        [HttpGet]
        public ActionResult LisaAtribuutikaLiik(AtribuutikaLiikModel model)
        {
            return View();
        }
    }
}
