namespace StoreModels
{
    public class Cart
    {
        public Cart()
        {}
        public Cart(Cart cart)
        {
            Id = cart.Id;
            LocationId = cart.LocationId;
            CustomerId = cart.CustomerId;
            InventoryId = cart.InventoryId;
            ProductId = cart.ProductId;
            Quantity = cart.Quantity;
            Price = cart.Price;
            Name = cart.Name;
        }
        public int Id { get; set; }
        public int TempId { get; set; }
        public int CustomerId{ get; set; }
        public int LocationId{ get; set; }
        public int InventoryId{ get; set; }
        public int ProductId{ get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }

    }
}