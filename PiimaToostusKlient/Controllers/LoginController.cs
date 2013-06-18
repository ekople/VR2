using System;
using System.Web;
using System.Web.Mvc;
using PiimaToostusKlient.Models;
using PiimaToostusKlient.PtServiceRef;
using PiimaToostusKlient.Utils;

namespace PiimaToostusKlient.Controllers
{
    public class LoginController : Controller
    {
        private string logonCookieName = System.Configuration.ConfigurationManager.AppSettings["LoginCookieName"];
        private static CookieSecurityProvider cookyeSec = new CookieSecurityProvider();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            HttpCookie cookie = Request.Cookies[logonCookieName];
            LoginModel loginMod = new LoginModel();
            if (cookie != null)
            {
                cookie = cookyeSec.Decrypt(cookie);
                loginMod.RememberMe = true;
                string[] cookieParts = cookie.Value.Split(new string[] { "---\\\\\\---" }, StringSplitOptions.None);
                if (cookieParts != null && cookieParts.Length == 2)
                {
                    loginMod.UserName = cookieParts[0];
                    loginMod.Password = cookieParts[1];
                }
            }

            return View(loginMod);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (model.RememberMe)
            {
                HttpCookie cookie = Request.Cookies[logonCookieName];
                if (cookie != null)
                {
                    cookie = cookyeSec.Decrypt(cookie);
                }
                else
                {
                    cookie = new HttpCookie(logonCookieName);
                }

                string cookieVal = model.UserName + "---\\\\\\---" + model.Password;
                cookie.Value = cookieVal;

                cookie = cookyeSec.Encrypt(cookie);
                Response.Cookies.Add(cookie);
            }
            else
            { Request.Cookies.Remove(logonCookieName); }

            AuthUserResponse resp;
            try
            { resp = PtServiceHelper.GetServiceInstance().AuthUser(new AuthUserRequest(model.UserName, Utils.Utils.GenerateSaltedHash(model.UserName, model.Password))); }
            catch (Exception exception)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = exception.Message;
                model.UserMsg.Pealkiri = "Autenimisel tekkis tehiline tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }
            if (resp == null)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = "Autentimine ebaõnnestus kuna teenuselt ei õnnestunud vastust saada!";
                model.UserMsg.Pealkiri = "Autenimisel tekkis tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }
            if (resp.AuthUserResult.IsAuthenticated != true)
            {
                model.UserMsg = new MsgModel.UserMessages();
                model.UserMsg.Msg = resp.AuthUserResult.AuthException.Message;
                model.UserMsg.Pealkiri = "Autenimisel tekkis tõrge!";
                model.UserMsg.MessageType = MsgModel.UserMessages.MsgType.Viga;
                return View(model);
            }

            System.Web.HttpContext.Current.Session.Add("UserContext",resp.AuthUserResult);

            return View("WelcomePage");
        }
    }
}