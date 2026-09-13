using Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.Repositories
{
    public interface IUnitOfWork 
    {
        IBaseRepository<TKey, TEntity> Repository<TKey, TEntity>() where TEntity : BaseEntity<TKey>;
      
    }
}
