using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LoowooTech.SCM.Manager
{
    public class ProductItemManager:ManagerBase
    {
        //public List<ProductItem> Acquire(HttpContextBase context,int PID)
        //{
        //    string[] SCID = context.Request.Form["CID"].Split(',');
        //    string[] SNumber = context.Request.Form["Amount"].Split(',');
        //    if (SCID.Count() != SNumber.Count())
        //    {
        //        throw new ArgumentException("服务器内部错误");
        //    }
        //    int Count = SCID.Count();
        //    int[] CID = new int[Count];
        //    for (var i = 0; i < Count; i++)
        //    {
        //        if (string.IsNullOrEmpty(SCID[i]))
        //        {
        //            throw new ArgumentException("部件ID为空，内部服务器错误");
        //        }
        //        int keyID = 0;
        //        int.TryParse(SCID[i], out keyID);
        //        CID[i] = keyID;
        //    }
        //    int[] Number = new int[Count];
        //    for (var i = 0; i < Count; i++)
        //    {
        //        if (string.IsNullOrEmpty(SNumber[i]))
        //        {
        //            throw new ArgumentException("部件对应的数量为空");
        //        }
        //        int keyNumber = 0;
        //        int.TryParse(SNumber[i], out keyNumber);
        //        Number[i] = keyNumber;
        //    }
        //    List<ProductItem> list = new List<ProductItem>();
        //    for (var i = 0; i < Count; i++)
        //    {
        //        list.Add(new ProductItem
        //        {
        //            Number = Number[i],
        //            ComponentId = CID[i],
        //            ProductId=PID
        //        });
        //    }
        //    return list;

        //}

        //public int Add(ProductItem item)
        //{
        //    using (var db = GetDataContext())
        //    {
        //        var entity = db.Items.FirstOrDefault(e => e.ProductId == item.ProductId && e.ComponentId == item.ComponentId);
        //        if (entity == null)
        //        {
        //            db.Items.Add(item);
        //            db.SaveChanges();
        //            return item.ID;
        //        }
        //        else
        //        {
        //            entity.Number += item.Number;
        //            db.SaveChanges();
        //            return entity.ID;
        //        }
                
        //    }
            
        //}

        public Product GetList(Product product)
        {
            var listTemp = Get(product.ID);
            List<ProductItem> list = new List<ProductItem>();
            foreach (var item in listTemp)
            {
                list.Add(Core.ComponentManager.GetComponents(item));
            }
            product.Items = list;
            return product;
        }

        public void Add(List<ProductItem> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        public List<ProductItem> Get(int PID)
        {
            using (var db = GetDataContext())
            {
                return db.ProductItems.Where(e => e.ProductId == PID).ToList();
            }
        }
    }
}
