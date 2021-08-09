using MediatR;
using SessionMan.DataAccess.DataTransfer.Session;

namespace SessionMan.DataAccess.Commands
{
    public record CreateSessionCommand(SessionCreateInput CreateSessionInput) : IRequest<SessionUpsertOutput>;
}