using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP_App.Models
{
    public static class UsersData
    {
        private static UserContext db;

        public static void SetDb(UserContext context)
        {
            db = context;
        }
        
        public static List<User> GetUsers()
        {
            var list = db.Users.ToListAsync().Result.ToArray();///  dg
            return db.Users.ToListAsync().Result;
        }

        public static void AddUser(User user)
        {
            db.Users.Add(user);
            db.SaveChangesAsync();
        }

        public static bool IsPasswordCorrect(User user)
        {
            var dbPassword = UsersData.GetUsers()
                .Where((x) => {
                    return string.Compare(x.Email, user.Email) == 0
                    && x.Password == user.Password;
                })
                .FirstOrDefault()
                .Password;

            return string.Compare(user.Password, dbPassword) == 0;
        }

        public static bool IsUserInBDbyEmail(string e)
        {
            var isEmailContains = UsersData.GetUsers()
                .Select(x => x.Email)
                .Contains(e);

            return isEmailContains;
        }

        public static User GetUserById(int id)
        {
            return db.Users.Find(id);
        }

        public static User GetUserByEmail(string email)
        {
            return db.Users.ToListAsync().Result
                 .Where(x => string.Compare(x.Email, email) == 0)
                 .FirstOrDefault();
        }
    }
}
