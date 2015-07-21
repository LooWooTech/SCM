using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Manager
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

        public Dictionary<Contact,Dictionary<ContactWay,List<AddressList>>> GetAddressList(int ID)
        {
            Dictionary<Contact, Dictionary<ContactWay, List<AddressList>>> DICT = new Dictionary<Contact, Dictionary<ContactWay, List<AddressList>>>();
            List<Contact> list = Get(ID);
            foreach (var item in list)
            {
                Dictionary<ContactWay, List<AddressList>> value = new Dictionary<ContactWay, List<AddressList>>();
                foreach (ContactWay way in Enum.GetValues(typeof(ContactWay)))
                {
                    value.Add(way, Core.AddressListManager.Search(item.ID, way));
                }
                DICT.Add(item, value);
            }
            return DICT;
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

        private bool Validate(Contact contact)
        {
            using (var db = GetDataContext())
            {
                var entity = db.Contacts.FirstOrDefault(e => e.Name.ToUpper() == contact.Name.ToUpper()&&e.sex==contact.sex);
                return entity == null ? true : false;
            }
        }
    }
}
