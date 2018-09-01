using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Model;

namespace SportStore.BusinessLogic
{
    public interface IAuthBusinessLogic
    {
        Session CurrentSession { get; }

        void SignUp(User user);
        void Login(string authToken);
        void Login(string userName, string password);
        void Logout();
    }
}
