using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Client;

namespace SessionMan.DataAccess.Queries
{
    public record GetClientByIdQuery(Guid Id) : IRequest<ClientRecord>;
}