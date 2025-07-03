using System;
using System.Collections.Generic;

namespace vistarubros.Domain.Entities;

public partial class CategoriasConvenios
{
    public short CatCodigo { get; set; }

    public short? AseCodigo { get; set; }

    public string CatNombre { get; set; } = null!;

    public string? CatDescripcion { get; set; }

    public DateOnly? CatFechaInicio { get; set; }

    public DateOnly? CatFechaFin { get; set; }

    public string? CatTipoPrecio { get; set; }

    public short? CatPorDescuento { get; set; }

    public bool CatEstado { get; set; }

    public virtual ICollection<AtencionDetalleCategorias> AtencionDetalleCategorias { get; set; } = new List<AtencionDetalleCategorias>();
}
