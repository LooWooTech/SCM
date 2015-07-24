using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SupplyChainManagement.Controllers
{
    public class ManufactureController : ControllerBase
    {
        //
        // GET: /Manufacture/

        public ActionResult Index()
        {
            var list = Core.ProductManager.GetAll();
            ViewBag.CList = Core.ComponentsManager.Get();
            return View(list);
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {
            var Index = Core.ProductManager.Add(product);
            if (Index > 0)
            {
                var list = Core.ItemManager.Acquire(HttpContext, Index);
                try
                {
                    Core.ItemManager.Add(list);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Price(int PID)
        {
            ViewBag.PID = PID;
            return View();
        }

        public string JavaScriptContext(int ID)
        {
            var list = Core.RateManager.Get(ID);
            return Core.RateManager.GetJavaScriptContext(list,Server.MapPath("~/Charts/Price.js"));
        }

        [HttpPost]
        public ActionResult AddRate(Rate rate)
        {
            Core.RateManager.Add(rate);
            Core.ProductManager.Edit(rate);
            return RedirectToAction("Index");
        }

    }
}
