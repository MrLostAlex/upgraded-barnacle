﻿using System;
using Newtonsoft.Json;

namespace SessionMan.DataAccess.DataTransfer.Session
{
    public record SessionUpsertOutput : AuditBaseRecord
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartDateTime { get; set; }

        public DateTimeOffset EndDateTime { get; set; }
        
        [JsonIgnore]
        public ErrorBaseRecord ErrorBaseRecord { get; set; }
    }
}