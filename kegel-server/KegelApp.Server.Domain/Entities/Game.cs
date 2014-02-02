using KegelApp.Ipc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KegelApp.Server.Domain.Entities
{
    /// <summary>
    /// Basislogik eines jeden Spiels
    /// </summary>
    [Serializable]
    public class Game
    {
        public virtual int Id { get; protected set; }
        public virtual GameEnum GameId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<Move> Moves { get; set; }
        public virtual User CurrentUser { get { return CurrentMove.Player; } }
        public virtual Move CurrentMove { get { return Moves.OrderByDescending(x => x.Id).FirstOrDefault(); } }
        public virtual IList<User> UsersToPlay { get; set; }
        public virtual bool Finished { get; set; }
    }
}
