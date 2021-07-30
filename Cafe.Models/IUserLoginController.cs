using Cafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    public interface IUserLoginController
    {


        IUser[]  Users{ get;}
        bool TryLogin(int userId, string password);
        int LoginCounter { get;}
    }
}
