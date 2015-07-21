using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Manager
{
    public class ContactManager:ManagerBase
    {
        public Dictionary<int,List<Contact>> Get()
        {
            using (var db = GetDataContext())
            {
                return db.Contacts.GroupBy(e=>e.EID).ToDictionary(e=>e.Key,g=>g.Where(e=>e.EID==g.Key).ToList());
            }
        }

        public int Add(Contact contact)
        {
            if (!Validate(contact))
            {
                throw new ArgumentException("存在相同姓名的联系人");
            }
            using (var db = GetDataContext())
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return contact.ID;
            }
        }

        private bool Validate(Contact contact)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Contacts.FirstOrDefault(e => e.Name.ToUpper() == contact.Name.ToUpper());
                return entity == null ? true : false;
            }
        }
    }
}
