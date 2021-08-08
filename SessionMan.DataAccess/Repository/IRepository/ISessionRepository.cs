using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SessionMan.DataAccess.DataTransfer.Session;

namespace SessionMan.DataAccess.Repository.IRepository
{
    public interface ISessionRepository
    {
        Task<ActionResult<SessionUpsertOutput>> CreateSession(SessionCreateInput sessionCreateInput);
        Task<ActionResult<SessionUpsertOutput>> UpdateSession(Guid sessionId, SessionUpdateInput sessionUpdateInput);
        Task<ActionResult> DeleteSession(Guid sessionId);
        Task<ActionResult<List<SessionRecord>>> GetAllSession();
        Task<ActionResult<SessionRecord>> GetSessionById(Guid sessionId);
        Task<bool> IsSessionExisting(Guid sessionId);
    }
}