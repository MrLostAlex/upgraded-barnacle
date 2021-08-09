using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SessionMan.DataAccess.Commands;
using SessionMan.DataAccess.DataTransfer;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Repository.IRepository;
using SessionMan.Shared.Helpers;

namespace SessionMan.DataAccess.Handlers
{
    public class UpdateClientHandler : IRequestHandler<UpdateClientCommand, ClientUpsertOutput>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateClientHandler> _logger;

        public UpdateClientHandler(IClientRepository clientRepository, IMapper mapper, ILogger<UpdateClientHandler> logger)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<ClientUpsertOutput> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(Handle)}.");
                (Guid clientId, ClientUpdateInput clientUpdateInput) = request;
                Client client = await _clientRepository.IsClientExisting(clientId, cancellationToken);
                if (client == null) throw new BadRequestException("Update Failed", $"Cannot update client with Id: {clientId}.");
                Client clientToUpdate = _mapper.Map(clientUpdateInput, client);
                clientToUpdate.UpdatedBy = clientUpdateInput.UpdaterId == default ? "System" : clientUpdateInput.UpdaterId.ToString();
                clientToUpdate.UpdateTime = DateTimeOffset.UtcNow;
                Client updatedClient = await _clientRepository.UpdateClient(clientToUpdate, cancellationToken);
                return _mapper.Map<ClientUpsertOutput>(updatedClient);
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(Handle)}.");
            }
        }
    }
}