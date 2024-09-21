using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

[ShortRunJob]
public class Tests
{
    private int[] _values = new int[32];

    [Params(0, 31)]
    public int Index { get; set; }

    [Benchmark]
    public void Invoke()
    {
        // here the values that are passed are next or close to each
        // other in the array
        Parallel.Invoke(
            () => Increment(ref _values[0]), 
            () => Increment(ref _values[1])
        );
    }

    static void Increment(ref int value)
    {
        for (int i = 0; i < 100_000_000; i++)
        {
            value++;
        }
    }
}