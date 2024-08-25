// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using System.Threading.Channels;

// for (int i = 0; i < 100; i++)
// {
//    _ = Task.Run(() => Console.WriteLine(i));
// }
// this prints 100 100 100 ... 100
// Task.Run method captures the variable i, but it does not capture the value of i at the time the lambda expression is created.
// Instead, it captures the variable itself, meaning all tasks will share the same i

// Fix
for (int i = 0; i < 100; i++)
{
   int temp = i;
   _ = Task.Run(() => Console.WriteLine(temp));
}


Console.ReadLine();