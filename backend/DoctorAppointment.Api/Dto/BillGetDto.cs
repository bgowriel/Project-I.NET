using DoctorAppointment.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Api.Dto
{
	public class BillGetDto
    {
		public Guid Id { get; set; }

		[Required]
		public DateTime Date { get; set; }

		public string? Description { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public double Amount { get; set; }

		public string? PatientId { get; set; }

		public string? DoctorId { get; set; }


	}
}
