using loowootech.SCM.Model;
using loowootech.SCM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace loowootech.SCM.Manager
{
    public class AddressListManager:ManagerBase
    {
        public List<AddressList> Search(int ID)
        {
            using (var db = GetDataContext())
            {
                return db.AddressLists.Where(e=>e.CID==ID).ToList();
            }
        }

        public List<AddressList> Acquire(HttpContextBase context,int ID)
        {
            var list = new List<AddressList>();
            foreach (ContactWay way in Enum.GetValues(typeof(ContactWay)))
            {
                string value = context.Request.Form[way.GetDescription()];
                if (!string.IsNullOrEmpty(value))
                {
                    list.Add(new AddressList()
                    {
                        way = way,
                        Value = value,
                        CID=ID
                    });
                }
            }
            return list;
        }

        public void Add(List<AddressList> list)
        {
            using (var db = GetDataContext())
            {
                foreach (var item in list)
                {
                    var entity = db.AddressLists.FirstOrDefault(e => e.CID == item.CID && e.way == item.way);
                    if (entity == null)
                    {
                        db.AddressLists.Add(item);
                        
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(entity.Value))
                        {
                            entity.Value = item.Value;
                        }
                        else
                        {
                            entity.Value += "；" + item.Value;
                        }
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}
