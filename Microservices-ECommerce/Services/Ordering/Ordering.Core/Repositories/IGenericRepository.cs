using Ordering.Core.Entityes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Repositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task AddAsync(T item);
		Task UpdateAsync(T item);
		Task DeleteAsync(T item);
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
