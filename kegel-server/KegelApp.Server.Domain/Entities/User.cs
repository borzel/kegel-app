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

        public virtual IList<Move> Moves { get; protected set; }
        public virtual IList<Game> GamesToPlay { get; protected set; } 

        public User()
        {
            Moves = new List<Move>();
            GamesToPlay = new List<Game>();
        }
	}
}
