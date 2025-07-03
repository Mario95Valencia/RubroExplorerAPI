using System;
using System.Collections.Generic;

namespace vistarubros.Domain.Entities;

public partial class AtencionDetalleCategorias
{
    public int AdaCodigo { get; set; }

    public int AteCodigo { get; set; }

    public short CatCodigo { get; set; }

    public DateOnly? AdaFechaInicio { get; set; }

    public DateOnly? AdaFechaFin { get; set; }

    public string? AdaAutorizacion { get; set; }

    public string? AdaContrato { get; set; }

    public decimal? AdaMontoCobertura { get; set; }

    public int? AdaOrden { get; set; }

    public bool? AdaEstado { get; set; }

    public int? HccCodigoTs { get; set; }

    public int? HccCodigoDe { get; set; }

    public int? HccCodigoEs { get; set; }

    public virtual CategoriasConvenios CatCodigoNavigation { get; set; } = null!;
}
