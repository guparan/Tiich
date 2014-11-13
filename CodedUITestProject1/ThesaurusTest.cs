using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiController.Tags;

namespace CodedUITestProject1
{
    class ThesaurusTest
    {
        public static void Main()
        {
            ThesaurusAltervista th = new ThesaurusAltervista();
            th.GetTags("guitare");
            th.GetTags("banjo");
            th.GetTags("piano"); 
        
        }
    }
}
