-- =============================================
-- Author:		Pablo Rendon
-- Create date: 2015/10/05
-- Description:	Obtine los municipios por delagacion
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerMunicipiosXDelegacion]  @Delegacion nvarchar(100)
AS
BEGIN
DECLARE
	@idDelegacion varchar(2)

	select @idDelegacion = Delegacion from catdelegaciones with(nolock)
	where Descripcion = @Delegacion

	SELECT DISTINCT TX_MUNICIPIO municipio FROM creditos with(nolock)
	WHERE CV_DELEGACION = @idDelegacion
	ORDER BY 1 
END