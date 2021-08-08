using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public SessionSqlRepository(IDbContextFactory<AppDbContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }
        
        public async Task<ActionResult<SessionUpsertOutput>> CreateSession(SessionCreateInput sessionCreateInput)
        {
            try
            {
                await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
                var session = _mapper.Map<Session>(sessionCreateInput);
                session.CreatedTime = DateTimeOffset.UtcNow;
                session.UpdateTime = session.CreatedTime;
                session.CreatedBy = sessionCreateInput.CreatorId == default ? "System" : sessionCreateInput.CreatorId.ToString();
                session.UpdatedBy = session.CreatedBy;

                await dbContext.Sessions.AddAsync(session);
                await dbContext.SaveChangesAsync();

                var createdSession = _mapper.Map<SessionUpsertOutput>(session);

                return createdSession;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<ActionResult<SessionUpsertOutput>> UpdateSession(Guid sessionId, SessionUpdateInput sessionUpdateInput)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> DeleteSession(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<List<SessionRecord>>> GetAllSession()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<SessionRecord>> GetSessionById(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsSessionExisting(Guid sessionId)
        {
            throw new NotImplementedException();
        }
    }
}