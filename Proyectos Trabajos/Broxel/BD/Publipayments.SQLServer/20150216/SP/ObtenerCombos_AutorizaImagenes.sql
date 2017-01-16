
/****** Object:  StoredProcedure [dbo].[ObtenerCombos_AutorizaImagenes]    Script Date: 02/10/2015 08:27:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Pablo Jaimes
-- Create date: 26/01/2015
-- Description:	Consulta y regresa tabla con valores necesarios para llenar combos de Auditoria Imagenes
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerCombos_AutorizaImagenes] @idCombo VARCHAR(20), @tipoCombo INT, @delegacion VARCHAR(4), @despacho INT, @supervisor INT
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

				SET @SQL = 'select idUsuario,Usuario from Usuario where idRol=3 and Estatus<>0 and idDominio=' + cast(@despacho AS VARCHAR(4)) + ' order by 2'
			END
			ELSE
			BEGIN
				SET @SQL = 'select distinct(u.idUsuario),u.Usuario from Ordenes o left join Creditos c on c.CV_CREDITO=o.num_Cred left join Usuario u on u.idUsuario=o.idUsuarioPadre where  o.Estatus=4 and o.idDominio=''' + cast(@despacho AS VARCHAR(4)) + ''' and c.CV_DELEGACION=''' + @delegacion + ''' order by 2'
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
					SET @SQL = 'select distinct(u.idUsuario),u.Usuario from Ordenes o left join Creditos c on c.CV_CREDITO=o.num_Cred left join Usuario u on u.idUsuario=o.idUsuario where  o.Estatus=4 and o.idDominio=''' + cast(@despacho AS VARCHAR(4)) + ''' and o.idUsuarioPadre=''' + cast(@supervisor AS VARCHAR(4)) + ''' order by 2 '
				END
				ELSE
				BEGIN
					SET @SQL = 'select distinct(u.idUsuario),u.Usuario from Ordenes o left join Creditos c on c.CV_CREDITO=o.num_Cred left join Usuario u on u.idUsuario=o.idUsuario where  o.Estatus=4 and o.idDominio=''' + cast(@despacho AS VARCHAR(4)) + ''' and c.CV_DELEGACION=''' + @delegacion + ''' and o.idUsuarioPadre=''' + cast(@supervisor AS VARCHAR(4)) + ''' order by 2 '
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
						SET @SQL = 'select distinct(d.idDominio),d.NombreDominio from Ordenes o left join Creditos c on c.CV_CREDITO=o.num_Cred left join Dominio d on d.idDominio=o.idDominio where  o.Estatus=4 order by 2'
					END
					ELSE
					BEGIN
						SET @SQL = 'select distinct(d.idDominio),d.NombreDominio from Ordenes o left join Creditos c on c.CV_CREDITO=o.num_Cred left join Dominio d on d.idDominio=o.idDominio where  o.Estatus=4 and c.CV_DELEGACION=''' + @delegacion + ''' order by 2'
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
						SET @SQL = 'select Delegacion,Descripcion from CatDelegaciones'
					END
				END
				ELSE
				BEGIN
					IF @idCombo = 'dictamenCombo'
					BEGIN
						SET @SQL = 
							'select distinct(case when cast(estFinal as varchar(10))=''381'' then ''381,392'' when cast(estFinal as varchar(10))=''392'' then ''381,392'' when cast(estFinal as varchar(10))=''126'' then ''126,424'' when cast(estFinal as varchar(10))=''424'' then ''126,424'' when cast(estFinal as varchar(10))=''413'' then ''404,413'' when cast(estFinal as varchar(10))=''404'' then ''404,413'' when cast(estFinal as varchar(10))=''410'' then ''157,410'' when cast(estFinal as varchar(10))=''157'' then ''157,410'' when cast(estFinal as varchar(10))=''442'' then ''442,136'' when cast(estFinal as varchar(10))=''136'' then ''442,136'' else cast(estFinal as varchar(10)) end)  as Value, isnull(Valor,''Sin Dictamen'') as Description from (select Distinct(idCampo) as estFinal, Valor from( select r.idCampo, r.Valor  from Respuestas r  left join Ordenes o on o.idOrden=r.idOrden   left join Creditos c on c.CV_CREDITO=o.num_Cred  left join CamposRespuesta cr on cr.idCampo=r.idCampo  where o.Estatus=4  and(cr.Nombre like ''Dictamen%'') ) as fre  ) as d '
					END
					ELSE
					BEGIN
						IF @idCombo = 'autorizacionCombo'
						BEGIN
							SET @SQL = 
								'select Codigo as Value,Estado as Description from CatEstatusOrdenes where Codigo in(4,47)'
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
