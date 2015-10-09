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
                var query = db.Enterprises.Where(e => !e.Deleted);
                if (filter == null)
                {
                    return query.ToList();
                }

                if (filter.Ids != null && filter.Ids.Length > 0)
                {
                    query = query.Where(e => filter.Ids.Contains(e.ID));
                }

                if (filter.Business.HasValue)
                {
                    query = query.Where(e => e.Business == filter.Business.Value);
                }
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

        public int Save(Enterprise model)
        {
            using (var db = GetDataContext())
            {
                if (model.ID > 0)
                {
                    var entity = db.Enterprises.FirstOrDefault(e => e.ID == model.ID);
                    if (entity != null)
                    {
                        db.Entry(entity).CurrentValues.SetValues(model);
                    }
                }
                else
                {
                    db.Enterprises.Add(model);
                }
                db.SaveChanges();
                return model.ID;
            }
        }

        public void Delete(int id)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Enterprises.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    entity.Deleted = true;
                    db.SaveChanges();
                }

            }
        }
    }
}
