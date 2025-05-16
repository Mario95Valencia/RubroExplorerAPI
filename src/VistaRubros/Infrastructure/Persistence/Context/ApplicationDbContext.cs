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

    public virtual DbSet<AtencionDetalleCategorias> AtencionDetalleCategorias { get; set; }

    public virtual DbSet<Atenciones> Atenciones { get; set; }

    public virtual DbSet<CategoriasConvenios> CategoriasConvenios { get; set; }

    public virtual DbSet<CuentasPacientes> CuentasPacientes { get; set; }

    public virtual DbSet<Medicos> Medicos { get; set; }

    public virtual DbSet<Pacientes> Pacientes { get; set; }

    public virtual DbSet<Rubros> Rubros { get; set; }

    public virtual DbSet<TipoIngreso> TipoIngreso { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AtencionDetalleCategorias>(entity =>
        {
            entity.HasKey(e => e.AdaCodigo).HasName("PK_ATENCION_DETALLE_ASEGURADORAS");

            entity.ToTable("ATENCION_DETALLE_CATEGORIAS", tb => tb.HasTrigger("trg_AtencionDetalleCategoria"));

            entity.Property(e => e.AdaCodigo)
                .ValueGeneratedNever()
                .HasColumnName("ADA_CODIGO");
            entity.Property(e => e.AdaAutorizacion)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("ADA_AUTORIZACION");
            entity.Property(e => e.AdaContrato)
                .HasMaxLength(25)
                .IsFixedLength()
                .HasColumnName("ADA_CONTRATO");
            entity.Property(e => e.AdaEstado).HasColumnName("ADA_ESTADO");
            entity.Property(e => e.AdaFechaFin).HasColumnName("ADA_FECHA_FIN");
            entity.Property(e => e.AdaFechaInicio).HasColumnName("ADA_FECHA_INICIO");
            entity.Property(e => e.AdaMontoCobertura)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("ADA_MONTO_COBERTURA");
            entity.Property(e => e.AdaOrden).HasColumnName("ADA_ORDEN");
            entity.Property(e => e.AteCodigo).HasColumnName("ATE_CODIGO");
            entity.Property(e => e.CatCodigo).HasColumnName("CAT_CODIGO");
            entity.Property(e => e.HccCodigoDe).HasColumnName("HCC_CODIGO_DE");
            entity.Property(e => e.HccCodigoEs).HasColumnName("HCC_CODIGO_ES");
            entity.Property(e => e.HccCodigoTs).HasColumnName("HCC_CODIGO_TS");

            entity.HasOne(d => d.CatCodigoNavigation).WithMany(p => p.AtencionDetalleCategorias)
                .HasForeignKey(d => d.CatCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ATENCION_DETALLE_CATEGORIAS_CATEGORIAS_CONVENIOS");
        });

        modelBuilder.Entity<Atenciones>(entity =>
        {
            entity.HasKey(e => e.AteCodigo);

            entity.ToTable("ATENCIONES", tb =>
                {
                    tb.HasTrigger("TR_ATENCIONES_AUDITORIA");
                    tb.HasTrigger("TR_ATENCIONES_UPDATE");
                    tb.HasTrigger("tr_Admision_Cuentas");
                    tb.HasTrigger("tr_historialhabitacion");
                });

            entity.HasIndex(e => e.HabCodigo, "FK_ATENCIONES_HABITACIONES_FK");

            entity.HasIndex(e => e.PacCodigo, "FK_ATENCIONES_PACIENTES_FK");

            entity.Property(e => e.AteCodigo)
                .ValueGeneratedNever()
                .HasColumnName("ATE_CODIGO");
            entity.Property(e => e.AflCodigo).HasColumnName("AFL_CODIGO");
            entity.Property(e => e.AteAcompananteCedula)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ATE_ACOMPANANTE_CEDULA");
            entity.Property(e => e.AteAcompananteCiudad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ATE_ACOMPANANTE_CIUDAD");
            entity.Property(e => e.AteAcompananteDireccion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ATE_ACOMPANANTE_DIRECCION");
            entity.Property(e => e.AteAcompananteNombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ATE_ACOMPANANTE_NOMBRE");
            entity.Property(e => e.AteAcompananteParentesco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ATE_ACOMPANANTE_PARENTESCO");
            entity.Property(e => e.AteAcompananteTelefono)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ATE_ACOMPANANTE_TELEFONO");
            entity.Property(e => e.AteCarnetConadis)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("ate_carnet_conadis");
            entity.Property(e => e.AteCierreHc).HasColumnName("ATE_CIERRE_HC");
            entity.Property(e => e.AteDiagnosticoFinal)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ATE_DIAGNOSTICO_FINAL");
            entity.Property(e => e.AteDiagnosticoInicial)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ATE_DIAGNOSTICO_INICIAL");
            entity.Property(e => e.AteDirectorio)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ATE_DIRECTORIO");
            entity.Property(e => e.AteDiscapacidad).HasColumnName("ate_discapacidad");
            entity.Property(e => e.AteEdadPaciente)
                .HasDefaultValue((short)0)
                .HasColumnName("ATE_EDAD_PACIENTE");
            entity.Property(e => e.AteEnQuirofano).HasColumnName("ATE_EN_QUIROFANO");
            entity.Property(e => e.AteEstado).HasColumnName("ATE_ESTADO");
            entity.Property(e => e.AteFacturaFecha).HasColumnName("ATE_FACTURA_FECHA");
            entity.Property(e => e.AteFacturaNombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ATE_FACTURA_NOMBRE");
            entity.Property(e => e.AteFacturaPaciente)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ATE_FACTURA_PACIENTE");
            entity.Property(e => e.AteFecIngHabitacion)
                .HasColumnType("datetime")
                .HasColumnName("ATE_FEC_ING_HABITACION");
            entity.Property(e => e.AteFecha)
                .HasColumnType("datetime")
                .HasColumnName("ATE_FECHA");
            entity.Property(e => e.AteFechaAlta)
                .HasColumnType("datetime")
                .HasColumnName("ATE_FECHA_ALTA");
            entity.Property(e => e.AteFechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("ATE_FECHA_INGRESO");
            entity.Property(e => e.AteFuenteInformacion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ATE_FUENTE_INFORMACION");
            entity.Property(e => e.AteGaranteCedula)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ATE_GARANTE_CEDULA");
            entity.Property(e => e.AteGaranteCiudad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ATE_GARANTE_CIUDAD");
            entity.Property(e => e.AteGaranteDireccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ATE_GARANTE_DIRECCION");
            entity.Property(e => e.AteGaranteMontoGarantia)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("ATE_GARANTE_MONTO_GARANTIA");
            entity.Property(e => e.AteGaranteNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ATE_GARANTE_NOMBRE");
            entity.Property(e => e.AteGaranteParentesco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ATE_GARANTE_PARENTESCO");
            entity.Property(e => e.AteGaranteTelefono)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ATE_GARANTE_TELEFONO");
            entity.Property(e => e.AteIdAccidente).HasColumnName("ATE_ID_ACCIDENTE");
            entity.Property(e => e.AteInstitucionEntrega)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ATE_INSTITUCION_ENTREGA");
            entity.Property(e => e.AteInstitucionTelefono)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ATE_INSTITUCION_TELEFONO");
            entity.Property(e => e.AteNumeroAdmision)
                .HasDefaultValue(0)
                .HasColumnName("ATE_NUMERO_ADMISION");
            entity.Property(e => e.AteNumeroAtencion)
                .HasMaxLength(20)
                .HasColumnName("ATE_NUMERO_ATENCION");
            entity.Property(e => e.AteNumeroControl)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ATE_NUMERO_CONTROL");
            entity.Property(e => e.AteObservaciones)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ATE_OBSERVACIONES");
            entity.Property(e => e.AteQuienEntregaPac)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("ATE_QUIEN_ENTREGA_PAC");
            entity.Property(e => e.AteReferido).HasColumnName("ATE_REFERIDO");
            entity.Property(e => e.AteReferidoDe)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ATE_REFERIDO_DE");
            entity.Property(e => e.CajCodigo)
                .HasDefaultValue((short)0)
                .HasColumnName("CAJ_CODIGO");
            entity.Property(e => e.CueEstado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CUE_ESTADO");
            entity.Property(e => e.DapCodigo)
                .HasDefaultValue(0)
                .HasColumnName("DAP_CODIGO");
            entity.Property(e => e.EscCodigo).HasColumnName("ESC_CODIGO");
            entity.Property(e => e.ForPago)
                .HasDefaultValue(0)
                .HasColumnName("FOR_PAGO");
            entity.Property(e => e.HabCodigo)
                .HasDefaultValue((short)0)
                .HasColumnName("HAB_CODIGO");
            entity.Property(e => e.IdTipoDescuento)
                .HasDefaultValue(1)
                .HasColumnName("idTipoDescuento");
            entity.Property(e => e.IdUsusario).HasColumnName("ID_USUSARIO");
            entity.Property(e => e.MedCodigo).HasColumnName("MED_CODIGO");
            entity.Property(e => e.PacCodigo)
                .HasDefaultValue(0)
                .HasColumnName("PAC_CODIGO");
            entity.Property(e => e.TiaCodigo)
                .HasDefaultValue((short)0)
                .HasColumnName("TIA_CODIGO");
            entity.Property(e => e.TifCodigo).HasColumnName("TIF_CODIGO");
            entity.Property(e => e.TifObservacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TIF_OBSERVACION");
            entity.Property(e => e.TipCodigo).HasColumnName("TIP_CODIGO");
            entity.Property(e => e.TipoAtencion)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.TirCodigo).HasColumnName("TIR_CODIGO");

            entity.HasOne(d => d.PacCodigoNavigation).WithMany(p => p.Atenciones)
                .HasForeignKey(d => d.PacCodigo)
                .HasConstraintName("FK_ATENCIONES_PACIENTES");

            entity.HasOne(d => d.TipCodigoNavigation).WithMany(p => p.Atenciones)
                .HasForeignKey(d => d.TipCodigo)
                .HasConstraintName("FK_ATENCIONES_TIPO_INGRESO");
        });

        modelBuilder.Entity<CategoriasConvenios>(entity =>
        {
            entity.HasKey(e => e.CatCodigo).HasName("PK_CATEGORIAS_SERVICIOS");

            entity.ToTable("CATEGORIAS_CONVENIOS");

            entity.Property(e => e.CatCodigo)
                .ValueGeneratedNever()
                .HasColumnName("CAT_CODIGO");
            entity.Property(e => e.AseCodigo).HasColumnName("ASE_CODIGO");
            entity.Property(e => e.CatDescripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CAT_DESCRIPCION");
            entity.Property(e => e.CatEstado).HasColumnName("CAT_ESTADO");
            entity.Property(e => e.CatFechaFin).HasColumnName("CAT_FECHA_FIN");
            entity.Property(e => e.CatFechaInicio).HasColumnName("CAT_FECHA_INICIO");
            entity.Property(e => e.CatNombre)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CAT_NOMBRE");
            entity.Property(e => e.CatPorDescuento).HasColumnName("CAT_POR_DESCUENTO");
            entity.Property(e => e.CatTipoPrecio)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("CAT_TIPO_PRECIO");
        });

        modelBuilder.Entity<CuentasPacientes>(entity =>
        {
            entity.HasKey(e => e.CueCodigo);

            entity.ToTable("CUENTAS_PACIENTES", tb =>
                {
                    tb.HasTrigger("CONTROLAIVA");
                    tb.HasTrigger("tr_Cuentasvalores");
                    tb.HasTrigger("trg_CuentaPacienteAud");
                });

            entity.Property(e => e.CueCodigo)
                .ValueGeneratedNever()
                .HasColumnName("CUE_CODIGO");
            entity.Property(e => e.AteCodigo).HasColumnName("ATE_CODIGO");
            entity.Property(e => e.CatCodigo).HasColumnName("CAT_CODIGO");
            entity.Property(e => e.CodigoPedido).HasColumnName("Codigo_Pedido");
            entity.Property(e => e.Costo).HasColumnName("COSTO");
            entity.Property(e => e.CueCantidad)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("CUE_CANTIDAD");
            entity.Property(e => e.CueDetalle)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("CUE_DETALLE");
            entity.Property(e => e.CueEstado).HasColumnName("CUE_ESTADO");
            entity.Property(e => e.CueFecha)
                .HasColumnType("datetime")
                .HasColumnName("CUE_FECHA");
            entity.Property(e => e.CueIva)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("CUE_IVA");
            entity.Property(e => e.CueNumControl)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("CUE_NUM_CONTROL");
            entity.Property(e => e.CueNumFac)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("CUE_NUM_FAC");
            entity.Property(e => e.CueObservacion)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("CUE_OBSERVACION");
            entity.Property(e => e.CueOrderImpresion).HasColumnName("CUE_ORDER_IMPRESION");
            entity.Property(e => e.CueValor)
                .HasColumnType("decimal(12, 3)")
                .HasColumnName("CUE_VALOR");
            entity.Property(e => e.CueValorUnitario)
                .HasColumnType("decimal(12, 3)")
                .HasColumnName("CUE_VALOR_UNITARIO");
            entity.Property(e => e.DivideFactura)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N");
            entity.Property(e => e.FechaFactura)
                .HasDefaultValueSql("('')")
                .HasColumnType("datetime")
                .HasColumnName("FECHA_FACTURA");
            entity.Property(e => e.IdTipoMedico).HasColumnName("Id_Tipo_Medico");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
            entity.Property(e => e.MedCodigo)
                .HasDefaultValue(0)
                .HasColumnName("MED_CODIGO");
            entity.Property(e => e.NumVale)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PedCodigo).HasColumnName("PED_CODIGO");
            entity.Property(e => e.ProCodigo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PRO_CODIGO");
            entity.Property(e => e.ProCodigoBarras)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PRO_CODIGO_BARRAS");
            entity.Property(e => e.RubCodigo).HasColumnName("RUB_CODIGO");
            entity.Property(e => e.UsuarioFactura)
                .HasDefaultValueSql("('')")
                .HasColumnName("USUARIO_FACTURA");
        });

        modelBuilder.Entity<Medicos>(entity =>
        {
            entity.HasKey(e => e.MedCodigo).IsClustered(false);

            entity.ToTable("MEDICOS", tb => tb.HasTrigger("trg_MedicosAud"));

            entity.Property(e => e.MedCodigo)
                .ValueGeneratedNever()
                .HasColumnName("MED_CODIGO");
            entity.Property(e => e.BanCodigo).HasColumnName("BAN_CODIGO");
            entity.Property(e => e.EscCodigo).HasColumnName("ESC_CODIGO");
            entity.Property(e => e.EspCodigo).HasColumnName("ESP_CODIGO");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
            entity.Property(e => e.MedApellidoMaterno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MED_APELLIDO_MATERNO");
            entity.Property(e => e.MedApellidoPaterno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MED_APELLIDO_PATERNO");
            entity.Property(e => e.MedAutorizacionSri)
                .HasMaxLength(49)
                .IsUnicode(false)
                .HasColumnName("MED_AUTORIZACION_SRI");
            entity.Property(e => e.MedCodigoFolio)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("MED_CODIGO_FOLIO");
            entity.Property(e => e.MedCodigoLibro)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("MED_CODIGO_LIBRO");
            entity.Property(e => e.MedCodigoMedico)
                .HasMaxLength(50)
                .HasColumnName("MED_CODIGO_MEDICO");
            entity.Property(e => e.MedConTransferencia).HasColumnName("MED_CON_TRANSFERENCIA");
            entity.Property(e => e.MedCuentaContable)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("((210101)-(5))")
                .HasColumnName("MED_CUENTA_CONTABLE");
            entity.Property(e => e.MedDireccion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("MED_DIRECCION");
            entity.Property(e => e.MedDireccionConsultorio)
                .HasMaxLength(160)
                .IsUnicode(false)
                .HasColumnName("MED_DIRECCION_CONSULTORIO");
            entity.Property(e => e.MedEmail)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("MED_EMAIL");
            entity.Property(e => e.MedEstado).HasColumnName("MED_ESTADO");
            entity.Property(e => e.MedEstadoCivil)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("MED_ESTADO_CIVIL");
            entity.Property(e => e.MedFacturaFinal)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MED_FACTURA_FINAL");
            entity.Property(e => e.MedFacturaInicial)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MED_FACTURA_INICIAL");
            entity.Property(e => e.MedFecha)
                .HasColumnType("datetime")
                .HasColumnName("MED_FECHA");
            entity.Property(e => e.MedFechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("MED_FECHA_MODIFICACION");
            entity.Property(e => e.MedFechaNacimiento)
                .HasColumnType("datetime")
                .HasColumnName("MED_FECHA_NACIMIENTO");
            entity.Property(e => e.MedGenero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MED_GENERO");
            entity.Property(e => e.MedNombre1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MED_NOMBRE1");
            entity.Property(e => e.MedNombre2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MED_NOMBRE2");
            entity.Property(e => e.MedNumCuenta)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("MED_NUM_CUENTA");
            entity.Property(e => e.MedRecibeLlamada).HasColumnName("MED_RECIBE_LLAMADA");
            entity.Property(e => e.MedRuc)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("MED_RUC");
            entity.Property(e => e.MedTelefonoCasa)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("MED_TELEFONO_CASA");
            entity.Property(e => e.MedTelefonoCelular)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("MED_TELEFONO_CELULAR");
            entity.Property(e => e.MedTelefonoConsultorio)
                .HasMaxLength(30)
                .HasColumnName("MED_TELEFONO_CONSULTORIO");
            entity.Property(e => e.MedTipoCuenta)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("MED_TIPO_CUENTA");
            entity.Property(e => e.MedValidezAutorizacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MED_VALIDEZ_AUTORIZACION");
            entity.Property(e => e.RetCodigo).HasColumnName("RET_CODIGO");
            entity.Property(e => e.TihCodigo).HasColumnName("TIH_CODIGO");
            entity.Property(e => e.TimCodigo).HasColumnName("TIM_CODIGO");
        });

        modelBuilder.Entity<Pacientes>(entity =>
        {
            entity.HasKey(e => e.PacCodigo).IsClustered(false);

            entity.ToTable("PACIENTES", tb =>
                {
                    tb.HasTrigger("TR_PACIENTES_UPDATE");
                    tb.HasTrigger("TR_PACIENTE_AUDITORIA");
                });

            entity.Property(e => e.PacCodigo)
                .ValueGeneratedNever()
                .HasColumnName("PAC_CODIGO");
            entity.Property(e => e.DipoCodiinec)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DIPO_CODIINEC");
            entity.Property(e => e.ECodigo).HasColumnName("E_CODIGO");
            entity.Property(e => e.GsCodigo).HasColumnName("GS_CODIGO");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
            entity.Property(e => e.PacAlergias)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PAC_ALERGIAS");
            entity.Property(e => e.PacApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("PAC_APELLIDO_MATERNO");
            entity.Property(e => e.PacApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("PAC_APELLIDO_PATERNO");
            entity.Property(e => e.PacDatosIncompletos)
                .HasDefaultValue(false)
                .HasColumnName("PAC_DATOS_INCOMPLETOS");
            entity.Property(e => e.PacDirectorio)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PAC_DIRECTORIO");
            entity.Property(e => e.PacEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAC_EMAIL");
            entity.Property(e => e.PacEstado).HasColumnName("PAC_ESTADO");
            entity.Property(e => e.PacEstadoNivedu)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PAC_ESTADO_NIVEDU");
            entity.Property(e => e.PacFechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("PAC_FECHA_CREACION");
            entity.Property(e => e.PacFechaNacimiento)
                .HasColumnType("datetime")
                .HasColumnName("PAC_FECHA_NACIMIENTO");
            entity.Property(e => e.PacGenero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAC_GENERO");
            entity.Property(e => e.PacHistoriaClinica)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAC_HISTORIA_CLINICA");
            entity.Property(e => e.PacIdentificacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PAC_IDENTIFICACION");
            entity.Property(e => e.PacImagen)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("PAC_IMAGEN");
            entity.Property(e => e.PacNacionalidad)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PAC_NACIONALIDAD");
            entity.Property(e => e.PacNombre1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("PAC_NOMBRE1");
            entity.Property(e => e.PacNombre2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("PAC_NOMBRE2");
            entity.Property(e => e.PacObservaciones)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("PAC_OBSERVACIONES");
            entity.Property(e => e.PacReferenteDireccion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("PAC_REFERENTE_DIRECCION");
            entity.Property(e => e.PacReferenteNombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PAC_REFERENTE_NOMBRE");
            entity.Property(e => e.PacReferenteParentesco)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("PAC_REFERENTE_PARENTESCO");
            entity.Property(e => e.PacReferenteTelefono)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("PAC_REFERENTE_TELEFONO");
            entity.Property(e => e.PacSegSalud)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PAC_SEG_SALUD");
            entity.Property(e => e.PacTipBono)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PAC_TIP_BONO");
            entity.Property(e => e.PacTipEmpresa)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PAC_TIP_EMPRESA");
            entity.Property(e => e.PacTipoIdentificacion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("PAC_TIPO_IDENTIFICACION");
        });

        modelBuilder.Entity<Rubros>(entity =>
        {
            entity.HasKey(e => e.RubCodigo);

            entity.ToTable("RUBROS");

            entity.Property(e => e.RubCodigo)
                .ValueGeneratedNever()
                .HasColumnName("RUB_CODIGO");
            entity.Property(e => e.PagaIva)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("PAGA_IVA");
            entity.Property(e => e.PedCodigo).HasColumnName("PED_CODIGO");
            entity.Property(e => e.RubAsociado).HasColumnName("RUB_ASOCIADO");
            entity.Property(e => e.RubEstado)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("RUB_ESTADO");
            entity.Property(e => e.RubGrupo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("RUB_GRUPO");
            entity.Property(e => e.RubNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RUB_NOMBRE");
            entity.Property(e => e.RubOrden).HasColumnName("RUB_ORDEN");
            entity.Property(e => e.RubOrdenImpresion).HasColumnName("RUB_ORDEN_IMPRESION");
            entity.Property(e => e.RubOrdenImpresionFactura).HasColumnName("RUB_ORDEN_IMPRESION_FACTURA");
            entity.Property(e => e.RubPrograma)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("RUB_PROGRAMA");
            entity.Property(e => e.RubTipo)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("RUB_TIPO");
            entity.Property(e => e.TipCodigo).HasColumnName("TIP_CODIGO");
        });

        modelBuilder.Entity<TipoIngreso>(entity =>
        {
            entity.HasKey(e => e.TipCodigo);

            entity.ToTable("TIPO_INGRESO");

            entity.Property(e => e.TipCodigo)
                .ValueGeneratedNever()
                .HasColumnName("TIP_CODIGO");
            entity.Property(e => e.TipDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TIP_DESCRIPCION");
            entity.Property(e => e.TipEstado).HasColumnName("TIP_ESTADO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
