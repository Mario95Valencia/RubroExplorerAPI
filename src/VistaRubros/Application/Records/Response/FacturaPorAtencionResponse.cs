namespace vistarubros.Application.Records.Response
{
    public class FacturaPorAtencionResponse
    {
        public long AteCodigo { get; set; }
        public string? AteNumero { get; set; }
        public string? HistoriaClinica { get; set; }
        public string? Paciente { get; set; }
        public string? Sexo { get; set; }
        public string? MedicoTratante { get; set; }
        public string? Seguro { get; set; }
        public string? TipoAtencion { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string? NumeroFactura { get; set; }
        public DateTime? FechaFactura { get; set; }
        public List<FacturaItem> Items { get; set; } = new();

        public decimal? SubtotalClinica => Items?.Sum(i => i.ValorTotal) ?? 0;
        public decimal? IvaTotal => Items?.Sum(i => i.Iva) ?? 0;
        public decimal? Total => SubtotalClinica + IvaTotal;
    }

}
