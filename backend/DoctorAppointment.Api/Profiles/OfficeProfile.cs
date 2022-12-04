using AutoMapper;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Application.Commands;
using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.Api.Profiles
{
    public class OfficeProfile : Profile
	{
		public OfficeProfile() {

			CreateMap<Office, OfficeGetDto>();

			CreateMap<OfficePutPostDto, InsertOffice>();
		}
		


	}
}
