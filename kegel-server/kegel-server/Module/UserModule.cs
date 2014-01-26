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
				return View["User.html", Server.Data.ListOfUser.OrderBy(x => x.Name)];
			};
			
			Post["/delete"] = _ =>
			{
				Guid userId = Request.Form["id"];
				
				UserData userToDelete = Server.Data.ListOfUser.Where(x => x.Id == userId).FirstOrDefault();
				if (userToDelete != null)
				{
					Server.Data.ListOfUser.Remove(userToDelete);
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
				
				if (Server.Data.ListOfUser.Any(x => x.Name == newUsername))
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
					Server.Data.ListOfUser.Add(u);

					return Response.AsRedirect("/user");
				}
			};
		}
	}
}
