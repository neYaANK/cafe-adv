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
        //public DbSet<DishStatus> CookStatuses { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Goods> Goods { get; set; }
        //public DbSet<OrdersGoods> OrdersGoods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>().HasMany(p => p.Goods)
                .WithMany(p => p.Orders).UsingEntity<OrdersGoods>(
                j => j.HasOne(pt => pt.Good).WithMany(t => t.OrdersGoods).HasForeignKey(pt => pt.GoodId),
                j => j.HasOne(pt => pt.Orders).WithMany(t => t.OrdersGoods).HasForeignKey(pt => pt.OrdersId),
                j =>
                {
                    j.HasKey(t => new { t.GoodId, t.OrdersId });
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
