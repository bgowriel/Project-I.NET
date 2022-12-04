using FluentValidation;
using DoctorAppointment.Api.Dto;

namespace DoctorAppointment.Api.Validators
{
    public class MedicalVisitPutPostDtoValidator : AbstractValidator<MedicalVisitPutPostDto>
    {
        public MedicalVisitPutPostDtoValidator()
        {
            RuleFor(x => x.DoctorId).NotEmpty();
            RuleFor(x => x.PatientId).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
     
        }
    }
   
}
