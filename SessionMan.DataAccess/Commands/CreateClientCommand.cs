using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Client;

namespace SessionMan.DataAccess.Commands
{
    public record CreateClientCommand(ClientCreateInput ClientCreateInput) : IRequest<ClientUpsertOutput>;
}