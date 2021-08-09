using System;
using MediatR;
using SessionMan.DataAccess.DataTransfer.Session;

namespace SessionMan.DataAccess.Queries
{
    public record GetSessionByIdQuery(Guid SessionId) : IRequest<SessionRecord>;
}