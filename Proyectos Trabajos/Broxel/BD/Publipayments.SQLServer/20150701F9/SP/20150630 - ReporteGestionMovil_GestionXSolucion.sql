
/****** Object:  StoredProcedure [dbo].[ReporteGestionMovil_GestionXSolucion]    Script Date: 02/07/2015 10:07:27 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 24/06/2015
-- Description:	obtiene los tres niveles de consulta para Reporte Gestion Movil: GestionXSolucion
-- =============================================
CREATE PROCEDURE [dbo].[ReporteGestionMovil_GestionXSolucion] 
	@delegacion VARCHAR(5), 
	@fechaCarga VARCHAR(50), 
	@estFinal VARCHAR(500), 
	@despacho VARCHAR(5), 
	@supervisor VARCHAR(5), 
	@tipoFormulario VARCHAR(5), 
	@tipoConsulta INT
AS
BEGIN
DECLARE @sql varchar(max)=''
	
	
	if @tipoConsulta = 1
	BEGIN		
		set @sql='Declare @temp table(idDominio int,NombreDominio [varchar](100) ,valor32 int,valor0 int,
		valor1 int,valor2 int,valor3 int,valor4 int,valor5 int,valor6 int);'
	
		set @sql+=' Insert into @temp
		SELECT Sumarizada.idDominio,d.nombreDominio as NombreDominio, Sumarizada.valor32, Sumarizada.valor0, Sumarizada.valor1, Sumarizada.valor2, Sumarizada.valor3
		, Sumarizada.valor4, Sumarizada.valor5, Sumarizada.valor6
		FROM ( SELECT CASE  WHEN (GROUPING(idDominio) = 1) THEN - 1 ELSE idDominio END idDominio, 
		SUM([valor32]) [valor32], SUM([valor0]) [valor0], SUM([valor1]) [valor1], SUM([valor2]) [valor2], SUM([valor3]) [valor3],
		 SUM([valor4]) [valor4], 
		SUM([valor5]) [valor5], SUM([valor6]) [valor6] FROM ( SELECT idDominio, ISNULL([100],0) as [valor32],ISNULL([Movil],0) as [valor0],
        ISNULL([Sin Asignar],0) as [valor1],ISNULL([Sin Asignar 2 Visita],0) as [valor2],
        ISNULL([Sin asignar 3 Visita],0) as [valor3],ISNULL([Validas],0) as [valor4],
        ISNULL([Validas Aprobadas],0) as [valor5],ISNULL([Validas Sin Aprobar],0) as [valor6] FROM ( SELECT idDominio, valor, ISNULL(resFinal, 100) AS resFinal FROM ReporteGestionMovil WITH (NOLOCK) WHERE 1 = 1 
		and Ruta='''+@tipoFormulario+''' '+
		case  when @delegacion='null' then ' AND CV_DELEGACION is null ' when @delegacion<>'' then ' AND CV_DELEGACION=''' + @delegacion + ''' ' else '' end +
		case  when @fechaCarga='null' then ' AND fechaCarga is null ' when @fechaCarga<>'' then ' AND convert(varchar(10),fechaCarga,103)+'' '' +convert(varchar(5), fechaCarga,108)=''' + @fechaCarga + ''' ' else '' end +
		case  when @estFinal='null' then ' AND estFinal is null ' when @estFinal<>'' then ' AND estFinal in(' + @estFinal + ') ' else '' end +
		' ) source pivot(sum(source.valor) FOR source.resFinal IN ( [100],[Movil],[Sin Asignar], [Sin Asignar 2 Visita],
                      [Sin asignar 3 Visita],[Validas],[Validas Aprobadas],[Validas Sin Aprobar] )) AS pivottable ) pivote GROUP BY idDominio WITH ROLLUP ) Sumarizada 
		left join Dominio d on d.idDominio=Sumarizada.idDominio;'
		set @sql+=' select * from @temp where idDominio<>-1; 
		select idDominio ,valor32 ,valor0 ,	valor1 ,valor2 ,valor3 ,valor4 ,valor5 ,valor6 from @temp where idDominio=-1;'
		
	END
	
	if @tipoConsulta = 2
	BEGIN		
		set @sql='Declare @temp table(idUsuarioPadre int,Nombre [varchar](100),Usuario [varchar](100) ,valor32 int,valor0 int,
		valor1 int,valor2 int,valor3 int,valor4 int,valor5 int,valor6 int);'
	
		set @sql+=' Insert into @temp
		SELECT Sumarizada.idUsuarioPadre,isnull(u.Nombre,''Sin Asignar'') as Nombre,isnull(u.Usuario,''---'') as Usuario, Sumarizada.valor32, Sumarizada.valor0, Sumarizada.valor1, Sumarizada.valor2, Sumarizada.valor3
, Sumarizada.valor4, Sumarizada.valor5, Sumarizada.valor6
		FROM ( SELECT CASE  WHEN (GROUPING(idUsuarioPadre) = 1) THEN - 1 ELSE idUsuarioPadre END idUsuarioPadre, 
		SUM([valor32]) [valor32], SUM([valor0]) [valor0], SUM([valor1]) [valor1], SUM([valor2]) [valor2], SUM([valor3]) [valor3],
		 SUM([valor4]) [valor4], 
		SUM([valor5]) [valor5], SUM([valor6]) [valor6] FROM ( SELECT idUsuarioPadre, ISNULL([100],0) as [valor32],ISNULL([Movil],0) as [valor0],
        ISNULL([Sin Asignar],0) as [valor1],ISNULL([Sin Asignar 2 Visita],0) as [valor2],
        ISNULL([Sin asignar 3 Visita],0) as [valor3],ISNULL([Validas],0) as [valor4],
        ISNULL([Validas Aprobadas],0) as [valor5],ISNULL([Validas Sin Aprobar],0) as [valor6] FROM ( 
        SELECT idUsuarioPadre, valor, ISNULL(resFinal, 100) AS resFinal FROM ReporteGestionMovil WITH (NOLOCK) WHERE 1 = 1 
		and Ruta='''+@tipoFormulario+''' '+
		case  when @delegacion='null' then ' AND CV_DELEGACION is null ' when @delegacion<>'' then ' AND CV_DELEGACION=''' + @delegacion + ''' ' else '' end +
		case  when @despacho='null' then ' AND idDominio is null ' when @despacho<>'' then ' AND idDominio=''' + @despacho + ''' ' else '' end +
		case  when @fechaCarga='null' then ' AND fechaCarga is null ' when @fechaCarga<>'' then ' AND convert(varchar(10),fechaCarga,103)+'' '' +convert(varchar(5), fechaCarga,108)=''' + @fechaCarga + ''' ' else '' end +
		case  when @estFinal='null' then ' AND estFinal is null ' when @estFinal<>'' then ' AND estFinal in(' + @estFinal + ') ' else '' end +
		' ) source pivot(sum(source.valor) FOR source.resFinal IN ( [100],[Movil],[Sin Asignar], [Sin Asignar 2 Visita],
                      [Sin asignar 3 Visita],[Validas],[Validas Aprobadas],[Validas Sin Aprobar] )) AS pivottable ) pivote GROUP BY idUsuarioPadre WITH ROLLUP ) Sumarizada 
		left join Usuario u on u.idUsuario=Sumarizada.idUsuarioPadre;'
		set @sql+=' select * from @temp where  isnull(idUsuarioPadre,0)<>-1; 
		select idUsuarioPadre ,valor32 ,valor0 ,	valor1 ,valor2 ,valor3 ,valor4 ,valor5 ,valor6 from @temp where idUsuarioPadre=-1;'
		
	END
	
	if @tipoConsulta = 3
	BEGIN		
		set @sql='Declare @temp table(idUsuario int,Nombre [varchar](100),Usuario [varchar](100) ,valor32 int,valor0 int,
		valor1 int,valor2 int,valor3 int,valor4 int,valor5 int,valor6 int);'
	
		set @sql+=' Insert into @temp
		SELECT Sumarizada.idUsuario,isnull(u.Nombre,''Sin Asignar'') as Nombre,isnull(u.Usuario,''---'') as Usuario, Sumarizada.valor32, Sumarizada.valor0, Sumarizada.valor1, Sumarizada.valor2, Sumarizada.valor3
, Sumarizada.valor4, Sumarizada.valor5, Sumarizada.valor6
		FROM ( SELECT CASE  WHEN (GROUPING(idUsuario) = 1) THEN - 1 ELSE idUsuario END idUsuario, 
		SUM([valor32]) [valor32], SUM([valor0]) [valor0], SUM([valor1]) [valor1], SUM([valor2]) [valor2], SUM([valor3]) [valor3],
		 SUM([valor4]) [valor4], 
		SUM([valor5]) [valor5], SUM([valor6]) [valor6] FROM ( SELECT idUsuario, ISNULL([100],0) as [valor32],ISNULL([Movil],0) as [valor0],
        ISNULL([Sin Asignar],0) as [valor1],ISNULL([Sin Asignar 2 Visita],0) as [valor2],
        ISNULL([Sin asignar 3 Visita],0) as [valor3],ISNULL([Validas],0) as [valor4],
        ISNULL([Validas Aprobadas],0) as [valor5],ISNULL([Validas Sin Aprobar],0) as [valor6] FROM ( 
        SELECT idUsuario, valor, ISNULL(resFinal, 100) AS resFinal FROM ReporteGestionMovil WITH (NOLOCK) WHERE 1 = 1 
		and Ruta='''+@tipoFormulario+''' '+
		case  when @delegacion='null' then ' AND CV_DELEGACION is null ' when @delegacion<>'' then ' AND CV_DELEGACION=''' + @delegacion + ''' ' else '' end +
		case  when @despacho='null' then ' AND idDominio is null ' when @despacho<>'' then ' AND idDominio=''' + @despacho + ''' ' else '' end +
		case  when @supervisor='null' then ' AND idUsuarioPadre is null ' when @supervisor<>'' then ' AND idUsuarioPadre=''' + @supervisor + ''' ' else '' end +
		case  when @fechaCarga='null' then ' AND fechaCarga is null ' when @fechaCarga<>'' then ' AND convert(varchar(10),fechaCarga,103)+'' '' +convert(varchar(5), fechaCarga,108)=''' + @fechaCarga + ''' ' else '' end +
		case  when @estFinal='null' then ' AND estFinal is null ' when @estFinal<>'' then ' AND estFinal in(' + @estFinal + ') ' else '' end +
		' ) source pivot(sum(source.valor) FOR source.resFinal IN ( [100],[Movil],[Sin Asignar], [Sin Asignar 2 Visita],
                      [Sin asignar 3 Visita],[Validas],[Validas Aprobadas],[Validas Sin Aprobar] )) AS pivottable ) pivote GROUP BY idUsuario WITH ROLLUP ) Sumarizada 
		left join Usuario u on u.idUsuario=Sumarizada.idUsuario;'
		set @sql+=' select * from @temp where  isnull(idUsuario,0)<>-1; 
		select idUsuario ,valor32 ,valor0 ,	valor1 ,valor2 ,valor3 ,valor4 ,valor5 ,valor6 from @temp where idUsuario=-1;'
		
	END
	
	
	
	
	EXECUTE sp_sqlexec @sql
	
	
	
END
