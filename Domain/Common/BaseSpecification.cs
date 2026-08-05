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

No crear Specifications para consultas simples como:
- GetByIdAsync
- ExistsAsync
- GetByEmailAsync

En esos casos es preferible un método directo en el repositorio.
*/
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
