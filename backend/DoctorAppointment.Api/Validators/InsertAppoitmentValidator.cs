using DoctorAppointment.Api.Dto;
using DoctorAppointment.Application.Commands;
using FluentValidation;

namespace DoctorAppointment.Api.Validators
{
    public class InsertAppoitmentValidator : AbstractValidator<AppointmentPutPostDto>
    {
        public InsertAppoitmentValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty().WithMessage("PatientId is required");
            RuleFor(x => x.DoctorId).NotEmpty().WithMessage("DoctorId is required");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required");
            RuleFor(x => x.Status).NotEmpty().MaximumLength(50).WithMessage("Status is required");
        }

    }
}