using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SessionMan.DataAccess.DataTransfer.Session;
using SessionMan.DataAccess.Queries;
using SessionMan.DataAccess.Repository.IRepository;

namespace SessionMan.DataAccess.Handlers
{
    public class GetSessionListHandler : IRequestHandler<GetSessionListQuery, List<SessionRecord>>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;

        public GetSessionListHandler(ISessionRepository sessionRepository, IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }
        
        public async Task<List<SessionRecord>> Handle(GetSessionListQuery request, CancellationToken cancellationToken)
        {
            var sessions = await _sessionRepository.GetAllSessions(cancellationToken);
            var sessionRecordsList = _mapper.Map<List<SessionRecord>>(sessions);
            return sessionRecordsList;
        }
    }
}