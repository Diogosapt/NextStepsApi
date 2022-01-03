using MongoDB.Driver;
using NextSteps.Adpater.Mongo.Data;
using NextSteps.Business.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NextSteps.Adpater.Mongo.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<Models.Person>
    {
        protected readonly IMongoContext _context;
        protected readonly IMongoCollection<Models.Person> _dbSet;

        public Repository(IMongoContext context)
        {
            _context = context;
            _dbSet = _context.GetCollection<Models.Person>(typeof(Models.Person).Name);
        }

        public async Task Create(Models.Person entity)
        {
            await _context.AddCommand(() => _dbSet.InsertOneAsync(entity));
        }

        public async Task<IEnumerable<Models.Person>> QueryMultipleAsync(
                    Expression<Func<Models.Person, bool>> predicate = null,
                    Func<IQueryable<Models.Person>, IOrderedQueryable<Models.Person>> orderBy = null)
        {
            return await Task.Run(() =>
            {
                IQueryable<Models.Person> query = _dbSet.AsQueryable();

                if (predicate != null) query = query.Where(predicate);
                if (orderBy != null) query = orderBy(query);

                return query.ToList();
            });
        }

        public void Delete(Guid id)
        {
            _context.AddCommand(() => _dbSet.DeleteOneAsync(Builders<Models.Person>.Filter.Eq("_id", id)));
        }

        public async Task<PagedResult<Models.Person>> QueryMultiplePagedAsync(
                int page,
                int pageSize,
                Expression<Func<Models.Person, bool>> predicate = null,
                Func<IQueryable<Models.Person>, IOrderedQueryable<Models.Person>> orderBy = null)
        {
            return await Task.Run(() =>
            {
                IQueryable<Models.Person> query = _dbSet.AsQueryable();

                if (predicate != null) query = query.Where(predicate);
                if (orderBy != null) query = orderBy(query);

                var skip = (page - 1) * pageSize;
                return new PagedResult<Models.Person>
                {
                    Total = query.Count(),
                    Page = page,
                    PageSize = pageSize,
                    Results = query
                        .Skip(skip)
                        .Take(pageSize)
                        .ToList()
                };
            });
        }

        public async Task<Models.Person> QuerySingleAsync(
            Expression<Func<Models.Person, bool>> predicate = null,
            Func<IQueryable<Models.Person>,
            IOrderedQueryable<Models.Person>> orderBy = null)
        {
            return await Task.Run(() =>
            {
                IQueryable<Models.Person> query = _dbSet.AsQueryable();

                if (predicate != null) query = query.Where(predicate);

                if (orderBy != null) query = orderBy(query);

                return query.FirstOrDefault();
            });
        }

        public async Task Update(Models.Person entity)
        {
            await _context.AddCommand(() =>
                _dbSet.FindOneAndReplaceAsync(Builders<Models.Person>.Filter.Eq("_id", entity.Id), entity));
        }
    }
}