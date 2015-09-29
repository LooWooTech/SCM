using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class ContactManager:ManagerBase
    {
        public Contact GetModel(int id)
        {
            using (var db = GetDataContext())
            {
                return db.Contacts.FirstOrDefault(e => e.ID == id);
            }
        }


        public int Save(Contact contact)
        {
            using (var db = GetDataContext())
            {
                if (contact.ID > 0)
                {
                    var entity = db.Contacts.FirstOrDefault(e => e.ID == contact.ID);
                    if (entity != null)
                    {
                        db.Entry(entity).CurrentValues.SetValues(contact);
                    }
                }
                else
                {
                    db.Contacts.Add(contact);
                }
                db.SaveChanges();
                return contact.ID;
            }
        }

        public void Delete(int ID)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Contacts.Find(ID);
                if (entity == null) return;
                db.Contacts.Remove(entity);
                db.SaveChanges();
            }
        }


        public Message GetContact(Message message)
        {
            message.Contact = GetModel(message.ContactId);
            return message;
        }

        public List<Contact> GetList(int enterpriseId)
        {
            using (var db = GetDataContext())
            {
                return db.Contacts.Where(e => e.EnterpriseId == enterpriseId).ToList();
            }
        }
    }
}
