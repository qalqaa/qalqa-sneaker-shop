using System.Text.Json;

namespace qalqasneakershop.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public ItemDescription Description { get; set; }
        public float Rating { get; set; }
        public List<ItemReview> Reviews { get; set; }
    }
    public class ItemDescription
    {
        public string Info1 { get; set; } = string.Empty;
        public string Info2 { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Season { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Article { get; set; } = string.Empty;
    }
    public class ItemReview
    {
        public int Estimation { get; set; }
        public int Id { get; set; }
        public string Reviewer { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
    
}
