using System.Linq.Expressions;

namespace Domain.Common
{
    public static class Predicate
    {
        public static Expression<Func<TRoot, bool>> For<TRoot, TMember>(
            Expression<Func<TRoot, TMember>> member,
            Expression<Func<TMember, bool>> predicate)
        {
            var body = new ReplaceParameterVisitor(
                    predicate.Parameters[0],
                    member.Body)
                .Visit(predicate.Body)!;

            return Expression.Lambda<Func<TRoot, bool>>(
                body,
                member.Parameters[0]);
        }

        public static Expression<Func<T, bool>> Or<T>(
            Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var leftBody = new ReplaceParameterVisitor(left.Parameters[0], parameter)
                .Visit(left.Body)!;
            var rightBody = new ReplaceParameterVisitor(right.Parameters[0], parameter)
                .Visit(right.Body)!;

            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(leftBody, rightBody),
                parameter);
        }

        private sealed class ReplaceParameterVisitor(
            ParameterExpression source,
            Expression replacement) : ExpressionVisitor
        {
            protected override Expression VisitParameter(ParameterExpression node)
                => node == source ? replacement : base.VisitParameter(node);
        }
    }
}
