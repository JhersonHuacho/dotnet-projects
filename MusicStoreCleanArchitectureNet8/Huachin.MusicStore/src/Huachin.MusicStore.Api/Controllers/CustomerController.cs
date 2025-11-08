using Huachin.MusicStore.Application.Handlers.Customers.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Huachin.MusicStore.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly IMediator _mediator;
		public CustomerController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Customer Controller is working");
		}

		[HttpPost]
		public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand request)
		{
			var result = await _mediator.Send(request);
			return CreatedAtAction("Get", new { id = result });
		}
	}
}
