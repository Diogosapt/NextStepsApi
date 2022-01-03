namespace NextSteps.Adpater.SQL.Infrastructure.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>() where T : class;
    }
}