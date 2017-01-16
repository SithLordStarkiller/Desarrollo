
/****** Object:  StoredProcedure [dbo].[ObtenerIndicadorBloqueDashboard]    Script Date: 10/05/2015 16:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************************************************
* Desarrollador: Mauricio López Sánchez
* Proyecto:	London-PubliPayments-Formiik
* Fecha de Creación: 29/04/2014
* Descripción de Creacion: Manejador de DashBoard Administrador
* Ultima Fecha de Modificaciòn: 31/10/2014
* Modificó: Pablo Jaimes
* Descripciòn de ultima modificacion: Cambio fecha para meses con 31 dias      
* Ultima Fecha de Modificaciòn: 28/01/2015
* Modificó: Alberto Rojas
* Descripciòn de ultima modificacion: Se agrega filtro por el tipo de formulario que se maneja
* Ultima Fecha de Modificaciòn: 10/02/2015
* Modificó: Pablo Jaimes
* Descripciòn de ultima modificacion: Se agrega indicador por status Sincronizando
****************************************************************************************************************/
ALTER PROCEDURE [dbo].[ObtenerIndicadorBloqueDashboard](
		@Accion					varchar(100) = null
		,@SubAccion				varchar(100) = null
		,@Bloque				int=null
		--Datos del usuario que esta ejecutando....
		,@fi_Usuario			int = null		
		,@fc_Usuario			varchar(50) = null		
		,@fi_Dominio			int = null		
		,@fi_Rol				int = null	
		--Datos que quiere visualizar.....
		,@fc_DashBoard			varchar(100) = null		
		,@fc_Delegacion			varchar(100) = null
		,@fc_Despacho			varchar(100) = null
		,@fc_Estado				varchar(100) = null
		,@fc_idUsuarioPadre		varchar(100) = null
		,@fc_idUsuario			varchar(100) = null
		,@debug					varchar(10) = null
		,@TipoFormulario VARCHAR(10) = null
		,@esCallCenter   varchar(5)='false'
) with recompile
AS
BEGIN -- Inicio del procedure.
		
	--Configuracion
	Set DATEFORMAT DMY;
	
	--Variables
	Declare @Error varchar(1000)
	Declare @nTotal_Sincro_Count int
	Declare @nTotal_Sincro_Porc int
	Declare @nTotal_London_Count int
	Declare @nTotal_London_Porc int
	Declare @nTotal_Ordenes_Count int
	Declare @nTotal_Ordenes_Porc int		
	Declare @nTotal_BitacoraCC_Count int
	Declare @nTotal_BitacoraCC_Porc int		
	Declare @nTotal_Respuestas_Count int
	Declare @nTotal_Respuestas_Porc int	
	
	Declare @nTotal_Visitados_Count int
	Declare @nTotal_Visitados_Porc int	
	Declare @nTotal_NoVisitados_Count int
	Declare @nTotal_NoVisitados_Porc int
	Declare @nTotal_Finalizados_Count int
	Declare @nTotal_Finalizados_Porc int
	Declare @nTotal_Visitados_NoAuth_Count int
	Declare @nTotal_Visitados_NoAuth_Porc int
	Declare @nTotal_Reasignados_Count int
	Declare @nTotal_Reasignados_Porc int
	Declare @nTotal_Gestores_Visitantes_Count int
	Declare @nTotal_Promedio_Visitas_X_Gestor_Count int
	Declare @nTotal_Promedio_visitas_X_Gestor_Porc int 
	Declare @nTotal_ReVisitados_Count int
	Declare @nTotal_ReVisitados_Porc int
	Declare @nTotal_Visitas_Realizadas_Count int
	Declare @nTotal_Visitas_Realizadas_Porc int
	Declare @nTotal_Cred_Sin_Asig_Count int
	Declare @nTotal_Cred_Sin_Asig_Porc int
	Declare @nTotal_Cred_Asig_Mov_Count int
	Declare @nTotal_Cred_Asig_Mov_Porc int
	Declare @nTotal_Sin_Orden_Count int
	Declare @nTotal_Sin_Orden_Porc int
	Declare @formularios nvarchar(max)
	
	Declare @nTotal_Temp_Count int
	Declare @nTotal_Temp_Porc int
	
	Declare @nValue bigint
	
	--Datos Extra
	Declare @fc_NombreCorto varchar(40)		
	Declare @nDate_Now bigint
	
	Set @fc_NombreCorto = ''
	Set @Error = ''
				
	--Inicio de Proceso...	
	BEGIN TRY
	
		--Validaciones iniciales
		set @nDate_Now = CONVERT(bigint,CONVERT(varchar,getdate(),112))
		select @fc_NombreCorto = d.nom_corto 
		from Usuario u join Dominio d on u.idDominio = d.idDominio 
		where idUsuario = @fi_Usuario
		
		select @fc_Delegacion=Case when @fc_Delegacion='False' then Delegacion else @fc_Delegacion  end from RelacionDelegaciones where idUsuario=@fi_Usuario
								
		--Comienzo a calcular los indicadores....   

		if @fc_Delegacion = '%' and @fc_Despacho = '%'
		begin
			IF @fc_idUsuarioPadre = '%' and @fc_idUsuario = '%' -- La consulta es todo, despacho y/o delegacion
			BEGIN
				print 'TODO NULO'
				
				/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
				SELECT @nTotal_London_Count = isnull(sum(ISNULL(C.CantCreditos, 0)),0)
				FROM Dominio d 
				LEFT JOIN (SELECT  TX_NOMBRE_DESPACHO, cv_delegacion,  COUNT(CV_CREDITO) CantCreditos FROM Creditos 
				WHERE CV_RUTA=@TipoFormulario GROUP BY TX_NOMBRE_DESPACHO, cv_delegacion) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
				join CatDelegaciones z on c.CV_DELEGACION = z.Delegacion
				WHERE d.idDominio > 1 AND d.Estatus = 1
			
				/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
			
				SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(1,11,12,15,3,4,5,6)
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden),0), @nTotal_Gestores_Visitantes_Count = isnull(COUNT(distinct o.idUsuario),0), @nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(3,4,6)
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_RUTA=@TipoFormulario
	

				/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
		
				SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15)
				AND o.idVisita > 1
				AND o.idDominio > 1
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15)
				and o.idUsuario = 0
				AND o.idDominio > 1
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

				if @fc_DashBoard <> 'Supervisor' and @fc_DashBoard <> 'Gestor'
				begin
					SELECT @nTotal_Sin_Orden_Count = isnull(sum(ISNULL(C.CantCreditos, 0)),0)
					FROM Dominio d 
					LEFT JOIN (
						SELECT  TX_NOMBRE_DESPACHO, cv_delegacion,  COUNT(CV_CREDITO) CantCreditos 
						FROM Creditos 
						WHERE CV_CREDITO NOT in (select num_cred from Ordenes where Estatus in (1,11,12,15,3,4,5,6))
						AND  CV_RUTA=@TipoFormulario
						GROUP BY TX_NOMBRE_DESPACHO, cv_delegacion
					) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
					join CatDelegaciones z on c.CV_DELEGACION = z.Delegacion
					WHERE d.idDominio > 1 AND d.Estatus = 1
				end

				/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15,6)
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_RUTA=@TipoFormulario

				/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
		
				SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(4)
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
		
				SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(3)
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de reasignados ++++++++++*/
		
				SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				join Respuestas r on o.idOrden = r.idOrden
				WHERE o.Estatus in(3)
				AND r.idCampo in (select idcampo from CamposRespuesta where Nombre in 
				('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_RUTA=@TipoFormulario
				
				/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
				
				SELECT @nTotal_Sincro_Count= isnull(sum(ISNULL(C.CantCreditos, 0)),0)
				FROM Dominio d 
				LEFT JOIN (select COUNT(o.idOrden) as CantCreditos,o.idDominio from Ordenes o left join Creditos c on c.CV_CREDITO=o.num_Cred where o.Estatus=6 
				AND CV_RUTA=@TipoFormulario group by idDominio) C on C.idDominio=d.idDominio
				WHERE d.idDominio > 1 AND d.Estatus = 1
							
			END
			ELSE
			BEGIN
				IF @fc_idUsuario = '%' -- La consulta es por supervisor
				BEGIN
					PRINT 'TODO NULO MENOS USUARIO PADRE'
					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/

					SELECT @nTotal_London_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/

					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/

					SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden),0), @nTotal_Gestores_Visitantes_Count = isnull(COUNT(distinct o.idUsuario),0), @nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3,4,6)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/

					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					AND o.idVisita > 1
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					AND o.idUsuario = 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

					SET @nTotal_Sin_Orden_Count = 0
					
					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15,6)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					
					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/

					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(4)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
			
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de reasignados ++++++++++*/
			
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					join Respuestas r on o.idOrden = r.idOrden
					WHERE o.Estatus in(3)
					AND r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					
					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					
					SELECT @nTotal_Sincro_Count= isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(6)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
				END
				ELSE
				BEGIN
					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					PRINT 'TODO NULO MENOS USUARIO HIJO'
					IF @fc_idUsuarioPadre = '%'
					BEGIN
						select @fc_idUsuarioPadre = idPadre from RelacionUsuarios where idHijo = @fc_idUsuario
					END
				
					SELECT @nTotal_London_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
				
					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
	
					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
				
					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/

					SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden),0), @nTotal_Gestores_Visitantes_Count = isnull(COUNT(distinct o.idUsuario),0), @nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3,4,6)
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
				
					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/

					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					AND o.idVisita > 1
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					AND o.idUsuario = 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

					SET @nTotal_Sin_Orden_Count = 0

					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15,6)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario

					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
				
					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(4)
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND O.IdUsuario = @fc_idUsuario				
					AND c.CV_RUTA=@TipoFormulario
					
					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
		
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de reasignados ++++++++++*/
			
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					join Respuestas r on o.idOrden = r.idOrden
					WHERE o.Estatus in(3)
					AND r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
					
					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					
					SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(6)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
				END
			END
		end
		else if @fc_Despacho = '%'
		begin
			IF @fc_idUsuarioPadre = '%' and @fc_idUsuario = '%' -- La consulta es todo, despacho y/o delegacion
			BEGIN
				PRINT 'TODO NULO MENOS DELEGACION'
				/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
				SELECT @nTotal_London_Count = isnull(sum(ISNULL(C.CantCreditos, 0)),0)
				FROM Dominio d 
				LEFT JOIN (SELECT  TX_NOMBRE_DESPACHO, cv_delegacion,  COUNT(CV_CREDITO) CantCreditos 
				FROM Creditos WHERE CV_RUTA=@TipoFormulario  GROUP BY TX_NOMBRE_DESPACHO, cv_delegacion) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
				join CatDelegaciones z on c.CV_DELEGACION = z.Delegacion
				WHERE d.idDominio > 1 AND d.Estatus = 1  
				and c.CV_DELEGACION = @fc_Delegacion
			
				/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
			
				SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(1,11,12,15,3,4,5,6)
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_DELEGACION = @fc_Delegacion
				AND c.CV_RUTA=@TipoFormulario
				/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden),0), @nTotal_Gestores_Visitantes_Count = isnull(COUNT(distinct o.idUsuario),0), @nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(3,4,6)
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_DELEGACION = @fc_Delegacion
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
		
				SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15)
				AND o.idVisita > 1
				AND o.idDominio > 1
				AND c.CV_DELEGACION = @fc_Delegacion	
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15)
				AND o.idUsuario = 0
				AND o.idDominio > 1
				AND c.CV_DELEGACION = @fc_Delegacion	
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

				if @fc_DashBoard <> 'Supervisor' and @fc_DashBoard <> 'Gestor'
				begin
					SELECT @nTotal_Sin_Orden_Count = isnull(sum(ISNULL(C.CantCreditos, 0)),0)
					FROM Dominio d 
					LEFT JOIN (
						SELECT  TX_NOMBRE_DESPACHO, cv_delegacion,  COUNT(CV_CREDITO) CantCreditos 
						FROM Creditos 
						WHERE CV_CREDITO NOT in (select num_cred from Ordenes where Estatus in (1,11,12,15,3,4,5,6))
						AND CV_RUTA=@TipoFormulario
						GROUP BY TX_NOMBRE_DESPACHO, cv_delegacion
					) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
					join CatDelegaciones z on c.CV_DELEGACION = z.Delegacion
					WHERE d.idDominio > 1 AND d.Estatus = 1
					and c.CV_DELEGACION = @fc_Delegacion
				end


				/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15,6)
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_DELEGACION = @fc_Delegacion	
				AND c.CV_RUTA=@TipoFormulario

				/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
		
				SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(4)
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_DELEGACION = @fc_Delegacion
				AND c.CV_RUTA=@TipoFormulario
				
				/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
		
				SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(3)
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_DELEGACION = @fc_Delegacion
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de reasignados ++++++++++*/
		
				SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				join Respuestas r on o.idOrden = r.idOrden
				WHERE o.Estatus in(3)
				AND r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_DELEGACION = @fc_Delegacion
				AND c.CV_RUTA=@TipoFormulario
				
				/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
				
				SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(6)
				AND o.idUsuario <> 0
				AND o.idDominio > 1
				AND c.CV_DELEGACION = @fc_Delegacion
				AND c.CV_RUTA=@TipoFormulario
			END
			ELSE
			BEGIN
				IF @fc_idUsuario = '%' -- La consulta es por supervisor
				BEGIN
					PRINT 'TODO NULO MENOS DELEGACION Y USUARIO PADRE'
					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/

					SELECT @nTotal_London_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/

					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/

					SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden),0), @nTotal_Gestores_Visitantes_Count = isnull(COUNT(distinct o.idUsuario),0), @nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3,4,6)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre					
					AND c.CV_RUTA=@TipoFormulario


					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/

					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					AND o.idVisita > 1
					AND o.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					AND o.idUsuario = 0
					AND o.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion	
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

					SET @nTotal_Sin_Orden_Count = 0
					
					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15,6)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion	
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					
					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/

					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(4)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
			
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de reasignados ++++++++++*/
			
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					join Respuestas r on o.idOrden = r.idOrden
					WHERE o.Estatus in(3)
					AND r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					
					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					
					SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(6)
					AND o.idUsuario <> 0
					AND o.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

				END
				ELSE
				BEGIN
					PRINT 'TODO NULO MENOS DELEGACION Y USUARIO HIJO'
					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					IF @fc_idUsuarioPadre = '%'
					BEGIN
						select @fc_idUsuarioPadre = idPadre from RelacionUsuarios where idHijo = @fc_idUsuario
					END
				
					SELECT @nTotal_London_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					AND c.CV_DELEGACION = @fc_Delegacion
					AND O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
				
					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
	
					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
				
					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/

					SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden),0), @nTotal_Gestores_Visitantes_Count = isnull(COUNT(distinct o.idUsuario),0), @nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3,4,6)
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
				
					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/

					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					and o.idVisita > 1
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					and o.idUsuario = 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion	
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

					SET @nTotal_Sin_Orden_Count = 0
					
					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15,6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion	
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
					
					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/

					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(4)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
				
					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(4)
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario			
					AND c.CV_RUTA=@TipoFormulario	
					
					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
			
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de reasignados ++++++++++*/
			
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					join Respuestas r on o.idOrden = r.idOrden
					WHERE o.Estatus in(3)
					and r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
					
					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					
					SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
				END
			END
		end
		else if @fc_Delegacion = '%'
		begin
			IF @fc_idUsuarioPadre = '%' and @fc_idUsuario = '%' -- La consulta es todo, despacho y/o delegacion
			BEGIN
				PRINT 'TODO NULO MENOS DESPACHO'
				/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
				SELECT @nTotal_London_Count = isnull(sum(ISNULL(C.CantCreditos, 0)),0)
				FROM Dominio d 
				LEFT JOIN (SELECT  TX_NOMBRE_DESPACHO, cv_delegacion,  COUNT(CV_CREDITO) CantCreditos 
				FROM Creditos WHERE CV_RUTA=@TipoFormulario GROUP BY TX_NOMBRE_DESPACHO, cv_delegacion) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
				join CatDelegaciones z on c.CV_DELEGACION = z.Delegacion
				WHERE d.idDominio > 1 AND d.Estatus = 1  
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
			
				/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
			
				SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(1,11,12,15,3,4,5,6)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden),0), @nTotal_Gestores_Visitantes_Count = isnull(COUNT(distinct o.idUsuario),0), @nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(3,4,6)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario
	
				/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
		
				SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15)
				and o.idVisita > 1
				AND o.idDominio > 1
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15)
				and o.idUsuario = 0
				AND o.idDominio > 1
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

				if @fc_DashBoard <> 'Supervisor' and @fc_DashBoard <> 'Gestor'
				begin
					SELECT @nTotal_Sin_Orden_Count = isnull(sum(ISNULL(C.CantCreditos, 0)),0)
					FROM Dominio d 
					LEFT JOIN (
						SELECT  TX_NOMBRE_DESPACHO, cv_delegacion,  COUNT(CV_CREDITO) CantCreditos 
						FROM Creditos 
						WHERE CV_CREDITO NOT in (select num_cred from Ordenes where Estatus in (1,11,12,15,3,4,5,6))
						AND CV_RUTA=@TipoFormulario
						GROUP BY TX_NOMBRE_DESPACHO, cv_delegacion
					) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
					join CatDelegaciones z on c.CV_DELEGACION = z.Delegacion
					WHERE d.idDominio > 1 AND d.Estatus = 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				end
				
				/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15,6)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

				/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
		
				SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(4)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
		
				SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(3)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de reasignados ++++++++++*/
		
				SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				join Respuestas r on o.idOrden = r.idOrden
				WHERE o.Estatus in(3)
				and r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario
				
				/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
				
				SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(6)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

			END
			ELSE
			BEGIN
				IF @fc_idUsuario = '%' -- La consulta es por supervisor
				BEGIN
					PRINT 'TODO NULO MENOS DESPACHO Y USUARIO PADRE'
					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/

					SELECT @nTotal_London_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/

					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/

					SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden),0), @nTotal_Gestores_Visitantes_Count = isnull(COUNT(distinct o.idUsuario),0), @nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3,4,6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/

					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15) 
					and o.idVisita > 1
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					and o.idUsuario = 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

					SET @nTotal_Sin_Orden_Count = 0
										
					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15,6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					
					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/

					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(4)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
			
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de reasignados ++++++++++*/
			
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					join Respuestas r on o.idOrden = r.idOrden
					WHERE o.Estatus in(3)
					and r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					
					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					
					SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

				END
				ELSE
				BEGIN
					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					PRINT 'TODO NULO MENOS DESPACHO Y USUARIO HIJO'
					IF @fc_idUsuarioPadre = '%'
					BEGIN
						select @fc_idUsuarioPadre = idPadre from RelacionUsuarios where idHijo = @fc_idUsuario
					END
				
					SELECT @nTotal_London_Count =case when @esCallCenter='true' then isnull(count(c.CV_CREDITO),0) else isnull(count(o.idOrden),0) end
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE 
					(
					(@esCallCenter='false' and o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho  and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario)
					or 
					( @esCallCenter='true' and c.CC_DESPACHO = @fc_Despacho)
					)
				
					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
	
					SELECT  @nTotal_Ordenes_Count =case when @esCallCenter='true' then isnull(count(c.CV_CREDITO),0) else isnull(count(o.idOrden),0) end
					FROM creditos c inner join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE (
					(@esCallCenter='false' and o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario)
					or 
					( @esCallCenter='true' and c.CC_DESPACHO = @fc_Despacho AND o.idUsuario <> 0)
					)
				
					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/

					SELECT  @nTotal_Visitados_Count =isnull(count(o.idOrden),0),   @nTotal_Gestores_Visitantes_Count =isnull(COUNT(distinct o.idUsuario),0),
					   @nTotal_Visitas_Realizadas_Count=isnull(SUM(o.idVisita),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE (
					(@esCallCenter='false' 
					and o.Estatus in(3,4,6)
					AND o.idDominio > 1
					AND c.TX_NOMBRE_DESPACHO = @fc_Despacho 
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario)
					or 
					( @esCallCenter='true' 
					and c.CC_DESPACHO = @fc_Despacho 
					and o.Estatus in(3,4,6)
					AND o.idDominio > 1
					)
					)
				
					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/

					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE (
					(@esCallCenter='false' and o.Estatus IN (1,11,12,15)
					and o.idVisita > 1
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho 
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario)
					or 
					( @esCallCenter='true' and c.CC_DESPACHO = @fc_Despacho
					and o.Estatus IN (1,11,12,15)
					and o.idVisita > 1
					AND o.idDominio > 1)
					)
					
					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE (
					(@esCallCenter='false' and o.Estatus IN (1,11,12,15)
					and o.idUsuario = 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho 
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario)
					or 
					( @esCallCenter='true' and c.CC_DESPACHO = @fc_Despacho  
					and o.Estatus IN (1,11,12,15)
					and o.idUsuario = 0
					AND o.idDominio > 1)
					)

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

					
					SELECT @nTotal_Sin_Orden_Count = case when @esCallCenter='true' then isnull(sum(ISNULL(C.CantCreditos, 0)),0) else 0 end 
					FROM Dominio d 
					LEFT JOIN (
						SELECT  TX_NOMBRE_DESPACHO, cv_delegacion,  COUNT(CV_CREDITO) CantCreditos 
						FROM Creditos 
						WHERE CV_CREDITO NOT in (select num_cred from Ordenes where Estatus in (1,11,12,15,3,4,5,6))
						AND  CV_RUTA=@TipoFormulario
						GROUP BY TX_NOMBRE_DESPACHO, cv_delegacion
					) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
					join CatDelegaciones z on c.CV_DELEGACION = z.Delegacion
					WHERE d.idDominio > 1 AND d.Estatus = 1
										
					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE (
					(@esCallCenter='false' and o.Estatus IN (1,11,12,15,6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho 
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario)
					or 
					( @esCallCenter='true' and c.CC_DESPACHO = @fc_Despacho and o.Estatus IN (1,11,12,15,6)
					and o.idUsuario <> 0
					AND o.idDominio > 1)
					)
					
					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
				
					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE (
					(@esCallCenter='false' and o.Estatus in(4)
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario				
					AND c.CV_RUTA=@TipoFormulario)
					or 
					( @esCallCenter='true' and c.CC_DESPACHO = @fc_Despacho  and o.Estatus in(4)
					AND o.idDominio > 1)
					)
					
					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
			
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE (
					(@esCallCenter='false' and o.Estatus in(3)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho 
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario				
					AND c.CV_RUTA=@TipoFormulario)
					or 
					( @esCallCenter='true' and c.CC_DESPACHO = @fc_Despacho and o.Estatus in(3)
					and o.idUsuario <> 0
					AND o.idDominio > 1)
					)

					/*++++++++ Calcular total de reasignados ++++++++++*/
			
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					join Respuestas r on o.idOrden = r.idOrden
					WHERE (
					(@esCallCenter='false' and o.Estatus in(3)
					and r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario				
					AND c.CV_RUTA=@TipoFormulario)
					or 
					( @esCallCenter='true' and c.CC_DESPACHO = @fc_Despacho and o.Estatus in(3)
					and r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
					and o.idUsuario <> 0
					AND o.idDominio > 1)
					)
					
					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					
					SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE (
					(@esCallCenter='false' and o.Estatus in(6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho 
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario				
					AND c.CV_RUTA=@TipoFormulario)
					or 
					( @esCallCenter='true' and c.CC_DESPACHO = @fc_Despacho and o.Estatus in(6)
					and o.idUsuario <> 0
					AND o.idDominio > 1)
					)
					
				END
			END
		end
		else 
		begin
			IF @fc_idUsuarioPadre = '%' and @fc_idUsuario = '%' -- La consulta es todo, despacho y/o delegacion
			BEGIN
				PRINT 'TODO NULO MENOS DELEGACION Y DESPACHO'
				/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
				SELECT @nTotal_London_Count = isnull(sum(ISNULL(C.CantCreditos, 0)),0)
				FROM Dominio d 
				LEFT JOIN (SELECT  TX_NOMBRE_DESPACHO, cv_delegacion,  COUNT(CV_CREDITO) CantCreditos FROM Creditos 
				WHERE CV_RUTA=@TipoFormulario GROUP BY TX_NOMBRE_DESPACHO, cv_delegacion) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
				join CatDelegaciones z on c.CV_DELEGACION = z.Delegacion
				WHERE d.idDominio > 1 AND d.Estatus = 1  
				and c.CV_DELEGACION = @fc_Delegacion
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
			
				/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
			
				SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(1,11,12,15,3,4,5,6)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.CV_DELEGACION = @fc_Delegacion
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden),0), @nTotal_Gestores_Visitantes_Count = isnull(COUNT(distinct o.idUsuario),0), @nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(3,4,6)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.CV_DELEGACION = @fc_Delegacion
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario
	
				/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/
		
				SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15)
				and o.idVisita > 1
				AND o.idDominio > 1
				and c.CV_DELEGACION = @fc_Delegacion
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario
				
				/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15)
				and o.idUsuario = 0
				AND o.idDominio > 1
				and c.CV_DELEGACION = @fc_Delegacion
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

				if @fc_DashBoard <> 'Supervisor' and @fc_DashBoard <> 'Gestor'
				begin
					SELECT @nTotal_Sin_Orden_Count = isnull(sum(ISNULL(C.CantCreditos, 0)),0)
					FROM Dominio d 
					LEFT JOIN (
						SELECT  TX_NOMBRE_DESPACHO, cv_delegacion,  COUNT(CV_CREDITO) CantCreditos 
						FROM Creditos 
						WHERE CV_CREDITO NOT in (select num_cred from Ordenes where Estatus in (1,11,12,15,3,4,5,6))
						AND CV_RUTA=@TipoFormulario
						GROUP BY TX_NOMBRE_DESPACHO, cv_delegacion
					) C ON C.TX_NOMBRE_DESPACHO = d.nom_corto
					join CatDelegaciones z on c.CV_DELEGACION = z.Delegacion
					WHERE d.idDominio > 1 AND d.Estatus = 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				end
									
				/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
		
				SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus IN (1,11,12,15,6)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.CV_DELEGACION = @fc_Delegacion
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

				/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
		
				SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(4)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.CV_DELEGACION = @fc_Delegacion
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
		
				SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(3)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.CV_DELEGACION = @fc_Delegacion
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario

				/*++++++++ Calcular total de reasignados ++++++++++*/
		
				SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				join Respuestas r on o.idOrden = r.idOrden
				WHERE o.Estatus in(3)
				and r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.CV_DELEGACION = @fc_Delegacion
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario
				
				/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
				
				SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden),0)
				FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
				WHERE o.Estatus in(6)
				and o.idUsuario <> 0
				AND o.idDominio > 1
				and c.CV_DELEGACION = @fc_Delegacion
				and c.TX_NOMBRE_DESPACHO = @fc_Despacho
				AND c.CV_RUTA=@TipoFormulario
			END
			ELSE
			BEGIN
				IF @fc_idUsuario = '%' -- La consulta es por supervisor
				BEGIN
					PRINT 'TODO NULO MENOS DELEGACION Y DESPACHO Y USUARIO PADRE'
					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/

					SELECT @nTotal_London_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					
					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/

					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/

					SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden),0), @nTotal_Gestores_Visitantes_Count = isnull(COUNT(distinct o.idUsuario),0), @nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3,4,6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/

					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					and o.idVisita > 1
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					and o.idUsuario = 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

					SET @nTotal_Sin_Orden_Count = 0
															
					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15,6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/

					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(4)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
			
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de reasignados ++++++++++*/
			
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					join Respuestas r on o.idOrden = r.idOrden
					WHERE o.Estatus in(3)
					and r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario
					
					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					
					SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					AND c.CV_RUTA=@TipoFormulario

				END
				ELSE
				BEGIN
					/*++++++++ Calcular total de pool para datos enviados ++++++++++*/
					PRINT 'TODO NULO MENOS DELEGACION Y DESPACHO Y USUARIO HIJO'
					IF @fc_idUsuarioPadre = '%'
					BEGIN
						select @fc_idUsuarioPadre = idPadre from RelacionUsuarios where idHijo = @fc_idUsuario
					END
				
					SELECT @nTotal_London_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
				
					/*++++++++ Calcular total de asignados a moviles +++++++++++++++*/
	
					SELECT @nTotal_Ordenes_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(1,11,12,15,3,4,5,6)
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
				
					/*++++++++ Calcular total de visitados ++++++++++++++++++++++++*/

					SELECT @nTotal_Visitados_Count = isnull(count(o.idOrden),0), @nTotal_Gestores_Visitantes_Count = isnull(COUNT(distinct o.idUsuario),0), @nTotal_Visitas_Realizadas_Count = isnull(SUM(o.idVisita),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3,4,6)
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de pendientes por revisitar ++++++++++++++++++++++++*/

					SELECT @nTotal_ReVisitados_Count = isnull(count(o.idOrden),0), @nTotal_Visitas_Realizadas_Count= @nTotal_Visitas_Realizadas_Count+isnull(sum(o.idVisita-1),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					and o.idVisita > 1
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar a un movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Sin_Asig_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15)
					and o.idUsuario = 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de creditos sin asignar (sin orden)  ++++++++++++++++++++++++*/

					SET @nTotal_Sin_Orden_Count = 0
															
					/*++++++++ Calcular total de creditos en movil ++++++++++++++++++++++++*/
			
					SELECT @nTotal_Cred_Asig_Mov_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus IN (1,11,12,15,6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario
					AND c.CV_RUTA=@TipoFormulario
				
					/*+++++++ Calcular total de finalizados +++++++++++++++++++++++*/
				
					SELECT @nTotal_Finalizados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(4)
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario				
					AND c.CV_RUTA=@TipoFormulario
					
					/*++++++++ Calcular total de visitados sin autorizar++++++++++*/
		
					SELECT @nTotal_Visitados_NoAuth_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(3)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario				
					AND c.CV_RUTA=@TipoFormulario

					/*++++++++ Calcular total de reasignados ++++++++++*/
			
					SELECT @nTotal_Reasignados_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					join Respuestas r on o.idOrden = r.idOrden
					WHERE o.Estatus in(3)
					and r.idCampo in (select idcampo from CamposRespuesta where Nombre in ('DictamenNopueaten','DictamenAcreditadoNoDisponible','Dictamentercvissincont','Dictamenavisoretencion','DictamenRecado','DictamenVisSContactoIDV2','DictamenVAdicIV2','DictamenIDV2AcNoDisponible') ) 
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario				
					AND c.CV_RUTA=@TipoFormulario
					
					/*++++++++ Calcular total de ordenes sincronizando  ++++++++++*/
					
					SELECT @nTotal_Sincro_Count = isnull(count(o.idOrden),0)
					FROM creditos c left join Ordenes o on c.CV_CREDITO = o.num_Cred
					WHERE o.Estatus in(6)
					and o.idUsuario <> 0
					AND o.idDominio > 1
					and c.CV_DELEGACION = @fc_Delegacion
					and c.TX_NOMBRE_DESPACHO = @fc_Despacho
					and O.IdUsuarioPadre = @fc_idUsuarioPadre
					and O.IdUsuario = @fc_idUsuario				
					AND c.CV_RUTA=@TipoFormulario

				END
			END
		end
		/*ººººººººººººººº Inicio: Parte 1 ºººººººººººººººººººººººººº*/		

				if @Bloque=0
				BEGIN
				
					--+++++++++++++++ inicia: CREDITOS ASIGNADOS POR POOL ++++++--
				
					set @SubAccion = 'Creditos Asignados por Pool y TDAM'
					
					if @nTotal_London_Count = 0 
						set @nTotal_London_Porc = 0
					else
						set @nTotal_London_Porc = 100
					
					--+++++++++++++++ final: CREDITOS ASIGNADOS POR POOL +++++++--
		
					--+++++++++++++++ inicia: CREDITOS ASIGNADOS POR MOViL +++++++--
						
					set @SubAccion = 'Creditos Asignados por Movil y CAGD'
				
					
					set @nTotal_London_Porc = 100
					if @nTotal_London_Count = 0 
						set @nTotal_Ordenes_Porc = 0
					else			
						set @nTotal_Ordenes_Porc = (@nTotal_Ordenes_Count * 100) / @nTotal_London_Count
						
					--++++++++++++++ Final: CREDITOS ASIGNADOS POR MOViL ++++++++--
			
					select @nTotal_London_Count value,@nTotal_London_Porc porcentaje
					,ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_CREDASIGPOOL'
					UNION ALL			
					select @nTotal_Ordenes_Count value,@nTotal_Ordenes_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_CREDASIGMOVIL'
					
				END
		
		/*ººººººººººººººººººº Final: Parte 1 ººººººººººººººººººººººººººººººººººººº*/
		
		/*ººººººººººººººººººº Inicio: Parte 2 ºººººººººººººººººººººººººººººººººººº*/
		
				IF @Bloque=1
				BEGIN
					
					--++++++++ Calculos: Créditos Visitados, pendientes por revisitar y no Visitados +++++++++++++++++++--
					set @SubAccion = 'Creditos Visitados'
					set @nTotal_London_Porc = 100
						
					if @nTotal_London_Count = 0 
						set @nTotal_Visitados_Porc = 0
					else			
						set @nTotal_Visitados_Porc = (@nTotal_Visitados_Count * 100) / @nTotal_London_Count
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					--++++++++ Calculos: Creditos pendientes por revisitar +++++++++++++++++++--
					set @SubAccion = 'Creditos pendientes por revisitar'
					set @nTotal_London_Porc = 100
					
					if @nTotal_London_Count = 0 
						set @nTotal_ReVisitados_Porc = 0
					else			
						set @nTotal_ReVisitados_Porc = (@nTotal_ReVisitados_Count * 100) / @nTotal_London_Count
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					--++++++++ Calculos: Creditos sin asignar en movil +++++++++++++++++++--
					set @SubAccion = 'Creditos sin asignar en movil'
				
					set @nTotal_London_Porc = 100
					
					if @nTotal_London_Count = 0 
						set @nTotal_Cred_Sin_Asig_Porc = 0
					else			
						set @nTotal_Cred_Sin_Asig_Porc = (@nTotal_Cred_Sin_Asig_Count * 100) / @nTotal_London_Count
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					--++++++++ Calculos: Creditos sin asignar sin orden +++++++++++++++++++--
					set @SubAccion = 'Creditos sin asignar sin orden'
					set @nTotal_London_Porc = 100
				
					if @nTotal_London_Count = 0 
						set @nTotal_Sin_Orden_Porc = 0
					else			
						set @nTotal_Sin_Orden_Porc = (@nTotal_Sin_Orden_Count * 100) / @nTotal_London_Count		
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					--++++++++ Calculos: Creditos en movil +++++++++++++++++++--
					set @SubAccion = 'Creditos en movil'
				
					set @nTotal_London_Porc = 100
					
					if @nTotal_London_Count = 0 
						set @nTotal_Cred_Asig_Mov_Porc = 0
					else			
						set @nTotal_Cred_Asig_Mov_Porc = (@nTotal_Cred_Asig_Mov_Count * 100) / @nTotal_London_Count
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--			
							
							
				
					--+++++++++++++++++++ inicia: Dias del Mes +++++++++++++++++++--		
					SELECT day(dateadd(mm,datediff(mm,-1,GETDATE()),-1)) value , 100 porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_TOTALMES'
					--+++++++++++++++++++ fin: Dias del Mes +++++++++++++++++++--
					Union all
					--+++++++++++++++++++ inicia: Dias restantes +++++++++++++++++++--	
					SELECT day(dateadd(mm,datediff(mm,-1,GETDATE()),-1)) - DAY(getdate()) value, 
					abs(((day(dateadd(mm,datediff(mm,-1,GETDATE()),-1)) - DAY(getdate())) * 100) / datediff(day, GETDATE(), dateadd(month, 1, getdate()))) porcentaje,
					 ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_DIASREST'
					--+++++++++++++++++++ Fin: Dias restantes +++++++++++++++++++--
					Union All
					--++++++++ inicia: Créditos Visitados, pendientes por revisitar y no Visitados +++++++++++++++++++--
					select @nTotal_Visitados_Count value, @nTotal_Visitados_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_CREDVISITADOS'
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					Union All
					--++++++++ inicia: Creditos pendientes por revisitar +++++++++++++++++++--			
					select @nTotal_ReVisitados_Count value, @nTotal_ReVisitados_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_GESTNVAVISITA' AND ((@esCallCenter = 'true' AND @fc_DashBoard <> 'Gestor') or (@esCallCenter='false'))
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					Union All
					--++++++++ inicia: Créditos sin asignar, en movil +++++++++++++++++++--
					select @nTotal_Cred_Sin_Asig_Count value, @nTotal_Cred_Sin_Asig_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_CREDSINASIG' AND ((@esCallCenter = 'true' AND @fc_DashBoard <> 'Gestor') or (@esCallCenter='false'))
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					Union All
					--++++++++ inicia: Créditos sin asignar, sin orden +++++++++++++++++++--			
					select @nTotal_Sin_Orden_Count value, @nTotal_Sin_Orden_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_CREDSINORD' and (@fc_DashBoard <> 'Supervisor' and( (@esCallCenter='false' and @fc_DashBoard <> 'Gestor') or (@esCallCenter='true' and @fc_DashBoard = 'Gestor') )) AND ((@esCallCenter = 'true' AND @fc_DashBoard <> 'Gestor') or (@esCallCenter='false'))
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					Union All									
					--++++++++ Inicia: Creditos en movil +++++++++++++++++++--
					select @nTotal_Cred_Asig_Mov_Count value, @nTotal_Cred_Asig_Mov_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_CREDENMOVIL' AND ((@esCallCenter = 'true' AND @fc_DashBoard <> 'Gestor') or (@esCallCenter='false'))
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
				END
				
		/*ººººººººººººººººººº Fin : Parte 2 ºººººººººººººººººººººººººººººººººººº*/
		
		/*ººººººººººººººººººº Inicio : Parte 3 ºººººººººººººººººººººººººººººººººººº*/											

				IF @Bloque=2
				BEGIN
					--++++++++ Calculos: Visitas Realizadas +++++++++++++++++++--
					set @SubAccion = 'Visitas Realizadas'
								
					if @nTotal_Visitas_Realizadas_Count = 0
						set @nTotal_Visitas_Realizadas_Porc = 0
					else
						set @nTotal_Visitas_Realizadas_Porc = 100
						
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					--++++++++ Calculos: Gestiones autorizadas +++++++++++++++++++--
					set @SubAccion = 'Gestiones autorizadas'
				
					if @nTotal_Finalizados_Count = 0 
						set @nTotal_Finalizados_Porc = 0
					else			
						set @nTotal_Finalizados_Porc = (@nTotal_Finalizados_Count * 100) / @nTotal_London_Count
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					--++++++++ Calculos: Gestiones no autorizadas +++++++++++++++++++--
					set @SubAccion = 'Gestiones no autorizadas'

					if @nTotal_Visitados_NoAuth_Count = 0 
						set @nTotal_Visitados_NoAuth_Porc = 0
					else			
						set @nTotal_Visitados_NoAuth_Porc = (@nTotal_Visitados_NoAuth_Count * 100) / @nTotal_London_Count
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					--++++++++ Calculos: Gestiones por reasignar +++++++++++++++++++--
					set @SubAccion = 'Gestiones por reasignar'

					if @nTotal_Reasignados_Count = 0 
						set @nTotal_Reasignados_Porc = 0
					else			
						set @nTotal_Reasignados_Porc = (@nTotal_Reasignados_Count * 100) / @nTotal_London_Count
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					--++++++++ Calculos: Gestiones en sincronizando +++++++++++++++++++--
					set @SubAccion = 'Gestiones en sincronizando'

					if @nTotal_Sincro_Count = 0 
						set @nTotal_Sincro_Porc = 0
					else			
						set @nTotal_Sincro_Porc = (@nTotal_Sincro_Count * 100) / @nTotal_London_Count
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--					
					--++++++++ Calculos: Gestiones visitadas promedio +++++++++++++++++++--
					set @SubAccion = 'Gestiones visitadas promedio'
				
					if @nTotal_Visitados_Count > 0 and @nTotal_Gestores_Visitantes_Count > 0
					begin
						set @nTotal_Promedio_Visitas_X_Gestor_Count = (@nTotal_Visitados_Count / @nTotal_Gestores_Visitantes_Count)
						set @nTotal_Promedio_visitas_X_Gestor_Porc = (@nTotal_Promedio_Visitas_X_Gestor_Count * 100) / @nTotal_London_Count
					end
					else			
					begin
						set @nTotal_Promedio_Visitas_X_Gestor_Count = 0
						set @nTotal_Promedio_Visitas_X_Gestor_Porc = 0
					end			
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
				
				
				
					--+++++++++++ inicia: Visitas Realizadas +++++++++++++++++--
					select @nTotal_Visitas_Realizadas_Count value, @nTotal_Visitas_Realizadas_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_VISITADOSREAL'
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					UNION ALL
					--+++++++++++ inicia: Gestiones autorizadas +++++++++++++++++--
					select @nTotal_Finalizados_Count value , @nTotal_Finalizados_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_CREDCONSOLUCI' AND ((@esCallCenter = 'true' AND @fc_DashBoard <> 'Gestor') or (@esCallCenter='false'))
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					UNION ALL
					--+++++++++++ inicia: Gestiones no autorizadas +++++++++++++++++--
					select @nTotal_Visitados_NoAuth_Count value , @nTotal_Visitados_NoAuth_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_GESTSINAUTH' AND ((@esCallCenter = 'true' AND @fc_DashBoard <> 'Gestor') or (@esCallCenter='false'))
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					UNION ALL
					--+++++++++++ inicia: Gestiones por reasignar +++++++++++++++++--
					select @nTotal_Reasignados_Count value, @nTotal_Reasignados_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_GESTREASIG' AND ((@esCallCenter = 'true' AND @fc_DashBoard <> 'Gestor') or (@esCallCenter='false'))
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--										
					UNION ALL
					--+++++++++++ inicia: Gestiones visitadas promedio +++++++++++++++++--
					select @nTotal_Promedio_Visitas_X_Gestor_Count value , @nTotal_Promedio_Visitas_X_Gestor_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_GESTVISPROM' AND ((@esCallCenter = 'true' AND @fc_DashBoard <> 'Gestor') or (@esCallCenter='false'))
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					UNION ALL
					--+++++++++++ inicia: Gestiones en sincronizando +++++++++++++++++--
					select @nTotal_Sincro_Count value, @nTotal_Sincro_Porc porcentaje,
					ud.fc_Clave as fc_Clave,ud.fc_Descripcion as descripcion
					,ud.fi_Parte as fi_Parte
					from [Utils_Descripciones] ud
					where ud.fc_Clave = 'DASH_GESTSINCRO'
					--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--
					
				END		

		/*ºººººººººººººººººººº Final: Parte 3 ºººººººººººººººººººººººººººººººººººººº*/
					
	END TRY
	BEGIN CATCH	
		Set @Error = 'Mod: ' +  ISNULL(@Accion,'') + ' SubMod: ' + ISNULL(@SubAccion,'') 
						+ ' DashBoard: ' + ISNULL(@fc_DashBoard,'') + ' Nom_Corto: ' + ISNULL(@fc_NombreCorto,'')
						+ ERROR_MESSAGE()
		goto Error			
	END CATCH 
	
	--Final del proceso...
	return;
	
	Error:
	Raiserror(@Error,1,1)
	return;
END --Final del Procedure
