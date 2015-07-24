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
        /// 向供应商部件下单
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
        public ActionResult Edit(int ID,string Express)
        {
            var order = Core.OrderManager.Acquire(HttpContext, ID, Express);
            try
            {
                Core.OrderManager.Edit(order);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return View();
        }

        /// <summary>
        /// 经销商 订货
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Backlog(int ID)
        {
            ViewBag.Enterprise = Core.EnterpriseManager.Get(ID);
            ViewBag.List = Core.ProductManager.Get();
            return View();
        }

        

    }
}
