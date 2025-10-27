namespace Inventar.Models
{
    public class SoldItem
    {
        public Guid ProductID { get; set; }
        public decimal Price { get; set; }

        public SoldItem(Guid productId,decimal price)
        {
            ProductID = productId;
            Price = price;
        }
    }
}
