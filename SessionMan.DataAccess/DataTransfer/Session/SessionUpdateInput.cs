using System;

namespace SessionMan.DataAccess.DataTransfer.Session
{
    public record SessionUpdateInput : AuditBaseRecord
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartDateTime { get; set; }

        public DateTimeOffset EndDateTime { get; set; }
    }
}