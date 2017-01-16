
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarioPorDelegacion]    Script Date: 03/06/2016 09:56:49 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/05/22
-- Description:	obtiene los usuarios que pertenecen a una delegacion y el rol deseado.
-- @idUsuario : usuario padre o usuario que esta realcionado a una delegacion, la busqueda se hace referenciando este dato
-- @idRol :		rol de los usuarios que se necesiten recuperar por el momento solo es para supervisores
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerUsuarioPorDelegacion] (@idUsuarioPadre int=0,@idRol int=3)
	
AS
BEGIN
	
	SET NOCOUNT ON;
	DECLARE @Delegacion INT =0
	   SELECT  * FROM usuario  WITH (NOLOCK) WHERE idrol =  CASE  WHEN @idRol > 0 THEN @idRol ELSE  idRol END
		AND idUsuario IN (
							  SELECT o.idUsuarioPadre FROM Creditos c  WITH (NOLOCK) inner join ordenes o  WITH (NOLOCK) on o.num_cred= c.CV_CREDITO where CV_DELEGACION 
							  IN ( select Delegacion from RelacionDelegaciones  WITH (NOLOCK) where idUsuario=@idUsuarioPadre) AND o.idusuario>0 
		) and Estatus!=0 ORDER BY iddominio ASC
END
