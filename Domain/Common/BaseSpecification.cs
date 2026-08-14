using System.Linq.Expressions;

namespace Domain.Common
{

/*
Las Specifications se utilizan para representar un escenario específico de consulta.

Su objetivo es mantener el repositorio genérico y encapsular la lógica de:
- Filtros (Where)
- Includes
- Ordenamientos
- Paginación
- Consultas diferentes según el contexto (Público, Admin, Reportes, etc.)

El mismo repositorio puede ejecutar distintas consultas simplemente recibiendo una Specification diferente.

Métodos específicos en el repositorio solo cuando la consulta no cabe en filtro + include
(proyecciones, SelectMany, joins especiales).
*/
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; protected set; } = _ => true;

        public List<Expression<Func<T, object>>> Includes { get; } = [];

        public List<string> IncludePaths { get; } = [];

        protected Expression<Func<T, bool>> And(Expression<Func<T, bool>> other)
            => Combine(Criteria, other, Expression.AndAlso);

        protected Expression<Func<T, bool>> Or(Expression<Func<T, bool>> other)
            => Combine(Criteria, other, Expression.OrElse);

        private static Expression<Func<T, bool>> Combine(
            Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right,
            Func<Expression, Expression, BinaryExpression> merge)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var leftBody = new ReplaceParameterVisitor(left.Parameters[0], parameter)
                .Visit(left.Body)!;
            var rightBody = new ReplaceParameterVisitor(right.Parameters[0], parameter)
                .Visit(right.Body)!;

            return Expression.Lambda<Func<T, bool>>(
                merge(leftBody, rightBody),
                parameter);
        }

        protected void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }

        protected void AddInclude(string path)
        {
            IncludePaths.Add(path);
        }

        private sealed class ReplaceParameterVisitor(
            ParameterExpression source,
            ParameterExpression target) : ExpressionVisitor
        {
            protected override Expression VisitParameter(ParameterExpression node)
                => node == source ? target : base.VisitParameter(node);
        }
    }
}
