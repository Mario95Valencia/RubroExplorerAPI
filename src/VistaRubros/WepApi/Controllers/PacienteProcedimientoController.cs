using MediatR;
using Microsoft.AspNetCore.Mvc;
using vistarubros.Application.Queries;
using vistarubros.Application.Records.Response;

namespace vistarubros.WepApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteProcedimientoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PacienteProcedimientoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            var result = await _mediator.Send(new GetByDatetimePacienteProcedimientoQuery(fechaInicio, fechaFin));
            return Ok(result);
        }
    }
}
