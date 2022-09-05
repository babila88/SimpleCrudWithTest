using Mc2.CrudTest.Application.DTOs.CustomerDTO;
using Mc2.CrudTest.Application.Features.CustomerFeatures.Requests.Commands;
using Mc2.CrudTest.Application.Features.CustomerFeatures.Requests.Queries;
using Mc2.CrudTest.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCustomerDTO customer)
        {
            var command = new CreateCustomerCommand { CreateCustomerDTO = customer};
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResultDTO>>> Get()
        {
            var customerList = await _mediator.Send(new CustomerListQuery());
            return Ok(customerList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResultDTO>> Get(int id)
        {
            var customer = await _mediator.Send(new CustomerQuery { Id=id});
            if(customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPut]
        public async Task<ActionResult<CustomerResultDTO>> Put([FromBody] UpdateCustomerDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customer = await _mediator.Send(new UpdateCustomerCommand { UpdateCustomerDTO = model });

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var customer = await _mediator.Send(new DeleteCustomerCommand { Id = id });

            return Ok(customer);
        }
    }
}
