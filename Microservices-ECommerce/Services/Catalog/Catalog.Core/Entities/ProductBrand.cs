using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.Entities
{
	public class ProductBrand:BaseEntity<string>
	{
		public string Name { get; set; }

	}
}
