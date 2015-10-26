using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.SCM.Manager
{
    public class InventoryManager : ManagerBase
    {
        public int HaveProduct(int productId, int number = 1)
        {
            using (var db = GetDataContext())
            {
                var productCount = db.Inventorys.Where(e => e.InfoID == productId && e.InfoType == InfoType.Product).Sum(e => e.Number);
                return productCount - number;
            }
        }

        public bool CanProduce(int productId, int number = 1)
        {
            var result = true;
            var product = Core.ProductManager.GetModel(productId);
            using (var db = GetDataContext())
            {
                if (product != null)
                {
                    var items = Core.ProductManager.GetItems(productId);
                    foreach (var item in items)
                    {
                        var sum = db.Inventorys.Where(e => e.InfoID == item.ID && e.InfoType == InfoType.Component).Sum(e => e.Number);
                        if (sum < item.Number * number)
                        {
                            result = false;
                            break;
                        }
                    }
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
