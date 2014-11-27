using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiController.WordExtraction
{
    public class HomeMadeExtraction : IRelevantWords
    {
        public List<string> Extract(string text)
        {
            throw new NotImplementedException();
        }

        private static string RemoveSpecialCaracters(string text)
        {
            throw new NotImplementedException();
        }

        public List<string> Extract(List<string> lString)
        {
            List<string> res = new List<string>();
            foreach (string s in lString)
            {
                //if(!)
            }
            return res;
        }
    }
}
