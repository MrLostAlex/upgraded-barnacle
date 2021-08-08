using System;

namespace SessionMan.DataAccess.DataTransfer
{
    public record AuditBaseRecord
    {
        public DateTimeOffset? CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        
        public DateTimeOffset? UpdateTime { get; set; }
        public string UpdatedBy { get; set; }
    }
}