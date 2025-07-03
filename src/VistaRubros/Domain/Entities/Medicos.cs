using System;
using System.Collections.Generic;

namespace vistarubros.Domain.Entities;

public partial class Medicos
{
    public int MedCodigo { get; set; }

    public short? RetCodigo { get; set; }

    public short? BanCodigo { get; set; }

    public short EspCodigo { get; set; }

    public short? EscCodigo { get; set; }

    public short? IdUsuario { get; set; }

    public short? TimCodigo { get; set; }

    public short? TihCodigo { get; set; }

    public string? MedCodigoMedico { get; set; }

    public DateTime MedFecha { get; set; }

    public DateTime? MedFechaModificacion { get; set; }

    public string? MedNombre1 { get; set; }

    public string? MedNombre2 { get; set; }

    public string? MedApellidoPaterno { get; set; }

    public string? MedApellidoMaterno { get; set; }

    public DateTime? MedFechaNacimiento { get; set; }

    public string? MedDireccion { get; set; }

    public string? MedDireccionConsultorio { get; set; }

    public string? MedRuc { get; set; }

    public string? MedEmail { get; set; }

    public string? MedGenero { get; set; }

    public string? MedNumCuenta { get; set; }

    public string? MedTipoCuenta { get; set; }

    public string? MedCuentaContable { get; set; }

    public string? MedTelefonoCasa { get; set; }

    public string? MedTelefonoConsultorio { get; set; }

    public string? MedTelefonoCelular { get; set; }

    public string? MedAutorizacionSri { get; set; }

    public string? MedValidezAutorizacion { get; set; }

    public string? MedFacturaInicial { get; set; }

    public string? MedFacturaFinal { get; set; }

    public bool? MedConTransferencia { get; set; }

    public bool? MedRecibeLlamada { get; set; }

    public bool MedEstado { get; set; }

    public string? MedCodigoLibro { get; set; }

    public string? MedCodigoFolio { get; set; }

    public string? MedEstadoCivil { get; set; }
}
