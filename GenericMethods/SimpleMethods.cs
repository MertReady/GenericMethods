using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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

        public static TEntity GetEntityById<TEntity>(this IQueryable<TEntity> entities, Guid id) where TEntity : class
        {
            var entity = default(TEntity);

            if (entity.HasPropertyWithType("Id", typeof(Guid)))
            {
                entity = entities.FirstOrDefault(e => Guid.Parse(typeof(TEntity).GetProperty("Id", typeof(Guid)).GetValue(e).ToString()) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IEnumerable<TEntity> entities, Guid id) where TEntity : class
        {
            var entity = default(TEntity);

            if (entity.HasPropertyWithType("Id", typeof(Guid)))
            {
                entity = entities.FirstOrDefault(e => Guid.Parse(typeof(TEntity).GetProperty("Id", typeof(Guid)).GetValue(e).ToString()) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IQueryable<TEntity> entities, int id) where TEntity : class
        {
            var entity = default(TEntity);

            if (entity.HasPropertyWithType("Id", typeof(int)))
            {
                entity = entities.FirstOrDefault(e => Convert.ToInt32(typeof(TEntity).GetProperty("Id", typeof(int)).GetValue(e).ToString()) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IEnumerable<TEntity> entities, int id) where TEntity : class
        {
            var entity = default(TEntity);

            if (entity.HasPropertyWithType("Id", typeof(int)))
            {
                entity = entities.FirstOrDefault(e => Convert.ToInt32(typeof(TEntity).GetProperty("Id", typeof(int)).GetValue(e).ToString()) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IQueryable<TEntity> entities, long id) where TEntity : class
        {
            var entity = default(TEntity);

            if (entity.HasPropertyWithType("Id", typeof(long)))
            {
                entity = entities.FirstOrDefault(e => Convert.ToInt64(typeof(TEntity).GetProperty("Id", typeof(long)).GetValue(e).ToString()) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IEnumerable<TEntity> entities, long id) where TEntity : class
        {
            var entity = default(TEntity);

            if (entity.HasPropertyWithType("Id", typeof(long)))
            {
                entity = entities.FirstOrDefault(e => Convert.ToInt64(typeof(TEntity).GetProperty("Id", typeof(long)).GetValue(e).ToString()) == id);
            }
            return entity;
        }

        public static bool HasProperty<TEntity>(this TEntity entity, string propertyName) where TEntity : class
        {
            return typeof(TEntity).GetProperty(propertyName) != null;
        }
        public static bool HasPropertyWithType<TEntity>(this TEntity entity, string propertyName, Type propertyType) where TEntity : class
        {
            return typeof(TEntity).GetProperty(propertyName, propertyType) != null;
        }

        public static bool HasMethod<TEntity>(this TEntity entity, string methodName) where TEntity : class
        {
            return typeof(TEntity).GetMethods().Count(m => m.Name == methodName) > 0;
        }

        // This method is also used for other Map functions
        public static TResult Map<TSource, TResult>(TSource source) where TSource : class where TResult : class,new()
        {
            TResult result = new TResult();

            foreach (PropertyInfo property in typeof(TResult).GetProperties())
            {
                if (source.HasPropertyWithType(property.Name, property.PropertyType))
                {
                   property.SetValue(result, property.GetValue(source));
                }
            }
            return result;
        }

        public static IEnumerable<TResult> Map<TSource, TResult>(IEnumerable<TSource> sourceCollection) where TSource : class where TResult : class, new()
        {
            return sourceCollection.Select(source => Map<TSource,TResult>(source));
        }

        public static IQueryable<TResult> Map<TSource, TResult>(IQueryable<TSource> sourceCollection) where TSource : class where TResult : class, new()
        {
            return sourceCollection.Select(source => Map<TSource, TResult>(source));
        }

        #endregion Linq
    }
}
