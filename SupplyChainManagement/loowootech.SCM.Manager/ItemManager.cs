using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LoowooTech.SCM.Manager
{
    public class ItemManager:ManagerBase
    {
        public List<Item> Acquire(HttpContextBase context,int PID)
        {
            string[] SCID = context.Request.Form["CID"].Split(',');
            string[] SNumber = context.Request.Form["Amount"].Split(',');
            if (SCID.Count() != SNumber.Count())
            {
                throw new ArgumentException("服务器内部错误");
            }
            int Count = SCID.Count();
            int[] CID = new int[Count];
            for (var i = 0; i < Count; i++)
            {
                if (string.IsNullOrEmpty(SCID[i]))
                {
                    throw new ArgumentException("部件ID为空，内部服务器错误");
                }
                int keyID = 0;
                int.TryParse(SCID[i], out keyID);
                CID[i] = keyID;
            }
            int[] Number = new int[Count];
            for (var i = 0; i < Count; i++)
            {
                if (string.IsNullOrEmpty(SNumber[i]))
                {
                    throw new ArgumentException("部件对应的数量为空");
                }
                int keyNumber = 0;
                int.TryParse(SNumber[i], out keyNumber);
                Number[i] = keyNumber;
            }
            List<Item> list = new List<Item>();
            for (var i = 0; i < Count; i++)
            {
                list.Add(new Item
                {
                    Number = Number[i],
                    CID = CID[i],
                    PID=PID
                });
            }
            return list;

        }

        public int Add(Item item)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Items.FirstOrDefault(e => e.PID == item.PID && e.CID == item.CID);
                if (entity == null)
                {
                    db.Items.Add(item);
                    db.SaveChanges();
                    return item.ID;
                }
                else
                {
                    entity.Number += item.Number;
                    db.SaveChanges();
                    return entity.ID;
                }
                
            }
            
        }

        public Product GetList(Product product)
        {
            var listTemp = Get(product.ID);
            List<Item> list = new List<Item>();
            foreach (var item in listTemp)
            {
                list.Add(Core.ComponentsManager.GetComponents(item));
            }
            product.Items = list;
            return product;
        }

        public void Add(List<Item> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        public List<Item> Get(int PID)
        {
            using (var db = GetDataContext())
            {
                return db.Items.Where(e => e.PID == PID).ToList();
            }
        }
    }
}
