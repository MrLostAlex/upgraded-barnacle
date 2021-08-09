using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Session;
using SessionMan.DataAccess.Models;

namespace SessionMan.DataAccess.Repository.IRepository
{
    public interface ISessionRepository
    {
        Task<Session> CreateSession(Session sessionCreateInput);
        Task<Session> UpdateSession(Session sessionUpdateInput);
        Task DeleteSession(Guid sessionId);
        Task<List<Session>> GetAllSessions();
        Task<Session> GetSessionById(Guid sessionId);
    }
}