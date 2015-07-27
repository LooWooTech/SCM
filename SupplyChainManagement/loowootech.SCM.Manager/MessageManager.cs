using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Manager
{
    public class MessageManager:ManagerBase
    {
        public List<Message> Get(int ID)
        {
            using (var db = GetDataContext())
            {
                return db.Messages.Where(e => e.EID == ID).ToList();
            }
        }


        public List<Message> GetAll(int ID)
        {
            var listTemp = Get(ID);
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
    }
}
