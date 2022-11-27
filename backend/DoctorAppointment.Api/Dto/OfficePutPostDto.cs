﻿using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.Api.Dto
{
	public class OfficePutPostDto
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }


	}
}
