using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PiimaToostusKlient.PtServiceRef;

namespace PiimaToostusKlient.Controllers
{
    public class BaseController : Controller
    {
        public RedirectToRouteResult CheckContext()
        {
            AuthResponse _authContext = System.Web.HttpContext.Current.Session["UserContext"] as AuthResponse;
            if (_authContext == null || _authContext.AuthException != null || _authContext.SessionValidTo < DateTime.Now)
            {
                Session.Clear();
                return RedirectToAction("Login", "Login");
            }
            return null;
        }

        public void UpdateAuthContext(AuthResponse authContext)
        {
            System.Web.HttpContext.Current.Session.Add("UserContext", authContext);
        }

        public AuthResponse GetCurrentContext()
        {
            AuthResponse _authContext = System.Web.HttpContext.Current.Session["UserContext"] as AuthResponse;
            return _authContext;
        }
    }
}
