using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Queries;
using SessionMan.DataAccess.Repository.IRepository;

namespace SessionMan.DataAccess.Handlers
{
    public class GetClientListHandler : IRequestHandler<GetClientListQuery, List<ClientRecord>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientListHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        
        public async Task<List<ClientRecord>> Handle(GetClientListQuery request, CancellationToken cancellationToken)
        {
            var clientList = await _clientRepository.GetAllClients(cancellationToken);
            var clientRecords = _mapper.Map<List<ClientRecord>>(clientList);

            return clientRecords;
        }
    }
}