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
  [Route("api/offices")]
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
           
            var validator = new OfficePutPostDtoValidator();
            var validationResult = validator.Validate(office);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

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
	

	[HttpPut]
	[Route("{id}")]
	public async Task<IActionResult> UpdateOffice(Guid id, [FromBody] OfficePutPostDto request)
	{
		var command = new UpdateOffice()
		{
			Name = request.Name,
			Description = request.Description,
			Address = request.Address,
			City = request.City,
			Email = request.Email,
			Phone = request.Phone,
		};

		var updated = await _mediator.Send(command);

		if (updated == null)
		{
			return NotFound();
		}

		var updatedDto = _mapper.Map<OfficeGetDto>(updated);

		return Ok(updatedDto);
	}

	[HttpDelete]
	[Route("{id}")]
	public async Task<IActionResult> DeleteOffice(Guid id)
	{
		var command = new DeleteOffice() { Id = id };
		var deleted = await _mediator.Send(command);

		if (deleted == null)
		{
			return NotFound();
		}

		var deletedDto = _mapper.Map<OfficeGetDto>(deleted);

		return Ok(deletedDto);
	}
}
}
