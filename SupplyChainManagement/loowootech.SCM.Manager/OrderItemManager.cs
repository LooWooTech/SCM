using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class OrderItemManager : ManagerBase
    {
        public void Update(OrderItem model)
        {
            using (var db = GetDataContext())
            {
                var entity = db.OrderItems.Find(model.ID);
                if (entity != null)
                {
                    db.Entry(entity).CurrentValues.SetValues(model);
                    db.SaveChanges();
                }
            }
        }

        public List<OrderItem> GetList(int orderId)
        {
            using (var db = GetDataContext())
            {
                var list = db.OrderItems.Where(e => e.OrderID == orderId).ToList();
                foreach (var item in list)
                {
                    if (item.ItemType == OrderItemType.Product)
                    {
                        var product = Core.ProductManager.GetModel(item.ItemID);
                        item.ItemName = product == null ? null : product.Number;
                    }
                    else
                    {
                        var component = Core.ComponentManager.GetModel(item.ID);
                        item.ItemName = component == null ? null : component.DisplayName;
                    }
                }
                return list;
            }
        }

        public void UpdateReceiveNumber(List<OrderItem> list)
        {
            using (var db = GetDataContext())
            {
                foreach (var item in list)
                {
                    var entity = db.OrderItems.FirstOrDefault(e => e.ID == item.ID);
                    entity.DealNumber = item.DealNumber;
                }
                db.SaveChanges();
            }
        }

        public void UpdateItems(int orderId, List<OrderItem> list)
        {
            using (var db = GetDataContext())
            {
                var added = db.OrderItems.Where(e => e.OrderID == orderId);
                db.OrderItems.RemoveRange(added);
                db.OrderItems.AddRange(list);
                db.SaveChanges();
            }
        }
    }
}
