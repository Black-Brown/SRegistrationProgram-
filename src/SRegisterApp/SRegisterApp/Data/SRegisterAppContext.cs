using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SRegisterApp.Models;

namespace SRegisterApp.Data
{
    public class SRegisterAppContext : DbContext
    {
        public SRegisterAppContext (DbContextOptions<SRegisterAppContext> options)
            : base(options)
        {
        }

        public DbSet<SRegisterApp.Models.Student> Student { get; set; } = default!;
    }
}
