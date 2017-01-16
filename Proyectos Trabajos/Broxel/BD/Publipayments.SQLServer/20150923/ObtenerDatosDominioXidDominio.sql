/*********************************************************************
* Proyecto:				London-PubliPayments
* Autor:				Pablo Rendon
* Fecha de creación:	09/09/2015
* Descripción:			Obtener datos dominio por idDominio
**********************************************************************/
CREATE PROCEDURE ObtenerDatosDominioXidDominio
(
	@idDominio int
)
AS BEGIN
	SELECT nombreDominio,nom_corto,estatus 
	FROM dominio
	WHERE idDominio = @idDominio
end