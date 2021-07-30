using Cafe.Data;
using Cafe.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAdmin.Data
{
    public class CafeAdminDbContext:BaseDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;DataBase=C:\DataBases\cafe.mdf");
            optionsBuilder.UseSqlite(@"FileName=C:\Users\neYa\DBases\cafe.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new User[] { User.DefaultAdmin, User.User1, User.User2, User.User3, User.User4, User.User5 });
            modelBuilder.Entity<UserAccesLevel>().HasData(new UserAccesLevel[] { UserAccesLevel.DefaultAdminAccessLevel, UserAccesLevel.User1AccessLevel, UserAccesLevel.User2AccessLevel, UserAccesLevel.User3AccessLevel, UserAccesLevel.User4AccessLevel, UserAccesLevel.User5AccessLevel ,UserAccesLevel.User5AccessLevel2});
        }

    }
}
