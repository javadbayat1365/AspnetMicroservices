namespace Shopping.Aggregator.Models
{
    public class ShoppingModel
    {
        public string UserName { get; set; }
        public BasketModel BasketModel { get; set; } = null!;
        public IEnumerable<OrderResponseModel> Orders { get; set; }
    }
}
