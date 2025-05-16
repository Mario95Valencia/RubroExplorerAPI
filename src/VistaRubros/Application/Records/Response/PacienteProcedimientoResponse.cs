using System.Text.Json.Serialization;

namespace vistarubros.Application.Records.Response
{
    public record PacienteProcedimientoResponse
    {
        [JsonPropertyName("paciente")]
        public string PACIENTE { get; set; } = string.Empty;

        [JsonPropertyName("sexo")]
        public string SEXO { get; set; } = string.Empty;

        [JsonPropertyName("medicoTratante")]
        public string MEDICO_TRATANTE { get; set; } = string.Empty;

        [JsonPropertyName("seguro")]
        public string SEGURO { get; set; } = string.Empty;

        [JsonPropertyName("fechaIntervencion")]
        public DateTime? F_INTERVENCION { get; set; }

        [JsonPropertyName("procedimiento")]
        public string PROCEDIMIENTO { get; set; } = string.Empty;

        [JsonPropertyName("cirujano")]
        public string CIRUJANO { get; set; } = string.Empty;

        [JsonPropertyName("especialidad")]
        public string ESPECIALIDAD { get; set; } = string.Empty;

        [JsonPropertyName("tipoMedico")]
        public string TIPO { get; set; } = string.Empty;

        [JsonPropertyName("horaInicio")]
        public string HORA_INICIO { get; set; } = string.Empty;

        [JsonPropertyName("horaFin")]
        public string HORA_FIN { get; set; } = string.Empty;

        [JsonPropertyName("tiempoTranscurrido")]
        public string TIEMPO_TRANSCURRIDO { get; set; } = string.Empty;

        [JsonPropertyName("tipoAtencion")]
        public string T_ATENCION { get; set; } = string.Empty;

        [JsonPropertyName("historiaClinica")]
        public string HC { get; set; } = string.Empty;

        [JsonPropertyName("numeroAtencion")]
        public string ATENCION { get; set; } = string.Empty;

        [JsonPropertyName("ateCodigo")]
        public long ATE_CODIGO { get; set; }

        [JsonPropertyName("factura")]
        public string FACTURA { get; set; } = string.Empty;

        [JsonPropertyName("facturaFecha")]
        public DateTime? FACTURA_FECHA { get; set; }

        [JsonPropertyName("proCodigo")]
        public long? PRO_CODIGO { get; set; }

        [JsonPropertyName("cueDetalle")]
        public string CUE_DETALLE { get; set; } = string.Empty;

        [JsonPropertyName("rubroNombre")]
        public string RUB_NOMBRE { get; set; } = string.Empty;

        [JsonPropertyName("cueCantidad")]
        public decimal? CUE_CANTIDAD { get; set; }

        [JsonPropertyName("cueValorUnitario")]
        public decimal? CUE_VALOR_UNITARIO { get; set; }

        [JsonPropertyName("cueValor")]
        public decimal? CUE_VALOR { get; set; }

        [JsonPropertyName("cueIva")]
        public decimal? CUE_IVA { get; set; }

        public PacienteProcedimientoResponse() { }
    }
}
