using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class SellController : ControllerBase
    {
        private Order GetOrder(int id)
        {
            var model = Core.OrderManager.GetModel(id);
            if (model == null)
            {
                throw new ArgumentException("参数不正确，没找到订单");
            }
            return model;
        }

        public ActionResult Index(int? state, int enterpriseId = 0, int page = 1, int rows = 20)
        {
            Enterprise enterprise = null;
            var filter = new OrderFilter
            {
                State = state,
                Type = OrderType.Shipment,
                EnterpriseId = enterpriseId,
                Page = new PageFilter(page, rows)
            };
            if (enterpriseId > 0)
            {
                enterprise = Core.EnterpriseManager.GetModel(enterpriseId);
                filter.Type = OrderType.Shipment;
            }
            ViewBag.Enterprise = enterprise;
            ViewBag.List = Core.OrderManager.GetList(filter);
            ViewBag.Page = filter.Page;
            return View();
        }

        public ActionResult Detail(int id)
        {
            var model = GetOrder(id);
            ViewBag.Model = model;
            ViewBag.Enterprise = Core.EnterpriseManager.GetModel(model.EnterpriseId);
            ViewBag.Message = Core.MessageManager.GetModelByOrderId(id);
            ViewBag.Contracts = Core.ContractManager.GetList(id);
            ViewBag.Remittance = Core.RemittanceManager.GetModel(id);

            ViewBag.Express = Core.ExpressManager.GetModel(model.Express);
            var products = Core.OrderItemManager.GetList(id);
            ViewBag.OrderItems = products;
            if (model.State == (int)SellOrderState.Created || model.State == (int)SellOrderState.Prepare)
            {
                Core.InventoryManager.SetStoreNumber(products);
            }
            return View();
        }

        public ActionResult SubmitCreated(int id)
        {
            var order = GetOrder(id);
            order.State = (int)SellOrderState.Prepare;
            Core.OrderManager.Update(order);
            return RedirectToAction("Detail", new { id });
        }

    }
}
