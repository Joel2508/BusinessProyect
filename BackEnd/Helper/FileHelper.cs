using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BackEnd.Helper
{
    public class FileHelper
    {
        public static string UpPhote(HttpPostedFileBase file, string folde)
        {
            var path = string.Empty;
            var pic = string.Empty;

            if(file != null)
            {
                pic = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folde), pic);
                file.SaveAs(path);
            }

            return pic;
        }
        
    }
}