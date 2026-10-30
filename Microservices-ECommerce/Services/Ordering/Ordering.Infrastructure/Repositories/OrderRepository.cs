using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entityes;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Ordering.Infrastructure.Repositories
{
	public class OrderRepository : IOrderRepository, IAsyncDisposable
	{
		private readonly OrderingDbContext _dbContextFactory;
		private readonly DbSet<Order> _dbSet;

		public OrderRepository(IDbContextFactory<OrderingDbContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory.CreateDbContext();
			_dbSet = _dbContextFactory.Set<Order>();
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContextFactory.SaveChangesAsync(cancellationToken);
		}
		public ValueTask DisposeAsync()
		{
			_dbContextFactory.DisposeAsync();
			return ValueTask.CompletedTask;
		}


		public async Task AddAsync(Order item)
			=> await _dbSet.AddAsync(item);

		public async Task DeleteAsync(Order item)
		=> _dbSet.Remove(item);

		public async Task<Order> GetOrderByIdAsync(int id)
		 => (await _dbSet.FirstOrDefaultAsync(x => x.Id == id))!;

		public async Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName)
		  => await _dbSet.Where(x=>x.UserName == userName).AsNoTracking().ToListAsync();

		public async Task UpdateAsync(Order item)
		=> _dbSet.Update(item);
		
	}
}
