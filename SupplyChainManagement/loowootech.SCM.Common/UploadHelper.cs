using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LoowooTech.SCM.Common
{
    public static class UploadHelper
    {
        private static string UploadDirectory = "Indentures/";

        private static string GetAbsoluteUploadDirectory(string fileName)
        {
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UploadDirectory, fileName);
        }

        public static HttpPostedFileBase GetPostedFile(this HttpContextBase context)
        {
            if (context.Request.Files.Count == 0)
            {
                return null;
            }

            HttpPostedFileBase file = null;
            for (var i = 0; i < context.Request.Files.Count; i++)
            {
                file = context.Request.Files[i];
                if (file.ContentLength > 0)
                {
                    break;
                }
            }
            return file;
        }

        public static string Upload(this HttpPostedFileBase file)
        {
            var ext = System.IO.Path.GetExtension(file.FileName);
            var fileName = file.FileName.Replace(ext, "") + "-" + DateTime.Now.Ticks.ToString() + ext;
            if (fileName.Length > 100)
            {
                fileName = fileName.Substring(fileName.Length - 100);
            }
            file.SaveAs(GetAbsoluteUploadDirectory(fileName));
            return UploadDirectory+fileName;
        }
    }
}
