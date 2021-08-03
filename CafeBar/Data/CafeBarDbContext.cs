using Cafe.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeBar.Data
{
    class CafeBarDbContext:BaseDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"FileName=C:\Users\neYa\DBases\cafe.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
