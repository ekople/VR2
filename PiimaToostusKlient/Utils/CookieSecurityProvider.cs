using System;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;

namespace PiimaToostusKlient.Utils
{
    public class CookieSecurityProvider
    {
        private static MethodInfo _encode;
        private static MethodInfo _decode;
        private static CookieProtection _cookieProtection = CookieProtection.All;

        public CookieSecurityProvider()
        {
            Assembly systemWeb = typeof(HttpContext).Assembly;
            Type cookieProtectionHelper = systemWeb.GetType("System.Web.Security.MachineKey");
            _encode = cookieProtectionHelper.GetMethod("Encode");
            _decode = cookieProtectionHelper.GetMethod("Decode");
        }

        public HttpCookie Encrypt(HttpCookie httpCookie)
        {
            byte[] data = Encoding.Default.GetBytes(httpCookie.Value);
            httpCookie.Value = (string)_encode.Invoke(null, new object[] { data, _cookieProtection });
            return httpCookie;
        } 

        public HttpCookie Decrypt(HttpCookie httpCookie)
        {
            byte[] buffer = (byte[])_decode.Invoke(null, new object[] { httpCookie.Value, _cookieProtection});
            httpCookie.Value = Encoding.Default.GetString(buffer, 0, buffer.Length);
            return httpCookie;
        }
    }
}