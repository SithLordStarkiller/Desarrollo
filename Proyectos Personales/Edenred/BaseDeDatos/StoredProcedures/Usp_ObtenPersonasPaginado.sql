SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Cruz Lagunes
-- Create date: 04/11/2015
-- Description:	Store procedure dedicado a obtener personas de forma paginada
-- =============================================
CREATE PROCEDURE Usp_ObtenPersonasPaginado 
	-- Add the parameters for the stored procedure here


AS
BEGIN

	SET NOCOUNT ON;

	SELECT * FROM PER_PERSONAS

END
GO
