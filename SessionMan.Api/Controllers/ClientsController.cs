using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.Commands;
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

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientRecord>>> GetAllClients(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetClientListQuery(), cancellationToken);
        }
        
        [HttpGet("{clientId:guid}")]
        public async Task<ActionResult<ClientRecord>> GetClientById(Guid clientId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetClientByIdQuery(clientId), cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<ClientUpsertOutput>> CreateClient(ClientCreateInput clientCreateInput, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new CreateClientCommand(clientCreateInput), cancellationToken);
        }
        
        // [HttpPatch]
        // [Route("{clientId:guid}")]
        // public async Task<ActionResult<ClientUpsertOutput>> UpdateClient(Guid clientId, ClientUpdateInput clientUpdateInput, CancellationToken cancellationToken)
        // {
        //     if (await _clientRepository.IsClientExisting(clientId, cancellationToken))
        //     {
        //         return await _clientRepository.UpdateClient(clientId, clientUpdateInput, cancellationToken);
        //     }
        //
        //     return BadRequest(new Error()
        //     {
        //         Title = "Bad Request",
        //         StatusCode = StatusCodes.Status400BadRequest,
        //         ErrorMessage = "Invalid client id provided."
        //     });
        // }
        //
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