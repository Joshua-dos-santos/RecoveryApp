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

        public DbSet<UserModel> usermodel { get; set; }
        public DbSet<PTModel> ptmodel { get; set; }
        public DbSet<DietModel> diet { get; set; }
    }
}
