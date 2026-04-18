using Delegates.Product_Filtering;

namespace Delegates.Custom_Sorting;

public class ProductSortService
{
    public void Sort(List<Product> products, Comparison<Product> comparison)
    {
        // added null checks for products and comparison in polished version
        ArgumentNullException.ThrowIfNull(products);
        ArgumentNullException.ThrowIfNull(comparison);
        
        products.Sort(comparison);
    }
}