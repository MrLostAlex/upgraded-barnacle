using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Queries;
using SessionMan.DataAccess.Repository.IRepository;

namespace SessionMan.DataAccess.Handlers
{
    public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, ClientRecord>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientByIdHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        
        public async Task<ClientRecord> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            Client client = await _clientRepository.GetClientById(request.Id, cancellationToken);
            var clientRecord = _mapper.Map<ClientRecord>(client);
            return clientRecord;
        }
    }
}