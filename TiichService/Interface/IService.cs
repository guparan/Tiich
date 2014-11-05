using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace TiichService.Interface
{
    public interface IService<T>
    {
        void Add(T obj, ErrorHandler eh);
        void Edit(T obj, ErrorHandler eh);
        void Delete(int id, ErrorHandler eh);
        void GenericTests(T obj, ErrorHandler eh);
    }
}
