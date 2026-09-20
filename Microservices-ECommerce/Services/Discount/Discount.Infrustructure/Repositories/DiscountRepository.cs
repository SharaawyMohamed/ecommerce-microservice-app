using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace Discount.Infrustructure.Repositories
{
	public class DiscountRepository : IDiscountRepository
	{
		private readonly string _connectionString;
		public DiscountRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		private IDbConnection CreateConnection()
		{
			return new NpgsqlConnection(_connectionString);
		}

		public async Task<Coupon> CreateDiscountAsync(Coupon coupon)
		{
			using var connection = CreateConnection();

			return (await connection.QueryFirstOrDefaultAsync<Coupon>(
				"INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount) RETURNING *",
				new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount }))!;
		}

		public async Task<bool> DeleteDiscountAsync(string productName)
		{
			using var connection = CreateConnection();

			return await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName }) > 0;
		}

		public async Task<Coupon> GetDiscountAsync(string productName)
		{
			using var connection = CreateConnection();

			return (await connection.QueryFirstOrDefaultAsync<Coupon>(
				"SELECT * FROM Coupon WHERE ProductName = @productName", new { ProductName = productName }))!;
		}

		public async Task<bool> UpdateDiscountAsync(Coupon coupon)
		{
			using var connection = CreateConnection();

			return (await connection.ExecuteAsync(
				"UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
				new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id })) > 0;
		}
	}
}
