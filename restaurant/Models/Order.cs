using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restaurant.Models
{
    public class Order
    {
        [Key]
        public int order_ID { get; set; }
        public string? customar_name { get; set; }
        public string? phone_number { get; set; }
        public int? price { get; set; }
        [DataType(DataType.DateTime)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? DateTime { get; set; }
        public string? address { get; set; }
        public string? messege { get; set; }
        public Boolean? payment { get; set; }
        public string? username_ID { get; set; }
        [ForeignKey("username_ID")]
        public User user { get; set; }
        public ICollection<Order_Food> orders { get; set; }


    }
}
