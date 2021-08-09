using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Session;
using SessionMan.DataAccess.Models;

namespace SessionMan.DataAccess.Repository.IRepository
{
    public interface ISessionRepository
    {
        Task<Session> CreateSession(Session sessionCreateInput, CancellationToken cancellationToken);
        Task<Session> UpdateSession(Session sessionUpdateInput, CancellationToken cancellationToken);
        Task DeleteSession(Guid sessionId, CancellationToken cancellationToken);
        Task<List<Session>> GetAllSessions(CancellationToken cancellationToken);
        Task<Session> GetSessionById(Guid sessionId, CancellationToken cancellationToken);
    }
}