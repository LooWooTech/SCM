using LoowooTech.SCM.Common;
using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LoowooTech.SCM.Manager
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

        public void Done(int ID)
        {
            var order = GetModel(ID);
            order.State = State.Done;
            Update(order);
        }


        public Order GetModel(int id)
        {
            if (id == 0) return null;
            using (var db = GetDataContext())
            {
                return db.Orders.Find(id);
            }
        }


        public List<Order> Get(OrderType Type)
        {
            using (var db = GetDataContext())
            {
                return db.Orders.Where(e=>e.Type==Type).OrderBy(e => e.CreateTime).ToList();
            }
        }



        public List<Order> GetAll(OrderType Type)
        {
            var listTemp = Get(Type);
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
                var entity = db.Enterprises.Find(order.EnterpriseId);
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
            Order order = GetModel(ID);
            if (order == null)
            {
                throw new ArgumentException("未找到部件进货订单");
            }
            order.ExpressNo = Express;
            order.Indenture = FilePath;
            if (!string.IsNullOrEmpty(Express))
            {
                order.State = State.Shipping;
            }
            return order;
        }


        public void Update(Order order)
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

        public void  Delete(int ID)
        {
            var order = GetModel(ID);
            if (order == null || order.State != State.Place)
            {
                throw new ArgumentException("当前要删除的订单无效或者无法删除状态");
            }

        }

        public List<Order> GetList(OrderFilter filter)
        {
            using (var db = GetDataContext())
            {
                var query = db.Orders.AsQueryable();
                if (filter.EnterpriseId > 0)
                {
                    query = query.Where(e => e.EnterpriseId == filter.EnterpriseId);
                }
                if (filter.Type.HasValue)
                {
                    query = query.Where(e => e.Type == filter.Type.Value);
                }
                if (filter.State.HasValue)
                {
                    query = query.Where(e => e.State == filter.State.Value);
                }

                var list = query.OrderByDescending(e => e.CreateTime).SetPage(filter.Page).ToList();
                if (filter.EnterpriseId == 0)
                {
                    var enterprises = Core.EnterpriseManager.GetList(new EnterpriseFilter { Ids = list.Select(e => e.EnterpriseId).ToArray() });
                    foreach (var item in list)
                    {
                        item.Enterprise = enterprises.FirstOrDefault(e => e.ID == item.EnterpriseId);
                    }
                }
                else
                {
                    var enterprise = Core.EnterpriseManager.GetModel(filter.EnterpriseId);
                    foreach (var item in list)
                    {
                        item.Enterprise = enterprise;
                    }
                }
                return list;
            }
        }
    }
}
