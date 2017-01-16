
/****** Object:  StoredProcedure [dbo].[ReporteGestionMovil_Soluciones]    Script Date: 02/07/2015 10:07:34 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 24/06/2015
-- Description:	obtiene  consulta para Reporte Gestion Movil: Soluciones
-- =============================================
CREATE PROCEDURE [dbo].[ReporteGestionMovil_Soluciones] 
	@delegacion VARCHAR(5), 
	@fechaCarga VARCHAR(50), 
	@despacho VARCHAR(5), 
	@supervisor VARCHAR(5), 
	@tipoFormulario VARCHAR(5)
AS
BEGIN
DECLARE @sql varchar(max)=''
	
	
	
	set @sql='Declare @temp table(idCampo int,Valor [varchar](100),valor32 int,valor0 int,
	valor1 int,valor2 int,valor3 int,valor4 int,valor5 int,valor6 int);'
	
		set @sql+=' Insert into @temp
		SELECT  Sumarizada.estFinal as idCampo,isnull(r.Valor, ''Sin Dictamen'') Valor,Sumarizada.valor32,Sumarizada.valor0,Sumarizada.valor1,Sumarizada.valor2
		,Sumarizada.valor3,Sumarizada.valor4,Sumarizada.valor5,Sumarizada.valor6 FROM ( SELECT CASE  WHEN (GROUPING(estFinal) = 1) THEN - 1 ELSE estFinal END estFinal, 
		SUM([valor32]) [valor32], SUM([valor0]) [valor0], SUM([valor1]) [valor1], SUM([valor2]) [valor2], SUM([valor3]) [valor3], SUM([valor4]) [valor4], 
		SUM([valor5]) [valor5], SUM([valor6]) [valor6] FROM ( SELECT estFinal , ISNULL([100], 0) AS [valor32] , 
		ISNULL([Movil], 0) AS [valor0] , ISNULL([Sin Asignar], 0) AS [valor1] ,
		ISNULL([Sin Asignar 2 Visita], 0) AS [valor2] , ISNULL([Sin asignar 3 Visita], 0) 
		AS [valor3] , ISNULL([Validas], 0) AS [valor4] , ISNULL([Validas Aprobadas], 0) 
		AS [valor5] , ISNULL([Validas Sin Aprobar], 0) AS [valor6] FROM ( SELECT estFinal, valor, ISNULL(resFinal, 100) AS resFinal FROM ReporteGestionMovil WITH (NOLOCK) WHERE 1 = 1 
		and Ruta='''+@tipoFormulario+''' '+
		case  when @delegacion='null' then ' AND CV_DELEGACION is null ' when @delegacion<>'' then ' AND CV_DELEGACION=''' + @delegacion + ''' ' else '' end +
		case  when @fechaCarga='null' then ' AND fechaCarga is null ' when @fechaCarga<>'' then ' AND convert(varchar(10),fechaCarga,103)+'' '' +convert(varchar(5), fechaCarga,108)=''' + @fechaCarga + ''' ' else '' end +
		case  when @despacho='null' then ' AND idDominio is null ' when @despacho<>'' then ' AND idDominio=''' + @despacho + ''' ' else '' end +		
		case  when @supervisor='null' then ' AND idUsuarioPadre is null ' when @supervisor<>'' then ' AND idUsuarioPadre=''' + @supervisor + ''' ' else '' end +		
		' ) source pivot(sum(source.valor) FOR source.resFinal IN ([100], [Movil], 
		[Sin Asignar], [Sin Asignar 2 Visita], [Sin asignar 3 Visita], [Validas], 
		[Validas Aprobadas], [Validas Sin Aprobar] )) AS pivottable ) pivote GROUP BY estFinal WITH ROLLUP ) Sumarizada 
		left join(select * from CatDictamenRespuesta) r on r.idCampo=Sumarizada.estFinal
		order by 2;'
		set @sql+=' select * from @temp where isnull(idCampo,0)<>-1 order by 2; 
		select idCampo ,valor32 ,valor0 ,	valor1 ,valor2 ,valor3 ,valor4 ,valor5 ,valor6 from @temp where idCampo=-1;'
	
	
	EXECUTE sp_sqlexec @sql
	
	
	
END
