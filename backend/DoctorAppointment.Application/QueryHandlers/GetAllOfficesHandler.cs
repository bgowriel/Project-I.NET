using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.QueryHandlers
{
	public class GetAllOfficesHandler : IRequestHandler<GetAllOffices, List<Office>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllOfficesHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<List<Office>?> Handle(GetAllOffices request, CancellationToken cancellationToken)
		{
			return await _unitOfWork.OfficeRepository.GetAll();
		}
	}
}
