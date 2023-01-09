using AutoMapper;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Api.Validators;
using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace DoctorAppointment.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    //[Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("1.5", Deprecated = true)]
    [ApiVersion("2.0")]
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
