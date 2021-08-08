using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Client;

namespace SessionMan.DataAccess.Repository.IRepository
{
    public interface IClientRepository
    {
        Task<ActionResult<ClientUpsertOutput>> CreateClient(ClientCreateInput clientCreateInput, CancellationToken cancellationToken);
        Task<ActionResult<ClientUpsertOutput>> UpdateClient(Guid clientId, ClientUpdateInput clientUpdateInput, CancellationToken cancellationToken);
        Task<ActionResult> DeleteClient(Guid clientId, CancellationToken cancellationToken);
        Task<ActionResult<List<ClientRecord>>> GetAllClients(CancellationToken cancellationToken);
        Task<ActionResult<ClientRecord>> GetClientById(Guid clientId, CancellationToken cancellationToken);
        Task<ActionResult<ClientRecord>> IsClientUnique(string email, Guid clientId, CancellationToken cancellationToken);
        Task<bool> IsClientExisting(Guid clientId, CancellationToken cancellationToken);
    }
}