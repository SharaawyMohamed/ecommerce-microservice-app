using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entityes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure.Data
{
	public class OrderingDbContext:DbContext
	{
		public OrderingDbContext(DbContextOptions<OrderingDbContext> options) : base(options)
		{
			
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<Order> Orders { get; set; }
	}
}
