using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichService.Interface;
using System.Net.Mail;
using TiichDAL;
using TiichRepository.Repository;
using Utils;

namespace TiichService.Service
{
    public class UserService : GenericService<User>
    {
        public UserService()
        {
            this._repo = new UserRepository();
        }

        override public void GenericTests(User user, ErrorHandler eh)
        {
            if(user.ID == 0)
            {
                //Valid Email 
                try
                {
                    MailAddress mail = new MailAddress(user.Email);
                }
                catch (Exception)
                {
                    eh.addError("Email non valide");
                }

                if (String.IsNullOrEmpty(user.Password))
                {
                    eh.addError("Mot de passe invalide");
                }   
            }
        }

        public bool IsValid(User user)
        {
            return _repo.IsValid(user);
        }

        public User GetUserByName(string p)
        {
            return ((UserRepository)_repo).GetUserByName(p);
        }

        public User GetUser(int id)
        {
            return ((UserRepository)_repo).GetUser(id);
        }
    }
}
