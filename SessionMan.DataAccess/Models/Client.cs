using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SessionMan.DataAccess.Models
{
    public class Client : AuditBase
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        
        [Required]
        [Phone]
        public string ContactNumber { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}