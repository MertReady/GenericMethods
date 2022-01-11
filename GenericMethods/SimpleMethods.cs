using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GenericMethods
{
    public static class SimpleMethods
    {
        public static IOrderedEnumerable<T> GetListWhereOrderBy<T,TKey>(this IEnumerable<T> myCollection, Func<T, bool> predicate,Func<T,TKey> key) where T : class
        {
            var result = myCollection.Where(predicate).OrderBy(key);
            return result;
        }
    }
}
