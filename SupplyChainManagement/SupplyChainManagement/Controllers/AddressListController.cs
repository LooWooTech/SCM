using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplyChainManagement.Controllers
{
    public class AddressListController : ControllerBase
    {
        [HttpPost]
        public ActionResult Add(string Name, ContactWay way, string value)
        {
            var contact = Core.ContactManager.Get(Name);
            return RedirectToAction("Index", "Enterprise");
        }
    }
}
