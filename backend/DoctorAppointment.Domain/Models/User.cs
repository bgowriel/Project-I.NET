﻿using Microsoft.AspNetCore.Identity;

namespace DoctorAppointment.Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public List<Appointment>? Appointments { get; set; }

        public List<MedicalVisit>? MedicalVisits { get; set; }

        public List<Bill>? Bills { get; set; }
    }
}