USE [SistemasCobranzaDesarrollo]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerTablaReporteGestionMovil]    Script Date: 02/11/2015 08:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes Vargas
-- Create date: 04/11/2014
-- Description:	Actualiza datos de tabla ReporteGestionMovil en base a datos obtenidos por consulta
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerTablaReporteGestionMovil]
AS
BEGIN
	DECLARE @temptable TABLE ([idUsuario] [int] NULL, [idUsuarioPadre] [int] NULL, [idDominio] [int] NULL, [valor] [int] NULL, [horaFinalDate] [int] NULL, [DiaFinalDate] [int] NULL, [MesFinalDate] [int] NULL, [AnioFinalDate] [int] NULL, [estFinal] [int] NULL, [resFinal] [varchar](100) NULL, [CV_DELEGACION] [int] NULL, [fechaCarga] [datetime] NULL, [Ruta] VARCHAR(10) NULL)

	INSERT @temptable
	SELECT idUsuario, idUsuarioPadre, idDominio, COUNT(*) AS valor, horaFinalDate, DiaFinalDate, MesFinalDate, AnioFinalDate, isnull(estFinal, 0) estFinal, resFinal, CV_DELEGACION, fechaCarga, Ruta
	FROM (
		SELECT dic.idCampo AS estFinal, c.CV_CREDITO, c.CV_DELEGACION, a.Fecha AS fechaCarga, dom.idDominio AS idDominio, o.idUsuarioPadre, o.idUsuario, br.Valor AS FinalDate, CASE 
				WHEN o.Estatus IN (1, 11, 12, 15, 17)
					AND o.idUsuario = 0
					AND idVisita = 3
					THEN 'Sin asignar 3 Visita'
				WHEN o.Estatus IN (1, 11, 12, 15, 17)
					AND o.idUsuario = 0
					AND idVisita = 2
					THEN 'Sin Asignar 2 Visita'
				WHEN (
						o.Estatus IN (1, 11, 12, 15, 17)
						AND o.idUsuario = 0
						)
					OR (idUsuarioPadre IS NULL)
					THEN 'Sin Asignar'
				WHEN o.Estatus IN (3, 37)
					AND o.idUsuario <> 0
					THEN 'Validas'
				WHEN o.Estatus IN (4, 47)
					AND o.idUsuario <> 0
					THEN 'Validas Aprobadas'
				WHEN o.Estatus IN (1, 11, 12, 15, 17)
					AND idVisita > 1
					THEN 'Validas Sin Aprobar'
				WHEN o.idUsuario <> 0
					AND o.idVisita = 1
					THEN 'Movil'
				ELSE NULL
				END AS resFinal, datepart(dd, cast(br.Valor AS DATETIME)) AS DiaFinalDate, datepart(mm, cast(br.Valor AS DATETIME)) AS MesFinalDate, datepart(yy, cast(br.Valor AS DATETIME)) AS AnioFinalDate, datepart(hh, dateadd(hh, 1, cast(br.Valor AS DATETIME))) AS horaFinalDate, c.CV_RUTA AS Ruta
		FROM Creditos c
		LEFT JOIN Dominio dom ON dom.nom_corto = c.TX_NOMBRE_DESPACHO
		LEFT JOIN Ordenes o ON o.num_Cred = c.CV_CREDITO
		LEFT JOIN Archivos a ON a.id = c.ID_ARCHIVO
		LEFT JOIN (
			SELECT idOrden, Valor
			FROM Respuestas
			WHERE idCampo IN (
					SELECT idCampo
					FROM CamposRespuesta
					WHERE Nombre = 'FinalDate'
					)
			) br ON br.idOrden = o.idOrden
		LEFT JOIN (
			SELECT br.idCampo, br.idOrden
			FROM BitacoraRespuestas br
			INNER JOIN (
				SELECT MAX(Fecha) UltimaFecha, idOrden
				FROM BitacoraRespuestas
				WHERE idOrden NOT IN (
						SELECT DISTINCT idOrden
						FROM Respuestas
						)
				GROUP BY idOrden
				) brm ON br.idOrden = brm.idOrden
				AND br.Fecha = brm.UltimaFecha
			LEFT JOIN CamposRespuesta cr ON br.idCampo = cr.idCampo
			WHERE cr.Nombre LIKE 'dictamen%'
			
			UNION ALL
			
			SELECT DISTINCT (cr.idCampo), r.idOrden
			FROM Respuestas r
			LEFT JOIN CamposRespuesta cr ON r.idCampo = cr.idCampo
			WHERE cr.Nombre LIKE 'dictamen%'
			) AS dic ON dic.idOrden = o.idOrden
		) AS t1
	WHERE datepart(mm, fechaCarga) = DATEPART(MM, GETDATE())
		AND datepart(yy, fechaCarga) = DATEPART(yy, GETDATE())
	GROUP BY DiaFinalDate, MesFinalDate, AnioFinalDate, horaFinalDate, idUsuario, idUsuarioPadre, idDominio, estFinal, resFinal, CV_DELEGACION, fechaCarga, Ruta
	ORDER BY resFinal, horaFinalDate, fechaCarga, idUsuario

	MERGE ReporteGestionMovil AS target
	USING @temptable AS source
		ON (
				(
					target.[idUsuario] = source.[idUsuario]
					OR (
						target.[idUsuario] IS NULL
						AND source.[idUsuario] IS NULL
						)
					)
				AND (
					target.[idUsuarioPadre] = source.[idUsuarioPadre]
					OR (
						target.[idUsuarioPadre] IS NULL
						AND source.[idUsuarioPadre] IS NULL
						)
					)
				AND (
					target.[idDominio] = source.[idDominio]
					OR (
						target.[idDominio] IS NULL
						AND source.[idDominio] IS NULL
						)
					)
				AND (
					target.[horaFinalDate] = source.[horaFinalDate]
					OR (
						target.[horaFinalDate] IS NULL
						AND source.[horaFinalDate] IS NULL
						)
					)
				AND (
					target.[DiaFinalDate] = source.[DiaFinalDate]
					OR (
						target.[DiaFinalDate] IS NULL
						AND source.[DiaFinalDate] IS NULL
						)
					)
				AND (
					target.[MesFinalDate] = source.[MesFinalDate]
					OR (
						target.[MesFinalDate] IS NULL
						AND source.[MesFinalDate] IS NULL
						)
					)
				AND (
					target.[AnioFinalDate] = source.[AnioFinalDate]
					OR (
						target.[AnioFinalDate] IS NULL
						AND source.[AnioFinalDate] IS NULL
						)
					)
				AND (
					target.[estFinal] = source.[estFinal]
					OR (
						target.[estFinal] IS NULL
						AND source.[estFinal] IS NULL
						)
					)
				AND (
					target.[resFinal] = source.[resFinal]
					OR (
						target.[resFinal] IS NULL
						AND source.[resFinal] IS NULL
						)
					)
				AND (
					target.[CV_DELEGACION] = source.[CV_DELEGACION]
					OR (
						target.[CV_DELEGACION] IS NULL
						AND source.[CV_DELEGACION] IS NULL
						)
					)
				AND (
					target.[fechaCarga] = source.[fechaCarga]
					OR (
						target.[fechaCarga] IS NULL
						AND source.[fechaCarga] IS NULL
						)
					)
				AND (
					target.[Ruta] = source.[Ruta]
					OR (
						target.[Ruta] IS NULL
						AND source.[Ruta] IS NULL
						)
					)
				)
	WHEN MATCHED
		AND target.valor <> source.valor
		THEN
			UPDATE
			SET Target.valor = Source.valor
	WHEN NOT MATCHED
		THEN
			INSERT ([idUsuario], [idUsuarioPadre], [idDominio], [valor], [horaFinalDate], [DiaFinalDate], [MesFinalDate], [AnioFinalDate], [estFinal], [resFinal], [CV_DELEGACION], [fechaCarga], [Ruta])
			VALUES (source.[idUsuario], source.[idUsuarioPadre], source.[idDominio], source.[valor], source.[horaFinalDate], source.[DiaFinalDate], source.[MesFinalDate], source.[AnioFinalDate], source.[estFinal], source.[resFinal], source.[CV_DELEGACION], source.[fechaCarga], source.[Ruta])
	WHEN NOT MATCHED BY SOURCE
		THEN
			DELETE
	OUTPUT $ACTION, inserted.[idUsuario], inserted.[idUsuarioPadre], inserted.[idDominio], inserted.[valor], inserted.[horaFinalDate], inserted.[DiaFinalDate], inserted.[MesFinalDate], inserted.[AnioFinalDate], inserted.[estFinal], inserted.[resFinal], inserted.[CV_DELEGACION], inserted.[fechaCarga], inserted.[Ruta], deleted.[idUsuario], deleted.[idUsuarioPadre], deleted.[idDominio], deleted.[valor], deleted.[horaFinalDate], deleted.[DiaFinalDate], deleted.[MesFinalDate], deleted.[AnioFinalDate], deleted.[estFinal], deleted.[resFinal], deleted.[CV_DELEGACION], deleted.[fechaCarga], deleted.[Ruta];

	/*Fecha de ultima Actualizacion*/
	UPDATE CatalogoGeneral
	SET [Valor] = convert(VARCHAR(16), GETDATE(), 120)
	WHERE id = 2

	/*Merge a tabla CatDictamenRespuesta*/
	DECLARE @temptable1 TABLE ([idCampo] INT, [Valor] VARCHAR(50), [Nombre] VARCHAR(50), [Bloqueo] INT)

	INSERT @temptable1
	SELECT DISTINCT tablaSource.idCampo, r.Valor, cr.Nombre, CASE 
			WHEN (
					r.Valor = 'Solución a tu medida'
					OR r.Valor = 'Borrón y cuenta nueva'
					OR r.Valor = 'Dictamen de Capacidad de Pago'
					OR r.Valor = 'Liquidación'
					OR r.Valor = 'Fondo de Protección de Pagos'
					)
				THEN 0
			ELSE 1
			END AS Bloqueo
	FROM (
		SELECT DISTINCT (br.idCampo)
		FROM BitacoraRespuestas br WITH (NOLOCK)
		INNER JOIN (
			SELECT MAX(Fecha) UltimaFecha, idOrden
			FROM BitacoraRespuestas WITH (NOLOCK)
			WHERE idOrden NOT IN (
					SELECT DISTINCT idOrden
					FROM Respuestas WITH (NOLOCK)
					)
			GROUP BY Fecha, idOrden
			) brm ON br.idOrden = brm.idOrden
			AND br.Fecha = brm.UltimaFecha
		LEFT JOIN CamposRespuesta cr WITH (NOLOCK) ON br.idCampo = cr.idCampo
		WHERE cr.Nombre LIKE 'dictamen%'
		
		UNION ALL
		
		SELECT DISTINCT (cr.idCampo)
		FROM Respuestas r WITH (NOLOCK)
		LEFT JOIN CamposRespuesta cr WITH (NOLOCK) ON r.idCampo = cr.idCampo
		WHERE cr.Nombre LIKE 'dictamen%'
		) AS tablaSource
	LEFT JOIN Respuestas r WITH (NOLOCK) ON r.idCampo = tablaSource.idCampo
	LEFT JOIN CamposRespuesta cr WITH (NOLOCK) ON cr.idCampo = tablaSource.idCampo

	MERGE CatDictamenRespuesta AS target
	USING @temptable1 AS source
		ON (target.idCampo = source.idCampo)
	WHEN MATCHED
		THEN
			UPDATE
			SET Target.Valor = source.Valor, Target.Nombre = source.Nombre, Target.Bloqueo = source.Bloqueo
	WHEN NOT MATCHED
		THEN
			INSERT ([idCampo], [Valor], [Nombre], [Bloqueo])
			VALUES (source.[idCampo], source.[Valor], source.[Nombre], source.Bloqueo)
	OUTPUT $ACTION, inserted.[idCampo], inserted.[Valor], inserted.[Nombre], inserted.[Bloqueo];
END
