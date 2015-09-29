using loowootech.SCM.Model;
using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class EnterpriseManager : ManagerBase
    {
        public List<Enterprise> GetList(EnterpriseFilter filter)
        {
            using (var db = GetDataContext())
            {
                var query = db.Enterprises.Where(e => e.Business == filter.Business);
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    query = query.Where(e => e.Name.Contains(filter.Name));
                }
                return query.OrderByDescending(e => e.ID).SetPage(filter.Page).ToList();
            }
        }

        public Enterprise GetModel(int id)
        {
            if (id == 0) return null;
            using (var db = GetDataContext())
            {
                return db.Enterprises.Find(id);
            }
        }

        public int Save(Enterprise enterprise)
        {
            using (var db = GetDataContext())
            {
                db.Enterprises.Add(enterprise);
                db.SaveChanges();
                return enterprise.ID;
            }
        }
    }
}
