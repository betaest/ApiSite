using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ApiSite {
    public static class EFExtends {
        #region Public Methods

        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source,
            string orderByProperty, bool desc) {
            var command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty,
                BindingFlags.IgnoreCase | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] {type, property.PropertyType},
                source.Expression, Expression.Quote(orderByExpression));
            return (IOrderedQueryable<TEntity>) source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        public static string Parse(this string source, object args) {
            if (string.IsNullOrEmpty(source) || args == null)
                return source;

            var props = args.GetType().GetProperties().ToDictionary(p => p.Name, p=>p.GetValue(args));
            var r = new Regex($@"\{{({string.Join("|", props.Keys)})\}}");

            return r.Replace(source, m => props[m.Groups[1].Value].ToString());
        }

        #endregion Public Methods
    }
}