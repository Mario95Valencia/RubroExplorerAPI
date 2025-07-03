using System.Text.Json.Serialization;

namespace vistarubros.Application.Records.Response
{
    public class EpicrisisPorAtencion
    {
        public long AteCodigo { get; set; }

        public string? CuadroClinico { get; set; }

        public string? Tratamiento { get; set; }

        public string? Egreso { get; set; }

        public string? Alta { get; set; }

        public string? CausaExterna { get; set; }

        public DateTime? ProximoControl { get; set; }

        public string? EstadoEgreso { get; set; }

        public string? AltaMedica { get; set; }

        public string? AltaVoluntaria { get; set; }

    }
}
