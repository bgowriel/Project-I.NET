using AutoMapper;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Application.Commands;
using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.Api.Profiles
{
    public class AvailableDateProfile : Profile
    {
        public AvailableDateProfile()
        {
            CreateMap<AvailableDate, AvailableDateGetDto>();
            
            CreateMap<AvailableDatePutPostDto, InsertAvailableDate>();
        }
    }
 
}
