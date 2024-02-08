namespace Shopping.Aggregator.Models
{
    public class BasketItemExtendedModel
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }

        //Product Additional Field
        public String Category { get; set; }
        public String Summery { get; set; }
        public String Description { get; set; }
        public String ImageFile { get; set; }
    }
}
