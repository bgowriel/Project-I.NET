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
    //[Authorize]
    [Route("api/[controller]")]
    public class AvailableDateController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public AvailableDateController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddAvailableDate([FromBody] AvailableDatePutPostDto request)
        {
            var validator = new AvailableDatePutPostValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var command = mapper.Map<InsertAvailableDate>(request);
            var result = await mediator.Send(command);
			return CreatedAtAction(nameof(GetAvailableDateById), new { id = result.Id }, result);
		}

        [HttpGet]
        public async Task<IActionResult> GetAvailableDates()
        {
            var availableDates = await mediator.Send(new GetAllAvailableDates());
            var mappedResult = mapper.Map<List<AvailableDateGetDto>>(availableDates);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetAvailableDateById(Guid id)
        {
            var availableDate = await mediator.Send(new GetAvailableDateById() { Id = id });

            if (availableDate == null)
            {
                return NotFound();
            }

            var mappedResult = mapper.Map<AvailableDateGetDto>(availableDate);
            return Ok(mappedResult);
        }


    }
}
