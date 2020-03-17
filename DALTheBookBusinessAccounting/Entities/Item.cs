using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Entities
{
    public class Item
    {
        public Item()
        {
            Images = new List<Image>();
        }

        public int Id { get; set; }

        public string Title { get; set; }
        public string InventoryNumber { get; set; }
        public string LocationOfItem { get; set; }
        public string About { get; set; }
        public string CategoryName { get; set; }
        public string StatusName { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Status Status { get; set; }
        public int StatusId { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
