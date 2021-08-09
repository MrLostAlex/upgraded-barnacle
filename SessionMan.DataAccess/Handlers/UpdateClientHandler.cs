using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SessionMan.DataAccess.Commands;
using SessionMan.DataAccess.DataTransfer;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Repository.IRepository;

namespace SessionMan.DataAccess.Handlers
{
    public class UpdateClientHandler : IRequestHandler<UpdateClientCommand, ClientUpsertOutput>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public UpdateClientHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        
        public async Task<ClientUpsertOutput> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            Client client = await _clientRepository.IsClientExisting(request.ClientId, cancellationToken);
            if (client == null) return new ClientUpsertOutput(){ ErrorBaseRecord = new ErrorBaseRecord()
            {
                Title = "Update Failed",
                ErrorMessage = $"Cannot update client with Id: {request.ClientId}.",
                StatusCode = StatusCodes.Status400BadRequest
            }};
            Client clientToUpdate = _mapper.Map(request.ClientUpdateInput, client);
            clientToUpdate.UpdatedBy = request.ClientUpdateInput.UpdaterId == default ? "System" : request.ClientUpdateInput.UpdaterId.ToString();
            clientToUpdate.UpdateTime = DateTimeOffset.UtcNow;
            Client updatedClient = await _clientRepository.UpdateClient(clientToUpdate, cancellationToken);
            return _mapper.Map<ClientUpsertOutput>(updatedClient);
        }
    }
}