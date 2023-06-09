using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace restaurant.Models
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public string username_ID { get; set; }
        public string Password { get; set; }
        public string name { get; set; }
        public string img { get; set; }
        public string email { get; set; }
      
        public ICollection<Order>? orders { get; set; }
        public ICollection <Payment>? Payment { get; set; }
    }
}
