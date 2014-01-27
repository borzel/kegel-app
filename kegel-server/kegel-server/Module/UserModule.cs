using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using kegel_server.Dto;
using Nancy;

namespace kegel_server.Module
{
	public class UserModule : NancyModule
	{
		public UserModule() : base("/user")
		{
			Get["/"] = _ => 
			{
				return View["User.html", Server.Instance.GetUsers().OrderBy(x=>x.Name)];
			};
			
			Post["/delete"] = _ =>
			{
				Guid userId = Request.Form["id"];
				
				UserData userToDelete = Server.Instance.GetUsers().Where(x => x.Id == userId).FirstOrDefault();
				if (userToDelete != null)
				{
					Server.Instance.RemoveUser(userToDelete);
					return Response.AsRedirect("/user");
				}
				else
				{
					return HttpStatusCode.InternalServerError;
				}
			};
			
			Post["/register"] = _ =>
			{
				string newUsername = Request.Form["username"];                
                EnumSex sex;

                if (!Enum.TryParse<EnumSex>(Request.Form["sex"], out sex))
                    sex = EnumSex.Mann;
				
				if (Server.Instance.GetUsers().Any(x => x.Name == newUsername))
				{
					return "Spieler exisitert bereits";
				}
				else
				{
                    UserData u = new UserData
                    {
                        Id = Guid.NewGuid(),
                        Name = newUsername,
                        Sex = sex
                    };
					Server.Instance.AddUser(u);

					return Response.AsRedirect("/user");
				}
			};
		}
	}
}
