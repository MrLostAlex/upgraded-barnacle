using System;

namespace SessionMan.DataAccess.DataTransfer.Session
{
    public record SessionRecord : AuditBaseRecord
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartDateTime { get; set; }

        public DateTimeOffset EndDateTime { get; set; }
        
    }
}