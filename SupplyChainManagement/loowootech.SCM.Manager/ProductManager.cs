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
            return list;
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
    }
}
