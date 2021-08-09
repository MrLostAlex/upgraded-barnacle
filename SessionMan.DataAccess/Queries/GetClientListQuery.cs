using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Client;

namespace SessionMan.DataAccess.Queries
{
    public record GetClientListQuery() : IRequest<List<ClientRecord>>;
}