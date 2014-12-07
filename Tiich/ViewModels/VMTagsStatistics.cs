using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiichDAL;
using TiichService.Service;

namespace Tiich.ViewModels
{
    public class VMTagsStatistics
    {
        public List<TagCount> Tags;

        public void CreateTagList(List<Tag> tags)
        {
            TagService service = new TagService();
            Tags = new List<TagCount>();
            foreach (Tag t in tags)
            {
                TagCount add = new TagCount();
                add.Tag = t;

                add.Count =  service.GetWorkshopCount(t);

                if(add.Count != 0)
                {
                    Tags.Add(add);
                }
            }

            Tags = Tags.OrderByDescending(t => t.Count).ToList();
        }
    }

    public class TagCount
    {
        public Tag Tag;
        public int Count;
    }
}