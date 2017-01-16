
/****** Object:  StoredProcedure [dbo].[ReporteLlamadasNoExito]    Script Date: 29/09/2016 15:08:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/09/28
-- Description:	Obtiene la informacion de las llamadas sin exito del transcurso del mes
-- =============================================
CREATE PROCEDURE [dbo].[ReporteLlamadasNoExito] (@idproceso INT,@idDominio INT)
AS
BEGIN

	SET NOCOUNT ON;

			DECLARE @Dias nvarchar(200)='',@Sql nvarchar(4000)=''
			DECLARE @Tope INT,@i INT

			SELECT @Tope=DAY  (GETDATE())


			SET @i=1
			
			WHILE @i <= @Tope
			BEGIN
				SET @Dias=@Dias+'['+CONVERT(NVARCHAR(2),@i)+'],';
			   SET @i = @i + 1;
			END;

			SET @Dias=LEFT(@Dias,LEN(@Dias)-1)

			IF(@idproceso=1)
			BEGIN
					SET @Sql +=
					+' SELECT idDominio,NombreDominio,'+@Dias+',SUM ('
					+REPLACE(REPLACE(REPLACE(@Dias,',','+'),'[','ISNULL(['),']','],0)')
					+') AS Total FROM '
					+'(SELECT d.iddominio,d.nombredominio'
					+',DAY(CONVERT(DATE,ll.FECHA_LLAMADA)) AS Dias'
					+',COUNT(d.iddominio) AS totalDias'
					+' FROM LlamadasSinExito ll with(nolock) '
					+' INNER JOIN creditos c with(nolock)'
					+' on ll.cv_credito=c.cv_credito'
					+' INNER JOIN dominio d with(nolock)  '
					+' ON d.nom_corto=c.tx_nombre_despacho'
					+' WHERE c.CV_RUTA=''RDST'''
					+' GROUP BY d.iddominio,d.nombredominio'
					+' ,CONVERT(DATE,ll.FECHA_LLAMADA)) AS tabla'
					+'  PIVOT (MAX(totalDias) FOR Dias IN ( '+@Dias +') ) AS  llamada '
					+' GROUP BY iddominio,nombredominio,'+@Dias
			END

			ELSE IF(@idproceso=2)
			BEGIN
					SET @Sql +=
					+' SELECT idDominio,Delegacion,'+@Dias+',SUM ('
					+REPLACE(REPLACE(REPLACE(@Dias,',','+'),'[','ISNULL(['),']','],0)')
					+') AS Total FROM '
					+'(SELECT d.iddominio,d.nombredominio,cd.Descripcion AS Delegacion'
					+',DAY(CONVERT(DATE,ll.FECHA_LLAMADA)) AS Dias'
					+',COUNT(d.iddominio) AS totalDias'
					+' FROM LlamadasSinExito ll with(nolock) '
					+' INNER JOIN creditos c with(nolock)'
					+' on ll.cv_credito=c.cv_credito'
					+' INNER JOIN dominio d with(nolock)  '
					+' ON d.nom_corto=c.tx_nombre_despacho'
					+' INNER JOIN  CatDelegaciones cd with(nolock)   ON cd.Delegacion=c.CV_DELEGACION'
					+' WHERE c.CV_RUTA=''RDST'' AND d.iddominio='+ CONVERT(NVARCHAR(2),@idDominio)
					+' GROUP BY d.iddominio,d.nombredominio,cd.Descripcion'
					+' ,CONVERT(DATE,ll.FECHA_LLAMADA)) AS tabla'
					+'  PIVOT (MAX(totalDias) FOR Dias IN ( '+@Dias +') ) AS  llamada '
					+' GROUP BY iddominio,Delegacion,'+@Dias
			END

			

			
			EXEC sp_executesql @Sql


END
