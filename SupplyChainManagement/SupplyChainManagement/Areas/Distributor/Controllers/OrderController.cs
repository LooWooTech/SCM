using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Areas.Distributor.Controllers
{
    public class OrderController : DistributorControllerBase
    {
        public ActionResult Index(int page = 1, int rows = 20)
        {
            var filter = new OrderFilter
            {
                EnterpriseId = Identity.EnterpriseId,
                Type = OrderType.Shipment,
                Page = new PageFilter(page, rows)
            };

            var orders = Core.OrderManager.GetList(filter);

            ViewBag.List = ViewBag.List;
            ViewBag.Page = filter.Page;

            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            var model = Core.OrderManager.GetModel(id) ?? new Order { EnterpriseId = Identity.EnterpriseId };
            if (model.ID > 0)
            {
                ViewBag.List = Core.OrderItemManager.GetList(model.ID);
            }
            ViewBag.Model = model;
            ViewBag.Products = Core.ProductManager.GetList();
            return View();
        }

        public ActionResult Submit(int id, int[] productId, double[] price, int[] number)
        {
            if (productId == null || productId.Length == 0)
            {
                throw new ArgumentException("参数异常");
            }

            Order order = Core.OrderManager.GetModel(id);
            if (order == null)
            {
                order = new Order
                {
                    EnterpriseId = Identity.EnterpriseId,
                    Type = OrderType.Shipment,
                    State = (int)SellOrderState.Created
                };
                id = Core.OrderManager.Add(order);
            }

            var list = new List<OrderItem>();
            for (var i = 0; i < productId.Length; i++)
            {
                var p = new OrderItem
                {
                    ItemID = productId[i],
                    ItemType = OrderItemType.Product,
                    Price = price[i],
                    Number = number[i],
                    OrderID = order.ID,
                };
                if (p.OrderID == 0 || p.Price == 0 || p.Number == 0)
                {
                    continue;
                }
                p.Status = Core.InventoryManager.GetProductStatus(p.ItemID, p.Number);
                list.Add(p);

            }

            Core.OrderItemManager.UpdateItems(order.ID, list);
            return RedirectToAction("Detail", new { order.ID });
        }

        public ActionResult Detail(int id)
        {
            return View();
        }

        public ActionResult Cancel(int id)
        {
            return JsonSuccess();
        }
    }
}
