using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichDAL;

namespace TiichRepository.Repository
{
    public class TagRepository : GenericRepository<Tag>
    {
        public int GetWorkshopCount(Tag t)
        {
            using(TiichEntities context = new TiichEntities())
            {
                return context.Tag.Include("Workshop").Single(x => t.ID == x.ID).Workshop.Count;
            }
        }

        public List<Tag> GetMostUsed(int toTake)
        {
            using(TiichEntities context = new TiichEntities())
            {
                return context.Tag.Include("Workshop").OrderByDescending(t => t.Workshop.Count).Take(toTake).ToList();
            }
        }
    }
}
