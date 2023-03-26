using System.Threading.Tasks;
using API.Application.Commands;
using API.Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirportsController : ControllerBase
    {
        private readonly ILogger<AirportsController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AirportsController(
            ILogger<AirportsController> logger,
            IMediator mediator,
            IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

		/// <summary>
		/// Store airport
		/// </summary>
		/// <param name="command"></param>
		/// <returns>Returns created airport</returns>
		[HttpPost]
        public async Task<IActionResult> Store([FromBody]CreateAirportCommand command)
        {
            var airport = await _mediator.Send(command);

            return Ok(_mapper.Map<AirportViewModel>(airport));
        }
    }
}