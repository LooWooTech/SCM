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


        public bool Delete(int ID)
        {
            using (var db = GetDataContext())
            {
                var entity = db.AddressLists.Find(ID);
                if (entity != null)
                {
                    db.AddressLists.Remove(entity);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public void DeleteAll(int ID)
        {
            var list = Search(ID);
            foreach (var item in list)
            {
                try
                {
                    Delete(item.ID);
                }
                catch
                {

                }
            }
        }

        public int Add(AddressList addressList)
        {
            using (var db = GetDataContext())
            {
                var entity = db.AddressLists.FirstOrDefault(e => e.CID == addressList.CID && e.way == addressList.way);
                if (entity == null)
                {
                    db.AddressLists.Add(addressList);
                    db.SaveChanges();
                    return addressList.ID;
                }
                else
                {
                    if (string.IsNullOrEmpty(entity.Value))
                    {
                        entity.Value = addressList.Value;
                    }
                    else
                    {
                        entity.Value += "；" + addressList.Value;
                    }
                    db.SaveChanges();
                    return entity.ID;
                }
                
            }
        }

        public void Add(List<AddressList> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }
    }
}
