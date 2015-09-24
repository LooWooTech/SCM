using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
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

        private ProductManager _productManager;
        public ProductManager ProductManager
        {
            get { return _productManager == null ? _productManager = new ProductManager() : _productManager; }
        }

        private ItemManager _itemManager;
        public ItemManager ItemManager
        {
            get { return _itemManager == null ? _itemManager = new ItemManager() : _itemManager; }
        }

        private RateManager _rateManager;
        public RateManager RateManager
        {
            get { return _rateManager == null ? _rateManager = new RateManager() : _rateManager; }
        }

        private MessageManager _messageManager;
        public MessageManager MessageManager
        {
            get { return _messageManager == null ? _messageManager = new MessageManager() : _messageManager; }
        }

        private InventoryManager _inventoryManager;
        public InventoryManager InventoryManager
        {
            get { return _inventoryManager == null ? _inventoryManager = new InventoryManager() : _inventoryManager; }
        }

        private RemittanceManager _remittanceManager;
        public RemittanceManager RemittanceManager
        {
            get { return _remittanceManager == null ? _remittanceManager = new RemittanceManager() : _remittanceManager; }
        }
        
    }
}
