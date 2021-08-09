using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SessionMan.DataAccess.Commands;
using SessionMan.DataAccess.DataTransfer;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Queries;
using SessionMan.DataAccess.Repository.IRepository;

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
        [ProducesResponseType(typeof(ErrorBaseRecord), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ClientRecord>>> GetAllClients(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(GetAllClients)}.");
                var clients = await _mediator.Send(new GetClientListQuery(), cancellationToken);
                return Ok(clients);
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(GetAllClients)}.");
            }
        }
        
        [HttpGet("{clientId:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorBaseRecord), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientRecord>> GetClientById(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(GetClientById)}.");
                ClientRecord client = await _mediator.Send(new GetClientByIdQuery(clientId), cancellationToken);
                if (client == null)
                {
                    return NotFound();
                }
                return Ok(client);
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(GetClientById)}.");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorBaseRecord), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientUpsertOutput>> CreateClient(ClientCreateInput clientCreateInput, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(CreateClient)}.");
                ClientUpsertOutput createdClient = await _mediator.Send(new CreateClientCommand(clientCreateInput), cancellationToken);
                if (createdClient.ErrorBaseRecord != null)
                {
                    return BadRequest(createdClient.ErrorBaseRecord);
                }
                return Ok(createdClient);
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(CreateClient)}.");
            }
        }
        
        [HttpPatch]
        [Route("{clientId:guid}")]
        public async Task<ActionResult<ClientUpsertOutput>> UpdateClient(Guid clientId, ClientUpdateInput clientUpdateInput, CancellationToken cancellationToken)
        {

            ClientUpsertOutput updatedClient = await _mediator.Send(new UpdateClientCommand(clientId, clientUpdateInput), cancellationToken);
            if (updatedClient.ErrorBaseRecord !=  null)
            {
                return BadRequest(updatedClient.ErrorBaseRecord);    
            }
            return updatedClient;
        }
        
        // [HttpDelete]
        // [Route("{clientId:guid}")]
        // public async Task<ActionResult> DeleteClient(Guid clientId, CancellationToken cancellationToken)
        // {
        //     if (await _clientRepository.IsClientExisting(clientId, cancellationToken))
        //     {
        //         return await _clientRepository.DeleteClient(clientId, cancellationToken);
        //     }
        //     
        //     return BadRequest(new Error()
        //     {
        //         Title = "Bad Request",
        //         StatusCode = StatusCodes.Status400BadRequest,
        //         ErrorMessage = "Invalid client id provided."
        //     });
        // }
    }
}