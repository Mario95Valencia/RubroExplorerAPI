using System;
using System.Collections.Generic;

namespace vistarubros.Domain.Entities;

public partial class CuentasPacientes
{
    public long CueCodigo { get; set; }

    public int? AteCodigo { get; set; }

    public DateTime? CueFecha { get; set; }

    public string? ProCodigo { get; set; }

    public string? CueDetalle { get; set; }

    public decimal? CueValorUnitario { get; set; }

    public decimal? CueCantidad { get; set; }

    public decimal? CueValor { get; set; }

    public decimal? CueIva { get; set; }

    public long? CueEstado { get; set; }

    public string? CueNumFac { get; set; }

    public short? RubCodigo { get; set; }

    public int? PedCodigo { get; set; }

    public short? IdUsuario { get; set; }

    public int? CatCodigo { get; set; }

    public string? ProCodigoBarras { get; set; }

    public string? CueNumControl { get; set; }

    public string? CueObservacion { get; set; }

    public int? MedCodigo { get; set; }

    public int? CueOrderImpresion { get; set; }

    public long? CodigoPedido { get; set; }

    public int? IdTipoMedico { get; set; }

    public double Costo { get; set; }

    public string? NumVale { get; set; }

    public string DivideFactura { get; set; } = null!;

    public double Descuento { get; set; }

    public double PorDescuento { get; set; }

    public int UsuarioFactura { get; set; }

    public DateTime FechaFactura { get; set; }
}
