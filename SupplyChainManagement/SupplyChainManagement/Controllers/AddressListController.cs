using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class AddressListController : ControllerBase
    {
        [HttpPost]
        public ActionResult Add(int ID, ContactWay way, string value)
        {
            if (ID > 0)
            {
                Contact cont = Core.ContactManager.GetByID(ID);
                Enterprise enterprise = Core.EnterpriseManager.GetList(cont.EID);
                var Index = Core.AddressListManager.Add(new AddressList
                {
                    way = way,
                    Value = value,
                    CID = ID
                });
                return RedirectToAction("Index", "Enterprise", new { business = enterprise.Business });
            }
            else
            {
                throw new ArgumentException("添加联系方式的时候，没有选择联系人或者内部服务器错误，请核对当前联系方式对应的联系人");
            }
            
            
        }
    }
}
