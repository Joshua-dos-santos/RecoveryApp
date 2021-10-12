using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recovery_BackEnd.Models;

namespace Recovery_BackEnd.Data
{
    public class Recovery_BackEndContext : DbContext
    {
        public Recovery_BackEndContext (DbContextOptions<Recovery_BackEndContext> options)
            : base(options)
        {
        }

        public DbSet<Recovery_BackEnd.Models.UserModel> usermodel { get; set; }
    }
}
