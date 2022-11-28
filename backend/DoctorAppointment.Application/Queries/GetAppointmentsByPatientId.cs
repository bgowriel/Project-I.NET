using DoctorAppointment.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Queries
{
    public class GetAppointmentsByPatientId : IRequest<List<Appointment>>
    {
        public string PatientId { get; set; }
    }
}
