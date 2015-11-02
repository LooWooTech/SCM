using LoowooTech.SCM.Common;
using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class OrderController : ControllerBase
    {
        public ActionResult Index(State? state, OrderType type = OrderType.Bought, int enterpriseId = 0, int page = 1, int rows = 20)
        {
            Enterprise enterprise = null;
            var filter = new OrderFilter
            {
                State = state,
                Type = type,
                EnterpriseId = enterpriseId,
                Page = new PageFilter(page, rows)
            };
            if (enterpriseId > 0)
            {
                enterprise = Core.EnterpriseManager.GetModel(enterpriseId);
                filter.Type = enterprise.Business == Business.销售商 ? OrderType.Shipment : OrderType.Bought;
            }
            ViewBag.Enterprise = enterprise;
            ViewBag.List = Core.OrderManager.GetList(filter);
            ViewBag.Page = filter.Page;
            return View();
        }

        public ActionResult Edit(int id = 0, int enterpriseId = 0)
        {
            if (id == 0 && enterpriseId == 0)
            {
                throw new ArgumentException("参数异常");
            }

            var model = Core.OrderManager.GetModel(id) ?? new Order { EnterpriseId = enterpriseId };
            var routeData = new { id = model.ID, enterpriseId = model.EnterpriseId };
            switch (model.State)
            {
                //case State.Turn:
                //case State.Payment:
                //    return RedirectToAction("Edit", routeData);
                default:
                    return RedirectToAction(model.State.ToString(), routeData);
            }
        }

        public ActionResult Contact(int id = 0, int enterpriseId = 0)
        {
            var model = Core.OrderManager.GetModel(id) ?? new Order { EnterpriseId = enterpriseId };
            ViewBag.Model = model;
            if (model.ID > 0)
            {
                ViewBag.Message = Core.MessageManager.GetModelByOrderId(model.ID);
            }
            ViewBag.Enterprise = Core.EnterpriseManager.GetModel(enterpriseId);
            ViewBag.Contacts = Core.ContactManager.GetList(enterpriseId);
            return View();
        }

        public ActionResult SubmitContact(Message model, bool submit = false)
        {
            if (model.ContactId == 0)
            {
                throw new ArgumentException("没有选择联系人");
            }

            if (model.EnterpriseId == 0)
            {
                throw new ArgumentException("缺少企业参数");
            }

            if (model.OrderId == 0)
            {
                model.OrderId = Core.OrderManager.Add(new Order
                {
                    EnterpriseId = model.EnterpriseId,
                    State = submit ? State.Place : State.Contact
                });
            }
            else
            {
                var order = Core.OrderManager.GetModel(model.OrderId);
                order.State = State.Place;
                Core.OrderManager.Update(order);
            }

            Core.MessageManager.Save(model);

            return RedirectToAction(submit ? "Place" : "Contact", new { id = model.OrderId });

        }

        private Order GetOrder(int id)
        {
            var model = Core.OrderManager.GetModel(id);
            if (model == null)
            {
                throw new ArgumentException("参数不正确，没找到订单");
            }
            return model;
        }

        public ActionResult Place(int id)
        {
            var model = GetOrder(id);
            ViewBag.Model = model;
            ViewBag.Components = Core.ComponentManager.GetList(null);
            ViewBag.List = Core.OrderComponentManager.GetList(model.ID);
            return View();
        }

        public ActionResult SubmitPlace(int id, int[] itemComponentId, string[] itemComponent, float[] itemPrice, int[] itemNumber, bool submit = false)
        {
            if (submit)
            {
                if (itemComponentId.Any(e => e == 0))
                {
                    throw new ArgumentException("没有选择正确的部件");
                }

                if (itemNumber.Any(e => e == 0))
                {
                    throw new ArgumentException("部件数量填写不正确");
                }
            }

            var list = new List<OrderComponent>();
            for (var i = 0; i < itemComponentId.Length; i++)
            {
                list.Add(new OrderComponent
                {
                    ComponentId = itemComponentId[i],
                    Price = itemPrice[i],
                    Number = itemNumber[i],
                    OrderId = id,
                });
            }

            var model = GetOrder(id);
            model.State = submit ? State.Contract : State.Place;
            Core.OrderComponentManager.UpdateComponents(model.ID, list);
            Core.OrderManager.Update(model);

            return RedirectToAction(submit ? "Contract" : "Place", new { id });
        }

        public ActionResult Contract(int id)
        {
            var model = GetOrder(id);
            ViewBag.Model = model;
            ViewBag.List = Core.ContractManager.GetList(model.ID);
            return View();
        }

        public ActionResult SubmitContract(int id, bool submit = false)
        {
            var model = GetOrder(id);
            model.State = submit ? State.Shipping : State.Contract;
            var list = new List<Contract>();
            if (Request.Files.Count > 0)
            {
                foreach (HttpPostedFileBase file in Request.Files)
                {
                    if (file.ContentLength > 0)
                    {
                        var filePath = file.Upload();
                        list.Add(new Contract { OrderId = id, File = filePath });
                    }
                }
            }
            Core.ContractManager.Save(id, list);
            return RedirectToAction(submit ? "Shipping" : "Contract", new { id });
        }

        public ActionResult Shipping(int id)
        {
            var model = GetOrder(id);
            ViewBag.List = Core.ExpressManager.GetList();
            ViewBag.Model = model;
            return View();
        }

        public ActionResult SubmitShipping(int id, int express, string expressNo, bool submit = false)
        {
            var model = GetOrder(id);
            if (submit)
            {
                if (string.IsNullOrEmpty(expressNo) || express == 0)
                {
                    throw new ArgumentException("请选择快递公司并填写运单号");
                }
                model.State = State.Receive;
            }

            model.ExpressNo = expressNo;
            model.Express = express;
            Core.OrderManager.Update(model);
            return RedirectToAction(submit ? "Receive" : "Shipping", new { id });
        }

        public ActionResult Receive(int id)
        {
            var model = GetOrder(id);
            ViewBag.Model = model;
            ViewBag.List = Core.OrderComponentManager.GetList(model.ID);
            return View();
        }

        public ActionResult SubmitReceive(int orderId, int[] itemId, double[] dealprice, int[] dealnumber, bool submit = false)
        {
            var model = GetOrder(orderId);
            var list = Core.OrderComponentManager.GetList(orderId);
            foreach (var item in list)
            {
                for (var i = 0; i < itemId.Length; i++)
                {
                    if (itemId[i] == item.ID)
                    {
                        item.DealNumber = dealnumber[i];
                        item.DealPrice = dealprice[i];
                    }
                }
            }
            Core.OrderComponentManager.UpdateReceiveNumber(list);
            model.State = submit ? State.Payment : State.Receive;
            Core.OrderManager.Update(model);
            return RedirectToAction(submit ? "Payment" : "Receive", new { id = orderId });
        }

        public ActionResult Payment(int id)
        {
            var order = GetOrder(id);
            ViewBag.Order = order;
            ViewBag.Model = Core.RemittanceManager.GetModel(id) ?? new Remittance { OrderId = order.ID };
            return View();
        }

        public ActionResult SubmitPayment(int id, Remittance data, bool submit = false)
        {
            var order = GetOrder(id);
            if (string.IsNullOrEmpty(data.Account))
            {
                throw new ArgumentException("没有填写汇款账户");
            }
            if (string.IsNullOrEmpty(data.Bank))
            {
                throw new ArgumentException("没有填写开户银行");
            }
            if (data.Money <= 0)
            {
                throw new ArgumentException("金额填写不正确");
            }
            data.OrderId = order.ID;
            Core.RemittanceManager.Save(data);
            order.State = submit ? State.Done : State.Payment;
            Core.OrderManager.Update(order);
            return RedirectToAction(submit ? "Done" : "Payment", new { id = id });
        }

        public ActionResult Done(int id)
        {
            return RedirectToAction("Detail", new { id });
        }

        public ActionResult Detail(int id)
        {
            var model = GetOrder(id);
            ViewBag.Model = model;
            ViewBag.Enterprise = Core.EnterpriseManager.GetModel(model.EnterpriseId);
            ViewBag.Message = Core.MessageManager.GetModelByOrderId(id);
            ViewBag.Contracts = Core.ContractManager.GetList(id);
            ViewBag.Remittance = Core.RemittanceManager.GetModel(id);
            ViewBag.OrderComponents = Core.OrderComponentManager.GetList(id);
            ViewBag.OrderProducts = Core.OrderProductManager.GetList(id);
            ViewBag.Express = Core.ExpressManager.GetModel(model.Express);
            //如果还没有配货或成产，则检查库存
            if (model.State == State.Place)
            {

            }
            return View();
        }
    }
}
