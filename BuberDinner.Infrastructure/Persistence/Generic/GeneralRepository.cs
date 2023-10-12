using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Interfaces.Persistence.Generic;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistence.Generic
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity>
    {
        private static readonly List<TEntity> storeRepository = new();

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false)
        {
            if (asNoTracking)
            {
                return storeRepository.AsQueryable().Where(predicate);
            }

            return storeRepository.AsQueryable().Where(predicate);
        }

        public async Task<int> Add(TEntity data)
        {
            storeRepository.Add(data);
            return await Task.FromResult(1);
        }

        public Task<int> Update(TEntity data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Remove(TEntity data)
        {
            storeRepository.Remove(data);
            return await Task.FromResult(1);
        }

        public Task<int> AddRange(IEnumerable<TEntity> data)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRange(IEnumerable<TEntity> data)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveRange(IEnumerable<TEntity> data)
        {
            throw new NotImplementedException();
        }
    }
}
