using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.Messaging;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace kegel_server
{
    public class UserRegisterService : Service, IGet<UserRegisterRequest>
    {
        public object Get(UserRegisterRequest request)
        {
            UserRegisterResponse response = new UserRegisterResponse();

            if (Server.Data.ListOfUser.Any(x => x.Name == request.Name || x.Nickname == request.NickName))
            {
                response.Added = false;
                response.ErrorMessage = "Spieler exisitert bereits";
            }
            else
            {
                User u = new User();
                u.Name = request.Name;
                u.Nickname = request.NickName;
                Server.Data.ListOfUser.Add(u);

                response.Added = true;
            }
            
            return response;
        }
    }
}
