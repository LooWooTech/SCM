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
            var filter = new OrderFilter
            {
                State = state,
                Type = type,
                EnterpriseId = enterpriseId,
                Page = new PageFilter(page, rows)
            };
            ViewBag.Enterprise = Core.EnterpriseManager.GetModel(enterpriseId);
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

        public ActionResult SubmitContact(bool submit, Message model)
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

            return RedirectToAction("Place", new { id = model.OrderId });

        }

        public ActionResult Place(int id)
        {
            var model = Core.OrderManager.GetModel(id);
            if (model == null)
            {
                throw new ArgumentException("参数不正确，没找到订单");
            }

            ViewBag.Model = model;
            ViewBag.Components = Core.ComponentManager.GetList(null);
            ViewBag.Quotations = Core.QuotationManager.GetList(model.ID);
            return View();
        }

        public ActionResult SubmitPlace(int id, int[] componentId, string[] component, float[] price, int[] number, bool submit = false)
        {
            if (submit)
            {
                if (componentId.Any(e => e == 0))
                {
                    throw new ArgumentException("没有选择正确的部件");
                }

                if (number.Any(e => e == 0))
                {
                    throw new ArgumentException("部件数量填写不正确");
                }
            }

            var list = new List<Quotation>();
            for (var i = 0; i < componentId.Length; i++)
            {
                list.Add(new Quotation
                {
                    ComponentId = componentId[i],
                    Price = price[i],
                    Number = number[i],
                    OrderId = id,
                });
            }

            var model = Core.OrderManager.GetModel(id);
            if (model == null)
            {
                throw new ArgumentException("没有找到对应的订单");
            }

            Core.QuotationManager.Save(model.ID, list);

            return RedirectToAction("Shipping", new { id });
        }

        public ActionResult Shipping(int id)
        {
            return View();
        }

        public ActionResult Turn(int id)
        {
            return View();
        }

        public ActionResult Done(int id)
        {
            return View();
        }

        public ActionResult Payment(int id)
        {
            return View();
        }
    }
}
