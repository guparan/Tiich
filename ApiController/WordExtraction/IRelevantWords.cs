using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiController.WordExtraction
{
    public interface IRelevantWords
    {
        List<string> Extract(string text);
    }
}
