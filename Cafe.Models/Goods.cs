using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    public class Goods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsFood { get; set; }

        public List<Orders> Orders { get; set; }
        public List<OrdersGoods> OrdersGoods{ get; set; }
    }
}
