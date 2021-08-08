using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.Commands;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Repository.IRepository;

namespace SessionMan.DataAccess.Handlers
{
    public class CreateClientHandler : IRequestHandler<CreateClientCommand, ActionResult<ClientUpsertOutput>>
    {
        private readonly IClientRepository _clientRepository;

        public CreateClientHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        
        public async Task<ActionResult<ClientUpsertOutput>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.CreateClient(request.ClientCreateInput, cancellationToken);
            return result;
        }
    }
}