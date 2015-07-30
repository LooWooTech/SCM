using loowootech.SCM.Model;
using loowootech.SCM.Common;
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

        public void Delete(int ID)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Components.Find(ID);
                if (entity == null) return;
                db.Components.Remove(entity);
                db.SaveChanges();
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

        public Item GetComponents(Item item)
        {
            using (var db = GetDataContext())
            {
                item.Components = db.Components.Find(item.CID);
            }
            return item;
        }

        public int GetKey(string Value)
        {
            string[] str = Value.Split('-');
            if (str.Count() != 4)
            {
                return 0;
            }
            UnitType type = str[1].GetEnum();
            string Brand = str[0];
            string specification = str[2];
            string Number = str[3];
            using (var db = GetDataContext())
            {
                var entity = db.Components.FirstOrDefault(e => e.Brand == Brand && e.Specification == specification && e.Number == Number&&e.Type==type);
                if (entity != null)
                {
                    return entity.ID;
                }
            }
            return 0;
        }
    }
}
