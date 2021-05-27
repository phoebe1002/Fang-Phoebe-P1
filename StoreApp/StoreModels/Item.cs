namespace StoreModels
{
    /// <summary>
    /// Data Structure used to models a product and quantity
    /// </summary>
    public class Item
    {
        public Item()
        {}

        public Item(int Quantity)
        {
            this.Quantity = Quantity;
        }
        public Item(int id, int quantity) : this(quantity)
        {
            this.Id = id;
        }
        public int Id { get; set; }
        public int Quantity { get; set; }
        //public Product Product { get; set;}
        public int ProductId { get; set; }
        public int OrderId { get; set; }

    }
}