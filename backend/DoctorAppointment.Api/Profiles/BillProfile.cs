using DoctorAppointment.Api.Dto;
using DoctorAppointment.Domain.Models;
using AutoMapper;
using DoctorAppointment.Application.Commands;

namespace DoctorAppointment.Api.Profiles
{
    public class BillProfile : Profile
    {
        public BillProfile()
        {
            CreateMap<Bill, BillGetDto>();
            
            CreateMap<BillPutPostDto, InsertBill>();
        }
    }
}
