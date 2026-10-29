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
		private readonly OrderingDbContext _dbContext;
		private readonly DbSet<Order> _dbSet;

		public OrderRepository(OrderingDbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<Order>();
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.SaveChangesAsync(cancellationToken);
		}
		public ValueTask DisposeAsync()
		{
			_dbContext.DisposeAsync();
			return ValueTask.CompletedTask;
		}


		public async Task AddAsync(Order item)
			=> await _dbContext.AddAsync(item);

		public async Task DeleteAsync(Order item)
		=> _dbContext.Remove(item);

		public async Task<Order> GetOrderByIdAsync(int id)
		 => (await _dbSet.FirstOrDefaultAsync(x => x.Id == id))!;

		public async Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName)
		  => await _dbSet.Where(x=>x.UserName == userName).AsNoTracking().ToListAsync();

		public async Task UpdateAsync(Order item)
		=> _dbContext.Update(item);
		
	}
}
