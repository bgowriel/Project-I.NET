﻿namespace DoctorAppointment.Domain.Models.Response
{
    public class AdminResponse
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string? Phone { get; set; }
        
        public string? Address { get; set; }
        
        public string Email { get; set; }
        
        public string Role { get; set; }
    }
}
