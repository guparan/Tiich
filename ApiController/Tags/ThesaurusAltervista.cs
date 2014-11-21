using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ApiController.Tags
{
    public class ThesaurusAltervista : ITagGetter
    {
        static string _baseUrl = "http://thesaurus.altervista.org/thesaurus/v1";
        static string _key = "NjcQQGpJbHuJQN6JqELf";
        static string _language = "fr_FR";

        public List<string> GetTags(string pWord)
        {
            List<string> res = new List<string>();
            try
            {
                string url = String.Format("{0}?word={1}&language={2}&key={3}&output=xml", _baseUrl, pWord, _language,_key);

                XmlTextReader reader = new XmlTextReader(url);
                string data = Utils.WebReader.GetWebData(url);

                //Extraction
                string synStart = "<synonyms>";
                string synStop = "</synonyms>";
                Regex reg = new Regex(synStart + "(.)*" + synStop);
                Match match = reg.Match(data);
                string bob = match.ToString();
                bob = bob.Substring(synStart.Length, bob.Length - synStart.Length);
                bob = bob.Substring(0, bob.Length - synStop.Length);

                char[] separator = new char[1] { '|' };
                res = bob.Split(separator).ToList();
            }
            catch (Exception)
            {
                //Ne rien faire 
            }
            return res;
        }

        public List<string> GetTags(List<string> pWords)
        {
            List<string> res = new List<string>();
            pWords.ForEach(x => res.AddRange(GetTags(x)));
            return res;
        }
    }
}
