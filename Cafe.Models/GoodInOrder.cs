using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    public class GoodInOrder
    {
        public int Amount { get; set; }
        public Goods Good { get; set; }

        public override string ToString()
        {
            return Good.Name;
        }
    }
}
