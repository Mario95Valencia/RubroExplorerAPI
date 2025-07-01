using MediatR;
using vistarubros.Application.Queries;
using vistarubros.Application.Records.Response;
using vistarubros.Domain.Interfaces.IDomainServices;

namespace vistarubros.Application.Handlers
{
    public class GetByDatetimePacienteProcedimientoHandler : IRequestHandler<GetByDatetimePacienteProcedimientoQuery, ApiResponse<IEnumerable<PacienteProcedimientoDetalleResponse>>>
    {
        private readonly IPacienteAtencionDomainService _pacienteAtencionService;

        public GetByDatetimePacienteProcedimientoHandler(IPacienteAtencionDomainService pacienteAtencionService)
        {
            _pacienteAtencionService = pacienteAtencionService;
        }

        public async Task<ApiResponse<IEnumerable<PacienteProcedimientoDetalleResponse>>> Handle(GetByDatetimePacienteProcedimientoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var resultado = await _pacienteAtencionService.ObtenerDetallePorRangoFechas(request.FechaInicio, request.FechaFin);
                return new ApiResponse<IEnumerable<PacienteProcedimientoDetalleResponse>>(Guid.NewGuid(), "LIST", resultado, "Success");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<PacienteProcedimientoDetalleResponse>>(Guid.NewGuid(), "ERROR", null, ex.Message);
            }
        }
    }
}
