using loowootech.SCM.Model;
using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class EnterpriseController : ControllerBase
    {
        public ActionResult Index(Business business, string name, int page = 1, int rows = 20)
        {
            var filter = new EnterpriseFilter
            {
                Business = business,
                Name = name,
                Page = new PageFilter(page, rows)
            };
            ViewBag.Page = filter.Page;
            ViewBag.List = Core.EnterpriseManager.GetList(filter);
            ViewBag.Business = business;
            return View();
        }

        public ActionResult Edit(int id = 0, Business business = Business.Supplier)
        {
            ViewBag.Model = Core.EnterpriseManager.GetModel(id) ?? new Enterprise { Business = business };
            return View();
        }

        [HttpPost]
        public ActionResult Save(Enterprise model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException("企业名称没有填写");
            }

            Core.EnterpriseManager.Save(model);
            return JsonSuccess();
        }

        public ActionResult Delete(int id)
        {
            var model = Core.EnterpriseManager.GetModel(id);
            if (model == null)
            {
                throw new ArgumentException("id参数错误");
            }
            Core.EnterpriseManager.Delete(id);
            return RedirectToAction("Index", new { model.Business });
        }

        public ActionResult Detail(int id)
        {
            ViewBag.Model = Core.EnterpriseManager.GetModel(id);
            ViewBag.Contacts = Core.ContactManager.GetList(id);
            return View();
        }
    }
}
