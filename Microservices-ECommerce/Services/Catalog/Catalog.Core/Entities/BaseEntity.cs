using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.Entities
{
	public class BaseEntity<T>
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public T id { get; set; }
	}
}
