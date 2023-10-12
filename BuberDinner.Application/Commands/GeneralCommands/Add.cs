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
    public class Add<TEntity> : IRequest<int>
    {
        public TEntity Data { get; }

        public Add(TEntity data)
        {
            Data = data;
        }
    }

    // Handler
    public class AddHandler<TEntity> : IRequestHandler<Add<TEntity>, int>
    {
        private readonly IGeneralRepository<TEntity> _generalRepository;

        public AddHandler(IGeneralRepository<TEntity> generalRepository)
        {
            _generalRepository = generalRepository;
        }
        public async Task<int> Handle(Add<TEntity> request, CancellationToken cancellationToken)
        {
            var result = await _generalRepository.Add(request.Data);
            return result;
        }
    }
}
