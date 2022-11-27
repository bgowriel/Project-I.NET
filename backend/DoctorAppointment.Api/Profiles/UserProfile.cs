﻿using AutoMapper;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Application.Commands;
using DoctorAppointment.Domain.Models;

namespace DoctorAppointment.Api.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserGetDto>();

		}
	}
}