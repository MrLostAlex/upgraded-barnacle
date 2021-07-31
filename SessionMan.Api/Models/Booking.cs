using System;
using Microsoft.AspNetCore.Http;

namespace SessionMan.Api.Models
{
    public class Booking : AuditBase
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public Guid ClientId { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public Client Client { get; set; }
        public Session Session { get; set; }
    }

    public enum BookingStatus
    {
        InProgress,
        Confirmed,
        Arrived,
        Cancelled,
        NoShow
    }
}