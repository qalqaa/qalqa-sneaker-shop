namespace qalqasneakershop.Models
{
    public class Order
    {
        public Guid UserID { get; set; }
        public int OrderID { get; set; }
        public string SneakerID { get; set; } = string.Empty;
    }
}
