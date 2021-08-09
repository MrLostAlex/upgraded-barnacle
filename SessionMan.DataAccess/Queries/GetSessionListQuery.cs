using System.Collections.Generic;
using MediatR;
using SessionMan.DataAccess.DataTransfer.Session;

namespace SessionMan.DataAccess.Queries
{
    public record GetSessionListQuery() : IRequest<List<SessionRecord>>;
}