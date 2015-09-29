using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class ContactController : ControllerBase
    {
        public ActionResult Edit(int id = 0, int enterpriseId = 0)
        {
            if (id == 0 && enterpriseId == 0)
            {
                throw new ArgumentException("参数异常");
            }
            ViewBag.Model = Core.ContactManager.GetModel(id) ?? new Contact { EnterpriseId = enterpriseId };
            return View();
        }

        [HttpPost]
        public ActionResult Save(Contact contact)
        {
            if (string.IsNullOrEmpty(contact.Name))
            {
                throw new ArgumentException("姓名没有填写");
            }

            if (string.IsNullOrEmpty(contact.Mobile))
            {
                throw new ArgumentException("联系电话没有填写");
            }

            Core.ContactManager.Save(contact);
            return JsonSuccess();
        }


        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("id参数错误");
            }
            var model = Core.ContactManager.GetModel(id);
            if (model == null)
            {
                throw new ArgumentException("id参数错误");
            }
            Core.ContactManager.Delete(id);
            return RedirectToAction("Details", "Enterprise", new { id = model.EnterpriseId });
        }

    }
}
