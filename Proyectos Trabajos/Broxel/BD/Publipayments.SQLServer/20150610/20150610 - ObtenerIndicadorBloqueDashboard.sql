/****** Object:  StoredProcedure [dbo].[ObtenerIndicadorBloqueDashboard]    Script Date: 10/06/2015 04:21:50 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************************************************
* Desarrollador: Mauricio L๓pez Sแnchez
* Proyecto:	London-PubliPayments-Formiik
* Fecha de Creaci๓n: 29/04/2014
* Descripci๓n de Creacion: Manejador de DashBoard Administrador
* Ultima Fecha de Modificaci๒n: 31/10/2014
* Modific๓: Pablo Jaimes
* Descripci๒n de ultima modificacion: Cambio fecha para meses con 31 dias      
* Ultima Fecha de Modificaci๒n: 28/01/2015
* Modific๓: Alberto Rojas
* Descripci๒n de ultima modificacion: Se agrega filtro por el tipo de formulario que se maneja
* Ultima Fecha de Modificaci๒n: 10/02/2015
* Modific๓: Pablo Jaimes
* Descripci๒n de ultima modificacion: Se agrega indicador por status Sincronizando
Modificacion: 20150610 MJNS Se optimizo la consulta para el bloque 0
****************************************************************************************************************/
ALTER PROCEDURE [dbo].[ObtenerIndicadorBloqueDashboard] (
	@Accion VARCHAR(100) = NULL
	,@SubAccion VARCHAR(100) = NULL
	,@Bloque INT = NULL
	--Datos del usuario que esta ejecutando....
	,@fi_Usuario INT = NULL
	,@fc_Usuario VARCHAR(50) = NULL
	,@fi_Dominio INT = NULL
	,@fi_Rol INT = NULL
	--Datos que quiere visualizar.....
	,@fc_DashBoard VARCHAR(100) = NULL
	,@fc_Delegacion VARCHAR(100) = NULL
	,@fc_Despacho VARCHAR(100) = NULL
	,@fc_Estado VARCHAR(100) = NULL
	,@fc_idUsuarioPadre VARCHAR(100) = NULL
	,@fc_idUsuario VARCHAR(100) = NULL
	,@debug VARCHAR(10) = NULL
	,@TipoFormulario VARCHAR(10) = NULL
	)
	WITH RECOMPILE
AS
BEGIN -- Inicio del procedure.
	--Configuracion
	SET DATEFORMAT DMY;

	--Variables
	DECLARE @Error VARCHAR(1000)
	DECLARE @nTotal_Sincro_Count INT
	DECLARE @nTotal_Sincro_Porc INT
	DECLARE @nTotal_London_Count INT
	DECLARE @nTotal_London_Porc INT
	DECLARE @nTotal_Ordenes_Count INT
	DECLARE @nTotal_Ordenes_Porc INT
	DECLARE @nTotal_BitacoraCC_Count INT
	DECLARE @nTotal_BitacoraCC_Porc INT
	DECLARE @nTotal_Respuestas_Count INT
	DECLARE @nTotal_Respuestas_Porc INT
	DECLARE @nTotal_Visitados_Count INT
	DECLARE @nTotal_Visitados_Porc INT
	DECLARE @nTotal_NoVisitados_Count INT
	DECLARE @nTotal_NoVisitados_Porc INT
	DECLARE @nTotal_Finalizados_Count INT
	DECLARE @nTotal_Finalizados_Porc INT
	DECLARE @nTotal_Visitados_NoAuth_Count INT
	DECLARE @nTotal_Visitados_NoAuth_Porc INT
	DECLARE @nTotal_Reasignados_Count INT
	DECLARE @nTotal_Reasignados_Porc INT
	DECLARE @nTotal_Gestores_Visitantes_Count INT
	DECLARE @nTotal_Promedio_Visitas_X_Gestor_Count INT
	DECLARE @nTotal_Promedio_visitas_X_Gestor_Porc INT
	DECLARE @nTotal_ReVisitados_Count INT
	DECLARE @nTotal_ReVisitados_Porc INT
	DECLARE @nTotal_Visitas_Realizadas_Count INT
	DECLARE @nTotal_Visitas_Realizadas_Porc INT
	DECLARE @nTotal_Cred_Sin_Asig_Count INT
	DECLARE @nTotal_Cred_Sin_Asig_Porc INT
	DECLARE @nTotal_Cred_Asig_Mov_Count INT
	DECLARE @nTotal_Cred_Asig_Mov_Porc INT
	DECLARE @nTotal_Sin_Orden_Count INT
	DECLARE @nTotal_Sin_Orden_Porc INT
	DECLARE @formularios NVARCHAR(max)
	DECLARE @nTotal_Temp_Count INT
	DECLARE @nTotal_Temp_Porc INT
	DECLARE @nValue BIGINT
	--Datos Extra
	DECLARE @fc_NombreCorto VARCHAR(40)
	DECLARE @nDate_Now BIGINT

	SET @fc_NombreCorto = ''
	SET @Error = ''

	--Inicio de Proceso...	
	BEGIN TRY
		--Validaciones iniciales
		SET @nDate_Now = CONVERT(BIGINT, CONVERT(VARCHAR, getdate(), 112))

		SELECT @fc_NombreCorto = d.nom_corto
		FROM Usuario u
		JOIN Dominio d ON u.idDominio = d.idDominio
		WHERE idUsuario = @fi_Usuario

		SELECT @fc_Delegacion = CASE 
				WHEN @fc_Delegacion = 'False'
					THEN Delegacion
				ELSE @fc_Delegacion
				END
		FROM RelacionDelegaciones
		WHERE idUsuario = @fi_Usuario

		--Comienzo a calcular los indicadores....   
		IF @fc_Delegacion = '%'
			AND @fc_Despacho = '%'
		BEGIN
			IF @fc_idUsuarioPadre = '%'
				AND @fc_idUsuario = '%' -- La consulta es todo, despacho y/o delegacion
			BEGIN
				PRINT 'TODO NULO'

				/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
				SELECT @nTotal_London_Count = isnull(sum(ISNULL(C.CantCreditos, 0)), 0)
				FROM Dominio d
				LEFT JOIN (
					SELECT TX_NOMBRE_DESPACHO
						,cv_delegacion
						,COUNT(CV_CREDITO) CantCreditos
					FROM Creditos
					WHERE CV_RUTA = @TipoFormulario
					GROUP BY TX_NOMBRE_DESPACHO
						,cv_delegacion
					) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
				JOIN CatDelegaciones z ON c.CV_DELEGACION = z.Delegacion
				WHERE d.idDominio > 2
					AND d.Estatus = 1

				/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
				SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
				FROM creditos c
				LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (
						1
						,11
						,12
						,15
						,3
						,4
						,5
						,6
						)
					AND o.idUsuario <> 0
					AND o.idDominio > 2
					AND c.CV_RUTA = @TipoFormulario

				IF @Bloque != 0
				BEGIN
					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
					SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
						,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
						,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							3
							,4
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
						,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							)
						AND o.idVisita > 1
						AND o.idDominio > 2
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							)
						AND o.idUsuario = 0
						AND o.idDominio > 2
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
					IF @fc_DashBoard <> 'Supervisor'
						AND @fc_DashBoard <> 'Gestor'
					BEGIN
						SELECT @nTotal_Sin_Orden_Count = isnull(sum(ISNULL(C.CantCreditos, 0)), 0)
						FROM Dominio d
						LEFT JOIN (
							SELECT TX_NOMBRE_DESPACHO
								,cv_delegacion
								,COUNT(CV_CREDITO) CantCreditos
							FROM Creditos
							WHERE CV_CREDITO NOT IN (
									SELECT num_cred
									FROM Ordenes
									WHERE Estatus IN (
											1
											,11
											,12
											,15
											,3
											,4
											,5
											,6
											)
									)
								AND CV_RUTA = @TipoFormulario
							GROUP BY TX_NOMBRE_DESPACHO
								,cv_delegacion
							) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
						JOIN CatDelegaciones z ON c.CV_DELEGACION = z.Delegacion
						WHERE d.idDominio > 2
							AND d.Estatus = 1
					END

					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_RUTA = @TipoFormulario

					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (4)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (3)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de reasignados ++++++++++*/
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					JOIN Respuestas r ON o.idOrden = r.idOrden
					WHERE o.Estatus IN (3)
						AND r.idCampo IN (
							SELECT idcampo
							FROM CamposRespuesta
							WHERE Nombre IN (
									'DictamenNopueaten'
									,'DictamenAcreditadoNoDisponible'
									,'Dictamentercvissincont'
									,'Dictamenavisoretencion'
									,'DictamenRecado'
									,'DictamenVisSContactoIDV2'
									,'DictamenVAdicIV2'
									,'DictamenIDV2AcNoDisponible'
									)
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					SELECT @nTotal_Sincro_Count = isnull(sum(ISNULL(C.CantCreditos, 0)), 0)
					FROM Dominio d
					LEFT JOIN (
						SELECT COUNT(o.idOrden) AS CantCreditos
							,o.idDominio
						FROM Ordenes o
						LEFT JOIN Creditos c ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus = 6
							AND CV_RUTA = @TipoFormulario
						GROUP BY idDominio
						) C ON C.idDominio = d.idDominio
					WHERE d.idDominio > 2
						AND d.Estatus = 1
				END
			END
			ELSE
			BEGIN
				IF @fc_idUsuario = '%' -- La consulta es por supervisor
				BEGIN
					PRINT 'TODO NULO MENOS USUARIO PADRE'

					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					SELECT @nTotal_London_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND c.CV_RUTA = @TipoFormulario

					IF @Bloque != 0
					BEGIN
						/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
						SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
							,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								3
								,4
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
						SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idVisita > 1
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idUsuario = 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
						SET @nTotal_Sin_Orden_Count = 0

						/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
						SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (4)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
						SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (3)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de reasignados ++++++++++*/
						SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						JOIN Respuestas r ON o.idOrden = r.idOrden
						WHERE o.Estatus IN (3)
							AND r.idCampo IN (
								SELECT idcampo
								FROM CamposRespuesta
								WHERE Nombre IN (
										'DictamenNopueaten'
										,'DictamenAcreditadoNoDisponible'
										,'Dictamentercvissincont'
										,'Dictamenavisoretencion'
										,'DictamenRecado'
										,'DictamenVisSContactoIDV2'
										,'DictamenVAdicIV2'
										,'DictamenIDV2AcNoDisponible'
										)
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
						SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (6)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario
					END
				END
				ELSE
				BEGIN
					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					PRINT 'TODO NULO MENOS USUARIO HIJO'

					IF @fc_idUsuarioPadre = '%'
					BEGIN
						SELECT @fc_idUsuarioPadre = idPadre
						FROM RelacionUsuarios
						WHERE idHijo = @fc_idUsuario
					END

					SELECT @nTotal_London_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND O.IdUsuario = @fc_idUsuario
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND O.IdUsuario = @fc_idUsuario
						AND c.CV_RUTA = @TipoFormulario

					IF @Bloque != 0
					BEGIN
						/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
						SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
							,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								3
								,4
								,6
								)
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
						SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idVisita > 1
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idUsuario = 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
						SET @nTotal_Sin_Orden_Count = 0

						/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
						SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (4)
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
						SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (3)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de reasignados ++++++++++*/
						SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						JOIN Respuestas r ON o.idOrden = r.idOrden
						WHERE o.Estatus IN (3)
							AND r.idCampo IN (
								SELECT idcampo
								FROM CamposRespuesta
								WHERE Nombre IN (
										'DictamenNopueaten'
										,'DictamenAcreditadoNoDisponible'
										,'Dictamentercvissincont'
										,'Dictamenavisoretencion'
										,'DictamenRecado'
										,'DictamenVisSContactoIDV2'
										,'DictamenVAdicIV2'
										,'DictamenIDV2AcNoDisponible'
										)
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
						SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (6)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario
					END
				END
			END
		END
		ELSE IF @fc_Despacho = '%'
		BEGIN
			IF @fc_idUsuarioPadre = '%'
				AND @fc_idUsuario = '%' -- La consulta es todo, despacho y/o delegacion
			BEGIN
				PRINT 'TODO NULO MENOS DELEGACION'

				/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
				SELECT @nTotal_London_Count = isnull(sum(ISNULL(C.CantCreditos, 0)), 0)
				FROM Dominio d
				LEFT JOIN (
					SELECT TX_NOMBRE_DESPACHO
						,cv_delegacion
						,COUNT(CV_CREDITO) CantCreditos
					FROM Creditos
					WHERE CV_RUTA = @TipoFormulario
					GROUP BY TX_NOMBRE_DESPACHO
						,cv_delegacion
					) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
				JOIN CatDelegaciones z ON c.CV_DELEGACION = z.Delegacion
				WHERE d.idDominio > 2
					AND d.Estatus = 1
					AND c.CV_DELEGACION = @fc_Delegacion

				/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
				SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
				FROM creditos c
				LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (
						1
						,11
						,12
						,15
						,3
						,4
						,5
						,6
						)
					AND o.idUsuario <> 0
					AND o.idDominio > 2
					AND c.CV_DELEGACION = @fc_Delegacion
					AND c.CV_RUTA = @TipoFormulario

				IF @Bloque != 0
				BEGIN
					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
					SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
						,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
						,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							3
							,4
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
						,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							)
						AND o.idVisita > 1
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							)
						AND o.idUsuario = 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
					IF @fc_DashBoard <> 'Supervisor'
						AND @fc_DashBoard <> 'Gestor'
					BEGIN
						SELECT @nTotal_Sin_Orden_Count = isnull(sum(ISNULL(C.CantCreditos, 0)), 0)
						FROM Dominio d
						LEFT JOIN (
							SELECT TX_NOMBRE_DESPACHO
								,cv_delegacion
								,COUNT(CV_CREDITO) CantCreditos
							FROM Creditos
							WHERE CV_CREDITO NOT IN (
									SELECT num_cred
									FROM Ordenes
									WHERE Estatus IN (
											1
											,11
											,12
											,15
											,3
											,4
											,5
											,6
											)
									)
								AND CV_RUTA = @TipoFormulario
							GROUP BY TX_NOMBRE_DESPACHO
								,cv_delegacion
							) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
						JOIN CatDelegaciones z ON c.CV_DELEGACION = z.Delegacion
						WHERE d.idDominio > 2
							AND d.Estatus = 1
							AND c.CV_DELEGACION = @fc_Delegacion
					END

					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.CV_RUTA = @TipoFormulario

					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (4)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (3)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de reasignados ++++++++++*/
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					JOIN Respuestas r ON o.idOrden = r.idOrden
					WHERE o.Estatus IN (3)
						AND r.idCampo IN (
							SELECT idcampo
							FROM CamposRespuesta
							WHERE Nombre IN (
									'DictamenNopueaten'
									,'DictamenAcreditadoNoDisponible'
									,'Dictamentercvissincont'
									,'Dictamenavisoretencion'
									,'DictamenRecado'
									,'DictamenVisSContactoIDV2'
									,'DictamenVAdicIV2'
									,'DictamenIDV2AcNoDisponible'
									)
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (6)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.CV_RUTA = @TipoFormulario
				END
			END
			ELSE
			BEGIN
				IF @fc_idUsuario = '%' -- La consulta es por supervisor
				BEGIN
					PRINT 'TODO NULO MENOS DELEGACION Y USUARIO PADRE'

					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					SELECT @nTotal_London_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND c.CV_RUTA = @TipoFormulario

					IF @Bloque != 0
					BEGIN
						/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
						SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
							,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								3
								,4
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
						SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idVisita > 1
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idUsuario = 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
						SET @nTotal_Sin_Orden_Count = 0

						/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
						SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (4)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
						SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (3)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de reasignados ++++++++++*/
						SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						JOIN Respuestas r ON o.idOrden = r.idOrden
						WHERE o.Estatus IN (3)
							AND r.idCampo IN (
								SELECT idcampo
								FROM CamposRespuesta
								WHERE Nombre IN (
										'DictamenNopueaten'
										,'DictamenAcreditadoNoDisponible'
										,'Dictamentercvissincont'
										,'Dictamenavisoretencion'
										,'DictamenRecado'
										,'DictamenVisSContactoIDV2'
										,'DictamenVAdicIV2'
										,'DictamenIDV2AcNoDisponible'
										)
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
						SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (6)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario
					END
				END
				ELSE
				BEGIN
					PRINT 'TODO NULO MENOS DELEGACION Y USUARIO HIJO'

					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					IF @fc_idUsuarioPadre = '%'
					BEGIN
						SELECT @fc_idUsuarioPadre = idPadre
						FROM RelacionUsuarios
						WHERE idHijo = @fc_idUsuario
					END

					SELECT @nTotal_London_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND O.IdUsuario = @fc_idUsuario
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND O.IdUsuario = @fc_idUsuario
						AND c.CV_RUTA = @TipoFormulario

					IF @Bloque != 0
					BEGIN
						/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
						SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
							,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								3
								,4
								,6
								)
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
						SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idVisita > 1
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idUsuario = 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
						SET @nTotal_Sin_Orden_Count = 0

						/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
						SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (4)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
						SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (4)
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
						SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (3)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de reasignados ++++++++++*/
						SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						JOIN Respuestas r ON o.idOrden = r.idOrden
						WHERE o.Estatus IN (3)
							AND r.idCampo IN (
								SELECT idcampo
								FROM CamposRespuesta
								WHERE Nombre IN (
										'DictamenNopueaten'
										,'DictamenAcreditadoNoDisponible'
										,'Dictamentercvissincont'
										,'Dictamenavisoretencion'
										,'DictamenRecado'
										,'DictamenVisSContactoIDV2'
										,'DictamenVAdicIV2'
										,'DictamenIDV2AcNoDisponible'
										)
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
						SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (6)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario
					END
				END
			END
		END
		ELSE IF @fc_Delegacion = '%'
		BEGIN
			IF @fc_idUsuarioPadre = '%'
				AND @fc_idUsuario = '%' -- La consulta es todo, despacho y/o delegacion
			BEGIN
				PRINT 'TODO NULO MENOS DESPACHO'

				/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
				SELECT @nTotal_London_Count = isnull(sum(ISNULL(C.CantCreditos, 0)), 0)
				FROM Dominio d
				LEFT JOIN (
					SELECT TX_NOMBRE_DESPACHO
						,cv_delegacion
						,COUNT(CV_CREDITO) CantCreditos
					FROM Creditos
					WHERE CV_RUTA = @TipoFormulario
					GROUP BY TX_NOMBRE_DESPACHO
						,cv_delegacion
					) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
				JOIN CatDelegaciones z ON c.CV_DELEGACION = z.Delegacion
				WHERE d.idDominio > 2
					AND d.Estatus = 1
					AND c.TX_NOMBRE_DESPACHO = @fc_Despacho

				/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
				SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
				FROM creditos c
				LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (
						1
						,11
						,12
						,15
						,3
						,4
						,5
						,6
						)
					AND o.idUsuario <> 0
					AND o.idDominio > 2
					AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
					AND c.CV_RUTA = @TipoFormulario

				IF @Bloque != 0
				BEGIN
					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
					SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
						,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
						,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							3
							,4
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
						,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							)
						AND o.idVisita > 1
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							)
						AND o.idUsuario = 0
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
					IF @fc_DashBoard <> 'Supervisor'
						AND @fc_DashBoard <> 'Gestor'
					BEGIN
						SELECT @nTotal_Sin_Orden_Count = isnull(sum(ISNULL(C.CantCreditos, 0)), 0)
						FROM Dominio d
						LEFT JOIN (
							SELECT TX_NOMBRE_DESPACHO
								,cv_delegacion
								,COUNT(CV_CREDITO) CantCreditos
							FROM Creditos
							WHERE CV_CREDITO NOT IN (
									SELECT num_cred
									FROM Ordenes
									WHERE Estatus IN (
											1
											,11
											,12
											,15
											,3
											,4
											,5
											,6
											)
									)
								AND CV_RUTA = @TipoFormulario
							GROUP BY TX_NOMBRE_DESPACHO
								,cv_delegacion
							) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
						JOIN CatDelegaciones z ON c.CV_DELEGACION = z.Delegacion
						WHERE d.idDominio > 2
							AND d.Estatus = 1
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
					END

					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (4)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (3)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de reasignados ++++++++++*/
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					JOIN Respuestas r ON o.idOrden = r.idOrden
					WHERE o.Estatus IN (3)
						AND r.idCampo IN (
							SELECT idcampo
							FROM CamposRespuesta
							WHERE Nombre IN (
									'DictamenNopueaten'
									,'DictamenAcreditadoNoDisponible'
									,'Dictamentercvissincont'
									,'Dictamenavisoretencion'
									,'DictamenRecado'
									,'DictamenVisSContactoIDV2'
									,'DictamenVAdicIV2'
									,'DictamenIDV2AcNoDisponible'
									)
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (6)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario
				END
			END
			ELSE
			BEGIN
				IF @fc_idUsuario = '%' -- La consulta es por supervisor
				BEGIN
					PRINT 'TODO NULO MENOS DESPACHO Y USUARIO PADRE'

					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					SELECT @nTotal_London_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND c.CV_RUTA = @TipoFormulario

					IF @Bloque != 0
					BEGIN
						/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
						SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
							,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								3
								,4
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
						SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idVisita > 1
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idUsuario = 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
						SET @nTotal_Sin_Orden_Count = 0

						/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
						SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (4)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
						SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (3)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de reasignados ++++++++++*/
						SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						JOIN Respuestas r ON o.idOrden = r.idOrden
						WHERE o.Estatus IN (3)
							AND r.idCampo IN (
								SELECT idcampo
								FROM CamposRespuesta
								WHERE Nombre IN (
										'DictamenNopueaten'
										,'DictamenAcreditadoNoDisponible'
										,'Dictamentercvissincont'
										,'Dictamenavisoretencion'
										,'DictamenRecado'
										,'DictamenVisSContactoIDV2'
										,'DictamenVAdicIV2'
										,'DictamenIDV2AcNoDisponible'
										)
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
						SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (6)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario
					END
				END
				ELSE
				BEGIN
					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					PRINT 'TODO NULO MENOS DESPACHO Y USUARIO HIJO'

					IF @fc_idUsuarioPadre = '%'
					BEGIN
						SELECT @fc_idUsuarioPadre = idPadre
						FROM RelacionUsuarios
						WHERE idHijo = @fc_idUsuario
					END

					SELECT @nTotal_London_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND O.IdUsuario = @fc_idUsuario
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND O.IdUsuario = @fc_idUsuario
						AND c.CV_RUTA = @TipoFormulario

					IF @Bloque != 0
					BEGIN
						/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
						SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
							,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								3
								,4
								,6
								)
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
						SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idVisita > 1
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idUsuario = 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
						SET @nTotal_Sin_Orden_Count = 0

						/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
						SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (4)
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
						SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (3)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de reasignados ++++++++++*/
						SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						JOIN Respuestas r ON o.idOrden = r.idOrden
						WHERE o.Estatus IN (3)
							AND r.idCampo IN (
								SELECT idcampo
								FROM CamposRespuesta
								WHERE Nombre IN (
										'DictamenNopueaten'
										,'DictamenAcreditadoNoDisponible'
										,'Dictamentercvissincont'
										,'Dictamenavisoretencion'
										,'DictamenRecado'
										,'DictamenVisSContactoIDV2'
										,'DictamenVAdicIV2'
										,'DictamenIDV2AcNoDisponible'
										)
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
						SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (6)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario
					END
				END
			END
		END
		ELSE
		BEGIN
			IF @fc_idUsuarioPadre = '%'
				AND @fc_idUsuario = '%' -- La consulta es todo, despacho y/o delegacion
			BEGIN
				PRINT 'TODO NULO MENOS DELEGACION Y DESPACHO'

				/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
				SELECT @nTotal_London_Count = isnull(sum(ISNULL(C.CantCreditos, 0)), 0)
				FROM Dominio d
				LEFT JOIN (
					SELECT TX_NOMBRE_DESPACHO
						,cv_delegacion
						,COUNT(CV_CREDITO) CantCreditos
					FROM Creditos
					WHERE CV_RUTA = @TipoFormulario
					GROUP BY TX_NOMBRE_DESPACHO
						,cv_delegacion
					) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
				JOIN CatDelegaciones z ON c.CV_DELEGACION = z.Delegacion
				WHERE d.idDominio > 2
					AND d.Estatus = 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND c.TX_NOMBRE_DESPACHO = @fc_Despacho

				/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
				SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
				FROM creditos c
				LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (
						1
						,11
						,12
						,15
						,3
						,4
						,5
						,6
						)
					AND o.idUsuario <> 0
					AND o.idDominio > 2
					AND c.CV_DELEGACION = @fc_Delegacion
					AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
					AND c.CV_RUTA = @TipoFormulario

				IF @Bloque != 0
				BEGIN
					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
					SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
						,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
						,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							3
							,4
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
						,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							)
						AND o.idVisita > 1
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							)
						AND o.idUsuario = 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
					IF @fc_DashBoard <> 'Supervisor'
						AND @fc_DashBoard <> 'Gestor'
					BEGIN
						SELECT @nTotal_Sin_Orden_Count = isnull(sum(ISNULL(C.CantCreditos, 0)), 0)
						FROM Dominio d
						LEFT JOIN (
							SELECT TX_NOMBRE_DESPACHO
								,cv_delegacion
								,COUNT(CV_CREDITO) CantCreditos
							FROM Creditos
							WHERE CV_CREDITO NOT IN (
									SELECT num_cred
									FROM Ordenes
									WHERE Estatus IN (
											1
											,11
											,12
											,15
											,3
											,4
											,5
											,6
											)
									)
								AND CV_RUTA = @TipoFormulario
							GROUP BY TX_NOMBRE_DESPACHO
								,cv_delegacion
							) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
						JOIN CatDelegaciones z ON c.CV_DELEGACION = z.Delegacion
						WHERE d.idDominio > 2
							AND d.Estatus = 1
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
					END

					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (4)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (3)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de reasignados ++++++++++*/
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					JOIN Respuestas r ON o.idOrden = r.idOrden
					WHERE o.Estatus IN (3)
						AND r.idCampo IN (
							SELECT idcampo
							FROM CamposRespuesta
							WHERE Nombre IN (
									'DictamenNopueaten'
									,'DictamenAcreditadoNoDisponible'
									,'Dictamentercvissincont'
									,'Dictamenavisoretencion'
									,'DictamenRecado'
									,'DictamenVisSContactoIDV2'
									,'DictamenVAdicIV2'
									,'DictamenIDV2AcNoDisponible'
									)
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (6)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND c.CV_RUTA = @TipoFormulario
				END
			END
			ELSE
			BEGIN
				IF @fc_idUsuario = '%' -- La consulta es por supervisor
				BEGIN
					PRINT 'TODO NULO MENOS DELEGACION Y DESPACHO Y USUARIO PADRE'

					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					SELECT @nTotal_London_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idUsuario <> 0
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND c.CV_RUTA = @TipoFormulario

					IF @Bloque != 0
					BEGIN
						/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
						SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
							,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								3
								,4
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
						SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idVisita > 1
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idUsuario = 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
						SET @nTotal_Sin_Orden_Count = 0

						/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
						SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (4)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
						SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (3)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de reasignados ++++++++++*/
						SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						JOIN Respuestas r ON o.idOrden = r.idOrden
						WHERE o.Estatus IN (3)
							AND r.idCampo IN (
								SELECT idcampo
								FROM CamposRespuesta
								WHERE Nombre IN (
										'DictamenNopueaten'
										,'DictamenAcreditadoNoDisponible'
										,'Dictamentercvissincont'
										,'Dictamenavisoretencion'
										,'DictamenRecado'
										,'DictamenVisSContactoIDV2'
										,'DictamenVAdicIV2'
										,'DictamenIDV2AcNoDisponible'
										)
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
						SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (6)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND c.CV_RUTA = @TipoFormulario
					END
				END
				ELSE
				BEGIN
					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					PRINT 'TODO NULO MENOS DELEGACION Y DESPACHO Y USUARIO HIJO'

					IF @fc_idUsuarioPadre = '%'
					BEGIN
						SELECT @fc_idUsuarioPadre = idPadre
						FROM RelacionUsuarios
						WHERE idHijo = @fc_idUsuario
					END

					SELECT @nTotal_London_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND O.IdUsuario = @fc_idUsuario
						AND c.CV_RUTA = @TipoFormulario

					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden), 0)
					FROM creditos c
					LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (
							1
							,11
							,12
							,15
							,3
							,4
							,5
							,6
							)
						AND o.idDominio > 2
						AND c.CV_DELEGACION = @fc_Delegacion
						AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
						AND O.IdUsuarioPadre = @fc_idUsuarioPadre
						AND O.IdUsuario = @fc_idUsuario
						AND c.CV_RUTA = @TipoFormulario

					IF @Bloque != 0
					BEGIN
						/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
						SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Gestores_Visitantes_Count = isnull(COUNT(DISTINCT o.idUsuario), 0)
							,@nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								3
								,4
								,6
								)
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
						SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden), 0)
							,@nTotal_Visitas_Realizadas_Count = @nTotal_Visitas_Realizadas_Count + isnull(sum(o.idVisita - 1), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idVisita > 1
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								)
							AND o.idUsuario = 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/
						SET @nTotal_Sin_Orden_Count = 0

						/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
						SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (
								1
								,11
								,12
								,15
								,6
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
						SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (4)
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
						SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (3)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de reasignados ++++++++++*/
						SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						JOIN Respuestas r ON o.idOrden = r.idOrden
						WHERE o.Estatus IN (3)
							AND r.idCampo IN (
								SELECT idcampo
								FROM CamposRespuesta
								WHERE Nombre IN (
										'DictamenNopueaten'
										,'DictamenAcreditadoNoDisponible'
										,'Dictamentercvissincont'
										,'Dictamenavisoretencion'
										,'DictamenRecado'
										,'DictamenVisSContactoIDV2'
										,'DictamenVAdicIV2'
										,'DictamenIDV2AcNoDisponible'
										)
								)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario

						/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
						SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden), 0)
						FROM creditos c
						LEFT JOIN Ordenes o ON c.CV_CREDITO = o.num_Cred
						WHERE o.Estatus IN (6)
							AND o.idUsuario <> 0
							AND o.idDominio > 2
							AND c.CV_DELEGACION = @fc_Delegacion
							AND c.TX_NOMBRE_DESPACHO = @fc_Despacho
							AND O.IdUsuarioPadre = @fc_idUsuarioPadre
							AND O.IdUsuario = @fc_idUsuario
							AND c.CV_RUTA = @TipoFormulario
					END
				END
			END
		END

		/*บบบบบบบบบบบบบบบ Inicio: Parte 1 บบบบบบบบบบบบบบบบบบบบบบบบบบ*/
		IF @Bloque = 0
		BEGIN
			--+++++++++++++++ inicia: CREDITOS ASIGNADOS POR POOL ++++++--
			SET @SubAccion = 'Creditos Asignados por Pool y TDAM'

			IF @nTotal_London_Count = 0
				SET @nTotal_London_Porc = 0
			ELSE
				SET @nTotal_London_Porc = 100

			--+++++++++++++++ final: CREDITOS ASIGNADOS POR POOL +++++++--
			--+++++++++++++++ inicia: CREDITOS ASIGNADOS POR MOViL +++++++--
			SET @SubAccion = 'Creditos Asignados por Movil y CAGD'
			SET @nTotal_London_Porc = 100

			IF @nTotal_London_Count = 0
				SET @nTotal_Ordenes_Porc = 0
			ELSE
				SET @nTotal_Ordenes_Porc = (@nTotal_Ordenes_Count * 100) / @nTotal_London_Count

			--++++++++++++++ Final: CREDITOS ASIGNADOS POR MOViL ++++++++--
			SELECT @nTotal_London_Count value
				,@nTotal_London_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_CREDASIGPOOL'
			
			UNION ALL
			
			SELECT @nTotal_Ordenes_Count value
				,@nTotal_Ordenes_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_CREDASIGMOVIL'
		END

		/*บบบบบบบบบบบบบบบบบบบ Final: Parte 1 บบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบ*/
		/*บบบบบบบบบบบบบบบบบบบ Inicio: Parte 2 บบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบ*/
		IF @Bloque = 1
		BEGIN
			--++++++++ Calculos: Cr้ditos Visitados, pendientes por revisitar y no Visitados +++++++++++++++++++--
			SET @SubAccion = 'Creditos Visitados'
			SET @nTotal_London_Porc = 100

			IF @nTotal_London_Count = 0
				SET @nTotal_Visitados_Porc = 0
			ELSE
				SET @nTotal_Visitados_Porc = (@nTotal_Visitados_Count * 100) / @nTotal_London_Count

			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			--++++++++ Calculos: Creditos pendientes por revisitar +++++++++++++++++++--
			SET @SubAccion = 'Creditos pendientes por revisitar'
			SET @nTotal_London_Porc = 100

			IF @nTotal_London_Count = 0
				SET @nTotal_ReVisitados_Porc = 0
			ELSE
				SET @nTotal_ReVisitados_Porc = (@nTotal_ReVisitados_Count * 100) / @nTotal_London_Count

			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			--++++++++ Calculos: Creditos sin asignar en movil +++++++++++++++++++--
			SET @SubAccion = 'Creditos sin asignar en movil'
			SET @nTotal_London_Porc = 100

			IF @nTotal_London_Count = 0
				SET @nTotal_Cred_Sin_Asig_Porc = 0
			ELSE
				SET @nTotal_Cred_Sin_Asig_Porc = (@nTotal_Cred_Sin_Asig_Count * 100) / @nTotal_London_Count

			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			--++++++++ Calculos: Creditos sin asignar sin orden +++++++++++++++++++--
			SET @SubAccion = 'Creditos sin asignar sin orden'
			SET @nTotal_London_Porc = 100

			IF @nTotal_London_Count = 0
				SET @nTotal_Sin_Orden_Porc = 0
			ELSE
				SET @nTotal_Sin_Orden_Porc = (@nTotal_Sin_Orden_Count * 100) / @nTotal_London_Count

			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			--++++++++ Calculos: Creditos en movil +++++++++++++++++++--
			SET @SubAccion = 'Creditos en movil'
			SET @nTotal_London_Porc = 100

			IF @nTotal_London_Count = 0
				SET @nTotal_Cred_Asig_Mov_Porc = 0
			ELSE
				SET @nTotal_Cred_Asig_Mov_Porc = (@nTotal_Cred_Asig_Mov_Count * 100) / @nTotal_London_Count

			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--			
			--+++++++++++++++++++ inicia: Dias del Mes +++++++++++++++++++--		
			SELECT day(dateadd(mm, datediff(mm, - 1, GETDATE()), - 1)) value
				,100 porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_TOTALMES'
			--+++++++++++++++++++ fin: Dias del Mes +++++++++++++++++++--
			
			UNION ALL
			
			--+++++++++++++++++++ inicia: Dias restantes +++++++++++++++++++--	
			SELECT day(dateadd(mm, datediff(mm, - 1, GETDATE()), - 1)) - DAY(getdate()) value
				,abs(((day(dateadd(mm, datediff(mm, - 1, GETDATE()), - 1)) - DAY(getdate())) * 100) / datediff(day, GETDATE(), dateadd(month, 1, getdate()))) porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_DIASREST'
			--+++++++++++++++++++ Fin: Dias restantes +++++++++++++++++++--
			
			UNION ALL
			
			--++++++++ inicia: Cr้ditos Visitados, pendientes por revisitar y no Visitados +++++++++++++++++++--
			SELECT @nTotal_Visitados_Count value
				,@nTotal_Visitados_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_CREDVISITADOS'
			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			
			UNION ALL
			
			--++++++++ inicia: Creditos pendientes por revisitar +++++++++++++++++++--			
			SELECT @nTotal_ReVisitados_Count value
				,@nTotal_ReVisitados_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_GESTNVAVISITA'
			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			
			UNION ALL
			
			--++++++++ inicia: Cr้ditos sin asignar, en movil +++++++++++++++++++--
			SELECT @nTotal_Cred_Sin_Asig_Count value
				,@nTotal_Cred_Sin_Asig_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_CREDSINASIG'
			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			
			UNION ALL
			
			--++++++++ inicia: Cr้ditos sin asignar, sin orden +++++++++++++++++++--			
			SELECT @nTotal_Sin_Orden_Count value
				,@nTotal_Sin_Orden_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_CREDSINORD'
				AND (
					@fc_DashBoard <> 'Supervisor'
					AND @fc_DashBoard <> 'Gestor'
					)
			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			
			UNION ALL
			
			--++++++++ Inicia: Creditos en movil +++++++++++++++++++--
			SELECT @nTotal_Cred_Asig_Mov_Count value
				,@nTotal_Cred_Asig_Mov_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_CREDENMOVIL'
				--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
		END

		/*บบบบบบบบบบบบบบบบบบบ Fin : Parte 2 บบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบ*/
		/*บบบบบบบบบบบบบบบบบบบ Inicio : Parte 3 บบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบ*/
		IF @Bloque = 2
		BEGIN
			--++++++++ Calculos: Visitas Realizadas +++++++++++++++++++--
			SET @SubAccion = 'Visitas Realizadas'

			IF @nTotal_Visitas_Realizadas_Count = 0
				SET @nTotal_Visitas_Realizadas_Porc = 0
			ELSE
				SET @nTotal_Visitas_Realizadas_Porc = 100

			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			--++++++++ Calculos: Gestiones autorizadas +++++++++++++++++++--
			SET @SubAccion = 'Gestiones autorizadas'

			IF @nTotal_Finalizados_Count = 0
				SET @nTotal_Finalizados_Porc = 0
			ELSE
				SET @nTotal_Finalizados_Porc = (@nTotal_Finalizados_Count * 100) / @nTotal_London_Count

			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			--++++++++ Calculos: Gestiones no autorizadas +++++++++++++++++++--
			SET @SubAccion = 'Gestiones no autorizadas'

			IF @nTotal_Visitados_NoAuth_Count = 0
				SET @nTotal_Visitados_NoAuth_Porc = 0
			ELSE
				SET @nTotal_Visitados_NoAuth_Porc = (@nTotal_Visitados_NoAuth_Count * 100) / @nTotal_London_Count

			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			--++++++++ Calculos: Gestiones por reasignar +++++++++++++++++++--
			SET @SubAccion = 'Gestiones por reasignar'

			IF @nTotal_Reasignados_Count = 0
				SET @nTotal_Reasignados_Porc = 0
			ELSE
				SET @nTotal_Reasignados_Porc = (@nTotal_Reasignados_Count * 100) / @nTotal_London_Count

			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			--++++++++ Calculos: Gestiones en sincronizando +++++++++++++++++++--
			SET @SubAccion = 'Gestiones en sincronizando'

			IF @nTotal_Sincro_Count = 0
				SET @nTotal_Sincro_Porc = 0
			ELSE
				SET @nTotal_Sincro_Porc = (@nTotal_Sincro_Count * 100) / @nTotal_London_Count

			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--					
			--++++++++ Calculos: Gestiones visitadas promedio +++++++++++++++++++--
			SET @SubAccion = 'Gestiones visitadas promedio'

			IF @nTotal_Visitados_Count > 0
				AND @nTotal_Gestores_Visitantes_Count > 0
			BEGIN
				SET @nTotal_Promedio_Visitas_X_Gestor_Count = (@nTotal_Visitados_Count / @nTotal_Gestores_Visitantes_Count)
				SET @nTotal_Promedio_visitas_X_Gestor_Porc = (@nTotal_Promedio_Visitas_X_Gestor_Count * 100) / @nTotal_London_Count
			END
			ELSE
			BEGIN
				SET @nTotal_Promedio_Visitas_X_Gestor_Count = 0
				SET @nTotal_Promedio_Visitas_X_Gestor_Porc = 0
			END

			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			--+++++++++++ inicia: Visitas Realizadas +++++++++++++++++--
			SELECT @nTotal_Visitas_Realizadas_Count value
				,@nTotal_Visitas_Realizadas_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_VISITADOSREAL'
			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			
			UNION ALL
			
			--+++++++++++ inicia: Gestiones autorizadas +++++++++++++++++--
			SELECT @nTotal_Finalizados_Count value
				,@nTotal_Finalizados_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_CREDCONSOLUCI'
			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			
			UNION ALL
			
			--+++++++++++ inicia: Gestiones no autorizadas +++++++++++++++++--
			SELECT @nTotal_Visitados_NoAuth_Count value
				,@nTotal_Visitados_NoAuth_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_GESTSINAUTH'
			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			
			UNION ALL
			
			--+++++++++++ inicia: Gestiones por reasignar +++++++++++++++++--
			SELECT @nTotal_Reasignados_Count value
				,@nTotal_Reasignados_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_GESTREASIG'
			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--										
			
			UNION ALL
			
			--+++++++++++ inicia: Gestiones visitadas promedio +++++++++++++++++--
			SELECT @nTotal_Promedio_Visitas_X_Gestor_Count value
				,@nTotal_Promedio_Visitas_X_Gestor_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_GESTVISPROM'
			--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
			
			UNION ALL
			
			--+++++++++++ inicia: Gestiones en sincronizando +++++++++++++++++--
			SELECT @nTotal_Sincro_Count value
				,@nTotal_Sincro_Porc porcentaje
				,ud.fc_Clave AS fc_Clave
				,ud.fc_Descripcion AS descripcion
				,ud.fi_Parte AS fi_Parte
			FROM [Utils_Descripciones] ud
			WHERE ud.fc_Clave = 'DASH_GESTSINCRO'
				--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
		END
				/*บบบบบบบบบบบบบบบบบบบบ Final: Parte 3 บบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบ*/
	END TRY

	BEGIN CATCH
		SET @Error = 'Mod: ' + ISNULL(@Accion, '') + ' SubMod: ' + ISNULL(@SubAccion, '') + ' DashBoard: ' + ISNULL(@fc_DashBoard, '') + ' Nom_Corto: ' + ISNULL(@fc_NombreCorto, '') + ERROR_MESSAGE()

		GOTO Error
	END CATCH

	--Final del proceso...
	RETURN;

	Error:

	RAISERROR (
			@Error
			,1
			,1
			)

	RETURN;
END --Final del Procedure
