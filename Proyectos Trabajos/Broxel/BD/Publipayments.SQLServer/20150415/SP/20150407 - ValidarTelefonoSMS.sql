
GO
/****** Object:  StoredProcedure [dbo].[ValidarTelefonoSMS]    Script Date: 07/04/2015 02:26:50 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Rojas
-- Create date: 2015/02/27
-- Description:	valida si un numero de telefono puede ser utilizado
-- Modificacion: 2015-04-13  JARO:  Validacion mas completa , se relaciona el numero de telefono con el credito solo sera uno a uno
-- =============================================
ALTER PROCEDURE [dbo].[ValidarTelefonoSMS]
				@Telefono char(10),
				@Credito nvarchar(15)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @CredAnt nvarchar(15)

		SELECT @CredAnt=num_Cred FROM AutorizacionSMS WHERE Telefono=@Telefono
	
	IF @CredAnt is not null
	BEGIN 
		IF(@CredAnt = @Credito )
			BEGIN	
				SELECT  0 as invalido  
			END
			ELSE
			BEGIN
				SELECT  1 as invalido  
			END
		END
	ELSE
	BEGIN
		SELECT 0 as invalido
	END

	SET NOCOUNT OFF;

END
