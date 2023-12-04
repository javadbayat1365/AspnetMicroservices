namespace Basket.API.Entities;

public class ShoppingCart
{
    public ShoppingCart()
    {

    }
    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
    public string UserName { get; set; }
    public List<ShoppingCartItem> shoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    public decimal TotalPrice
    {
        get
        {
            decimal total = 0;
            foreach (var item in shoppingCartItems)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }
    }
}
