using System;

namespace SessionMan.Api.DTO
{
    public record AuditBaseRecord
    {
        public DateTimeOffset CreatedTime { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}