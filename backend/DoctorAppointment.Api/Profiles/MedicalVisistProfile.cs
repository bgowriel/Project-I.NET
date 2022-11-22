using AutoMapper;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Application.Commands;
using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.Api.Profiles
{
    public class MedicalVisistProfile : Profile
    {
        public MedicalVisistProfile()
        {
            CreateMap<MedicalVisit, MedicalVisitGetDto>();
            
            CreateMap<MedicalVisitPutPostDto, InsertMedicalVisit>();
        }
    }
}
