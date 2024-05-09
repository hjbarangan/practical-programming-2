using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

// Multi-threading
public class NumberProcessor
{
    public void ProcessNumber(int number)
    {
        // Simulate time-consuming operation
        Thread.Sleep(new Random().Next(1000, 5000));
        Console.WriteLine($"Processed number: {number}");
    }
}

// LINQ
public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
}

// Events
public delegate void ProgressUpdate(int percentage);

public class Downloader
{
    public int Progress { get; private set; }
    public event ProgressUpdate ProgressChanged;

    public void SimulateDownload()
    {
        for (int i = 0; i <= 100; i += 10)
        {
            Progress = i;
            OnProgressChanged(Progress);
            Thread.Sleep(1000);
        }
    }

    protected virtual void OnProgressChanged(int percentage)
    {
        ProgressChanged?.Invoke(percentage);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Multi-threading
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        NumberProcessor processor = new NumberProcessor();

        foreach (var number in numbers)
        {
            Thread thread = new Thread(() => processor.ProcessNumber(number));
            thread.Start();
        }

        // LINQ
        List<Product> products = new List<Product>
        {
            new Product { Name = "Product1", Price = 10.99 },
            new Product { Name = "Product2", Price = 15.49 },
            new Product { Name = "Product3", Price = 20.99 },
            new Product { Name = "Product4", Price = 5.99 },
            new Product { Name = "Product5", Price = 25.99 }
        };

        double specifiedPrice = 15.00;
        var filteredProducts = products.Where(p => p.Price >= specifiedPrice);
        var productNamesUpper = filteredProducts.Select(p => p.Name.ToUpper());

        Console.WriteLine("Products with price greater than or equal to " + specifiedPrice + ":");
        foreach (var product in filteredProducts)
        {
            Console.WriteLine($"{product.Name}: ${product.Price}");
        }

        Console.WriteLine("\nProduct names in uppercase:");
        foreach (var name in productNamesUpper)
        {
            Console.WriteLine(name);
        }

        // Events
        Downloader downloader = new Downloader();
        downloader.ProgressChanged += ProgressHandler;
        downloader.SimulateDownload();
    }

    static void ProgressHandler(int percentage)
    {
        Console.WriteLine($"Download Progress: {percentage}%");
    }
}
