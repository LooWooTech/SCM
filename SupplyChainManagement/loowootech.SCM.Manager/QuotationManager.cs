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
            using (var db = GetDataContext())
            {
                return db.Quotations.Where(e => e.OID == OID).ToList();
            }
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
