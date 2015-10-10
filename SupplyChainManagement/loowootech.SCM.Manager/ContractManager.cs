using LoowooTech.SCM.Common;
using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class ContractManager : ManagerBase
    {
        public List<Contract> GetList(int orderId)
        {
            using (var db = GetDataContext())
            {
                return db.Contracts.Where(e => e.OrderId == orderId).ToList();
            }
        }

        
        public void Save(int orderId, IEnumerable<Contract> list)
        {
            using (var db = GetDataContext())
            {
                var old = db.Contracts.Where(e => e.OrderId == orderId);
                foreach (var item in old)
                {
                    db.Contracts.Remove(item);
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, item.File);
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch { }
                }

                db.Contracts.AddRange(list);
                db.SaveChanges();
            }
        }
    }
}
