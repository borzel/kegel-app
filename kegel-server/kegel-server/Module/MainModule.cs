using System;
using Nancy;

namespace kegel_server.Module
{
	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = _ =>
			{
				return View["index.html", Server.Data.ListOfUser.Count];
			};
		}
	}
}
