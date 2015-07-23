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
    }
}
