using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class ProductController : ControllerBase
    {
        //
        // GET: /product/

        public ActionResult Index(string key, int page = 1, int rows = 20)
        {
            var filter = new ProductFilter { Page = new PageFilter(page, rows), SearchKey = key };
            ViewBag.Page = filter;
            ViewBag.List = Core.ProductManager.GetList(filter);
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.Model = Core.ProductManager.GetModel(id) ?? new Product();
            ViewBag.Items = Core.ProductManager.GetItems(id);
            ViewBag.Components = Core.ComponentManager.GetList();
            return View();
        }

        [HttpPost]
        public ActionResult Submit(Product product, int[] itemComponentId, int[] itemNumber, bool updatePrice = true)
        {
            Core.ProductManager.Save(product);
            var items = new List<ProductComponent>();
            for (var i = 0; i < itemComponentId.Length; i++)
            {
                items.Add(new ProductComponent
                {
                    ComponentId = itemComponentId[i],
                    Number = itemNumber[i],
                    ProductId = product.ID
                });
            }
            Core.ProductManager.SaveItems(items);
            if (updatePrice)
            {
                Core.ProductManager.AddPriceLog(product.ID, product.Price);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Price(int id)
        {
            ViewBag.Model = Core.ProductManager.GetModel(id);
            ViewBag.List = Core.ProductManager.GetPriceLogs(id);
            return View();
        }

        public ActionResult Delete(int id)
        {
            Core.ProductManager.Delete(id);
            return RedirectToAction("Index");
        }

        //public string JavaScriptContext(int ID)
        //{
        //    var list = Core.RateManager.Get(ID);
        //    return Core.RateManager.GetJavaScriptContext(list, Server.MapPath("~/Charts/Price.js"));
        //}

        //[HttpPost]
        //public ActionResult AddRate(Rate rate)
        //{
        //    Core.RateManager.Add(rate);
        //    Core.ProductManager.Edit(rate);
        //    return RedirectToAction("Index");
        //}

    }
}
