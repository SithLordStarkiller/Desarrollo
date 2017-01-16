SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes	
-- Create date: 14/04/2015
-- Description:	Obtiene telefonos registrados anteriormente para SMS de creditos diferentes
-- =============================================
Create PROCEDURE ObtenerReporteTelefonosDuplicados 
	 @idDominio INT
	,@idUsuarioPadre INT
	,@delegacion INT
	,@TipoFormulario varchar(15)
AS
BEGIN
	Declare @sql varchar(max)
	
	set @sql='SELECT t1.idOrden
		,t1.num_Cred
		,t1.Valor
		,ug.Nombre AS Gestor
		,us.Nombre AS Supervisor
		,au.num_Cred AS credOrig
	FROM AutorizacionSMS au
	INNER JOIN (
		SELECT o.idOrden
			,o.num_Cred
			,Valor
		FROM Respuestas r
		LEFT JOIN CamposRespuesta cr ON r.idCampo = cr.idCampo
		LEFT JOIN Ordenes o ON r.idOrden = o.idOrden
		WHERE cr.Nombre LIKE ''CelularSMS_%''
			AND cr.Nombre <> ''CelularSMS_Ant''
		) t1 ON (
			t1.Valor = au.Telefono
			AND au.num_Cred <> t1.num_Cred
			)
	LEFT JOIN Ordenes o ON o.idOrden = t1.idOrden
	LEFT JOIN Creditos c ON c.CV_CREDITO = t1.num_Cred
	LEFT JOIN Usuario ug ON ug.idUsuario = o.idUsuario
	LEFT JOIN Usuario us ON us.idUsuario = o.idUsuarioPadre
	WHERE c.CV_RUTA='+@TipoFormulario;
	
	IF @idDominio is not null and @idDominio<>'' and @idDominio<>'9999'
	begin
		set @sql=@sql+' AND o.idDominio = '+cast(@idDominio as varchar(5))
	end
	
	IF @idUsuarioPadre is not null and @idUsuarioPadre<>'' and @idUsuarioPadre<>'9999'
	begin
		set @sql=@sql+' AND o.idUsuarioPadre = '+cast(@idUsuarioPadre as varchar(5))
	end
	
	IF @delegacion is not null and @delegacion<>'' and @delegacion<>'9999'
	begin
		set @sql=@sql+' AND c.CV_DELEGACION = '+cast(@delegacion as varchar(5))
	end
	
	
	EXECUTE sp_sqlexec @sql
	
END
GO


