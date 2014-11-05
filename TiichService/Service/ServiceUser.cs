using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiichService.Interface;
using System.Net.Mail;
using TiichDAL;

namespace TiichService.Service
{
    public class ServiceUser : ServiceGeneric<User>
    {
        override public void GenericTests(User obj, Utils.ErrorHandler eh)
        {
            //Valid Email 
            try
            {
                MailAddress mail = new MailAddress(obj.Email);
            }
            catch (Exception)
            {
                eh.addError("Email non valide");
            }
        }
    }
}
