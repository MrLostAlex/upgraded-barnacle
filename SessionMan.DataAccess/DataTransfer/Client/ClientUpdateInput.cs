using System;
using System.ComponentModel.DataAnnotations;

namespace SessionMan.DataAccess.DataTransfer.Client
{
    public record ClientUpdateInput
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string ContactNumber { get; set; }

        public Guid UpdaterId { get; set; } = default;
    }
}