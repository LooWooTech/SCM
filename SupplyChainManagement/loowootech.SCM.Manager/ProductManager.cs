using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LoowooTech.SCM.Manager
{
    public class ProductManager : ManagerBase
    {
        public Product GetModel(int id)
        {
            if (id == 0) return null;
            using (var db = GetDataContext())
            {
                return db.Products.FirstOrDefault(e => e.ID == id);
            }
        }

        public List<Product> GetList(ProductFilter filter = null)
        {
            using (var db = GetDataContext())
            {
                var query = db.Products.AsQueryable();
                if (filter == null)
                {
                    return query.ToList();
                }
                if (!string.IsNullOrEmpty(filter.SearchKey))
                {
                    query = query.Where(e => e.Number.Contains(filter.SearchKey));
                }

                return query.OrderByDescending(e => e.ID).SetPage(filter.Page).ToList();
            }
        }

        public void Delete(int id)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Products.FirstOrDefault(e => e.ID == id);
                db.Products.Remove(entity);
                var items = db.ProductComponents.Where(e => e.ProductId == id);
                db.ProductComponents.RemoveRange(items);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 生产一个型号产品，消耗库存组件
        /// </summary>
        public void Produce(int productId, int orderId)
        {
            var items = GetItems(productId);
            using (var db = GetDataContext())
            {
                foreach (var item in items)
                {
                    var query = db.Inventorys.Where(e => e.InfoID == item.ID && e.InfoType == InfoType.Component);
                    if (query.Sum(e => e.Number) >= item.Number)
                    {
                        //根据型号的部件的数量，减去库存的数量
                        var total = item.Number;
                        foreach (var component in query)
                        {
                            if (total > component.Number)
                            {
                                component.Number = 0;
                                total = total - component.Number;
                            }
                            else
                            {
                                component.Number = component.Number - total;
                                break;
                            }
                        }
                        db.ProduceLogs.Add(new ProduceLog { ComponentID = item.ID, ProductID = productId, Number = item.Number, OrderID = orderId });
                    }
                }
                db.SaveChanges();
            }
        }

        public int Save(Product model)
        {
            using (var db = GetDataContext())
            {
                var exist = db.Products.FirstOrDefault(e => e.Number.ToUpper() == model.Number.ToUpper());
                if (exist != null)
                {
                    if ((model.ID > 0 && exist.ID != model.ID) || model.ID == 0)
                    {
                        throw new ArgumentException("存在相同型号的产品，请核对产品型号名");
                    }
                }
                if (model.ID > 0)
                {
                    var entity = db.Products.FirstOrDefault(e => e.ID == model.ID);
                    if (entity != null)
                    {
                        db.Entry(entity).CurrentValues.SetValues(model);
                    }
                }
                else
                {
                    db.Products.Add(model);
                }
                db.SaveChanges();
            }
            return model.ID;
        }

        public void AddPriceLog(int productId, double price)
        {
            using (var db = GetDataContext())
            {
                db.ProductPriceLogs.Add(new ProductPriceLog { Price = price, ProductId = productId });
                db.SaveChanges();
            }
        }

        public List<ProductPriceLog> GetPriceLogs(int productId)
        {
            using (var db = GetDataContext())
            {
                return db.ProductPriceLogs.Where(e => e.ProductId == productId).ToList();
            }
        }

        public List<ProductComponent> GetItems(int productId)
        {
            using (var db = GetDataContext())
            {
                var list = db.ProductComponents.Where(e => e.ProductId == productId).ToList();
                foreach (var item in list)
                {
                    item.Component = Core.ComponentManager.GetModel(item.ComponentId);
                }
                return list;
            }
        }

        public void SaveItems(IEnumerable<ProductComponent> items)
        {
            using (var db = GetDataContext())
            {
                var productId = 0;
                var firstItem = items.FirstOrDefault();
                if (firstItem == null)
                {
                    return;
                }
                productId = firstItem.ProductId;

                var entity = db.Products.FirstOrDefault(e => e.ID == productId);
                if (entity != null)
                {
                    var old = db.ProductComponents.Where(e => e.ProductId == productId);
                    db.ProductComponents.RemoveRange(old);
                    db.ProductComponents.AddRange(items);
                    db.SaveChanges();
                }
            }
        }
    }
}
