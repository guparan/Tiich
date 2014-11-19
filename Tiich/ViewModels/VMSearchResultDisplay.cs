using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiichDAL;

namespace Tiich.ViewModels
{
    public class VMSearchResultDisplay
    {
        public string ResultType { get; set; }
        public List<VMWorkshop> VMWorshops { get; set; }
    }
}