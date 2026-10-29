using Discount.Application.Protos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.GrpcServices
{
	public class GrpcDiscountService
	{
		private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

		public GrpcDiscountService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
		{
			_discountProtoServiceClient = discountProtoServiceClient;
		}

		public async Task<GetDiscountResponse> GetDiscount(string productName)
		{
			var request = new GetDiscountQuery { ProductName = productName };
			var response = await _discountProtoServiceClient.GetDiscountAsync(request);
			return response;
		}

		public async Task<CreateDiscountResponse> CreateDiscount(string productName, int amount, string description)
		{
			var request = new CreateDiscountRequest
			{
				ProductName = productName,
				Amount = amount,
				Description = description
			};
			var response = await _discountProtoServiceClient.CreateDiscountAsync(request);
			return response;
		}
	 
		public async Task<UpdateDiscountResponse> UpdateDiscount(int id, string productName, int amount, string description)
		{
			var request = new UpdateDiscountRequest
			{
				Id = id,
				ProductName = productName,
				Amount = amount,
				Description = description
			};
			var response = await _discountProtoServiceClient.UpdateDiscountAsync(request);
			return response;
		}

		public async Task<DeleteDiscountResponse> DeleteDiscount(string productname)
		{
			var request = new DeleteDiscountRequest { ProductName = productname };
			var response = await _discountProtoServiceClient.DeleteDiscountAsync(request);
			return response;
		}
	}

}
