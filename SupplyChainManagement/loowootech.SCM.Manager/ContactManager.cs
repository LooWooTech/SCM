using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class ContactManager:ManagerBase
    {
        public List<Contact> Get(int ID)
        {
            using (var db = GetDataContext())
            {
                return db.Contacts.Where(e => e.EID == ID).ToList();
            }
        }

        public Dictionary<Contact,List<AddressList>> GetAddressList(int ID)
        {
            Dictionary<Contact, List<AddressList>> DICT = new Dictionary<Contact, List<AddressList>>();
            
            List<Contact> list = Get(ID);
            foreach (var item in list)
            {
                DICT.Add(item, Core.AddressListManager.Search(item.ID));
            }
            return DICT;
        }

        public Dictionary<int,string> GetNames(int ID)
        {
            using (var db = GetDataContext())
            {
                return db.Contacts.Where(e => e.EID == ID).ToDictionary(e => e.ID, e => e.Name);
            }
        }

        public Contact GetByID(int ID)
        {
            using (var db = GetDataContext())
            {
                return db.Contacts.Find(ID);
            }
        }

        public int Add(Contact contact)
        {
            if (!Validate(contact))
            {
                throw new ArgumentException("存在相同信息的联系人");
            }
            using (var db = GetDataContext())
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return contact.ID;
            }
        }

        public void Delete(int ID)
        {
            Core.AddressListManager.DeleteAll(ID);
            using (var db = GetDataContext())
            {
                var entity = db.Contacts.Find(ID);
                if (entity == null) return;
                db.Contacts.Remove(entity);
                db.SaveChanges();
            }
        }

        private bool Validate(Contact contact)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Contacts.FirstOrDefault(e => e.Name.ToUpper() == contact.Name.ToUpper()&&e.sex==contact.sex);
                return entity == null ? true : false;
            }
        }

        public Message GetContact(Message message)
        {
            message.Contact = GetByID(message.CID);
            return message;
        }
    }
}
