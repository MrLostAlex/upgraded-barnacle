using System;

namespace SessionMan.DataAccess.DataTransfer.Session
{
    public record SessionCreateInput : AuditBaseRecord
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartDateTime { get; set; }

        public DateTimeOffset EndDateTime { get; set; }
        
        public Guid CreatorId { get; set; } = default;
    }
}