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
                    //Tags processing 
                    List<Tag> tags = new List<Tag>();
                    foreach (var item in obj.Tag)
                    {
                        item.label = item.label.Trim();
                        Tag dbTag = context.Tag.Where(u => u.label.Equals(item.label)).FirstOrDefault();
                        
                        if (dbTag != null)
                        {
                            tags.Add(dbTag);
                            context.Entry(dbTag).State = EntityState.Modified;
                            context.Tag.Attach(dbTag);
                        }
                        else
                        {
                            tags.Add(item);
                        }
                    }

                    obj.Tag = tags;

                    //Saving
                    context.User.Attach(obj.User);
                    context.Workshop.Add(obj);
                    context.SaveChanges();
                }
            }
        }

        public List<Workshop> GetLast(int p)
        {
            using (TiichEntities context = new TiichEntities())
            {
                return context.Workshop.Include("User").OrderByDescending(w => w.CreationDate).Take(p).ToList();
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
                        workshops.AddRange(context.Workshop.Include("User").Where(w =>
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
                            workshops.AddRange(context.Workshop.Include("User").Where(w =>
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
                        workshops.AddRange(context.Workshop.Include("User").Where(w =>
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
                            workshops.AddRange(context.Workshop.Include("User").Where(w =>
                                w.Tag.Where(t => t.label.Trim().Equals(term)).FirstOrDefault() != null));
                        }

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
                return context.Workshop.Include("User").Include("Participants").Where(w => w.ID == id).FirstOrDefault();
            }
        }

        public void AddVisitorToWorkshop(int userID, int wsID)
        {
            using (TiichEntities context = new TiichEntities())
            {
                User user = context.User.Where(u => u.ID == userID).FirstOrDefault();
                Workshop workshop = context.Workshop.Where(u => u.ID == wsID).FirstOrDefault();

                if(user.SeenWorkshop.Where(w => w.ID == wsID).FirstOrDefault() == null)
                {
                    user.SeenWorkshop.Add(workshop);
                    context.SaveChanges();
                }
            }
        }

        public List<Workshop> FavoriteSearch(string email,string research, Enums.ResearchEnums.ResearchOption option)
        {
            using(TiichEntities context = new TiichEntities())
            {
                List<Workshop> workshops = new List<Workshop>();
                List<Workshop> seenWS = context.User.Include(u => u.SeenWorkshop).Include("SeenWorkshop.User").Where(u => u.Email.Equals(email)).FirstOrDefault().SeenWorkshop.ToList();
                List<Tag> favoriteTags = new List<Tag>();
                foreach (Workshop ws in seenWS)
                {
                    favoriteTags.AddRange(ws.Tag);
                }

                foreach (Tag tag in favoriteTags)
	            {
                    List<Tag> list = context.Tag.Include("Workshop.User").Where(t => t.ID == tag.ID).ToList();
                    list.ForEach(t => workshops.AddRange(t.Workshop));
                    //workshops.AddRange(tag.Workshop);
	            }

                workshops = workshops.Distinct().ToList();

                return workshops;
            }
        }

        public void AddParticpant(int userID, int wsID)
        {
            using(TiichEntities context = new TiichEntities())
            {
                User user = context.User.Find(userID);
                Workshop ws = context.Workshop.Find(wsID);

                user.ParticipateAt.Add(ws);
                context.SaveChanges();
            }
        }

        public void RemoveParticpant(int userID, int wsID)
        {
            using (TiichEntities context = new TiichEntities())
            {
                User user = context.User.Find(userID);
                Workshop ws = context.Workshop.Find(wsID);

                user.ParticipateAt.Remove(ws);
                context.SaveChanges();
            }
        }
    }
}
