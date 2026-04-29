// See https://aka.ms/new-console-template for more information

using Bogus;
using System.Collections.Generic;
using LinqTraining.ProblemNo1;

var orderItemFaker = new Faker<OrderItem>()
    .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())
    .RuleFor(i => i.Price, f => f.Random.Decimal(5, 500));

var orderFaker = new Faker<Order>()
    .RuleFor(o => o.Id, f => f.IndexFaker + 1)
    .RuleFor(o => o.Items, f => orderItemFaker.Generate(f.Random.Int(1, 5)))
    .RuleFor(o => o.CustomerId, f => f.Random.Int(1, 10));

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
orders
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
orders
    .SelectMany(o => o.Items)
    .GroupBy(i => i.ProductName)
    .Select(g => new
    {
        ProductName = g.Key,
        SoldCount = g.Count(),
    })
    .OrderByDescending(o => o.SoldCount)
    .Take(3)
    .ToList();