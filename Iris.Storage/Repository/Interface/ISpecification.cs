using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace Iris.Commons.Storage.Repository.Interface
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }
        Expression<Func<T, object>> Join { get; }
        Expression<Func<T, object>> GroupJoin { get; }
        Expression<Func<IGrouping<object, T>, T>> GroupBySelect { get; }
        Action<IQueryable<T>> CustomInclude { get; set; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
        bool IsProcedureEnabled { get; }
        List<SqlParameter> SqlParameters { get; }
        bool IsCustomsPagingEnabled { get; set; }
        bool IsDistinctEnabled { get; }
        IEqualityComparer<T> EqualityComparerDistinct { get; }
    }
}