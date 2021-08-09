using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SessionMan.DataAccess.Commands;
using SessionMan.DataAccess.DataTransfer.Session;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Repository.IRepository;

namespace SessionMan.DataAccess.Handlers
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, SessionUpsertOutput>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateClientHandler> _logger;

        public CreateSessionHandler(ISessionRepository sessionRepository, IMapper mapper, ILogger<CreateClientHandler> logger)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<SessionUpsertOutput> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var session = _mapper.Map<Session>(request.CreateSessionInput);
            session.CreatedBy = request.CreateSessionInput.CreatorId == default ? "System" : request.CreateSessionInput.CreatorId.ToString();
            session.CreatedTime = DateTimeOffset.UtcNow;
            session.UpdatedBy = session.CreatedBy;
            session.UpdateTime = session.CreatedTime;
            Session sessionCreated = await _sessionRepository.CreateSession(session);
            var sessionRecord = _mapper.Map<SessionUpsertOutput>(sessionCreated);
            return sessionRecord;
        }
    }
}