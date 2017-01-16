
-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/05/22
-- Description:	obtiene los usuarios que pertenecen a una delegacion y el rol deseado.
-- @idUsuario : usuario padre o usuario que esta realcionado a una delegacion, la busqueda se hace referenciando este dato
-- @idRol :		rol de los usuarios que se necesiten recuperar
-- =============================================
CREATE PROCEDURE ObtenerUsuarioPorDelegacion (@idUsuarioPadre int=0,@idRol int=0)
	
AS
BEGIN
	
	SET NOCOUNT ON;
	DECLARE @Delegacion INT =0
	   SELECT  * FROM usuario WHERE idrol =  CASE  WHEN @idRol > 0 THEN @idRol ELSE  idRol END
		AND idUsuario IN (
							  SELECT o.idUsuarioPadre FROM Creditos c inner join ordenes o on o.num_cred= c.CV_CREDITO where CV_DELEGACION 
							  IN ( select Delegacion from RelacionDelegaciones where idUsuario=@idUsuarioPadre) 
		) and Estatus!=0 ORDER BY iddominio ASC
END
GO
