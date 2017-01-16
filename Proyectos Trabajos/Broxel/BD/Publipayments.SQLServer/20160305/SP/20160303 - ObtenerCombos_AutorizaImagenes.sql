
/****** Object:  StoredProcedure [dbo].[ObtenerCombos_AutorizaImagenes]    Script Date: 25/02/2016 05:37:50 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 26/01/2015
-- Description:	Consulta y regresa tabla con valores necesarios para llenar combos de Auditoria Imagenes
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerCombos_AutorizaImagenes] 
	@idCombo VARCHAR(20), 
	@tipoCombo INT, 
	@delegacion VARCHAR(4), 
	@despacho INT, 
	@supervisor INT
AS
BEGIN
	DECLARE @SQL VARCHAR(max)

	IF @idCombo = 'supervisorCombo'
	BEGIN
		IF @tipoCombo <> 3
			AND @tipoCombo <> 1
		BEGIN
			SET @SQL = 'select ''9999'' as Nombre,''Error al cargar el filtro'' as Valor'
		END
		ELSE
		BEGIN
			IF @delegacion = '9999'
				OR @delegacion IS NULL
			BEGIN
				PRINT @despacho

				SET @SQL = 'select idUsuario,Usuario from Usuario WITH(NOLOCK) where idRol=3 and Estatus<>0 and idDominio=' + cast(@despacho AS VARCHAR(4)) + ' order by 2'
			END
			ELSE
			BEGIN
				SET @SQL = 'select distinct(u.idUsuario),u.Usuario from Ordenes o WITH(NOLOCK) left join Creditos c WITH(NOLOCK) on c.CV_CREDITO=o.num_Cred left join Usuario u WITH(NOLOCK) on u.idUsuario=o.idUsuarioPadre where o.idDominio=''' + cast(@despacho AS VARCHAR(4)) + ''' and c.CV_DELEGACION=''' + @delegacion + ''' order by 2'
			END
		END
	END
	ELSE
	BEGIN
		IF @idCombo = 'gestorCombo'
		BEGIN
			IF @tipoCombo <> 4
				AND @tipoCombo <> 1
			BEGIN
				SET @SQL = 'select ''9999'' as Nombre,''Error al cargar el filtro'' as Valor'
			END
			ELSE
			BEGIN
				IF @delegacion = '9999'
					OR @delegacion IS NULL
				BEGIN
					SET @SQL = 'select distinct(u.idUsuario),u.Usuario from Ordenes o WITH(NOLOCK) left join Creditos c WITH(NOLOCK) on c.CV_CREDITO=o.num_Cred left join Usuario u WITH(NOLOCK) on u.idUsuario=o.idUsuario where o.idDominio=''' + cast(@despacho AS VARCHAR(4)) + ''' and o.idUsuarioPadre=''' + cast(@supervisor AS VARCHAR(4)) + ''' order by 2 '
				END
				ELSE
				BEGIN
					SET @SQL = 'select distinct(u.idUsuario),u.Usuario from Ordenes o WITH(NOLOCK) left join Creditos c  WITH(NOLOCK) on c.CV_CREDITO=o.num_Cred left join Usuario u  WITH(NOLOCK) on u.idUsuario=o.idUsuario where o.idDominio=''' + cast(@despacho AS VARCHAR(4)) + ''' and c.CV_DELEGACION=''' + @delegacion + ''' and o.idUsuarioPadre=''' + cast(@supervisor AS VARCHAR(4)) + ''' order by 2 '
				END
			END
		END
		ELSE
		BEGIN
			IF @idCombo = 'despachoCombo'
			BEGIN
				IF @tipoCombo <> 2
				BEGIN
					SET @SQL = 'select ''9999'' as Nombre,''Error al cargar el filtro'' as Valor'
				END
				ELSE
				BEGIN
					IF @delegacion = '9999'
						OR @delegacion IS NULL
					BEGIN
						SET @SQL = 'select distinct(d.idDominio),(d.nom_corto + '' - '' + d.NombreDominio) AS NombreDominio from Ordenes o WITH(NOLOCK) left join Creditos c WITH(NOLOCK) on c.CV_CREDITO=o.num_Cred left join Dominio d WITH(NOLOCK) on d.idDominio=o.idDominio order by 2'
					END
					ELSE
					BEGIN
						SET @SQL = 'select distinct(d.idDominio),(d.nom_corto + '' - '' + d.NombreDominio) AS NombreDominio from Ordenes o WITH(NOLOCK) left join Creditos c WITH(NOLOCK) on c.CV_CREDITO=o.num_Cred left join Dominio d WITH(NOLOCK) on d.idDominio=o.idDominio where c.CV_DELEGACION=''' + @delegacion + ''' order by 2'
					END
				END
			END
			ELSE
			BEGIN
				IF @idCombo = 'delegacionCombo'
				BEGIN
					IF @tipoCombo <> 5
					BEGIN
						SET @SQL = 'select ''9999'' as Nombre,''Error al cargar el filtro'' as Valor'
					END
					ELSE
					BEGIN
						SET @SQL = 'select Delegacion,Descripcion from CatDelegaciones WITH(NOLOCK) '
					END
				END
				ELSE
				BEGIN
					IF @idCombo = 'dictamenCombo'
					BEGIN
						SET @SQL = 
							'select distinct valor as Value,Valor as Description from CatDictamenRespuesta WITH(NOLOCK) order by 1'
					END
					ELSE
					BEGIN
						IF @idCombo = 'autorizacionCombo'
						BEGIN
							SET @SQL = 
								'SELECT DISTINCT CAST(Estatus as varchar(2)) + Tipo as Value	,CASE WHEN o.Tipo = ''S'' THEN ceo.Estado + '' SMS'' WHEN o.Tipo = ''C'' THEN ceo.Estado + '' Call Center'' ELSE ceo.Estado END Description FROM Ordenes o WITH(NOLOCK) LEFT JOIN CatEstatusOrdenes ceo WITH(NOLOCK) ON ceo.Codigo = o.Estatus '
						END
						ELSE
						BEGIN
							SET @SQL = 'select ''9999'' as Nombre,''Error al cargar el filtro'' as Valor'
						END
					END
				END
			END
		END
	END

	EXECUTE sp_sqlexec @SQL
END
