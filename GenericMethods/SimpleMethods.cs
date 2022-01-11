using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GenericMethods
{
    public static class SimpleMethods
    {
        public static IOrderedEnumerable<TSource> GetEnumerableWhereOrderBy<TSource, TKey>(this IEnumerable<TSource> myCollection, Func<TSource, bool> predicate, Func<TSource, TKey> key) where TSource : class where TKey : struct
        {
            var result = myCollection.Where(predicate).OrderBy(key);
            return result;
        }
    }
}
