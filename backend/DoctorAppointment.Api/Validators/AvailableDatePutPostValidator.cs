using DoctorAppointment.Api.Dto;
using FluentValidation;

namespace DoctorAppointment.Api.Validators
{
    public class AvailableDatePutPostValidator : AbstractValidator<AvailableDatePutPostDto>
    {
        public AvailableDatePutPostValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Free).NotEmpty();
        }
    }
   
}
