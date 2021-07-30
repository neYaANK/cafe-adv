using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data
{
    public class LoginController<Context> : IUserLoginController
                    where Context : BaseDbContext ,new() 
    {
        AccessLevel _userAccesLevel;
        int _loginCounter = 4;
        public LoginController(AccessLevel userAccesLevel)
        {
            _userAccesLevel = userAccesLevel;
        }

        public IUser[] Users
        {
            get
            {
                using (var context = new Context())
                {
                    return context.UserAccesLevels.Where(s=>s.AccessLevel == _userAccesLevel)
                                                  .Include(i=>i.User)
                                                  .Select(sel=>sel.User)
                                                  .ToArray();
                }

            } 
        }

        public int LoginCounter => _loginCounter;

        public bool TryLogin(int userId, string password)
        {
            if (_loginCounter <=0) return false;
            using (var context = new Context())
            {
                if (context.Users.SingleOrDefault(s=>s.Id == userId && s.Password == password) == null)
                {
                    _loginCounter--;
                    return false;
                }
                else
                    return true;

            }
        }
    }
}
