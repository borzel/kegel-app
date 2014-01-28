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
using System.Linq;

namespace KegelApp.Server.Database.Entities
{
    /// <summary>
    /// Description of Spiel.
    /// </summary>
    [Serializable]
    public class Game
    {
        public virtual int Id { get; protected set; }
        public virtual GameEnum GameId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<Move> Moves { get; protected set; }
        public virtual User CurrentUser { get { return CurrentMove.Player; } }
        public virtual Move CurrentMove { get { return Moves.OrderByDescending(x => x.Id).FirstOrDefault(); } }
        public virtual IList<User> UsersToPlay { get; protected set; }
        public virtual bool Finished { get; set; }

        public Game()
        {
            Moves = new List<Move>();
            UsersToPlay = new List<User>();
        }

        public virtual void AddMove(Move move)
        {
            move.InGame = this;
            Moves.Add(move);
        }

        public virtual void AddUsers(IList<User> users)
        {
            foreach (User user in users)
            {
                user.GamesToPlay.Add(this);
                UsersToPlay.Add(user);
            }
        }

        public virtual User GetNextUser()
        {
            if (!UsersToPlay.Any())
                return null;

            User nextUser = UsersToPlay.First();
            UsersToPlay.Remove(nextUser);
            return nextUser;
        }
    }
}
