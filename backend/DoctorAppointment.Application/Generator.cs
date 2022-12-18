using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace DoctorAppointment.Application
{
    [ExcludeFromCodeCoverage]
    public class Generator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Generator(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Generate()
        {
            var roleExists = await _roleManager.RoleExistsAsync("Patient");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Patient"));
            }

            roleExists = await _roleManager.RoleExistsAsync("Doctor");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Doctor"));
            }
            
            // call methods to generate data
            await GenerateOffices(10);
            await GenerateDoctors(10);
            await GeneratePatients(10);
            await GeneratePatients(10);
            await AddRolesToUsers();
            await GenerateAppointments(10);
            await GenerateAppointments(10);
            await _unitOfWork.Save();
        }

        // add users with Role == "Doctor" to role "Doctor" and users with Role == "Patient" to role "Patient"
        private async Task AddRolesToUsers()
        {
            var users = await _userManager.Users.Take(1000).ToListAsync();
            foreach (var user in users)
            {
                if (user.Role == "Doctor")
                {
                    await _userManager.AddToRoleAsync(user, "Doctor");
                }
                else if (user.Role == "Patient")
                {
                    await _userManager.AddToRoleAsync(user, "Patient");
                }
            }
        }

        // generate mock doctors
        public async Task GenerateDoctors(int howMany)
        {
            var offices = await _unitOfWork.OfficeRepository.GetAll();

            if (offices is null or { Count: 0 })
            {
                return;
            }
            
            var officesIds = offices.Select(o => o.Id).ToList();
            
            for (int i = 0; i < howMany; i++)
            {
                var doctor = new User
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Role = "Doctor",
                    PhoneNumber = Faker.Phone.Number(),
                    OfficeId = officesIds[RandomNumberGenerator.GetInt32(officesIds.Count)]
                };
                doctor.Email = doctor.FirstName + "." + doctor.LastName + RandomNumberGenerator.GetInt32(1950, 2000) + "@gmail.com";
                doctor.UserName = doctor.Email;

                if (_userManager.Users.Any(u => u.Email == doctor.Email))
                {
                    continue;
                }

                var password = doctor.FirstName + doctor.LastName + ".2022";
                password = password.Replace(" ", "");

                await _userManager.CreateAsync(doctor, password);
            }
            await _unitOfWork.Save();
        }

        // generate mock patients
        public async Task GeneratePatients(int howMany)
        {
            for (int i = 0; i < howMany; i++)
            {
                var patient = GetFakePatientData();

                if (_userManager.Users.Any(u => u.Email == patient.Email))
                {
                    continue;
                }

                var password = patient.FirstName + patient.LastName + ".2022";
                password = password.Replace(" ", "");

                await _userManager.CreateAsync(patient, password);
            }
            await _unitOfWork.Save();
        }

        // generate mock offices
        public async Task GenerateOffices(int howMany)
        {
            for (int i = 0; i < howMany; i++)
            {
                var office = new Office
                {
                    Name = Faker.Company.Name(),
                    Description = Faker.Lorem.Sentence(),
                    Address = Faker.Address.StreetAddress(),
                    City = Faker.Address.City(),
                    Email = Faker.Internet.Email(),
                    Phone = Faker.Phone.Number(),
                    Status = RandomNumberGenerator.GetInt32(2) == 0 ? "Pending" : "Approved",
                };

                await _unitOfWork.OfficeRepository.Insert(office);
            }
            await _unitOfWork.Save();
        }

        // generate mock appointments
        public async Task GenerateAppointments(int howMany)
        {
            var doctors = await _userManager.GetUsersInRoleAsync("Doctor");
            var patients = await _userManager.GetUsersInRoleAsync("Patient");

            for (int i = 0; i < howMany; i++)
            {
                var doctor = doctors[RandomNumberGenerator.GetInt32(doctors.Count)];
                var date = DateTime.Today.AddDays(RandomNumberGenerator.GetInt32(1, 50)).AddHours(RandomNumberGenerator.GetInt32(8, 17)).AddMinutes(0).AddSeconds(0);

                var appointment = new Appointment
                {
                    Date = date,
                    Description = Faker.Lorem.Sentence(),
                    Status = RandomNumberGenerator.GetInt32(2) == 0 ? "Pending" : "Approved",
                    DoctorId = doctor.Id,
                    PatientId = patients[RandomNumberGenerator.GetInt32(patients.Count)].Id,
                    OfficeId = doctor.OfficeId
                };

                await _unitOfWork.AppointmentRepository.Insert(appointment);
            }
            await _unitOfWork.Save();
        }

        private static User GetFakePatientData()
        {
            var patient = new User
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Role = "Patient",
                PhoneNumber = Faker.Phone.Number(),
            };
            patient.Email = patient.FirstName + "." + patient.LastName + RandomNumberGenerator.GetInt32(1950, 2000) + "@gmail.com";
            patient.UserName = patient.Email;
            return patient;
        }
    }
}
