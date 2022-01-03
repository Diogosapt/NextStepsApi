using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NextSteps.Adpater.Mongo.Data
{
    public interface IMongoContext : IDisposable
    {
        Task AddCommand(Func<Task> func);

        Task<int> SaveChanges(CancellationToken cancellationToken = default);

        IMongoCollection<T> GetCollection<T>(string name);
    }

    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }

        public MongoClient MongoClient { get; set; }

        private readonly List<Func<Task>> _commands;

        public MongoContext(IOptions<DataBaseMongo> dataBaseMongo)
        {
            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();

            RegisterConventions();

            MongoClient = new MongoClient(dataBaseMongo.Value.ConnectionString);
            Database = MongoClient.GetDatabase(dataBaseMongo.Value.Collection);
        }

        public IClientSessionHandle Session { get; set; }

        private static void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true),
            };

            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public async Task AddCommand(Func<Task> func)
        {
            await Task.Run(() => _commands.Add(func));
        }

        public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
        {
            var commandTasks = _commands.Select(c => c());
            await Task.WhenAll(commandTasks);
            return _commands.Count;
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}