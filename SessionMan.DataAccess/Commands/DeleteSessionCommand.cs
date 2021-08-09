using System;
using System.Threading.Tasks;
using MediatR;

namespace SessionMan.DataAccess.Commands
{
    public record DeleteSessionCommand(Guid SessionId) : IRequest<Task>;
}