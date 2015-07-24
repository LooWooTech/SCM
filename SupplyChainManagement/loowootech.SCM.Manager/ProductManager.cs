using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace loowootech.SCM.Manager
{
    public class ProductManager:ManagerBase
    {
        public List<Product> GetAll()
        {
            var listTemp = Get();
            List<Product> list = new List<Product>();
            foreach (var item in listTemp)
            {
                list.Add(Core.ItemManager.GetList(item));
            }
            return list;
        }

        public Product Get(int ID)
        {
            using (var db = GetDataContext())
            {
                return db.Products.Find(ID);
            }
        }

        public List<Product> Get()
        {
            using (var db = GetDataContext())
            {
                return db.Products.ToList();
            }
        }
        public int Add(Product product)
        {
            if (!Validate(product))
            {
                throw new ArgumentException("存在相同型号的产品，请核对产品型号名");
            }
            using (var db = GetDataContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
            Core.RateManager.Add(new Rate
            {
                Price = product.Price,
                SID = product.ID
            });
            return product.ID;
        }

        public bool Validate(Product product)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Products.Where(e => e.Number.ToUpper() == product.Number.ToUpper()).FirstOrDefault();
                return entity == null ? true : false;
            }
        }

        public void Edit(Product product)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Products.FirstOrDefault(e => e.Number.ToUpper() == product.Number.ToUpper());
                if (entity != null)
                {
                    product.ID = entity.ID;
                    db.Entry(entity).CurrentValues.SetValues(product);
                    db.SaveChanges();
                }
            }
        }

        public void Edit(Rate rate)
        {
            var product = Get(rate.SID);
            if (product != null)
            {
                product.Price = rate.Price;
                Edit(product);
            }
        }
    }
}
