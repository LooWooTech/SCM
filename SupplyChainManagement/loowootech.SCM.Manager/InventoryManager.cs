using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class InventoryManager:ManagerBase
    {
        public void Add(List<OrderComponent> List)
        {
            foreach (var item in List)
            {
                Add(item);
            }
        }

        public void Add(OrderComponent quotation)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Inventorys.FirstOrDefault(e => e.CID == quotation.ComponentId);
                if (entity != null)
                {
                    entity.Number += quotation.Number;
                }
                else
                {
                    db.Inventorys.Add(new Inventory { 
                        CID=quotation.ComponentId,
                        Number=quotation.Number
                    });
                }
                db.SaveChanges();
            }
        }
    }
}
