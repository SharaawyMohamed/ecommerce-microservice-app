using Catalog.Application.Features.Product.Commands.UpdateProduct;
using Catalog.Application.Features.Product.Queries.GetAllProducts;
using Catalog.Application.Features.Product.Queries.GetProductById;
using Catalog.Application.Features.Product.Queries.GetProductByName;
using Catalog.Application.Features.Product.Queries.GetProductsByType;
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
			config.NewConfig<UpdateProductCommand, Product>();
			config.NewConfig<Product, GetAllProductsQuery>();
			config.NewConfig<Product,GetProductByIdQuery>();
			config.NewConfig<Product,GetProductByNameQuery>();
			config.NewConfig<Product,GetProductsByTypeQuery>();
		}
	}
}
