using System;
using System.Collections.Generic;

namespace vistarubros.Domain.Entities;

public partial class Rubros
{
    public short RubCodigo { get; set; }

    public string? RubNombre { get; set; }

    public string? RubTipo { get; set; }

    public long? RubAsociado { get; set; }

    public string? RubPrograma { get; set; }

    public int? RubOrden { get; set; }

    public string? RubEstado { get; set; }

    public short? TipCodigo { get; set; }

    public long? PedCodigo { get; set; }

    public string? RubGrupo { get; set; }

    public int? RubOrdenImpresion { get; set; }

    public short? RubOrdenImpresionFactura { get; set; }

    public string? PagaIva { get; set; }
}
