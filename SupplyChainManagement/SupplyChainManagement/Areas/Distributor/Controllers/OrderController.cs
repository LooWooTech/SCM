using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Areas.Distributor.Controllers
{
    public class OrderController : DistributorControllerBase
    {
        public ActionResult Index(int page = 1, int rows = 20)
        {
            var filter = new OrderFilter
            {
                EnterpriseId = Identity.EnterpriseId,
                Type = OrderType.Shipment,
                Page = new PageFilter(page, rows)
            };
            
            ViewBag.List = Core.OrderManager.GetList(filter);
            ViewBag.Page = filter.Page;

            return View();
        }

    }
}
