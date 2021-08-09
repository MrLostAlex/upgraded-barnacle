using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SessionMan.DataAccess.DataTransfer.Client
{
    public record ClientUpsertOutput : AuditBaseRecord
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }
        
        public string ContactNumber { get; set; }

        [JsonIgnore]
        public ErrorBaseRecord ErrorBaseRecord { get; set; }
    }
}