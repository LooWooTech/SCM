using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace LoowooTech.SCM.Web
{
    public class UserPrincipal : IPrincipal
    {
        public UserPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }

    public class UserIdentity : System.Security.Principal.IIdentity
    {
        public static readonly UserIdentity Guest = new UserIdentity();

        public int UserID { get; set; }

        public UserRole Role { get; set; }

        public string Username { get; set; }

        public string AuthenticationType
        {
            get { return "Web.Session"; }
        }

        public string Name
        {
            get { return Username; }
        }

        public bool IsAuthenticated
        {
            get
            {
                if (Role == UserRole.User)
                {
                    return EnterpriseId > 0 && UserID > 0;
                }
                if (Role == UserRole.Admin)
                {
                    return UserID > 0;
                }
                return false;
            }
        }

        public int EnterpriseId { get; set; }
    }
}