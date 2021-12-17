using Microsoft.EntityFrameworkCore;
using Recovery_Models.Models;
using System;

namespace Recovery_Backend_Data
{
    public class RecoveryDBContext : DbContext
    {
        public RecoveryDBContext(DbContextOptions<RecoveryDBContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecoveryDBContext).Assembly);
        }

        public DbSet<RegisterModel> usermodel { get; set; }
        public DbSet<PTModel> physical_therapist { get; set; }
        public DbSet<DietModel> diet { get; set; }
        public DbSet<InjuryModel> injury { get; set; }
        //public DbSet<ExerciseModel> exercise { get; set; }

    }
}
