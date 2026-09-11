using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.Entities
{
	public class ProductType : BaseEntity<string>
	{
		public string Name { get; set; }
	}
}
