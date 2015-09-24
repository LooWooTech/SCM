using LoowooTech.SCM.Model;
using LoowooTech.SCM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class StockController : ControllerBase
    {
        //
        // GET: /Stock/

        public ActionResult Index(OrderType Type=OrderType.bought)
        {
            var list = Core.OrderManager.GetAll(Type);
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
            return RedirectToAction("Logistics", new { ID=ID});
        }

        public ActionResult Logistics(int ID)
        {
            var order = Core.OrderManager.Get(ID);
            if (order == null)
            {
                throw new ArgumentException("未找到相关的订单详情");
            }
            ViewBag.List = Core.QuotationManager.GetAll(order.ID);
            return View(order);
        }


        [HttpPost]
        public ActionResult CheckOut(int ID)
        {
            //获取当前订单中的部件ID
            var listNames = Core.QuotationManager.GetByOID(ID).Select(e=>e.ID).ToList();
            //获取字典 key为部件ID  value为最终确认部件数量
            var dict = Core.QuotationManager.Acquire(HttpContext, listNames);
            //假如存在部分损坏部件，那么就更新本地订单部件数量
            Core.QuotationManager.Update(dict);
            var list = Core.QuotationManager.GetByOID(ID);
            //确认本地订单部件数量 将本地部件进入本地仓库
            Core.InventoryManager.Add(list);
            //修改本地订单状态
            Core.OrderManager.Done(ID);
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult OrderList(int ID)
        {
            var list = Core.QuotationManager.GetAll(ID);
            ViewBag.Index = ID;
            return PartialView("OrderList", list);
        }

        public ActionResult Gain()
        {
            var list = Core.ComponentsManager.Get().Select(e => e.Brand + "-" + e.Type.GetDescription() + "-" + e.Specification + "-" + e.Number).ToList();
            return Json(list,JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int ID)
        {

            return RedirectToAction("Index");
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
