namespace DALTheBookBusinessAccounting.Entities
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] Screen { get; set; }
        public string ScreenFormat { get; set; }

        public Item Item { get; set; }
        public int ItemId { get; set; }
    }
}
