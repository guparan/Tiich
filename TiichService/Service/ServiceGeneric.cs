using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichService.Interface;
using Utils;

namespace TiichService.Service
{
    public class ServiceGeneric<T> : IService<T>
    {
        public void Add(T obj, ErrorHandler eh)
        {
            GenericTests(obj, eh);
        }

        public void Edit(T obj, ErrorHandler eh)
        {
            GenericTests(obj, eh);
        }

        public void Delete(int id, ErrorHandler eh)
        {
            throw new NotImplementedException();
        }

        virtual public void GenericTests(T obj, ErrorHandler eh)
        {
            // rien de generic
        }
    }
}
