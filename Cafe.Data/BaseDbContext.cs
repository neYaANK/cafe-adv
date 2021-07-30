using Cafe.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cafe.Data
{
    public class BaseDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccesLevel> UserAccesLevels { get; set; }
        public DbSet<ClientTable> ClientTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClientTable>().HasKey(  k=> new { k.Name,k.Id });
            modelBuilder.Entity<ClientTable>().Property(b=>b.Description).HasColumnName("DS");
            modelBuilder.Entity<ClientTable>().Property(b => b.Summ).HasPrecision(10, 4).HasComment("Це тестова сумма");
        }
    }
}
