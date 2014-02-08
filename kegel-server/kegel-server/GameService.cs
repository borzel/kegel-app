using KegelApp.Ipc;
using KegelApp.Ipc.Data;
using KegelApp.Server.Database;
using KegelApp.Server.Domain;
using KegelApp.Server.Domain.Entities;
using KegelApp.Server.Domain.Logic;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KegelApp.Server
{
    public static class GameService
    {
        static ISession session = KegelSessionFactory.Instance.GetSession();

        public static void Save()
        {
            session.Flush();
        }

        #region Users
        public static List<User> GetUsers()
        {
            List<User> users = session.Query<User>().ToList();
            return users;
        }

        public static List<User> GetUsers(Expression<Func<User, bool>> f)
        {
            List<User> users = session.Query<User>().Where(f).ToList();
            return users;
        }

        public static bool ExistsUser(Expression<Func<User, bool>> f)
        {
            return session.Query<User>().Where(f).Any();
        }

        public static int GetUserCount()
        {
            return session.Query<User>().Count();
        }

        public static void AddUser(User newUser)
        {
            session.SaveOrUpdate(newUser);
            session.Flush();
        }

        public static void RemoveUser(User oldUser)
        {
            session.Delete(oldUser);
            session.Flush();
        }

        public static void EditUser(User changedUser)
        {
            User u = GetUsers(user => user.Id == changedUser.Id).FirstOrDefault();
            if (u != null)
            {
                u.Name = changedUser.Name;
                u.Sex = changedUser.Sex;

                session.SaveOrUpdate(u);
                session.Flush();
            }
        }

        public static string CurrentUserNameOfCurrentGame()
        {
            return session.Query<Game>().OrderByDescending(game => game.Id).FirstOrDefault()
                .Moves.OrderByDescending(move => move.Id).Select(move => move.Player.Name).FirstOrDefault();
        }
        #endregion

        #region Spiele

        public static void StartGame(string gameToStart)
        {
            GameBase spiel = GameBase.CreateGame((GameEnum)Enum.Parse(typeof(GameEnum), gameToStart));
                        
            if (GameService.GetUserCount() > 0)
            {
                spiel.Start(GameService.GetUsers());
                session.SaveOrUpdate(spiel.GameData);
                session.Flush();
            }
        }

        public static GameBase FindGameById(int id)
        {
            Game data = session.Query<Game>().FirstOrDefault(g => g.Id == id);
            GameBase game = GameBase.CreateGame(data.GameId);
            game.GameData = data;
            return game;           

        }

        public static List<ResultData> GetGameResult(GameBase game)
        {
            return ResultCalculator.GetResult(game.GameData);
        }

        public static List<Game> GetAllGames()
        {
            return session.Query<Game>().ToList();
        }

        public static int GetGameCount()
        {
            return session.Query<Game>().Count();
        }

        public static GameBase CurrentGame()
        {
            Game data = session.Query<Game>().OrderByDescending(x => x.Id).FirstOrDefault();
            if (data == null)
                return null;

            GameBase game = GameBase.CreateGame(data.GameId);
            game.GameData = data;
            return game;
        }

        public static bool CurrentGameIsFinished()
        {
            return session.Query<Game>().OrderByDescending(x => x.Id).Select(x => x.Finished).FirstOrDefault();
        }

        public static string CurrentGameName()
        {
            return session.Query<Game>().OrderByDescending(x => x.Id).Select(x => x.Name).FirstOrDefault();
        }
        #endregion

        #region Wurf
        public static void AddShot(string erg)
        {
            Shot wurf = new Shot();

            if (erg == "R")
            {
                wurf.Value = 0;
                wurf.NullShot = true;
            }
            else if (erg == "U")
            {
                wurf.Value = 0;
                wurf.Fault = true;
            }
            else if (erg != "")
            {
                wurf.Value = int.Parse(erg);
            }

            GameBase g = CurrentGame();
            g.AddShot(wurf);

            session.SaveOrUpdate(g.GameData);
            session.Flush();
        }
        #endregion
    }
}
