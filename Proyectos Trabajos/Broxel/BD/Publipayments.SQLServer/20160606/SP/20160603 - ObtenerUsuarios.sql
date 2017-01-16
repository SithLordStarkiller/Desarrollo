
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2016/06/03
-- Description:	Obtiene los usuarios registrados en el sistema de acuerdo a los filtros necesarios
-- =============================================
CREATE PROCEDURE ObtenerUsuarios
				(	@idDominio  INT =-1
					,@idPadre INT =-1
					,@idRol INT =-1
					,@Estatus INT =-1
					,@Delegacion VARCHAR(2) ='%')
	
AS
BEGIN
	
	SET NOCOUNT ON;

    
		SELECT distinct vu.idusuario,vu.idDominio,vu.idRol,vu.Usuario,vu.nombre,vu.email,vu.password,vu.Alta,vu.UltimoLogin,vu.Estatus,vu.Intentos,vu.Bloqueo 
		FROM  vusuarios vu
		WHERE vu.iddominio= CASE  WHEN  @idDominio >= 0 THEN  @idDominio ELSE vu.iddominio END
		AND vu.idPadre = CASE  WHEN  @idPadre >= 0 THEN  @idPadre ELSE vu.idPadre END
		AND vu.idRol = CASE  WHEN  @idRol >= 0 THEN  @idRol ELSE vu.idRol END
		AND vu.Estatus = CASE  WHEN  @Estatus >= 0 THEN  @Estatus ELSE vu.Estatus END

END
GO
