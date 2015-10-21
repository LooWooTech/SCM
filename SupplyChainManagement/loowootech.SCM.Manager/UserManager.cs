using LoowooTech.SCM.Model;
using LoowooTech.SCM.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class UserManager : ManagerBase
    {
        public User GetModel(int id)
        {
            using (var db = GetDataContext())
            {
                return db.Users.FirstOrDefault(e => e.ID == id);
            }
        }

        public User GetModel(string username, string password = null)
        {
            using (var db = GetDataContext())
            {
                var model = db.Users.FirstOrDefault(e => e.Username.ToLower() == username.ToLower() && !e.Deleted);
                if (model != null)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        if (model.Password != password.MD5())
                        {
                            throw new ArgumentException("密码不正确");
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("没有找到此用户");
                }
                return model;
            }
        }

        public void Save(User model)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Users.FirstOrDefault(e => e.Username.ToLower() == model.Username.ToLower());
                if (model.ID > 0)
                {
                    if (entity != null && entity.ID != model.ID)
                    {
                        throw new ArgumentException("用户名已使用");
                    }
                    entity = db.Users.FirstOrDefault(e => e.ID == model.ID);
                    if (entity != null)
                    {
                        entity.Username = model.Username;
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            entity.Password = model.Password.MD5();
                        }
                    }
                }
                else
                {
                    if (entity != null)
                    {
                        throw new ArgumentException("用户名已使用");
                    }
                    model.Password = model.Password.MD5();
                    db.Users.Add(model);
                    db.SaveChanges();
                }
            }
        }

        public User GetUserByEnterpriseId(int enterpriseId)
        {
            using (var db = GetDataContext())
            {
                return db.Users.FirstOrDefault(e => e.EnterpriseId == enterpriseId);
            }
        }

        public void Delete(int id)
        {
            using (var db = GetDataContext())
            {
                var model = db.Users.FirstOrDefault(e => e.ID == id);
                model.Deleted = true;
                db.SaveChanges();
            }
        }
    }
}
