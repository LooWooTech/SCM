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
                var items = db.ProductItems.Where(e => e.ProductId == id);
                db.ProductItems.RemoveRange(items);
                db.SaveChanges();
            }
        }

        public int Save(Product product)
        {
            using(var db = GetDataContext())
            {
                var exist =  db.Products.FirstOrDefault(e => e.Number.ToUpper() == product.Number.ToUpper());
                if (exist != null)
                {
                    if ((product.ID > 0 && exist.ID != product.ID) || product.ID == 0)
                    {
                        throw new ArgumentException("存在相同型号的产品，请核对产品型号名");
                    }
                }
                if (product.ID > 0)
                {
                    var entity = db.Products.FirstOrDefault(e => e.ID == product.ID);
                    if (entity != null)
                    {
                        db.Entry(entity).CurrentValues.SetValues(product);
                    }
                }
                else
                {
                    db.Products.Add(product);
                }

                db.SaveChanges();
            }
            return product.ID;
        }

        public List<ProductItem> GetItems(int productId)
        {
            using (var db = GetDataContext())
            {
                var list = db.ProductItems.Where(e => e.ProductId == productId).ToList();
                foreach (var item in list)
                {
                    item.Component = Core.ComponentManager.GetModel(item.ComponentId);
                }
                return list;
            }
        }

        public void SaveItems(IEnumerable<ProductItem> items)
        {
            using (var db = GetDataContext())
            { 
                var productId = 0;
                var firstItem  = items.FirstOrDefault();
                if(firstItem == null)
                {
                    return ;
                }
                productId = firstItem.ProductId;

                var entity = db.Products.FirstOrDefault(e => e.ID == productId);
                if (entity != null)
                {
                    var old = db.ProductItems.Where(e => e.ProductId == productId);
                    db.ProductItems.RemoveRange(old);
                    db.ProductItems.AddRange(items);
                    db.SaveChanges();
                }
            }
        }
    }
}
