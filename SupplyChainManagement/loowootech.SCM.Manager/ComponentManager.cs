using LoowooTech.SCM.Model;
using LoowooTech.SCM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class ComponentManager : ManagerBase
    {
        public Component GetModel(int id)
        {
            if (id == 0) return null;
            using (var db = GetDataContext())
            {
                return db.Components.FirstOrDefault(e => e.ID == id);
            }
        }


        public int Save(Component model)
        {
            using (var db = GetDataContext())
            {
                if (model.ID > 0)
                {
                    var entity = db.Components.FirstOrDefault(e => e.ID == model.ID);
                    if (entity != null)
                    {
                        db.Entry(entity).CurrentValues.SetValues(model);
                    }
                }
                else
                {
                    db.Components.Add(model);
                }

                var e1 = db.Components.FirstOrDefault(e =>
                e.Type == model.Type
                && e.Brand.ToUpper() == model.Brand.ToUpper()
                && e.Specification.ToUpper() == model.Specification.ToUpper()
                );

                if ((model.ID == 0 && e1 != null) || (model.ID > 0 && e1.ID != model.ID))
                {
                    throw new ArgumentException("存在相同类型、品牌、规格的部件");
                }
                db.SaveChanges();
                return model.ID;
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
                var entity = db.Components.FirstOrDefault(e => e.Brand == Brand && e.Specification == specification && e.Number == Number && e.Type == type);
                if (entity != null)
                {
                    return entity.ID;
                }
            }
            return 0;
        }

        public List<Component> GetList(ComponentFilter filter)
        {
            using (var db = GetDataContext())
            {
                var query = db.Components.AsQueryable();
                if (!string.IsNullOrEmpty(filter.Number))
                {
                    query = query.Where(e => e.Number.Contains(filter.Number));
                }
                if (filter.UnitType.HasValue)
                {
                    query = query.Where(e => e.Type == filter.UnitType.Value);
                }

                return query.OrderByDescending(e => e.ID).SetPage(filter.Page).ToList();
            }
        }
    }
}
