using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using vistarubros.Domain.Entities;

namespace vistarubros.Infrastructure.Persistence.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HcProtocoloOperatorio> HcProtocoloOperatorio { get; set; }

    public virtual DbSet<HcProtocoloOperatorioProcedimiento> HcProtocoloOperatorioProcedimiento { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HcProtocoloOperatorio>(entity =>
        {
            entity.HasKey(e => e.ProtCodigo);

            entity.ToTable("HC_PROTOCOLO_OPERATORIO");

            entity.Property(e => e.ProtCodigo).HasColumnName("PROT_CODIGO");
            entity.Property(e => e.AdfCodigo).HasColumnName("ADF_CODIGO");
            entity.Property(e => e.AteCodigo).HasColumnName("ATE_CODIGO");
            entity.Property(e => e.Cocirujano1)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("COCIRUJANO_1");
            entity.Property(e => e.Cocirujano2)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("COCIRUJANO_2");
            entity.Property(e => e.Cultivo).HasColumnName("CULTIVO");
            entity.Property(e => e.CultivoDetalle)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("CULTIVO_DETALLE");
            entity.Property(e => e.DetalleHistopatologico)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("DETALLE_HISTOPATOLOGICO");
            entity.Property(e => e.Dren).HasColumnName("DREN");
            entity.Property(e => e.DrenDetalle)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("DREN_DETALLE");
            entity.Property(e => e.OtroAnestesia)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ProtAnestesista)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROT_ANESTESISTA");
            entity.Property(e => e.ProtAyuanestesia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROT_AYUANESTESIA");
            entity.Property(e => e.ProtCirculante)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROT_CIRCULANTE");
            entity.Property(e => e.ProtCirujano)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PROT_CIRUJANO");
            entity.Property(e => e.ProtComplicaciones)
                .IsUnicode(false)
                .HasColumnName("PROT_COMPLICACIONES");
            entity.Property(e => e.ProtDiagnosticoh)
                .IsUnicode(false)
                .HasColumnName("PROT_DIAGNOSTICOH");
            entity.Property(e => e.ProtDictado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROT_DICTADO");
            entity.Property(e => e.ProtDieresis)
                .IsUnicode(false)
                .HasColumnName("PROT_DIERESIS");
            entity.Property(e => e.ProtElectiva).HasColumnName("PROT_ELECTIVA");
            entity.Property(e => e.ProtEmergente).HasColumnName("PROT_EMERGENTE");
            entity.Property(e => e.ProtEscrita)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROT_ESCRITA");
            entity.Property(e => e.ProtEstado).HasColumnName("PROT_ESTADO");
            entity.Property(e => e.ProtExamenhis).HasColumnName("PROT_EXAMENHIS");
            entity.Property(e => e.ProtExploracion)
                .IsUnicode(false)
                .HasColumnName("PROT_EXPLORACION");
            entity.Property(e => e.ProtExposicion)
                .IsUnicode(false)
                .HasColumnName("PROT_EXPOSICION");
            entity.Property(e => e.ProtFecha)
                .HasColumnType("datetime")
                .HasColumnName("PROT_FECHA");
            entity.Property(e => e.ProtFechadic)
                .HasColumnType("datetime")
                .HasColumnName("PROT_FECHADIC");
            entity.Property(e => e.ProtHoraAnestesia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROT_HORA_ANESTESIA");
            entity.Property(e => e.ProtHoradic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PROT_HORADIC");
            entity.Property(e => e.ProtHorafin)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PROT_HORAFIN");
            entity.Property(e => e.ProtHorainicio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PROT_HORAINICIO");
            entity.Property(e => e.ProtInstrumentista)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROT_INSTRUMENTISTA");
            entity.Property(e => e.ProtMedBiopsia).HasColumnName("PROT_MED_BIOPSIA");
            entity.Property(e => e.ProtMedHistopatologico).HasColumnName("PROT_MED_HISTOPATOLOGICO");
            entity.Property(e => e.ProtPaleativa).HasColumnName("PROT_PALEATIVA");
            entity.Property(e => e.ProtPayudante)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROT_PAYUDANTE");
            entity.Property(e => e.ProtPostoperatorio)
                .IsUnicode(false)
                .HasColumnName("PROT_POSTOPERATORIO");
            entity.Property(e => e.ProtPreoperatorio)
                .IsUnicode(false)
                .HasColumnName("PROT_PREOPERATORIO");
            entity.Property(e => e.ProtProcedimiento)
                .IsUnicode(false)
                .HasColumnName("PROT_PROCEDIMIENTO");
            entity.Property(e => e.ProtProfesional)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROT_PROFESIONAL");
            entity.Property(e => e.ProtProyectada)
                .IsUnicode(false)
                .HasColumnName("PROT_PROYECTADA");
            entity.Property(e => e.ProtRealizado)
                .IsUnicode(false)
                .HasColumnName("PROT_REALIZADO");
            entity.Property(e => e.ProtSala)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PROT_SALA");
            entity.Property(e => e.ProtSayudante)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROT_SAYUDANTE");
            entity.Property(e => e.ProtServicio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROT_SERVICIO");
            entity.Property(e => e.ProtSintesis)
                .IsUnicode(false)
                .HasColumnName("PROT_SINTESIS");
            entity.Property(e => e.ProtTayudante)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROT_TAYUDANTE");
            entity.Property(e => e.ProtTipoanest)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("PROT_TIPOANEST");
        });

        modelBuilder.Entity<HcProtocoloOperatorioProcedimiento>(entity =>
        {
            entity.HasKey(e => e.PotCodigo).HasName("PK__HC_PROTO__A319F3FD65DCC530");

            entity.ToTable("HC_PROTOCOLO_OPERATORIO_PROCEDIMIENTO");

            entity.Property(e => e.PotCodigo).HasColumnName("POT_CODIGO");
            entity.Property(e => e.PotObservacion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("POT_OBSERVACION");
            entity.Property(e => e.PotPorcentaje)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("POT_PORCENTAJE");
            entity.Property(e => e.PotTarifario)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("POT_TARIFARIO");
            entity.Property(e => e.PotTipo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("POT_TIPO");
            entity.Property(e => e.ProtCodigo).HasColumnName("PROT_CODIGO");

            entity.HasOne(d => d.ProtCodigoNavigation).WithMany(p => p.HcProtocoloOperatorioProcedimiento)
                .HasForeignKey(d => d.ProtCodigo)
                .HasConstraintName("FK__HC_PROTOC__PROT___13498123");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
