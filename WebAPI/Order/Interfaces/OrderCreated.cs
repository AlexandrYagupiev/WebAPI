namespace WebAPI.Order.Interfaces
{
    public class OrderCreated
    {
        Guid Id { get; set; }
        string ProductName { get; set; }
        decimal Price { get; set; }
        int Quantity { get; set; }
    }
}
