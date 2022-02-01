using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP_App.Models
{
    public sealed class SessionContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }

        public SessionContext(DbContextOptions<SessionContext> options)
          : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
