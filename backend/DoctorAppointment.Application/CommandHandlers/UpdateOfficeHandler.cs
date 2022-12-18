using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.Domain.Models;
using MediatR;

namespace DoctorAppointment.Application.CommandHandlers
{
    public class UpdateOfficeHandler : IRequestHandler<UpdateOffice, Office>
	{
		private readonly IUnitOfWork _unitOfWork;

		public UpdateOfficeHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<Office> Handle(UpdateOffice request, CancellationToken cancellationToken)
		{
			var toUpdate = new Office
			{
				Id = request.Id,
				Name = request.Name,
				Description = request.Description,
				Address = request.Address,
				City = request.City,
				Email = request.Email,
				Phone = request.Phone,
				Status = request.Status
			};

			_unitOfWork.OfficeRepository.Update(toUpdate);
			await _unitOfWork.Save();

			return toUpdate;
		}
	}
}
