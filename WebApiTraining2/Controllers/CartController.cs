using Contracts.RequestModels.Cart;
using Contracts.ResponseModels.Cart;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiTraining2.Controllers
{
    [Route("api/v1/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CartController>
        [HttpGet]
        public async Task<ActionResult<CartDataListResponse>> Get(CancellationToken cancellationToken)
		{
			var request = new CartDataListRequest();
			var response = await _mediator.Send(request, cancellationToken);

			return Ok(response);
		}

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDetailResponse>> Get(Guid id, [FromServices] IValidator<CartDetailRequest> validator, CancellationToken cancellationToken)
        {
            var request = new CartDetailRequest
            {
                CartID = id
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

        // POST api/<CartController>
        [HttpPost]
        public async Task<ActionResult<CreateCartResponse>> Post([FromBody] CreateCartRequest request, [FromServices] IValidator<CreateCartRequest> validator, CancellationToken cancellationToken)
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

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateCartDataRequest>> Put(Guid id, [FromBody] UpdateCartDataModel model,
            [FromServices] IValidator<UpdateCartDataRequest> validator, CancellationToken cancellationToken)
        {
            var request = new UpdateCartDataRequest { CartID = id, Quantity = model.Quantity};
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);

        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteCartRequest>> Delete(Guid id, [FromServices] IValidator<DeleteCartRequest> validator, CancellationToken cancellationToken)
        {
            var request = new DeleteCartRequest
            {
                CartID = id
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

        // DELETE api/<CartController>
        [HttpDelete]
        public async Task<ActionResult<DeleteAllCartRequest>> Delete(CancellationToken cancellationToken)
        {
            var request = new DeleteAllCartRequest();

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
