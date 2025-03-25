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

        public DbSet<SRegisterApp.Models.Students> Students { get; set; } = default!;
        public DbSet<SRegisterApp.Models.Professors> Professors { get; set; } = default!;

        public DbSet<SRegisterApp.Models.Sections> Sections { get; set; } = default!;

        public DbSet<SRegisterApp.Models.StudentSection> StudentSection { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Uno a Muchos entre Professors y Sections
            modelBuilder.Entity<Professors>()
                .HasMany(p => p.Sections)
                .WithOne(s => s.Profesor)
                .HasForeignKey(s => s.ProfesorID)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
