using ApiController.Tags;
using ApiController.WordExtraction;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichDAL;
using TiichRepository.Repository;

namespace TiichService.Service
{
    public class WorkshopService : GenericService<Workshop>
    {
        public override void Add(Workshop obj, Utils.ErrorHandler eh, List<object> toAttach = null)
        {
            IRelevantWords extractor = new HomeMadeExtraction();

            //Prepare the texte 
            char[] separator = new char[1] {' '};
            List<string> textToProcess = obj.Label.Split(separator).ToList();

            if(!String.IsNullOrEmpty(obj.Details))
                textToProcess.AddRange(obj.Details.Split(separator).ToList());
            
            List<string> relevantText = (textToProcess);

            //Tagg the rest 
            ThesaurusAltervista th = new ThesaurusAltervista();
            List<Tag> tagList = new List<Tag>();
            
            //Tag the relevant words

            //tag with thesaurus
            foreach (string tagLabel in th.GetTags(relevantText))
            {
                Tag tag = new Tag();
                tag.label = tagLabel;
                tagList.Add(tag);
            }
            obj.Tag = tagList;

            //Traitement concernant uniquement le workshop
            base.Add(obj, eh, toAttach);
        }

        public WorkshopService()
        {
            _repo = new WorkshopRepository();
        }

        public override void GenericTests(Workshop obj, Utils.ErrorHandler eh)
        {
            //eh.addError("Pas implémenté");
        }

        public List<Workshop> GetLast(int p)
        {
            return ((WorkshopRepository)_repo).GetLast(p);
        }

        public List<Workshop> StraightSearch(string research, ResearchEnums.ResearchOption option)
        {
            return ((WorkshopRepository)_repo).StraightSearch(research, option);
        }

        public List<Workshop> IndirectSearch(string research, ResearchEnums.ResearchOption option)
        {
            return ((WorkshopRepository)_repo).IndirectSearch(research, option);
        }

        public Workshop Find(int id)
        {
            return ((WorkshopRepository)_repo).Find(id);
        }

        public void AddVisitorToWorkshop(string userID, int wsID)
        {
            ((WorkshopRepository)_repo).AddVisitorToWorkshop(int.Parse(userID),wsID);
        }

        public List<Workshop> FavoriteSearch(string email, string research, ResearchEnums.ResearchOption option)
        {
            return ((WorkshopRepository)_repo).FavoriteSearch(email, research, option);
        }
    }
}
