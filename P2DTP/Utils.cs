using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DTP
{
    public class Utils
    {
        public static bool IsImageExtenstion(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            string[] imgExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            return Array.IndexOf(imgExtensions, ext) > -1;
        }

    }
}
