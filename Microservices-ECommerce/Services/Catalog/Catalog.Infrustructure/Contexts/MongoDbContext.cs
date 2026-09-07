using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrustructure.Contexts
{
	public class MongoDbContext
	{
		private readonly IMongoDatabase _database;

		public MongoDbContext(IMongoDatabase database)
		{
			_database = database;
		}

		public IMongoCollection<T> GetCollection<T>(string collectionName)
		{
			return _database.GetCollection<T>(collectionName);
		}
	}
}
