using NextSteps.Adpater.Mongo.Data;
using NextSteps.Adpater.Mongo.Infrastructure.Repositories;
using NextSteps.Adpater.Mongo.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextSteps.Adpater.Mongo.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);

        IRepository<T> GetRepository<T>() where T : class, IEntity;
    }

    public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : IMongoContext
    {
        TContext Context { get; }
    }
}