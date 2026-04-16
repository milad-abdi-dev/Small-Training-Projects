namespace Delegates.Product_Filtering;

public class ProductService
{
    public List<Product> FilterProducts(IEnumerable<Product> products, Predicate<Product> filter)
    {
        ArgumentNullException.ThrowIfNull(products);
        ArgumentNullException.ThrowIfNull(filter);

        List<Product> filteredProducts = new List<Product>();
        foreach (Product product in products)
        {
            if (filter(product))
            {
                filteredProducts.Add(product);
            }
        }
        
        return filteredProducts;
    }
}