using DoctorAppointment.Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Application.Commands
{
    public class InsertBill : IRequest<Bill>
    {
		public DateTime Date { get; set; }

		public string? Description { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public double Amount { get; set; }

		public string? PatientId { get; set; }

		public User? Patient { get; set; }

		public string? DoctorId { get; set; }

		public User? Doctor { get; set; }

	}
}
