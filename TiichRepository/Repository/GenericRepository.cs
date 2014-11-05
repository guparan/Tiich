using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichRepository.Interface;

namespace TiichRepository.Repository
{
    class GenericRepository : IRepository
    {
        public void Add(object obj, Utils.ErrorHandler eh)
        {
            throw new NotImplementedException();
        }

        public void Edit(object obj, Utils.ErrorHandler eh)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, Utils.ErrorHandler eh)
        {
            throw new NotImplementedException();
        }

        public void GenericTests(object obj, Utils.ErrorHandler eh)
        {
            throw new NotImplementedException();
        }
    }
}
