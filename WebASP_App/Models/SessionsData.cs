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

        public static void DeleteSession(User user, string sessionValue)
        {
            db.Sessions.Remove(new Session { UserId = user.Id, SessionId = sessionValue });
            db.SaveChangesAsync();
        }

        public static User GetUserBySession(string session)
        {
            if (db == null)
                return null;
           
            int userId = db.Sessions.ToListAsync().Result
                .Where(x => string.Compare(x.SessionId, session) == 0)
                .Select(x=>x.UserId)
                .FirstOrDefault();
            return UsersData.GetUserById(userId);
        }

        public static List<Session> GetAllCurrentSessions()
        {
            return db.Sessions.ToListAsync().Result;
        }

    }
}
