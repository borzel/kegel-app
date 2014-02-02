using KegelApp.Ipc;
using System;
using System.Collections.Generic;

namespace KegelApp.Server.Domain.Entities
{
	[Serializable]
	public class User
	{
		public virtual int Id {get; protected set;}
        public virtual string Name { get; set; }
        public virtual SexEnum Sex { get; set; }
	}
}
