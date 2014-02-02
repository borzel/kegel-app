using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using KegelApp.Server.Database;
using KegelApp.Server.Domain;
using KegelApp.Server.Domain.Entities;
using Nancy;
using KegelApp.Server;
using KegelApp.Ipc;

namespace kegel_server.Module
{
    public class UserModule : NancyModule
    {
		const string MODULE_BASEURL = "/user";

        public UserModule() : base(MODULE_BASEURL)
        {
            Get ["/"] = _ => 
            {
                return View ["User.html", GameService.GetUsers().OrderBy(x => x.Name)];
            };

            //Post ["/edit"] = _ =>
            //{
            //    Guid userId = Request.Form ["id"];
            //    string newName = Request.Form ["username"];
            //    string newSex = Request.Form ["sex"];

            //    if (newName=="")
            //    {
            //        return "Bitte Namen ausf&uuml;llen!!";
            //    }

            //    UserData user = Server.Instance.GetUsers().FirstOrDefault(u => u.Id == userId);
            //    if (user != null)
            //    {
            //        user.Name = newName;

            //        EnumSex newSexEnum;
            //        if (!Enum.TryParse<EnumSex>(newSex, out newSexEnum))
            //        {
            //            user.Sex = newSexEnum;
            //        }

            //        // Update :-)
            //        Server.Instance.RemoveUser(user);
            //        Server.Instance.AddUser(user);

            //        return Response.AsRedirect(MODULE_BASEURL);
            //    }

            //    return HttpStatusCode.InternalServerError;
            //};
			
            //Post ["/delete"] = _ =>
            //{
            //    Guid userId = Request.Form ["id"];
				
            //    UserData userToDelete = Server.Instance.GetUsers().Where(x => x.Id == userId).FirstOrDefault();
            //    if (userToDelete != null)
            //    {
            //        Server.Instance.RemoveUser(userToDelete);
            //        return Response.AsRedirect(MODULE_BASEURL);
            //    } else
            //    {
            //        return HttpStatusCode.InternalServerError;
            //    }
            //};

            Post["/register"] = _ =>
            {
                string newUsername = Request.Form["username"];
                SexEnum sex;

                if (!Enum.TryParse<SexEnum>(Request.Form["sex"], out sex))
                    sex = SexEnum.Man;

                if (GameService.ExistsUser(x => x.Name == newUsername))
                {
                    return "Spieler exisitert bereits";
                }
                else
                {
                    User u = new User
                    {
                        Name = newUsername,
                        Sex = sex
                    };
                    GameService.AddUser(u);

                    return Response.AsRedirect(MODULE_BASEURL);
                }
            };
        }
    }
}
