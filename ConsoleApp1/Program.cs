// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine($"Main method running on thread {Thread.CurrentThread.ManagedThreadId}");

         CallingMethod();

        Console.WriteLine($"Main method resumed on thread {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(10000);
    }

    public static async Task CallingMethod()
    {
        Console.WriteLine($"CallingMethod started on thread {Thread.CurrentThread.ManagedThreadId}");

        MySyncLikeAsyncMethod();
        // Main method running on thread 1
        // CallingMethod started on thread 1
        // MySyncLikeAsyncMethod started on thread 1
        // CallingMethod resumed on thread 1
        // Main method resumed on thread 1
        // MySyncLikeAsyncMethod resumed on thread 10
        // CPU work completed on thread 10 -- continues synchronously

        
        
        //-------------------------------------
       //Task.Run(()=>MySyncLikeAsyncMethod()) ;
       // Main method running on thread 1
       // CallingMethod started on thread 1
       // CallingMethod resumed on thread 1
       // Main method resumed on thread 1
       // MySyncLikeAsyncMethod started on thread 7
       // MySyncLikeAsyncMethod resumed on thread 8 -- continuation async
       // CPU work completed on thread 8

       

        Console.WriteLine($"CallingMethod resumed on thread {Thread.CurrentThread.ManagedThreadId}");
    }

    public static async Task MySyncLikeAsyncMethod()
    {
        Console.WriteLine($"MySyncLikeAsyncMethod started on thread {Thread.CurrentThread.ManagedThreadId}");

        // I/O-bound operation
        await Task.Delay(1000); // This is an asynchronous operation

        Console.WriteLine($"MySyncLikeAsyncMethod resumed on thread {Thread.CurrentThread.ManagedThreadId}");

        // CPU-bound work
        for (int i = 0; i < 1000000; i++)
        {
            // Simulate CPU-bound work
        }

        Console.WriteLine($"CPU work completed on thread {Thread.CurrentThread.ManagedThreadId}");
    }
}

