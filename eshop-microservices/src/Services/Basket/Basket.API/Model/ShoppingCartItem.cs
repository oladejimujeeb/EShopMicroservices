namespace Basket.API.Model
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; } = default;
        public string Colour { get; set; } = default!;
        public decimal Price { get; set; } = default;
        public Guid ProductID { get; set; } = default;
        public string ProductName { get; set; } = default!;
    }
}
