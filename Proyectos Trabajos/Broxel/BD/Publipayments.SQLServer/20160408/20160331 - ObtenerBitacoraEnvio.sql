/****** Object:  StoredProcedure [dbo].[ObtenerBitacoraEnvio]    Script Date: 31/03/2016 02:16:10 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Maximiliano Silva
-- Create date: 2014/09/26
-- Description:	Obtiene los registros a eviar al WS
-- Modificacion: 2016/31/03
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerBitacoraEnvio] @TipoConsulta INT = 1
AS
BEGIN
	SET NOCOUNT ON;

	IF (@TipoConsulta = 1)
	BEGIN
		SELECT be.id
			,be.idOrden
			,be.EstatusEnvio
			,be.EstatusAnterior
			,be.Fecha
			,be.FechaEnvio
			,be.RespuestaEnvio
			, CASE  WHEN Canceladas.idOrden IS NOT NULL THEN 1 ELSE 0  END AS CancelAnteriorCc
		FROM BitacoraEnvio be WITH (NOLOCK) INNER JOIN ordenes o ON be.idorden=o.idorden
		LEFT JOIN  (select distinct  be.idorden from   ordenes o  inner join bitacoraenvio be on o.idorden=be.idorden 
					where be.estatusEnvio=12 and be.fecha> o.fecharecepcion and be.estatusanterior=1 and o.tipo like '%C%') Canceladas ON Canceladas.idOrden=be.idOrden
		WHERE be.FechaEnvio IS NULL
		ORDER BY id;
	END
	ELSE
	BEGIN
		SELECT be.id
			,be.idOrden
			,be.EstatusEnvio
			,be.EstatusAnterior
			,be.Fecha
			,be.FechaEnvio
			,be.RespuestaEnvio
				, CASE  WHEN Canceladas.idOrden IS NOT NULL THEN 1 ELSE 0  END AS CancelAnteriorCc
		FROM BitacoraEnvio be WITH (NOLOCK) INNER JOIN ordenes o ON be.idorden=o.idorden
		LEFT JOIN  (select distinct  be.idorden from   ordenes o  inner join bitacoraenvio be on o.idorden=be.idorden 
					where be.estatusEnvio=12 and be.fecha> o.fecharecepcion and be.estatusanterior=1 and o.tipo like '%C%') Canceladas ON Canceladas.idOrden=be.idOrden
		WHERE be.FechaEnvio > DATEADD(minute, - 2, GETDATE())
		ORDER BY id;
	END
END

