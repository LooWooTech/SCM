using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class RemitController : ControllerBase
    {

        [HttpPost]
        public ActionResult Add(Remittance remittance)
        {
            var index = Core.RemittanceManager.Add(remittance);
            var order = Core.OrderManager.Get(remittance.SID);
            return RedirectToAction("Index", "Stock", new { Type=order.Type});
        }

    }
}
