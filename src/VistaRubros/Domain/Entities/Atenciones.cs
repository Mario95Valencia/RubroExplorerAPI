using System;
using System.Collections.Generic;

namespace vistarubros.Domain.Entities;

public partial class Atenciones
{
    public int AteCodigo { get; set; }

    public string? AteNumeroAtencion { get; set; }

    public DateTime? AteFecha { get; set; }

    public string? AteNumeroControl { get; set; }

    public string? AteFacturaPaciente { get; set; }

    public DateOnly? AteFacturaFecha { get; set; }

    public DateTime? AteFechaIngreso { get; set; }

    public DateTime? AteFechaAlta { get; set; }

    public bool? AteReferido { get; set; }

    public string? AteReferidoDe { get; set; }

    public short? AteEdadPaciente { get; set; }

    public string? AteAcompananteNombre { get; set; }

    public string? AteAcompananteCedula { get; set; }

    public string? AteAcompananteParentesco { get; set; }

    public string? AteAcompananteTelefono { get; set; }

    public string? AteAcompananteDireccion { get; set; }

    public string? AteAcompananteCiudad { get; set; }

    public string? AteGaranteNombre { get; set; }

    public string? AteGaranteCedula { get; set; }

    public string? AteGaranteParentesco { get; set; }

    public decimal? AteGaranteMontoGarantia { get; set; }

    public string? AteGaranteTelefono { get; set; }

    public string? AteGaranteDireccion { get; set; }

    public string? AteGaranteCiudad { get; set; }

    public string? AteDiagnosticoInicial { get; set; }

    public string? AteDiagnosticoFinal { get; set; }

    public string? AteObservaciones { get; set; }

    public bool? AteEstado { get; set; }

    public string? AteFacturaNombre { get; set; }

    public string? AteDirectorio { get; set; }

    public int? PacCodigo { get; set; }

    public int? DapCodigo { get; set; }

    public short? HabCodigo { get; set; }

    public short? CajCodigo { get; set; }

    public short? TiaCodigo { get; set; }

    public short? IdUsusario { get; set; }

    public short? TirCodigo { get; set; }

    public short? AflCodigo { get; set; }

    public int? MedCodigo { get; set; }

    public short? TipCodigo { get; set; }

    public short? TifCodigo { get; set; }

    public string? TifObservacion { get; set; }

    public int? AteNumeroAdmision { get; set; }

    public bool? AteEnQuirofano { get; set; }

    public int? ForPago { get; set; }

    public string? AteQuienEntregaPac { get; set; }

    public bool? AteCierreHc { get; set; }

    public DateTime? AteFecIngHabitacion { get; set; }

    public int? EscCodigo { get; set; }

    public string? CueEstado { get; set; }

    public string? TipoAtencion { get; set; }

    public bool? AteDiscapacidad { get; set; }

    public string? AteCarnetConadis { get; set; }

    public int? AteIdAccidente { get; set; }

    public int IdTipoDescuento { get; set; }

    public string? AteFuenteInformacion { get; set; }

    public string? AteInstitucionEntrega { get; set; }

    public string? AteInstitucionTelefono { get; set; }

    public virtual Pacientes? PacCodigoNavigation { get; set; }

    public virtual TipoIngreso? TipCodigoNavigation { get; set; }
}
