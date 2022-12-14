using AutoMapper;
using DoctorAppointment.Application.Queries;
using DoctorAppointment.Api.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DoctorAppointment.Application.Commands;
using Microsoft.AspNetCore.Authorization;

namespace DoctorAppointment.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/appointments")]
    [ApiVersion("1.0")]
    [ApiVersion("1.5", Deprecated = true)]
    [ApiVersion("2.0")]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AppointmentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] AppointmentPutPostDto appointmentPutPostDto)
        {            
            var command = _mapper.Map<InsertAppointment>(appointmentPutPostDto);
            var created = await _mediator.Send(command);
            var createdDto = _mapper.Map<AppointmentGetDto>(created);

            return CreatedAtAction(nameof(AddAppointment), new { id = created.Id }, createdDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _mediator.Send(new GetAllAppointments());
            var mappedResult = _mapper.Map<List<AppointmentGetDto>>(appointments);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var appointment = await _mediator.Send(new GetAppointmentById() { Id = id });

            if (appointment == null)
            {
                return NotFound();
            }

            var mappedResult = _mapper.Map<AppointmentGetDto>(appointment);
            return Ok(mappedResult);
        }


        [HttpGet]
        [Route("date/{date}")]
        public async Task<IActionResult> GetAppointmentsByDate(DateTime date)
        {
            var appointments = await _mediator.Send(new GetAppointmentsByDate() { Date = date });
            var mappedResult = _mapper.Map<List<AppointmentGetDto>>(appointments);
            return Ok(mappedResult);
        }

      
		[HttpGet]
		[Route("doctor/{doctorId}/date/{date}")]
		public async Task<IActionResult> GetAppointmentsByDoctorIdAndDate(string doctorId, DateTime date)
		{
			var appointments = await _mediator.Send(new GetAppointmentsByDoctorIdAndDate() { DoctorId = doctorId, Date = date });

			var mappedResult = _mapper.Map<List<AppointmentGetDto>>(appointments);
			return Ok(mappedResult);
		}

		// get appointments by patient id
		[HttpGet]
        [Route("patient/{id}")]
        public async Task<IActionResult> GetAppointmentsByPatientId(string id)
        {
            var appointments = await _mediator.Send(new GetAppointmentsByPatientId() { PatientId = id });
            var mappedResult = _mapper.Map<List<AppointmentGetDto>>(appointments);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("doctor/{id}")]
        public async Task<IActionResult> GetAppointmentsByDoctorId(string id)
        {
            var appointments = await _mediator.Send(new GetAppointmentsByDoctorId() { DoctorId = id });
            var mappedResult = _mapper.Map<List<AppointmentGetDto>>(appointments);
            return Ok(mappedResult);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] AppointmentPutPostDto appointmentPutPostDto)
        {
            var command = new UpdateAppointment()
            {
                Id = id,
                Date = appointmentPutPostDto.Date,
                Hour = appointmentPutPostDto.Hour,
                Description = appointmentPutPostDto.Description,
                Status = appointmentPutPostDto.Status,
                DoctorId = appointmentPutPostDto.DoctorId,
                PatientId = appointmentPutPostDto.PatientId,
                OfficeId = appointmentPutPostDto.OfficeId
            };

            var updated = await _mediator.Send(command);

            if (updated == null)
            {
                return NotFound();
            }
            
            var updatedDto = _mapper.Map<AppointmentGetDto>(updated);

            return Ok(updatedDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var command = new DeleteAppointment() { Id = id };
            var deleted = await _mediator.Send(command);

            if (deleted == null)
            {
                return NotFound();
            }

            var deletedDto = _mapper.Map<AppointmentGetDto>(deleted);

            return Ok(deletedDto);
        }
    }
}
