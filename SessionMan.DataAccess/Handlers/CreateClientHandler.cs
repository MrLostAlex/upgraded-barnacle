using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.Commands;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Repository.IRepository;

namespace SessionMan.DataAccess.Handlers
{
    public class CreateClientHandler : IRequestHandler<CreateClientCommand, ClientUpsertOutput>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public CreateClientHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        
        public async Task<ClientUpsertOutput> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var client = _mapper.Map<Client>(request.ClientCreateInput);
            client.CreatedBy = request.ClientCreateInput.CreatorId == default ? "System" : request.ClientCreateInput.CreatorId.ToString();
            client.CreatedTime = DateTimeOffset.UtcNow;
            client.UpdatedBy = request.ClientCreateInput.CreatorId == default ? "System" : request.ClientCreateInput.CreatorId.ToString();
            client.UpdateTime = client.CreatedTime;
            Client createdClient = await _clientRepository.CreateClient(client, cancellationToken);
            var createdClientOutput = _mapper.Map<ClientUpsertOutput>(createdClient);
            return createdClientOutput;
        }
    }
}