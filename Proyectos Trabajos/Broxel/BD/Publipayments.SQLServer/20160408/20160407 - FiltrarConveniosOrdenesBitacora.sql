
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/04/07
-- Descripcion: Filtra las gestiones que anteriormente se gestionaron como convevio "BitacoraRespuestas" 
-- =============================================
CREATE PROCEDURE [dbo].[FiltrarConveniosOrdenesBitacora] (@Formulario varchar (50) ,@Ordenes VARCHAR(8000), @OrdenesFiltradas VARCHAR(8000) OUT)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @tablaOrdenes TABLE (idOrden INT PRIMARY KEY)
	INSERT INTO @tablaOrdenes
	SELECT Item FROM SplitStrings_Moden(@Ordenes,',')

	DECLARE @OFiltradas VARCHAR(8000)
	--Filtra las Ordenes que sean de SMS y no sean los dictamenes Dictamenpromdepago o DictamenFPP
	SELECT @OFiltradas = COALESCE(@OFiltradas + ', ', '') + CONVERT(VARCHAR(10), o.idOrden)
	FROM Ordenes o
	INNER JOIN BitacoraRespuestas br WITH (NOLOCK) ON br.idDominio = o.idDominio
		AND br.idUsuarioPadre = o.idUsuarioPadre
		AND br.idOrden = o.idOrden
	INNER JOIN CamposRespuesta c WITH (NOLOCK) ON br.idCampo = c.idCampo
	INNER JOIN  (select idorden,max(fecha) as fecha from bitacorarespuestas WITH (NOLOCK) group by idorden) br2 
			on br.idorden=br2.idorden and br.fecha=br2.fecha
	WHERE
	 o.idOrden IN (SELECT idOrden FROM @tablaOrdenes)
		AND c.Nombre LIKE 'Dictamen%'
		AND (
			o.Tipo not like '%S%'
			OR c.Nombre = 'Dictamenpromdepago'
			OR c.Nombre = 'DictamenpromdepagoTOM'
			OR c.Nombre = 'Dictamenliquida'
			)
		AND br.idformulario IN (select idformulario from formulario WITH (NOLOCK) where nombre like @Formulario)
	SET  @OrdenesFiltradas = @OFiltradas
END
