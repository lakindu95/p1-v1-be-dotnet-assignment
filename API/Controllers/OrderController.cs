using System.Threading.Tasks;
using API.Application.Commands;
using API.Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace API.Controllers
{
	public class OrderController : ControllerBase
	{
		private readonly ILogger<OrderController> _logger;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public OrderController(
				ILogger<OrderController> logger,
				IMediator mediator,
				IMapper mapper)
		{
			_logger = logger;
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpPost]
		[Route("Order")]
		public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
		{
			var order = await _mediator.Send(command);
			return new ObjectResult(_mapper.Map<OrderViewModel>(order)) 
			{ StatusCode = StatusCodes.Status201Created };
		}
	}

}