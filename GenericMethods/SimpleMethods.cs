using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GenericMethods
{
    public static class SimpleMethods
    {
        #region Linq

        public static IOrderedEnumerable<TSource> GetFilteredListByOrder<TSource, TKey>(this IEnumerable<TSource> mainCollection, Func<TSource, bool> predicate, Func<TSource, TKey> key) where TSource : class where TKey : struct
        {
            var result = mainCollection.Where(predicate).OrderBy(key);
            return result;
        }
        public static IOrderedEnumerable<TSource> GetFilteredListByReverseOrder<TSource, TKey>(this IEnumerable<TSource> mainCollection, Func<TSource, bool> predicate, Func<TSource, TKey> key) where TSource : class where TKey : struct
        {
            var result = mainCollection.Where(predicate).OrderByDescending(key);
            return result;
        }

        public static IOrderedQueryable<TSource> GetFilteredListByOrder<TSource, TKey>(this IQueryable<TSource> mainCollection, Expression<Func<TSource, bool>> predicate, Expression<Func<TSource, TKey>> key) where TSource : class where TKey : struct
        {
            var result = mainCollection.Where(predicate).OrderBy(key);
            return result;
        }
        public static IOrderedQueryable<TSource> GetFilteredListByReverseOrder<TSource, TKey>(this IQueryable<TSource> mainCollection, Expression<Func<TSource, bool>> predicate, Expression<Func<TSource, TKey>> key) where TSource : class where TKey : struct
        {
            var result = mainCollection.Where(predicate).OrderByDescending(key);
            return result;
        }

        public static TEntity GetEntityById<TEntity>(this IQueryable<TEntity> entities,Guid id) where TEntity : class
        {
            var entity = default(TEntity);

            if (typeof(TEntity).GetProperty("Id",typeof(Guid)) != null)
            {
                entity = entities.FirstOrDefault(e=> (Guid)(typeof(TEntity).GetProperty("Id",typeof(Guid)).GetValue(e)) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IEnumerable<TEntity> entities, Guid id) where TEntity : class
        {
            var entity = default(TEntity);

            if (typeof(TEntity).GetProperty("Id", typeof(Guid)) != null)
            {
                entity = entities.FirstOrDefault(e => (Guid)(typeof(TEntity).GetProperty("Id", typeof(Guid)).GetValue(e)) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IQueryable<TEntity> entities, int id) where TEntity : class
        {
            var entity = default(TEntity);

            if (typeof(TEntity).GetProperty("Id", typeof(int)) != null)
            {
                entity = entities.FirstOrDefault(e => (int)(typeof(TEntity).GetProperty("Id", typeof(int)).GetValue(e)) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IEnumerable<TEntity> entities, int id) where TEntity : class
        {
            var entity = default(TEntity);

            if (typeof(TEntity).GetProperty("Id", typeof(int)) != null)
            {
                entity = entities.FirstOrDefault(e => (int)(typeof(TEntity).GetProperty("Id", typeof(int)).GetValue(e)) == id);
            }
            return entity;
        }
        #endregion
    }
}
