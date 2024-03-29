using Contracts.RequestModels.Customer;
using Contracts.ResponseModels.Customer;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Entity.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiTraining2.Controllers
{
	[Route("api/v1/customer")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
			_mediator = mediator;
        }

        // GET: api/<CustomerController>
        [HttpGet]
		public async Task<ActionResult<CustomerDataListResponse>> Get(CancellationToken cancellationToken)
		{
			var request = new CustomerDataListRequest();
			var response = await _mediator.Send(request, cancellationToken);

			return Ok(response);
		}

		// GET api/<CustomerController>/5
		[HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetailResponse>> Get(Guid id, [FromServices] IValidator<CustomerDetailRequest> validator, CancellationToken cancellationToken)
        {
            var request = new CustomerDetailRequest
            {
                CustomerId = id
            };
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        // POST api/<CustomerController>
        [HttpPost]
		public async Task<ActionResult<CreateCustomerResponse>> Post([FromBody] CreateCustomerRequest request,  [FromServices] IValidator<CreateCustomerRequest> validator, CancellationToken cancellationToken)
		{
			var validationResult = await validator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				validationResult.AddToModelState(ModelState);
				return ValidationProblem(ModelState);
			}

			var response = await _mediator.Send(request, cancellationToken);

			return Ok(response);
		}

		// PUT api/<CustomerController>/5
		[HttpPut("{id}")]
        public async Task<ActionResult<UpdateCustomerDataRequest>> Put(Guid id, [FromBody] UpdateCustomerDataModel model,
            [FromServices] IValidator<UpdateCustomerDataRequest> validator, CancellationToken cancellationToken)
        {
            var request = new UpdateCustomerDataRequest { CustomerId = id, Name = model.Name, Email = model.Email };
            //request.CustomerId = id;
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);

        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
		public async Task<ActionResult<DeleteCustomerRequest>> Delete(Guid id, [FromServices] IValidator<DeleteCustomerRequest> validator, CancellationToken cancellationToken)
		{
            var request = new DeleteCustomerRequest
            {
                CustomerId = id
            };

            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        // DELETE api/<CustomerController>
        [HttpDelete]
        public async Task<ActionResult<DeleteAllCustomerRequest>> Delete(CancellationToken cancellationToken)
        {
            var request = new DeleteAllCustomerRequest();

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }
    }
}
