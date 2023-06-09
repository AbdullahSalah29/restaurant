namespace restaurant.Models
{
    public class ViewUserOrdersTabel
    {
        public User User { get; set; }
        public List<Order> Order { get; set; }
        public List<Tabel> Tabel { get; set; }
        public List<List<Order_Food>> Food { get; set; }
        
    }
}
