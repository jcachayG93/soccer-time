using Xunit;

namespace Application.TestCommon.Extensions;

public static class TestCollectionExtensions
{
    public static IEnumerable<T> ToCollection<T>(this T item, params T[] additionalItems)
    {
        var result = new List<T>() {item};
        result.AddRange(additionalItems);
        return result;
    }

    public static void ShouldBeEquivalentTo<T1, T2>(this IEnumerable<T1> col,
        IEnumerable<T2> other, Func<T1, T2, bool> equalityCriteria)
    {
     
        if (!col.IsEquivalentTo(other, equalityCriteria, out var reason))
        {
            Assert.Fail(reason);
        }
    }
    
    public static void ShouldBeEquivalentTo<T1, T2>(this IEnumerable<T1> col,
        IEnumerable<T2> other)
    {
     
        if (!col.IsEquivalentTo(other, 
                (x,y)=>x.Equals(y), out var reason))
        {
            Assert.Fail(reason);
        }
    }

    public static bool IsEquivalentTo<T>(this IEnumerable<T> col,
        IEnumerable<T> other)
    {
        return col.IsEquivalentTo(other, (x, y) => x.Equals(y));
    }
    public static bool IsEquivalentTo<T1, T2>(this IEnumerable<T1> col,
        IEnumerable<T2> other, Func<T1,T2, bool> equalityCriteria, out string reason)
    {
        if (!col.Any())
        {
            reason = "Collection is empty";
            return false;
        }

        if (col.Count() != other.Count())
        {
            reason = "Both collections have the same number of elements";
            return false;
        }

        var caseA = col.All(x =>
            other.Any(y => equalityCriteria(x, y)));

        if (!caseA)
        {
            reason = "All elements in the collection must have a match in the other collection";
            return false;
        }

        var caseB = other.All(x =>
            col.Any(y => equalityCriteria(y, x)));

        if (!caseB)
        {
            reason = "All elements in other collection must have a match in the collection";
            return false;
        }

        reason = "";
        return true;
    }

    public static bool IsEquivalentTo<T1, T2>(this IEnumerable<T1> col,
        IEnumerable<T2> other, Func<T1, T2, bool> equalityCriteria)
    {
        return col.IsEquivalentTo(other, equalityCriteria, out _);
    }
}