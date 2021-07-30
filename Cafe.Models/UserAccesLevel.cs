using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    public class UserAccesLevel
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public static UserAccesLevel DefaultAdminAccessLevel = new UserAccesLevel() { 
         Id= 1, UserId =User.DefaultAdmin.Id,AccessLevel= AccessLevel.Admin
        };
        public static UserAccesLevel User1AccessLevel = new UserAccesLevel() { Id = 2, UserId = User.User1.Id, AccessLevel= AccessLevel.Waiter };
        public static UserAccesLevel User2AccessLevel = new UserAccesLevel() { Id = 3, UserId = User.User2.Id, AccessLevel = AccessLevel.Waiter };
        public static UserAccesLevel User3AccessLevel = new UserAccesLevel() { Id = 4, UserId = User.User3.Id, AccessLevel = AccessLevel.Waiter };
        public static UserAccesLevel User4AccessLevel = new UserAccesLevel() { Id = 5, UserId = User.User4.Id, AccessLevel = AccessLevel.Barmen };
        public static UserAccesLevel User5AccessLevel = new UserAccesLevel() { Id = 6, UserId = User.User5.Id, AccessLevel = AccessLevel.Barmen };
        public static UserAccesLevel User5AccessLevel2 = new UserAccesLevel() { Id = 7, UserId = User.User5.Id, AccessLevel = AccessLevel.Waiter };

    }
}
