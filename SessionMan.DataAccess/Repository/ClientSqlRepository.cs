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

        public ClientSqlRepository(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        
        public async Task<Client> CreateClient(Client clientToBeCreated, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                await dbContext.Clients.AddAsync(clientToBeCreated, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                return clientToBeCreated;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<Client> UpdateClient(Client clientUpdateInput, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                dbContext.Clients.Update(clientUpdateInput);
                await dbContext.SaveChangesAsync(cancellationToken);
                return clientUpdateInput;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task DeleteClient(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                Client clientToDelete = await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId, cancellationToken: cancellationToken);
                if (clientToDelete == null) return;
                dbContext.Clients.Remove(clientToDelete);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            finally
            {
            }
        }

        public async Task<List<Client>> GetAllClients(CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                var clientsFromDb = await dbContext.Clients.ToListAsync(cancellationToken: cancellationToken);
                return clientsFromDb;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<Client> GetClientById(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                Client clientFromDb = await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId, cancellationToken: cancellationToken);

                return clientFromDb;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<bool> IsClientUnique(string email, Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                if (clientId == default)
                {
                    Client clientFromDb =
                        await dbContext.Clients.FirstOrDefaultAsync(c =>
                            string.Equals(c.EmailAddress, email, StringComparison.CurrentCultureIgnoreCase), cancellationToken: cancellationToken);

                    return clientFromDb != null;
                }
                else
                {
                    Client clientFromDb =
                        await dbContext.Clients.FirstOrDefaultAsync(c =>
                            string.Equals(c.EmailAddress, email, StringComparison.CurrentCultureIgnoreCase)
                            && c.Id != clientId, cancellationToken: cancellationToken);

                    return clientFromDb != null;
                }

            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<Client> IsClientExisting(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                return await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId, cancellationToken: cancellationToken);
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}