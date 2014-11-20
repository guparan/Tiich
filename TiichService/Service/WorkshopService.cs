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
        public WorkshopService()
        {
            _repo = new WorkshopRepository();
        }

        public override void GenericTests(Workshop obj, Utils.ErrorHandler eh)
        {
            eh.addError("Pas implémenté");
        }

        public List<Workshop> GetLast(int p)
        {
            return ((WorkshopRepository)_repo).GetLast(p);
        }
    }
}
