using NextSteps.Adpater.Mongo.Models;

namespace NextSteps.Adpater.Mongo.Infrastructure.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : class, IEntity;
    }
}