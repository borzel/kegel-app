using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kegel_server.Dto;
using Nancy;

namespace kegel_server.Module
{
	public class ResultModule: NancyModule
	{
		public ResultModule() : base("/result")
		{
			
			Get["/"] = _ =>
			{
				return View["result", Server.Data.ListOfSpiele];
			};
		}
	}
}
