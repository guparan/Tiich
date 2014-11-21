using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichDAL;

namespace TiichRepository.Repository
{
    public class WorkshopRepository : GenericRepository<Workshop>
    {
        public override void GenericTests(Workshop obj, Utils.ErrorHandler eh)
        {
            base.GenericTests(obj, eh);
        }

        public override void Add(Workshop obj, Utils.ErrorHandler eh, List<object> toAttach = null)
        {
            using (TiichEntities context = new TiichEntities())
            {
                GenericTests(obj, eh);

                if (!eh.hasErrors())
                {
                    context.Workshop.Add(obj);

                    foreach (var item in obj.Tag)
                    {
                        if(context.Tag.Where(u => u.label.Equals(item.label)).FirstOrDefault() != null)
                            context.Tag.Attach(item);
                    }

                    context.SaveChanges();
                }
            }
        }

        public List<Workshop> GetLast(int p)
        {
            using (TiichEntities context = new TiichEntities())
            {
                return context.Workshop.OrderByDescending(w => w.CreationDate).Take(p).ToList();
            }
        }
    }
}
