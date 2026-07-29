using System.Linq.Expressions;

namespace Domain.Common
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; protected set; } = _ => true;

        public List<Expression<Func<T, object>>> Includes { get; } = [];

        public List<string> IncludePaths { get; } = [];

        protected Expression<Func<T, bool>> And(Expression<Func<T, bool>> other)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var body = Expression.Invoke(
                Expression.AndAlso(
                    Expression.Invoke(Criteria, param),
                    Expression.Invoke(other, param)
                ),
                param
            );

            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        protected Expression<Func<T, bool>> Or(Expression<Func<T, bool>> other)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var body = Expression.Invoke(
                Expression.OrElse(
                    Expression.Invoke(Criteria, param),
                    Expression.Invoke(other, param)
                ),
                param
            );

            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        protected void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }

        protected void AddInclude(string path)
        {
            IncludePaths.Add(path);
        }
    }
}
