using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplyChainManagement.Controllers
{
    public class StockController : ControllerBase
    {
        //
        // GET: /Stock/

        public ActionResult Index()
        {
            var list = Core.OrderManager.GetAll();
            return View(list);
        }


        /// <summary>
        /// 开始下单
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Place(int ID)
        {
            ViewBag.Enterprise = Core.EnterpriseManager.Get(ID);
            ViewBag.List = Core.ComponentsManager.Get();
            return View();
        }

        [HttpPost]
        public ActionResult Add(int EID)
        {
            var index = Core.OrderManager.Add(new Order { EID = EID });
            if (index > 0)
            {
                var list = Core.QuotationManager.Acquire(HttpContext, index);
                if (list != null)
                {
                    Core.QuotationManager.AddAll(list);
                }
            }

            return RedirectToAction("Detail", new { ID = index });
        }


        public ActionResult Detail(int ID)
        {
            Order order = Core.OrderManager.Get(ID);
            ViewBag.List = Core.QuotationManager.GetAll(ID);
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(string Express)
        {

            return View();
        }
        

        

    }
}
