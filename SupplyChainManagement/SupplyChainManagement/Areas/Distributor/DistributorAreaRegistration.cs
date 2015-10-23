using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Areas.Distributor
{
    public class DistributorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Distributor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Distributor_default",
                "Distributor/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { this.GetType().Namespace + ".Controllers" }
            );
        }
    }
}
