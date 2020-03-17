using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Entities
{
    public class Category
    {
        public Category()
        {
            Items = new List<Item>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
