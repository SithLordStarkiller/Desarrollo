
/****** Object:  StoredProcedure [dbo].[ObtenerCombos_AutorizaImagenes]    Script Date: 25/01/2016 04:57:26 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 26/01/2016
-- Description:	Obtiene los datos necesarios para crar filtros 
-- =============================================
CREATE  PROCEDURE [dbo].[ObtenerCombosFiltro] 
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
		IF @delegacion = '9999' OR @delegacion IS NULL
			SET @SQL = 'SELECT idUsuario,Usuario from Usuario WITH(NOLOCK) where idRol=3 and Estatus<>0 and idDominio=' + cast(@despacho AS VARCHAR(4)) + ' order by 2'
		ELSE
			SET @SQL = 'SELECT distinct(u.idUsuario),u.Usuario from Ordenes o WITH(NOLOCK) INNER JOIN Creditos c WITH(NOLOCK) on c.CV_CREDITO=o.num_Cred INNER JOIN Usuario u WITH(NOLOCK) on u.idUsuario=o.idUsuarioPadre where o.idDominio=''' + cast(@despacho AS VARCHAR(4)) + ''' and c.CV_DELEGACION=''' + @delegacion + ''' order by 2'
	END
	ELSE
	BEGIN
		IF @idCombo = 'despachoCombo'
			BEGIN
				BEGIN
					IF @delegacion = '9999' OR @delegacion IS NULL
						SET @SQL = 'SELECT  DISTINCT(d.idDominio),d.NombreDominio  FROM creditos c INNER JOIN dominio d ON d.nom_corto=c.tx_nombre_despacho order by 2'
					ELSE
						SET @SQL = 'SELECT  DISTINCT(d.idDominio),d.NombreDominio  FROM creditos c INNER JOIN dominio d ON d.nom_corto=c.tx_nombre_despacho WHERE c.CV_DELEGACION=''' + @delegacion + ''' order by 2'
				END
			END
			ELSE
			BEGIN
				IF @idCombo = 'delegacionCombo'
				BEGIN
					SET @SQL = 'select Delegacion,Descripcion from CatDelegaciones'
				END
			END
	END

	EXECUTE sp_sqlexec @SQL
END
