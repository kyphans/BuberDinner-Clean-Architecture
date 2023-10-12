using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Interfaces.Persistence.Generic;

namespace BuberDinner.Application.Commands.GeneralCommands
{
    // Request
    public class Remove<TEntity> : IRequest<int>
    {
        public TEntity Data { get; }

        public Remove(TEntity data)
        {
            Data = data;
        }
    }

    // Handler
    public class RemoveHandler<TEntity> : IRequestHandler<Remove<TEntity>, int>
    {
        private readonly IGeneralRepository<TEntity> _generalRepository;

        public RemoveHandler(IGeneralRepository<TEntity> generalRepository)
        {
            _generalRepository = generalRepository;
        }
        public async Task<int> Handle(Remove<TEntity> request, CancellationToken cancellationToken)
        {
            var result = await _generalRepository.Remove(request.Data);
            return result;
        }
    }
}