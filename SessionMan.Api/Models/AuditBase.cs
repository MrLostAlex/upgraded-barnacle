using System;

namespace SessionMan.Api.Models
{
    public class AuditBase
    {
        public DateTimeOffset CreatedTime { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}