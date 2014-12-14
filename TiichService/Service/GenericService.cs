using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichRepository.Interface;
using TiichRepository.Repository;
using TiichService.Interface;
using Utils;

namespace TiichService.Service
{
    public class GenericService<T> : IService<T> where T :class
    {
        protected GenericRepository<T> _repo;

        virtual public void Add(T obj, ErrorHandler eh, List<object> toAttach = null)
        {
            GenericTests(obj, eh);

            if (!eh.hasErrors())
                _repo.Add(obj,eh, toAttach);
        }

        public void Edit(T obj, ErrorHandler eh)
        {
            GenericTests(obj, eh);

            if (!eh.hasErrors())
                _repo.Edit(obj, eh);
        }

        public void Delete(int id, ErrorHandler eh)
        {
            throw new NotImplementedException();
        }

        virtual public void GenericTests(T obj, ErrorHandler eh)
        {
            // rien de generic
        }


        public List<T>  GetAll(int taken = -1, List<string> toInclude = null)
        {
            return _repo.GetAll(taken, toInclude);
        }
    }
}
