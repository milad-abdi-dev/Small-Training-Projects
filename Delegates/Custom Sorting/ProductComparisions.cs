using Delegates.Product_Filtering;

namespace Delegates.Custom_Sorting;

// Senior level extension added in polished version
public static class ProductComparisons
{
    public static Comparison<Product> ByPriceThenDescendingStock()
    {
        return (p1, p2) =>
        {
            int priceComparison = p1.Price.CompareTo(p2.Price);
            return priceComparison != 0 ? priceComparison : p2.Stock.CompareTo(p1.Stock);
        };
    }

    public static Comparison<Product> ByStock() =>
        (p1, p2) => p1.Stock.CompareTo(p2.Stock);
}