using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class InventoryManager : ManagerBase
    {
        public void SetStoreNumber(List<OrderItem> list)
        {
            using (var db = GetDataContext())
            {
                foreach (var OrderItem in list)
                {
                    OrderItem.StoreNumber = db.Inventorys.Where(e => e.InfoID == OrderItem.ItemID && e.InfoType == InfoType.Product).Sum(e => (int?)e.Number) ?? 0;
                    var produceCount = OrderItem.Number - OrderItem.StoreNumber ;
                    OrderItem.Status = OrderItem.StoreNumber >= OrderItem.Number ? OrderItemStatus.Normal : OrderItemStatus.Producing;
                    OrderItem.Items = Core.ProductManager.GetItems(OrderItem.ItemID);
                    foreach (var productItem in OrderItem.Items)
                    {
                        productItem.StoreNumber = db.Inventorys.Where(e => e.InfoID == productItem.ComponentId && e.InfoType == InfoType.Component).Sum(e => (int?)e.Number) ?? 0;
                        if (productItem.StoreNumber < (productItem.Number * produceCount))
                        {
                            OrderItem.Status = OrderItemStatus.Shortage;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 是否缺货
        /// </summary>
        public OrderItemStatus GetProductStatus(int productId, int number)
        {
            using (var db = GetDataContext())
            {
                var productCount = db.Inventorys.Where(e => e.InfoID == productId && e.InfoType == InfoType.Product).Sum(e => (int?)e.Number);
                if (productCount < number)
                {
                    var produceCount = number - productCount;
                    var items = Core.ProductManager.GetItems(productId);
                    foreach (var item in items)
                    {
                        var sum = db.Inventorys.Where(e => e.InfoID == item.ID && e.InfoType == InfoType.Component).Sum(e => (int?)e.Number);
                        if (sum < item.Number * produceCount)
                        {
                            return OrderItemStatus.Shortage;
                        }
                    }
                    return OrderItemStatus.Producing;
                }
                else
                {
                    return OrderItemStatus.Normal;
                }
            }
        }
    }
}
