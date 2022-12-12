using AutoMapper;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Api.Validators;
using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MedicalVisitController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public MedicalVisitController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicalVisit([FromBody] MedicalVisitPutPostDto medicalVisitPutPostDto)
        {
            var validator = new MedicalVisitPutPostDtoValidator();
            var validationResult = validator.Validate(medicalVisitPutPostDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var command = mapper.Map<InsertMedicalVisit>(medicalVisitPutPostDto);
            var created = await mediator.Send(command);
            var createdDto = mapper.Map<MedicalVisitGetDto>(created);

            return CreatedAtAction(nameof(GetMedicalVisitById), new { id = created.Id }, createdDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicalVisits()
        {
            var medicalVisits = await mediator.Send(new GetAllMedicalVisits());
            var mappedResult = mapper.Map<List<MedicalVisitGetDto>>(medicalVisits);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMedicalVisitById(Guid id)
        {
            var medicalVisit = await mediator.Send(new GetMedicalVisitById() { Id = id });

            if (medicalVisit == null)
            {
                return NotFound();
            }

            var mappedResult = mapper.Map<MedicalVisitGetDto>(medicalVisit);
            return Ok(mappedResult);
        }
    }
}
