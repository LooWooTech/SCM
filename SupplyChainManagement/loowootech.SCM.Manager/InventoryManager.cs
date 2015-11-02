using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class InventoryManager : ManagerBase
    {
        /// <summary>
        /// 是否缺货
        /// </summary>
        public bool IsShortage(int productId, int number)
        {
            using (var db = GetDataContext())
            {
                var productCount = db.Inventorys.Where(e => e.InfoID == productId && e.InfoType == InfoType.Product).Sum(e => e.Number);
                if (productCount < number)
                {
                    var produceCount = number - productCount;
                    var items = Core.ProductManager.GetItems(productId);
                    foreach (var item in items)
                    {
                        var sum = db.Inventorys.Where(e => e.InfoID == item.ID && e.InfoType == InfoType.Component).Sum(e => e.Number);
                        if (sum < item.Number * produceCount)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
    }
}
