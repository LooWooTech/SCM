using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Manager
{
    public class OrderManager:ManagerBase
    {
        public int Add(Order order)
        {
            using (var db = GetDataContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return order.ID;
            }
        }

        public Order Get(int ID)
        {
            using (var db = GetDataContext())
            {
                return db.Orders.Find(ID);
            }
        }

        public List<Order> Get()
        {
            using (var db = GetDataContext())
            {
                return db.Orders.OrderBy(e => e.Time).ToList();
            }
        }


        public List<Order> GetAll()
        {
            var listTemp = Get();
            List<Order> list = new List<Order>();
            foreach (var item in listTemp)
            {
                list.Add(GetEnterprise(item));
            }
            return list;
        }


        public Order GetEnterprise(Order order)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Enterprises.Find(order.EID);
                if (entity != null)
                {
                    order.Enterprise = entity;
                }
            }
            return order;
        }
    }
}
