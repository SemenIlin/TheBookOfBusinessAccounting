using DALTheBookBusinessAccounting.Entities;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.ModelsDto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string InventoryNumber { get; set; }
        public string LocationOfItem { get; set; }
        public string About { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public ICollection<Image> Images { get; set; }

        public int CategoryId { get; set; }
        public int StatusId { get; set; }
    }
}
