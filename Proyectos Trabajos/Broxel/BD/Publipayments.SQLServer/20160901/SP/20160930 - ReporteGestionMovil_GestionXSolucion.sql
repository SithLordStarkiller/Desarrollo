
/****** Object:  StoredProcedure [dbo].[ReporteGestionMovil_GestionXSolucion]    Script Date: 21/09/2016 16:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 24/06/2015
-- Description:	obtiene los tres niveles de consulta para Reporte Gestion Movil: GestionXSolucion
-- =============================================
ALTER PROCEDURE [dbo].[ReporteGestionMovil_GestionXSolucion] 
	@delegacion VARCHAR(5), 
	@fechaCarga VARCHAR(50), 
	@estFinal VARCHAR(500), 
	@despacho VARCHAR(5), 
	@supervisor VARCHAR(5), 
	@tipoFormulario VARCHAR(10), 
	@tipoConsulta INT
AS
BEGIN
DECLARE @sql varchar(max)=''
	
	
	if @tipoConsulta = 1
	BEGIN		
		set @sql='Declare @temp table(idDominio int,NombreDominio [varchar](100) ,valor32 int,valor0 int,valor1 int,valor2 int,valor3 int,valor4 int,valor5 int,valor6 int);'
	
		set @sql+=' Insert into @temp'
		+' SELECT Sumarizada.idDominio,d.nombreDominio as NombreDominio, Sumarizada.valor32, Sumarizada.valor0, Sumarizada.valor1, Sumarizada.valor2, Sumarizada.valor3,'
		+' Sumarizada.valor4, Sumarizada.valor5, Sumarizada.valor6'
		+' FROM ( SELECT CASE  WHEN (GROUPING(idDominio) = 1) THEN - 1 ELSE idDominio END idDominio,'
		+' SUM([valor32]) [valor32], SUM([valor0]) [valor0], SUM([valor1]) [valor1], SUM([valor2]) [valor2], SUM([valor3]) [valor3],'
		+' SUM([valor4]) [valor4], '
		+' SUM([valor5]) [valor5], SUM([valor6]) [valor6] FROM ( SELECT idDominio, ISNULL([100],0) as [valor32],ISNULL([Movil],0) as [valor0],'
        +' ISNULL([Sin Asignar],0) as [valor1],ISNULL([Sin Asignar 2 Visita],0) as [valor2],'
        +' ISNULL([Sin asignar 3 Visita],0) as [valor3],ISNULL([Validas],0) as [valor4],'
        +' ISNULL([Validas Aprobadas],0) as [valor5],ISNULL([Validas Sin Aprobar],0) as [valor6] FROM ( SELECT idDominio, valor, ISNULL(resFinal, 100) AS resFinal FROM ReporteGestionMovil WITH (NOLOCK) WHERE 1 = 1 '
		+' and Ruta='''+@tipoFormulario+''' '+
		case  when @delegacion='null' then ' AND CV_DELEGACION is null ' when @delegacion<>'' then ' AND CV_DELEGACION=''' + @delegacion + ''' ' else '' end +
		case  when @fechaCarga='null' then ' AND fechaCarga is null ' when @fechaCarga<>'' then ' AND convert(varchar(10),fechaCarga,103)+'' '' +convert(varchar(5), fechaCarga,108)=''' + @fechaCarga + ''' ' else '' end +
		case  when @estFinal='null' then ' AND estFinal is null ' when @estFinal<>'' then ' AND estFinal in(' + @estFinal + ') ' else '' end +
		' ) source pivot(sum(source.valor) FOR source.resFinal IN ( [100],[Movil],[Sin Asignar], [Sin Asignar 2 Visita],'
        +' [Sin asignar 3 Visita],[Validas],[Validas Aprobadas],[Validas Sin Aprobar] )) AS pivottable ) pivote GROUP BY idDominio WITH ROLLUP ) Sumarizada '
		+' left join Dominio d on d.idDominio=Sumarizada.idDominio;'
		set @sql+=' select * from @temp where idDominio<>-1; '
		+'select idDominio ,valor32 ,valor0 ,	valor1 ,valor2 ,valor3 ,valor4 ,valor5 ,valor6 from @temp where idDominio=-1;'
		
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
	
	
	
		if @tipoConsulta = 4
	BEGIN		
		set @sql='Declare @temp2 table(idDominio int,NombreDominio varchar (100) ,SinGestionar int,Gestionadas int,Total int); '
		set @sql+=' INSERT INTO @temp2'
					+' SELECT '
					+' CASE WHEN NGestionadas.iddominio IS NULL THEN Gestionadas.iddominio ELSE  NGestionadas.iddominio END,'
					+' CASE WHEN NGestionadas.NombreDominio IS NULL THEN Gestionadas.NombreDominio ELSE  NGestionadas.NombreDominio END,'
					+' ISNULL(NGestionadas.SinGestionar,0),'
					+' ISNULL(Gestionadas.Gestionadas,0),'
					+' SUM(ISNULL (NGestionadas.SinGestionar,0)+ ISNULL(Gestionadas.Gestionadas,0)) AS  Total'
					+' FROM (SELECT d.iddominio,d.NombreDominio,COUNT(CV_CREDITO) AS ''SinGestionar'' FROM creditos c with(nolock) INNER JOIN dominio d WITH(NOLOCK) ON c.TX_NOMBRE_DESPACHO=d.nom_corto'
					+' WHERE c.CV_CREDITO NOT IN ('
					+' SELECT num_cred FROM ordenes o WITH(NOLOCK) WHERE num_cred in (SELECT CV_CREDITO FROM creditos WITH(NOLOCK) WHERE CV_RUTA='''+@tipoFormulario+''') AND estatus in (3,4))'
					+' AND c.CV_RUTA='''+@tipoFormulario+''''+
					+' AND c.CV_DELEGACION = CASE  WHEN '''+@delegacion+''' != '''' THEN ''' +@delegacion+ ''' ELSE CV_DELEGACION  END'
					+' group by d.NombreDominio,d.iddominio'
					+' ) AS NGestionadas'					
					+' RIGHT JOIN'
					+' (SELECT d.iddominio,d.NombreDominio,COUNT(CV_CREDITO) AS ''Gestionadas''	from creditos c WITH(NOLOCK) INNER JOIN dominio d WITH(NOLOCK) ON c.TX_NOMBRE_DESPACHO=d.nom_corto'
					+' WHERE c.CV_CREDITO  IN ('
					+' SELECT num_cred FROM ordenes o WITH(NOLOCK) WHERE num_cred in (SELECT CV_CREDITO FROM creditos WITH(NOLOCK) WHERE CV_RUTA='''+@tipoFormulario+''') AND estatus in (3,4))'
					+' AND c.CV_RUTA='''+@tipoFormulario+''''
					+' AND c.CV_DELEGACION = CASE  WHEN '''+@delegacion+''' != '''' THEN ''' +@delegacion+ ''' ELSE CV_DELEGACION  END'
					+' group by d.NombreDominio,d.iddominio'
					+' ) AS Gestionadas ON NGestionadas.iddominio=Gestionadas.iddominio'
					+' GROUP BY NGestionadas.idDominio,NGestionadas.NombreDominio,NGestionadas.SinGestionar, Gestionadas.idDominio,Gestionadas.NombreDominio,Gestionadas.Gestionadas;'
					+' SELECT  * from @temp2'
					+' UNION'
					+' SELECT 666666666 AS idDominio, ''Total'' AS NombreDominio,SUM(SinGestionar) AS SinGestionar,SUM(Gestionadas) AS Gestionadas, SUM(Total) AS Total from @temp2'
					+' ORDER BY iddominio ASC'
		
	END

	if @tipoConsulta = 5
	BEGIN		
	set @sql=''
	set @sql+='SELECT '
			+'ISNULL(NGestionadas.iddominio,Gestionadas.iddominio) AS ''iddominio'','
			+'ISNULL(NGestionadas.NombreDominio,Gestionadas.NombreDominio) AS ''NombreDominio'','
			+'Gestionadas.idusuario,'
			+'Gestionadas.Nombre,'
			+'Gestionadas.usuario,'
			+'ISNULL(NGestionadas.SinGestionar,0) AS ''SinGestionar'', '
			+'Gestionadas.Gestionadas AS ''Gestionadas'''
			+' FROM (SELECT d.iddominio,d.NombreDominio,COUNT(CV_CREDITO) AS ''SinGestionar'' FROM creditos c with(nolock) '
			+' INNER JOIN dominio d WITH(NOLOCK) ON c.TX_NOMBRE_DESPACHO=d.nom_corto'
			+' WHERE c.CV_CREDITO NOT IN ( SELECT num_cred FROM ordenes o WITH(NOLOCK) WHERE num_cred in (SELECT CV_CREDITO FROM creditos WITH(NOLOCK) WHERE CV_RUTA='''+@tipoFormulario+''') AND estatus in (3,4)) '
			+' AND c.CV_RUTA='''+@tipoFormulario+''''
			+' AND d.iddominio = CASE  WHEN '''+@despacho+''' != '''' THEN ''' +@despacho+ ''' ELSE d.iddominio  END'
			+' GROUP BY  d.NombreDominio,d.iddominio ) AS NGestionadas '
			+' RIGHT JOIN '
			+' (SELECT d.iddominio,d.NombreDominio,u.idusuario,u.nombre,u.usuario,COUNT(u.idusuario) AS ''Gestionadas'''
			+' FROM creditos c WITH(NOLOCK) '
			+' INNER JOIN dominio d WITH(NOLOCK) ON c.TX_NOMBRE_DESPACHO=d.nom_corto'
			+' INNER JOIN ordenes o WITH(NOLOCK) ON o.num_cred=c.CV_CREDITO'
			+' INNER JOIN usuario u WITH(NOLOCK) ON u.idusuario=o.idusuario'
			+' WHERE O.ESTATUS IN (3,4)'
			+' AND c.CV_RUTA='''+@tipoFormulario+''''
			+' AND c.CV_DELEGACION = CASE  WHEN '''+@delegacion+''' != '''' THEN ''' +@delegacion+ ''' ELSE CV_DELEGACION  END' 
			+' AND d.iddominio = CASE  WHEN '''+@despacho+''' != '''' THEN ''' +@despacho+ ''' ELSE d.iddominio  END'
			+' GROUP BY  d.iddominio,d.NombreDominio,u.idusuario,u.nombre,u.usuario'
			+') '
			+' AS Gestionadas '
			+' ON NGestionadas.iddominio=Gestionadas.iddominio '
			+' GROUP BY NGestionadas.idDominio,NGestionadas.NombreDominio,NGestionadas.SinGestionar, Gestionadas.idDominio,Gestionadas.NombreDominio,Gestionadas.idusuario,Gestionadas.Nombre,Gestionadas.usuario,Gestionadas.Gestionadas; '
	END
	

	
	print @sql
	EXECUTE sp_sqlexec @sql
	
	
	
END
