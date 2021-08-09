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
using SessionMan.DataAccess.DataTransfer.Session;
using SessionMan.DataAccess.Queries;
using SessionMan.Shared.Models;

namespace SessionMan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SessionsController> _logger;

        public SessionsController(IMediator mediator, ILogger<SessionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("SessionsController initialised.");
        }
        
        // GET: api/Sessions
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorRecord), StatusCodes.Status400BadRequest)]
        public async Task<List<SessionRecord>> GetAllSessions(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(GetAllSessions)}.");
                var sessions = await _mediator.Send(new GetSessionListQuery(), cancellationToken);
                return sessions;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(GetAllSessions)}.");
            }
        }

        // // GET: api/Sessions/5
        // [HttpGet("{id}", Name = "Get")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // POST: api/Sessions
        [HttpPost]
        [ProducesResponseType(typeof(ErrorRecord), StatusCodes.Status400BadRequest)]
        public async Task<SessionUpsertOutput> CreateSession([FromBody] SessionCreateInput sessionCreateInput, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(CreateSession)}.");
                SessionUpsertOutput session = await _mediator.Send(new CreateSessionCommand(sessionCreateInput), cancellationToken);
                return session;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(CreateSession)}.");
            }
        }

        // // PUT: api/Sessions/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }
        //
        // // DELETE: api/Sessions/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
