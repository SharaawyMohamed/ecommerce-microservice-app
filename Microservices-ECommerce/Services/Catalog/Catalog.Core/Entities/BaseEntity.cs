using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.Entities
{
	public class BaseEntity
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string id { get; set; }
	}
}
