using System;
using System.Collections.Generic;

namespace vistarubros.Domain.Entities;

public partial class HcProtocoloOperatorioProcedimiento
{
    public int PotCodigo { get; set; }

    public int? ProtCodigo { get; set; }

    public string? PotTarifario { get; set; }

    public string? PotPorcentaje { get; set; }

    public string? PotObservacion { get; set; }

    public string? PotTipo { get; set; }

    public virtual HcProtocoloOperatorio? ProtCodigoNavigation { get; set; }
}
