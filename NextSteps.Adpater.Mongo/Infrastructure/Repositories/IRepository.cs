using NextSteps.Business.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NextSteps.Adpater.Mongo.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task Create(T entity);

        Task Update(T entity);

        void Delete(Guid id);

        Task<T> QuerySingleAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null);

        Task<IEnumerable<T>> QueryMultipleAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        Task<PagedResult<T>> QueryMultiplePagedAsync(
            int page,
            int pageSize,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    }
}