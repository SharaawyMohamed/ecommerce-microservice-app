using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MongoDB.Driver;


namespace Catalog.Infrustructure.Repositories
{
    public class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        private readonly IMongoCollection<TEntity> _collection;

        public BaseRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.id, id);

            return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var filter = Builders<TEntity>.Filter.Empty;

            return await _collection.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
                await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(TKey id, TEntity entity, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.id, id);
                await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.id, id);
                await _collection.DeleteOneAsync(filter, cancellationToken: cancellationToken);
        }
    }
}
