using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SessionMan.DataAccess.Commands;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Repository.IRepository;
using SessionMan.Shared.Helpers;

namespace SessionMan.DataAccess.Handlers
{
    public class DeleteSessionHandler : IRequestHandler<DeleteSessionCommand, Task>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ILogger<DeleteSessionHandler> _logger;

        public DeleteSessionHandler(ISessionRepository sessionRepository, ILogger<DeleteSessionHandler> logger)
        {
            _sessionRepository = sessionRepository;
            _logger = logger;
        }
        
        public async Task<Task> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
        {
            Session sessionToDelete = await _sessionRepository.GetSessionById(request.SessionId, cancellationToken);
            if(sessionToDelete == null) throw new InvalidDataStateException("Delete Failed", $"Unable to delete session. Session Id {request.SessionId} not found.");
            return _sessionRepository.DeleteSession(sessionToDelete.Id, cancellationToken);
        }
    }
}