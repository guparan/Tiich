using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichDAL;
using TiichRepository.Interface;
using Utils;

namespace TiichRepository.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        virtual public void Add(T obj, Utils.ErrorHandler eh)
        {
            using (TiichEntities context = new TiichEntities())
            {
                GenericTests(obj, eh);

                if(!eh.hasErrors())
                {
                    context.Set<T>().Add(obj);
                    context.SaveChanges();
                }
            }
        }

        public void Edit(T obj, Utils.ErrorHandler eh)
        {
            using (TiichEntities context = new TiichEntities())
            {
                GenericTests(obj, eh);

                if (!eh.hasErrors())
                {
                    context.Entry(obj);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id, Utils.ErrorHandler eh)
        {
            throw new NotImplementedException();
        }

        virtual public void GenericTests(T obj, ErrorHandler eh)
        {
            //Empty
        }

        public bool IsValid(User user)
        {
            using (TiichEntities context = new TiichEntities())
            {
                string password = Utils.Crypter.EncryptPassword(user.Password);
                User dbUser = context.User.Where(u => u.Email.Equals(user.Email) && u.Password.Equals(password)).FirstOrDefault();
                return dbUser != null;
            }
        }
    }
}
