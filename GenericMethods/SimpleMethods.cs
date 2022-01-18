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

        // IConvertible is used for ordering. It implements such as string, int, double, decimal cases.
        // Note that 'struct' couldn't be used for constraint because it doesn't provide 'string' data type(which is most likely could be used).
        public static IOrderedEnumerable<TSource> GetFilteredListByOrder<TSource, TKey>(this IEnumerable<TSource> mainCollection, Func<TSource, bool> predicate, Func<TSource, TKey> key) where TSource : class where TKey : IConvertible
        {
            return mainCollection.Where(predicate).OrderBy(key);
        }
        public static IOrderedEnumerable<TSource> GetFilteredListByReverseOrder<TSource, TKey>(this IEnumerable<TSource> mainCollection, Func<TSource, bool> predicate, Func<TSource, TKey> key) where TSource : class where TKey : IConvertible
        {
            return mainCollection.Where(predicate).OrderByDescending(key);
        }

        public static IOrderedQueryable<TSource> GetFilteredListByOrder<TSource, TKey>(this IQueryable<TSource> mainCollection, Expression<Func<TSource, bool>> predicate, Expression<Func<TSource, TKey>> key) where TSource : class where TKey : IConvertible
        {
            return mainCollection.Where(predicate).OrderBy(key);
        }
        public static IOrderedQueryable<TSource> GetFilteredListByReverseOrder<TSource, TKey>(this IQueryable<TSource> mainCollection, Expression<Func<TSource, bool>> predicate, Expression<Func<TSource, TKey>> key) where TSource : class where TKey : IConvertible
        {
            return mainCollection.Where(predicate).OrderByDescending(key);
        }
        #endregion Linq

        #region GetEntityById

        // This method directly pass an Id as a parameter and find a spesific entity by Id, apart from type of entity without any need for predicate.
        // If your 'Id' column is not named exactly 'Id', you could configure it by the help of "idPropertyName" parameter.Example: 'Cars.GetEntityById(id,"CarId");'
        // If named 'Id' already, just use it like 'Cars.GetEntityById(id);'
        public static TEntity GetEntityById<TEntity>(this IQueryable<TEntity> entities, Guid id, string idPropertyName = null) where TEntity : class
        {
            var entity = default(TEntity);
            string idProperty = idPropertyName ?? "Id";

            if (entity.HasPropertyWithType(idProperty, typeof(Guid)))
            {
                entity = entities.FirstOrDefault(e => Guid.Parse(typeof(TEntity).GetProperty(idProperty, typeof(Guid)).GetValue(e).ToString()) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IEnumerable<TEntity> entities, Guid id, string idPropertyName = null) where TEntity : class
        {
            var entity = default(TEntity);
            string idProperty = idPropertyName ?? "Id";

            if (entity.HasPropertyWithType(idProperty, typeof(Guid)))
            {
                entity = entities.FirstOrDefault(e => Guid.Parse(typeof(TEntity).GetProperty(idProperty, typeof(Guid)).GetValue(e).ToString()) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IQueryable<TEntity> entities, int id, string idPropertyName = null) where TEntity : class
        {
            var entity = default(TEntity);
            string idProperty = idPropertyName ?? "Id";

            if (entity.HasPropertyWithType(idProperty, typeof(int)))
            {
                entity = entities.FirstOrDefault(e => Convert.ToInt32(typeof(TEntity).GetProperty(idProperty, typeof(int)).GetValue(e).ToString()) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IEnumerable<TEntity> entities, int id, string idPropertyName = null) where TEntity : class
        {
            var entity = default(TEntity);
            string idProperty = idPropertyName ?? "Id";

            if (entity.HasPropertyWithType(idProperty, typeof(int)))
            {
                entity = entities.FirstOrDefault(e => Convert.ToInt32(typeof(TEntity).GetProperty(idProperty, typeof(int)).GetValue(e).ToString()) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IQueryable<TEntity> entities, long id, string idPropertyName = null) where TEntity : class
        {
            var entity = default(TEntity);
            string idProperty = idPropertyName ?? "Id";

            if (entity.HasPropertyWithType(idProperty, typeof(long)))
            {
                entity = entities.FirstOrDefault(e => Convert.ToInt64(typeof(TEntity).GetProperty(idProperty, typeof(long)).GetValue(e).ToString()) == id);
            }
            return entity;
        }

        public static TEntity GetEntityById<TEntity>(this IEnumerable<TEntity> entities, long id, string idPropertyName = null) where TEntity : class
        {
            var entity = default(TEntity);
            string idProperty = idPropertyName ?? "Id";

            if (entity.HasPropertyWithType(idProperty, typeof(long)))
            {
                entity = entities.FirstOrDefault(e => Convert.ToInt64(typeof(TEntity).GetProperty(idProperty, typeof(long)).GetValue(e).ToString()) == id);
            }
            return entity;
        }
        #endregion GetEntityById

        #region HasProperty

        // Determines whether an entity has the specified property
        public static bool HasProperty<TEntity>(this TEntity entity, string propertyName) where TEntity : class
        {
            return typeof(TEntity).GetProperty(propertyName) != null;
        }
        // Determines whether an entity has the specified property with the specified type
        public static bool HasPropertyWithType<TEntity>(this TEntity entity, string propertyName, Type propertyType) where TEntity : class
        {
            return typeof(TEntity).GetProperty(propertyName, propertyType) != null;
        }
        // Determines whether an entity has the specified method, 'count' is used for avoiding exception caused by multiple methods with the same name (overloads)
        public static bool HasMethod<TEntity>(this TEntity entity, string methodName) where TEntity : class
        {
            return typeof(TEntity).GetMethods().Count(m => m.Name == methodName) > 0;
        }

        #endregion HasProperty

        #region Mapping for DTO

        // This methods could be used for DTO usage. 
        // First overload of method is also used in other overloads.

        // One to one entity mapping
        public static TResult Map<TSource, TResult>(TSource source) where TSource : class where TResult : class, new()
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
        // Many to many entity mapping, returns an IEnumerable collection of targeted DTO
        public static IEnumerable<TResult> Map<TSource, TResult>(IEnumerable<TSource> sourceCollection) where TSource : class where TResult : class, new()
        {
            return sourceCollection.Select(source => Map<TSource, TResult>(source));
        }
        // Many to many entity mapping, returns an IQueryable collection of targeted DTO
        public static IQueryable<TResult> Map<TSource, TResult>(IQueryable<TSource> sourceCollection) where TSource : class where TResult : class, new()
        {
            return sourceCollection.Select(source => Map<TSource, TResult>(source));
        }
        #endregion Mapping for DTO

    }
}
