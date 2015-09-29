using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LoowooTech.SCM.Manager
{
    public class QuotationManager : ManagerBase
    {
        public List<Quotation> Acquire(HttpContextBase context, int OID)
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

            var result = new List<Quotation>();
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

                result.Add(new Quotation
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

        public void Update(Quotation quotation)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Quotations.Find(quotation.ID);
                if (entity != null)
                {
                    db.Entry(entity).CurrentValues.SetValues(quotation);
                    db.SaveChanges();
                }
            }
        }

        public Quotation Get(int ID)
        {
            using (var db = GetDataContext())
            {
                return db.Quotations.Find(ID);
            }
        }

        public void AddAll(List<Quotation> List)
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

        public List<Quotation> GetAll(int OID)
        {
            var listTemp = GetByOID(OID);
            List<Quotation> list = new List<Quotation>();
            foreach (var item in listTemp)
            {
                list.Add(GetComponents(item));
            }
            return list;
        }

        public List<Quotation> GetByOID(int OID)
        {
            using (var db = GetDataContext())
            {
                return db.Quotations.Where(e => e.OrderId == OID).ToList();
            }
        }


        public Quotation GetComponents(Quotation quotation)
        {
            using (var db = GetDataContext())
            {
                var components = db.Components.Find(quotation.ComponentId);
                if (components != null)
                {
                    quotation.Components = components;
                }
            }
            return quotation; ;
        }

        public void Add(Quotation quotation)
        {
            using (var db = GetDataContext())
            {
                db.Quotations.Add(quotation);
                db.SaveChanges();
            }
        }
    }
}
