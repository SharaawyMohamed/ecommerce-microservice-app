using Ordering.Core.Entityes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Repositories
{
	public interface IOrderRepository: IGenericRepository<Order>
	{
		Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName);
		Task<Order> GetOrderByIdAsync(int id);
	}
}
