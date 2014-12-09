using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichDAL;
using TiichRepository.Repository;

namespace TiichService.Service
{
    public class TagService : GenericService<Tag>
    {
        public TagService()
        {
            this._repo = new TagRepository();
        }

        public int GetWorkshopCount(Tag t)
        {
            return ((TagRepository)this._repo).GetWorkshopCount(t);
        }

        public List<Tag> GetMostUsed(int toTake)
        {
            return ((TagRepository)_repo).GetMostUsed(toTake);
        }

        public List<Tag>[] GetCommonWords(string tag)
        {
            return ((TagRepository)_repo).GetCommonWords(tag);
        }

        public void SetTags(List<int> tags, bool activate)
        {
            ((TagRepository)_repo).SetTags(tags,activate);
        }

        public List<Workshop> GetWorkshopsFromTags(List<int> directIDS)
        {
            return ((TagRepository)_repo).GetWorkshopsFromTags(directIDS);
        }
    }
}
