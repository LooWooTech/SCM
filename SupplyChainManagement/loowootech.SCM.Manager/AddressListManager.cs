using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Manager
{
    public class AddressListManager:ManagerBase
    {
        public List<AddressList> Search(int ID, ContactWay way)
        {
            using (var db = GetDataContext())
            {
                return db.AddressLists.Where(e => e.CID == ID && e.way == way).ToList();
            }
        }
    }
}
