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
    //[ExcludeFromCodeCoverage]
    [ApiController]
    //[Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("1.5", Deprecated = true)]
    [ApiVersion("2.0")]
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
