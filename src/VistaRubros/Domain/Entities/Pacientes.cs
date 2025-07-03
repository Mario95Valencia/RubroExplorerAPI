using System;
using System.Collections.Generic;

namespace vistarubros.Domain.Entities;

public partial class Pacientes
{
    public int PacCodigo { get; set; }

    public string? PacHistoriaClinica { get; set; }

    public short? IdUsuario { get; set; }

    public string? DipoCodiinec { get; set; }

    public short? ECodigo { get; set; }

    public DateTime? PacFechaCreacion { get; set; }

    public string? PacNombre1 { get; set; }

    public string? PacNombre2 { get; set; }

    public string? PacApellidoPaterno { get; set; }

    public string? PacApellidoMaterno { get; set; }

    public DateTime? PacFechaNacimiento { get; set; }

    public string? PacNacionalidad { get; set; }

    public string? PacTipoIdentificacion { get; set; }

    public string? PacIdentificacion { get; set; }

    public string? PacEmail { get; set; }

    public string? PacGenero { get; set; }

    public string? PacImagen { get; set; }

    public bool PacEstado { get; set; }

    public string? PacDirectorio { get; set; }

    public string PacReferenteNombre { get; set; } = null!;

    public string? PacReferenteParentesco { get; set; }

    public string? PacReferenteTelefono { get; set; }

    public string? PacAlergias { get; set; }

    public string? PacObservaciones { get; set; }

    public short? GsCodigo { get; set; }

    public string? PacReferenteDireccion { get; set; }

    public bool? PacDatosIncompletos { get; set; }

    public string? PacEstadoNivedu { get; set; }

    public string? PacTipEmpresa { get; set; }

    public string? PacSegSalud { get; set; }

    public string? PacTipBono { get; set; }

    public virtual ICollection<Atenciones> Atenciones { get; set; } = new List<Atenciones>();
}
