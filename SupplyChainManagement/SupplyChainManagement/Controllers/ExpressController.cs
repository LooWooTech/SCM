using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class ExpressController : ControllerBase
    {
        public ActionResult Index()
        {
            ViewBag.List = Core.ExpressManager.GetList();
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.Model = Core.ExpressManager.GetModel(id) ?? new Express();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Express model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("快递公司名称没有填写");
            }
            Core.ExpressManager.Save(model);
            return JsonSuccess();
        }

        public ActionResult Delete(int id)
        {
            Core.ExpressManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
