using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SessionMan.DataAccess.Data;
using SessionMan.DataAccess.DataTransfer.Session;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Repository.IRepository;
using SessionMan.Shared.Helpers;

namespace SessionMan.DataAccess.Repository
{
    public class SessionSqlRepository : ISessionRepository
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<SessionSqlRepository> _logger;

        public SessionSqlRepository(IDbContextFactory<AppDbContext> dbContextFactory, IMapper mapper,
            ILogger<SessionSqlRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Session> CreateSession(Session sessionCreate, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(CreateSession)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                await dbContext.Sessions.AddAsync(sessionCreate, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                return sessionCreate;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(CreateSession)}.");
            }
        }

        public async Task<Session> UpdateSession(Session sessionUpdate, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(UpdateSession)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                dbContext.Sessions.Update(sessionUpdate);
                await dbContext.SaveChangesAsync(cancellationToken);
                return sessionUpdate;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(UpdateSession)}.");
            }
        }

        public async Task DeleteSession(Guid sessionId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(DeleteSession)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                Session sessionToDelete = await dbContext.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId, cancellationToken);
                if (sessionToDelete == null) throw new InvalidDataStateException("Delete Failed", $"Unable to delete session. Session Id {sessionId} not found.");
                dbContext.Sessions.Remove(sessionToDelete);
                int result = await dbContext.SaveChangesAsync(cancellationToken);
                if(result > 0) return;
                throw new InvalidDataStateException("Delete Failed", $"Unable to delete Session Id {sessionId}.");
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(DeleteSession)}.");
            }
        }

        public async Task<List<Session>> GetAllSessions(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(GetAllSessions)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                var sessionsList = await dbContext.Sessions.ToListAsync(cancellationToken);
                return sessionsList;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(GetAllSessions)}.");
            }
        }

        public async Task<Session> GetSessionById(Guid sessionId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(GetSessionById)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                Session session = await dbContext.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId, cancellationToken);
                return session;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(GetSessionById)}.");
            }
        }
    }
}