using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure.Data
{
	public class OrderingDbContextFactory:IDesignTimeDbContextFactory<OrderingDbContext>
	{
		public OrderingDbContext CreateDbContext(string[] args)
		{

			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.Build();


			var optionsBuilder =
				new DbContextOptionsBuilder<OrderingDbContext>();

			optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

			return new OrderingDbContext(optionsBuilder.Options);
		}
	}
}
