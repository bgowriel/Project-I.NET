﻿namespace DoctorAppointment.Domain.Models.Response
{
    public class MedicineResponse
    {
        public Guid Id { get; private set; }

        public string? Name { get; set; }

        public string? Code { get; set; }
        
        public float? Price { get; set; }
        
        public int? Quantity { get; set; }
        
        public string? Description { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ExpirationDate { get; set; }
    }
}
