using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LoowooTech.SCM.Web
{
    public static class AuthUtils
    {
        private const string _cookieName = ".scm_user";

        public static void SaveAuth(this HttpContextBase context, User user)
        {
            if (user == null)
            {
                ClearAuth(context);
                return;
            }
            var ticket = new FormsAuthenticationTicket(Newtonsoft.Json.JsonConvert.SerializeObject(user), true, 60);
            var cookieValue = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(_cookieName, cookieValue);
            context.Response.Cookies.Remove(_cookieName);
            context.Response.Cookies.Add(cookie);
        }

        public static UserIdentity GetCurrentUser(this HttpContextBase context)
        {
            var cookie = context.Request.Cookies.Get(_cookieName);
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null && !string.IsNullOrEmpty(ticket.Name))
                {
                    try
                    {
                        var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(ticket.Name);
                        return new UserIdentity
                        {
                            UserID = user.ID,
                            Role = user.Role,
                            Username = user.Username,
                            EnterpriseId = user.EnterpriseId
                        };
                    }
                    catch
                    {
                    }
                }
            }
            return UserIdentity.Guest;
        }

        public static void ClearAuth(this HttpContextBase context)
        {
            var cookie = context.Request.Cookies.Get(_cookieName);
            if (cookie == null) return;
            cookie.Value = null;
            cookie.Expires = DateTime.Now.AddDays(-1);
            context.Response.SetCookie(cookie);
        }
    }
}