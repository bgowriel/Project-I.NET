using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.Commands
{
    public class InsertAvailableDate :IRequest<AvailableDate>
    {
            public Guid Id { get; set; }

            public DateTime Date { get; set; }

            public bool Free { get; set; }

    }
}
