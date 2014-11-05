using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiController.Tags
{
    interface ITagGetter
    {
        List<string> GetTags(string pWord);
        List<string> GetTags(List<string> pWords);
    }
}
