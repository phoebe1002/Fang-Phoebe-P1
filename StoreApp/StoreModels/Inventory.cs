namespace StoreModels
{
    public class Inventory
    {
        public Inventory()
        {}

        // public Inventory(int Quantity)
        // {
        //     this.Quantity = Quantity;
        // }
        // public Inventory(int id, int quantity) : this(quantity)
        // {
        //     this.Id = id;
        // }

        public Inventory(int id, int quantity)
        {
            this.Id = id;
            this.Quantity = quantity;
        }
        public int Id { get; set; }

        public int Quantity { get; set; }
        public Product Product { get; set;}
        public int ProductId { get; set; }
        public int LocationId { get; set; }

        
    }
}