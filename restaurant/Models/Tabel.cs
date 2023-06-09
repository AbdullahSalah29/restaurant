namespace restaurant.Models
{
    public class Tabel
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string KindOfFood { get; set; }

        public User user { get; set; }
    }
}
