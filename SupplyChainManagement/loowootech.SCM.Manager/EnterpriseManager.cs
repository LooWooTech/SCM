using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Manager
{
    public class EnterpriseManager:ManagerBase
    {
        public List<Enterprise> Get(Business business)
        {
            using (var db = GetDataContext())
            {
                return db.Enterprises.Where(e => e.Business == business).ToList();
            }
        }

        public int Add(Enterprise enterprise)
        {
            if (!Validate(enterprise))
            {
                throw new ArgumentException("存在相同的企业名称！");
            }
            using (var db = GetDataContext())
            {
                db.Enterprises.Add(enterprise);
                db.SaveChanges();
                return enterprise.ID;
            }
        }

        private bool  Validate(Enterprise enterprise)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Enterprises.FirstOrDefault(e => e.Name.ToUpper() == enterprise.Name.ToUpper());
                return entity == null ? true : false;
            }
        }
    }
}
