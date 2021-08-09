using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SessionMan.DataAccess.Data;
using SessionMan.DataAccess.DataTransfer.Session;
using SessionMan.DataAccess.Models;
using SessionMan.DataAccess.Repository.IRepository;

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

        public async Task<Session> CreateSession(Session sessionCreate)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(CreateSession)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                await dbContext.Sessions.AddAsync(sessionCreate);
                await dbContext.SaveChangesAsync();
                return sessionCreate;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(CreateSession)}.");
            }
        }

        public async Task<Session> UpdateSession(Session sessionUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteSession(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Session>> GetAllSessions()
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(GetAllSessions)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                var sessionsList = await dbContext.Sessions.ToListAsync();
                return sessionsList;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(GetAllSessions)}.");
            }
        }

        public async Task<Session> GetSessionById(Guid sessionId)
        {
            try
            {
                _logger.LogInformation($"Entered method {nameof(GetSessionById)}.");
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                Session session = await dbContext.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
                return session;
            }
            finally
            {
                _logger.LogInformation($"Exit method {nameof(GetSessionById)}.");
            }
        }
    }
}