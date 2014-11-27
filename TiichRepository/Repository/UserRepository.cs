using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichDAL;
using TiichRepository.Interface;
using System.Security.Cryptography;
using Utils;

namespace TiichRepository.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public override void Add(User user, Utils.ErrorHandler eh, List<object> toAttach = null)
        {
            user.Password = Crypter.EncryptPassword(user.Password);
            base.Add(user, eh);
        }

        public override void GenericTests(User user, ErrorHandler eh)
        {
            using (TiichEntities context = new TiichEntities())
            {
                //Si création
                if(user.ID == null )
                {
                    //Email unique
                    User old = context.User.Where(u => u.Email.Equals(user.Email)).FirstOrDefault();
                    if (old != null)
                    {
                        eh.addError("Email déjà présent dans la base de donnée");
                    }
                }
                
            }
        }

        public User GetUserByName(string p)
        {
            using (TiichEntities context = new TiichEntities())
            {
                User user = context.User.Include("Workshop").Include("ParticipateAt").Where(u => u.Email.Equals(p)).FirstOrDefault();
                return user;
            }
        }
    }
}
