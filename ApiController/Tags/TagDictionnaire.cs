using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiController.Tags
{
    class TagDictionnaire : ITagGetter
    {
        public List<string> GetTags(string pWord)
        {
            using (WebClient client = new WebClient()) // classe WebClient hérite IDisposable
            {
                client.DownloadFile("http://site.com/page.html", @"C:\localfile.html");

                // Ou tu peux obtenir le contenu du fichier sans l'enregistrer:
                string codeHtml = client.DownloadString("http://yoursite.com/page.html");
                //...
                throw new NotImplementedException();
            }
        }

        public List<string> GetTags(List<string> pWords)
        {
            throw new NotImplementedException();
        }
    }
}
