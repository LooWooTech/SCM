using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LoowooTech.SCM.Manager
{
    public class OrderItemManager : ManagerBase
    {
        public List<OrderItem> Acquire(HttpContextBase context, int OID)
        {
            var ids = context.Request.Form["ComponentId"].Split(',');
            var names = context.Request.Form["Component"].Split(',');
            var prices = context.Request.Form["Price"].Split(',');
            var numbers = context.Request.Form["Number"].Split(',');
            var count = ids.Length;
            if (names.Length != count || prices.Length != count || numbers.Length != count)
            {
                throw new ArgumentException("获取对应的值数量不对");
            }

            var result = new List<OrderItem>();
            for (var i = 0; i < count; i++)
            {
                var id = 0;
                int.TryParse(ids[i], out id);
                if (id == 0)
                {
                    throw new ArgumentException("没有选择正确的部件");
                }

                double price = 0;
                if (!double.TryParse(prices[i], out price))
                {
                    throw new ArgumentException("单价填写不正确");
                }

                var number = 0;
                int.TryParse(numbers[i], out number);
                if (number == 0)
                {
                    throw new ArgumentException("数量填写不正确");
                }

                result.Add(new OrderItem
                {
                    ComponentId = id,
                    Price = price,
                    Number = number,
                    OrderId = OID,
                });
            }
            return result;
        }

        public Dictionary<int, int> Acquire(HttpContextBase context, List<int> Names)
        {
            Dictionary<int, int> values = new Dictionary<int, int>();
            foreach (var item in Names)
            {
                if (context.Request.Form[item.ToString()] != null)
                {
                    string key = context.Request.Form[item.ToString()].ToString();
                    int Number = 0;
                    int.TryParse(key, out Number);
                    if (!values.ContainsKey(item))
                    {
                        values.Add(item, Number);
                    }
                }
                else
                {
                    throw new ArgumentException("内部服务器错误");
                }
            }
            return values;
        }

        public bool Check(Dictionary<int, int> DICT)
        {
            foreach (var item in DICT.Keys)
            {
                var entity = Get(item);
                if (entity.Number < DICT[item])
                {
                    return false;
                }
            }
            return true;
        }

        public void Update(Dictionary<int, int> DICT)
        {
            if (!Check(DICT))
            {
                throw new ArgumentException("最终确认的部件清单数量超出下单的数量，请核对");
            }
            foreach (var item in DICT.Keys)
            {
                Update(item, DICT[item]);
            }
        }

        public void Update(int ID, int Number)
        {
            var quotation = Get(ID);
            if (quotation.Number < Number)
            {
                return;
            }
            quotation.Number = Number;
            try
            {
                Update(quotation);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void Update(OrderItem quotation)
        {
            using (var db = GetDataContext())
            {
                var entity = db.OrderItems.Find(quotation.ID);
                if (entity != null)
                {
                    db.Entry(entity).CurrentValues.SetValues(quotation);
                    db.SaveChanges();
                }
            }
        }

        public OrderItem Get(int ID)
        {
            using (var db = GetDataContext())
            {
                return db.OrderItems.Find(ID);
            }
        }

        public void AddAll(List<OrderItem> List)
        {
            foreach (var item in List)
            {
                try
                {
                    Add(item);
                }
                catch
                {

                }

            }
        }

        public List<OrderItem> GetByOID(int OID)
        {
            using (var db = GetDataContext())
            {
                return db.OrderItems.Where(e => e.OrderId == OID).ToList();
            }
        }

        public void Add(OrderItem quotation)
        {
            using (var db = GetDataContext())
            {
                db.OrderItems.Add(quotation);
                db.SaveChanges();
            }
        }

        public List<OrderItem> GetList(int orderId)
        {
            using (var db = GetDataContext())
            {
                var list = db.OrderItems.Where(e => e.OrderId == orderId).ToList();
                foreach (var item in list)
                {
                    item.Component = Core.ComponentManager.GetModel(item.ComponentId);
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

        public void Save(int orderId, List<OrderItem> list)
        {
            using (var db = GetDataContext())
            {
                var added = db.OrderItems.Where(e => e.OrderId == orderId);
                db.OrderItems.RemoveRange(added);
                db.OrderItems.AddRange(list);
                db.SaveChanges();
            }
        }
    }
}
