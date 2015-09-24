using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class InventoryManager:ManagerBase
    {
        public void Add(List<Quotation> List)
        {
            foreach (var item in List)
            {
                Add(item);
            }
        }

        public void Add(Quotation quotation)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Inventorys.FirstOrDefault(e => e.CID == quotation.CID);
                if (entity != null)
                {
                    entity.Number += quotation.Number;
                }
                else
                {
                    db.Inventorys.Add(new Inventory { 
                        CID=quotation.CID,
                        Number=quotation.Number
                    });
                }
                db.SaveChanges();
            }
        }
    }
}
