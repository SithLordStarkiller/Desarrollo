
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 2015/04/08
-- Description:	Filtra las ordenes pasadas como parametro y regresa un varchar(8000) con las ordenes filtradas
-- =============================================
CREATE PROCEDURE FiltrarEstatusUsuarioOrdenes (@Ordenes VARCHAR(8000), @OrdenesFiltradas VARCHAR(8000) OUT)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @OFiltradas VARCHAR(1500)
	, @sql VARCHAR(1500) = ''
	--Filtra las Ordenes que sean de SMS y no sean los dictamenes Dictamenpromdepago o DictamenFPP
	SELECT @OFiltradas = COALESCE(@OFiltradas + ', ', '') + CONVERT(VARCHAR(10), o.idOrden)
	FROM Ordenes o
	INNER JOIN Respuestas r ON r.idDominio = o.idDominio
		AND r.idUsuarioPadre = o.idUsuarioPadre
		AND r.idOrden = o.idOrden
	INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo
	WHERE o.idOrden IN (SELECT Item FROM SplitStrings_Moden(@Ordenes,','))
		AND c.Nombre LIKE 'Dictamen%'
		AND (
			o.Tipo != 'S'
			OR c.Nombre = 'Dictamenpromdepago'
			OR c.Nombre = 'DictamenpromdepagoTOM'
			OR c.Nombre = 'Dictamenliquida'
			OR c.Nombre = 'DictamenFPP'
			)

	SET  @OrdenesFiltradas = @OFiltradas
END
GO

/*
DECLARE @Resultado VARCHAR(8000)

EXEC FiltrarEstatusUsuarioOrdenes '280453,280456' , @Resultado OUTPUT

SELECT @Resultado res
*/
