using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace loowootech.SCM.Manager
{
    public class QuotationManager:ManagerBase
    {
        public List<Quotation> Acquire(HttpContextBase context,int OID)
        {
            string[] SCID = context.Request.Form["CID"].Split(',');
            string[] SPrice = context.Request.Form["Price"].Split(',');
            string[] SNumber = context.Request.Form["Number"].Split(',');
            if (SCID.Count() != SPrice.Count() || SPrice.Count() != SNumber.Count())
            {
                throw new ArgumentException("获取对应的值数量不对");
            }
            int Count = SCID.Count();
            int[] CID = new int[Count];
            for (var i = 0; i < Count; i++)
            {
                if (string.IsNullOrEmpty(SCID[i]))
                {
                    throw new ArgumentException("无法获取部件的值");
                }
                int keyID = 0;
                int.TryParse(SCID[i], out keyID);
                CID[i] = keyID;
            }
            double[] Price = new double[Count];
            for (var i = 0; i < Count; i++)
            {
                if (string.IsNullOrEmpty(SPrice[i]))
                {
                    throw new ArgumentException("未填写单价");
                    
                }
                double keyPrice = 0.0;
                double.TryParse(SPrice[i], out keyPrice);
                Price[i] = keyPrice;
            }
            int[] Number = new int[Count];
            for (var i = 0; i < Count; i++)
            {
                if (string.IsNullOrEmpty(SNumber[i]))
                {
                    throw new ArgumentException("未填写数量");
                }
                int keyNumber = 0;
                int.TryParse(SNumber[i], out keyNumber);
                Number[i] = keyNumber;
            }
            List<Quotation> list = new List<Quotation>();
            for (var i = 0; i < Count; i++)
            {
                list.Add(new Quotation
                {
                    Price = Price[i],
                    Number = Number[i],
                    CID = CID[i],
                    OID=OID
                });
            }   
            return list;
        }

        public Dictionary<int,int> Acquire(HttpContextBase context, List<int> Names)
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

        public void  Update(int ID, int Number)
        {
            var quotation = Get(ID);
            if (quotation.Number < Number)
            {
                return ;
            }
            quotation.Number = Number;
            try
            {
                Update(quotation);
            }
            catch(Exception ex)
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
                return db.Quotations.Where(e => e.OID == OID).ToList();
            }
        }


        public Quotation GetComponents(Quotation quotation)
        {
            using (var db = GetDataContext())
            {
                var components = db.Components.Find(quotation.CID);
                if (components != null)
                {
                    quotation.Components = components;
                }
            }
            return quotation; ;
        }

        public void  Add(Quotation quotation)
        {
            using (var db = GetDataContext())
            {
                db.Quotations.Add(quotation);
                db.SaveChanges();
            }
        }
    }
}
