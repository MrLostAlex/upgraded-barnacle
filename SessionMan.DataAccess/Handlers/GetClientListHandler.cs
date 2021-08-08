using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Queries;
using SessionMan.DataAccess.Repository.IRepository;

namespace SessionMan.DataAccess.Handlers
{
    public class GetClientListHandler : IRequestHandler<GetClientListQuery, ActionResult<List<ClientRecord>>>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientListHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        
        public async Task<ActionResult<List<ClientRecord>>> Handle(GetClientListQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetAllClients(cancellationToken);

            return result.Value.Count > 0 ? result : new NotFoundResult();
        }
    }
}