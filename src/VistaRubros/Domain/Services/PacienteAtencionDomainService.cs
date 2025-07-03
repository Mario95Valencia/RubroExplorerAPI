using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using vistarubros.Application.Records.Response;
using vistarubros.Domain.Interfaces.IDomainServices;
using vistarubros.Infrastructure.Persistence.Context;
using System.Data.SqlClient;


namespace vistarubros.Domain.Services
{
    public class PacienteAtencionDomainService : IPacienteAtencionDomainService
    {
        private readonly ApplicationDbContext _context;

        public PacienteAtencionDomainService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region CODIGO ANTERIOR 
        //        public async Task<List<PacienteProcedimientoDetalleResponse>> ObtenerDetallePorRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        //        {
        //            using var connection = _context.Database.GetDbConnection();
        //            if (connection.State == ConnectionState.Closed)
        //                await connection.OpenAsync();

        //            var sql = @"WITH 
        //Seguro AS(select  ATE_CODIGO,STRING_AGG(CC.CAT_NOMBRE,', ') AS 'ASEGURADORA' from ATENCION_DETALLE_CATEGORIAS ADC
        //INNER JOIN CATEGORIAS_CONVENIOS CC ON ADC.CAT_CODIGO = CC.CAT_CODIGO
        //GROUP BY ATE_CODIGO),

        //Atencion AS  (select A.ATE_CODIGO, p.PAC_APELLIDO_PATERNO+' '+p.PAC_APELLIDO_MATERNO+' '+p.PAC_NOMBRE1+' '+p.PAC_NOMBRE2 as 'PACIENTE',T.TIP_DESCRIPCION
        //,P.PAC_GENERO AS SEXO,M.MED_APELLIDO_PATERNO+' '+M.MED_APELLIDO_MATERNO+' '+M.MED_NOMBRE1+' '+M.MED_NOMBRE2 AS 'MEDICO_TRATANTE'
        //,P.PAC_HISTORIA_CLINICA AS 'HC' ,a.ATE_NUMERO_ATENCION as 'ATENCION',a.ATE_FECHA_INGRESO as 'FECHA INGRESO',a.ATE_FECHA_ALTA as 'FECHA ALTA'
        //, a.ATE_FACTURA_PACIENTE AS 'FACTURA', a.ATE_FACTURA_FECHA AS 'FACTURA FECHA' 
        //, c.PRO_CODIGO ,c.CUE_DETALLE ,r.RUB_NOMBRE,
        //c.CUE_CANTIDAD,c.CUE_VALOR_UNITARIO,c.CUE_VALOR ,c.CUE_IVA 
        //from  ATENCIONES A 
        //INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO
        //inner join CUENTAS_PACIENTES C on A.ATE_CODIGO = C.ATE_CODIGO
        //inner join MEDICOS m on A.MED_CODIGO = m.MED_CODIGO
        //INNER JOIN RUBROS R  ON C.RUB_CODIGO = R.RUB_CODIGO
        //INNER JOIN TIPO_INGRESO  T ON A.TIP_CODIGO = T.TIP_CODIGO
        //where 
        //C.CUE_CANTIDAD <> 0
        //AND a.ATE_FACTURA_FECHA BETWEEN @FechaInicio AND @FechaFin
        //AND a.ATE_FACTURA_PACIENTE NOT LIKE '%PRE%'),

        //Procedimiento AS (select ATE_CODIGO, ISNULL(QPP_FECHA,(select fecha_registro from REGISTRO_QUIROFANO where ATE_CODIGO = Q.ATE_CODIGO)) AS 'F_INTERVENCION',
        //isnull(PCI_DESCRIPCION,(select intervencion from REGISTRO_QUIROFANO where ATE_CODIGO = Q.ATE_CODIGO)) as 'PROCEDIMIENTO',
        //isnull(QPP_CIRUJANO ,(select cirujano from REGISTRO_QUIROFANO where ATE_CODIGO = Q.ATE_CODIGO)) AS 'CIRUJANO'
        //,ISNULL(CONVERT(VARCHAR(5), q.QPP_HORAINICIO , 108 ), (SELECT  CONVERT(VARCHAR(5), hora_inicio, 108) 
        //     FROM REGISTRO_QUIROFANO  WHERE ATE_CODIGO = Q.ATE_CODIGO)) AS 'HORA_INICIO',
        //ISNULL(CONVERT(VARCHAR(5), QPP_HORAFIN, 108),(SELECT CONVERT(VARCHAR(5), hora_fin, 108) 
        //     FROM REGISTRO_QUIROFANO WHERE ATE_CODIGO = Q.ATE_CODIGO)) AS 'HORA_FIN'
        //,ISNULL(CONVERT(VARCHAR(8), QPP_DURACION, 108),
        //(SELECT CONCAT(CASE WHEN DATEDIFF(HOUR,hora_inicio,hora_fin)%24 < 0 THEN DATEDIFF(HOUR,hora_fin,hora_inicio)%24 ELSE DATEDIFF(HOUR,hora_inicio,hora_fin)%24 END,
        //':',CASE WHEN DATEDIFF(MINUTE,hora_inicio,hora_fin)%60 < 0 THEN DATEDIFF(MINUTE,hora_fin,hora_inicio)%60 ELSE DATEDIFF(MINUTE,hora_inicio,hora_fin)%60 END)
        //FROM REGISTRO_QUIROFANO WHERE ate_codigo = Q.ATE_CODIGO)) AS TIEMPO_TRANSCURRIDO

        //from  QUIROFANO_PROCE_PRODU q inner join PROCEDIMIENTOS_CIRUGIA p on q.PCI_CODIGO = p.PCI_CODIGO 
        //where ATE_CODIGO IS NOT NULL AND QPP_CIRUJANO IS NOT NULL and QPP_CANTIDAD is null),

        //Registro AS(select ate_codigo,fecha_registro,cirujano,hora_inicio,hora_fin,intervencion,
        //CONCAT(CASE WHEN DATEDIFF(HOUR,hora_inicio,hora_fin)%24 < 0 THEN DATEDIFF(HOUR,hora_fin,hora_inicio)%24 ELSE DATEDIFF(HOUR,hora_inicio,hora_fin)%24 END,
        //':',
        //CASE WHEN DATEDIFF(MINUTE,hora_inicio,hora_fin)%60 < 0 THEN DATEDIFF(MINUTE,hora_fin,hora_inicio)%60 ELSE DATEDIFF(MINUTE,hora_inicio,hora_fin)%60 END)
        //AS 'TIEMPO TRANSCURRIDO'
        //from REGISTRO_QUIROFANO),

        //Medico AS(SELECT M.MED_CODIGO,M.MED_APELLIDO_PATERNO+' '+M.MED_APELLIDO_MATERNO+' '+M.MED_NOMBRE1+' '+M.MED_NOMBRE2 AS 'MEDICO',
        //E.ESP_NOMBRE,T.TIM_NOMBRE FROM MEDICOS M 
        //INNER JOIN ESPECIALIDADES_MEDICAS E ON M.ESP_CODIGO = E.ESP_CODIGO
        //INNER JOIN TIPO_MEDICO T ON M.TIM_CODIGO = T.TIM_CODIGO)

        //select 
        //a.ATE_CODIGO,A.PACIENTE,A.SEXO,A.MEDICO_TRATANTE,A.HC,A.TIP_DESCRIPCION AS 'T_ATENCION',A.ATENCION,A.FACTURA,A.[FACTURA FECHA],A.PRO_CODIGO,A.CUE_DETALLE,A.RUB_NOMBRE,CUE_CANTIDAD
        //,A.CUE_VALOR_UNITARIO,A.CUE_VALOR,A.CUE_IVA,S.ASEGURADORA AS SEGURO,ISNULL(P.F_INTERVENCION,R.fecha_registro)AS 'F_INTERVENCION',
        //ISNULL(M2.MEDICO,M.MEDICO) AS 'CIRUJANO',ISNULL(M2.ESP_NOMBRE,M.ESP_NOMBRE)AS 'ESPECIALIDAD',ISNULL(M2.TIM_NOMBRE,M.TIM_NOMBRE)AS'TIPO',
        //ISNULL(P.PROCEDIMIENTO,R.intervencion) AS 'PROCEDIMIENTO'
        //,ISNULL(P.HORA_INICIO,R.hora_inicio)AS 'HORA_INICIO',ISNULL(P.HORA_FIN,R.hora_fin) AS 'HORA_FIN', ISNULL(P.TIEMPO_TRANSCURRIDO,R.[TIEMPO TRANSCURRIDO]) AS 'TIEMPO_TRANSCURRIDO' 
        //from Atencion a 
        //inner join Seguro s ON a.ATE_CODIGO = s.ATE_CODIGO
        //left join Procedimiento p ON a.ATE_CODIGO = p.ATE_CODIGO
        //LEFT JOIN Registro R ON A.ATE_CODIGO = R.ate_codigo
        //LEFT join Medico M ON R.cirujano = M.MED_CODIGO
        //LEFT JOIN Medico M2 ON P.CIRUJANO = M2.MED_CODIGO";

        //            // Ejecutar la consulta original, reutilizando el modelo plano anterior
        //            var result = await connection.QueryAsync<PacienteProcedimientoResponse>(sql, new { FechaInicio = fechaInicio, FechaFin = fechaFin });

        //            // Agrupar y transformar la estructura
        //            var agrupado = result
        //                    .GroupBy(r => r.ATE_CODIGO)
        //                    .Select(grupo =>
        //                    {
        //                        var primero = grupo.First();

        //                        return new PacienteProcedimientoDetalleResponse
        //                        {
        //                            NumeroAtencion = primero.ATENCION,
        //                            HistoriaClinica = primero.HC,
        //                            Paciente = primero.PACIENTE,
        //                            Sexo = primero.SEXO,
        //                            MedicoTratante = primero.MEDICO_TRATANTE,
        //                            Seguro = primero.SEGURO,
        //                            TipoAtencion = primero.T_ATENCION,
        //                            FechaIngreso = primero.FECHA_INGRESO,
        //                            FechaAlta = primero.FECHA_ALTA,

        //                            DetalleEpicrisis = new DetalleEpicrisis
        //                            {
        //                                DiagnosticoPrincipal = primero.DIAG_PRINCIPAL ?? "XXXX",
        //                                Complicaciones = primero.COMPLICACIONES ?? "Ninguna",
        //                                Observaciones = primero.OBSERVACIONES ?? "Procedimiento realizado sin novedades."
        //                            },

        //                            Procedimientos = grupo
        //                                .GroupBy(p => new
        //                                {
        //                                    p.PROCEDIMIENTO,
        //                                    p.F_INTERVENCION,
        //                                    p.CIRUJANO,
        //                                    p.HORA_INICIO,
        //                                    p.HORA_FIN,
        //                                    p.ESPECIALIDAD,
        //                                    p.TIPO
        //                                })
        //                                .Select(proc =>
        //                                {
        //                                    var pr = proc.First();
        //                                    return new ProcedimientoItem
        //                                    {
        //                                        FechaIntervencion = pr.F_INTERVENCION,
        //                                        Procedimiento = pr.PROCEDIMIENTO,
        //                                        Cirujano = pr.CIRUJANO,
        //                                        Especialidad = pr.ESPECIALIDAD,
        //                                        TipoMedico = pr.TIPO,
        //                                        HoraInicio = pr.HORA_INICIO,
        //                                        HoraFin = pr.HORA_FIN,
        //                                        TiempoTranscurrido = pr.TIEMPO_TRANSCURRIDO,
        //                                    };
        //                                }).ToList(),

        //                            Factura = new FacturaDetalle
        //                            {
        //                                NumeroFactura = primero.FACTURA,
        //                                FechaFactura = primero.FACTURA_FECHA,
        //                                SubtotalClinica = grupo.Sum(g => g.CUE_VALOR ?? 0),
        //                                IvaTotal = grupo.Sum(g => g.CUE_IVA ?? 0),
        //                                Total = grupo.Sum(g => g.CUE_VALOR ?? 0),
        //                                Items = grupo.Select(r => new FacturaItem
        //                                {
        //                                    ProCodigo = r.PRO_CODIGO,
        //                                    RubroNombre = r.RUB_NOMBRE,
        //                                    Cantidad = r.CUE_CANTIDAD,
        //                                    Iva = r.CUE_IVA,
        //                                    ValorUnitario = r.CUE_VALOR_UNITARIO,
        //                                    ValorTotal = r.CUE_VALOR
        //                                }).ToList()
        //                            }
        //                        };
        //                    }).ToList();
        //            return agrupado;
        //        }
        #endregion

        public async Task<List<PacienteProcedimientoDetalleResponse>> ObtenerDetallePorRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            var facturas = await ObtenerFacturasAsync(fechaInicio, fechaFin);
            var codigosAtencion = facturas.Select(f => f.AteCodigo).Distinct().ToList();
            var procedimientos = await ObtenerProcedimientosAsync(codigosAtencion);
            var epicrisis = await ObtenerEpicrisisAsync(codigosAtencion);

            var resultado = facturas.Select(f =>
            {
                var procedimientosAtencion = procedimientos
                    .Where(p => p.AteCodigo == f.AteCodigo)
                    .Select(p => new ProcedimientoItem
                    {
                        FechaIntervencion = p.FechaIntervencion,
                        Procedimiento = p.Procedimiento,
                        Cirujano = p.Cirujano,
                        Especialidad = p.Especialidad,
                        TipoMedico = p.TipoMedico,
                        HoraInicio = p.HoraInicio,
                        HoraFin = p.HoraFin,
                        TiempoTranscurrido = p.TiempoTranscurrido,
                    }).ToList();

                var epicrisisAtencion = epicrisis
                    .Where(e => e.AteCodigo == f.AteCodigo)
                    .Select(e => new DetalleEpicrisis
                    {
                        Tratamiento = e.Tratamiento,
                        Egreso = e.Egreso,
                        Alta = e.Alta,
                        CausaExterna = e.CausaExterna,
                        ProximoControl = e.ProximoControl,
                        EstadoEgreso = e.EstadoEgreso,
                        AltaMedica = e.AltaMedica,
                        AltaVoluntaria = e.AltaVoluntaria
                    }).ToList();

                return new PacienteProcedimientoDetalleResponse
                {
                    NumeroAtencion = f.AteNumero,
                    HistoriaClinica = f.HistoriaClinica,
                    Paciente = f.Paciente,
                    Sexo = f.Sexo,
                    MedicoTratante = f.MedicoTratante,
                    Seguro = f.Seguro,
                    TipoAtencion = f.TipoAtencion,
                    FechaIngreso = f.FechaIngreso,
                    FechaAlta = f.FechaAlta,

                    DetalleEpicrisis = epicrisis
    .Where(e => e.AteCodigo == f.AteCodigo)
    .Select(e => new DetalleEpicrisis
    {
        Tratamiento = e.Tratamiento,
        Egreso = e.Egreso,
        Alta = e.Alta,
        CausaExterna = e.CausaExterna,
        ProximoControl = e.ProximoControl,
        EstadoEgreso = e.EstadoEgreso,
        AltaMedica = e.AltaMedica,
        AltaVoluntaria = e.AltaVoluntaria
    }).ToList(),

                    Procedimientos = procedimientosAtencion,

                    Factura = new FacturaDetalle
                    {
                        NumeroFactura = f.NumeroFactura,
                        FechaFactura = f.FechaFactura,
                        SubtotalClinica = f.SubtotalClinica,
                        IvaTotal = f.IvaTotal,
                        Total = f.Total,
                        Items = f.Items
                    }
                };
            }).ToList();

            return resultado.OrderBy(r=>r.Paciente).ToList();
        }


        public async Task<List<FacturaPorAtencionResponse>> ObtenerFacturasAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var connectionString = _context.Database.GetConnectionString();
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();


            var sql = @"
    WITH 
    Seguro AS (
        SELECT ATE_CODIGO, STRING_AGG(CC.CAT_NOMBRE, ', ') AS ASEGURADORA
        FROM ATENCION_DETALLE_CATEGORIAS ADC
        INNER JOIN CATEGORIAS_CONVENIOS CC ON ADC.CAT_CODIGO = CC.CAT_CODIGO
        GROUP BY ATE_CODIGO
    ),
    Atencion AS (
        SELECT 
            A.ATE_CODIGO,A.ATE_NUMERO_ATENCION,
            P.PAC_HISTORIA_CLINICA AS HC,
            P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,
            P.PAC_GENERO AS SEXO,
            M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO_TRATANTE,
            T.TIP_DESCRIPCION AS TIPO_ATENCION,
            A.ATE_FECHA_INGRESO,
            A.ATE_FECHA_ALTA,
            A.ATE_FACTURA_PACIENTE AS FACTURA,
            A.ATE_FACTURA_FECHA AS FECHA_FACTURA
        FROM ATENCIONES A
        INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO
        INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO
        INNER JOIN TIPO_INGRESO T ON A.TIP_CODIGO = T.TIP_CODIGO
        WHERE 
            A.ATE_FACTURA_FECHA BETWEEN @FechaInicio AND @FechaFin
            AND A.ATE_FACTURA_PACIENTE NOT LIKE '%PRE%'
    )

    SELECT 
        a.ATE_CODIGO,
        a.ATE_NUMERO_ATENCION,
        a.HC AS HistoriaClinica,
        a.PACIENTE,
        a.SEXO,
        a.MEDICO_TRATANTE,
        s.ASEGURADORA AS Seguro,
        a.TIPO_ATENCION AS TipoAtencion,
        a.ATE_FECHA_INGRESO AS FechaIngreso,
        a.ATE_FECHA_ALTA AS FechaAlta,
        a.FACTURA AS NumeroFactura,
        a.FECHA_FACTURA AS FechaFactura,
        c.PRO_CODIGO AS ProCodigo,
        c.CUE_DETALLE AS CueDetalle,
        r.RUB_NOMBRE AS RubroNombre,
        c.CUE_CANTIDAD AS Cantidad,
        c.CUE_VALOR_UNITARIO AS ValorUnitario,
        c.CUE_VALOR AS ValorTotal,
        c.CUE_IVA AS Iva
    FROM Atencion a
    INNER JOIN CUENTAS_PACIENTES c ON a.ATE_CODIGO = c.ATE_CODIGO
    INNER JOIN RUBROS r ON c.RUB_CODIGO = r.RUB_CODIGO
    INNER JOIN Seguro s ON a.ATE_CODIGO = s.ATE_CODIGO
    WHERE c.CUE_CANTIDAD <> 0;
    ";

            var raw = await connection.QueryAsync<dynamic>(sql, new { FechaInicio = fechaInicio, FechaFin = fechaFin });

            var agrupado = raw
                    .GroupBy(r => (long)r.ATE_CODIGO)
                .Select(g =>
                {
                    var first = g.First();
                    return new FacturaPorAtencionResponse
                    {
                        AteCodigo = first.ATE_CODIGO,
                        AteNumero = first.ATE_NUMERO_ATENCION,
                        HistoriaClinica = first.HistoriaClinica,
                        Paciente = first.PACIENTE,
                        Sexo = first.SEXO,
                        MedicoTratante = first.MEDICO_TRATANTE,
                        Seguro = first.Seguro,
                        TipoAtencion = first.TipoAtencion,
                        FechaIngreso = first.FechaIngreso,
                        FechaAlta = first.FechaAlta,
                        NumeroFactura = first.NumeroFactura,
                        FechaFactura = first.FechaFactura,
                        Items = g.Select(item => new FacturaItem
                        {
                            ProCodigo = long.TryParse(item.ProCodigo?.ToString(), out long cod) ? cod : (long?)null,
                            RubroNombre = item.RubroNombre,
                            Cantidad = item.Cantidad,
                            Iva = item.Iva,
                            ValorUnitario = item.ValorUnitario,
                            ValorTotal = item.ValorTotal
                        }).ToList()
                    };
                }).ToList();


            return agrupado;
        }


        public async Task<List<ProcedimientosPorAtencionResponse>> ObtenerProcedimientosAsync(List<long> codigosAtencion)
        {
            var connectionString = _context.Database.GetConnectionString();
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var sql = @"
With
Procedimiento AS (
  -- Fuente 1: QUIROFANO_PROCE_PRODU + PROCEDIMIENTOS_CIRUGIA
  SELECT 
    q.ATE_CODIGO,
    ISNULL(QPP_FECHA, rq.fecha_registro) AS F_INTERVENCION,
    ISNULL(PCI_DESCRIPCION, rq.intervencion) AS PROCEDIMIENTO,
    ISNULL(QPP_CIRUJANO, rq.cirujano) AS CIRUJANO,
    ISNULL(CONVERT(VARCHAR(5), QPP_HORAINICIO, 108), CONVERT(VARCHAR(5), rq.hora_inicio, 108)) AS HORA_INICIO,
    ISNULL(CONVERT(VARCHAR(5), QPP_HORAFIN, 108), CONVERT(VARCHAR(5), rq.hora_fin, 108)) AS HORA_FIN,
    ISNULL(CONVERT(VARCHAR(8), QPP_DURACION, 108),
           CONCAT(
               ABS(DATEDIFF(HOUR, rq.hora_inicio, rq.hora_fin) % 24), ':',
               ABS(DATEDIFF(MINUTE, rq.hora_inicio, rq.hora_fin) % 60)
           )
    ) AS TIEMPO_TRANSCURRIDO
  FROM QUIROFANO_PROCE_PRODU q
  INNER JOIN PROCEDIMIENTOS_CIRUGIA p ON q.PCI_CODIGO = p.PCI_CODIGO
  LEFT JOIN REGISTRO_QUIROFANO rq ON q.ATE_CODIGO = rq.ATE_CODIGO
  WHERE q.ATE_CODIGO IS NOT NULL AND q.QPP_CIRUJANO IS NOT NULL AND q.QPP_CANTIDAD IS NULL

  UNION ALL

  -- Fuente 2: solo REGISTRO_QUIROFANO (si no está en QUIROFANO_PROCE_PRODU)
  SELECT 
    rq.ATE_CODIGO,
    rq.fecha_registro AS F_INTERVENCION,
    rq.intervencion AS PROCEDIMIENTO,
    rq.cirujano AS CIRUJANO,
    CONVERT(VARCHAR(5), rq.hora_inicio, 108) AS HORA_INICIO,
    CONVERT(VARCHAR(5), rq.hora_fin, 108) AS HORA_FIN,
    CONCAT(
      ABS(DATEDIFF(HOUR, rq.hora_inicio, rq.hora_fin) % 24), ':',
      ABS(DATEDIFF(MINUTE, rq.hora_inicio, rq.hora_fin) % 60)
    ) AS TIEMPO_TRANSCURRIDO
  FROM REGISTRO_QUIROFANO rq
  WHERE NOT EXISTS (
    SELECT 1 
    FROM QUIROFANO_PROCE_PRODU q 
    WHERE q.ATE_CODIGO = rq.ATE_CODIGO 
      AND q.QPP_CIRUJANO IS NOT NULL 
      AND q.QPP_CANTIDAD IS NULL
  )
),

Registro AS(select ate_codigo,fecha_registro,cirujano,hora_inicio,hora_fin,intervencion,
CONCAT(CASE WHEN DATEDIFF(HOUR,hora_inicio,hora_fin)%24 < 0 THEN DATEDIFF(HOUR,hora_fin,hora_inicio)%24 ELSE DATEDIFF(HOUR,hora_inicio,hora_fin)%24 END,
':',
CASE WHEN DATEDIFF(MINUTE,hora_inicio,hora_fin)%60 < 0 THEN DATEDIFF(MINUTE,hora_fin,hora_inicio)%60 ELSE DATEDIFF(MINUTE,hora_inicio,hora_fin)%60 END)
AS 'TIEMPO TRANSCURRIDO'
from REGISTRO_QUIROFANO),

Medico AS(SELECT M.MED_CODIGO,M.MED_APELLIDO_PATERNO+' '+M.MED_APELLIDO_MATERNO+' '+M.MED_NOMBRE1+' '+M.MED_NOMBRE2 AS 'MEDICO',
E.ESP_NOMBRE,T.TIM_NOMBRE FROM MEDICOS M 
INNER JOIN ESPECIALIDADES_MEDICAS E ON M.ESP_CODIGO = E.ESP_CODIGO
INNER JOIN TIPO_MEDICO T ON M.TIM_CODIGO = T.TIM_CODIGO)

select p.ATE_CODIGO,
ISNULL(P.F_INTERVENCION,R.fecha_registro)AS 'F_INTERVENCION',
ISNULL(M2.MEDICO,M.MEDICO) AS 'CIRUJANO',ISNULL(M2.ESP_NOMBRE,M.ESP_NOMBRE)AS 'ESPECIALIDAD',ISNULL(M2.TIM_NOMBRE,M.TIM_NOMBRE)AS'TIPO',
ISNULL(P.PROCEDIMIENTO,R.intervencion) AS 'PROCEDIMIENTO'
,ISNULL(P.HORA_INICIO,R.hora_inicio)AS 'HORA_INICIO',ISNULL(P.HORA_FIN,R.hora_fin) AS 'HORA_FIN', ISNULL(P.TIEMPO_TRANSCURRIDO,R.[TIEMPO TRANSCURRIDO]) AS 'TIEMPO_TRANSCURRIDO'

from Procedimiento p 
LEFT JOIN Registro R ON p.ATE_CODIGO = R.ate_codigo
LEFT join Medico M ON R.cirujano = M.MED_CODIGO
LEFT JOIN Medico M2 ON P.CIRUJANO = M2.MED_CODIGO;
    ";

            var result = await connection.QueryAsync<dynamic>(sql);

            return result.Select(r => new ProcedimientosPorAtencionResponse
            {
                AteCodigo = r.ATE_CODIGO,
                Cirujano = r.CIRUJANO,
                Especialidad = r.ESPECIALIDAD,
                TipoMedico = r.TIPO,
                Procedimiento = r.PROCEDIMIENTO,
                FechaIntervencion = r.F_INTERVENCION,
                HoraInicio = r.HORA_INICIO,
                HoraFin = r.HORA_FIN,
                TiempoTranscurrido = r.TIEMPO_TRANSCURRIDO
            }).ToList();
        }

        public async Task<List<EpicrisisPorAtencion>> ObtenerEpicrisisAsync(List<long> codigosAtencion)
        {
            var connectionString = _context.Database.GetConnectionString();
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var sql = @"
            Select ATE_CODIGO,
            EPI_CUADRO_CLINICO as 'CUADRO_CLINICO',
            EPI_EVOLUCION AS 'EVOLUCION',
            EPI_HALLAZGOS AS 'HALLAZGOS',
            EPI_TRATAMIENTO AS 'TRATAMIENTO', 
            EPI_CONDICIONES_EGRESO AS 'EGRESO'
            ,EPI_ALTA,
            EPI_CAUSAEXTERNA,
            EPI_PROXIMO_CONTROL,
            CASE WHEN EPI_ESTADO_EGRESO = 'V' THEN 'VIVO' WHEN EPI_ESTADO_EGRESO = 'F' THEN 'FALLECIDO' END  AS 'ESTADO_EGRESO',  
            CASE WHEN EPI_AMEDICA = 1 THEN 'SI'ELSE 'NO' END AS 'ALTA_MEDICA',CASE WHEN EPI_AVOLUNTARIA = 1 THEN 'SI'ELSE 'NO' END AS 'ALTA_VOLUNTARIA'
            from His3000..HC_EPICRISIS;
    ";

            var result = await connection.QueryAsync<dynamic>(sql);

            return result.Select(e => new EpicrisisPorAtencion
            {
                AteCodigo = e.ATE_CODIGO,
                CuadroClinico =  e.CUADRO_CLINICO,
                Tratamiento = e.TRATAMIENTO,
                Egreso = e.EGRESO,
                Alta = e.EPI_ALTA,
                CausaExterna = e.EPI_CAUSAEXTERNA,
                ProximoControl = e.EPI_PROXIMO_CONTROL,
                EstadoEgreso = e.ESTADO_EGRESO,
                AltaMedica = e.ALTA_MEDICA,
                AltaVoluntaria = e.ALTA_VOLUNTARIA
            }).ToList();
        }
    }
}
