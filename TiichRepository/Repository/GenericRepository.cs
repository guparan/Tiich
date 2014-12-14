using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichDAL;
using TiichRepository.Interface;
using Utils;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TiichRepository.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private string _namespace = "TiichDAL.";

        virtual public void Add(T obj, Utils.ErrorHandler eh, List<object> toAttach = null)
        {
            using (TiichEntities context = new TiichEntities())
            {
                GenericTests(obj, eh);

                if(!eh.hasErrors())
                {
                    DbSet dbSet = context.Set<T>();
                    if(toAttach != null)
                        foreach (var item in toAttach)
                        {
                            var x = item;
                            dbSet.Attach(item.GetType());
                        }
                    
                    dbSet.Add(obj);
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
                    context.Entry(obj).State = EntityState.Modified;
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

        public List<T> GetAll(int taken = -1, List<string> toInclude = null)
        {
            using(TiichEntities context = new TiichEntities())
            {
                IQueryable<T> query = context.Set<T>();

                if(toInclude != null)
                {
                    foreach (string inc in toInclude)
                    {
                        query.Include(inc);
                    }
                }

                if(taken != -1)
                {
                    return query.Take(taken).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
        }
    }
}
