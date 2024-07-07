IEnumerable<int> result = Enumerable.Range(0,3).NaiveSelect(i => i * 2);
// until we call move next, no code from the body of the iterator is called
// So the null checks inside the NaiveSelect method wont be executed
Console.WriteLine("No exceptions thrown till now");

// The ArgumentNullException will be thrown at this point since the
// collection is iterated for the first time here and this is where the body of the 
// naiveSelect method gets executed
foreach (var item in result)
{
    Console.WriteLine(item); 
}

Console.ReadLine();


public static class SelectImplementationProgram
{

    public static IEnumerable<TResult> NaiveSelect<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(selector);
        
        foreach (var item in source)
        {
            yield return selector(item);
        }
    }

   public static IEnumerable<TResult> NaiveSelectWithImplementation<TSource, TResult>(this IEnumerable<TSource> src,
        Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(selector);

        return SelectImplementation(src, selector);
       
        // Now we have separated the method that does the iteration from the method that 
        // does the validation. So the Exception checks will not be deferred.
        // Otherwise, unitl the response is iterated upon the null checks won't be executed
        static IEnumerable<TResult> SelectImplementation(IEnumerable<TSource> src,
            Func<TSource, TResult> selector)
        {
            foreach (var item in src)
            {
               yield return selector(item);
            }
        }
    }
}