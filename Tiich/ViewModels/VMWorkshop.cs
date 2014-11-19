using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiichDAL;

namespace Tiich.ViewModels
{
    public class VMWorkshop
    {
        public string Category { get; set; }
        public List<Workshop> Workshops {get;set;}
    }
}