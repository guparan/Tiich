using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utils
{
    public class StringHelper
    {
        public static List<string> RemoveNull(List<string> list)
        {
            List<string> res = new List<string>();
            foreach (var item in list)
            {
                if(!String.IsNullOrEmpty(item))
                {
                    Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                    string str = rgx.Replace(item, "");
                    res.Add(str);
                }
            }
            return res;
        }
    }
}
