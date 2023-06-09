using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace restaurant.Models
{
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int food_ID { get; set; }
        public string food_name { get; set; }
        public string food_description { get; set; }
        public string type { get; set; }
        public int price { get; set; }
        public string img { get; set; }
       
        public ICollection<Order_Food> orders { get; set; }
        

    }
}
