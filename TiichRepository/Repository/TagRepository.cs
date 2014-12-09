using ApiController.WordExtraction;
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

        public List<Tag>[] GetCommonWords(string tag)
        {
            using (TiichEntities context = new TiichEntities())
            {
                List<Tag>[] res = new List<Tag>[2];
                List<Tag> commonTags = new List<Tag>();
                List<Workshop> ws = context.Tag.Where(t => t.label.Equals(tag.Trim())).FirstOrDefault().Workshop.ToList();

                HomeMadeExtraction extractor = new HomeMadeExtraction();

                int iteration = 0;
                foreach (Workshop w in ws)
                {
                    if(iteration == 0)
                        commonTags.AddRange(w.Tag);
                    else
                    {
                        List<Tag> nList = new List<Tag>();

                        foreach (Tag item in commonTags)
	                    {
		                    if(w.Tag.Contains(item))
                            {
                                nList.Add(item);
                            }
	                    }
                        commonTags = nList;
                    }

                    iteration++;
                }
                
                commonTags = commonTags.Distinct().ToList();

                List<Tag> initTag = new List<Tag>();
                iteration = 0;

                foreach (Tag t in commonTags)
                {
                    bool present = true;
                    foreach(Workshop w in ws)
                    {
                        if(present)
                        {

                            //Si pas dans label
                            if (!w.Label.Contains(t.label.Trim()))
                            {
                                present = false;
                            }
                            else if (w.Details != null)
                            {
                                if (w.Details.Contains(t.label.Trim()))
                                {
                                    present = true;
                                }
                            }
                        }
                    }
                    if (present)
                        initTag.Add(t);

                    res[0] = initTag;
                    res[1] = commonTags;
                }


                return res;
            }
        }

        public void SetTags(List<int> tags, bool activate)
        {
            using(TiichEntities context = new TiichEntities())
            {
                int act = activate ? 1 : 0;

                foreach (var item in tags)
                {
                    Tag tag = context.Tag.Find(item);
                    tag.activate = act;
                    context.SaveChanges();
                }
            }
        }

        public List<Workshop> GetWorkshopsFromTags(List<int> directIDS)
        {
           using(TiichEntities context = new TiichEntities())
           {
               List<Workshop> ws = new List<Workshop>();
               foreach (int id in directIDS)
               {
                   Tag tag = context.Tag.Include("Workshop").Include("Workshop.Tag").Where(t => t.ID == id).FirstOrDefault();
                   ws.AddRange(tag.Workshop.ToList());
               }
               return ws;
           }
        }
    }
}
