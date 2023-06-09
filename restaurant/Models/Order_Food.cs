using System.ComponentModel.DataAnnotations.Schema;

namespace restaurant.Models
{
    public class Order_Food
    {
        public int id { get; set; }
        public int order_ID { get; set; }
        public int food_ID { get; set; }

        public int qty { get; set; }
        [ForeignKey("order_ID")]

        public Order order { get; set; }
        [ForeignKey("food_ID")]
        public Food food { get; set; }

    }
}
