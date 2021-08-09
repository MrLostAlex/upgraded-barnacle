using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SessionMan.DataAccess.Commands;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Queries;
using SessionMan.Shared.Models;

namespace SessionMan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IMediator mediator, ILogger<ClientsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("ClientsController initialised.");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorRecord), StatusCodes.Status400BadRequest)]
        public async Task<List<ClientRecord>> GetAllClients(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(GetAllClients)}.");
                var clients = await _mediator.Send(new GetClientListQuery(), cancellationToken);
                return clients;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(GetAllClients)}.");
            }
        }
        
        [HttpGet("{clientId:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorRecord), StatusCodes.Status400BadRequest)]
        public async Task<ClientRecord> GetClientById(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(GetClientById)}.");
                ClientRecord client = await _mediator.Send(new GetClientByIdQuery(clientId), cancellationToken);
                return client;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(GetClientById)}.");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorRecord), StatusCodes.Status400BadRequest)]
        public async Task<ClientUpsertOutput> CreateClient(ClientCreateInput clientCreateInput, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(CreateClient)}.");
                ClientUpsertOutput createdClient = await _mediator.Send(new CreateClientCommand(clientCreateInput), cancellationToken);
                return createdClient;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(CreateClient)}.");
            }
        }
        
        [HttpPatch]
        [Route("{clientId:guid}")]
        public async Task<ClientUpsertOutput> UpdateClient(Guid clientId, ClientUpdateInput clientUpdateInput, CancellationToken cancellationToken)
        {

            try
            {
                _logger.LogInformation($"Entered method {nameof(UpdateClient)}.");
                ClientUpsertOutput updatedClient = await _mediator.Send(new UpdateClientCommand(clientId, clientUpdateInput), cancellationToken);
                return updatedClient;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(UpdateClient)}.");
            }
        }
        
        [HttpDelete]
        [Route("{clientId:guid}")]
        public async Task<NoContentResult> DeleteClient(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(DeleteClient)}.");
                await _mediator.Send(new DeleteClientCommand(clientId), cancellationToken);
                return NoContent();
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(DeleteClient)}.");
            }
        }
    }
}