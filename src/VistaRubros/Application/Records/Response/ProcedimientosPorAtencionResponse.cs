namespace vistarubros.Application.Records.Response
{
    public class ProcedimientosPorAtencionResponse
    {
        public long AteCodigo { get; set; }
        public string? Cirujano { get; set; }
        public string? Especialidad { get; set; }
        public string? TipoMedico { get; set; }
        public string? Procedimiento { get; set; }
        public DateTime? FechaIntervencion { get; set; }
        public string? HoraInicio { get; set; }
        public string? HoraFin { get; set; }
        public string? TiempoTranscurrido { get; set; }
    }

}
