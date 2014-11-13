using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiController.Tags
{
    class GoogleSuggest : ITagGetter
    {
        private string _basURL = "http://www.google.com/complete/search?output=toolbar&oe=utf8&hl=fr&q=";
        public List<string> GetTags(string pWord)
        {
            throw new NotImplementedException();
        }

        public List<string> GetTags(List<string> pWords)
        {
            throw new NotImplementedException();
        }
    }
}
