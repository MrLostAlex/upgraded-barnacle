using System.ComponentModel.DataAnnotations;

namespace SessionMan.Api.DTO.Client
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
    }
}