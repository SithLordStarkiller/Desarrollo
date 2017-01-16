
/****** Object:  StoredProcedure [dbo].[FiltrarEstatusUsuarioOrdenes]    Script Date: 10/06/2015 13:33:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 2015/04/08
-- Modificacion: 20150421 MJNS Fix subquery con función
-- Description:	Filtra las ordenes pasadas como parametro y regresa un varchar(8000) con las ordenes filtradas
-- Modification: 06102015 PEJV Se cambia tipo a like '%s'
-- Modification: 2015/10/27 JARO  se agrega like DictamenFPP% para cubrir los nuevos dictamenes FPP
-- =============================================
ALTER PROCEDURE [dbo].[FiltrarEstatusUsuarioOrdenes] (@Ordenes VARCHAR(8000), @OrdenesFiltradas VARCHAR(8000) OUT)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @tablaOrdenes TABLE (idOrden INT PRIMARY KEY)
	INSERT INTO @tablaOrdenes
	SELECT Item FROM SplitStrings_Moden(@Ordenes,',')

	DECLARE @OFiltradas VARCHAR(1500)
	, @sql VARCHAR(1500) = ''
	--Filtra las Ordenes que sean de SMS y no sean los dictamenes Dictamenpromdepago o DictamenFPP
	SELECT @OFiltradas = COALESCE(@OFiltradas + ', ', '') + CONVERT(VARCHAR(10), o.idOrden)
	FROM Ordenes o
	INNER JOIN Respuestas r ON r.idDominio = o.idDominio
		AND r.idUsuarioPadre = o.idUsuarioPadre
		AND r.idOrden = o.idOrden
	INNER JOIN CamposRespuesta c ON r.idCampo = c.idCampo
	WHERE o.idOrden IN (SELECT idOrden FROM @tablaOrdenes)
		AND c.Nombre LIKE 'Dictamen%'
		AND (
			o.Tipo not like '%S%'
			OR c.Nombre = 'Dictamenpromdepago'
			OR c.Nombre = 'DictamenpromdepagoTOM'
			OR c.Nombre = 'Dictamenliquida'
			)

	SET  @OrdenesFiltradas = @OFiltradas
END
