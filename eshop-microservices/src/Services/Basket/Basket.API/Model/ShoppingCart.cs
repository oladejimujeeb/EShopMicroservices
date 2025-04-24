namespace Basket.API.Model
{
    public class ShoppingCart
    {
        public string UseName { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price);
        public ShoppingCart()
        {
        }
        public ShoppingCart(string useName)
        {
            UseName = useName;
        }
    }
}
