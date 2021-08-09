using System;
using System.Threading.Tasks;
using MediatR;

namespace SessionMan.DataAccess.Commands
{
    public record DeleteClientCommand(Guid ClientId) : IRequest<Task>;
}