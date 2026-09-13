using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MongoDB.Driver;

using System.Collections.Concurrent;


namespace Catalog.Infrustructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoDatabase _database;
        private readonly ConcurrentDictionary<string, object> _repo = new();

        public UnitOfWork(IMongoDatabase database)
        {
            _database = database;
        }

        public IBaseRepository<TKey, TEntity> Repository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;

            return (IBaseRepository<TKey, TEntity>)_repo.GetOrAdd(typeName, _ =>
                new BaseRepository<TKey, TEntity>(_database));
        }
    }
}
