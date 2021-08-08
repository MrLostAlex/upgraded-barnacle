using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SessionMan.DataAccess.Data;
using SessionMan.DataAccess.DataTransfer.Client;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Repository.IRepository;

namespace SessionMan.DataAccess.Repository
{
    public class ClientSqlRepository : IClientRepository
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly IMapper _mapper;
        
        public ClientSqlRepository(IDbContextFactory<AppDbContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }
        
        public async Task<ActionResult<ClientUpsertOutput>> CreateClient(ClientCreateInput clientCreateInput, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                var client = _mapper.Map<Client>(clientCreateInput);
                client.CreatedBy = clientCreateInput.CreatorId == default ? "System" : clientCreateInput.CreatorId.ToString();
                client.CreatedTime = DateTimeOffset.UtcNow;
                client.UpdatedBy = clientCreateInput.CreatorId == default ? "System" : clientCreateInput.CreatorId.ToString();
                client.UpdateTime = client.CreatedTime;

                await dbContext.Clients.AddAsync(client, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                var createdClient = _mapper.Map<ClientUpsertOutput>(client);

                return createdClient;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<ActionResult<ClientUpsertOutput>> UpdateClient(Guid clientId, ClientUpdateInput clientUpdateInput, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                Client clientToUpdate = await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId, cancellationToken: cancellationToken);

                if (clientToUpdate == null) return new NotFoundResult();
                Client updatedClientForDb = _mapper.Map(clientUpdateInput, clientToUpdate);
                updatedClientForDb.UpdatedBy = clientUpdateInput.UpdaterId == default ? "System" : clientUpdateInput.UpdaterId.ToString();
                updatedClientForDb.UpdateTime = DateTimeOffset.UtcNow;

                dbContext.Clients.Update(updatedClientForDb);
                await dbContext.SaveChangesAsync(cancellationToken);
                return _mapper.Map<ClientUpsertOutput>(updatedClientForDb);

            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<ActionResult> DeleteClient(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                Client clientToDelete = await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId, cancellationToken: cancellationToken);
                if (clientToDelete == null) return new NotFoundResult();
                dbContext.Clients.Remove(clientToDelete);
                int result = await dbContext.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return new NoContentResult();
                }
                else
                {
                    return new ConflictResult();
                }
            }
            catch (Exception exception)
            {
                return new ConflictResult();
            }
        }

        public async Task<ActionResult<List<ClientRecord>>> GetAllClients(CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                var clientsFromDb = await dbContext.Clients.ToListAsync(cancellationToken: cancellationToken);

                var clientRecords = _mapper.Map<List<ClientRecord>>(clientsFromDb);

                return clientRecords;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<ActionResult<ClientRecord>> GetClientById(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                Client clientFromDb = await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId, cancellationToken: cancellationToken);

                if (clientFromDb == null)
                {
                }
                var clientRecord = _mapper.Map<ClientRecord>(clientFromDb);

                return clientRecord;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<ActionResult<ClientRecord>> IsClientUnique(string email, Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                if (clientId == default)
                {
                    Client clientFromDb =
                        await dbContext.Clients.FirstOrDefaultAsync(c =>
                            string.Equals(c.EmailAddress, email, StringComparison.CurrentCultureIgnoreCase), cancellationToken: cancellationToken);

                    return _mapper.Map<ClientRecord>(clientFromDb);
                }
                else
                {
                    Client clientFromDb =
                        await dbContext.Clients.FirstOrDefaultAsync(c =>
                            string.Equals(c.EmailAddress, email, StringComparison.CurrentCultureIgnoreCase)
                            && c.Id != clientId, cancellationToken: cancellationToken);

                    return _mapper.Map<ClientRecord>(clientFromDb);
                }

            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<bool> IsClientExisting(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                return await dbContext.Clients.AnyAsync(c => c.Id == clientId, cancellationToken: cancellationToken);
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}