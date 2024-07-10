using System.Drawing;
using System.Drawing.Imaging;

namespace qalqasneakershop.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Price { get; set; }
        public byte[]? ImageData { get; set; }
    }
}
