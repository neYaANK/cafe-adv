using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    public interface IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
