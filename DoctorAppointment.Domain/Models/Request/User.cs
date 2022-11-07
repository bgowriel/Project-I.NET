﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Domain.Models.Request
{
    public class User
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public string? Role { get; set; }
        public List<MedicalVisit>? MedicalVisits { get; set; }

        
    }
}
