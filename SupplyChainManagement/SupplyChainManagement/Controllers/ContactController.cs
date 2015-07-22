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
                if (Index > 0)
                {
                    var list = Core.AddressListManager.Acquire(HttpContext, Index);
                    Core.AddressListManager.Add(list);
                }
            }
            var entity = Core.EnterpriseManager.Get(contact.EID);
            if (entity == null)
            {
                throw new ArgumentException("未找到企业信息");
            }

            return RedirectToAction("Index", "Enterprise", new { business = entity.Business });
        }


        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete(int ID)
        {
            var contact = Core.ContactManager.GetByID(ID);
            if (contact == null)
            {
                throw new ArgumentException("未找到相关的联系人");
            }
            Core.ContactManager.Delete(ID);
            var enterprise = Core.EnterpriseManager.Get(contact.EID);
            if (enterprise == null)
            {
                throw new ArgumentException("未找到相关企业信息");
            }

            return RedirectToAction("Index", "Enterprise", new { business = enterprise.Business });
        }

    }
}
