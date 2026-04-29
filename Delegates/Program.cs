// See https://aka.ms/new-console-template for more information

// ---------- Callback ----------
/*using Delegates.Callback;
void SendEmail(Order order) => Console.WriteLine($"Sending email for order with Id {order.Id} for customer {order.CustomerName} ...");
void LogOrder(Order order) => Console.WriteLine($"Log: Order processed with Id {order.Id} for customer {order.CustomerName} ...");
void NotifyWarehouses(Order order) => Console.WriteLine($"Warehouse Notification: Order processed with Id {order.Id} for customer {order.CustomerName} ...");

Action<Order> actions = SendEmail;
actions += LogOrder;
actions += NotifyWarehouses;

var order1 = new Order(1, "Milad", 5_000_000);
var orderService = new OrderService();
orderService.ProcessOrder(order1, actions);*/


// -------- Order Validation --------

/*
using Delegates.Callback;
using OrderService = Delegates.Order_Validation.OrderService;

var order = new Order(1, "Milad", 500, 100);
OrderService orderService = new OrderService();


// Shopping Rules
Func<Order, bool> rules = o => string.IsNullOrWhiteSpace(o.CustomerName) &&
                                     o.TotalAmount > 0;
// Shipping Rules
rules += o =>
{
     

     if (o.TotalWeight <= 0) return false;

     return true;
};

orderService.ProcessOrder(order, rules);*/

// -------- Product Filtering --------

/*using Delegates.Product_Filtering;

List<Product> products = new List<Product>
{
    Product.Create(1, "Egg", 2, 500),
    Product.Create(2, "Cheese", 5, 200),
    Product.Create(3, "Pepperoni", 10, 100),
    Product.Create(4, "Pineapple", 10, 50)
};

ProductService productService = new ProductService();
List<Product> filterProducts = productService.FilterProducts(products, p => p is { Price: < 10, Stock: > 300 });
foreach (Product product in filterProducts)
{
    Console.WriteLine(product.Name);
    Console.WriteLine(product.Price);
    Console.WriteLine(product.Stock);
    Console.WriteLine(product.Id);
}*/

// -------- Product Sorting --------

using Delegates.Custom_Sorting;
using Delegates.Product_Filtering;

List<Product> products = new List<Product>
{
    Product.Create(1, "Egg", 2, 1000),
    Product.Create(2, "Cheese", 15, 200),
    Product.Create(3, "Pepperoni", 60, 100),
    Product.Create(4, "Pineapple", 10, 50),
            Product.Create(5, "Bread", 2, 1500),
        Product.Create(6, "Milk", 2, 500),
        Product.Create(7, "Butter", 12, 200),
         Product.Create(8, "Sausage", 10, 100),
         Product.Create(9, "Olives", 100, 50),
};

ProductSortService productSortService = new ProductSortService();
// Senior level sorting using reusable comparison method from ProductComparisons class
productSortService.Sort(products, ProductComparisons.ByPriceThenDescendingStock());
productSortService.Sort(products, (p1, p2) =>
{
    int compareTo = p1.Price.CompareTo(p2.Price);
    return compareTo == 0 ? p2.Stock.CompareTo(p1.Stock) : compareTo;
});
foreach (Product product in products)
{
    Console.WriteLine(product.Name);
    Console.WriteLine(product.Price);
    Console.WriteLine(product.Stock);
}
