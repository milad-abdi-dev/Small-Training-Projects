// See https://aka.ms/new-console-template for more information

using Bogus;
using System.Collections.Generic;
using LinqTraining.ProblemNo1;

var orderItemFaker = new Faker<OrderItem>()
    .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())
    .RuleFor(i => i.Price, f => f.Random.Decimal(5, 500));

var customerFaker = new Faker<Customer>()
    .RuleFor(c => c.Id, f => f.IndexFaker + 1)
    .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0]);

List<Customer> customers = customerFaker.Generate(10);

var orderFaker = new Faker<Order>()
    .RuleFor(o => o.Id, f => f.IndexFaker + 1)
    .RuleFor(o => o.Items, f => orderItemFaker.Generate(f.Random.Int(1, 5)))
    .RuleFor(o => o.CustomerId, f => f.PickRandom(customers).Id);


List<Order> orders = orderFaker.Generate(1000);

List<string> productNames = orders.SelectMany(o => o.Items)
    .Where(i => i.Price > 50)
    .OrderByDescending(i => i.Price)
    .Take(5)
    .Select(i => i.ProductName)
    .ToList();
    
// Top 3 customers by total spending
var r1 = orders.GroupBy(o => o.CustomerId)
    .Select(g => new
    {
        CustomerId = g.Key,
        TotalSpending = g.SelectMany(o => o.Items).Sum(i => i.Price),
    })
    .OrderByDescending(o => o.TotalSpending)
    .Take(3)
    .ToList();
    
// Total sales per product
var r2 = orders
    .SelectMany(o => o.Items)
    .GroupBy(i => i.ProductName)
    .Select(g => new
    {
        ProductName = g.Key,
        TotalSpending = g.Sum(i => i.Price),
    })
    .OrderByDescending(o => o.TotalSpending)
    .ToList();

// Top 3 most sold products
// Based on count of occurrences
var r3 = orders
    .SelectMany(o => o.Items)
    .GroupBy(i => i.ProductName)
    .Select(g => new
    {
        ProductName = g.Key,
        SoldCount = g.Count(),
    })
    .OrderByDescending(x => x.SoldCount)
    .Take(3)
    .ToList();

// Top 5 products with: 
// - Total sales (Sum)
// - Sold count
// Only products with sales > 1000
// Sorted by total sales
var r4 = orders
    .SelectMany(o => o.Items)
    .GroupBy(i => i.ProductName)
    .Select(g => new
    {
        ProductName = g.Key,
        SoldCount = g.Count(),
        TotalSpending = g.Sum(i => i.Price),
    })
    .Where(x => x.TotalSpending > 1000)
    .OrderByDescending(x => x.TotalSpending)
    .Take(5)
    .ToList();
    
// Return:
// - Customer name
// - Order count
var r5 = customers.GroupJoin(orders,
        customer => customer.Id,
        order => order.CustomerId,
        (customer, ordersGroup) => new
        {
            CustomerName = customer.Name,
            OrderCount = ordersGroup.Count(),
        })
    .ToList();
    
// Return:
// Customer name
// OrderId (including customers with No orders)
var r6 = customers.GroupJoin(orders,
        customer => customer.Id,
        order => order.CustomerId,
        (customer, ordersGroup) => new
        {
            customer,
            ordersGroup,
        })
    .SelectMany(x => x.ordersGroup.DefaultIfEmpty(),
        (x, order) => new
        {
            CustomerName = x.customer.Name,
            OrderId = order?.Id
        })
    .ToList();
    
// Given: Customer -> Orders -> Items
var r7 = customers.GroupJoin(orders,
    customer => customer.Id,
    order => order.CustomerId,
    (customer, ordersGroup) => new
    {
        CustomerName = customer.Name,
        orderGroup = ordersGroup.DefaultIfEmpty(),
    })
    .SelectMany(x => x.orderGroup,
        (x, order) => new
        {
            x.CustomerName,
            orderItemGroup = order?.Items ?? Enumerable.Empty<OrderItem>(),
        })
    .SelectMany(x => x.orderItemGroup,
        (x, item) => new
        {
            x.CustomerName,
            item?.ProductName,
            item?.Price,
        });
        
// Customers who have at least one order item with price > 100
var r8 = customers.Where(c => c.Orders.Any(o => o.Items.Any(i => i.Price > 100)))
    .Select(c => c.Name)
    .ToList();

// Customers where all order items price > 50
var r9 = customers.Where(c => c.Orders.SelectMany(o => o.Items).All(i => i.Price > 50));

// Customers with no orders
var r10 = customers.Where(c => !c.Orders.Any()).ToList();

// Orders that contain any product from a given list 
var targetProducts = new[] { "Laptop", "Phone" };
var r11 = orders.Where(o => o.Items.Any(i => targetProducts.Contains(i.ProductName))).ToList();
