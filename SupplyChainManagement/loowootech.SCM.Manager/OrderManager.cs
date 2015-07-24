using loowootech.SCM.Common;
using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

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

        public Order Acquire(HttpContextBase context,int ID,string Express)
        {
            var file = UploadHelper.GetPostedFile(context);
            string FilePath = string.Empty;
            if (file != null)
            {
                FilePath = UploadHelper.Upload(file);
            }
            Order order = Get(ID);
            if (order == null)
            {
                throw new ArgumentException("未找到部件进货订单");
            }
            order.Express = Express;
            order.Indenture = FilePath;
            return order;
        }


        public void Edit(Order order)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Orders.Find(order.ID);
                if (entity != null)
                {
                    db.Entry(entity).CurrentValues.SetValues(order);
                    db.SaveChanges();
                }
            }
        }
    }
}
