using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichDAL;

namespace Utils
{
    public class WorkshopHelper
    {
        public static void Diff(List<Workshop> source, List<Workshop> toRemove)
        {
            foreach (Workshop ws in toRemove)
            {
                Workshop old = source.Where(w => w.ID == ws.ID).FirstOrDefault();
                if (old != null)
                {
                    source.Remove(old);
                }
            }
        }
    }
}
