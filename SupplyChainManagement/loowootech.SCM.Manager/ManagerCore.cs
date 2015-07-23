using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loowootech.SCM.Manager
{
    public class ManagerCore
    {
        public static ManagerCore Instance = new ManagerCore();
        private EnterpriseManager _enterpriseManager;
        public EnterpriseManager EnterpriseManager
        {
            get { return _enterpriseManager == null ? _enterpriseManager = new EnterpriseManager() : _enterpriseManager; }
        }

        private ComponentsManager _componentsManager;
        public ComponentsManager ComponentsManager
        {
            get { return _componentsManager == null ? _componentsManager = new ComponentsManager() : _componentsManager; }
        }

        private ContactManager _contactManager;
        public ContactManager ContactManager
        {
            get { return _contactManager == null ? _contactManager = new ContactManager() : _contactManager; }
        }

        private AddressListManager _addresslistManager;
        public AddressListManager AddressListManager
        {
            get { return _addresslistManager == null ? _addresslistManager = new AddressListManager() : _addresslistManager; }
        }

        private QuotationManager _quotationManager;
        public QuotationManager QuotationManager
        {
            get { return _quotationManager == null ? _quotationManager = new QuotationManager() : _quotationManager; }
        }
        private OrderManager _orderManager;
        public OrderManager OrderManager
        {
            get { return _orderManager == null ? _orderManager = new OrderManager() : _orderManager; }
        }
        
    }
}
