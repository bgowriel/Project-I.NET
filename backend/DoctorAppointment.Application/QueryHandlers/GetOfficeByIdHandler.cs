using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
	public class GetOfficeByIdHandler : IRequestHandler<GetOfficeById, Office>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetOfficeByIdHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<Office> Handle(GetOfficeById request, CancellationToken cancellationToken)
		{
			return await _unitOfWork.OfficeRepository.GetById(request.Id);
		}
	}
}
