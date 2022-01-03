using Microsoft.EntityFrameworkCore.Query;
using NextSteps.Business.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace NextSteps.Adpater.SQL
{
    public interface IRepository<T>
    {
        Task Create(T entity, CancellationToken cancellationToken = default);

        Task CreateRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        void Delete(object id);

        void DeleteRange(IEnumerable<T> entities);

        Task<int> CountAsync(
           Expression<Func<T, bool>> predicate = null,
           CancellationToken cancellationToken = default);

        Task<T> QuerySingleAsync(
           Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> QueryMultipleAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            CancellationToken cancellationToken = default);

        Task<PagedResult<T>> QueryMultiplePagedAsync(
            int page,
            int pageSize,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            CancellationToken cancellationToken = default);
    }
}