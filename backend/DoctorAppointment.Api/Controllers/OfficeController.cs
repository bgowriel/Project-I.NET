using AutoMapper;
using DoctorAppointment.Api.Dto;
using DoctorAppointment.Application.Commands;
using DoctorAppointment.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Api.Controllers
{
	[Route("api/offices")]
	[ApiController]
	public class OfficeController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public OfficeController(IMediator mediator, IMapper mapper)
		{
			this._mediator = mediator;
			this._mapper = mapper;
		}
		[HttpPost]
		public async Task<IActionResult> AddOffice([FromBody] OfficePutPostDto office)
		{
			var command = _mapper.Map<InsertOffice>(office);

			var created = await _mediator.Send(command);
			var createdDto = _mapper.Map<OfficeGetDto>(created);

			return CreatedAtAction(nameof(GetOfficeById), new { id = created.Id }, createdDto);
		}

		[HttpGet]
		public async Task<IActionResult> GetOffices()
		{
			var offices = await _mediator.Send(new GetAllOffices());
			var mappedResult = _mapper.Map<List<OfficeGetDto>>(offices);
			return Ok(mappedResult);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetOfficeById(Guid id)
		{
			var office = await _mediator.Send(new GetOfficeById() { Id = id });
			if (office == null)
			{
				return NotFound();
			}

			var mappedResult = _mapper.Map<OfficeGetDto>(office);
			return Ok(mappedResult);
		}

		[HttpGet]
		[Route("/doctors/{id}")]
		public async Task<IActionResult> GetDoctors(Guid id)
		{
			var doctors = await _mediator.Send(new GetAllDoctors() { OfficeId = id});
			var mappedResult = _mapper.Map<List<UserGetDto>>(doctors);
			return Ok(mappedResult);
		}
	}
}
