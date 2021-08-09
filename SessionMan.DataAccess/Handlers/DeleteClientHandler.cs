using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SessionMan.DataAccess.Commands;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Repository.IRepository;
using SessionMan.Shared.Helpers;

namespace SessionMan.DataAccess.Handlers
{
    public class DeleteClientHandler : IRequestHandler<DeleteClientCommand, Task>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<DeleteClientHandler> _logger;

        public DeleteClientHandler(IClientRepository clientRepository, IMapper mapper, ILogger<DeleteClientHandler> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }
        
        public async Task<Task> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            Client client = await _clientRepository.IsClientExisting(request.ClientId, cancellationToken);
            if (client == null)
                throw new InvalidDataStateException("Delete Failed", $"Unable to delete client. Client Id {request.ClientId} not found.");
            return _clientRepository.DeleteClient(client.Id, cancellationToken);
        }
    }
}