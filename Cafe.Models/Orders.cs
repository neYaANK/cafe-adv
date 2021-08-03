using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public ClientTable Table { get; set; }
        public DateTime OrderTime { get; set; }

        public List<Goods> Goods { get; set; }
        public List<OrdersGoods> OrdersGoods { get; set; }
        public bool IsPaid { get; set; } = false;
        [NotMapped]
        public int Sum { get {
                int sum = 0;
                OrdersGoods.ForEach(c =>
                {
                    sum += c.Good.Price * c.Amount;
                });
                return sum;
            
            } }

    }
}
