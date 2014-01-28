/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 22:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;

namespace KegelApp.Server.Database.Entities
{
	[Serializable]
	public class Move
	{		
		public virtual int Id {get; protected set;}
        public virtual User Player { get; set; }
        public virtual int Score { get; set; }
        public virtual IList<Shot> Shots { get; protected set; }

	    public virtual Game InGame { get; set; }

        public Move()
        {
            Shots = new List<Shot>();
        }

        public virtual void AddShot(Shot shot)
        {
            shot.InMove = this;
            Shots.Add(shot);
        }
	}
}
