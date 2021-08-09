using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Models;

namespace SessionMan.DataAccess.Repository.IRepository
{
    public interface IClientRepository
    {
        Task<Client> CreateClient(Client clientCreateInput, CancellationToken cancellationToken);
        Task<Client> UpdateClient(Client clientUpdateInput, CancellationToken cancellationToken);
        Task DeleteClient(Guid clientId, CancellationToken cancellationToken);
        Task<List<Client>> GetAllClients(CancellationToken cancellationToken);
        Task<Client> GetClientById(Guid clientId, CancellationToken cancellationToken);
        Task<bool> IsClientUnique(string email, Guid clientId, CancellationToken cancellationToken);
        Task<Client> IsClientExisting(Guid clientId, CancellationToken cancellationToken);
    }
}