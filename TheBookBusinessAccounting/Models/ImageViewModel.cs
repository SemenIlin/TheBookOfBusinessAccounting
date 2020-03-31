using System.Collections.Generic;

namespace TheBookBusinessAccounting.Models
{
    public class ImageViewModel
    {
        public int Id { get; set; }

        public byte[] Screen { get; set; }
        public string ScreenFormat { get; set; }

        public int ItemId { get; set; }

        public Dictionary<int,string> Items { get; set; }
    }
}