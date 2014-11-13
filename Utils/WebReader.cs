using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class WebReader
    {
        public static string GetWebData(string url)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData(url);
            string webData = System.Text.Encoding.UTF8.GetString(raw);
            return webData;
        }
    }
}
