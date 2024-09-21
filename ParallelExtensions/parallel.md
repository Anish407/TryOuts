~~~
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
~~~
The reason this code is slower when incrementing values that are adjacent (e.g., _values[0] and _values[1]) compared to values that are farther apart has to do with how modern CPUs handle memory access, specifically with cache lines and false sharing.

Explanation:
## Cache Lines:

Think of your computer's memory (RAM) as a giant bookshelf, with each "book" (or slot) holding data. The CPU, which does the calculations, can't reach the bookshelf directly every time—it would be too slow. Instead, it uses a smaller, faster "desk" (called cache) where it keeps the data it's currently working on.

However, the CPU doesn't bring just one piece of data to the desk at a time. It grabs a whole block of data from the shelf. This block is called a cache line, which is usually about 64 bytes (enough to fit 16 integers, since an integer is typically 4 bytes). When the CPU needs a piece of data, 
it grabs the whole block, even if it only needs one item in the block.

CPUs store data in chunks of memory called cache lines, typically 64 bytes. When a thread modifies a value, it caches the entire cache line that contains the value.
In this case, _values[0] and _values[1] are likely to be on the same cache line because they are next to each other in memory (since an int is typically 4 bytes, you can fit several ints into one cache line).
## False Sharing:

If two threads are trying to modify values that are in the same cache line (even if they are modifying different variables), this creates false sharing. Every time one thread modifies its value, the cache line is invalidated in the other thread’s cache, forcing it to reload the cache line from main memory.
As a result, both threads end up continuously invalidating and reloading the same cache line, which significantly slows down performance.
Far Apart Values:

When the values being incremented are farther apart (i.e., in different cache lines), 
the threads can operate independently without causing cache invalidation, 
thus avoiding false sharing and improving performance.

Now, imagine two workers (threads) sitting at the same desk. Each worker is working on a different part of the same book (same cache line). If one worker writes something on the page (modifies data), the other worker has to pause and get a fresh copy of that page, even if they were working on a different part of the page. This constant passing of updates between the two workers is called false sharing.

In programming terms, when two threads work on different parts of the same cache line (like two nearby elements in an array), they keep interfering with each other even though they’re not using the same piece of data. This makes the program slower because each thread has to keep reloading the cache line.

This unnecessary back-and-forth can make things slower, which is why false sharing happens when multiple threads modify data that’s stored too close together in memory.

### Solution:
To avoid this, you can ensure that threads are working on data that’s far apart in memory (on different cache lines), so they don’t interfere with each other.