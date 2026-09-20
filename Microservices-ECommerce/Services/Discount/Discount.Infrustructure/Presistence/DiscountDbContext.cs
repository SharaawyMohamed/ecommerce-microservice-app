using Discount.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Infrustructure.Presistence
{
	public class DiscountDbContext:DbContext
	{
		public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
		{
		}

		public DbSet<Coupon> Coupons => Set<Coupon>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Coupon>(entity =>
			{
				entity.ToTable("coupons");
				entity.HasKey(p => p.Id);
				entity.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
				entity.Property(p=>p.Description).HasMaxLength(500);
				entity.Property(p => p.Amount).HasColumnType("numeric(18,2)");
			});
		}
	}
}
