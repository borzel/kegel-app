using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kegel_server.Dto
{
	[Serializable]
	public class UserData
	{
		public Guid Id {get; set;}
		public string Name { get; set; }
	}
}
