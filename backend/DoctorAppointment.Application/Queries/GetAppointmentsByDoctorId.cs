using DoctorAppointment.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Application.Queries
{
    public class GetAppointmentsByDoctorId : IRequest<List<Appointment>>
    {
        public string DoctorId { get; set; }
    }
}
