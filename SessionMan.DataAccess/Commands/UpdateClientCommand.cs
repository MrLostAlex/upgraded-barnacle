using System;
using MediatR;
using SessionMan.DataAccess.DataTransfer.Client;

namespace SessionMan.DataAccess.Commands
{
    public record UpdateClientCommand(Guid ClientId, ClientUpdateInput ClientUpdateInput) : IRequest<ClientUpsertOutput>;
}