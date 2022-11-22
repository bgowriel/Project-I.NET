using DoctorAppointment.Api.Dto;
using DoctorAppointment.Application.Commands;
using DoctorAppointment.Domain.Models;
using AutoMapper;


namespace DoctorAppointment.Api.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentGetDto>();
            
            CreateMap<AppointmentPutPostDto, InsertAppointment>();
        }
    }
}
