using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class ComponentController : ControllerBase
    {
        public ActionResult Index(string name, UnitType? type, int page = 1, int rows = 20)
        {
            var filter = new ComponentFilter
            {
                Number = name,
                UnitType = type,
                Page = new PageFilter(page, rows)
            };
            ViewBag.List = Core.ComponentManager.GetList(filter);
            ViewBag.Page = filter.Page;
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.Model = Core.ComponentManager.GetModel(id) ?? new Component();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Component model)
        {
            Core.ComponentManager.Save(model);
            return JsonSuccess();
        }

        public ActionResult Delete(int ID)
        {
            Core.ComponentManager.Delete(ID);
            return RedirectToAction("Index");
        }

    }
}
