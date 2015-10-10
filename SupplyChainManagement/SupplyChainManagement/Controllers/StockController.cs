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

        public ActionResult Index(OrderType Type = OrderType.Bought)
        {
            var list = Core.OrderManager.GetAll(Type);
            return View(list);
        }

        public ActionResult Profile(int id)
        {
            var order = Core.OrderManager.GetModel(id);
            var orderList = Core.OrderItemManager.GetList(order.ID);

            return View();
        }

        public ActionResult Contact(int id)
        {
            ViewBag.Enterprise = Core.EnterpriseManager.GetModel(id);
            ViewBag.Contacts = Core.ContactManager.GetList(id);
            return View();
        }

        /// <summary>
        /// 向供应商部件下单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Place(int id)
        {
            ViewBag.Enterprise = Core.EnterpriseManager.GetModel(id);
            ViewBag.Contacts = Core.ContactManager.GetList(id);
            ViewBag.Components = Core.ComponentManager.GetList(null);
            return View();
        }

        [HttpPost]
        public ActionResult Add(int enterpriseId)
        {
            var orderId = Core.OrderManager.Add(new Order { EnterpriseId = enterpriseId });
            if (orderId > 0)
            {
                var list = Core.OrderItemManager.Acquire(HttpContext, orderId);
                if (list != null)
                {
                    Core.OrderItemManager.AddAll(list);
                }
            }

            return RedirectToAction("Detail", new { ID = orderId });
        }



        public ActionResult Detail(int ID)
        {
            Order order = Core.OrderManager.GetModel(ID);
            ViewBag.List = Core.OrderItemManager.GetList(ID);
            return View(order);
        }

        [HttpPost]
        public ActionResult Update(int id, HttpPostedFileBase file, string express)
        {
            var model = Core.OrderManager.GetModel(id);
            if (file != null)
            {
                //model.Indenture = file.Upload();
            }
            model.ExpressNo = express;
            if (!string.IsNullOrEmpty(model.ExpressNo))
            {
                model.State = State.Shipping;
            }
            Core.OrderManager.Update(model);

            return RedirectToAction("Logistics", new { id });
        }

        public ActionResult Logistics(int id)
        {
            var order = Core.OrderManager.GetModel(id);
            if (order == null)
            {
                throw new ArgumentException("未找到相关的订单详情");
            }
            ViewBag.List = Core.OrderItemManager.GetList(order.ID);
            return View(order);
        }


        [HttpPost]
        public ActionResult CheckOut(int ID)
        {
            //获取当前订单中的部件ID
            var listNames = Core.OrderItemManager.GetByOID(ID).Select(e => e.ID).ToList();
            //获取字典 key为部件ID  value为最终确认部件数量
            var dict = Core.OrderItemManager.Acquire(HttpContext, listNames);
            //假如存在部分损坏部件，那么就更新本地订单部件数量
            Core.OrderItemManager.Update(dict);
            var list = Core.OrderItemManager.GetByOID(ID);
            //确认本地订单部件数量 将本地部件进入本地仓库
            Core.InventoryManager.Add(list);
            //修改本地订单状态
            Core.OrderManager.Done(ID);
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult OrderList(int ID)
        {
            var list = Core.OrderItemManager.GetList(ID);
            ViewBag.Index = ID;
            return PartialView("OrderList", list);
        }

        public ActionResult Gain()
        {
            var list = Core.ComponentManager.GetList(null).Select(e => e.Brand + "-" + e.Type.GetDescription() + "-" + e.Specification + "-" + e.Number).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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
            ViewBag.Enterprise = Core.EnterpriseManager.GetModel(ID);
            ViewBag.List = Core.ProductManager.Get();
            return View();
        }



    }
}
