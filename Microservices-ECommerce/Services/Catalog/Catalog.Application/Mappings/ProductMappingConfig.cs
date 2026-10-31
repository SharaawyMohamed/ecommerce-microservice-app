using Catalog.Application.Features.Product.Commands.UpdateProduct;
using Catalog.Application.Features.Product.Queries.GetAllProducts;
using Catalog.Application.Features.Product.Queries.GetProductById;
using Catalog.Core.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Mappings
{
	public class ProductMappingConfig : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			// Domain to Response mappings
			config.NewConfig<Product, GetProductByIdResponse>();
		}
	}
}
