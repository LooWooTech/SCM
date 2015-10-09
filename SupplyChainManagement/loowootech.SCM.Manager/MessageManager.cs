using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class MessageManager:ManagerBase
    {
        public List<Message> GetEnterpriseMessages(int enterpriseId)
        {
            using (var db = GetDataContext())
            {
                return db.Messages.Where(e => e.EnterpriseId == enterpriseId).ToList();
            }
        }


        public List<Message> GetAll(int ID)
        {
            var listTemp = GetEnterpriseMessages(ID);
            List<Message> list = new List<Message>();
            foreach (var item in listTemp)
            {
                list.Add(Core.ContactManager.GetContact(item));
            }
            return list;
        }

        public int Add(Message message)
        {
            using (var db = GetDataContext())
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return message.ID;
            }
        }

        public Message GetModelByOrderId(int orderId)
        {
            using (var db = GetDataContext())
            {
                return db.Messages.FirstOrDefault(e => e.OrderId == orderId);
            }
        }

        public void Save(Message model)
        {
            using (var db = GetDataContext())
            {
                if (model.ID > 0)
                {
                    var entity = db.Messages.FirstOrDefault(e => e.ID == model.ID);
                    if (entity != null)
                    {
                        db.Entry(entity).CurrentValues.SetValues(model);
                    }
                }
                else
                {
                    db.Messages.Add(model);
                }
                db.SaveChanges();
            }
        }
    }
}
