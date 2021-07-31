using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SessionMan.Api.DTO.Client;

namespace SessionMan.Api.Repository.IRepository
{
    public interface IClientRepository
    {
        Task<ClientUpsertOutput> CreateClient(ClientCreateInput clientCreateInput);
        Task<ClientUpsertOutput> UpdateClient(ClientUpdateInput clientUpdateInput);
        Task<int> DeleteClient(Guid clientId);
        Task<List<ClientRecord>> GetAllClients();
        Task<ClientRecord> GetClientById();
        Task<ClientRecord> IsClientUnique(string email, Guid clientId = default);
    }
}