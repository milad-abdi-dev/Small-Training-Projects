using Delegates.Callback;

namespace Delegates.Order_Validation;

public class OrderService
{
    public bool ProcessOrder(Order order, Func<Order, bool> validator)
    {
        ArgumentNullException.ThrowIfNull(order);
        ArgumentNullException.ThrowIfNull(validator);
        
        bool isValid = validator(order);

        if (!isValid)
        {
            Console.WriteLine("Order validation failed");
            return false;
        }
        
        Console.WriteLine($"Order {order.Id} is being processed");
        Thread.Sleep(500);
        Console.WriteLine("Payment completed");
        
        return true;
    }
}