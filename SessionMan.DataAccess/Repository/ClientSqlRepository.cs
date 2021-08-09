using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SessionMan.DataAccess.Data;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Repository.IRepository;
using SessionMan.Shared.Helpers;

namespace SessionMan.DataAccess.Repository
{
    public class ClientSqlRepository : IClientRepository
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly ILogger<ClientSqlRepository> _logger;

        public ClientSqlRepository(IDbContextFactory<AppDbContext> dbContextFactory,
            ILogger<ClientSqlRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public async Task<Client> CreateClient(Client clientToBeCreated, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(CreateClient)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                await dbContext.Clients.AddAsync(clientToBeCreated, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                return clientToBeCreated;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(CreateClient)}.");
            }
        }

        public async Task<Client> UpdateClient(Client clientUpdateInput, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(UpdateClient)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                dbContext.Clients.Update(clientUpdateInput);
                await dbContext.SaveChangesAsync(cancellationToken);
                return clientUpdateInput;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(UpdateClient)}.");
            }
        }

        public async Task DeleteClient(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(DeleteClient)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                Client clientToDelete =
                    await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId, cancellationToken);
                if (clientToDelete == null)
                    throw new BadRequestException("Delete Failed",$"Unable to delete client. Client Id {clientId} not found.");
                dbContext.Clients.Remove(clientToDelete);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(DeleteClient)}.");
            }
        }

        public async Task<List<Client>> GetAllClients(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(GetAllClients)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                var clientsFromDb = await dbContext.Clients.ToListAsync(cancellationToken);
                return clientsFromDb;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(GetAllClients)}.");
            }
        }

        public async Task<Client> GetClientById(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(GetClientById)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                Client clientFromDb =
                    await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId,
                        cancellationToken);

                return clientFromDb;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(GetClientById)}.");
            }
        }

        public async Task<bool> IsClientUnique(string email, Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(IsClientUnique)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                if (clientId == default)
                {
                    Client clientFromDb =
                        await dbContext.Clients.FirstOrDefaultAsync(c =>
                                string.Equals(c.EmailAddress, email, StringComparison.CurrentCultureIgnoreCase),
                            cancellationToken);

                    return clientFromDb != null;
                }
                else
                {
                    Client clientFromDb =
                        await dbContext.Clients.FirstOrDefaultAsync(c =>
                            string.Equals(c.EmailAddress, email, StringComparison.CurrentCultureIgnoreCase)
                            && c.Id != clientId, cancellationToken);

                    return clientFromDb != null;
                }
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(IsClientUnique)}.");
            }
        }

        public async Task<Client> IsClientExisting(Guid clientId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(IsClientExisting)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                return await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId,
                    cancellationToken);
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(IsClientExisting)}.");
            }
        }
    }
}