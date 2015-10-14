using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class ManagerCore
    {
        private ManagerCore() { }
        public static readonly ManagerCore Instance = new ManagerCore();
        private EnterpriseManager _enterpriseManager;
        public EnterpriseManager EnterpriseManager
        {
            get { return _enterpriseManager == null ? _enterpriseManager = new EnterpriseManager() : _enterpriseManager; }
        }

        private ComponentManager _componentManager;
        public ComponentManager ComponentManager
        {
            get { return _componentManager == null ? _componentManager = new ComponentManager() : _componentManager; }
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

        private OrderItemManager _orderItemManager;
        public OrderItemManager OrderItemManager
        {
            get { return _orderItemManager == null ? _orderItemManager = new OrderItemManager() : _orderItemManager; }
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

        private ExpressManager _expressManager;
        public ExpressManager ExpressManager
        {
            get { return _expressManager == null ? _expressManager = new ExpressManager() : _expressManager; }
        }

        private ContractManager _contractManager;
        public ContractManager ContractManager
        {
            get { return _contractManager == null ? _contractManager = new ContractManager() : _contractManager; }
        }
        
    }
}
