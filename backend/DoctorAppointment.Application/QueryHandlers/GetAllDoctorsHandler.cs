﻿using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
	public class GetAllDoctorsHandler : IRequestHandler<GetAllDoctors, List<User>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllDoctorsHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<List<User>> Handle(GetAllDoctors request, CancellationToken cancellationToken)
		{
			return await _unitOfWork.OfficeRepository.GetAllDoctors(request.OfficeId);
		}
	}
}
