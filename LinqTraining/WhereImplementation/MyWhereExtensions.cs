namespace LinqTraining.WhereImplementation;

public static class MyWhereExtensions
{
    public static IEnumerable<T> MyWhere<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);


        foreach (var item in source)
        {
            if (predicate(item))
                yield return item;
        }
    }

    public static IEnumerable<TResult> MySelect<T, TResult>(
        this IEnumerable<T> source,
        Func<T, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(selector);


        foreach (var item in source)
        {
            yield return selector(item);
        }
    }
    
    public static IEnumerable<TResult> MySelectMany<T, TResult>(
        this IEnumerable<T> source,
        Func<T, IEnumerable<TResult>> selector)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(selector);


        foreach (var item in source)
        {
            foreach (var subItem in selector(item))
            {
                yield return subItem;
            }
        }
    }

    public static bool MyAny<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);
        
        foreach (var item in source)
        {
            if (predicate(item))
                return true;
        }
        
        return false;
    }

    public static bool MyAll<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);
        
        foreach (var item in source)
        {
            if (!predicate(item))
            {
                return false;
            }
        }
        
        return true;
    }

    public static int MyCount<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);
        
        int count = 0;
        foreach (var item in source)
        {
            if (predicate(item))
            {
                count++;
            }
        }
        
        return count;
    }
}