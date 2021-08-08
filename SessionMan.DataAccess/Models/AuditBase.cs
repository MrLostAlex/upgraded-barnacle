using System;

namespace SessionMan.DataAccess.Models
{
    public class AuditBase
    {
        public DateTimeOffset CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
        public string UpdatedBy { get; set; }
    }
}