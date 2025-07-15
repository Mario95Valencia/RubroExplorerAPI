using System;
using System.Collections.Generic;

namespace vistarubros.Domain.Entities;

public partial class HcProtocoloOperatorio
{
    public int ProtCodigo { get; set; }

    public int? AteCodigo { get; set; }

    public string? ProtServicio { get; set; }

    public string? ProtSala { get; set; }

    public string? ProtPreoperatorio { get; set; }

    public string? ProtPostoperatorio { get; set; }

    public string? ProtProyectada { get; set; }

    public long? ProtElectiva { get; set; }

    public long? ProtEmergente { get; set; }

    public long? ProtPaleativa { get; set; }

    public string? ProtRealizado { get; set; }

    public string? ProtCirujano { get; set; }

    public string? ProtPayudante { get; set; }

    public string? ProtSayudante { get; set; }

    public string? ProtTayudante { get; set; }

    public string? ProtInstrumentista { get; set; }

    public string? ProtCirculante { get; set; }

    public string? ProtAnestesista { get; set; }

    public string? ProtAyuanestesia { get; set; }

    public DateTime? ProtFecha { get; set; }

    public string? ProtHorainicio { get; set; }

    public string? ProtHorafin { get; set; }

    public string? ProtTipoanest { get; set; }

    public string? ProtDieresis { get; set; }

    public string? ProtExposicion { get; set; }

    public string? ProtExploracion { get; set; }

    public string? ProtProcedimiento { get; set; }

    public string? ProtSintesis { get; set; }

    public string? ProtComplicaciones { get; set; }

    public bool? ProtExamenhis { get; set; }

    public string? ProtDiagnosticoh { get; set; }

    public string? ProtDictado { get; set; }

    public DateTime? ProtFechadic { get; set; }

    public string? ProtHoradic { get; set; }

    public string? ProtEscrita { get; set; }

    public string? ProtProfesional { get; set; }

    public int? AdfCodigo { get; set; }

    public string? ProtHoraAnestesia { get; set; }

    public string? OtroAnestesia { get; set; }

    public string? Cocirujano1 { get; set; }

    public string? Cocirujano2 { get; set; }

    public string? DetalleHistopatologico { get; set; }

    public bool? Cultivo { get; set; }

    public string? CultivoDetalle { get; set; }

    public bool? Dren { get; set; }

    public string? DrenDetalle { get; set; }

    public int ProtMedBiopsia { get; set; }

    public int ProtMedHistopatologico { get; set; }

    public bool ProtEstado { get; set; }

    public virtual ICollection<HcProtocoloOperatorioProcedimiento> HcProtocoloOperatorioProcedimiento { get; set; } = new List<HcProtocoloOperatorioProcedimiento>();
}
