using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrustructure.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrustructure.Repositories
{
    public class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        private readonly IMongoCollection<TEntity> _collection;
        public BaseRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<TEntity>(nameof(TEntity));
        }
        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
