using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class EpistolizeController : ControllerBase
    {
        //
        // GET: /Epistolize/

        public ActionResult Index(int ID)
        {
            var enterprise = Core.EnterpriseManager.GetModel(ID);
            if (enterprise == null)
            {
                throw new ArgumentException("未找到相关企业信息");
            }
            ViewBag.List = Core.ContactManager.GetModel(enterprise.ID);
            ViewBag.Enterprise = enterprise;
            ViewBag.MList = Core.MessageManager.GetAll(ID);
            return View();
        }

        [HttpPost]
        public ActionResult Add(Message message)
        {
            var index = Core.MessageManager.Add(message);
            return RedirectToAction("Index", new { ID = message.EID });
        }
    }
}
