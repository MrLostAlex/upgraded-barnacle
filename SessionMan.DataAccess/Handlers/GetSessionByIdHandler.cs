using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SessionMan.DataAccess.DataTransfer.Session;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Queries;
using SessionMan.DataAccess.Repository.IRepository;
using SessionMan.Shared.Helpers;

namespace SessionMan.DataAccess.Handlers
{
    public class GetSessionByIdHandler : IRequestHandler<GetSessionByIdQuery, SessionRecord>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;

        public GetSessionByIdHandler(ISessionRepository sessionRepository, IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }
        
        public async Task<SessionRecord> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
        {
            Session session = await _sessionRepository.GetSessionById(request.SessionId, cancellationToken);
            var sessionRecord = _mapper.Map<SessionRecord>(session);
            if (sessionRecord == null) throw new InvalidDataStateException("Session Not Found", $"Session with Id {request.SessionId} has not been found.");
            return sessionRecord;
        }
    }
}