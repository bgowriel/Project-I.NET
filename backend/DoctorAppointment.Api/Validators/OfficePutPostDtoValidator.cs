using DoctorAppointment.Api.Dto;
using FluentValidation;

namespace DoctorAppointment.Api.Validators
{
    public class OfficePutPostDtoValidator : AbstractValidator<OfficePutPostDto>
    {
        public OfficePutPostDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty();
           
            
        }
    }
}
