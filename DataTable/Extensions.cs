using SVN.AspNet.DataTransferObjects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SVN.AspNet.Logic
{
    public static class Extensions
    {
        public static IQueryable<T> CallExpression<T>(this IQueryable<T> entity, string methodName, string propertyName)
        {
            var seed = Expression.Parameter(typeof(T), "x");
            var body = propertyName.Split('.').Aggregate<string, Expression>(seed, Expression.PropertyOrField);
            var typeArguments = new[] { typeof(T), body.Type };
            var lambda = Expression.Lambda(body, seed);

            return (IQueryable<T>)entity.Provider.CreateQuery(Expression.Call(typeof(Queryable), methodName, typeArguments, entity.Expression, lambda));
        }

        //public static IQueryable<T> FilterByValue<T>(this IQueryable<T> entity, Expression<Func<T, string>> propertySelector, string propertyValue)
        //{
        //    Expression<Func<T, string, bool>> expression =
        //        (ex, value) => SqlFunctions.PatIndex(propertyValue.Trim(),
        //            value.Trim()) > 0;

        //    var newSelector = propertySelector.Body.Replace(propertySelector.Parameters[0], expression.Parameters[0]);
        //    var body = expression.Body.Replace(expression.Parameters[1], newSelector);
        //    var lambda = Expression.Lambda<Func<T, bool>>(body, expression.Parameters[0]);

        //    return entity.Where(lambda);
        //}

        public static IQueryable<T> ApplyDataTableParameters<T>(this IQueryable<T> entity, DataTableParameters parameters)
        {
            parameters.search.value = (parameters.search.value ?? string.Empty).Trim();

            var order = parameters.order[0];
            var column = parameters.columns[order.column];
            var propertyName = column.data;
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
            var property = typeof(T).GetProperty(propertyName, bindingFlags);
            propertyName = property.Name;

            if (order.asc)
            {
                entity = entity.CallExpression("OrderBy", propertyName);
            }
            if (order.desc)
            {
                entity = entity.CallExpression("OrderByDescending", propertyName);
            }

            //entity = entity.Where(x => parameters.search.value == string.Empty || property.GetValue(x, null).ToString().Contains(parameters.search.value));
            entity = entity.Skip(parameters.index * parameters.length).Take(parameters.length);

            return entity;
        }
    }
}