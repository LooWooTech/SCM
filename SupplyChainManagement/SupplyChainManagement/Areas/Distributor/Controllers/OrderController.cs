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

            ViewBag.List = Core.OrderManager.GetList(filter);
            ViewBag.Page = filter.Page;

            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.List = Core.ProductManager.GetList();
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
                    State = State.Place//备货
                };
                id = Core.OrderManager.Add(order);
            }

            var list = new List<OrderProduct>();
            for (var i = 0; i < productId.Length; i++)
            {
                var p = new OrderProduct
                {
                    ProductID = productId[i],
                    Price = price[i],
                    Number = number[i],
                    OrderID = order.ID,
                    Status = ProductStatus.Producing
                };
                if (p.ProductID == 0 || p.Price == 0 || p.Number == 0)
                {
                    continue;
                }
                list.Add(p);

            }

            Core.OrderProductManager.UpdateProducts(order.ID, list);

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
