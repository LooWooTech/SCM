using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class EnterpriseManager : ManagerBase
    {
        public List<Enterprise> GetList(Business business)
        {
            using (var db = GetDataContext())
            {
                return db.Enterprises.Where(e => e.Business == business).ToList();
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
