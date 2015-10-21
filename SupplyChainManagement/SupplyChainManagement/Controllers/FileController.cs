using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class FileController : ControllerBase
    {
        public ActionResult Index(string fileName)
        {
            var path = Request.MapPath(fileName);
            return File(path, WebUtility.GetContentType(path));
        }

    }
}
