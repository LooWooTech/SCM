using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplyChainManagement.Controllers
{
    public class ContactController : ControllerBase
    {
        
        [HttpPost]
        public ActionResult Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var Index = Core.ContactManager.Add(contact);
            }
            return View();
        }

    }
}
