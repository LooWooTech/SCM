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

        public ActionResult Edit(int id = 0, Business business = Business.供应商)
        {
            var model = Core.EnterpriseManager.GetModel(id) ?? new Enterprise { Business = business };
            ViewBag.Model = model;
            ViewBag.User = Core.UserManager.GetUserByEnterpriseId(model.ID);
            return View();
        }

        [HttpPost]
        public ActionResult Save(Enterprise model, string username, string password)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException("企业名称没有填写");
            }

            if (model.ID == 0 && model.Business == Business.销售商)
            {
                if (string.IsNullOrEmpty(username))
                {
                    throw new ArgumentException("用户名没有填写");
                }
                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("密码没有填写");
                }
            }

            Core.EnterpriseManager.Save(model);
            if (model.Business == Business.销售商)
            {
                var user = Core.UserManager.GetUserByEnterpriseId(model.ID);
                if (user == null)
                {
                    user = new User
                    {
                        Username = username,
                        Password = password,
                        EnterpriseId = model.ID,
                        Role = UserRole.User
                    };
                    Core.UserManager.Save(user);
                }
            }
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
