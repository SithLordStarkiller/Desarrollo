USE [SistemasCobranza]
GO
/****** Object:  StoredProcedure [dbo].[MonitoreoOperacion]    Script Date: 04/12/2015 05:43:27 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 12/02/2015
-- Description:	Ejecuta monitoreos de operacion
-- Modificacion: MJNS - 20151204 - Se agrega with (nolock)
-- =============================================
ALTER PROCEDURE [dbo].[MonitoreoOperacion] 
	@TipoMonitoreo VARCHAR(100)
AS
BEGIN
	DECLARE @SQL VARCHAR(max)

	IF @TipoMonitoreo = 'NumeroSincronizando'
	BEGIN
		DECLARE @nTotal_Sincro_Count INT
		
		SELECT @nTotal_Sincro_Count = isnull(sum(ISNULL(C.CantCreditos, 0)), 0)
		FROM Dominio d WITH (NOLOCK)
		LEFT JOIN (
			SELECT COUNT(o.idOrden) AS CantCreditos, o.idDominio
			FROM Ordenes o WITH (NOLOCK)
			LEFT JOIN Creditos c WITH (NOLOCK) ON c.CV_CREDITO = o.num_Cred
			WHERE o.Estatus = 6
			GROUP BY idDominio
			) C ON C.idDominio = d.idDominio
		WHERE d.idDominio > 2
			AND d.Estatus = 1

		SET @SQL = 'select ''' + CAST(@nTotal_Sincro_Count AS VARCHAR(5)) + ''' as Valor,Count(*) Cuenta from Ordenes WITH (NOLOCK)'		
	END

	IF @TipoMonitoreo = 'UltimaActGestionMovil'
	BEGIN
		SET @SQL = 'select DATEDIFF(minute,valor,GETDATE()) Valor from CatalogoGeneral WITH (NOLOCK)  where id=2'
	END

	EXECUTE sp_sqlexec @SQL
END
