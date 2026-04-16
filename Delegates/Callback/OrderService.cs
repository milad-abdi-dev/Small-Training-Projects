namespace Delegates.Callback;

public class OrderService
{
    public void ProcessOrder(Order order, Action<Order> onProcessed)
    {
        ArgumentNullException.ThrowIfNull(order);
        ArgumentNullException.ThrowIfNull(onProcessed);
        
        Console.WriteLine($"Order {order.Id} is being processed");
        Thread.Sleep(500);
        Console.WriteLine("Payment completed");
        
        onProcessed(order);
    }
}