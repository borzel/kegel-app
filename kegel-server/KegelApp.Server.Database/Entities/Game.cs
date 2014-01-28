/*
 * Created by SharpDevelop.
 * User: alex
 * Date: 23.01.2014
 * Time: 22:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;

namespace KegelApp.Server.Database.Entities
{
	/// <summary>
	/// Description of Spiel.
	/// </summary>
	[Serializable]
	public class Game
	{		
		public virtual int Id {get; protected set;}
		public virtual string Name {get; set;}
        public virtual IList<Move> Moves { get; protected set; }

        public Game()
        {
            Moves = new List<Move>();
        }

        public virtual void AddMove(Move move)
        {
            move.InGame = this;
            Moves.Add(move);
        }
	}
}
