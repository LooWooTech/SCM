using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LoowooTech.SCM.Manager
{
    public class OrderComponentManager : ManagerBase
    {
        public void Update(OrderComponent model)
        {
            using (var db = GetDataContext())
            {
                var entity = db.OrderComponents.Find(model.ID);
                if (entity != null)
                {
                    db.Entry(entity).CurrentValues.SetValues(model);
                    db.SaveChanges();
                }
            }
        }

        public List<OrderComponent> GetList(int orderId)
        {
            using (var db = GetDataContext())
            {
                var list = db.OrderComponents.Where(e => e.OrderId == orderId).ToList();
                foreach (var item in list)
                {
                    item.Component = Core.ComponentManager.GetModel(item.ComponentId);
                }
                return list;
            }
        }

        public void UpdateReceiveNumber(List<OrderComponent> list)
        {
            using (var db = GetDataContext())
            {
                foreach (var item in list)
                {
                    var entity = db.OrderComponents.FirstOrDefault(e => e.ID == item.ID);
                    entity.DealNumber = item.DealNumber;
                }
                db.SaveChanges();
            }
        }

        public void UpdateComponents(int orderId, List<OrderComponent> list)
        {
            using (var db = GetDataContext())
            {
                var added = db.OrderComponents.Where(e => e.OrderId == orderId);
                db.OrderComponents.RemoveRange(added);
                db.OrderComponents.AddRange(list);
                db.SaveChanges();
            }
        }
    }
}
