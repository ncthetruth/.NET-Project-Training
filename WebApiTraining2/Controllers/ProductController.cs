using Contracts.RequestModels.Product;
using Contracts.ResponseModels.Product;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiTraining2.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<ProductDataListResponse>> Get(CancellationToken cancellationToken)
        {
            ProductDataListRequest request = new ProductDataListRequest();
            ProductDataListResponse response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailResponse>> Get(Guid id, [FromServices] IValidator<ProductDetailRequest> validator, CancellationToken cancellationToken)
        {
            var request = new ProductDetailRequest
            {
                ProductId = id
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

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<CreateProductResponse>> Post([FromBody] CreateProductRequest request, CancellationToken cancellation)
        { 
            CreateProductResponse response = await _mediator.Send(request, cancellation);
            return Ok(response);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateProductResponse>> Put(Guid id, [FromBody] UpdateProductModel requestModel,[FromServices]IValidator<UpdateProductRequest> _validator, CancellationToken cancellationToken)
        {
            UpdateProductRequest request = new UpdateProductRequest
            {
                ProductID = id,
                Name = requestModel.Name,
                Price = requestModel.Price,
            };

            var validationResult = await _validator.ValidateAsync(request);
            if(!validationResult.IsValid) 
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }

            UpdateProductResponse response = await _mediator.Send(request,cancellationToken);

            return Ok(response);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteProductRequest>> Delete(Guid id ,[FromServices] IValidator<DeleteProductRequest> validator, CancellationToken cancellationToken)
        {
            var request = new DeleteProductRequest
            {
                ProductId = id
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
        public async Task<ActionResult<DeleteAllProductRequest>> Delete(CancellationToken cancellationToken)
        {
            var request = new DeleteAllProductRequest();

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
