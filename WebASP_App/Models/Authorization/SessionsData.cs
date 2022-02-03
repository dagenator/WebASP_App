using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP_App.Models
{
    public static class SessionsData
    {
        private static SessionContext db;

        public static void SetDb(SessionContext context)
        {
            db = context;
        }

        public async static Task AddNewSession(User user , string sessionValue)
        {
            var session = new Session { UserId = user.Id, SessionId = sessionValue };
            await db.Sessions.AddAsync(session);
            await db.SaveChangesAsync();
            var sess = await db.Sessions.ToListAsync();
        }

        public static void DeleteSession(string sessionValue)
        {
            var deleted = db.Sessions.Find(sessionValue);
            db.Sessions.Remove(deleted);
            db.SaveChanges();
        }

        public static int GetUserIDBySession(string session)
        {
            return db.Sessions.ToList()
                .Where(x => string.Compare(x.SessionId, session) == 0)
                .Select(x => x.UserId)
                .FirstOrDefault();
        }

        public static User GetUserBySession(string session)
        {
            if (db == null)
                return null;
           
            return UsersData.GetUserById(GetUserIDBySession(session));
        }

        public static List<Session> GetAllCurrentSessions()
        {
            return db.Sessions.ToListAsync().Result;
        }

    }
}
