// See https://aka.ms/new-console-template for more information

// Foreach Internally calls the GetEnumerator

#region Foreach

IEnumerable<int> values = GetValues();
using (IEnumerator<int> enumerator = values.GetEnumerator()) 
// which converts to a try finally, and we call dispose on the enumerator
{
    while (enumerator.MoveNext())
    {
        var value = enumerator.Current;
        Console.WriteLine(value);
    }    
};

// instead of the using it would lower to
IEnumerator<int> enumerator1 = values.GetEnumerator();
try
{
    var value= enumerator1.Current;
    Console.WriteLine(value);
}
catch (Exception e)
{
    enumerator1?.Dispose();
}

foreach (var i in GetValues())
{
    Console.WriteLine(i);
}


static IEnumerable<int> GetValues()
{
    yield return 1;
    yield return 2;
    yield return 3;
}

#endregion
Console.WriteLine("Hello, World!");