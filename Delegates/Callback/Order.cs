namespace Delegates.Callback;

public class Order
{
    /// <summary>
    /// create Order
    /// </summary>
    /// <param name="id"></param>
    /// <param name="customerName"></param>
    /// <param name="totalAmount"></param>
    /// <param name="totalWeight">weight is based on KG</param>
    public Order(long id, string customerName, decimal totalAmount, int totalWeight)
    {
        Id = id;
        CustomerName = customerName;
        TotalAmount = totalAmount;
        TotalWeight = totalWeight;
        
        if (TotalWeight > 50 && TotalAmount < 1_000_000)
        {
            IncreaseTotalAmount(500);
        }
    }

    public long Id { get; private set; }
    public string CustomerName { get; private set; }
    public decimal TotalAmount { get; private set; }
    public int TotalWeight { get; private set; }

    private void IncreaseTotalAmount(decimal amount)
    {
        TotalAmount += amount;
    }
}