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
    [Route("api/v{version:apiVersion}/bills")]
    [ApiVersion("1.0")]
    [ApiVersion("1.5", Deprecated = true)]
    [ApiVersion("2.0")]
    public class BillController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public BillController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddBill([FromBody] BillPutPostDto billPutPostDto)
        {
            var command = mapper.Map<InsertBill>(billPutPostDto);
            var created = await mediator.Send(command);
            var createdDto = mapper.Map<BillGetDto>(created);

            return CreatedAtAction(nameof(GetBillById), new { id = created.Id }, createdDto);
        }

        [HttpGet]

        public async Task<IActionResult> GetBills()
        {
            var bills = await mediator.Send(new GetAllBills());
            var mappedResult = mapper.Map<List<BillGetDto>>(bills);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBillById(Guid id)
        {
            var bill = await mediator.Send(new GetBillById() { Id = id });

            if (bill == null)
            {
                return NotFound();
            }

            var mappedResult = mapper.Map<BillGetDto>(bill);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("patient/{id}")]
        public async Task<IActionResult> GetBillsByPatientId(string id)
        {
            var bills = await mediator.Send(new GetBillsByPatientId() { PatientId = id });
            var mappedResult = mapper.Map<List<BillGetDto>>(bills);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("doctor/{id}")]
        public async Task<IActionResult> GetBillsByDoctorId(string id)
        {
            var bills = await mediator.Send(new GetBillsByDoctorId() { DoctorId = id });
            var mappedResult = mapper.Map<List<BillGetDto>>(bills);
            return Ok(mappedResult);
        }

    }
}
