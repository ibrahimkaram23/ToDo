using System.Linq.Expressions;

namespace ToDo.Infrastructure.DI
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var invokedExpression = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, invokedExpression),
                left.Parameters);
        }
    }
}
