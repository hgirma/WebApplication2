﻿namespace WebApplication2.Models
{
    public class AppUser : BaseModel
    {
        public required string Id { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public DateTime? CreatedDate { get; set; } = TimeProvider.System.GetLocalNow().LocalDateTime;
    }
}
