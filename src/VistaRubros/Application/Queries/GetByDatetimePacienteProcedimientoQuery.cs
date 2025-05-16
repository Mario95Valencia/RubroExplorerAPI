using MediatR;
using vistarubros.Application.Records.Response;

namespace vistarubros.Application.Queries
{
    public record GetByDatetimePacienteProcedimientoQuery(DateTime FechaInicio, DateTime FechaFin) : IRequest<ApiResponse<IEnumerable<PacienteProcedimientoResponse>>>;

}
