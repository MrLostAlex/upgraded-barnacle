using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Queries;
using SessionMan.DataAccess.Repository.IRepository;

namespace SessionMan.DataAccess.Handlers
{
    public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, ActionResult<ClientRecord>>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByIdHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        
        public async Task<ActionResult<ClientRecord>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetClientById(request.Id, cancellationToken);

            return result.Value == null ? new NotFoundResult() : result;
        }
    }
}