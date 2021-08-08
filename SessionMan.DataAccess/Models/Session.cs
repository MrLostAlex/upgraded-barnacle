using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SessionMan.DataAccess.Models
{
    public class Session : AuditBase
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        [Required]
        public DateTimeOffset StartDateTime { get; set; }
        
        [Required]
        public DateTimeOffset EndDateTime { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}