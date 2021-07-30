using System;
using System.Collections;

namespace Cafe.Models
{
    public class User:IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public static User DefaultAdmin = new User 
        {
            Id = 1,
            Name ="Admin", 
            Password ="12345"
        };
        public static User User1 = new User { Id = 2, Name = "Ivan Gardin", Password = "12345" };
        public static User User2 = new User { Id = 3, Name ="Petro Stepanov",Password = "12345" };
        public static User User3 = new User { Id = 4, Name ="Mirko Shuher",Password = "12345" };
        public static User User4 = new User { Id = 5, Name ="Peter Hugert",Password = "12345" };
        public static User User5 = new User { Id = 6, Name ="Ruzhena Stefanic",Password = "12345" };


    }
}
