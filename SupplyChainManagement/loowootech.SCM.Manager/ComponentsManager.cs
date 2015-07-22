using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Manager
{
    public class ComponentsManager:ManagerBase
    {
        public List<Components> Get()
        {
            using (var db = GetDataContext())
            {
                return db.Components.ToList();
            }
        }


        public int Add(Components components)
        {
            if (!Validate(components))
            {
                throw new ArgumentException("存在相同类型、品牌、规格的部件");
            }
            using (var db = GetDataContext())
            {
                db.Components.Add(components);
                db.SaveChanges();
                return components.ID;
            }
        }


        private bool Validate(Components componets)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Components.FirstOrDefault(e => e.Type == componets.Type && e.Brand.ToUpper() == componets.Brand.ToUpper() && e.Specification.ToUpper() == componets.Specification.ToUpper());
                return entity == null ? true : false;
            }
        }
    }
}
