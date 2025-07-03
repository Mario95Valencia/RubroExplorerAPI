using vistarubros.Application.Records.Response;

namespace vistarubros.Domain.Interfaces.IDomainServices
{
    public interface IPacienteAtencionDomainService
    {
        Task<List<PacienteProcedimientoDetalleResponse>> ObtenerDetallePorRangoFechas(DateTime fechaInicio, DateTime fechaFin);

        //Task<List<FacturaPorAtencionResponse>> ObtenerFacturasAsync(DateTime fechaInicio, DateTime fechaFin);

        //Task<List<ProcedimientosPorAtencionResponse>> ObtenerProcedimientosAsync();
    }
}
