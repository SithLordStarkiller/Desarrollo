-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 29/10/2015
-- Description:	Eliminar registro de personas
-- =============================================
CREATE PROCEDURE Usp_EliminarPersona 
	-- Add the parameters for the stored procedure here
	@IdPersona int = 0
AS
	BEGIN
		DELETE FROM PER_PERSONAS WHERE IDPERSONA = @IdPersona
	END
GO