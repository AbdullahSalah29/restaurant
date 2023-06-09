namespace restaurant.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Master_card  { get; set; }
        public string csv { get; set; }
        public User user { get; set; }
    }
}
