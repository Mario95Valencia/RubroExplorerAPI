using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using vistarubros.Application.Records.Response;
using vistarubros.Domain.Interfaces.IDomainServices;
using vistarubros.Infrastructure.Persistence.Context;

namespace vistarubros.Domain.Services
{
    public class PacienteAtencionDomainService : IPacienteAtencionDomainService
    {
        private readonly ApplicationDbContext _context;

        public PacienteAtencionDomainService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PacienteProcedimientoDetalleResponse>> ObtenerDetallePorRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            using var connection = _context.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
                await connection.OpenAsync();

            var sql = @"WITH 
Seguro AS(select  ATE_CODIGO,STRING_AGG(CC.CAT_NOMBRE,', ') AS 'ASEGURADORA' from ATENCION_DETALLE_CATEGORIAS ADC
INNER JOIN CATEGORIAS_CONVENIOS CC ON ADC.CAT_CODIGO = CC.CAT_CODIGO
GROUP BY ATE_CODIGO),

Atencion AS  (select A.ATE_CODIGO, p.PAC_APELLIDO_PATERNO+' '+p.PAC_APELLIDO_MATERNO+' '+p.PAC_NOMBRE1+' '+p.PAC_NOMBRE2 as 'PACIENTE',T.TIP_DESCRIPCION
,P.PAC_GENERO AS SEXO,M.MED_APELLIDO_PATERNO+' '+M.MED_APELLIDO_MATERNO+' '+M.MED_NOMBRE1+' '+M.MED_NOMBRE2 AS 'MEDICO_TRATANTE'
,P.PAC_HISTORIA_CLINICA AS 'HC' ,a.ATE_NUMERO_ATENCION as 'ATENCION',a.ATE_FECHA_INGRESO as 'FECHA INGRESO',a.ATE_FECHA_ALTA as 'FECHA ALTA'
, a.ATE_FACTURA_PACIENTE AS 'FACTURA', a.ATE_FACTURA_FECHA AS 'FACTURA FECHA' 
, c.PRO_CODIGO ,c.CUE_DETALLE ,r.RUB_NOMBRE,
c.CUE_CANTIDAD,c.CUE_VALOR_UNITARIO,c.CUE_VALOR ,c.CUE_IVA 
from  ATENCIONES A 
INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO
inner join CUENTAS_PACIENTES C on A.ATE_CODIGO = C.ATE_CODIGO
inner join MEDICOS m on A.MED_CODIGO = m.MED_CODIGO
INNER JOIN RUBROS R  ON C.RUB_CODIGO = R.RUB_CODIGO
INNER JOIN TIPO_INGRESO  T ON A.TIP_CODIGO = T.TIP_CODIGO
where 
C.CUE_CANTIDAD <> 0
AND a.ATE_FACTURA_FECHA BETWEEN @FechaInicio AND @FechaFin
AND a.ATE_FACTURA_PACIENTE NOT LIKE '%PRE%'),

Procedimiento AS (select ATE_CODIGO, ISNULL(QPP_FECHA,(select fecha_registro from REGISTRO_QUIROFANO where ATE_CODIGO = Q.ATE_CODIGO)) AS 'F_INTERVENCION',
isnull(PCI_DESCRIPCION,(select intervencion from REGISTRO_QUIROFANO where ATE_CODIGO = Q.ATE_CODIGO)) as 'PROCEDIMIENTO',
isnull(QPP_CIRUJANO ,(select cirujano from REGISTRO_QUIROFANO where ATE_CODIGO = Q.ATE_CODIGO)) AS 'CIRUJANO'
,ISNULL(CONVERT(VARCHAR(5), q.QPP_HORAINICIO , 108 ), (SELECT  CONVERT(VARCHAR(5), hora_inicio, 108) 
     FROM REGISTRO_QUIROFANO  WHERE ATE_CODIGO = Q.ATE_CODIGO)) AS 'HORA_INICIO',
ISNULL(CONVERT(VARCHAR(5), QPP_HORAFIN, 108),(SELECT CONVERT(VARCHAR(5), hora_fin, 108) 
     FROM REGISTRO_QUIROFANO WHERE ATE_CODIGO = Q.ATE_CODIGO)) AS 'HORA_FIN'
,ISNULL(CONVERT(VARCHAR(8), QPP_DURACION, 108),
(SELECT CONCAT(CASE WHEN DATEDIFF(HOUR,hora_inicio,hora_fin)%24 < 0 THEN DATEDIFF(HOUR,hora_fin,hora_inicio)%24 ELSE DATEDIFF(HOUR,hora_inicio,hora_fin)%24 END,
':',CASE WHEN DATEDIFF(MINUTE,hora_inicio,hora_fin)%60 < 0 THEN DATEDIFF(MINUTE,hora_fin,hora_inicio)%60 ELSE DATEDIFF(MINUTE,hora_inicio,hora_fin)%60 END)
FROM REGISTRO_QUIROFANO WHERE ate_codigo = Q.ATE_CODIGO)) AS TIEMPO_TRANSCURRIDO

from  QUIROFANO_PROCE_PRODU q inner join PROCEDIMIENTOS_CIRUGIA p on q.PCI_CODIGO = p.PCI_CODIGO 
where ATE_CODIGO IS NOT NULL AND QPP_CIRUJANO IS NOT NULL and QPP_CANTIDAD is null),

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

select 
a.ATE_CODIGO,A.PACIENTE,A.SEXO,A.MEDICO_TRATANTE,A.HC,A.TIP_DESCRIPCION AS 'T_ATENCION',A.ATENCION,A.FACTURA,A.[FACTURA FECHA],A.PRO_CODIGO,A.CUE_DETALLE,A.RUB_NOMBRE,CUE_CANTIDAD
,A.CUE_VALOR_UNITARIO,A.CUE_VALOR,A.CUE_IVA,S.ASEGURADORA AS SEGURO,ISNULL(P.F_INTERVENCION,R.fecha_registro)AS 'F_INTERVENCION',
ISNULL(M2.MEDICO,M.MEDICO) AS 'CIRUJANO',ISNULL(M2.ESP_NOMBRE,M.ESP_NOMBRE)AS 'ESPECIALIDAD',ISNULL(M2.TIM_NOMBRE,M.TIM_NOMBRE)AS'TIPO',
ISNULL(P.PROCEDIMIENTO,R.intervencion) AS 'PROCEDIMIENTO'
,ISNULL(P.HORA_INICIO,R.hora_inicio)AS 'HORA_INICIO',ISNULL(P.HORA_FIN,R.hora_fin) AS 'HORA_FIN', ISNULL(P.TIEMPO_TRANSCURRIDO,R.[TIEMPO TRANSCURRIDO]) AS 'TIEMPO_TRANSCURRIDO' 
from Atencion a 
inner join Seguro s ON a.ATE_CODIGO = s.ATE_CODIGO
left join Procedimiento p ON a.ATE_CODIGO = p.ATE_CODIGO
LEFT JOIN Registro R ON A.ATE_CODIGO = R.ate_codigo
LEFT join Medico M ON R.cirujano = M.MED_CODIGO
LEFT JOIN Medico M2 ON P.CIRUJANO = M2.MED_CODIGO";

            // Ejecutar la consulta original, reutilizando el modelo plano anterior
            var result = await connection.QueryAsync<PacienteProcedimientoResponse>(sql, new { FechaInicio = fechaInicio, FechaFin = fechaFin });

            // Agrupar y transformar la estructura
            var agrupado = result
                    .GroupBy(r => r.ATE_CODIGO)
                    .Select(grupo =>
                    {
                        var primero = grupo.First();

                        return new PacienteProcedimientoDetalleResponse
                        {
                            NumeroAtencion = primero.ATENCION,
                            HistoriaClinica = primero.HC,
                            Paciente = primero.PACIENTE,
                            Sexo = primero.SEXO,
                            MedicoTratante = primero.MEDICO_TRATANTE,
                            Seguro = primero.SEGURO,
                            TipoAtencion = primero.T_ATENCION,
                            FechaIngreso = primero.FECHA_INGRESO,
                            FechaAlta = primero.FECHA_ALTA,

                            DetalleEpicrisis = new DetalleEpicrisis
                            {
                                DiagnosticoPrincipal = primero.DIAG_PRINCIPAL ?? "XXXX",                                
                                Complicaciones = primero.COMPLICACIONES ?? "Ninguna",
                                Observaciones = primero.OBSERVACIONES ?? "Procedimiento realizado sin novedades."
                            },

                            Procedimientos = grupo
                                .GroupBy(p => new
                                {
                                    p.PROCEDIMIENTO,
                                    p.F_INTERVENCION,
                                    p.CIRUJANO,
                                    p.HORA_INICIO,
                                    p.HORA_FIN,
                                    p.ESPECIALIDAD,
                                    p.TIPO
                                })
                                .Select(proc =>
                                {
                                    var pr = proc.First();
                                    return new ProcedimientoItem
                                    {
                                        FechaIntervencion = pr.F_INTERVENCION,
                                        Procedimiento = pr.PROCEDIMIENTO,
                                        Cirujano = pr.CIRUJANO,
                                        Especialidad = pr.ESPECIALIDAD,
                                        TipoMedico = pr.TIPO,
                                        HoraInicio = pr.HORA_INICIO,
                                        HoraFin = pr.HORA_FIN,
                                        TiempoTranscurrido = pr.TIEMPO_TRANSCURRIDO,
                                    };
                                }).ToList(),

                            Factura = new FacturaDetalle
                            {
                                NumeroFactura = primero.FACTURA,
                                FechaFactura = primero.FACTURA_FECHA,
                                SubtotalClinica = grupo.Sum(g => g.CUE_VALOR ?? 0),
                                IvaTotal = grupo.Sum(g => g.CUE_IVA ?? 0),
                                Total = grupo.Sum(g => g.CUE_VALOR ?? 0),
                                Items = grupo.Select(r => new FacturaItem
                                {
                                    ProCodigo = r.PRO_CODIGO,
                                    RubroNombre = r.RUB_NOMBRE,
                                    Cantidad = r.CUE_CANTIDAD,
                                    Iva = r.CUE_IVA,
                                    ValorUnitario = r.CUE_VALOR_UNITARIO,
                                    ValorTotal = r.CUE_VALOR
                                }).ToList()
                            }
                        };
                    }).ToList();
            return agrupado;
        }
    }
}
