/*********************************************************************
* Proyecto:				London-PubliPayments
* Autor:				Pablo Rendon
* Fecha de creación:	09/09/2015
* Descripción:			Actualizar datos dominio por idDominio
**********************************************************************/
CREATE PROCEDURE [dbo].[ActualizarDatosDominioXidDominio]
(
	@idDominio int,
	@nombreDominio nvarchar(100),
	@nom_corto nvarchar(40),
	@estatus int
)
AS BEGIN
	DECLARE	
	@dominio int

	SELECT @dominio = idDominio
	FROM dominio
	WHERE idDominio = @idDominio

	if @dominio > 0 or @dominio is not null
	begin
		UPDATE dominio
		SET NombreDominio = @nombreDominio,
		nom_corto = @nom_corto,
		Estatus = @estatus
		where idDominio = @dominio
		
		select 1 'R'
	end
	else
		begin
			select -1 'R'
		end
end