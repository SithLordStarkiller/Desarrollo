-- =============================================
-- Proyecto:    London-PubliPayments-Formiik
-- Author:		Pablo Rendon
-- Create date: 15/09/2014
-- Description:	Revisa si nombre corto del dominio no exista en otro
-- =============================================
CREATE PROCEDURE [dbo].[ValidarDominioNomCorto](
				 @nomCorto nvarchar(40),
				 @idDominio int
				)
AS
BEGIN
	DECLARE
		@nomDom nvarchar(100),
		@idDominioE int

	SELECT 
		@idDominioE = idDominio
	FROM dominio 
	WHERE UPPER(nom_corto) = UPPER(@nomCorto)

	IF @idDominioE IS NOT NULL and @idDominioE != @idDominio
	BEGIN
		SELECT 1 nomCorto
	END
	ELSE
	BEGIN
		SELECT -1 nomCorto
	END
END

