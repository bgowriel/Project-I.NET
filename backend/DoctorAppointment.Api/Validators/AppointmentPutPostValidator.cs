using DoctorAppointment.Api.Dto;
using FluentValidation;

namespace DoctorAppointment.Api.Validators
{
    public class AppointmentPutPostDtoValidator : AbstractValidator<AppointmentPutPostDto>
    {
        public AppointmentPutPostDtoValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty();
            RuleFor(x => x.DoctorId).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Status).NotEmpty().MaximumLength(50);
        }
    }

}
