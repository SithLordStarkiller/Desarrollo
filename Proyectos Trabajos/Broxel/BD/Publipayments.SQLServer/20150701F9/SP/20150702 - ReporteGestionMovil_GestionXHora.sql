
/****** Object:  StoredProcedure [dbo].[ReporteGestionMovil_GestionXHora]    Script Date: 02/07/2015 10:07:21 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 24/06/2015
-- Description:	obtiene los tres niveles de consulta para Reporte Gestion Movil: GestionXHora
-- =============================================
CREATE PROCEDURE [dbo].[ReporteGestionMovil_GestionXHora] 
	@delegacion VARCHAR(5), 
	@fechaCarga VARCHAR(50), 
	@resFinal VARCHAR(50), 
	@diaGestion VARCHAR(5), 
	@despacho VARCHAR(5), 
	@supervisor VARCHAR(5), 
	@tipoFormulario VARCHAR(5), 
	@tipoConsulta INT
AS
BEGIN
DECLARE @sql varchar(max)=''
	
	
	if @tipoConsulta = 1
	BEGIN
		
	set @sql='Declare @temp table(NombreDominio [varchar](100) ,idDominio int,valor32 int,valor0 int,
	valor1 int,valor2 int,valor3 int,valor4 int,valor5 int,valor6 int,valor7 int,valor8 int,valor9 int,valor10 int,valor11 int,
	valor12 int,valor13 int,valor14 int,valor15 int,valor16 int,valor17 int,valor18 int,valor19 int,valor20 int,valor21 int,valor22 int,
	valor23 int,valor24 int,valor25 int,valor26 int,valor27 int,valor28 int,valor29 int,valor30 int,valor31 int);'
	
		set @sql+=' Insert into @temp
		SELECT d.NombreDominio, Sumarizada.* FROM ( SELECT CASE  WHEN (GROUPING(idDominio) = 1) THEN - 1 ELSE idDominio END idDominio, 
		SUM([valor32]) [valor32], SUM([valor0]) [valor0], SUM([valor1]) [valor1], SUM([valor2]) [valor2], SUM([valor3]) [valor3], SUM([valor4]) [valor4], 
		SUM([valor5]) [valor5], SUM([valor6]) [valor6], SUM([valor7]) [valor7], SUM([valor8]) [valor8], SUM([valor9]) [valor9], SUM([valor10]) [valor10], 
		SUM([valor11]) [valor11], SUM([valor12]) [valor12], SUM([valor13]) [valor13], SUM([valor14]) [valor14], SUM([valor15]) [valor15], SUM([valor16]) [valor16],
		SUM([valor17]) [valor17], SUM([valor18]) [valor18], SUM([valor19]) [valor19], SUM([valor20]) [valor20], SUM([valor21]) [valor21], SUM([valor22]) [valor22], 
		SUM([valor23]) [valor23], SUM([valor24]) [valor24], SUM([valor25]) [valor25], SUM([valor26]) [valor26], SUM([valor27]) [valor27], SUM([valor28]) [valor28], 
		SUM([valor29]) [valor29], SUM([valor30]) [valor30], SUM([valor31]) [valor31] FROM ( SELECT idDominio, isnull([100], 0) [valor32], isnull([0], 0) [valor0], 
		isnull([1], 0) [valor1], isnull([2], 0) [valor2], isnull([3], 0) [valor3], isnull([4], 0) [valor4], isnull([5], 0) [valor5], isnull([6], 0) [valor6], 
		isnull([7], 0) [valor7], isnull([8], 0) [valor8], isnull([9], 0) [valor9], isnull([10], 0) [valor10], isnull([11], 0) [valor11], isnull([12], 0) [valor12], 
		isnull([13], 0) [valor13], isnull([14], 0) [valor14], isnull([15], 0) [valor15], isnull([16], 0) [valor16], isnull([17], 0) [valor17], isnull([18], 0) [valor18], 
		isnull([19], 0) [valor19], isnull([20], 0) [valor20], isnull([21], 0) [valor21], isnull([22], 0) [valor22], isnull([23], 0) [valor23], isnull([24], 0) [valor24], 
		isnull([25], 0) [valor25], isnull([26], 0) [valor26], isnull([27], 0) [valor27], isnull([28], 0) [valor28], isnull([29], 0) [valor29], isnull([30], 0) [valor30], 
		isnull([31], 0) [valor31] FROM ( SELECT idDominio, valor, ISNULL(horaFinalDate, 100) AS horaFinalDate FROM ReporteGestionMovil WITH (NOLOCK) WHERE 1 = 1 
		and Ruta='''+@tipoFormulario+''' '+
		case  when @delegacion='null' then ' AND CV_DELEGACION is null ' when @delegacion<>'' then ' AND CV_DELEGACION=''' + @delegacion + ''' ' else '' end +
		case  when @fechaCarga='null' then ' AND fechaCarga is null ' when @fechaCarga<>'' then ' AND convert(varchar(10),fechaCarga,103)+'' '' +convert(varchar(5), fechaCarga,108)=''' + @fechaCarga + ''' ' else '' end +
		case  when @resFinal='null' then ' AND resFinal is null ' when @resFinal<>'' then ' AND resFinal=''' + @resFinal + ''' ' else '' end +
		case  when @diaGestion='null' then ' AND DiaFinalDate is null ' when @diaGestion<>'' then ' AND DiaFinalDate=''' + @diaGestion + ''' ' else '' end +		
		' ) source pivot(sum(source.valor) FOR source.horaFinalDate IN ( [100],[0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],
		[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31] )) AS pivottable ) pivote GROUP BY idDominio WITH ROLLUP ) Sumarizada LEFT JOIN Dominio d 
		ON Sumarizada.idDominio = d.idDominio;'
		set @sql+=' select * from @temp where idDominio<>-1; 
		select idDominio ,valor32 ,valor0 ,	valor1 ,valor2 ,valor3 ,valor4 ,valor5 ,valor6 ,valor7 ,valor8 ,valor9 ,valor10 ,valor11 ,
	valor12 ,valor13 ,valor14 ,valor15 ,valor16 ,valor17 ,valor18 ,valor19 ,valor20 ,valor21 ,valor22 ,
	valor23 ,valor24 ,valor25 ,valor26 ,valor27 ,valor28 ,valor29 ,valor30 ,valor31 from @temp where idDominio=-1;'
		
	END
	
	if @tipoConsulta = 2
	BEGIN
		
	set @sql='Declare @temp table(idUsuarioPadre int,Nombre [varchar](100),Usuario [varchar](50),valor32 int,valor0 int,
	valor1 int,valor2 int,valor3 int,valor4 int,valor5 int,valor6 int,valor7 int,valor8 int,valor9 int,valor10 int,valor11 int,
	valor12 int,valor13 int,valor14 int,valor15 int,valor16 int,valor17 int,valor18 int,valor19 int,valor20 int,valor21 int,valor22 int,
	valor23 int,valor24 int,valor25 int,valor26 int,valor27 int,valor28 int,valor29 int,valor30 int,valor31 int);'
	
		set @sql+=' Insert into @temp
		SELECT Sumarizada.idUsuarioPadre,isnull(u.Nombre,''Sin Asignar'') as Nombre,isnull(u.Usuario,''---'') as Usuario, Sumarizada.valor32, Sumarizada.valor0, Sumarizada.valor1, Sumarizada.valor2,
 Sumarizada.valor3, Sumarizada.valor4, Sumarizada.valor5, Sumarizada.valor6, Sumarizada.valor7, Sumarizada.valor8, 
 Sumarizada.valor9, Sumarizada.valor10, Sumarizada.valor11, Sumarizada.valor12, Sumarizada.valor13, Sumarizada.valor14,
  Sumarizada.valor15, Sumarizada.valor16, Sumarizada.valor17, Sumarizada.valor18, Sumarizada.valor19, Sumarizada.valor20, 
  Sumarizada.valor21, Sumarizada.valor22, Sumarizada.valor23, Sumarizada.valor24, Sumarizada.valor25, Sumarizada.valor26, 
  Sumarizada.valor27, Sumarizada.valor28, Sumarizada.valor29, Sumarizada.valor30, Sumarizada.valor31
		FROM ( SELECT CASE  WHEN (GROUPING(idUsuarioPadre) = 1) THEN - 1 ELSE idUsuarioPadre END idUsuarioPadre, 
		SUM([valor32]) [valor32], SUM([valor0]) [valor0], SUM([valor1]) [valor1], SUM([valor2]) [valor2], SUM([valor3]) [valor3], SUM([valor4]) [valor4], 
		SUM([valor5]) [valor5], SUM([valor6]) [valor6], SUM([valor7]) [valor7], SUM([valor8]) [valor8], SUM([valor9]) [valor9], SUM([valor10]) [valor10], 
		SUM([valor11]) [valor11], SUM([valor12]) [valor12], SUM([valor13]) [valor13], SUM([valor14]) [valor14], SUM([valor15]) [valor15], SUM([valor16]) [valor16],
		SUM([valor17]) [valor17], SUM([valor18]) [valor18], SUM([valor19]) [valor19], SUM([valor20]) [valor20], SUM([valor21]) [valor21], SUM([valor22]) [valor22], 
		SUM([valor23]) [valor23], SUM([valor24]) [valor24], SUM([valor25]) [valor25], SUM([valor26]) [valor26], SUM([valor27]) [valor27], SUM([valor28]) [valor28], 
		SUM([valor29]) [valor29], SUM([valor30]) [valor30], SUM([valor31]) [valor31] FROM ( SELECT idUsuarioPadre, isnull([100], 0) [valor32], isnull([0], 0) [valor0], 
		isnull([1], 0) [valor1], isnull([2], 0) [valor2], isnull([3], 0) [valor3], isnull([4], 0) [valor4], isnull([5], 0) [valor5], isnull([6], 0) [valor6], 
		isnull([7], 0) [valor7], isnull([8], 0) [valor8], isnull([9], 0) [valor9], isnull([10], 0) [valor10], isnull([11], 0) [valor11], isnull([12], 0) [valor12], 
		isnull([13], 0) [valor13], isnull([14], 0) [valor14], isnull([15], 0) [valor15], isnull([16], 0) [valor16], isnull([17], 0) [valor17], isnull([18], 0) [valor18], 
		isnull([19], 0) [valor19], isnull([20], 0) [valor20], isnull([21], 0) [valor21], isnull([22], 0) [valor22], isnull([23], 0) [valor23], isnull([24], 0) [valor24], 
		isnull([25], 0) [valor25], isnull([26], 0) [valor26], isnull([27], 0) [valor27], isnull([28], 0) [valor28], isnull([29], 0) [valor29], isnull([30], 0) [valor30], 
		isnull([31], 0) [valor31] FROM ( SELECT idUsuarioPadre, valor, ISNULL(horaFinalDate, 100) AS horaFinalDate FROM ReporteGestionMovil WITH (NOLOCK) WHERE 1 = 1 
		and Ruta='''+@tipoFormulario+''' '+
		case  when @delegacion='null' then ' AND CV_DELEGACION is null ' when @delegacion<>'' then ' AND CV_DELEGACION=''' + @delegacion + ''' ' else '' end +
		case  when @fechaCarga='null' then ' AND fechaCarga is null ' when @fechaCarga<>'' then ' AND convert(varchar(10),fechaCarga,103)+'' '' +convert(varchar(5), fechaCarga,108)=''' + @fechaCarga + ''' ' else '' end +
		case  when @resFinal='null' then ' AND resFinal is null ' when @resFinal<>'' then ' AND resFinal=''' + @resFinal + ''' ' else '' end +
		case  when @diaGestion='null' then ' AND DiaFinalDate is null ' when @diaGestion<>'' then ' AND DiaFinalDate=''' + @diaGestion + ''' ' else '' end +		
		case  when @despacho='null' then ' AND idDominio is null ' when @despacho<>'' then ' AND idDominio=''' + @despacho + ''' ' else '' end +		
		' ) source pivot(sum(source.valor) FOR source.horaFinalDate IN ( [100],[0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],
		[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31] )) AS pivottable ) pivote GROUP BY idUsuarioPadre WITH ROLLUP ) Sumarizada 
		left join Usuario u on u.idUsuario=Sumarizada.idUsuarioPadre;'
		set @sql+=' select * from @temp where isnull(idUsuarioPadre,0)<>-1; 
		select idUsuarioPadre ,valor32 ,valor0 ,	valor1 ,valor2 ,valor3 ,valor4 ,valor5 ,valor6 ,valor7 ,valor8 ,valor9 ,valor10 ,valor11 ,
	valor12 ,valor13 ,valor14 ,valor15 ,valor16 ,valor17 ,valor18 ,valor19 ,valor20 ,valor21 ,valor22 ,
	valor23 ,valor24 ,valor25 ,valor26 ,valor27 ,valor28 ,valor29 ,valor30 ,valor31 from @temp where idUsuarioPadre=-1;'
		
	END
	
	if @tipoConsulta = 3
	BEGIN
		
	set @sql='Declare @temp table(idUsuario int,Nombre [varchar](100),Usuario [varchar](50),valor32 int,valor0 int,
	valor1 int,valor2 int,valor3 int,valor4 int,valor5 int,valor6 int,valor7 int,valor8 int,valor9 int,valor10 int,valor11 int,
	valor12 int,valor13 int,valor14 int,valor15 int,valor16 int,valor17 int,valor18 int,valor19 int,valor20 int,valor21 int,valor22 int,
	valor23 int,valor24 int,valor25 int,valor26 int,valor27 int,valor28 int,valor29 int,valor30 int,valor31 int);'
	
		set @sql+=' Insert into @temp
		SELECT Sumarizada.idUsuario,isnull(u.Nombre,''Sin Asignar'') as Nombre,isnull(u.Usuario,''---'') as Usuario, Sumarizada.valor32, Sumarizada.valor0, Sumarizada.valor1, Sumarizada.valor2,
 Sumarizada.valor3, Sumarizada.valor4, Sumarizada.valor5, Sumarizada.valor6, Sumarizada.valor7, Sumarizada.valor8, 
 Sumarizada.valor9, Sumarizada.valor10, Sumarizada.valor11, Sumarizada.valor12, Sumarizada.valor13, Sumarizada.valor14,
  Sumarizada.valor15, Sumarizada.valor16, Sumarizada.valor17, Sumarizada.valor18, Sumarizada.valor19, Sumarizada.valor20, 
  Sumarizada.valor21, Sumarizada.valor22, Sumarizada.valor23, Sumarizada.valor24, Sumarizada.valor25, Sumarizada.valor26, 
  Sumarizada.valor27, Sumarizada.valor28, Sumarizada.valor29, Sumarizada.valor30, Sumarizada.valor31
		FROM ( SELECT CASE  WHEN (GROUPING(idUsuario) = 1) THEN - 1 ELSE idUsuario END idUsuario, 
		SUM([valor32]) [valor32], SUM([valor0]) [valor0], SUM([valor1]) [valor1], SUM([valor2]) [valor2], SUM([valor3]) [valor3], SUM([valor4]) [valor4], 
		SUM([valor5]) [valor5], SUM([valor6]) [valor6], SUM([valor7]) [valor7], SUM([valor8]) [valor8], SUM([valor9]) [valor9], SUM([valor10]) [valor10], 
		SUM([valor11]) [valor11], SUM([valor12]) [valor12], SUM([valor13]) [valor13], SUM([valor14]) [valor14], SUM([valor15]) [valor15], SUM([valor16]) [valor16],
		SUM([valor17]) [valor17], SUM([valor18]) [valor18], SUM([valor19]) [valor19], SUM([valor20]) [valor20], SUM([valor21]) [valor21], SUM([valor22]) [valor22], 
		SUM([valor23]) [valor23], SUM([valor24]) [valor24], SUM([valor25]) [valor25], SUM([valor26]) [valor26], SUM([valor27]) [valor27], SUM([valor28]) [valor28], 
		SUM([valor29]) [valor29], SUM([valor30]) [valor30], SUM([valor31]) [valor31] FROM ( SELECT idUsuario, isnull([100], 0) [valor32], isnull([0], 0) [valor0], 
		isnull([1], 0) [valor1], isnull([2], 0) [valor2], isnull([3], 0) [valor3], isnull([4], 0) [valor4], isnull([5], 0) [valor5], isnull([6], 0) [valor6], 
		isnull([7], 0) [valor7], isnull([8], 0) [valor8], isnull([9], 0) [valor9], isnull([10], 0) [valor10], isnull([11], 0) [valor11], isnull([12], 0) [valor12], 
		isnull([13], 0) [valor13], isnull([14], 0) [valor14], isnull([15], 0) [valor15], isnull([16], 0) [valor16], isnull([17], 0) [valor17], isnull([18], 0) [valor18], 
		isnull([19], 0) [valor19], isnull([20], 0) [valor20], isnull([21], 0) [valor21], isnull([22], 0) [valor22], isnull([23], 0) [valor23], isnull([24], 0) [valor24], 
		isnull([25], 0) [valor25], isnull([26], 0) [valor26], isnull([27], 0) [valor27], isnull([28], 0) [valor28], isnull([29], 0) [valor29], isnull([30], 0) [valor30], 
		isnull([31], 0) [valor31] FROM ( SELECT idUsuario, valor, ISNULL(horaFinalDate, 100) AS horaFinalDate FROM ReporteGestionMovil WITH (NOLOCK) WHERE 1 = 1 
		and Ruta='''+@tipoFormulario+''' '+
		case  when @delegacion='null' then ' AND CV_DELEGACION is null ' when @delegacion<>'' then ' AND CV_DELEGACION=''' + @delegacion + ''' ' else '' end +
		case  when @fechaCarga='null' then ' AND fechaCarga is null ' when @fechaCarga<>'' then ' AND convert(varchar(10),fechaCarga,103)+'' '' +convert(varchar(5), fechaCarga,108)=''' + @fechaCarga + ''' ' else '' end +
		case  when @resFinal='null' then ' AND resFinal is null ' when @resFinal<>'' then ' AND resFinal=''' + @resFinal + ''' ' else '' end +
		case  when @diaGestion='null' then ' AND DiaFinalDate is null ' when @diaGestion<>'' then ' AND DiaFinalDate=''' + @diaGestion + ''' ' else '' end +		
		case  when @despacho='null' then ' AND idDominio is null ' when @despacho<>'' then ' AND idDominio=''' + @despacho + ''' ' else '' end +		
		case  when @supervisor='null' then ' AND idUsuarioPadre is null ' when @supervisor<>'' then ' AND idUsuarioPadre=''' + @supervisor + ''' ' else '' end +		
		' ) source pivot(sum(source.valor) FOR source.horaFinalDate IN ( [100],[0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],
		[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31] )) AS pivottable ) pivote GROUP BY idUsuario WITH ROLLUP ) Sumarizada 
		left join Usuario u on u.idUsuario=Sumarizada.idUsuario;'
		set @sql+=' select * from @temp where isnull(idUsuario,0)<>-1; 
		select idUsuario ,valor32 ,valor0 ,	valor1 ,valor2 ,valor3 ,valor4 ,valor5 ,valor6 ,valor7 ,valor8 ,valor9 ,valor10 ,valor11 ,
	valor12 ,valor13 ,valor14 ,valor15 ,valor16 ,valor17 ,valor18 ,valor19 ,valor20 ,valor21 ,valor22 ,
	valor23 ,valor24 ,valor25 ,valor26 ,valor27 ,valor28 ,valor29 ,valor30 ,valor31 from @temp where idUsuario=-1;'
		
	END
	
	
	EXECUTE sp_sqlexec @sql
	
	
	
END
