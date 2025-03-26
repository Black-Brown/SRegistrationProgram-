using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SRegisterApp.Domain.entities;

namespace SRegisterApp.Persistence
{
    public class SRegisterAppContext : DbContext
    {
        public SRegisterAppContext (DbContextOptions<SRegisterAppContext> options)
            : base(options)
        {
        }

        public DbSet<Students> Students { get; set; } = default!;
        public DbSet<Professors> Professors { get; set; } = default!;

    }
}
