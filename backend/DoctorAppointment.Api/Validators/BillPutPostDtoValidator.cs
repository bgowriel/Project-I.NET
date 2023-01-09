using DoctorAppointment.Api.Dto;
using FluentValidation;

namespace DoctorAppointment.Api.Validators
{
    public class BillPutPostDtoValidator : AbstractValidator<BillPutPostDto>
    {
        public BillPutPostDtoValidator()
        {
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.PatientId).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}
