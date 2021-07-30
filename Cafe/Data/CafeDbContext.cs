using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Models;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Cafe.Data
{
    public class CafeDbContext:BaseDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DataBases\restoran.mdf;Integrated Security=True;Connect Timeout=30");
            optionsBuilder.UseSqlite(@"FileName=C:\Users\neYa\DBases\cafe.db");
            optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
