using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace TiichRepository.Interface
{
    public interface IRepository
    {
        void Add(object obj, ErrorHandler eh);
        void Edit(object obj, ErrorHandler eh);
        void Delete(int id, ErrorHandler eh);
        void GenericTests(object obj, ErrorHandler eh);
    }
}
