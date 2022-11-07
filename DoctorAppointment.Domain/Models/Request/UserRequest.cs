using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Domain.Models.Request
{
    public class UserRequest
    {
        public string? Name { get; set; }

        public string? Email { get; set; }
        
        public string? Phone { get; set; }
        
        public string? Address { get; set; }

        public string? Role { get; set; } //client or doctor

        public List<AppointmentRequest>? Appointments { get; set; }
        
        public List<MedicalVisitRequest>? MedicalVisits { get; set; }        
    }
}
