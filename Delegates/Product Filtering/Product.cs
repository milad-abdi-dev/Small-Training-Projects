namespace Delegates.Product_Filtering;

public class Product
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    private Product(long id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public static Product Create(long id, string name, decimal price, int stock) =>
        new Product(id, name, price, stock);
        
}