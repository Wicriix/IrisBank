using Iris.Commons.Storage.Repository.Interface;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace Iris.Commons.Storage.Repository
{
    public abstract class BaseSpecification<T>: ISpecification<T>
    {
        #nullable disable
        protected BaseSpecification()
        {
            IsProcedureEnabled = true;
            SqlParameters = new List<SqlParameter>();
        }

        protected BaseSpecification(SqlParameter codigoRespuesta, SqlParameter mensaje)
        {
            IsProcedureEnabled = true;
            SqlParameters = new List<SqlParameter>
            {
                codigoRespuesta,
                mensaje
            };
        }

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; private set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }
        public Expression<Func<T, object>> Join { get; private set; }
        public Expression<Func<T, object, object>> ResultSelector { get; private set; }
        public Expression<Func<T, object>> GroupJoin { get; private set; }
        public Expression<Func<IGrouping<object, T>, T>> GroupBySelect { get; private set; }
        public Expression<Func<T, object>> Select { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        public bool IsProcedureEnabled { get; private set; }
        public Action<IQueryable<T>> CustomInclude { get; set; }
        public List<SqlParameter> SqlParameters { get; private set; }
        public bool IsCustomsPagingEnabled { get; set; }
        public bool IsDistinctEnabled { get; private set; } = false;
        public IEqualityComparer<T> EqualityComparerDistinct { get; private set; }

        protected virtual void AddSelect(Expression<Func<T, object>> select)
        {
            Select = select;
        }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected virtual void AddCustomeInclude(Action<IQueryable<T>> customInclude)
        {
            CustomInclude = customInclude;
            IsCustomsPagingEnabled = true;
        }

        protected virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected virtual void take(int take)
        {
            Skip = 1;
            Take = take;
            IsPagingEnabled = true;
        }

        protected virtual void IsProcedure()
        {
            IsProcedureEnabled = true;
        }

        protected virtual void ApplyStoredProcedureParameters(List<SqlParameter> sqlParameters)
        {
            if (!(sqlParameters is null) && sqlParameters.Count >0)
            {
                SqlParameters.AddRange(sqlParameters);
            }
            IsProcedureEnabled = true;  
        }
        protected virtual void ApplyDistinct(IEqualityComparer<T> equalityComparer = null)
        {
            EqualityComparerDistinct=equalityComparer;
            IsDistinctEnabled = true;
        }

        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected virtual void ApplyJoin<Z>(BaseSpecification<Z> specification
            , Expression<Func<T, object>> JoinExpression)
        {
            Join = JoinExpression;
        }

        protected virtual void ApplyGroupJoin<Z>(Expression<Func<T, object>> groupJoinExpression)
        {
            GroupJoin = groupJoinExpression;
        }

        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected virtual void ApplyGroupBy<Z>(Expression<Func<T,Z>> groupByExpression) where Z : class
        {
            GroupBy = groupByExpression as Expression<Func<T, object>>;
        }

        protected virtual void ApplySelectGroupBy<Z>(Expression<Func<IGrouping<Z,T>,T>> groupJoinSelectExpression) where Z: class
        {
            GroupBySelect = groupJoinSelectExpression as Expression<Func<IGrouping<object, T>, T>>;
        }
    }
}