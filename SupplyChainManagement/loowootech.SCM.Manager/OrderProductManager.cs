using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class OrderProductManager : ManagerBase
    {
        public void Update(OrderProduct model)
        {
            using (var db = GetDataContext())
            {
                var entity = db.OrderProducts.Find(model.ID);
                if (entity != null)
                {
                    db.Entry(entity).CurrentValues.SetValues(model);
                    db.SaveChanges();
                }
            }
        }

        public List<OrderProduct> GetList(int orderId)
        {
            using (var db = GetDataContext())
            {
                var list = db.OrderProducts.Where(e => e.OrderID == orderId).ToList();
                foreach (var item in list)
                {
                    item.Product = Core.ProductManager.GetModel(item.ProductID);
                }
                return list;
            }
        }

        public void UpdateReceiveNumber(List<OrderProduct> list)
        {
            using (var db = GetDataContext())
            {
                foreach (var item in list)
                {
                    var entity = db.OrderProducts.FirstOrDefault(e => e.ID == item.ID);
                    entity.DealNumber = item.DealNumber;
                }
                db.SaveChanges();
            }
        }

        public void UpdateProducts(int orderId, List<OrderProduct> list)
        {
            using (var db = GetDataContext())
            {
                var added = db.OrderProducts.Where(e => e.OrderID == orderId);
                db.OrderProducts.RemoveRange(added);
                db.OrderProducts.AddRange(list);
                db.SaveChanges();
            }
        }
    }
}
