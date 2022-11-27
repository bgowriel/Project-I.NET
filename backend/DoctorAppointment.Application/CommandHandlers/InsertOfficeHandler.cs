using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
	public class InsertOfficeHandler : IRequestHandler<InsertOffice, Office>
	{
		private readonly IUnitOfWork _unitOfWork;

		public InsertOfficeHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<Office> Handle(InsertOffice request, CancellationToken cancellationToken)
		{

			var office = new Office
			{
				Id = request.Id,
				Name = request.Name,
				Description = request.Description,
				Address = request.Address,
				City = request.City,
				Email = request.Email,
				Phone = request.Phone,

			};

			await _unitOfWork.OfficeRepository.Insert(office);
			await _unitOfWork.Save();

			return office;
		}
	}
}
