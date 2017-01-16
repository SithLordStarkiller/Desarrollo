
/****** Object:  StoredProcedure [dbo].[ObtenerPorcentajeCapturaWeb]    Script Date: 22/06/2016 03:58:15 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Autor: Maximiliano Silva
--Fecha: 2014-12-17
--Descripción: Obtiene el porcentaje de ordenes del supervisor y la cantidad actual gestionada por captura web
ALTER PROCEDURE [dbo].[ObtenerPorcentajeCapturaWeb] (@idUsuarioPadre INT)
AS
BEGIN
	DECLARE @CantidadActual INT, @Porcentaje FLOAT

	SELECT @Porcentaje = COUNT(idOrden) * .1
	FROM Ordenes WITH (NOLOCK)
	WHERE idUsuarioPadre = @idUsuarioPadre
	AND CvRuta='CSD'

	SELECT @CantidadActual = COUNT(r.idOrden)
	 from dbo.Respuestas r WITH (NOLOCK)
	WHERE r.idUsuarioPadre = @idUsuarioPadre
		AND r.idCampo in (
		select idcampo from camposrespuesta cr WITH (NOLOCK) inner join formulario f WITH (NOLOCK) on cr.idformulario=f.idformulario 
		where cr.Nombre='FormiikResponseSource' and  f.estatus=1  and f.idaplicacion = (select valor from catalogogeneral WITH (NOLOCK) where id=3))
		AND r.idFormulario in (  
		SELECT idFormulario FROM formulario WITH (NOLOCK) where Ruta='CSD'
		)
		AND r.Valor = 'CapturaWeb'


	SELECT CASE @Porcentaje WHEN 0 THEN 1 ELSE @Porcentaje END PorcentajePermitido , @CantidadActual CantidadActual


END