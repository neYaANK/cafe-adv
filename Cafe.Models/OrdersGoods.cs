using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    
    public class OrdersGoods
    {
        public int OrdersId { get; set; }
        public Orders Orders { get; set; }
        public int GoodId { get; set; }
        public Goods Good { get; set; }
        public bool IsServed { get; set; } = false;
        public int Amount { get; set; }

        public int? CreatorId { get; set; } = null;
        public User? Creator { get; set; } = null;

        public bool IsDone { get; set; }
    }
}
