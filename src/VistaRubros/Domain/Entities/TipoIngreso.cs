using System;
using System.Collections.Generic;

namespace vistarubros.Domain.Entities;

public partial class TipoIngreso
{
    public short TipCodigo { get; set; }

    public string? TipDescripcion { get; set; }

    public bool? TipEstado { get; set; }

    public int? TipServicioCodigo { get; set; }

    public virtual ICollection<Atenciones> Atenciones { get; set; } = new List<Atenciones>();
}
