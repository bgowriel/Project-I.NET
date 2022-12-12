﻿using DoctorAppointment.DataAccess;
using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.IntegrationTests
{
    public static class Utilities
    {
		private static Guid appointmentId;

		public static Guid AppointmentId { get => appointmentId; set => appointmentId = value; }

		public static void InitializeDbForTests(DatabaseContext db)
        {
            User doctor = new()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Role = "Doctor",
                UserName = "john.doe@example.com"
            };

            User patient = new()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                Role = "Patient",
                UserName = "jane.doe@example.com"
            };

            db.Users.Add(doctor);
            db.Users.Add(patient);
            db.SaveChanges();

            var users = db.Users.Take(2).ToList();

            Appointment appointment = new()
            {
                Date = DateTime.Now,
                Description = "Surgeon",
                Status = "Pending",
                DoctorId = users[0].Id,
                PatientId = users[1].Id,
                OfficeId = Guid.NewGuid()
            };

            db.Appointments.Add(appointment);
            db.SaveChanges();

            // get id of first appointment
            if (db.Appointments == null)
            {
				throw new Exception("No appointments found");
			}
            
            if (db.Appointments.FirstOrDefault() == null)
            {
                throw new Exception("No appointments found");
            }

            if (db.Appointments.FirstOrDefault()?.Id == null)
            {
                throw new Exception("No appointments found");
            }

            AppointmentId = (Guid)(db.Appointments.FirstOrDefault()?.Id);

            if (AppointmentId == null)
            {
                throw new Exception("No appointments found");
            }


        }
    }
}
