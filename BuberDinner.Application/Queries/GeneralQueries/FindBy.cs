using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Interfaces.Persistence.Generic;
using MediatR;

namespace BuberDinner.Application.Queries.GeneralQueries
{
    public class FindBy<TEntity> : IRequest<IQueryable<TEntity>>
    {
        public Expression<Func<TEntity, bool>> Predicate { get; }
        public bool AsNoTracking { get; }

        public FindBy(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false)
        {
            Predicate = predicate;
            AsNoTracking = asNoTracking;
        }
    }

    public class FindByHandler<TEntity> : IRequestHandler<FindBy<TEntity>, IQueryable<TEntity>>
    {
        private readonly IGeneralRepository<TEntity> _generalRepository;

        public FindByHandler(IGeneralRepository<TEntity> generalRepository)
        {
            _generalRepository = generalRepository;
        }

        public Task<IQueryable<TEntity>> Handle(FindBy<TEntity> request, CancellationToken cancellationToken)
        {
            var result = _generalRepository.FindBy(request.Predicate, request.AsNoTracking);
            return Task.FromResult(result);
        }
    }
}
