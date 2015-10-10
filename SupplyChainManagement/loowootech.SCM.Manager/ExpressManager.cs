using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class ExpressManager : ManagerBase
    {
        public Express GetModel(int id)
        {
            if (id == 0) return null;
            using (var db = GetDataContext())
            {
                return db.Expresses.FirstOrDefault(e => e.ID == id);
            }
        }

        public List<Express> GetList()
        {
            using (var db = GetDataContext())
            {
                return db.Expresses.Where(e => !e.Deleted).ToList();
            }
        }

        public void Delete(int id)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Expresses.FirstOrDefault(e => e.ID == id);
                entity.Deleted = true;
                db.SaveChanges();
            }
        }

        public void Save(Express model)
        {
            using (var db = GetDataContext())
            {
                var exist = db.Expresses.FirstOrDefault(e => e.Name == model.Name && !e.Deleted);
                if (exist != null)
                {
                    if (model.ID == 0 || (model.ID > 0 && exist.ID != model.ID))
                    {
                        throw new ArgumentException("该快递公司名称已存在");
                    }
                }
                if (model.ID > 0)
                {
                    var entity = db.Expresses.FirstOrDefault(e => e.ID == model.ID);
                    if (entity != null)
                    {
                        db.Entry(entity).CurrentValues.SetValues(model);
                    }
                }
                else
                {
                    db.Expresses.Add(model);
                }
                db.SaveChanges();
            }
        }
    }
}
