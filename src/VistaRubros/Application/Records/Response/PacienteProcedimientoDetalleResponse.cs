using System.Text.Json.Serialization;

namespace vistarubros.Application.Records.Response
{
    public class PacienteProcedimientoDetalleResponse
    {
        [JsonPropertyName("numeroAtencion")]
        public string? NumeroAtencion { get; set; }

        [JsonPropertyName("historiaClinica")]
        public string? HistoriaClinica { get; set; }

        [JsonPropertyName("paciente")]
        public string? Paciente { get; set; }

        [JsonPropertyName("sexo")]
        public string? Sexo { get; set; }

        [JsonPropertyName("medicoTratante")]
        public string? MedicoTratante { get; set; }

        [JsonPropertyName("seguro")]
        public string? Seguro { get; set; }

        [JsonPropertyName("tipoAtencion")]
        public string? TipoAtencion { get; set; }

        [JsonPropertyName("fechaIngreso")]
        public DateTime? FechaIngreso { get; set; }

        [JsonPropertyName("fechaAlta")]
        public DateTime? FechaAlta { get; set; }

        [JsonPropertyName("detalleEpicrisis")]
        public List<DetalleEpicrisis> DetalleEpicrisis { get; set; } = new();

        [JsonPropertyName("procedimientos")]
        public List<ProcedimientoItem> Procedimientos { get; set; } = new();

        [JsonPropertyName("factura")]
        public FacturaDetalle Factura { get; set; } = new();
    }

    public class DetalleEpicrisis
    {
        [JsonPropertyName("tratamiento")]
        public string? Tratamiento { get; set; }

        [JsonPropertyName("egreso")]
        public string? Egreso { get; set; }

        [JsonPropertyName("alta")]
        public string? Alta { get; set; }

        [JsonPropertyName("causaExterna")]
        public string? CausaExterna { get; set; }

        [JsonPropertyName("proximoControl")]
        public DateTime? ProximoControl { get; set; }

        [JsonPropertyName("estadoEgreso")]
        public string? EstadoEgreso { get; set; }

        [JsonPropertyName("altaMedica")]
        public string? AltaMedica { get; set; }

        [JsonPropertyName("altaVoluntaria")]
        public string? AltaVoluntaria { get; set; }
    }

    public class ProcedimientoItem
    {
        [JsonPropertyName("fechaIntervencion")]
        public DateTime? FechaIntervencion { get; set; }

        [JsonPropertyName("procedimiento")]
        public string? Procedimiento { get; set; }

        [JsonPropertyName("cirujano")]
        public string? Cirujano { get; set; }

        [JsonPropertyName("especialidad")]
        public string? Especialidad { get; set; }

        [JsonPropertyName("tipoMedico")]
        public string? TipoMedico { get; set; }

        [JsonPropertyName("horaInicio")]
        public string? HoraInicio { get; set; }

        [JsonPropertyName("horaFin")]
        public string? HoraFin { get; set; }

        [JsonPropertyName("tiempoTranscurrido")]
        public string? TiempoTranscurrido { get; set; }
    }

    public class FacturaDetalle
    {
        [JsonPropertyName("numeroFactura")]
        public string? NumeroFactura { get; set; }

        [JsonPropertyName("fechaFactura")]
        public DateTime? FechaFactura { get; set; }

        [JsonPropertyName("subtotalClinica")]
        public decimal? SubtotalClinica { get; set; }

        [JsonPropertyName("ivaTotal")]
        public decimal? IvaTotal { get; set; }

        [JsonPropertyName("total")]
        public decimal? Total { get; set; }

        [JsonPropertyName("items")]
        public List<FacturaItem> Items { get; set; } = new();
    }

    public class FacturaItem
    {
        [JsonPropertyName("proCodigo")]
        public long? ProCodigo { get; set; }

        [JsonPropertyName("rubroNombre")]
        public string? RubroNombre { get; set; }

        [JsonPropertyName("cantidad")]
        public decimal? Cantidad { get; set; }

        [JsonPropertyName("iva")]
        public decimal? Iva { get; set; }

        [JsonPropertyName("valorUnitario")]
        public decimal? ValorUnitario { get; set; }

        [JsonPropertyName("valorTotal")]
        public decimal? ValorTotal { get; set; }
    }
}
