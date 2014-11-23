using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TiichDAL;
using TiichDAL.ProcedureConverters;

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

                    List<Tag> dbTags = new List<Tag>();

                    foreach (var item in obj.Tag)
                    {
                        item.label = item.label.Trim();
                        Tag dbTag = context.Tag.Where(u => u.label.Equals(item.label)).FirstOrDefault();
                        if (dbTag != null)
                        {
                            dbTags.Add(dbTag);
                        }
                    }

                    foreach (Tag tag in dbTags)
                    {
                        Tag olgTag = obj.Tag.Where(t => t.label.Equals(tag.label.Trim())).FirstOrDefault();
                        obj.Tag.Remove(olgTag);
                    }

                    context.Workshop.Add(obj);
                    context.SaveChanges();

                    obj.Tag = dbTags;
                    context.Entry(obj).State = EntityState.Modified;
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

        public List<Workshop> StraightSearch(string research, Enums.ResearchEnums.ResearchOption option)
        {
            using (TiichEntities context = new TiichEntities())
            {
                List<Workshop> workshops = new List<Workshop>();
                List<string> terms = research.Split(new char[1] { ' ' }).ToList();

                switch (option)
                {
                    case Enums.ResearchEnums.ResearchOption.And:
                        workshops.AddRange(context.Workshop.Where(w =>
                                w.Label.Contains(terms.FirstOrDefault()) ||
                                w.Details.Contains(terms.FirstOrDefault())
                                ));
                        terms.Skip(1);
                        while (terms.Count > 0)
                        {
                            workshops = workshops.Where(w =>
                                w.Label.Contains(terms.FirstOrDefault()) ||
                                w.Details.Contains(terms.FirstOrDefault())
                                ).ToList();
                            terms.Skip(1);
                        }
                        break;
                    case Enums.ResearchEnums.ResearchOption.Or:
                        foreach (String term in terms)
                        {
                            workshops.AddRange(context.Workshop.Where(w =>
                                w.Label.Contains(term) ||
                                w.Details.Contains(term)
                                ));
                        }

                        break;
                    default:
                        break;
                }
                return workshops;
            }
        }

        public List<Workshop> IndirectSearch(string research, Enums.ResearchEnums.ResearchOption option)
        {
            using (TiichEntities context = new TiichEntities())
            {
                List<Workshop> workshops = new List<Workshop>();
                List<string> terms = research.Split(new char[1] { ' ' }).ToList();

                switch (option)
                {
                    case Enums.ResearchEnums.ResearchOption.And:
                        workshops.AddRange(context.Workshop.Where(w =>
                                w.Label.Contains(terms.FirstOrDefault()) ||
                                w.Details.Contains(terms.FirstOrDefault())
                                ));
                        terms.Skip(1);
                        while (terms.Count > 0)
                        {
                            workshops = workshops.Where(w =>
                                w.Label.Contains(terms.FirstOrDefault()) ||
                                w.Details.Contains(terms.FirstOrDefault())
                                ).ToList();
                            terms.Skip(1);
                        }
                        break;
                    case Enums.ResearchEnums.ResearchOption.Or:
                        //Recherche sur les tags de l'annonce 
                        List<Tag> tags = new List<Tag>();

                        foreach (string term in terms)
                        {
                            tags.AddRange(context.Tag.Where(t => t.label.Equals(term)));
                        }

                        tags.ForEach(t => workshops.AddRange(t.Workshop));
                        break;
                    default:
                        break;
                }
                return workshops;
            }
        }

        public Workshop Find(int id)
        {
            using(TiichEntities context = new TiichEntities())
            {
                return context.Workshop.Where(w => w.ID == id).FirstOrDefault();
            }
        }
    }
}
