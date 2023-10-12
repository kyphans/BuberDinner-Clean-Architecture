using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Interfaces.Persistence.Generic
{
    public interface IGeneralRepository<TEntity>
    {
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false);

        Task<int> Add(TEntity data);

        Task<int> Update(TEntity data);

        Task<int> Remove(TEntity data);

        Task<int> AddRange(IEnumerable<TEntity> data);

        Task<int> UpdateRange(IEnumerable<TEntity> data);

        Task<int> RemoveRange(IEnumerable<TEntity> data);
    }
}
