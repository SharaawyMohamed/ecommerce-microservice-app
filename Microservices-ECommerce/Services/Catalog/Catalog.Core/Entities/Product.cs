using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Catalog.Core.Entities
{
	public class Product:BaseEntity<string>
	{
		public string Name { get; set; } = null!;

		public string Description { get; set; } = null!;

		public decimal Price { get; set; }

		public string ImageFile { get; set; } = null!;

		public string Summary { get; set; } = null!;

		[JsonPropertyName("brands")]
		public ProductBrand Brand { get; set; }

		[JsonPropertyName("types")]
		public ProductType Type { get; set; }

	}
}
